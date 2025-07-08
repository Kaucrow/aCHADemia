using aCHADemia.MVVM;
using aCHADemia.View.Pages;
using System.Windows;

namespace aCHADemia.ViewModel
{
    internal class ReportsViewModel : ViewModelBase
    {
        public ReportsViewModel()
        {
            NavigateToGradesSheetCommand = new RelayCommand(execute => NavigateTo<GradeSheetPage>());
        }

        public RelayCommand NavigateToGradesSheetCommand { get; }
    }
}
