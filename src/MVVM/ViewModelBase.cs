using aCHADemia.Core.Interfaces;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace aCHADemia.MVVM
{
    internal class ViewModelBase : ObservableObject
    {
        protected IWindowService WindowService =>
            App.WindowService ?? throw new InvalidOperationException("WindowService not initialized");

        protected INavigationService NavigationService =>
            App.NavigationService ?? throw new InvalidOperationException("NavigationService not initialized");

        protected ViewModelBase()
        {
            GoBackCommand = new RelayCommand(execute => GoBack());
        }

        public RelayCommand GoBackCommand { get; }

        protected void NavigateTo<T>() where T : Page => NavigationService.NavigateTo<T>();
        protected void GoBack() => NavigationService.GoBack();
        protected void OpenWindow<T>() where T : Window, new() => WindowService.OpenWindow<T>();
    }
}
