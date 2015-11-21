using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.ViewModels
{
    public class PlayerMarkerVM :  IModel
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
