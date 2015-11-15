using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using GalaxyFootball.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
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
        private MainWindowVM _model;
        public MainWindow()
        {
            PlaygroundZone zone1 = new PlaygroundZone();
            zone1.Id = 1;
            zone1.LeftBottom = new GalaxyFootball.Core.Concrete.Helper.Point(0, 0);
            zone1.LeftTop = new GalaxyFootball.Core.Concrete.Helper.Point(0, 233);
            zone1.RightBottom = new GalaxyFootball.Core.Concrete.Helper.Point(350, 0);
            zone1.RightTop = new GalaxyFootball.Core.Concrete.Helper.Point(350, 233);
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

            this.DataContext = _model = new MainWindowVM();
            (this.DataContext as MainWindowVM).PlayerPositionsChanged += MainWindow_PlayerPositionsChanged;
            InitializeComponent();
            ChangePlayersPositions();
        }

        void MainWindow_PlayerPositionsChanged(object sender, EventArgs e)
        {
            ChangePlayersPositions();
        }

        private void ChangePlayersPositions()
        {
            Canvas.SetLeft(this.ballMarker, _model.Game.Ball.Position.X);
            Canvas.SetTop(ballMarker, _model.Game.Ball.Position.Y);
            Canvas.SetLeft(this.playerHomeMarker1, _model.Game.TeamHome.Players[0].Position.X);
            Canvas.SetTop(playerHomeMarker1, _model.Game.TeamHome.Players[0].Position.Y);
            Canvas.SetLeft(playerHomeMarker2, _model.Game.TeamHome.Players[1].Position.X);
            Canvas.SetTop(playerHomeMarker2, _model.Game.TeamHome.Players[1].Position.Y);
            Canvas.SetLeft(playerHomeMarker3, _model.Game.TeamHome.Players[2].Position.X);
            Canvas.SetTop(playerHomeMarker3, _model.Game.TeamHome.Players[2].Position.Y);
            Canvas.SetLeft(playerHomeMarker4, _model.Game.TeamHome.Players[3].Position.X);
            Canvas.SetTop(playerHomeMarker4, _model.Game.TeamHome.Players[3].Position.Y);
            Canvas.SetLeft(playerHomeMarker5, _model.Game.TeamHome.Players[4].Position.X);
            Canvas.SetTop(playerHomeMarker5, _model.Game.TeamHome.Players[4].Position.Y);
            Canvas.SetLeft(playerHomeMarker6, _model.Game.TeamHome.Players[5].Position.X);
            Canvas.SetTop(playerHomeMarker6, _model.Game.TeamHome.Players[5].Position.Y);
            Canvas.SetLeft(playerHomeMarker7, _model.Game.TeamHome.Players[6].Position.X);
            Canvas.SetTop(playerHomeMarker7, _model.Game.TeamHome.Players[6].Position.Y);
            Canvas.SetLeft(playerHomeMarker8, _model.Game.TeamHome.Players[7].Position.X);
            Canvas.SetTop(playerHomeMarker8, _model.Game.TeamHome.Players[7].Position.Y);
            Canvas.SetLeft(playerHomeMarker9, _model.Game.TeamHome.Players[8].Position.X);
            Canvas.SetTop(playerHomeMarker9, _model.Game.TeamHome.Players[8].Position.Y);
            Canvas.SetLeft(playerHomeMarker10, _model.Game.TeamHome.Players[9].Position.X);
            Canvas.SetTop(playerHomeMarker10, _model.Game.TeamHome.Players[9].Position.Y);
            Canvas.SetLeft(playerHomeMarker11, _model.Game.TeamHome.Players[10].Position.X);
            Canvas.SetTop(playerHomeMarker11, _model.Game.TeamHome.Players[10].Position.Y);

            Canvas.SetLeft(playerAwayMarker1, _model.Game.TeamAway.Players[0].Position.X);
            Canvas.SetTop(playerAwayMarker1, _model.Game.TeamAway.Players[0].Position.Y);
            Canvas.SetLeft(playerAwayMarker2, _model.Game.TeamAway.Players[1].Position.X);
            Canvas.SetTop(playerAwayMarker2, _model.Game.TeamAway.Players[1].Position.Y);
            Canvas.SetLeft(playerAwayMarker3, _model.Game.TeamAway.Players[2].Position.X);
            Canvas.SetTop(playerAwayMarker3, _model.Game.TeamAway.Players[2].Position.Y);
            Canvas.SetLeft(playerAwayMarker4, _model.Game.TeamAway.Players[3].Position.X);
            Canvas.SetTop(playerAwayMarker4, _model.Game.TeamAway.Players[3].Position.Y);
            Canvas.SetLeft(playerAwayMarker5, _model.Game.TeamAway.Players[4].Position.X);
            Canvas.SetTop(playerAwayMarker5, _model.Game.TeamAway.Players[4].Position.Y);
            Canvas.SetLeft(playerAwayMarker6, _model.Game.TeamAway.Players[5].Position.X);
            Canvas.SetTop(playerAwayMarker6, _model.Game.TeamAway.Players[5].Position.Y);
            Canvas.SetLeft(playerAwayMarker7, _model.Game.TeamAway.Players[6].Position.X);
            Canvas.SetTop(playerAwayMarker7, _model.Game.TeamAway.Players[6].Position.Y);
            Canvas.SetLeft(playerAwayMarker8, _model.Game.TeamAway.Players[7].Position.X);
            Canvas.SetTop(playerAwayMarker8, _model.Game.TeamAway.Players[7].Position.Y);
            Canvas.SetLeft(playerAwayMarker9, _model.Game.TeamAway.Players[8].Position.X);
            Canvas.SetTop(playerAwayMarker9, _model.Game.TeamAway.Players[8].Position.Y);
            Canvas.SetLeft(playerAwayMarker10, _model.Game.TeamAway.Players[9].Position.X);
            Canvas.SetTop(playerAwayMarker10, _model.Game.TeamAway.Players[9].Position.Y);
            Canvas.SetLeft(playerAwayMarker11, _model.Game.TeamAway.Players[10].Position.X);
            Canvas.SetTop(playerAwayMarker11, _model.Game.TeamAway.Players[10].Position.Y);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            for (int i = 0; i < 50; i++)
            {
                _model.Game.Ball.Position = new GalaxyFootball.Core.Concrete.Helper.Point(_model.Game.Ball.Position.X + r.Next(-3, 3) * 2, _model.Game.Ball.Position.Y + r.Next(-3, 3) * 2);
                Thread.Sleep(500);
            }
        }

        private async void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Random r = new Random();
            for (int i = 0; i < 50000; i++)
            {
                _model.Game.Ball.Position = new GalaxyFootball.Core.Concrete.Helper.Point(_model.Game.Ball.Position.X + r.Next(-3, 3) * 2, _model.Game.Ball.Position.Y + r.Next(-3, 3) * 2);
                await Task.Delay(10);
            }
            //Random r = new Random();
            //Canvas.SetTop(ballMarker, r.Next(100, 400));
            //Canvas.SetLeft(ballMarker, r.Next(100, 400));
        }
    }
}
