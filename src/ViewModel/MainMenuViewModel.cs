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
            OpenMaintenanceCommand = new RelayCommand(Execute => NavigateTo<MaintancePage>());
        }

        public RelayCommand NavigateToReportsCommand { get; }
        public RelayCommand NavigateToGradeRegistrationCommand { get; }
        public RelayCommand OpenMaintenanceCommand { get; }
    }
}
