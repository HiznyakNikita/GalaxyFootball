using GalaxyFootball.Core;
using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using GalaxyFootball.Core.Concrete.TeamStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Test
{
    public static class InitHelper
    {
        public static Game TestGame;
        public static Ball TestBall;
        public static Team TestTeamHome;
        public static Team TestTeamAway;
        public static Playground TestPlayground;
        public static List<Player> playersHome = new List<Player>();
        public static List<Player> playersAway = new List<Player>();

        public static void Init()
        {
            TestBall = new Ball();
            TestPlayground = new Playground("Anfield");
            InitPlayers();
            TestTeamHome = new Team(TeamScheme.Home451,new BalancedStrategy(),playersHome);
            TestTeamAway = new Team(TeamScheme.Away451,new BalancedStrategy(),playersAway);
            TestBall.AttachObserver(TestTeamAway);
            TestBall.AttachObserver(TestTeamHome);
            TestGame = new Game(TestTeamHome,TestTeamAway,TestBall,TestPlayground);
        }

        private static void InitPlayers()
        {
            Player test1H = new Player("test1H", PlayerType.GoalkeeperHome, TestPlayground);
            Player test7H = new Player("test7H", PlayerType.CentralMidfielderHome, TestPlayground);
            Player test2H = new Player("test2H", PlayerType.LeftDefenderHome, TestPlayground);
            Player test8H = new Player("test8H", PlayerType.LeftMidfielderHome, TestPlayground);
            Player test3H = new Player("test3H", PlayerType.RightDefenderHome, TestPlayground);
            Player test9H = new Player("test9H", PlayerType.RightMidfielderHome, TestPlayground);
            Player test4H = new Player("test4H", PlayerType.CentralDefenderHomeLeft, TestPlayground);
            Player test10H = new Player("test10H", PlayerType.AttackMidfielderHome, TestPlayground);
            Player test5H = new Player("test5H", PlayerType.CentralDefenderHomeRight, TestPlayground);
            Player test11H = new Player("test11H", PlayerType.CentralForwardHome, TestPlayground);
            Player test6H = new Player("test6H", PlayerType.DefensiveMidfielderHome, TestPlayground);

            playersHome.Add(test1H); playersHome.Add(test2H); playersHome.Add(test3H); playersHome.Add(test4H); playersHome.Add(test5H); playersHome.Add(test6H);
            playersHome.Add(test7H); playersHome.Add(test8H); playersHome.Add(test9H); playersHome.Add(test10H); playersHome.Add(test11H);

            Player test1A = new Player("test1A", PlayerType.GoalkeeperAway, TestPlayground);
            Player test7A = new Player("test7A", PlayerType.CentralMidfielderAway, TestPlayground);
            Player test2A = new Player("test2A", PlayerType.LeftDefenderAway, TestPlayground);
            Player test8A = new Player("test8A", PlayerType.LeftMidfielderAway, TestPlayground);
            Player test3A = new Player("test3A", PlayerType.RightDefenderAway, TestPlayground);
            Player test9A = new Player("test9A", PlayerType.RightMidfielderAway, TestPlayground);
            Player test4A = new Player("test4A", PlayerType.CentralDefenderAwayLeft, TestPlayground);
            Player test10A = new Player("test10A", PlayerType.AttackMidfielderAway, TestPlayground);
            Player test5A = new Player("test5A", PlayerType.CentralDefenderAwayRight, TestPlayground);
            Player test11A = new Player("test11A", PlayerType.CentralForwardAway, TestPlayground);
            Player test6A = new Player("test6A", PlayerType.DefensiveMidfielderAway, TestPlayground);

            playersAway.Add(test1A); playersAway.Add(test2A); playersAway.Add(test3A); playersAway.Add(test4A); playersAway.Add(test5A); playersAway.Add(test6A);
            playersAway.Add(test7A); playersAway.Add(test8A); playersAway.Add(test9A); playersAway.Add(test10A); playersAway.Add(test11A); 




        }
    }
}
