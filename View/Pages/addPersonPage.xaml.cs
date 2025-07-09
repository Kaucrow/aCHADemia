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

        // this isnt part of the proyect, this is just a debugging tool to see if the values are being set correctly
        private void testInput(object sender, RoutedEventArgs e)
        {
            if (DataContext is aCHADemia.ViewModel.AddPersonPageViewModel vm)
            {
                string tipoPersona = vm.SelectedPersonType ?? "No seleccionado";
                MessageBox.Show($"CI: {vm.CI}\nNombre: {vm.Name}\nTipo: {tipoPersona}", "Valores actuales");
            }
        }

        private void VerValores_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}