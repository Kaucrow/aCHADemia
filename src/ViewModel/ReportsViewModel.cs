using aCHADemia.MVVM;
using aCHADemia.View.Pages;
using System.Windows;

namespace aCHADemia.ViewModel
{
    internal class ReportsViewModel : ViewModelBase
    {
        public ReportsViewModel()
        {
        }

        public Action? CloseAction { get; set; }
    }
}
