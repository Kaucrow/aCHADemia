using aCHADemia.Core.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace aCHADemia.MVVM
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IWindowService _windowService;
        private readonly INavigationService _navigationService;

        public ViewModelBase()
        {
            _windowService = App.WindowService ?? throw new ArgumentNullException(nameof(App.WindowService));
            _navigationService = App.NavigationService ?? throw new ArgumentNullException(nameof(App.NavigationService));
            
            GoBackCommand = new RelayCommand(execute => GoBack());
        }

        public RelayCommand GoBackCommand { get; }

        public void NavigateTo<T>() where T : Page => _navigationService.NavigateTo<T>();
        public void GoBack() => _navigationService.GoBack();
        public void OpenWindow<T>() where T : Window, new() => _windowService.OpenWindow<T>();

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
