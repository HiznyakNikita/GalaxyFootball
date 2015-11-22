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
                isDefensive = isHomePlayer ? !GameEngine.CurrentGame.TeamHome.Players.Contains(GameEngine.CurrentGame.Ball.Owner)
                    : !GameEngine.CurrentGame.TeamAway.Players.Contains(GameEngine.CurrentGame.Ball.Owner);
                //player and partners doesn't have ball - defense strategy
                if (isDefensive)
                    return DefensiveStrategy(player);
                //attack strategy
                else
                    return AttackStrategy(player);
            }
            //if we ontrol player
            else
            {
                if(isVerticalDown)
                {
                    if(isHorizontalRight)
                    {
                        if (GameEngine.CurrentGame.Ball.State == BallState.Controlled)
                            GameEngine.CurrentGame.Ball.Position = new Point(player.SpeedPoints / (Double)200 + player.Position.X,
                                player.SpeedPoints / (Double)200 + player.Position.Y);
                        return new Point(player.SpeedPoints / (Double)200 + player.Position.X, player.SpeedPoints / (Double)200 + player.Position.Y);
                    }
                    else if (isHorizontalLeft)
                    {
                        if (GameEngine.CurrentGame.Ball.State == BallState.Controlled)
                            GameEngine.CurrentGame.Ball.Position = new Point(-player.SpeedPoints / (Double)200 + player.Position.X,
                                player.SpeedPoints / (Double)200 + player.Position.Y);
                        return new Point(-player.SpeedPoints / (Double)200 + player.Position.X, player.SpeedPoints / (Double)200 + player.Position.Y);
                    }
                    else
                    {
                        if (GameEngine.CurrentGame.Ball.State == BallState.Controlled)
                            GameEngine.CurrentGame.Ball.Position = new Point(player.Position.X, player.SpeedPoints / (Double)200 + player.Position.Y);
                        return new Point(player.Position.X, player.SpeedPoints / (Double)200 + player.Position.Y);
                    }
                }
                else if(isVerticalUp)
                {
                    if (isHorizontalRight)
                    {
                        if (GameEngine.CurrentGame.Ball.State == BallState.Controlled)
                            GameEngine.CurrentGame.Ball.Position = new Point(player.SpeedPoints / (Double)200 + player.Position.X,
                                -player.SpeedPoints / (Double)200 + player.Position.Y);
                        return new Point(player.SpeedPoints / (Double)200 + player.Position.X, -player.SpeedPoints / (Double)200 + player.Position.Y);
                    }
                    else if (isHorizontalLeft)
                    {
                        if (GameEngine.CurrentGame.Ball.State == BallState.Controlled)
                            GameEngine.CurrentGame.Ball.Position = new Point(-player.SpeedPoints / (Double)200 + player.Position.X,
                                -player.SpeedPoints / (Double)200 + player.Position.Y);
                        return new Point(-player.SpeedPoints / (Double)200 + player.Position.X, -player.SpeedPoints / (Double)200 + player.Position.Y);
                    }
                    else
                    {
                        if (GameEngine.CurrentGame.Ball.State == BallState.Controlled)
                            GameEngine.CurrentGame.Ball.Position = new Point(player.Position.X, -player.SpeedPoints / (Double)200 + player.Position.Y);
                        return new Point(player.Position.X, -player.SpeedPoints / (Double)200 + player.Position.Y);
                    }
                }
                else if (isHorizontalLeft)
                {
                    if (GameEngine.CurrentGame.Ball.State == BallState.Controlled)
                        GameEngine.CurrentGame.Ball.Position = new Point(-player.SpeedPoints / (Double)200 + player.Position.X, player.Position.Y);
                    return new Point(- player.SpeedPoints / (Double)200 + player.Position.X, player.Position.Y);
                }
                else if (isHorizontalRight)
                {
                    if (GameEngine.CurrentGame.Ball.State == BallState.Controlled)
                        GameEngine.CurrentGame.Ball.Position = new Point(player.Position.X + player.SpeedPoints / (Double)200, player.Position.Y);
                    return new Point(player.Position.X + player.SpeedPoints / (Double)200, player.Position.Y);
                }
                else
                {
                    return player.Position;
                }
            }
        }

        private static Point AttackStrategy(Player player)
        {
            PlaygroundZone currentPlayerZone = GameEngine.CurrentGame.Playground.Zones.Where(z => z.CheckForZoneIntersection(player.Position)).FirstOrDefault();
            Point newPosition = player.Position;

            //if player without a ball
            if (GameEngine.CurrentGame.Ball.Owner != null && !GameEngine.CurrentGame.Ball.Owner.Equals(player))
            {
                if(player.Type.ToString().Contains("Goalkeeper"))
                {
                    newPosition = BallInPlayerZone(player, currentPlayerZone);
                    return newPosition;
                }
                if(player.Type.ToString().Contains("Defender"))
                {
                    return DefenderAttack(player, currentPlayerZone, ref newPosition);
                }
                if(player.Type.ToString().Contains("Midfielder"))
                {
                    return MidfielderAttack(player, currentPlayerZone, ref newPosition);
                }
                if(player.Type.ToString().Contains("Forward"))
                {
                    return ForwardAttack(player, currentPlayerZone, ref newPosition);
                }
            }
            else if (GameEngine.CurrentGame.Ball.Owner != null && GameEngine.CurrentGame.Ball.Owner.Equals(player))
            {
                BallOwnerAttack(player, currentPlayerZone);
                return newPosition;
            }
            return newPosition;
        }

        private static void BallOwnerAttack(Player player, PlaygroundZone currentPlayerZone)
        {
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
                    while (currentPlayerZone.CheckForZoneIntersection(player.Position) && GameEngine.CurrentGame.Ball.Owner.Equals(player))
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

        private static Point ForwardAttack(Player player, PlaygroundZone currentPlayerZone, ref Point newPosition)
        {
            bool isHomeTeam = player.Type.ToString().Contains("Home");
            bool isIntersectWithPartnerInZone = player.Type.ToString().Contains("Home")
                ? GameEngine.CurrentGame.TeamHome.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0
                : GameEngine.CurrentGame.TeamAway.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0;
            if (!isIntersectWithPartnerInZone)
            {
                if (isHomeTeam)
                {
                    if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                    {
                        newPosition = BallInPlayerZone(player, currentPlayerZone);
                        return newPosition;
                    }
                    if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X && player.Position.X < player.DefaultZone.RightBottom.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)200 + newPosition.X;
                        newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? player.SpeedPoints / (Double)200 + newPosition.Y
                            : -player.SpeedPoints / (Double)200 + newPosition.Y;
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X && player.Position.X > player.DefaultZone.LeftBottom.X)
                    {
                        newPosition.X = -player.SpeedPoints / (Double)400 + newPosition.X;
                        newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? player.SpeedPoints / (Double)400 + newPosition.Y
                            : -player.SpeedPoints / (Double)400 + newPosition.Y;
                    }
                }
                else
                {
                    if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                    {
                        newPosition = BallInPlayerZone(player, currentPlayerZone);
                        return newPosition;
                    }
                    if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X && player.Position.X > player.DefaultZone.LeftBottom.X)
                    {
                        newPosition.X = -player.SpeedPoints / (Double)200 + newPosition.X;
                        newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? player.SpeedPoints / (Double)200 + newPosition.Y
                            : -player.SpeedPoints / (Double)200 + newPosition.Y;
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X && player.Position.X < player.DefaultZone.RightBottom.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)400 + newPosition.X;
                        newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? player.SpeedPoints / (Double)400 + newPosition.Y
                            : -player.SpeedPoints / (Double)400 + newPosition.Y;
                    }
                }
            }
            else
            {
                newPosition.X = 1 / (Double)200 + newPosition.X;
                newPosition.Y = 1 / (Double)200 + newPosition.Y;
            }
            return newPosition;
        }

        private static Point MidfielderAttack(Player player, PlaygroundZone currentPlayerZone, ref Point newPosition)
        {
            bool isHomeTeam = player.Type.ToString().Contains("Home");
            bool isIntersectWithPartnerInZone = player.Type.ToString().Contains("Home")
                ? GameEngine.CurrentGame.TeamHome.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0
                : GameEngine.CurrentGame.TeamAway.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0;
            if (!isIntersectWithPartnerInZone)
            {
                if (isHomeTeam)
                {
                    if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                    {
                        newPosition = BallInPlayerZone(player, currentPlayerZone);
                        return newPosition;
                    }
                    if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X && player.Position.X < player.DefaultZone.HorizontalNeighbour.Center.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)400 + newPosition.X;
                        newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? player.SpeedPoints / (Double)400 + newPosition.Y
                            : -player.SpeedPoints / (Double)400 + newPosition.Y;
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X && player.Position.X > player.DefaultZone.LeftBottom.X)
                    {
                        newPosition.X = -player.SpeedPoints / (Double)400 + newPosition.X;
                        newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? player.SpeedPoints / (Double)400 + newPosition.Y
                            : -player.SpeedPoints / (Double)400 + newPosition.Y;
                    }
                }
                else
                {
                    if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                    {
                        newPosition = BallInPlayerZone(player, currentPlayerZone);
                        return newPosition;
                    }
                    if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X && player.Position.X > player.DefaultZone.LeftBottom.X)
                    {
                        newPosition.X = -player.SpeedPoints / (Double)400 + newPosition.X;
                        newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? player.SpeedPoints / (Double)400 + newPosition.Y
                            : -player.SpeedPoints / (Double)400 + newPosition.Y;
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X && player.Position.X < player.DefaultZone.RightBottom.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)400 + newPosition.X;
                        newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? player.SpeedPoints / (Double)400 + newPosition.Y
                            : -player.SpeedPoints / (Double)400 + newPosition.Y;
                    }
                }
            }
            else
            {
                newPosition.X = GameEngine.CurrentGame.Ball.Position.X > player.Position.X ? 1 / (Double)200 + newPosition.X
                       : -1 / (Double)200 + newPosition.X;
                newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? 1 / (Double)200 + newPosition.Y
                    : -1 / (Double)200 + newPosition.Y;
            }

            return newPosition;
        }

        private static Point DefenderAttack(Player player, PlaygroundZone currentPlayerZone, ref Point newPosition)
        {
            bool isHomeTeam = player.Type.ToString().Contains("Home");
            bool isIntersectWithPartnerInZone = player.Type.ToString().Contains("Home")
               ? GameEngine.CurrentGame.TeamHome.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0
               : GameEngine.CurrentGame.TeamAway.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0;
            if (!isIntersectWithPartnerInZone)
            {
                if (isHomeTeam)
                {
                    if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                    {
                        newPosition = BallInPlayerZone(player, currentPlayerZone);
                        return newPosition;
                    }

                    //if this is flang defender
                    if (!player.Type.ToString().Contains("Central"))
                    {
                        if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X
                            && player.Position.X < player.DefaultZone.HorizontalNeighbour.HorizontalNeighbour.Center.X)
                        {
                            newPosition.X = player.SpeedPoints / (Double)400 + newPosition.X;
                        }
                    }
                    else if (player.Type.ToString().Contains("Central"))
                    {
                        if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X && player.Position.X < player.DefaultZone.HorizontalNeighbour.Center.X)
                        {
                            newPosition.X = player.SpeedPoints / (Double)400 + newPosition.X;
                        }
                    }
                    if (GameEngine.CurrentGame.Playground.Zones.Where(z => z.RightBottom.X == currentPlayerZone.RightBottom.X && !z.Equals(currentPlayerZone)
                        && z.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position)).Count() > 0
                        && GameEngine.CurrentGame.Ball.Position.X <= player.Position.X)
                    {
                        if (player.Position.X > currentPlayerZone.LeftBottom.X)
                        {
                            newPosition.X = -player.SpeedPoints / (Double)200 + newPosition.X;
                        }
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)400 + newPosition.X;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)400 + newPosition.Y
                            : -player.SpeedPoints / (Double)400 + newPosition.Y;
                    }
                }
                else
                {
                    if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                    {
                        newPosition = BallInPlayerZone(player, currentPlayerZone);
                        return newPosition;
                    }

                    //if this is flang defender
                    if (!player.Type.ToString().Contains("Central"))
                    {
                        if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X
                            && player.Position.X > player.DefaultZone.HorizontalNeighbour.HorizontalNeighbour.Center.X)
                        {
                            newPosition.X = -player.SpeedPoints / (Double)400 + newPosition.X;
                        }
                    }
                    else if (player.Type.ToString().Contains("Central"))
                    {
                        if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X && player.Position.X > player.DefaultZone.HorizontalNeighbour.Center.X)
                        {
                            newPosition.X = -player.SpeedPoints / (Double)400 + newPosition.X;
                        }
                    }
                    if (GameEngine.CurrentGame.Playground.Zones.Where(z => z.RightBottom.X == currentPlayerZone.RightBottom.X && !z.Equals(currentPlayerZone)
                        && z.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position)).Count() > 0
                        && GameEngine.CurrentGame.Ball.Position.X >= player.Position.X)
                    {
                        if (player.Position.X < currentPlayerZone.RightBottom.X)
                        {
                            newPosition.X = player.SpeedPoints / (Double)200 + newPosition.X;
                        }
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)400 + newPosition.X;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)400 + newPosition.Y
                            : -player.SpeedPoints / (Double)400 + newPosition.Y;
                    }
                }
            }
            else
            {
                newPosition.X = player.Type.ToString().Contains("Home") ? 1 / (Double)200 + newPosition.X
                       : 1 / (Double)200 + newPosition.X;
                newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? 1 / (Double)200 + newPosition.Y
                    : 1 / (Double)200 + newPosition.Y;
            }
            return newPosition;
        }

        private static Point BallInPlayerZone(Player player, PlaygroundZone currentPlayerZone)
        {
            Point newPosition = player.Position;
            //if ball in current player zone => go to ball
            if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
            {
                newPosition.X = player.Position.X < GameEngine.CurrentGame.Ball.Position.X ? player.SpeedPoints / (Double)200 + newPosition.X
                    : -player.SpeedPoints / (Double)200 + newPosition.X;
                newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)200 + newPosition.Y
                    : -player.SpeedPoints / (Double)200 + newPosition.Y;
            }
            return newPosition;
        }

        private static Point DefensiveStrategy(Player player)
        {
            PlaygroundZone currentPlayerZone = GameEngine.CurrentGame.Playground.Zones.Where(z => z.CheckForZoneIntersection(player.Position)).FirstOrDefault();
            Point newPosition = player.Position;

            if (GameEngine.CurrentGame.Ball.IsCanPick(player.Position))
                player.Pick(GameEngine.CurrentGame.Ball);

            if (player.Type.ToString().Contains("Goalkeeper"))
            {
                newPosition = BallInPlayerZone(player, currentPlayerZone);
                return newPosition;
            }
            if (player.Type.ToString().Contains("Defender"))
            {
                return DefenderDefensive(player, currentPlayerZone, ref newPosition);
            }
            if (player.Type.ToString().Contains("Midfielder"))
            {
                return MidfielderDefensive(player, currentPlayerZone, ref newPosition);
            }
            if (player.Type.ToString().Contains("Forward"))
            {
                return ForwardDefensive(player, currentPlayerZone, ref newPosition);
            }
            return newPosition;
        }

        private static Point ForwardDefensive(Player player, PlaygroundZone currentPlayerZone, ref Point newPosition)
        {
            bool isHomeTeam = player.Type.ToString().Contains("Home");
            bool isIntersectWithPartnerInZone = player.Type.ToString().Contains("Home")
               ? GameEngine.CurrentGame.TeamHome.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0
               : GameEngine.CurrentGame.TeamAway.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0;
            if (!isIntersectWithPartnerInZone)
            {
                if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                {
                    newPosition = BallInPlayerZone(player, currentPlayerZone);
                    return newPosition;
                }
                if(isHomeTeam)
                {
                    if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X)
                    {
                        newPosition.X = player.Position.X < GameEngine.CurrentGame.Ball.Position.X ? player.SpeedPoints / (Double)200 + newPosition.X
                        : -player.SpeedPoints / (Double)200 + newPosition.X;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)200 + newPosition.Y
                            : -player.SpeedPoints / (Double)200 + newPosition.Y;
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X 
                        && player.Position.X > GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 5).FirstOrDefault().Center.X)
                    {
                        newPosition.X = -player.SpeedPoints / (Double)400 + newPosition.X;
                    }
                    return newPosition;
                }
                else
                {
                    if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X)
                    {
                        newPosition.X = player.Position.X < GameEngine.CurrentGame.Ball.Position.X ? player.SpeedPoints / (Double)200 + newPosition.X
                        : -player.SpeedPoints / (Double)200 + newPosition.X;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)200 + newPosition.Y
                            : -player.SpeedPoints / (Double)200 + newPosition.Y;
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X
                        && player.Position.X < GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 5).FirstOrDefault().Center.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)400 + newPosition.X;
                    }
                    return newPosition;
                }
            }
            else
            {
                newPosition.X = player.Type.ToString().Contains("Home") ? 4 / (Double)200 + newPosition.X
                       : 4 / (Double)200 + newPosition.X;
                newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? 4 / (Double)200 + newPosition.Y
                    : -4 / (Double)200 + newPosition.Y;
            }
            return newPosition;
        }

        private static Point MidfielderDefensive(Player player, PlaygroundZone currentPlayerZone, ref Point newPosition)
        {
            bool isHomeTeam = player.Type.ToString().Contains("Home");
            bool isIntersectWithPartnerInZone = player.Type.ToString().Contains("Home")
               ? GameEngine.CurrentGame.TeamHome.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0
               : GameEngine.CurrentGame.TeamAway.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0;
            if (!isIntersectWithPartnerInZone)
            {
                if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                {
                    newPosition = BallInPlayerZone(player, currentPlayerZone);
                    return newPosition;
                }
                if (isHomeTeam)
                {
                    if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X)
                    {
                        newPosition.X = -player.SpeedPoints / (Double)200 + newPosition.X;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)200 + newPosition.Y
                            : -player.SpeedPoints / (Double)200 + newPosition.Y;
                    }
                    else if (GameEngine.CurrentGame.Playground.Zones.Where(z => z.RightBottom.X == currentPlayerZone.RightBottom.X && !z.Equals(currentPlayerZone)
                        && z.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position)).Count() > 0
                        && GameEngine.CurrentGame.Ball.Position.X >= player.Position.X)
                    {
                        newPosition.X = -player.SpeedPoints / (Double)400 + newPosition.X;
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X
                        && player.Position.X > GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 2).FirstOrDefault().Center.X)
                    {
                        newPosition.X = -player.SpeedPoints / (Double)200 + newPosition.X;
                    }
                    return newPosition;
                }
                else
                {
                    if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)200 + newPosition.X;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)200 + newPosition.Y
                            : -player.SpeedPoints / (Double)200 + newPosition.Y;
                    }
                    else if (GameEngine.CurrentGame.Playground.Zones.Where(z => z.RightBottom.X == currentPlayerZone.RightBottom.X && !z.Equals(currentPlayerZone)
                        && z.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position)).Count() > 0
                        && GameEngine.CurrentGame.Ball.Position.X >= player.Position.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)400 + newPosition.X;
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X
                        && player.Position.X < GameEngine.CurrentGame.Playground.Zones.Where(z => z.Id == 8).FirstOrDefault().Center.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)200 + newPosition.X;
                    }
                    return newPosition;
                }
            }
            else
            {
                newPosition.X = player.Type.ToString().Contains("Home") ? 4 / (Double)200 + newPosition.X
                       : 4 / (Double)200 + newPosition.X;
                newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? 4 / (Double)200 + newPosition.Y
                    : -4 / (Double)200 + newPosition.Y;
            }
            return newPosition;
        }

        private static Point DefenderDefensive(Player player, PlaygroundZone currentPlayerZone, ref Point newPosition)
        {
            bool isHomeTeam = player.Type.ToString().Contains("Home");
            bool isIntersectWithPartnerInZone = player.Type.ToString().Contains("Home")
               ? GameEngine.CurrentGame.TeamHome.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0
               : GameEngine.CurrentGame.TeamAway.Players.Where(p => p.CheckForIntersectionInZone(player) && !p.Equals(player)).Count() > 0;
            if (!isIntersectWithPartnerInZone)
            {
                if (currentPlayerZone.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position))
                {
                    newPosition = BallInPlayerZone(player, currentPlayerZone);
                    return newPosition;
                }
                if (isHomeTeam)
                {
                    if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X 
                        && player.DefaultZone.HorizontalNeighbour.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position)
                        && player.Position.X < player.DefaultZone.RightBottom.X)
                    {
                        newPosition.X = player.SpeedPoints / (Double)400 + newPosition.X;
                    }
                    else if (GameEngine.CurrentGame.Playground.Zones.Where(z => z.RightBottom.X == currentPlayerZone.RightBottom.X && !z.Equals(currentPlayerZone)
                        && z.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position)).Count() > 0
                        && GameEngine.CurrentGame.Ball.Position.X >= player.Position.X)
                    {
                        newPosition.X = player.Position.X < GameEngine.CurrentGame.Ball.Position.X ? player.SpeedPoints / (Double)400 + newPosition.X
                        : -player.SpeedPoints / (Double)400 + newPosition.X;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)400 + newPosition.Y
                            : -player.SpeedPoints / (Double)400 + newPosition.Y;
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X
                        && player.Position.X > 20)
                    {
                        newPosition.X = player.Position.X < GameEngine.CurrentGame.Ball.Position.X ? player.SpeedPoints / (Double)200 + newPosition.X
                        : -player.SpeedPoints / (Double)200 + newPosition.X;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)200 + newPosition.Y
                            : -player.SpeedPoints / (Double)200 + newPosition.Y;
                    }
                    return newPosition;
                }
                else
                {
                    if (GameEngine.CurrentGame.Ball.Position.X < player.Position.X
                        && player.DefaultZone.HorizontalNeighbour.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position)
                        && player.Position.X > player.DefaultZone.LeftBottom.X)
                    {
                        newPosition.X = -player.SpeedPoints / (Double)400 + newPosition.X;
                    }
                    else if (GameEngine.CurrentGame.Playground.Zones.Where(z => z.RightBottom.X == currentPlayerZone.RightBottom.X && !z.Equals(currentPlayerZone)
                        && z.CheckForZoneIntersection(GameEngine.CurrentGame.Ball.Position)).Count() > 0
                        && GameEngine.CurrentGame.Ball.Position.X <= player.Position.X)
                    {
                        newPosition.X = player.Position.X < GameEngine.CurrentGame.Ball.Position.X ? player.SpeedPoints / (Double)400 + newPosition.X
                        : -player.SpeedPoints / (Double)400 + newPosition.X;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)400 + newPosition.Y
                            : -player.SpeedPoints / (Double)400 + newPosition.Y;
                    }
                    else if (GameEngine.CurrentGame.Ball.Position.X > player.Position.X
                        && player.Position.X < 1010)
                    {
                        newPosition.X = player.Position.X < GameEngine.CurrentGame.Ball.Position.X ? player.SpeedPoints / (Double)200 + newPosition.X
                        : -player.SpeedPoints / (Double)200 + newPosition.X;
                        newPosition.Y = player.Position.Y < GameEngine.CurrentGame.Ball.Position.Y ? player.SpeedPoints / (Double)200 + newPosition.Y
                            : -player.SpeedPoints / (Double)200 + newPosition.Y;
                    }
                    return newPosition;
                }
            }
            else
            {
                newPosition.X = player.Type.ToString().Contains("Home") ? 4 / (Double)200 + newPosition.X
                       : 4 / (Double)200 + newPosition.X;
                newPosition.Y = GameEngine.CurrentGame.Ball.Position.Y > player.Position.Y ? 4 / (Double)200 + newPosition.Y
                    : -4 / (Double)200 + newPosition.Y;
            }
            return newPosition;
        }



    }
}
