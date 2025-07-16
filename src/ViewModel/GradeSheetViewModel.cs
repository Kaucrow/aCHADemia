using aCHADemia.Core.Classes;
using aCHADemia.Core.DBComponent;
using aCHADemia.Model;
using aCHADemia.MVVM;
using aCHADemia.View.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using Npgsql;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
// new library for creating PDFs files
// need alias to avoid ambuguity
using iText=iTextSharp.text;
using iTextPdf=iTextSharp.text.pdf;

namespace aCHADemia.ViewModel
{
    internal partial class GradeSheetViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Course> _courses = [];

        [ObservableProperty]
        private Course? _selectedCourse;

        [ObservableProperty]
        private ObservableCollection<Section> _sections = [];

        [ObservableProperty]
        private Section? _selectedSection;

        public List<string> ColumnHeaders { get; } = ["Alumno", "C.I.", "Calificación"];

        [ObservableProperty]
        private ObservableCollection<DataGridRow> _studentRows = [];

        public GradeSheetViewModel()
        {
            GenerateReportCommand = new RelayCommand(execute => GeneratePdfReport());
        }

        public RelayCommand GenerateReportCommand { get; }


        async partial void OnSelectedCourseChanged(Course? value)
        {
            Sections.Clear();
            SelectedSection = null;

            try
            {
                using (DbDataReader reader = await App.Db?.Fetch(
                    DbType.Postgres,
                    Config.Queries.Section.GetByCourse,
                    new NpgsqlParameter("@subject_id", SelectedCourse?.SubjectId)
                ))
                {
                    while (await reader.ReadAsync())
                    {
                        var sectionId = reader.GetInt32(reader.GetOrdinal("section_id"));
                        var name = reader.GetString(reader.GetOrdinal("name"));

                        Sections.Add(new Section
                        {
                            Id = sectionId,
                            Name = name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading sections: {ex.Message}");
            }
        }

        async partial void OnSelectedSectionChanged(Section? value)
        {
            StudentRows.Clear();

            if (value == null) return;

            try
            {
                using (DbDataReader reader = await App.Db?.Fetch(
                    DbType.Postgres,
                    Config.Queries.Student.GetByCourseSection,
                    new NpgsqlParameter("@subject_id", SelectedCourse?.SubjectId),
                    new NpgsqlParameter("@section_id", SelectedSection?.Id)
                ))
                {
                    while (await reader.ReadAsync())
                    {
                        var name = reader.GetString(reader.GetOrdinal("name"));
                        var ci = reader.GetInt32(reader.GetOrdinal("ci"));
                        var grade = reader.GetFloat(reader.GetOrdinal("grade"));

                        var row = new DataGridRow(name, ci.ToString(), grade.ToString());
                        StudentRows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading sections: {ex.Message}");
            }
        }

        public async Task InitializeAsync()
        {
            try
            {
                using (DbDataReader reader = await App.Db?.Fetch(
                    DbType.Postgres,
                    Config.Queries.Course.GetActive
                ))
                {
                    while (await reader.ReadAsync())
                    {
                        var subjectId = reader.GetInt32(reader.GetOrdinal("subject_id"));
                        var name = reader.GetString(reader.GetOrdinal("name"));

                        Courses.Add(new Course
                        {
                            SubjectId = subjectId,
                            Name = name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading courses: {ex.Message}");
            }
        }

        // method to create pdf document

        private void CreatePdfDocument(string filePath)
        {
            // pdf config
            iText.Document document = new iText.Document(iText.PageSize.A4.Rotate(), 20, 20, 30, 30);

            try
            {
                iTextPdf.PdfWriter writer = iTextPdf.PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // fonts and styles
                var titleFont = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 16, iText.BaseColor.DARK_GRAY);
                var headerFont = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 12, iText.BaseColor.WHITE);
                var cellFont = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 11, iText.BaseColor.BLACK);

                // title
                var title = new iText.Paragraph($"PLANILLA DE NOTAS - {SelectedCourse.Name.ToUpper()} - SECCIÓN {SelectedSection.Name}", titleFont)
                {
                    Alignment = iText.Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                document.Add(title);

                // table
                iTextPdf.PdfPTable table = new iTextPdf.PdfPTable(ColumnHeaders.Count);
                table.WidthPercentage = 95;
                table.SetWidths(new float[] { 3, 1, 1 });

                // headers
                foreach (var header in ColumnHeaders)
                {
                    iTextPdf.PdfPCell cell = new iTextPdf.PdfPCell(new iText.Phrase(header, headerFont))
                    {
                        // modify background color through RGB values
                        BackgroundColor = new iText.BaseColor(41, 49, 54),  
                        HorizontalAlignment = iText.Element.ALIGN_CENTER,
                        Padding = 8
                    };
                    table.AddCell(cell);
                }

                // student data
                foreach (var student in StudentRows)
                {
                    foreach (var value in student.Values)
                    {
                        iTextPdf.PdfPCell cell = new iTextPdf.PdfPCell(new iText.Phrase(value, cellFont))
                        {
                            HorizontalAlignment = iText.Element.ALIGN_CENTER,
                            Padding = 6
                        };
                        table.AddCell(cell);
                    }
                }

                document.Add(table);
            }
            catch (iText.DocumentException dex)
            {
                MessageBox.Show($"Error al generar PDF: {dex.Message}");
            }
            catch (IOException iex)
            {
                MessageBox.Show($"Error de archivo: {iex.Message}");
            }
            finally
            {
                document.Close();
            }
        }

        // method to generate pdf 

        private void GeneratePdfReport()
        {
            if (SelectedCourse == null || SelectedSection == null || StudentRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una materia y sección válidas con estudiantes registrados");
                return;
            }

            var saveDialog = new SaveFileDialog
            {
                // filter so it doesnt overrider existing files
                Filter = "Archivos PDF|*.pdf",
                FileName = $"Planilla_{SelectedCourse.Name}_{SelectedSection.Name}.pdf"
            };

            if (saveDialog.ShowDialog() == true)
            {
                CreatePdfDocument(saveDialog.FileName);
                MessageBox.Show("Planilla generada exitosamente!");
            }
        }
    }
}
