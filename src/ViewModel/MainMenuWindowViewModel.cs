using aCHADemia.MVVM;
using aCHADemia.View;
using System.Windows;

namespace aCHADemia.ViewModel
{
    internal class MainMenuWindowViewModel : ViewModelBase
    {
        public MainMenuWindowViewModel(Window window) : base(window)
        {
            OpenReportsWindowCommand = new RelayCommand(execute => OpenWindow<ReportsWindow>());
        }

        public Action? CloseAction { get; set; }
        public RelayCommand OpenReportsWindowCommand { get; }
    }
}
