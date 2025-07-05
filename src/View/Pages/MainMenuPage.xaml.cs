using aCHADemia.ViewModel;
using System.Windows.Controls;

namespace aCHADemia.View.Pages
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();

            MainMenuViewModel vm = new MainMenuViewModel();
            DataContext = vm;
        }
    }
}
