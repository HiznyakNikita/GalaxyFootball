using GalaxyFootball.Core;
using GalaxyFootball.Core.Abstract;
using GalaxyFootball.Core.Concrete.Helper;
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
    public class BallTest
    {
        [TestMethod]
        public void BallObserverTest()
        {
            InitHelper.Init();
            //change position of ball
            InitHelper.TestBall.Position = new Point(505, 320);
            foreach(var p in InitHelper.playersHome)
            {
                //test for move in the same zone
                if(p.Type == PlayerType.DefensiveMidfielderHome)
                {
                    Assert.AreEqual(p.Position.X, 394);
                    Assert.AreEqual(p.Position.Y, 348);
                }
                // test for move in parallel horizontal zone
                if(p.Type == PlayerType.AttackMidfielderHome)
                {
                    Assert.AreEqual(p.Position.X, 700);
                    Assert.AreEqual(p.Position.Y, 348);
                }
                //test for move in diagonal zone
                if (p.Type == PlayerType.LeftDefenderHome)
                {
                    Assert.AreEqual(p.Position.X, 176);
                    Assert.AreEqual(p.Position.Y, 581);
                }
                //test for move in parallel vertical zone
                if (p.Type == PlayerType.LeftMidfielderAway)
                {
                    Assert.AreEqual(p.Position.X, 524);
                    Assert.AreEqual(p.Position.Y, 115);
                }
            }

        }
    }
}
