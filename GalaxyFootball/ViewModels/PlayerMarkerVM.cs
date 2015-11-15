using GalaxyFootball.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.ViewModels
{
    public class PlayerMarkerVM
    {
        public PlayerMarkerVM(int number, Player player)
        {
            Number = number;
            Player = player;
        }

        public int Number 
        { 
            get; 
            private set; 
        }

        public Player Player
        {
            get;
            private set;
        }

    }
}
