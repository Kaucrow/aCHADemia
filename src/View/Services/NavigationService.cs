using aCHADemia.Core.Interfaces;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace aCHADemia.View.Services
{
    public class NavigationService : INavigationService
    {
        private Frame? _frame;
        private readonly Dictionary<string, Type> _pages = [];
        private bool _isNavigating;
        private Frame Frame => _frame ?? throw new InvalidOperationException(
            "NavigationService not initialized. Call Initialize() first.");

        public void Initialize(Frame frame)
        {
            if (_frame != null) throw new InvalidOperationException("NavigationService has already been initialized");

            _frame = frame ?? throw new ArgumentNullException(nameof(frame));
            _frame.Navigating += OnNavigating;
            _frame.Navigated += OnNavigated;
        }

        public bool CanGoBack => Frame.CanGoBack;

        public void Configure(string key, Type pageType)
        {
            if (_pages.ContainsKey(key))
                throw new ArgumentException($"Page {key} already configured");

            _pages.Add(key, pageType);
        }

        public Type GetPageType(string key)
        {
            if (!_pages.TryGetValue(key, out var pageType))
                throw new ArgumentException($"Page {key} not configured");

            return pageType;
        }

        public void NavigateTo<T>() where T : Page => NavigateTo(typeof(T));

        public void NavigateTo<T>(object parameter) where T : Page
            => NavigateTo(typeof(T), parameter);

        public void NavigateTo(string pageKey, object? parameter = null)
        {
            if (!_pages.TryGetValue(pageKey, out var pageType))
                throw new ArgumentException($"Page {pageKey} not configured");

            NavigateTo(pageType, parameter);
        }

        private void NavigateTo(Type pageType, object? parameter = null)
        {
            if (_isNavigating) return;

            try
            {
                _isNavigating = true;
                var page = Activator.CreateInstance(pageType);
                Frame.Navigate(page, parameter);
            }
            finally
            {
                _isNavigating = false;
            }
        }

        public void GoBack()
        {
            if (CanGoBack && !_isNavigating)
            {
                try
                {
                    _isNavigating = true;
                    Frame.GoBack();
                }
                finally
                {
                    _isNavigating = false;
                }
            }
        }

        public void ClearBackStack()
        {
            while (Frame.CanGoBack)
            {
                Frame.RemoveBackEntry();
            }
        }

        private void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                // Pre-navigation logic here
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            // Post-navigation logic here
            if (e.Content is Page page && page.DataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(e.ExtraData);
            }
        }
    }
}