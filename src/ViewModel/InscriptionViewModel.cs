using aCHADemia.Model;
using aCHADemia.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace aCHADemia.ViewModel
{
    internal class InscriptionViewModel : ViewModelBase
    {
        private bool _isUpdatingStudentSearchTextFromSelection = false;
        private bool _isUpdatingSectionSearchTextFromSelection = false;
        private bool _isUpdatingSubjectSearchTextFromSelection = false;

        // Student Properties 
        private ObservableCollection<Persona>? _availableStudents;
        private Persona? _selectedStudent;
        private string? _studentSearchText;
        private List<Persona> _allStudents;
        private bool _isStudentDropdownOpen;

        public ObservableCollection<Persona>? AvailableStudents
        {
            get => _availableStudents;
            set { _availableStudents = value; OnPropertyChanged(); }
        }

        public Persona? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (_selectedStudent != value) 
                {
                    _selectedStudent = value;
                    OnPropertyChanged();

                    // update StudentSearchText if a new item is selected.
                    _isUpdatingStudentSearchTextFromSelection = true;
                    if (value != null)
                    {
                        // Set text to the selected item's name
                        StudentSearchText = value.Name;
                    }
                    else
                    {
                        // If selection is cleared, also clear the text
                        StudentSearchText = string.Empty;
                    }
                    _isUpdatingStudentSearchTextFromSelection = false; 

                    // Close dropdown when an item is selected or selection is cleared 
                    IsStudentDropdownOpen = false;
                }
            }
        }

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
                        // Open dropdown if text is not empty AND there are filtered results
                        IsStudentDropdownOpen = !string.IsNullOrWhiteSpace(value) && AvailableStudents?.Any() == true;
                    }

                    //if the text is empty, we clear the selection
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        // If there was a selected item, clear it 
                        if (SelectedStudent != null)
                        {
                            SelectedStudent = null;
                        }
                        else
                        {
                            // If no item was selected, we just ensure dropdown is closed
                            IsStudentDropdownOpen = false;
                        }
                    }
                }
            }
        }

        public bool IsStudentDropdownOpen
        {
            get => _isStudentDropdownOpen;
            set { _isStudentDropdownOpen = value; OnPropertyChanged(); }
        }

        // Section Properties
        private ObservableCollection<Section>? _availableSections;
        private Section? _selectedSection;
        private string? _sectionSearchText;
        private List<Section> _allSections;
        private bool _isSectionDropdownOpen;

        public ObservableCollection<Section>? AvailableSections
        {
            get => _availableSections;
            set { _availableSections = value; OnPropertyChanged(); }
        }

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
                    if (value != null)
                    {
                        SectionSearchText = value.Name;
                    }
                    else
                    {
                        SectionSearchText = string.Empty;
                    }
                    _isUpdatingSectionSearchTextFromSelection = false;
                    IsSectionDropdownOpen = false;
                }
            }
        }

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
                        if (SelectedSection != null)
                        {
                            SelectedSection = null;
                        }
                        else
                        {
                            IsSectionDropdownOpen = false;
                        }
                    }
                }
            }
        }

        public bool IsSectionDropdownOpen
        {
            get => _isSectionDropdownOpen;
            set { _isSectionDropdownOpen = value; OnPropertyChanged(); }
        }

        // Subject Properties 
        private ObservableCollection<Subject>? _availableSubjects;
        private Subject? _selectedSubject;
        private string? _subjectSearchText;
        private List<Subject> _allSubjects;
        private bool _isSubjectDropdownOpen;

        public ObservableCollection<Subject>? AvailableSubjects
        {
            get => _availableSubjects;
            set { _availableSubjects = value; OnPropertyChanged(); }
        }

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
                    if (value != null)
                    {
                        SubjectSearchText = value.Name;
                    }
                    else
                    {
                        SubjectSearchText = string.Empty;
                    }
                    _isUpdatingSubjectSearchTextFromSelection = false;
                    IsSubjectDropdownOpen = false;
                }
            }
        }

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
                        if (SelectedSubject != null)
                        {
                            SelectedSubject = null;
                        }
                        else
                        {
                            IsSubjectDropdownOpen = false;
                        }
                    }
                }
            }
        }

        public bool IsSubjectDropdownOpen
        {
            get => _isSubjectDropdownOpen;
            set { _isSubjectDropdownOpen = value; OnPropertyChanged(); }
        }

        public RelayCommand SaveInscriptionCommand { get; private set; }

        public InscriptionViewModel()
        {
            InitializeData();
            InitializeCommands();

            // We made sure the text boxes start empty
            StudentSearchText = string.Empty;
            SectionSearchText = string.Empty;
            SubjectSearchText = string.Empty;
        }

        private void InitializeData()
        {
            _allStudents = new List<Persona>
            {
                new Persona { IdNumber = "S001", Id = 2, Name = "Javier Perez" },
                new Persona { IdNumber = "S002", Id = 2, Name = "El Atlas"  },
                new Persona { IdNumber = "S003", Id = 2, Name = "Odiar Idiar" },
                new Persona { IdNumber = "KKKK", Id = 2, Name = "Balatro Balatrin" },
                new Persona { IdNumber = "S005", Id = 2, Name = "El Gato" },
                new Persona { IdNumber = "S006", Id = 2, Name = "El Perro" },
                new Persona { IdNumber = "S007", Id = 2, Name = "Juanes" },
                new Persona { IdNumber = "S008", Id = 2, Name = "Beckarby" },
                new Persona { IdNumber = "S009", Id = 2, Name = "Rebecca Bracho"},
            };
            AvailableStudents = new ObservableCollection<Persona>(_allStudents);

            _allSections = new List<Section>
            {
                new Section { Id = 1, Name = "A" },
                new Section { Id = 2, Name = "B" },
                new Section { Id = 3, Name = "C" },
                new Section { Id = 4, Name = "D" },
            };
            AvailableSections = new ObservableCollection<Section>(_allSections);

            _allSubjects = new List<Subject>
            {
                new Subject { Id = 1, Name = "Matemáticas", Code = "MATH-101" },
                new Subject { Id = 2, Name = "Física", Code = "PHYS-201" },
                new Subject { Id = 3, Name = "Química", Code = "CHEM-101" },
                new Subject { Id = 4, Name = "Biología", Code = "BIO-101" },
                new Subject { Id = 5, Name = "Literatura", Code = "LIT-202" },
                new Subject { Id = 6, Name = "Programación I", Code = "CS-101" }
            };
            AvailableSubjects = new ObservableCollection<Subject>(_allSubjects);
        }

        private void InitializeCommands()
        {
            SaveInscriptionCommand = new RelayCommand(
                _ => ExecuteSaveInscription(),
                _ => CanSaveInscription()
            );
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

        private void ExecuteSaveInscription()
        {
            if (SelectedStudent != null && SelectedSection != null && SelectedSubject != null)
            {
                System.Windows.MessageBox.Show(
                    $"Inscribing student: {SelectedStudent.Name}\n" +
                    $"To section: {SelectedSection.Name}\n" +
                    $"For subject: {SelectedSubject.Name}\n" +
                    "Inscription saved!"
                );
            }
        }

        private bool CanSaveInscription()
        {
            return SelectedStudent != null && SelectedSection != null && SelectedSubject != null;
        }
    }
}