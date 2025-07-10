using aCHADemia.Core.Interfaces;
using DependencyPropertyGenerator;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace aCHADemia.View.UserControls
{
    [DependencyProperty<List<string>>("ColumnHeaders")]
    [DependencyProperty<IList>("Rows")]
    [DependencyProperty<bool[]>("CanEditColumns")]
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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }
    }

    public class IndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                values[0] is IEnumerable itemsSource &&
                values[1] is object currentItem &&
                values[2] is bool[] canEditColumns)
            {
                int index = 0;
                foreach (var item in itemsSource)
                {
                    if (item == currentItem)
                    {
                        return index < canEditColumns.Length && canEditColumns[index];
                    }
                    index++;
                }
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsSelectableToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is ContentPresenter presenter && presenter.Content is ISelectableRow?
                Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
