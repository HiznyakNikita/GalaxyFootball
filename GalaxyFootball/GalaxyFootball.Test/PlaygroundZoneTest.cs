using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Test
{
    [TestClass]
    public class PlaygroundZoneTest
    {
        [TestMethod]
        public void TestIntersectZone()
        {
            PlaygroundZone zone1 = new PlaygroundZone();
            zone1.Id = 1;
            zone1.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(0, 0);
            zone1.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(0, 233);
            zone1.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(350, 0);
            zone1.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(300, 233);
            zone1.Center = new GalaxyFootball.Core.Concrete.Helper.Point(175, 116);
            zone1.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.DefenderZone;

            Assert.AreEqual(true,zone1.CheckForZoneIntersection(new Point(175, 116)));
            Assert.AreEqual(false, zone1.CheckForZoneIntersection(new Point(500, 500)));
        }
    }
}
