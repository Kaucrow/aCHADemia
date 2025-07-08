using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DependencyPropertyGenerator;

namespace aCHADemia.View.UserControls
{
    [DependencyProperty<ICommand>("Command")]
    [DependencyProperty<string>("Icon")]
    [DependencyProperty<string>("Text")]
    [DependencyProperty<Brush>("Brush")]
    [DependencyProperty<double>("IconSize", DefaultValue=80.0)]
    [DependencyProperty<double>("FontSize", DefaultValue=16.0)]
    [DependencyProperty<Thickness>("TextMargin")]
    public partial class NavigationButton : UserControl
    {
        public NavigationButton()
        {
            InitializeComponent();

            TextMargin = new Thickness(0, 20, 0, 0);
        }
    }
}