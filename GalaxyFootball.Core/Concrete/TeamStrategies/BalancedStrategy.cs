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
        //REFACTOR THIS!!!
        public Point ChangePlayerPosition(
            Player player,
            bool isVerticalUp = false, 
            bool isVerticalDown = false, 
            bool isHorizontalRight = false, 
            bool isHorizontalLeft = false)
        {
            // if we don't control player
            if (!player.IsSelected)
            {
                //find home or away player for analyze defensive or attack strategy
                bool isHomePlayer = player.Type.ToString().Contains("Home") ? true : false;
                bool isDefensive = false;
                if (GameEngine.CurrentGame.Ball.Owner == null)
                    isDefensive = true;
                //find if any of partners is owner of ball => attack strategy else defensive
                isDefensive = isHomePlayer ? GameEngine.CurrentGame.TeamHome.Players.Contains(GameEngine.CurrentGame.Ball.Owner)
                    : GameEngine.CurrentGame.TeamAway.Players.Contains(GameEngine.CurrentGame.Ball.Owner);
                //player and partners doesn't have ball - defense strategy
                if (isDefensive)
                {
                    PlaygroundZone currentPlayerZone = GameEngine.CurrentGame.Playground.Zones.Where(z => z.CheckForZoneIntersection(player.Position)).FirstOrDefault();
                    Point newPosition = player.Position;

                    //if ball in current player zone => go to ball
                    if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                    {
                        newPosition.X = player.Position.X < GameEngine.CurrentGame.Ball.Position.X ? player.SpeedPoints/10 + newPosition.X 
                            : newPosition.X - player.SpeedPoints/10;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints/10 + newPosition.Y 
                            : newPosition.Y - player.SpeedPoints/10;
                    }
                    else if (player.Type != PlayerType.GoalkeeperAway && player.Type != PlayerType.GoalkeeperHome)
                    {
                        foreach (var z in GameEngine.CurrentGame.Playground.Zones)
                        {
                            if (z.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                            {
                                //if ball in horizontal parallel zone => go to neighbour horizontal zone center
                                if (z.Category == currentPlayerZone.HorizontalNeighbour.Category)
                                {
                                    newPosition.X = player.Position.X < currentPlayerZone.HorizontalNeighbour.Center.X ? player.SpeedPoints/10 +  newPosition.X 
                                        : newPosition.X - player.SpeedPoints/10;
                                    newPosition.Y = player.Position.Y < currentPlayerZone.HorizontalNeighbour.Center.Y ? player.SpeedPoints/10 +  newPosition.Y 
                                        : newPosition.Y - player.SpeedPoints/10;
                                }
                                //if ball in vertical parallel zone => go to neighbour vertical zone center. DELETED BECAUSE UNREAL SITUATIONS
                                //else if (z.Category == currentPlayerZone.VerticalNeighbour.Category)
                                //{
                                //    newPosition.X = player.Position.X < currentPlayerZone.VerticalNeighbour.Center.X ? ++newPosition.X : --newPosition.X;
                                //    newPosition.Y = player.Position.Y < currentPlayerZone.VerticalNeighbour.Center.Y ? ++newPosition.Y : --newPosition.Y;
                                //}
                                else
                                {
                                    newPosition.X = player.Position.X < GameEngine.CurrentGame.Ball.Position.X ? player.SpeedPoints/10 + newPosition.X 
                                        : newPosition.X - player.SpeedPoints/10;
                                    newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints/10 + newPosition.Y 
                                        : newPosition.Y - player.SpeedPoints/10;
                                }
                            }
                        }
                    }

                    return newPosition;
                }
                //attack strategy
                else
                {
                    if (GameEngine.CurrentGame.Ball.Owner != null && GameEngine.CurrentGame.Ball.Owner.Equals(player))
                    {
                        PlaygroundZone currentPlayerZone = GameEngine.CurrentGame.Playground.Zones
                            .Where(z => z.CheckForZoneIntersection(player.Position)).FirstOrDefault();
                        List<Player> opponentPlayers = player.Type.ToString().Contains("Home")
                            ? GameEngine.CurrentGame.TeamAway.Players : GameEngine.CurrentGame.TeamHome.Players;
                        List<Player> partnerPlayers = player.Type.ToString().Contains("Home")
                            ? GameEngine.CurrentGame.TeamHome.Players : GameEngine.CurrentGame.TeamAway.Players;
                        foreach (var p in opponentPlayers)
                        {
                            //if opponent in 15px radius pass the ball to partner
                            if (p.CollisionWithPlayer(player))
                            {
                                player.Pass(GameEngine.CurrentGame.Ball, player.FindPartnerForPass());
                                break;
                            }
                            else
                            {
                                // if no opponents in radius control the ball and attack through the end of current zone
                                while (currentPlayerZone.CheckForZoneIntersection(player.Position))
                                    player.Control(GameEngine.CurrentGame.Ball, false, false, true, false);
                            }
                            // if player in the last wing attack zone find solution - pass to forward or go to cental zone
                            if (((GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 1).FirstOrDefault().CheckForZoneIntersection(player.Position)
                                || GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 3).FirstOrDefault().CheckForZoneIntersection(player.Position))
                                && player.Type.ToString().Contains("Away"))
                                || ((GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 7).FirstOrDefault().CheckForZoneIntersection(player.Position)
                                || GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 9).FirstOrDefault().CheckForZoneIntersection(player.Position))
                                && player.Type.ToString().Contains("Home")))
                            {
                                if (player.Type.ToString().Contains("Away") &&
                                    player.Type != PlayerType.CentralForwardAway)
                                {
                                    player.Pass(GameEngine.CurrentGame.Ball,
                                        GameEngine.CurrentGame.TeamAway.Players.Where(pl => pl.Type == PlayerType.CentralForwardAway).FirstOrDefault());
                                    break;
                                }
                                else if (player.Type.ToString().Contains("Home") &&
                                    player.Type != PlayerType.CentralForwardHome)
                                {
                                    player.Pass(GameEngine.CurrentGame.Ball,
                                        GameEngine.CurrentGame.TeamHome.Players.Where(pl => pl.Type == PlayerType.CentralForwardHome).FirstOrDefault());
                                    break;
                                }
                                else if (GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 1).FirstOrDefault().CheckForZoneIntersection(player.Position)
                                || GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 7).FirstOrDefault().CheckForZoneIntersection(player.Position))
                                {
                                    player.Control(GameEngine.CurrentGame.Ball, true);
                                }
                                else if (GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 3).FirstOrDefault().CheckForZoneIntersection(player.Position)
                                || GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 9).FirstOrDefault().CheckForZoneIntersection(player.Position))
                                {
                                    player.Control(GameEngine.CurrentGame.Ball, false, true);
                                }
                            }
                            else
                            {
                                if ((player.Type.ToString().Contains("Home") &&
                                (player.Position.X > GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 8).FirstOrDefault().Center.X) ||
                                GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 11).FirstOrDefault().CheckForZoneIntersection(player.Position)) ||
                                (player.Type.ToString().Contains("Away") &&
                                (player.Position.X < GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 2).FirstOrDefault().Center.X ||
                                GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 10).FirstOrDefault().CheckForZoneIntersection(player.Position))))
                                {
                                    player.Shoot(GameEngine.CurrentGame.Ball);
                                }
                                else
                                    player.Pass(GameEngine.CurrentGame.Ball, player.FindPartnerForPass());
                            }
                        }
                    }
                    return new Point(0, 0);
                }
            }
            else
            {
                if (isHorizontalLeft)
                    return new Point(player.Position.X - player.SpeedPoints / 10,player.Position.Y);
                if (isHorizontalRight)
                    return  new Point(player.Position.X + player.SpeedPoints / 10,player.Position.Y);
                if (isVerticalDown)
                    return  new Point(player.Position.X,player.Position.Y - player.SpeedPoints / 10);
                if (isVerticalUp)
                    return new Point(player.Position.X, player.Position.Y + player.SpeedPoints / 10);
                else
                    return new Point(0, 0);
            }
        }
    }
}
