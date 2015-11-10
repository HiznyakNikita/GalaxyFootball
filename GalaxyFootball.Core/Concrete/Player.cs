using GalaxyFootball.Core.Abstract;
using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    public class Player : IObserver
    {
        public Player(Ball ball, string name, PlayerType type, PlaygroundZone zone)
        {
            Ball = ball;
            Name = name;
            Type = type;
            DefaultZone = zone;
            SetStartPosition(Type);
        }

        #region Properties

        public Ball Ball
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public Point Position
        {
            get;
            private set;
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
            private set;
        }

        #endregion

        public void Update(ITeamStrategy strategy)
        {
            ChangeMyPosition(strategy);
        }

        private void ChangeMyPosition(ITeamStrategy strategy)
        {
            Position = strategy.ChangePlayerPosition(Type, Position, DefaultZone, Ball);
        }

        private void SetStartPosition(PlayerType type)
        { }
    }
}
