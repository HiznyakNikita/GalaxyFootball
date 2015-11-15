using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    [Serializable]
    public class PlaygroundZone
    {
        private int _id;
        private Point _leftTop;
        private Point _rightTop;
        private Point _leftBottom;
        private Point _rightBottom;
        private Point _center;
        private PlaygroundZoneCategory _category;
        private PlaygroundZone _horizontalNeighbour;
        private PlaygroundZone _verticalNeighbour;

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

        public PlaygroundZoneCategory Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }

        public PlaygroundZone HorizontalNeighbour
        {
            get
            {
                return _horizontalNeighbour;
            }
            set
            {
                _horizontalNeighbour = value;
            }
        }

        public PlaygroundZone VerticalNeighbour
        {
            get
            {
                return _verticalNeighbour;
            }
            set
            {
                _verticalNeighbour = value;
            }
        }

        #endregion      

        public bool CheckForZoneIntersection(Point pointForCheck)
        {
            if((pointForCheck.X <= _rightTop.X && pointForCheck.X >= _leftTop.X)
                && (pointForCheck.Y >= _leftBottom.Y && pointForCheck.Y <= _leftTop.Y))
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
