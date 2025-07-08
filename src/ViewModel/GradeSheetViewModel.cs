using aCHADemia.Core.DBComponent;
using aCHADemia.Core.Classes;
using aCHADemia.Model;
using aCHADemia.MVVM;
using CommunityToolkit.Mvvm.ComponentModel;
using Npgsql;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;

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
        private ObservableCollection<SelectableDataGridRow> _studentRows = [];

        [ObservableProperty]
        private IList<ObservableCollection<string>> _selectedStudentRows = [];

        public GradeSheetViewModel()
        {
        }

        async partial void OnSelectedCourseChanged(Course? value)
        {
            Sections.Clear();
            SelectedSection = null;

            try
            {
                using (DbDataReader reader = await App.Db?.Fetch(
                    DbType.Postgres,
                    Config.Queries.GradeSheet.GetCourseSections,
                    new NpgsqlParameter("@materia_id", SelectedCourse?.SubjectId)
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
                    Config.Queries.GradeSheet.GetCourseStudents,
                    new NpgsqlParameter("@materia_id", SelectedCourse?.SubjectId),
                    new NpgsqlParameter("@seccion_id", SelectedSection?.Id)
                ))
                {
                    while (await reader.ReadAsync())
                    {
                        var name = reader.GetString(reader.GetOrdinal("name"));
                        var ci = reader.GetInt32(reader.GetOrdinal("ci"));
                        var grade = reader.GetFloat(reader.GetOrdinal("grade"));

                        var row = new SelectableDataGridRow(name, ci.ToString(), grade.ToString());
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
                    Config.Queries.GradeSheet.GetActiveCourses
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
    }
}
