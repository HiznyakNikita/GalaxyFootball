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
            Assert.IsTrue(Math.Abs(_testBall.Position.X - _testPlayer2.Position.X)<3);
            Assert.IsTrue(Math.Abs(_testBall.Position.Y - _testPlayer2.Position.Y) < 3);
        }

        [TestMethod]
        public void KeyboardUpMoveTest()
        {
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(800,340));
            _testBall.Owner = _testPlayer1;
            _testPlayer1.IsSelected = true;
            _testPlayer1.SetMoveDirection(true);
            Assert.AreEqual(332, _testPlayer1.Position.Y);
        }

        [TestMethod]
        public void KeyboardDownMoveTest()
        {
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451,new BalancedStrategy(),new List<Player>()),
                new Team(TeamScheme.Home451,new BalancedStrategy(),new List<Player>()),new Ball(),new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(800, 340));
            _testBall.Owner = _testPlayer1;
            _testPlayer1.IsSelected = true;
            _testPlayer1.SetMoveDirection(false,true);
            Assert.AreEqual(348, _testPlayer1.Position.Y);
        }

        [TestMethod]
        public void KeyboardLeftMoveTest()
        {
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()),
    new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), new Ball(), new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(800, 340));
            _testBall.Owner = _testPlayer1;
            _testPlayer1.IsSelected = true;
            _testPlayer1.SetMoveDirection(false,false,false,true);
            Assert.AreEqual(792, _testPlayer1.Position.X);
        }

        [TestMethod]
        public void KeyboardRightMoveTest()
        {
            GameEngine.CurrentGame = new Game(new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()),
    new Team(TeamScheme.Home451, new BalancedStrategy(), new List<Player>()), new Ball(), new Playground("New"));
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testPlayer1.SetStartPosition(new Core.Concrete.Helper.Point(800, 340));
            _testBall.Owner = _testPlayer1;
            _testPlayer1.IsSelected = true;
            _testPlayer1.SetMoveDirection(false,false,true,false);
            Assert.AreEqual(808, _testPlayer1.Position.X);
        }
    }
}
