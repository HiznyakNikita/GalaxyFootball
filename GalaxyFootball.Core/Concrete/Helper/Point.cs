using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete.Helper
{
    [Serializable]
    public class Point : INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        public Point()
        {

        }

        public Point(double x, double y)
        {
            _x = x;
            _y = y;

            NotifyPropertyChanged("X");
            NotifyPropertyChanged("Y");

        }

        public double X 
        { 
            get
            {
                return _x;
            }
            set
            {
                if (value != null)
                    _x = value;
                NotifyPropertyChanged("X");
            }
        }

        public double Y 
        {
            get
            {
                return _y;
            }
            set
            {
                if (value != null)
                    _y = value;
                NotifyPropertyChanged("Y");
            } 
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string property)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion
    }
}
