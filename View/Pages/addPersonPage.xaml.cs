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

        private void testInput(object sender, RoutedEventArgs e)
        {
            if (DataContext is aCHADemia.ViewModel.AddPersonPageViewModel vm)
            {
                string tipoPersona = vm.SelectedPersonType ?? "No seleccionado";
                string ci = vm.CurrentPerson?.CI.ToString() ?? "No ingresado";
                string nombre = vm.CurrentPerson?.Name ?? "No ingresado";

                MessageBox.Show($"CI: {ci}\nNombre: {nombre}\nTipo: {tipoPersona}", "Valores actuales");
            }
        }

    }
}