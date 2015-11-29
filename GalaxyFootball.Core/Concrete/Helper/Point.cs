using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete.Helper
{
    [Serializable]
    public class Point
    {
        public Point()
        {

        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }
    }
}
