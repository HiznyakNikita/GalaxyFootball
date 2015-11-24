using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using GalaxyFootball.UserControls;
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
        PlayerMarkerRed playerHomeMarker1;
        PlayerMarkerRed playerHomeMarker2;
        PlayerMarkerRed playerHomeMarker3;

        PlayerMarkerRed playerHomeMarker4;

        PlayerMarkerRed playerHomeMarker5;

        PlayerMarkerRed playerHomeMarker6;
        PlayerMarkerRed playerHomeMarker7;
        PlayerMarkerRed playerHomeMarker8;
        PlayerMarkerRed playerHomeMarker9;
        PlayerMarkerRed playerHomeMarker10;
        PlayerMarkerRed playerHomeMarker11;

        PlayerMarkerBlack playerAwayMarker1;
        PlayerMarkerBlack playerAwayMarker2;

        PlayerMarkerBlack playerAwayMarker3;
        PlayerMarkerBlack playerAwayMarker4;
        PlayerMarkerBlack playerAwayMarker5;
        PlayerMarkerBlack playerAwayMarker6;
        PlayerMarkerBlack playerAwayMarker7;
        PlayerMarkerBlack playerAwayMarker8;
        PlayerMarkerBlack playerAwayMarker9;
        PlayerMarkerBlack playerAwayMarker10;
        PlayerMarkerBlack playerAwayMarker11;



        public MainWindow()
        {
            this.DataContext = _model = new MainWindowVM();
            (this.DataContext as MainWindowVM).PlayerPositionsChanged += MainWindow_PlayerPositionsChanged;
            InitializeComponent();
            playerHomeMarker1 = new PlayerMarkerRed(new PlayerMarkerVM(1, GameEngine.CurrentGame.TeamHome.Players[0]));
            playerHomeMarker2 = new PlayerMarkerRed(new PlayerMarkerVM(4, GameEngine.CurrentGame.TeamHome.Players[1]));
            playerHomeMarker3 = new PlayerMarkerRed(new PlayerMarkerVM(2, GameEngine.CurrentGame.TeamHome.Players[2]));

            playerHomeMarker4 = new PlayerMarkerRed(new PlayerMarkerVM(3, GameEngine.CurrentGame.TeamHome.Players[3]));

            playerHomeMarker5 = new PlayerMarkerRed(new PlayerMarkerVM(5, GameEngine.CurrentGame.TeamHome.Players[4]));
            playerHomeMarker6 = new PlayerMarkerRed(new PlayerMarkerVM(6, GameEngine.CurrentGame.TeamHome.Players[5]));
            playerHomeMarker7 = new PlayerMarkerRed(new PlayerMarkerVM(14, GameEngine.CurrentGame.TeamHome.Players[6]));
            playerHomeMarker8 = new PlayerMarkerRed(new PlayerMarkerVM(7, GameEngine.CurrentGame.TeamHome.Players[7]));
            playerHomeMarker9 = new PlayerMarkerRed(new PlayerMarkerVM(11, GameEngine.CurrentGame.TeamHome.Players[8]));
            playerHomeMarker10 = new PlayerMarkerRed(new PlayerMarkerVM(10, GameEngine.CurrentGame.TeamHome.Players[9]));
            playerHomeMarker11 = new PlayerMarkerRed(new PlayerMarkerVM(9, GameEngine.CurrentGame.TeamHome.Players[10]));

            playerAwayMarker1 = new PlayerMarkerBlack(new PlayerMarkerVM(1, GameEngine.CurrentGame.TeamAway.Players[0]));
            playerAwayMarker2 = new PlayerMarkerBlack(new PlayerMarkerVM(4, GameEngine.CurrentGame.TeamAway.Players[1]));
            playerAwayMarker3 = new PlayerMarkerBlack(new PlayerMarkerVM(3, GameEngine.CurrentGame.TeamAway.Players[2]));
            playerAwayMarker4 = new PlayerMarkerBlack(new PlayerMarkerVM(2, GameEngine.CurrentGame.TeamAway.Players[3]));
            playerAwayMarker5 = new PlayerMarkerBlack(new PlayerMarkerVM(5, GameEngine.CurrentGame.TeamAway.Players[4]));
            playerAwayMarker6 = new PlayerMarkerBlack(new PlayerMarkerVM(7, GameEngine.CurrentGame.TeamAway.Players[5]));
            playerAwayMarker7 = new PlayerMarkerBlack(new PlayerMarkerVM(6, GameEngine.CurrentGame.TeamAway.Players[6]));
            playerAwayMarker8 = new PlayerMarkerBlack(new PlayerMarkerVM(8, GameEngine.CurrentGame.TeamAway.Players[7]));
            playerAwayMarker9 = new PlayerMarkerBlack(new PlayerMarkerVM(9, GameEngine.CurrentGame.TeamAway.Players[8]));
            playerAwayMarker10 = new PlayerMarkerBlack(new PlayerMarkerVM(10, GameEngine.CurrentGame.TeamAway.Players[9]));
            playerAwayMarker11 = new PlayerMarkerBlack(new PlayerMarkerVM(22, GameEngine.CurrentGame.TeamAway.Players[10]));


            _model.Game.TeamHome.Players[6].IsSelected = true;
            ChangePlayersPositions();
            PlaygroundCanvas.Children.Add(playerHomeMarker1);
            PlaygroundCanvas.Children.Add(playerHomeMarker2);
            PlaygroundCanvas.Children.Add(playerHomeMarker3);
            PlaygroundCanvas.Children.Add(playerHomeMarker4);
            PlaygroundCanvas.Children.Add(playerHomeMarker5);
            PlaygroundCanvas.Children.Add(playerHomeMarker6);
            PlaygroundCanvas.Children.Add(playerHomeMarker7);
            PlaygroundCanvas.Children.Add(playerHomeMarker8);

            PlaygroundCanvas.Children.Add(playerHomeMarker9);
            PlaygroundCanvas.Children.Add(playerHomeMarker10);
            PlaygroundCanvas.Children.Add(playerHomeMarker11);

            PlaygroundCanvas.Children.Add(playerAwayMarker1);
            PlaygroundCanvas.Children.Add(playerAwayMarker2);
            PlaygroundCanvas.Children.Add(playerAwayMarker3);
            PlaygroundCanvas.Children.Add(playerAwayMarker4);
            PlaygroundCanvas.Children.Add(playerAwayMarker5);
            PlaygroundCanvas.Children.Add(playerAwayMarker6);
            PlaygroundCanvas.Children.Add(playerAwayMarker7);
            PlaygroundCanvas.Children.Add(playerAwayMarker8);
            PlaygroundCanvas.Children.Add(playerAwayMarker9);
            PlaygroundCanvas.Children.Add(playerAwayMarker10);
            PlaygroundCanvas.Children.Add(playerAwayMarker11);
        }

        void MainWindow_PlayerPositionsChanged(object sender, EventArgs e)
        {
            ChangePlayersPositions();
        }

        private void ChangePlayersPositions()
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
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
            }));
        }

        private async void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GameEngine.CurrentGame.Ball.Position = new GalaxyFootball.Core.Concrete.Helper.Point
                (GameEngine.CurrentGame.Ball.Position.X + 1, GameEngine.CurrentGame.Ball.Position.Y + 1);
            //Random r = new Random();
            //Canvas.SetTop(ballMarker, r.Next(100, 400));
            //Canvas.SetLeft(ballMarker, r.Next(100, 400));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Q)
            {
                _model.ChangePlayer(null);
            }
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
            {
                bool isUp = false;
                bool isDown = false;
                bool isRight = false;
                bool isLeft = false;
                if (Keyboard.IsKeyDown(Key.Up))
                    isUp = true;
                if (Keyboard.IsKeyDown(Key.Down))
                    isDown = true;
                if (Keyboard.IsKeyDown(Key.Left))
                    isLeft = true;
                if (Keyboard.IsKeyDown(Key.Right))
                    isRight = true;
                _model.MoveSelectedPlayer(isUp,isDown,isRight,isLeft);
            }
            if(e.Key == Key.X)
            {
                bool isUp = false; 
                bool isDown = false; 
                bool isRight = false; 
                bool isLeft = false;
                if (Keyboard.IsKeyDown(Key.Up))
                    isUp = true;
                if (Keyboard.IsKeyDown(Key.Down))
                    isDown = true;
                if (Keyboard.IsKeyDown(Key.Left))
                    isLeft = true;
                if (Keyboard.IsKeyDown(Key.Right))
                    isRight = true;
                _model.ActionSelectedPlayer(false, true, false, isUp, isDown, isRight, isLeft);
            }
            if(e.Key == Key.D)
            {
                _model.ActionSelectedPlayer(true, false,false);
            }
            if(e.Key == Key.A)
            {
                _model.ActionSelectedPlayer(false, false, true);
            }
        }
    }
}
