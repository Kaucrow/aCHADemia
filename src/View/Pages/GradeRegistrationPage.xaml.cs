using aCHADemia.ViewModel;
using System.Windows.Controls;

namespace aCHADemia.View.Pages
{
    /// <summary>
    /// Interaction logic for GradesSheet.xaml
    /// </summary>
    public partial class GradeRegistrationPage : Page
    {
        public GradeRegistrationPage()
        {
            InitializeComponent();

            GradeRegistrationViewModel vm = new GradeRegistrationViewModel();
            DataContext = vm;
            Loaded += async (s, e) => await vm.InitializeAsync();
        }
    }
}
