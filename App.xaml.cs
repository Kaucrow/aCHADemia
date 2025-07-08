using aCHADemia.Core.DBComponent;
using aCHADemia.Core.Interfaces;
using aCHADemia.View.Pages;
using aCHADemia.View.Services;
using aCHADemia.View.Windows;
using System.Windows;

namespace aCHADemia
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string postgresConnString = string.Format(
                "Host={0};Username={1};Password={2};Database={3};SSL Mode=Require;Channel Binding=Require",
                Core.DBComponent.Config.Postgres.Host,
                Core.DBComponent.Config.Postgres.User,
                Core.DBComponent.Config.Postgres.Password,
                Core.DBComponent.Config.Postgres.Name
            );

            await Pool.Initialize(DbType.Postgres, postgresConnString, Config.Pool.StartupSize, Config.Pool.MaxSize, Config.Pool.SizeIncrement);
            var pool = Pool.Instance;

            Db = new DbComponent(pool);

            WindowService = new WindowService();
            NavigationService = new NavigationService();

            var mainWindow = new MainWindow();

            NavigationService.NavigateTo<MainMenuPage>();

            mainWindow.Show();
        }
        
        public static INavigationService? NavigationService { get; private set; }
        public static IWindowService? WindowService { get; private set; }
        public static DbComponent? Db { get; private set; }
    }
}
