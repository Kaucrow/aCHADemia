using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace aCHADemia.View.UserControls
{
    public partial class DataGrid : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty ColumnHeadersProperty =
            DependencyProperty.Register("ColumnHeaders", typeof(List<string>), typeof(DataGrid), new PropertyMetadata(new List<string>()));

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(List<List<string>>), typeof(DataGrid), new PropertyMetadata(new List<List<string>>()));

        public static readonly DependencyProperty ColumnWidthProperty =
            DependencyProperty.Register("ColumnWidth", typeof(double), typeof(DataGrid), new PropertyMetadata(120.0));

        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(string), typeof(DataGrid), new PropertyMetadata("Segoe UI"));

        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(DataGrid), new PropertyMetadata(12.0));

        public static readonly DependencyProperty FontColorProperty =
            DependencyProperty.Register("FontColor", typeof(Brush), typeof(DataGrid), new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(Brush), typeof(DataGrid), new PropertyMetadata(Brushes.LightGray));

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(DataGrid), new PropertyMetadata(Brushes.WhiteSmoke));

        public static readonly DependencyProperty HeaderBorderColorProperty =
            DependencyProperty.Register("HeaderBorderColor", typeof(Brush), typeof(DataGrid), new PropertyMetadata(Brushes.Gray));

        public static readonly DependencyProperty RowBorderColorProperty =
            DependencyProperty.Register("RowBorderColor", typeof(Brush), typeof(DataGrid), new PropertyMetadata(Brushes.LightGray));

        // Properties
        public List<string> ColumnHeaders
        {
            get => (List<string>)GetValue(ColumnHeadersProperty);
            set => SetValue(ColumnHeadersProperty, value);
        }

        public List<List<string>> Rows
        {
            get => (List<List<string>>)GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        public double ColumnWidth
        {
            get => (double)GetValue(ColumnWidthProperty);
            set => SetValue(ColumnWidthProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public Brush FontColor
        {
            get => (Brush)GetValue(FontColorProperty);
            set => SetValue(FontColorProperty, value);
        }

        public Brush BorderColor
        {
            get => (Brush)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Brush HeaderBackground
        {
            get => (Brush)GetValue(HeaderBackgroundProperty);
            set => SetValue(HeaderBackgroundProperty, value);
        }

        public Brush HeaderBorderColor
        {
            get => (Brush)GetValue(HeaderBorderColorProperty);
            set => SetValue(HeaderBorderColorProperty, value);
        }

        public Brush RowBorderColor
        {
            get => (Brush)GetValue(RowBorderColorProperty);
            set => SetValue(RowBorderColorProperty, value);
        }

        public DataGrid()
        {
            InitializeComponent();
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
