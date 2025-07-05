using aCHADemia.ViewModel;
using System.Windows.Controls;

namespace aCHADemia.View.Pages
{
    /// <summary>
    /// Interaction logic for ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();

            ReportsViewModel vm = new ReportsViewModel();
            DataContext = vm;
        }
    }
}
