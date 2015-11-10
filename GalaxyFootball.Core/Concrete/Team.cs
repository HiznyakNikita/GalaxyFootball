using GalaxyFootball.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    public class Team
    {
        private readonly List<Player> _players;
        public Team()
        {

        }

        public string Name 
        { 
            get; 
            private set; 
        }

        public IReadOnlyCollection<Player> Players
        {
            get
            {
                return _players.AsReadOnly();
            }
        }

        public ITeamStrategy TeamStrategy
        {
            get;
            private set;
        }


    }
}
