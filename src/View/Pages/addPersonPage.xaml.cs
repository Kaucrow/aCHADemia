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
    }
}