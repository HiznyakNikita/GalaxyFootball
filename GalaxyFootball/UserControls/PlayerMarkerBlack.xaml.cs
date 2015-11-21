using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Helpers.Abstract;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GalaxyFootball.UserControls
{
    public class IsSelectedToStrokeAwayColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return new System.Windows.Media.SolidColorBrush(Colors.Yellow);
            else
                return new System.Windows.Media.SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    /// <summary>
    /// Interaction logic for PlayerMarkerBlack.xaml
    /// </summary>
    public partial class PlayerMarkerBlack : UserControl, IControl
    {
        public PlayerMarkerBlack(IModel model)
        {
            this.DataContext = model;
            InitializeComponent();
        }

        public PlayerMarkerBlack()
        {
            InitializeComponent();
        }
    }
}
