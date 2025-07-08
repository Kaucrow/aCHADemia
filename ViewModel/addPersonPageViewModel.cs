using aCHADemia.MVVM;
using aCHADemia.View.Pages;
using System.Windows;

namespace aCHADemia.ViewModel
{
    internal class addPersonPageViewModel : ViewModelBase
    {

        //CI

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

        //name 

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

        public addPersonPageViewModel()
        {
           
        }

    }
}