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


        public MainWindow()
        {
            this.DataContext = _model = new MainWindowVM();
            (this.DataContext as MainWindowVM).PlayerPositionsChanged += MainWindow_PlayerPositionsChanged;
            InitializeComponent();

            _model.Game.TeamHome.Players[6].IsSelected = true;
            ChangePlayersPositions();
        }

        void MainWindow_PlayerPositionsChanged(object sender, EventArgs e)
        {
            ChangePlayersPositions();
        }

        private void ChangePlayersPositions()
        {

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
