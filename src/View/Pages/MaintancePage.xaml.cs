using aCHADemia.ViewModel;
using System.Windows.Controls;

namespace aCHADemia.View.Pages
{
    /// <summary>
    /// Interaction logic for ReportsPage.xaml
    /// </summary>
    public partial class MaintancePage : Page
    {
        public MaintancePage()
        {
            InitializeComponent();

            MaintanceViewModel vm = new MaintanceViewModel();
            DataContext = vm;
        }

        private void NavigationButton_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}