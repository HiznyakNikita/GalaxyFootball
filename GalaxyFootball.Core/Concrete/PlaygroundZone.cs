using GalaxyFootball.Core.Concrete.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    public class PlaygroundZone
    {
        private int _id;
        private Point _leftTop;
        private Point _rightTop;
        private Point _leftBottom;
        private Point _rightBottom;
        private Point _center;

        public PlaygroundZone()
        {

        }

        #region Properties

        public Point LeftTop
        {
            get
            {
                return _leftTop;
            }
            set
            {
                _leftTop = value;
            }
        }

        public Point RightTop
        {
            get
            {
                return _rightTop;
            }
            set
            {
                _rightTop = value;
            }
        }

        public Point LeftBottom
        {
            get
            {
                return _leftBottom;
            }
            set
            {
                _leftBottom = value;
            }
        }

        public Point RightBottom
        {
            get
            {
                return _rightBottom;
            }
            set
            {
                _rightBottom = value;
            }
        }

        public Point Center
        {
            get
            {
                return _center;
            }
            set
            {
                _center = value;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        #endregion      

        public bool CheckForZoneIntersection(Point pointForCheck)
        {
            if((pointForCheck.X < _rightTop.X && pointForCheck.X > _leftTop.X)
                && (pointForCheck.Y > _leftBottom.Y && pointForCheck.Y < _leftTop.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
