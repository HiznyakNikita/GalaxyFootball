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
        public Point ChangePlayerPositon(PlayerType playerType, Point previousPosition, PlaygroundZone zone, Ball ball)
        {
            Point newPosition = new Point();

            if(zone.CheckForZoneIntersection(ball.Position))
            {
                newPosition.X = previousPosition.X < ball.Position.X ? previousPosition.X++ : previousPosition.X--;
                newPosition.Y = previousPosition.Y < ball.Position.Y ? previousPosition.Y++ : previousPosition.Y--;
            }
            
        }
    }
}
