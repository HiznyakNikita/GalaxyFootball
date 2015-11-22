using GalaxyFootball.Core;
using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using GalaxyFootball.Core.Concrete.TeamStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalaxyFootball.Test
{
    [TestClass]
    public class PlayerTest
    {
        private Player _testPlayer1 = new Player("TestPlayer1", PlayerType.CentralForwardHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
        private Player _testPlayer2 = new Player("TestPlayer2", PlayerType.CentralDefenderAwayRight, new Playground("Test"), 3,75,98,50,75,75);
        private Ball _testBall = new Ball();

        #region Work with ball tests

        [TestMethod]
        public void ShootTest()
        {
            _testBall.Position = new Core.Concrete.Helper.Point(920,240);
            _testBall.Owner = _testPlayer1;
            _testPlayer1.Shoot(_testBall);
            Thread.Sleep(5000);
            Assert.AreEqual(1030, _testBall.Position.X);
        }

        [TestMethod]
        public void PickTest()
        {
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testBall.Owner = _testPlayer1;
            _testPlayer2.SetStartPosition(new Core.Concrete.Helper.Point(918, 242));
            _testPlayer2.Pick(_testBall);
            Thread.Sleep(5000);
            Assert.AreEqual(_testPlayer2, _testBall.Owner);
        }

        [TestMethod]
        public void PassTest()
        {
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testBall.Owner = _testPlayer1;
            _testPlayer2.SetStartPosition(new Core.Concrete.Helper.Point(875, 116)); 
            _testPlayer1.Pass(_testBall, _testPlayer2);
            Thread.Sleep(5000);
            Assert.AreEqual(_testPlayer2, _testBall.Owner);
            Assert.IsTrue(Math.Abs(_testBall.Position.X - _testPlayer2.Position.X)<5);
            Assert.IsTrue(Math.Abs(_testBall.Position.Y - _testPlayer2.Position.Y) < 5);
        }

        #endregion

        #region Keyboard move tests

        [TestMethod]
        public void KeyboardUpMoveTest()
        {
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(800,340));
            _testBall.Owner = _testPlayer1;
            _testPlayer1.IsSelected = true;
            double playerYPos = _testPlayer1.Position.Y;
            _testPlayer1.SetMoveDirection(true);
            Assert.AreEqual(-_testPlayer1.SpeedPoints / (Double)200 + playerYPos, _testPlayer1.Position.Y);
        }

        [TestMethod]
        public void KeyboardDownMoveTest()
        {
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451,new BalancedStrategy(),new List<Player>()),
                new Team(TeamScheme.Home451,new BalancedStrategy(),new List<Player>()),_testBall,new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(800, 340));
            _testBall.Owner = _testPlayer1;
            _testPlayer1.IsSelected = true;
            double playerYPos = _testPlayer1.Position.Y;
            _testPlayer1.SetMoveDirection(false,true);
            Assert.AreEqual(_testPlayer1.SpeedPoints / (Double)200 + playerYPos, _testPlayer1.Position.Y);
        }

        [TestMethod]
        public void KeyboardLeftMoveTest()
        {
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()),
    new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(800, 340));
            _testBall.Owner = _testPlayer1;
            _testPlayer1.IsSelected = true;
            double playerXPos = _testPlayer1.Position.X;
            _testPlayer1.SetMoveDirection(false,false,false,true);
            Assert.AreEqual(-_testPlayer1.SpeedPoints / (Double)200 + playerXPos, _testPlayer1.Position.X);
        }

        [TestMethod]
        public void KeyboardRightMoveTest()
        {
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()),
    new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(800, 340));
            _testBall.Owner = _testPlayer1;
            _testPlayer1.IsSelected = true;
            double playerXPos = _testPlayer1.Position.X;
            _testPlayer1.SetMoveDirection(false,false,true,false);
            Assert.AreEqual(_testPlayer1.SpeedPoints / (Double)200 + playerXPos, _testPlayer1.Position.X);
        }

        #endregion

        #region Move tests

        [TestMethod]
        public void DefenderAttackBallAheadTest()
        {
            Player _testPlayer3 = new Player("TestPlayer3", PlayerType.LeftDefenderHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>() { _testPlayer1, _testPlayer3 } ),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testBall.Owner = _testPlayer1;
            _testPlayer3.SetStartPosition(new Core.Concrete.Helper.Point(175, 407));
            double playerXPos = _testPlayer3.Position.X;
            _testPlayer3.ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
            Assert.AreEqual(_testPlayer3.SpeedPoints / (Double)(400) + playerXPos, _testPlayer3.Position.X);
        }

        [TestMethod]
        public void DefenderAttackBallInZoneTest()
        {
            Player _testPlayer3 = new Player("TestPlayer3", PlayerType.LeftDefenderHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>() { _testPlayer1, _testPlayer3 }),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(185, 417);
            _testBall.Owner = _testPlayer1;
            _testPlayer3.SetStartPosition(new Core.Concrete.Helper.Point(175, 407));
            double playerXPos = _testPlayer3.Position.X;
            double playerYPos = _testPlayer3.Position.Y;
            _testPlayer3.ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
            Assert.AreEqual(_testPlayer3.SpeedPoints / (Double)(200) + playerXPos, _testPlayer3.Position.X);
            Assert.AreEqual(_testPlayer3.SpeedPoints / (Double)(200) + playerYPos, _testPlayer3.Position.Y);
        }

        [TestMethod]
        public void DefenderAttackBallBehindTest()
        {
            Player _testPlayer3 = new Player("TestPlayer3", PlayerType.LeftDefenderHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>() { _testPlayer1, _testPlayer3 }),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(165, 590);
            _testBall.Owner = _testPlayer1;
            _testPlayer3.SetStartPosition(new Core.Concrete.Helper.Point(175, 407));
            double playerXPos = _testPlayer3.Position.X;
            _testPlayer3.ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
            Assert.AreEqual(-_testPlayer3.SpeedPoints / (Double)(200) + playerXPos, _testPlayer3.Position.X);
        }

        [TestMethod]
        public void MidfielderAttackBallAheadTest()
        {
            Player _testPlayer3 = new Player("TestPlayer3", PlayerType.CentralMidfielderHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>() { _testPlayer1, _testPlayer3 }),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testBall.Owner = _testPlayer1;
            _testPlayer3.SetStartPosition(new Core.Concrete.Helper.Point(393, 349));
            double playerXPos = _testPlayer3.Position.X;
            double playerYPos = _testPlayer3.Position.Y;
            _testPlayer3.ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
            Assert.AreEqual(_testPlayer3.SpeedPoints / (Double)(400) + playerXPos, _testPlayer3.Position.X);
            Assert.AreEqual(- _testPlayer3.SpeedPoints / (Double)(400) + playerYPos, _testPlayer3.Position.Y);
        }

        [TestMethod]
        public void MidfielderAttackBallInZoneTest()
        {
            Player _testPlayer3 = new Player("TestPlayer3", PlayerType.CentralMidfielderHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>() { _testPlayer1, _testPlayer3 }),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(373, 417);
            _testBall.Owner = _testPlayer1;
            _testPlayer3.SetStartPosition(new Core.Concrete.Helper.Point(393, 349));
            double playerXPos = _testPlayer3.Position.X;
            double playerYPos = _testPlayer3.Position.Y;
            _testPlayer3.ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
            Assert.AreEqual(-_testPlayer3.SpeedPoints / (Double)(200) + playerXPos, _testPlayer3.Position.X);
            Assert.AreEqual(_testPlayer3.SpeedPoints / (Double)(200) + playerYPos, _testPlayer3.Position.Y);
        }

        [TestMethod]
        public void MidfielderAttackBallBehindTest()
        {
            Player _testPlayer3 = new Player("TestPlayer3", PlayerType.CentralMidfielderHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>() { _testPlayer1, _testPlayer3 }),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(240, 320);
            _testBall.Owner = _testPlayer1;
            _testPlayer3.SetStartPosition(new Core.Concrete.Helper.Point(393, 349));
            double playerXPos = _testPlayer3.Position.X;
            double playerYPos = _testPlayer3.Position.Y;
            _testPlayer3.ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
            Assert.AreEqual(-_testPlayer3.SpeedPoints / (Double)(400) + playerXPos, _testPlayer3.Position.X);
            Assert.AreEqual(-_testPlayer3.SpeedPoints / (Double)(400) + playerYPos, _testPlayer3.Position.Y);
        }

        [TestMethod]
        public void ForwardAttackBallAheadTest()
        {
            Player _testPlayer3 = new Player("TestPlayer3", PlayerType.CentralForwardHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>() { _testPlayer1, _testPlayer3 }),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(175, 349));
            _testBall.Position = new Core.Concrete.Helper.Point(940, 300);
            _testBall.Owner = _testPlayer1;
            _testPlayer3.SetStartPosition(new Core.Concrete.Helper.Point(875, 349));
            double playerXPos = _testPlayer3.Position.X;
            double playerYPos = _testPlayer3.Position.Y;
            _testPlayer3.ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
            Assert.AreEqual(_testPlayer3.SpeedPoints / (Double)(200) + playerXPos, _testPlayer3.Position.X);
            Assert.AreEqual(-_testPlayer3.SpeedPoints / (Double)(200) + playerYPos, _testPlayer3.Position.Y);
        }

        [TestMethod]
        public void ForwardAttackBallInZoneTest()
        {
            Player _testPlayer3 = new Player("TestPlayer3", PlayerType.CentralForwardHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>() { _testPlayer1, _testPlayer3 }),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(920, 340);
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(175, 349));
            _testBall.Owner = _testPlayer1;
            _testPlayer3.SetStartPosition(new Core.Concrete.Helper.Point(875, 349));
            double playerXPos = _testPlayer3.Position.X;
            double playerYPos = _testPlayer3.Position.Y;
            _testPlayer3.ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
            Assert.AreEqual(_testPlayer3.SpeedPoints / (Double)(200) + playerXPos, _testPlayer3.Position.X);
            Assert.AreEqual(-_testPlayer3.SpeedPoints / (Double)(200) + playerYPos, _testPlayer3.Position.Y);
        }

        [TestMethod]
        public void ForwardAttackBallBehindTest()
        {
            Player _testPlayer3 = new Player("TestPlayer3", PlayerType.CentralForwardHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>() { _testPlayer1, _testPlayer3 }),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(240, 320);
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(175, 349));
            _testBall.Owner = _testPlayer1;
            _testPlayer3.SetStartPosition(new Core.Concrete.Helper.Point(875, 349));
            double playerXPos = _testPlayer3.Position.X;
            double playerYPos = _testPlayer3.Position.Y;
            _testPlayer3.ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
            Assert.AreEqual(-_testPlayer3.SpeedPoints / (Double)(400) + playerXPos, _testPlayer3.Position.X);
            Assert.AreEqual(-_testPlayer3.SpeedPoints / (Double)(400) + playerYPos, _testPlayer3.Position.Y);
        }

        [TestMethod]
        public void IntersectWithPartnerInZoneTest()
        {
            Player _testPlayer3 = new Player("TestPlayer3", PlayerType.CentralForwardHome, new Playground("Test"), 7, 80, 50, 80, 95, 65);
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>() { _testPlayer1, _testPlayer3 }),
                new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), _testBall, new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(240, 320);
            _testBall.Owner = _testPlayer1;
            _testPlayer3.SetStartPosition(new Core.Concrete.Helper.Point(875, 349));
            double playerXPos = _testPlayer3.Position.X;
            double playerYPos = _testPlayer3.Position.Y;
            _testPlayer3.ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
            Assert.AreEqual(1 / (Double)(200) + playerXPos, _testPlayer3.Position.X);
            Assert.AreEqual(1 / (Double)(200) + playerYPos, _testPlayer3.Position.Y);
        }

        #endregion
    }
}
