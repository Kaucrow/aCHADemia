using aCHADemia.Core.Interfaces;
using System.Windows;

namespace aCHADemia.View.Services
{
    internal class WindowService : IWindowService
    {
        private Window? _currentWindow;

        public WindowService(Window initialWindow)
        {
            _currentWindow = initialWindow;
        }

        public void OpenWindow<T>(Action? onClosing = null) where T : Window, new()
        {
            var currentState = new
            {
                State = _currentWindow?.WindowState ?? WindowState.Normal,
                Top = _currentWindow?.Top ?? 100,  // Default values if null
                Left = _currentWindow?.Left ?? 100,
                Width = _currentWindow?.ActualWidth ?? 800,
                Height = _currentWindow?.ActualHeight ?? 600,
                Topmost = _currentWindow?.Topmost ?? false
            };

            var newWindow = new T();

            // Apply previous window properties
            newWindow.WindowState = WindowState.Normal; // Must reset before setting size/position
            newWindow.Top = currentState.Top;
            newWindow.Left = currentState.Left;
            newWindow.Width = currentState.Width;
            newWindow.Height = currentState.Height;
            newWindow.Topmost = currentState.Topmost;

            // Restore state last (maximized/fullscreen will override size/position)
            newWindow.WindowState = currentState.State;

            newWindow.Closed += (s, e) => onClosing?.Invoke();

            newWindow.Show();
            
            _currentWindow?.Close();

            _currentWindow = newWindow;
        }

        public void CloseCurrentWindow()
        {
            _currentWindow?.Close();
        }
    }
}
