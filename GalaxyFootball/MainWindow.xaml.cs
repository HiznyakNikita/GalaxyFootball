using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace GalaxyFootball
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PlaygroundZone zone1 = new PlaygroundZone();
            zone1.Id = 1;
            zone1.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(0, 0);
            zone1.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(0, 233);
            zone1.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(350, 0);
            zone1.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(300, 233);
            zone1.Center = new GalaxyFootball.Core.Concrete.Helper.Point(175, 116);
            zone1.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.DefenderZone;

            PlaygroundZone zone2 = new PlaygroundZone();
            zone2.Id = 2;
            zone2.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(116, 233);
            zone2.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(116, 466);
            zone2.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(350, 233);
            zone2.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(350, 466);
            zone2.Center = new GalaxyFootball.Core.Concrete.Helper.Point(175, 349);
            zone2.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.DefenderZone;

            PlaygroundZone zone3 = new PlaygroundZone();
            zone3.Id = 3;
            zone3.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(0, 466);
            zone3.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(0, 700);
            zone3.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(350, 466);
            zone3.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(350, 700);
            zone3.Center = new GalaxyFootball.Core.Concrete.Helper.Point(175, 582);
            zone3.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.DefenderZone;

            PlaygroundZone zone4 = new PlaygroundZone();
            zone4.Id = 4;
            zone4.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(350, 466);
            zone4.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(350, 700);
            zone4.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(700, 466);
            zone4.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(700, 700);
            zone4.Center = new GalaxyFootball.Core.Concrete.Helper.Point(525, 582);
            zone4.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.MidfielderZone;

            PlaygroundZone zone5 = new PlaygroundZone();
            zone5.Id = 5;
            zone5.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(350, 233);
            zone5.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(350, 466);
            zone5.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(700, 233);
            zone5.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(700, 466);
            zone5.Center = new GalaxyFootball.Core.Concrete.Helper.Point(525, 349);
            zone5.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.MidfielderZone;

            PlaygroundZone zone6 = new PlaygroundZone();
            zone6.Id = 6;
            zone6.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(350, 0);
            zone6.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(350, 233);
            zone6.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(700, 0);
            zone6.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(700, 233);
            zone6.Center = new GalaxyFootball.Core.Concrete.Helper.Point(525, 116);
            zone6.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.MidfielderZone;

            PlaygroundZone zone7 = new PlaygroundZone();
            zone7.Id = 7;
            zone7.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(700, 0);
            zone7.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(700, 233);
            zone7.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(1050, 0);
            zone7.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(1050, 233);
            zone7.Center = new GalaxyFootball.Core.Concrete.Helper.Point(875, 116);
            zone7.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.ForwardZone;

            PlaygroundZone zone8 = new PlaygroundZone();
            zone8.Id = 8;
            zone8.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(700, 233);
            zone8.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(700, 466);
            zone8.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(934, 233);
            zone8.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(934, 466);
            zone8.Center = new GalaxyFootball.Core.Concrete.Helper.Point(875, 349);
            zone8.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.ForwardZone;

            PlaygroundZone zone9 = new PlaygroundZone();
            zone9.Id = 9;
            zone9.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(700, 466);
            zone9.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(700, 700);
            zone9.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(1050, 466);
            zone9.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(1050, 700);
            zone9.Center = new GalaxyFootball.Core.Concrete.Helper.Point(875, 582);
            zone9.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.ForwardZone;

            PlaygroundZone zone10 = new PlaygroundZone();
            zone10.Id = 10;
            zone10.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(0, 233);
            zone10.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(0, 466);
            zone10.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(116, 233);
            zone10.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(116, 466);
            zone10.Center = new GalaxyFootball.Core.Concrete.Helper.Point(58, 349);
            zone10.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.GoalkeeperZone;

            PlaygroundZone zone11 = new PlaygroundZone();
            zone11.Id = 11;
            zone11.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(934, 233);
            zone11.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(934, 466);
            zone11.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(1050, 233);
            zone11.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(1050, 466);
            zone11.Center = new GalaxyFootball.Core.Concrete.Helper.Point(932, 349);
            zone11.Category = Core.Concrete.Helper.Enums.PlaygroundZoneCategory.GoalkeeperZone;

            zone1.VerticalNeighbour = zone2;
            zone1.HorizontalNeighbour = zone6;
            zone2.VerticalNeighbour = zone3;
            zone2.HorizontalNeighbour = zone5;
            zone3.VerticalNeighbour = zone2;
            zone3.HorizontalNeighbour = zone4;
            zone4.VerticalNeighbour = zone5;
            zone4.HorizontalNeighbour = zone9;
            zone5.VerticalNeighbour = zone8;
            zone5.HorizontalNeighbour = zone6;
            zone6.VerticalNeighbour = zone7;
            zone6.HorizontalNeighbour = zone5;
            zone7.VerticalNeighbour = zone8;
            zone7.HorizontalNeighbour = zone6;
            zone8.VerticalNeighbour = zone9;
            zone8.HorizontalNeighbour = zone5;
            zone9.VerticalNeighbour = zone8;
            zone9.HorizontalNeighbour = zone4;
            zone10.HorizontalNeighbour = zone2;
            zone10.VerticalNeighbour = zone3;
            zone11.HorizontalNeighbour = zone8;
            zone11.VerticalNeighbour = zone9;

            List<PlaygroundZone> zones = new List<PlaygroundZone>();
            zones.Add(zone1); zones.Add(zone2); zones.Add(zone3); zones.Add(zone4); zones.Add(zone5); zones.Add(zone6); zones.Add(zone7); zones.Add(zone8);
            zones.Add(zone9); zones.Add(zone10); zones.Add(zone11);

            try
            {
                using (Stream stream = File.Open("playgroundZones.bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, zones);
                }
            }
            catch (IOException)
            {
            }


            Dictionary<PlayerType, GalaxyFootball.Core.Concrete.Helper.Point> playersDefaultPositions451Away = new Dictionary<PlayerType, GalaxyFootball.Core.Concrete.Helper.Point>();
            //4-5-1 home
            playersDefaultPositions451Away.Add(PlayerType.GoalkeeperAway, new GalaxyFootball.Core.Concrete.Helper.Point(992, 349));
            playersDefaultPositions451Away.Add(PlayerType.LeftDefenderAway, new GalaxyFootball.Core.Concrete.Helper.Point(875, 116));
            playersDefaultPositions451Away.Add(PlayerType.CentralDefenderAwayLeft, new GalaxyFootball.Core.Concrete.Helper.Point(875, 407));
            playersDefaultPositions451Away.Add(PlayerType.CentralDefenderAwayRight, new GalaxyFootball.Core.Concrete.Helper.Point(875, 291));
            playersDefaultPositions451Away.Add(PlayerType.RightDefenderAway, new GalaxyFootball.Core.Concrete.Helper.Point(875, 582));
            playersDefaultPositions451Away.Add(PlayerType.DefensiveMidfielderAway, new GalaxyFootball.Core.Concrete.Helper.Point(612, 349));
            playersDefaultPositions451Away.Add(PlayerType.CentralMidfielderAway, new GalaxyFootball.Core.Concrete.Helper.Point(525, 349));
            playersDefaultPositions451Away.Add(PlayerType.LeftMidfielderAway, new GalaxyFootball.Core.Concrete.Helper.Point(525, 116));
            playersDefaultPositions451Away.Add(PlayerType.RightMidfielderAway, new GalaxyFootball.Core.Concrete.Helper.Point(525, 582));
            playersDefaultPositions451Away.Add(PlayerType.AttackMidfielderAway, new GalaxyFootball.Core.Concrete.Helper.Point(351, 349));
            playersDefaultPositions451Away.Add(PlayerType.CentralForwardAway, new GalaxyFootball.Core.Concrete.Helper.Point(175, 349));

            try
            {
                using (Stream stream = File.Open("playersDefaultPositions451Away.bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, playersDefaultPositions451Away);
                }
            }
            catch (IOException)
            {
            }

            Dictionary<PlayerType, GalaxyFootball.Core.Concrete.Helper.Point> playersDefaultPositions451Home = new Dictionary<PlayerType, GalaxyFootball.Core.Concrete.Helper.Point>();
            //4-5-1 home
            playersDefaultPositions451Home.Add(PlayerType.GoalkeeperHome, new GalaxyFootball.Core.Concrete.Helper.Point(58, 349));
            playersDefaultPositions451Home.Add(PlayerType.LeftDefenderHome, new GalaxyFootball.Core.Concrete.Helper.Point(175, 582));
            playersDefaultPositions451Home.Add(PlayerType.CentralDefenderHomeLeft, new GalaxyFootball.Core.Concrete.Helper.Point(175, 407));
            playersDefaultPositions451Home.Add(PlayerType.CentralDefenderHomeRight, new GalaxyFootball.Core.Concrete.Helper.Point(175, 291));
            playersDefaultPositions451Home.Add(PlayerType.RightDefenderHome, new GalaxyFootball.Core.Concrete.Helper.Point(175, 116));
            playersDefaultPositions451Home.Add(PlayerType.DefensiveMidfielderHome, new GalaxyFootball.Core.Concrete.Helper.Point(393, 349));
            playersDefaultPositions451Home.Add(PlayerType.CentralMidfielderHome, new GalaxyFootball.Core.Concrete.Helper.Point(500, 349));
            playersDefaultPositions451Home.Add(PlayerType.LeftMidfielderHome, new GalaxyFootball.Core.Concrete.Helper.Point(525, 582));
            playersDefaultPositions451Home.Add(PlayerType.RightMidfielderHome, new GalaxyFootball.Core.Concrete.Helper.Point(525, 116));
            playersDefaultPositions451Home.Add(PlayerType.AttackMidfielderHome, new GalaxyFootball.Core.Concrete.Helper.Point(701, 349));
            playersDefaultPositions451Home.Add(PlayerType.CentralForwardHome, new GalaxyFootball.Core.Concrete.Helper.Point(875, 349));

            try
            {
                using (Stream stream = File.Open("playersDefaultPositions451Home.bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, playersDefaultPositions451Home);
                }
            }
            catch (IOException)
            {
            }
        }
    }
}
