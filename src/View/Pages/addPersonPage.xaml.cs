using aCHADemia.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace aCHADemia.View.Pages
{
    /// <summary>
    /// Interaction logic for ReportsPage.xaml
    /// </summary>
    public partial class AddPersonPage : Page
    {
        public AddPersonPage()
        {
            InitializeComponent();

            AddPersonPageViewModel vm = new AddPersonPageViewModel();
            DataContext = vm;
        }

        private void Button_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void InputField_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Dropdown_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void TextBoxField_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }


    }
}