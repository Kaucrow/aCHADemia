using aCHADemia.MVVM;
using aCHADemia.View.Pages;

namespace aCHADemia.ViewModel
{
    internal class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel()
        {
            NavigateToReportsCommand = new RelayCommand(execute => NavigateTo<ReportsPage>());
            OpenMaintenanceCommand = new RelayCommand(Execute => NavigateTo<MaintancePage>());
        }

        public RelayCommand NavigateToReportsCommand { get; }
        public RelayCommand OpenMaintenanceCommand { get; }
        public RelayCommand HelloWorldCommand { get; }
    }
}
