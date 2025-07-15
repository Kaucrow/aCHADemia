using aCHADemia.ViewModel;
using System.Windows.Controls;

namespace aCHADemia.View.Pages
{
    /// <summary>
    /// Lógica de interacción para InscriptionPage.xaml
    /// </summary>
    public partial class InscriptionPage : Page
    {
        private InscriptionViewModel _viewModel;
        public InscriptionPage()
        {
            InitializeComponent();
            _viewModel = new InscriptionViewModel();
            DataContext = _viewModel;
            Loaded += Page_Loaded;
        }

        private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _viewModel.InitializeAsync();
        }
    }
}
