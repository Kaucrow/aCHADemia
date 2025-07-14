using aCHADemia.MVVM;
using aCHADemia.View.Pages;

namespace aCHADemia.ViewModel
{
    internal class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel()
        {
            NavigateToReportsCommand = new RelayCommand(execute => NavigateTo<ReportsPage>());
            NavigateToInscriptionCommand = new RelayCommand(execute => NavigateTo<InscriptionPage>());
        }

        public RelayCommand NavigateToReportsCommand { get; }
        public RelayCommand NavigateToInscriptionCommand { get; }
        public RelayCommand HelloWorldCommand { get; }
    }
}
