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
    public class Player
    {
        private Playground _playground;


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
            set;
        }

        #endregion

        public void Update(ITeamStrategy strategy)
        {
            ChangePosition(strategy);
        }

        private void ChangePosition(ITeamStrategy strategy)
        {
            Position = strategy.ChangePlayerPosition(this);
        }

        public void SetStartPosition(Point position)
        {
            Position = StartPosition = position;
        }
    }
}
