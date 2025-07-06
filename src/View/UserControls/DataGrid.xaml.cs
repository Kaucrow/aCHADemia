using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DependencyPropertyGenerator;

namespace aCHADemia.View.UserControls
{
    [DependencyProperty<List<string>>("ColumnHeaders")]
    [DependencyProperty<List<List<string>>>("Rows")]
    [DependencyProperty<double>("ColumnWidth", DefaultValue = 120.0)]
    [DependencyProperty<double>("FontSize", DefaultValue = 12.0)]
    [DependencyProperty<double>("BorderThickness", DefaultValue = 2.0)]
    [DependencyProperty<double>("BorderRadius", DefaultValue = 0.0)]
    [DependencyProperty<Brush>("FontColor")]
    [DependencyProperty<Brush>("BorderColor")]
    [DependencyProperty<Brush>("HeaderBackground")]
    [DependencyProperty<Brush>("HeaderBorderColor")]
    [DependencyProperty<Brush>("RowBorderColor")]
    [DependencyProperty<FontFamily>("FontFamily")]
    public partial class DataGrid : UserControl
    {
        public DataGrid()
        {
            InitializeComponent();

            FontColor = Brushes.Black;
            BorderColor = Brushes.LightGray;
            HeaderBackground = Brushes.Transparent;
            HeaderBorderColor = Brushes.Gray;
            RowBorderColor = Brushes.LightGray;
            FontFamily = new FontFamily("Arial");
        }

        private void ContentScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            double scrollDelta = e.Delta;
            
            // Higher = slower scroll
            double speedFactor = 4.0;
            double scrollDistance = scrollDelta / speedFactor;

            double newOffset;
            
            newOffset = scv.VerticalOffset - scrollDistance;

            // Clamp the new offset within valid bounds
            newOffset = Math.Max(0, Math.Min(scv.ScrollableHeight, newOffset));

            scv.ScrollToVerticalOffset(newOffset);

            e.Handled = true; // Mark the event as handled to stop default scrolling
        }
    }
}
