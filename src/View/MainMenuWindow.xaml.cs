using aCHADemia.ViewModel;
using System.Windows;

namespace aCHADemia.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
            MainMenuWindowViewModel vm = new MainMenuWindowViewModel(this);
            DataContext = vm;
        }
    }
}