using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DependencyPropertyGenerator;

namespace aCHADemia.View.UserControls
{
    [DependencyProperty<ICommand>("Command")]
    [DependencyProperty<string>("Text", DefaultValue="Atrás")]
    [DependencyProperty<Brush>("IconBrush")]
    [DependencyProperty<Brush>("TextBrush")]
    public partial class BackButton : UserControl
    {
        public BackButton()
        {
            InitializeComponent();
            
            IconBrush = (Brush)App.Current.FindResource("LightPrimaryBrush");
            TextBrush = (Brush)App.Current.FindResource("LightPrimaryBrush");
        }
    }
}