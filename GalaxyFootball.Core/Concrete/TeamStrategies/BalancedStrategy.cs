using GalaxyFootball.Core.Abstract;
using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete.TeamStrategies
{
    /// <summary>
    /// Represent balanced strategy with normal position of players by zone
    /// </summary>
    public class BalancedStrategy : ITeamStrategy
    {
        public Point ChangePlayerPosition(Player player)
        {
            PlaygroundZone currentPlayerZone = GameEngine.CurrentGame.Playground.Zones.Where(z => z.CheckForZoneIntersection(player.Position)).FirstOrDefault();
            Point newPosition = player.Position;

            //if ball in current player zone => go to ball
            if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
            {
                newPosition.X = player.Position.X < GameEngine.CurrentGame.Ball.Position.X ? ++newPosition.X : --newPosition.X;
                newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? ++newPosition.Y : --newPosition.Y;
            }
            else
            {
                foreach (var z in GameEngine.CurrentGame.Playground.Zones)
                {
                    if (z.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                    {
                        //if ball in horizontal parallel zone => go to neighbour horizontal zone center
                        if(z.Category == currentPlayerZone.HorizontalNeighbour.Category)
                        {
                            newPosition.X = player.Position.X < currentPlayerZone.HorizontalNeighbour.Center.X ? ++newPosition.X : --newPosition.X;
                            newPosition.Y = player.Position.Y < currentPlayerZone.HorizontalNeighbour.Center.Y ? ++newPosition.Y : --newPosition.Y;
                        }
                        //if ball in vertical parallel zone => go to neighbour vertical zone center
                        else if (z.Category == currentPlayerZone.VerticalNeighbour.Category)
                        {
                            newPosition.X = player.Position.X < currentPlayerZone.VerticalNeighbour.Center.X ? ++newPosition.X : --newPosition.X;
                            newPosition.Y = player.Position.Y < currentPlayerZone.VerticalNeighbour.Center.Y ? ++newPosition.Y : --newPosition.Y;
                        }
                    }
                }
            }

            return newPosition;
            
        }
    }
}
