using aCHADemia.ViewModel;
using System.Windows.Controls;

namespace aCHADemia.View.Pages
{
    /// <summary>
    /// Lógica de interacción para InscriptionPage.xaml
    /// </summary>
    public partial class InscriptionPage : Page
    {
        public InscriptionPage()
        {
            InitializeComponent();
            InscriptionViewModel vm = new InscriptionViewModel();
            DataContext = vm;
        }
    }
}
