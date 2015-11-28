using GalaxyFootball.Helpers.Abstract;
using GalaxyFootball.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GalaxyFootball.UserControls
{
    public class IsSelectedToStrokeHomeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return new System.Windows.Media.SolidColorBrush(Colors.Yellow);
            else
                return new System.Windows.Media.SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    /// <summary>
    /// Interaction logic for PlayerMarkerRed.xaml
    /// </summary>
    public partial class PlayerMarkerRed : UserControl, IControl
    {
        public PlayerMarkerRed()
        {
            InitializeComponent();
        }
    }
}
