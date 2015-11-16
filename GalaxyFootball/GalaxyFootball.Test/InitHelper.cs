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
            Player playerHome1 = new Player("Mignolet", PlayerType.GoalkeeperHome, TestPlayground, 1, 70, 90, 50, 85, 80);
            Player playerHome2 = new Player("Moreno", PlayerType.LeftDefenderHome, TestPlayground, 18, 90, 80, 70, 75, 75);
            Player playerHome3 = new Player("Clyne", PlayerType.RightDefenderHome, TestPlayground, 5, 90, 85, 65, 80, 75);
            Player playerHome4 = new Player("Skrtel", PlayerType.CentralDefenderHomeRight, TestPlayground, 4, 75, 90, 50, 85, 70);
            Player playerHome5 = new Player("Sakho", PlayerType.CentralDefenderHomeLeft, TestPlayground, 3, 80, 90, 50, 85, 75);
            Player playerHome6 = new Player("Can", PlayerType.DefensiveMidfielderHome, TestPlayground, 23, 75, 85, 70, 85, 80);
            Player playerHome7 = new Player("Henderson", PlayerType.CentralMidfielderHome, TestPlayground, 14, 80, 80, 80, 85, 80);
            Player playerHome8 = new Player("Milner", PlayerType.RightMidfielderHome, TestPlayground, 7, 70, 80, 75, 85, 80);
            Player playerHome9 = new Player("Coutinho", PlayerType.LeftMidfielderHome, TestPlayground, 10, 85, 70, 90, 85, 85);
            Player playerHome10 = new Player("Lallana", PlayerType.AttackMidfielderHome, TestPlayground, 20, 88, 65, 90, 82, 85);
            Player playerHome11 = new Player("Benteke", PlayerType.CentralForwardHome, TestPlayground, 9, 70, 80, 65, 85, 85);
            List<Player> playersHome = new List<Player>();
            playersHome.Add(playerHome1); playersHome.Add(playerHome2); playersHome.Add(playerHome3); playersHome.Add(playerHome4);
            playersHome.Add(playerHome5); playersHome.Add(playerHome6); playersHome.Add(playerHome7); playersHome.Add(playerHome8);
            playersHome.Add(playerHome9); playersHome.Add(playerHome10); playersHome.Add(playerHome11);
            Player playerAway1 = new Player("De Gea", PlayerType.GoalkeeperAway, TestPlayground, 1, 70, 90, 50, 85, 80);
            Player playerAway2 = new Player("Blind", PlayerType.LeftDefenderAway, TestPlayground, 2, 70, 80, 70, 85, 80);
            Player playerAway3 = new Player("Darmian", PlayerType.RightDefenderAway, TestPlayground, 3, 70, 85, 80, 85, 80);
            Player playerAway4 = new Player("Rojo", PlayerType.CentralDefenderAwayLeft, TestPlayground, 4, 70, 90, 50, 85, 80);
            Player playerAway5 = new Player("Smalling", PlayerType.CentralDefenderAwayRight, TestPlayground, 5, 72, 90, 50, 85, 80);
            Player playerAway6 = new Player("Carrick", PlayerType.DefensiveMidfielderAway, TestPlayground, 6, 73, 90, 50, 85, 80);
            Player playerAway7 = new Player("Herrera", PlayerType.CentralMidfielderAway, TestPlayground, 7, 77, 90, 50, 85, 80);
            Player playerAway8 = new Player("Depay", PlayerType.LeftMidfielderAway, TestPlayground, 8, 70, 90, 50, 85, 80);
            Player playerAway9 = new Player("Valencia", PlayerType.RightMidfielderAway, TestPlayground, 9, 76, 90, 50, 85, 80);
            Player playerAway10 = new Player("Mata", PlayerType.AttackMidfielderAway, TestPlayground, 10, 74, 80, 70, 85, 80);
            Player playerAway11 = new Player("Rooney", PlayerType.CentralForwardAway, TestPlayground, 11, 72, 85, 60, 85, 80);
            List<Player> playersAway = new List<Player>();
            playersAway.Add(playerAway1); playersAway.Add(playerAway2); playersAway.Add(playerAway3); playersAway.Add(playerAway4);
            playersAway.Add(playerAway5); playersAway.Add(playerAway6); playersAway.Add(playerAway7); playersAway.Add(playerAway8);
            playersAway.Add(playerAway9); playersAway.Add(playerAway10); playersAway.Add(playerAway11);
        }
    }
}
