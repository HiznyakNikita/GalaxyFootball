using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    public class Game
    {
        public Game()
        {

        }

        #region Properties

        public Team TeamHome
        {
            get;
            private set;
        }

        public Team TeamAway
        {
            get;
            private set;
        }

        public Ball Ball
        {
            get;
            private set;
        }

        public int GoalsHome
        {
            get;
            set;
        }

        public int GoalsAway
        {
            get;
            set;
        }

        public Playground Playground
        {
            get;
            private set;
        }

        #endregion
    }
}
