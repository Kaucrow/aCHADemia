using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DependencyPropertyGenerator;

namespace aCHADemia.View.UserControls
{
    [DependencyProperty<double>("Width", DefaultValue = 150.0)]
    [DependencyProperty<double>("Height", DefaultValue = 40.0)]
    [DependencyProperty<object>("ButtonContent")]
    [DependencyProperty<ICommand>("Command")]
    [DependencyProperty<Brush>("Foreground")]
    [DependencyProperty<Brush>("Background")]
    [DependencyProperty<FontFamily>("FontFamily")]
    [DependencyProperty<CornerRadius>("CornerRadius")]
    public partial class Button : UserControl
    {
        public Button()
        {
            InitializeComponent();
            
            CornerRadius = new CornerRadius(0);
            ButtonContent = "";
            Foreground = (Brush)App.Current.FindResource("LightBackgroundBrush");
            Background = (Brush)App.Current.FindResource("LightPrimaryBrush");
            FontFamily = (FontFamily)App.Current.FindResource("NormalFont");
        }
    }
}
