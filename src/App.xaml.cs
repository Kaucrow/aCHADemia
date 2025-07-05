using aCHADemia.Core.Interfaces;
using aCHADemia.View.Windows;
using aCHADemia.View.Services;
using System.Windows;
using aCHADemia.View.Pages;

namespace aCHADemia
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            WindowService = new WindowService();
            NavigationService = new NavigationService();

            var mainWindow = new MainWindow();

            NavigationService.NavigateTo<MainMenuPage>();

            mainWindow.Show();
        }
        
        public static INavigationService? NavigationService { get; private set; }
        public static IWindowService? WindowService { get; private set; }
    }
}
