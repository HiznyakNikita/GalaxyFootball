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
