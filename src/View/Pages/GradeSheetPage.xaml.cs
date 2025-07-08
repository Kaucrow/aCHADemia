using aCHADemia.ViewModel;
using System.Windows.Controls;

namespace aCHADemia.View.Pages
{
    /// <summary>
    /// Interaction logic for GradesSheet.xaml
    /// </summary>
    public partial class GradeSheetPage : Page
    {
        public GradeSheetPage()
        {
            InitializeComponent();

            GradeSheetViewModel vm = new GradeSheetViewModel();
            DataContext = vm;
            Loaded += async (s, e) => await vm.InitializeAsync();
        }
    }
}
