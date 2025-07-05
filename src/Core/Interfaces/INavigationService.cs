using System.Windows.Controls;

namespace aCHADemia.Core.Interfaces
{
    public interface INavigationService
    {
        void Initialize(Frame frame);
        void NavigateTo<T>() where T : Page;
        void NavigateTo<T>(object parameter) where T : Page;
        void GoBack();
        bool CanGoBack { get; }
        void ClearBackStack();
        void Configure(string key, Type pageType);
        Type GetPageType(string key);
    }
}