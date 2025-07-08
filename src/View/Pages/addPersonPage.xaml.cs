using aCHADemia.ViewModel;
using System.Windows.Controls;

namespace aCHADemia.View.Pages
{
    /// <summary>
    /// Interaction logic for ReportsPage.xaml
    /// </summary>
    public partial class addPersonPage : Page
    {
        public addPersonPage()
        {
            InitializeComponent();

            ReportsViewModel vm = new ReportsViewModel();
            DataContext = vm;
        }

        private void Button_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Dropdown_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void InputField_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Dropdown_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}