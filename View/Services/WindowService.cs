using aCHADemia.Core.Interfaces;
using System.Windows;

namespace aCHADemia.View.Services
{
    public class WindowService : IWindowService
    {
        private readonly Dictionary<string, Type> _windows = [];
        private Window? _owner;

        public void Configure(string key, Type windowType)
        {
            if (!typeof(Window).IsAssignableFrom(windowType))
                throw new ArgumentException("Type must be a Window");

            _windows[key] = windowType;
        }

        public void SetOwner(Window owner)
        {
            _owner = owner;
        }

        public void OpenWindow<T>(object? parameter = null) where T : Window
        {
            var window = CreateWindow(typeof(T), parameter);
            window.Show();
        }

        public bool? ShowDialog<T>(object? parameter = null) where T : Window
        {
            var window = CreateWindow(typeof(T), parameter);
            return window.ShowDialog();
        }

        private Window CreateWindow(Type windowType, object? parameter)
        {
            var window = Activator.CreateInstance(windowType) as Window ??
                throw new InvalidOperationException($"Failed to create instance of {windowType.Name}");

            if (window.DataContext is IWindowAware aware)
            {
                aware.OnWindowOpened(parameter);
            }

            if (_owner != null)
            {
                window.Owner = _owner;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            return window;
        }

        public void CloseWindow(Window window)
        {
            window?.Close();
        }
    }
}