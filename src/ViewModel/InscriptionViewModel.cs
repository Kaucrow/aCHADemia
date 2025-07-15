using aCHADemia.Core.Classes;
using aCHADemia.Core.DBComponent;
using aCHADemia.Model;
using aCHADemia.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using Npgsql;



namespace aCHADemia.ViewModel
{
    internal class InscriptionViewModel : ViewModelBase
    {
        private bool _isUpdatingStudentSearchTextFromSelection = false;
        private bool _isUpdatingSectionSearchTextFromSelection = false;
        private bool _isUpdatingSubjectSearchTextFromSelection = false;

        // Student Properties 
        private ObservableCollection<Persona>? _availableStudents = new ObservableCollection<Persona>();
        public ObservableCollection<Persona> AvailableStudents
        {
            get => _availableStudents;
            set { _availableStudents = value; OnPropertyChanged(); }
        }
        private Persona? _selectedStudent;
        public Persona? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (_selectedStudent != value)
                {
                    _selectedStudent = value;
                    OnPropertyChanged();

                    _isUpdatingStudentSearchTextFromSelection = true;
                    StudentSearchText = value?.Name ?? string.Empty;
                    _isUpdatingStudentSearchTextFromSelection = false;

                    IsStudentDropdownOpen = false;
                    SaveInscriptionCommand?.RaiseCanExecuteChanged();
                }
            }
        }
        private string? _studentSearchText;
        public string? StudentSearchText
        {
            get => _studentSearchText;
            set
            {
                if (_studentSearchText != value)
                {
                    _studentSearchText = value;
                    OnPropertyChanged();

                    if (!_isUpdatingStudentSearchTextFromSelection)
                    {
                        FilterStudents();
                        IsStudentDropdownOpen = !string.IsNullOrWhiteSpace(value) && AvailableStudents.Any();
                    }

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        if (SelectedStudent != null) { SelectedStudent = null; }
                        else { IsStudentDropdownOpen = false; }
                    }
                    SaveInscriptionCommand?.RaiseCanExecuteChanged();
                }
            }
        }
        
        private bool _isStudentDropdownOpen;
        public bool IsStudentDropdownOpen
        {
            get => _isStudentDropdownOpen;
            set
            {
                _isStudentDropdownOpen = value; OnPropertyChanged();
            }
        }
      
   
        // Section Properties
        private ObservableCollection<Section>? _availableSections = new ObservableCollection<Section>();
        public ObservableCollection<Section>? AvailableSections
        {
            get => _availableSections;
            set { _availableSections = value; OnPropertyChanged(); }
        }

        private Section? _selectedSection;
        public Section? SelectedSection
        {
            get => _selectedSection;
            set
            {
                if (_selectedSection != value)
                {
                    _selectedSection = value;
                    OnPropertyChanged();
                    _isUpdatingSectionSearchTextFromSelection = true;
                    SectionSearchText = value?.Name ?? string.Empty;
                    _isUpdatingSectionSearchTextFromSelection = false;
                    IsSectionDropdownOpen = false;
                    _ = UpdateCursoId(); // Asynchronous call to find curso_id
                    SaveInscriptionCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        private string? _sectionSearchText;
        public string? SectionSearchText
        {
            get => _sectionSearchText;
            set
            {
                if (_sectionSearchText != value)
                {
                    _sectionSearchText = value;
                    OnPropertyChanged();
                    if (!_isUpdatingSectionSearchTextFromSelection)
                    {
                        FilterSections();
                        IsSectionDropdownOpen = !string.IsNullOrWhiteSpace(value) && AvailableSections?.Any() == true;
                    }
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        if (SelectedSection != null) { SelectedSection = null; }
                        else { IsSectionDropdownOpen = false; }
                    }
                    SaveInscriptionCommand?.RaiseCanExecuteChanged();
                }
            }
        }
        
        private bool _isSectionDropdownOpen;
        public bool IsSectionDropdownOpen
        {
            get => _isSectionDropdownOpen;
            set { _isSectionDropdownOpen = value; OnPropertyChanged(); }
        }

        // Subject Properties 
        private ObservableCollection<Subject>? _availableSubjects = new ObservableCollection<Subject>();
        public ObservableCollection<Subject>? AvailableSubjects
        {
            get => _availableSubjects;
            set { _availableSubjects = value; OnPropertyChanged(); }
        }

        private Subject? _selectedSubject;
        public Subject? SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                if (_selectedSubject != value)
                {
                    _selectedSubject = value;
                    OnPropertyChanged();
                    _isUpdatingSubjectSearchTextFromSelection = true;
                    SubjectSearchText = value?.Name ?? string.Empty;
                    _isUpdatingSubjectSearchTextFromSelection = false;
                    IsSubjectDropdownOpen = false;
                    _ = UpdateCursoId(); // Asynchronous call to find curso_id
                    SaveInscriptionCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        private string? _subjectSearchText;
        public string? SubjectSearchText
        {
            get => _subjectSearchText;
            set
            {
                if (_subjectSearchText != value)
                {
                    _subjectSearchText = value;
                    OnPropertyChanged();
                    if (!_isUpdatingSubjectSearchTextFromSelection)
                    {
                        FilterSubjects();
                        IsSubjectDropdownOpen = !string.IsNullOrWhiteSpace(value) && AvailableSubjects?.Any() == true;
                    }
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        if (SelectedSubject != null) { SelectedSubject = null; }
                        else { IsSubjectDropdownOpen = false; }
                    }
                    SaveInscriptionCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        
        private bool _isSubjectDropdownOpen;      
        public bool IsSubjectDropdownOpen
        {
            get => _isSubjectDropdownOpen;
            set { _isSubjectDropdownOpen = value; OnPropertyChanged(); }
        }

        private int? _currentCursoId;
        public int? CurrentCursoId
        {
            get => _currentCursoId;
            set { _currentCursoId = value; OnPropertyChanged(); SaveInscriptionCommand?.RaiseCanExecuteChanged(); }
        }

        private List<Persona> _allStudents = new List<Persona>();
        private List<Section> _allSections = new List<Section>();
        private List<Subject> _allSubjects = new List<Subject>();

        public RelayCommand SaveInscriptionCommand { get; private set; }

        public InscriptionViewModel()
        {
            //InitializeData();
            InitializeCommands();

            // We made sure the text boxes start empty
            StudentSearchText = string.Empty;
            SectionSearchText = string.Empty;
            SubjectSearchText = string.Empty;
        }

        //private void InitializeData()
        //{
        //    _allStudents = new List<Persona>
        //    {
        //        new Persona { IdNumber = "S001", Id = 2, Name = "Javier Perez" },
        //        new Persona { IdNumber = "S002", Id = 2, Name = "El Atlas"  },
        //        new Persona { IdNumber = "S003", Id = 2, Name = "Odiar Idiar" },
        //        new Persona { IdNumber = "KKKK", Id = 2, Name = "Balatro Balatrin" },
        //        new Persona { IdNumber = "S005", Id = 2, Name = "El Gato" },
        //        new Persona { IdNumber = "S006", Id = 2, Name = "El Perro" },
        //        new Persona { IdNumber = "S007", Id = 2, Name = "Juanes" },
        //        new Persona { IdNumber = "S008", Id = 2, Name = "Beckarby" },
        //        new Persona { IdNumber = "S009", Id = 2, Name = "Rebecca Bracho"},
        //    };
        //    AvailableStudents = new ObservableCollection<Persona>(_allStudents);

        //    _allSections = new List<Section>
        //    {
        //        new Section { Id = 1, Name = "A" },
        //        new Section { Id = 2, Name = "B" },
        //        new Section { Id = 3, Name = "C" },
        //        new Section { Id = 4, Name = "D" },
        //    };
        //    AvailableSections = new ObservableCollection<Section>(_allSections);

        //    _allSubjects = new List<Subject>
        //    {
        //        new Subject { Id = 1, Name = "Matemáticas", Code = "MATH-101" },
        //        new Subject { Id = 2, Name = "Física", Code = "PHYS-201" },
        //        new Subject { Id = 3, Name = "Química", Code = "CHEM-101" },
        //        new Subject { Id = 4, Name = "Biología", Code = "BIO-101" },
        //        new Subject { Id = 5, Name = "Literatura", Code = "LIT-202" },
        //        new Subject { Id = 6, Name = "Programación I", Code = "CS-101" }
        //    };
        //    AvailableSubjects = new ObservableCollection<Subject>(_allSubjects);
        //}

        

        public async Task InitializeAsync()
        {
            await LoadStudents();
            await LoadSections();
            await LoadSubjects();
        }

        private async Task LoadStudents()
        {
            try
            {
                using (DbDataReader reader = await App.Db?.Fetch(
                    DbType.Postgres,
                    Config.Queries.Student.GetAllStudents
                )!)
                {
                    _allStudents.Clear();
                    while (await reader.ReadAsync())
                    {
                        _allStudents.Add(new Persona
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("persona_ci")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            IdNumber = reader.GetInt32(reader.GetOrdinal("IdNumber")).ToString()
                        });
                    }
                }
                AvailableStudents = new ObservableCollection<Persona>(_allStudents);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading students: {ex.Message}");
                System.Windows.MessageBox.Show($"Error loading students: {ex.Message}", "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private async Task LoadSections()
        {
            try
            {
                using (DbDataReader reader = await App.Db?.Fetch(
                    DbType.Postgres,
                    Config.Queries.Section.GetAll
                )!)
                {
                    _allSections.Clear();
                    while (await reader.ReadAsync())
                    {
                        _allSections.Add(new Section
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("seccion_id")),
                            Name = reader.GetString(reader.GetOrdinal("name"))
                        });
                    }
                }
                AvailableSections = new ObservableCollection<Section>(_allSections);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading sections: {ex.Message}");
                System.Windows.MessageBox.Show($"Error loading sections: {ex.Message}", "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private async Task LoadSubjects()
        {
            try
            {
                using (DbDataReader reader = await App.Db?.Fetch(
                    DbType.Postgres,
                    Config.Queries.Subject.GetAll
                )!)
                {
                    _allSubjects.Clear();
                    while (await reader.ReadAsync())
                    {
                        _allSubjects.Add(new Subject
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("materia_id")),
                            Name = reader.GetString(reader.GetOrdinal("name"))
                        });
                    }
                }
                AvailableSubjects = new ObservableCollection<Subject>(_allSubjects);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading subjects: {ex.Message}");
                System.Windows.MessageBox.Show($"Error loading subjects: {ex.Message}", "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
             
        // Filtering Methods
        private void FilterStudents()
        {
            if (string.IsNullOrWhiteSpace(StudentSearchText))
            {
                AvailableStudents = new ObservableCollection<Persona>(_allStudents);
            }
            else
            {
                var filtered = _allStudents.Where(s =>
                    (s.Name?.Contains(StudentSearchText, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (s.IdNumber?.Contains(StudentSearchText, System.StringComparison.OrdinalIgnoreCase) ?? false)
                ).ToList();
                AvailableStudents = new ObservableCollection<Persona>(filtered);
            }

            var exactMatchInFilteredList = AvailableStudents.FirstOrDefault(s =>
                (s.Name?.Equals(StudentSearchText, System.StringComparison.OrdinalIgnoreCase) ?? false));

            if (exactMatchInFilteredList != null && SelectedStudent != exactMatchInFilteredList)
            {
                _isUpdatingStudentSearchTextFromSelection = true;
                SelectedStudent = exactMatchInFilteredList;
                _isUpdatingStudentSearchTextFromSelection = false;
            }
            else if (SelectedStudent != null && (exactMatchInFilteredList == null || SelectedStudent != exactMatchInFilteredList))
            {
                SelectedStudent = null;
            }
        }

        private void FilterSections()
        {
            if (string.IsNullOrWhiteSpace(SectionSearchText))
            {
                AvailableSections = new ObservableCollection<Section>(_allSections);
            }
            else
            {
                var filtered = _allSections.Where(s =>
                    (s.Name?.Contains(SectionSearchText, System.StringComparison.OrdinalIgnoreCase) ?? false)
                ).ToList();
                AvailableSections = new ObservableCollection<Section>(filtered);
            }

            var exactMatchInFilteredList = AvailableSections.FirstOrDefault(s =>
                (s.Name?.Equals(SectionSearchText, System.StringComparison.OrdinalIgnoreCase) ?? false));

            if (exactMatchInFilteredList != null && SelectedSection != exactMatchInFilteredList)
            {
                _isUpdatingSectionSearchTextFromSelection = true;
                SelectedSection = exactMatchInFilteredList;
                _isUpdatingSectionSearchTextFromSelection = false;
            }
            else if (SelectedSection != null && (exactMatchInFilteredList == null || SelectedSection != exactMatchInFilteredList))
            {
                SelectedSection = null;
            }
        }

        private void FilterSubjects()
        {
            if (string.IsNullOrWhiteSpace(SubjectSearchText))
            {
                AvailableSubjects = new ObservableCollection<Subject>(_allSubjects);
            }
            else
            {
                var filtered = _allSubjects.Where(s =>
                    (s.Name?.Contains(SubjectSearchText, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (s.Code?.Contains(SubjectSearchText, System.StringComparison.OrdinalIgnoreCase) ?? false)
                ).ToList();
                AvailableSubjects = new ObservableCollection<Subject>(filtered);
            }

            var exactMatchInFilteredList = AvailableSubjects.FirstOrDefault(s =>
                (s.Name?.Equals(SubjectSearchText, System.StringComparison.OrdinalIgnoreCase) ?? false));

            if (exactMatchInFilteredList != null && SelectedSubject != exactMatchInFilteredList)
            {
                _isUpdatingSubjectSearchTextFromSelection = true;
                SelectedSubject = exactMatchInFilteredList;
                _isUpdatingSubjectSearchTextFromSelection = false;
            }
            else if (SelectedSubject != null && (exactMatchInFilteredList == null || SelectedSubject != exactMatchInFilteredList))
            {
                SelectedSubject = null;
            }
        }

        private async Task UpdateCursoId()
        {
            CurrentCursoId = null; // Clear previous ID

            if (SelectedSubject != null && SelectedSection != null)
            {
                try
                {
                    object? result = await App.Db?.FetchOne(
                        DbType.Postgres,
                        Config.Queries.Course.GetBySubjectAndSection,
                        new NpgsqlParameter("@subject_id", SelectedSubject.Id),
                        new NpgsqlParameter("@section_id", SelectedSection.Id)
                    )!;

                    if (result != null && result != DBNull.Value)
                    {
                        CurrentCursoId = Convert.ToInt32(result);
                        Debug.WriteLine($"Found Curso ID: {CurrentCursoId} for Subject: {SelectedSubject.Name}, Section: {SelectedSection.Name}");
                    }
                    else
                    {
                        Debug.WriteLine($"No active Curso found for Subject: {SelectedSubject.Name}, Section: {SelectedSection.Name}");
                        System.Windows.MessageBox.Show("No active course found for the selected Subject and Section combination. Please adjust your selections.", "Course Not Found", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error fetching Curso ID: {ex.Message}");
                    CurrentCursoId = null;
                    System.Windows.MessageBox.Show($"Error loading course ID: {ex.Message}", "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
            else
            {
                Debug.WriteLine("Subject or Section not selected, cannot update Curso ID.");
            }
            SaveInscriptionCommand?.RaiseCanExecuteChanged();
        }

        private void InitializeCommands()
        {
            SaveInscriptionCommand = new RelayCommand(
                _ => ExecuteSaveInscription(),
                _ => CanSaveInscription()
            );
        }

        private async void ExecuteSaveInscription()
        {
            if (CanSaveInscription())
            {
                try
                {
                    // 1. Check for existing inscription to prevent duplicates
                    object? existingInscriptionResult = await App.Db?.FetchOne(
                        DbType.Postgres,
                        Config.Queries.Inscription.CheckExisting,
                        new NpgsqlParameter("@alumno_ci", SelectedStudent?.Id),
                        new NpgsqlParameter("@curso_id", CurrentCursoId)
                    )!;

                    if (existingInscriptionResult != null && existingInscriptionResult != DBNull.Value)
                    {
                        System.Windows.MessageBox.Show("Student is already inscribed in this course.", "Duplicate Inscription", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                        return;
                    }

                    // 2. Perform the inscription (INSERT)
                    int rowsAffected = await App.Db?.Execute(
                        DbType.Postgres,
                        Config.Queries.Inscription.Add,
                        new NpgsqlParameter("@curso_id", CurrentCursoId),
                        new NpgsqlParameter("@alumno_ci", SelectedStudent?.Id),
                        new NpgsqlParameter("@inscripcion_dt", DateTime.Today)
                    ) ?? 0;

                    if (rowsAffected > 0)
                    {
                        System.Windows.MessageBox.Show(
                            $"Student {SelectedStudent?.Name} successfully inscribed to Subject {SelectedSubject?.Name} in Section {SelectedSection?.Name}!",
                            "Inscription Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information
                        );
                        // Clear selections after successful inscription for a fresh form
                        SelectedStudent = null;
                        SelectedSection = null;
                        SelectedSubject = null;
                        CurrentCursoId = null;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Failed to save inscription: No rows affected. This might indicate an issue with the query or data.", "Inscription Failed", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error saving inscription: {ex.Message}");
                    System.Windows.MessageBox.Show($"An unexpected error occurred during inscription: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
        }

        private bool CanSaveInscription()
        {
            bool canExecute = SelectedStudent != null && SelectedSection != null && SelectedSubject != null && CurrentCursoId.HasValue;
            Debug.WriteLine($"CanSaveInscription: {canExecute} (Student: {SelectedStudent?.Name}, Section: {SelectedSection?.Name}, Subject: {SelectedSubject?.Name}, CursoId: {CurrentCursoId})");
            return canExecute;
        }
    }
}