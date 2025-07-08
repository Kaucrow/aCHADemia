using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DependencyPropertyGenerator;

namespace aCHADemia.View.UserControls
{
    [DependencyProperty<double>("FontSize", DefaultValue = 14.0)]
    [DependencyProperty<IEnumerable>("ItemsSource")]
    [DependencyProperty<object>("SelectedItem")]
    [DependencyProperty<string>("DisplayMemberPath")]
    [DependencyProperty<string>("SelectedValuePath")]
    [DependencyProperty<Brush>("Foreground")]
    [DependencyProperty<Brush>("Background")]
    [DependencyProperty<Brush>("BorderBrush")]
    [DependencyProperty<Thickness>("BorderThickness")]
    [DependencyProperty<FontFamily>("FontFamily")]
    [DependencyProperty<Thickness>("Padding")]
    [DependencyProperty<Brush>("ArrowBrush")]
    [DependencyProperty<Brush>("DropdownBackground")]
    [DependencyProperty<Brush>("DropdownBorderBrush")]
    public partial class TextBoxField : UserControl
    {

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(TextBoxField),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
            );

        public TextBoxField()
        {
            InitializeComponent();

            SelectedItem = new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault);
            Foreground = (Brush)App.Current.FindResource("LightPrimaryBrush");
            Background = (Brush)App.Current.FindResource("LightBackgroundBrush");
            BorderBrush = Brushes.LightGray;
            BorderThickness = new Thickness(1);
            FontFamily = (FontFamily)App.Current.FindResource("NormalFont");
            Padding = new Thickness(8, 4, 8, 4);
            ArrowBrush = Brushes.DimGray;
            DropdownBackground = (Brush)App.Current.FindResource("LightBackgroundBrush");
            DropdownBorderBrush = Brushes.LightGray;
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

    }
}