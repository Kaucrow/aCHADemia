using aCHADemia.MVVM;
using aCHADemia.View.Pages;
using System.Collections.ObjectModel;
using System.Windows;

namespace aCHADemia.ViewModel
{
    internal class AddPersonPageViewModel : ViewModelBase
    {

        // CI

        private string _ci;
        public string CI
        {
            get => _ci;
            set
            {
                if (_ci != value)
                {
                    _ci = value;
                    OnPropertyChanged();
                }
            }
        }

        // name 

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        // comboBox properties
        private ObservableCollection<string> _personTypes;
        public ObservableCollection<string> PersonTypes
        {
            get => _personTypes;
            set
            {
                _personTypes = value;
                OnPropertyChanged();
            }
        }

        // person type

        private string _selectedPersonType;
        public string SelectedPersonType
        {
            get => _selectedPersonType;
            set
            {
                if (_selectedPersonType != value)
                {
                    _selectedPersonType = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddPersonPageViewModel()
        {

            // Initialize with 2 mock values
            PersonTypes = new ObservableCollection<string>
            {
                "Profesor",
                "Alumno"
            };

            SelectedPersonType = PersonTypes.FirstOrDefault();

        }

    }
}