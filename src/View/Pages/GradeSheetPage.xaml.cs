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
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
