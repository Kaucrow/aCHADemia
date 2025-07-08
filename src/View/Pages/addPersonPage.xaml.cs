using aCHADemia.ViewModel;
using System.Windows;
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

            addPersonPageViewModel vm = new addPersonPageViewModel();
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

        // this isnt part of the proyect, this is just a debugging tool to see if the values are being set correctly
        private void testInput(object sender, RoutedEventArgs e)
        {
            if (DataContext is aCHADemia.ViewModel.addPersonPageViewModel vm)
            {
                MessageBox.Show($"CI: {vm.CI}\nNombre: {vm.Name}", "Valores actuales");
            }
        }

        private void VerValores_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}