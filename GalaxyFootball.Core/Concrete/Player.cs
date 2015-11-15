using GalaxyFootball.Core.Abstract;
using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    public class Player
    {
        private Playground _playground;
        private Thread _thread;
        private Point _position;

        public Player(string name, PlayerType type, Playground playground)
        {
            Name = name;
            Type = type;
            _playground = playground;
        }

        #region Properties

        public string Name
        {
            get;
            private set;
        }

        public Point Position
        {
            get
            {
                return _position;
            }
            private set
            {
                if (value.X < 20 || value.Y < 20)
                {
                    if (value.X < 20)
                        _position.X = 20;
                    else
                        _position.X = value.X;
                    if (value.Y < 20)
                        _position.Y = 20;
                    else
                        _position.Y = value.Y;
                }
                else
                {
                    if (value.Y > 680)
                        _position.Y = 680;
                    else
                        _position.Y = value.Y;
                    if (value.X > 1030)
                        _position.X = 1030;
                    else
                        _position.X = value.X;
                }
            }
        }

        public Point StartPosition
        {
            get;
            private set;
        }

        public PlayerType Type
        {
            get;
            private set;
        }

        public PlaygroundZone DefaultZone
        {
            get;
            set;
        }

        #endregion

        public void Update(ITeamStrategy strategy)
        {
            _thread = new Thread(new ParameterizedThreadStart(ChangePosition));
            ChangePosition(strategy);
        }

        private void ChangePosition(object strategy)
        {
            Position = ((ITeamStrategy)strategy).ChangePlayerPosition(this);
        }

        public void SetStartPosition(Point position)
        {
            _position = StartPosition = position;
        }
    }
}
