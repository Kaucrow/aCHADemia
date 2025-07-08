using aCHADemia.ViewModel;
using System.Windows;

namespace aCHADemia.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var frame = ContentFrame;
            if (frame == null)
                throw new InvalidOperationException("Frame not found in Window");

            App.NavigationService?.Initialize(frame);
        }
    }
}