using aCHADemia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace aCHADemia.View.Pages
{
    /// <summary>
    /// Interaction logic for MaintenancePerson.xaml
    /// </summary>
    public partial class MaintenancePersonPage : Page
    {
        public MaintenancePersonPage()
        {
            InitializeComponent();
            MaintenancePersonViewModel vm = new MaintenancePersonViewModel();
            DataContext = vm;
            
        }

        
    }
}
