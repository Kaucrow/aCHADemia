using aCHADemia.Core.Classes;
using aCHADemia.Core.DBComponent;
using aCHADemia.Model;
using aCHADemia.MVVM;
using CommunityToolkit.Mvvm.ComponentModel;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Windows;

namespace aCHADemia.ViewModel
{
    internal partial class GradeRegistrationViewModel : ViewModelBase
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

        public RelayCommand SaveCommand { get; }

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

                        var row = new DataGridRow(name, ci.ToString(), "\t\t\t");
                        StudentRows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading sections: {ex.Message}");
            }
        }

        public GradeRegistrationViewModel()
        {
            SaveCommand = new RelayCommand(
                execute: async (_) => await ExecuteSave()
            );
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

        private async Task ExecuteSave()
        {
            try
            {
                var inserts = new List<object>();

                foreach (var row in StudentRows.Where(row => row.IsModified))
                {
                    // Trim whitespace
                    string studentCIString = row.Values[1]?.Trim() ?? string.Empty;
                    string newGradeString = row.Values[2]?.Trim() ?? string.Empty;

                    int studentCI;
                    float newGrade;

                    // Attempt to parse StudentId
                    if (!int.TryParse(studentCIString, out studentCI))
                    {
                        MessageBox.Show($"Error: C.I. inválida para la fila con C.I.: '{studentCIString}'. Por favor introduzca un número entero.", "Error de Parseo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Stop saving if one is invalid
                    }

                    // Attempt to parse NewGrade
                    if (!float.TryParse(newGradeString, NumberStyles.Float, CultureInfo.InvariantCulture, out newGrade))
                    {
                        MessageBox.Show($"Error: Calificación inválida para el estudiante con C.I. {studentCI}. Por favor introduzca un número valido (ej.: 15.5).", "Error de Parseo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Stop saving if one is invalid
                    }

                    // Validate grade is within the acceptable range
                    if (newGrade < 0.0 || newGrade > 20.0)
                    {
                        MessageBox.Show($"Error: Calificación inválida para el estudiante con C.I. {studentCI}. Por favor introduzca un número entre 0 y 20.", "Error de Rango", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Add to inserts list
                    inserts.Add(new
                    {
                        StudentCi = studentCI,
                        NewGrade = newGrade
                    });
                }

                if (!inserts.Any())
                {
                    MessageBox.Show("No hay calificaciones por guardar.");
                    return;
                }

                await InsertGrades(inserts);
                MessageBox.Show($"{inserts.Count} calificaciones añadidas exitosamente.");

                foreach (var row in StudentRows.Where(r => r.IsModified))
                {
                    row.IsModified = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error guardando calificaciones: {ex.Message}", "Error de Guardado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteSave() => StudentRows?.Any(row => row.IsModified) ?? false;

        private async Task InsertGrades(IEnumerable<dynamic> inserts)
        {
            try
            {
                int totalAffected = 0;
  
                foreach (var insert in inserts)
                {
                    totalAffected += await App.Db?.Execute(
                        DbType.Postgres,
                        Config.Queries.Grade.Add,
                        new NpgsqlParameter("@subject_id", SelectedCourse?.SubjectId),
                        new NpgsqlParameter("@section_id", SelectedSection?.Id),
                        new NpgsqlParameter("@student_ci", insert.StudentCi),
                        new NpgsqlParameter("@grade", insert.NewGrade)
                    );
                }

                Debug.WriteLine($"Successfully inserted {totalAffected} grades");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving grades: {ex.Message}");
            }
        }
    }
}
