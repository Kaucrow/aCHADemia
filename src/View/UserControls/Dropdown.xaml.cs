using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace aCHADemia.View.UserControls
{
    public partial class Dropdown : UserControl
    {
        #region Dependency Properties

        // Items
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(Dropdown));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(Dropdown),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(Dropdown));

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(Dropdown));

        // Styling
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(Dropdown),
            new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(Dropdown),
            new PropertyMetadata(Brushes.LightGray));

        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(Dropdown),
            new PropertyMetadata(new Thickness(1)));

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(Dropdown),
            new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(Dropdown),
            new PropertyMetadata(new FontFamily("Poppins")));

        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(Dropdown),
            new PropertyMetadata(14.0));

        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register("Padding", typeof(Thickness), typeof(Dropdown),
            new PropertyMetadata(new Thickness(8, 4, 8, 4)));

        public static readonly DependencyProperty ArrowBrushProperty =
            DependencyProperty.Register("ArrowBrush", typeof(Brush), typeof(Dropdown),
            new PropertyMetadata(Brushes.DimGray));

        public static readonly DependencyProperty DropdownBackgroundProperty =
            DependencyProperty.Register("DropdownBackground", typeof(Brush), typeof(Dropdown),
            new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty DropdownBorderBrushProperty =
            DependencyProperty.Register("DropdownBorderBrush", typeof(Brush), typeof(Dropdown),
            new PropertyMetadata(Brushes.LightGray));

        #endregion

        #region Public Properties

        // Items
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }

        public string SelectedValuePath
        {
            get => (string)GetValue(SelectedValuePathProperty);
            set => SetValue(SelectedValuePathProperty, value);
        }

        // Styling
        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public new Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public new Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public new Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        public new FontFamily FontFamily
        {
            get => (FontFamily)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public new double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public Brush ArrowBrush
        {
            get => (Brush)GetValue(ArrowBrushProperty);
            set => SetValue(ArrowBrushProperty, value);
        }

        public Brush DropdownBackground
        {
            get => (Brush)GetValue(DropdownBackgroundProperty);
            set => SetValue(DropdownBackgroundProperty, value);
        }

        public Brush DropdownBorderBrush
        {
            get => (Brush)GetValue(DropdownBorderBrushProperty);
            set => SetValue(DropdownBorderBrushProperty, value);
        }

        #endregion

        public Dropdown()
        {
            InitializeComponent();
        }

        private void InternalComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}