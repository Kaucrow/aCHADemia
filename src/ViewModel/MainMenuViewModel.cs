using aCHADemia.MVVM;
using aCHADemia.View.Pages;
using System.Diagnostics;

namespace aCHADemia.ViewModel
{
    internal class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel()
        {
            NavigateToReportsCommand = new RelayCommand(execute => NavigateTo<ReportsPage>());
            NavigateToGradeRegistrationCommand = new RelayCommand(execute => NavigateTo<GradeRegistrationPage>());
        }

        public RelayCommand NavigateToReportsCommand { get; }
        public RelayCommand NavigateToGradeRegistrationCommand { get; }
    }
}
