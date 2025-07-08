using aCHADemia.MVVM;
using aCHADemia.View.Pages;
using System.Windows;

namespace aCHADemia.ViewModel
{
    internal class MaintanceViewModel : ViewModelBase
    {
        public MaintanceViewModel()
        {
            NavigateToAddPersonCommand = new RelayCommand(execute => NavigateTo<addPersonPage>());
        }

        public RelayCommand NavigateToAddPersonCommand { get; }
    }
}
