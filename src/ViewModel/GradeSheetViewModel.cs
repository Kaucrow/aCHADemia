using aCHADemia.Model;
using aCHADemia.MVVM;
using System.Collections.ObjectModel;

namespace aCHADemia.ViewModel
{
    internal class GradeSheetViewModel : ViewModelBase
    {
        private ObservableCollection<Subject>? _subjects;
        private Subject? _selectedSubject;

        public ObservableCollection<Subject>? Subjects
        {
            get => _subjects;
            set
            {
                _subjects = value;
                OnPropertyChanged();
            }
        }

        public Subject? SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                _selectedSubject = value;
                OnPropertyChanged();
            }
        }

        public List<string> ColumnHeaders { get; } = new List<string> { "ID", "Name", "Price" };
        public List<List<string>> Rows { get; } = new List<List<string>>
        {
            new List<string> { "101", "Laptop", "$999" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "103", "Elatla", "2 Bs." },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
            new List<string> { "102", "Mouse", "$25" },
        };

        public GradeSheetViewModel()
        {
            // Initialize with sample data
            Subjects = new ObservableCollection<Subject>
        {
            new Subject { Id = 1, Name = "Matemáticas", Code = "MATH-101" },
            new Subject { Id = 2, Name = "Física", Code = "PHYS-201" },
            new Subject { Id = 3, Name = "Química", Code = "CHEM-101" },
            new Subject { Id = 4, Name = "Biología", Code = "BIO-101" },
            new Subject { Id = 5, Name = "Literatura", Code = "LIT-202" }
        };
        }
    }
}
