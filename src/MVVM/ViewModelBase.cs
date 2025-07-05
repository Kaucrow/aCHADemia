using aCHADemia.Core.Interfaces;
using aCHADemia.View.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace aCHADemia.MVVM
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IWindowService _windowService;

        public ViewModelBase(Window window)
        {
            _windowService = new WindowService(window);
        }

        public void OpenWindow<T>() where T : Window, new() => _windowService.OpenWindow<T>();

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
