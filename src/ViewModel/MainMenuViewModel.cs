using aCHADemia.MVVM;
using aCHADemia.View.Pages;
using System.Windows;

namespace aCHADemia.ViewModel
{
    internal class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel()
        {
            NavigateToReportsCommand = new RelayCommand(execute => NavigateTo<ReportsPage>());
        }

        public Action? CloseAction { get; set; }
        public RelayCommand NavigateToReportsCommand { get; }
    }
}
