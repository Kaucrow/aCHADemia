using System.Windows;

namespace aCHADemia.Core.Interfaces
{
    public interface IWindowService
    {
        void OpenWindow<T>(object parameter = null) where T : Window;
        void Configure(string key, Type windowType);
        void CloseWindow(Window window);
        void SetOwner(Window owner);
        bool? ShowDialog<T>(object parameter = null) where T : Window;
    }
}