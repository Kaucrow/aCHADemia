using System.Windows;

namespace aCHADemia.Core.Interfaces
{
    public interface IWindowService
    {
        void OpenWindow<T>(Action? onClosing = null) where T : Window, new();
        void CloseCurrentWindow();
    }
}
