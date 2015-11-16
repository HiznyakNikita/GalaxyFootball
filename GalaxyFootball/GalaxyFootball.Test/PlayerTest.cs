using GalaxyFootball.Core;
using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Assert.AreEqual(1030, _testBall.Position.X);
        }

        [TestMethod]
        public void PickTest()
        {
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testBall.Owner = _testPlayer1;
            _testPlayer2.SetStartPosition(new Core.Concrete.Helper.Point(918, 242));
            _testPlayer2.Pick(_testBall);
            Assert.AreEqual(_testPlayer2, _testBall.Owner);
        }

        [TestMethod]
        public void PassTest()
        {
            _testBall.Position = new Core.Concrete.Helper.Point(920, 240);
            _testBall.Owner = _testPlayer1;
            _testPlayer2.SetStartPosition(new Core.Concrete.Helper.Point(875, 116)); 
            _testPlayer1.Pass(_testBall, _testPlayer2);
            Assert.AreEqual(_testPlayer2, _testBall.Owner);
            Assert.IsTrue(Math.Abs(_testBall.Position.X - _testPlayer2.Position.X)<3);
            Assert.IsTrue(Math.Abs(_testBall.Position.Y - _testPlayer2.Position.Y) < 3);
        }
    }
}
