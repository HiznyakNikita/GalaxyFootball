using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    public class Game
    {
        private volatile Ball _ball;
        private object _lock = new Object();
        public Game(Team teamHome, Team teamAway, Ball ball, Playground playground)
        {
            TeamHome = teamHome;
            TeamAway = teamAway;
            _ball = ball;
            Playground = playground;
            FindPlayersDefaultZone();
            GameEngine.CurrentGame = this;
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
            get
            {
                lock(_lock)
                {
                    return _ball;
                }
            }
            set
            {
                lock(_lock)
                {
                    if(value != null)
                        _ball = value;
                }
            }
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

        //Refactor to resolve dependency betweenplayer and game 
        //Find way to put this into team class
        public void FindPlayersDefaultZone()
        {
            foreach (var p in TeamHome.Players)
            {
                FindZoneForPlayer(p);
            }

            foreach (var p in TeamAway.Players)
            {
                FindZoneForPlayer(p);
            }
        }

        private void FindZoneForPlayer(Player p)
        {
            foreach (var z in Playground.Zones)
            {
                if (z.CheckForZoneIntersection(p.StartPosition))
                {
                    p.DefaultZone = z;
                }
            }
        }

        // reset players and ball position after scored goal
        public void ResetPositionsAfterGoal(bool isHomeScored)
        {
            _ball.Reset();
            foreach (Player p in TeamHome.Players)
            {
                p.IsSelected = false;
                p.Reset();
            }
            foreach (Player p in TeamAway.Players)
            {
                p.IsSelected = false;
                p.Reset();
            }
            if (!isHomeScored)
            {
                _ball.Owner = TeamHome.Players[6];
                TeamHome.Players[6].IsSelected = true;
                _ball.State = BallState.Controlled;
            }
            else
            {
                _ball.Owner = TeamAway.Players[6];
                TeamHome.Players[4].IsSelected = true;
                _ball.State = BallState.Controlled;
            }
        }

        // reset players and ball position after out
        public void ResetPositionsAfterOut(bool isHomeSideOut)
        {
            _ball.Reset();
            foreach (Player p in TeamHome.Players)
            {
                p.Reset();
            }
            foreach (Player p in TeamAway.Players)
            {
                p.Reset();
            }
            if(isHomeSideOut)
            {
                _ball.Owner = TeamHome.Players.Where(p => p.Type == PlayerType.GoalkeeperHome).FirstOrDefault();
                _ball.Owner.IsSelected = true;
                _ball.State = BallState.Controlled;
                _ball.Position = _ball.Owner.Position;
            }
            else
            {
                _ball.Owner = TeamAway.Players.Where(p => p.Type == PlayerType.GoalkeeperAway).FirstOrDefault();
                _ball.State = BallState.Controlled;
                TeamHome.Players[4].IsSelected = true;
                _ball.Position = _ball.Owner.Position;
            }
        }
    }
}
