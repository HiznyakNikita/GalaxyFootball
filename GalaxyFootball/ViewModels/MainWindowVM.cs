using GalaxyFootball.Core;
using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using GalaxyFootball.Core.Concrete.Helper.EventArgsHelpers;
using GalaxyFootball.Core.Concrete.TeamStrategies;
using GalaxyFootball.Helpers;
using GalaxyFootball.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.ViewModels
{
    public class MainWindowVM: IModel, INotifyPropertyChanged
    {
        private Game _game;
        private Playground _playground = new Playground("Anfield");
        Player _currentSelectedPlayer;
        public MainWindowVM()
        {
            _game = InitGameStub();
            _game.Ball.PositionChanged += Ball_PositionChanged;
            _currentSelectedPlayer = GameEngine.CurrentGame.TeamHome.Players[6];//GameEngine.CurrentGame.TeamHome.Players[6];
            ChangePlayerCommand = new Command(ChangePlayer);
            GameEngine.CurrentGame.Ball.GoalScored += Ball_GoalScored;
            GameEngine.CurrentGame.Ball.OutOfPlayground += Ball_OutOfPlayground;
            _game.Ball.Notify();
        }

        public event EventHandler PlayerPositionsChanged;

        public Game Game 
        { 
            get
            {
                return _game;
            }
            set
            {
                _game = value;
            }
        }

        public Player WhoScored { get; set; }
        public string Score 
        { 
            get
            {
                return Game.TeamHome.Name + " " + Game.GoalsHome + "-" + Game.GoalsAway + " " + Game.TeamAway.Name;
            } 
        }

        private void Ball_PositionChanged(object sender, EventArgs e)
        {
            OnPlayerPositionChanged();
        }

        private void OnPlayerPositionChanged()
        {
            if (PlayerPositionsChanged != null)
            {
                PlayerPositionsChanged(this, EventArgs.Empty);
            }
        }

        private Game InitGameStub()
        {
            Player playerHome1 = new Player("Mignolet", PlayerType.GoalkeeperHome, _playground, 99, 70, 90, 50, 85, 80);
            Player playerHome2 = new Player("Moreno", PlayerType.LeftDefenderHome, _playground, 18, 90, 80, 70, 75, 75);
            Player playerHome3 = new Player("Clyne", PlayerType.RightDefenderHome, _playground, 5, 90, 85, 65, 80, 75);
            Player playerHome4 = new Player("Skrtel", PlayerType.CentralDefenderHomeRight, _playground, 4, 75, 90, 50, 85, 70);
            Player playerHome5 = new Player("Sakho", PlayerType.CentralDefenderHomeLeft, _playground, 3, 80, 90, 50, 85, 75);
            Player playerHome6 = new Player("Can", PlayerType.DefensiveMidfielderHome, _playground, 23, 75, 85, 70, 85, 80);
            Player playerHome7 = new Player("Henderson", PlayerType.CentralMidfielderHome, _playground, 14, 80, 80, 80, 85, 80);
            Player playerHome8 = new Player("Milner", PlayerType.RightMidfielderHome, _playground, 7, 70, 80, 75, 85, 80);
            Player playerHome9 = new Player("Coutinho", PlayerType.LeftMidfielderHome, _playground, 10, 85, 70, 90, 85, 85);
            Player playerHome10 = new Player("Lallana", PlayerType.AttackMidfielderHome, _playground, 20, 88, 65, 90, 82, 85);
            Player playerHome11 = new Player("Benteke", PlayerType.CentralForwardHome, _playground, 9, 70, 80, 65, 85, 85);
            List<Player> homeTeamPlayers = new List<Player>();
            homeTeamPlayers.Add(playerHome1); homeTeamPlayers.Add(playerHome2); homeTeamPlayers.Add(playerHome3); homeTeamPlayers.Add(playerHome4);
            homeTeamPlayers.Add(playerHome5); homeTeamPlayers.Add(playerHome6);
            homeTeamPlayers.Add(playerHome7); homeTeamPlayers.Add(playerHome8);
            homeTeamPlayers.Add(playerHome9); homeTeamPlayers.Add(playerHome10); homeTeamPlayers.Add(playerHome11);
            Player playerAway1 = new Player("De Gea", PlayerType.GoalkeeperAway, _playground, 1, 70, 90, 50, 85, 80);
            Player playerAway2 = new Player("Blind", PlayerType.LeftDefenderAway, _playground, 2, 70, 80, 70, 85, 80);
            Player playerAway3 = new Player("Darmian", PlayerType.RightDefenderAway, _playground, 3, 70, 85, 80, 85, 80);
            Player playerAway4 = new Player("Rojo", PlayerType.CentralDefenderAwayLeft, _playground, 4, 70, 90, 50, 85, 80);
            Player playerAway5 = new Player("Smalling", PlayerType.CentralDefenderAwayRight, _playground, 5, 72, 90, 50, 85, 80);
            Player playerAway6 = new Player("Carrick", PlayerType.DefensiveMidfielderAway, _playground, 6, 73, 90, 50, 85, 80);
            Player playerAway7 = new Player("Herrera", PlayerType.CentralMidfielderAway, _playground, 7, 77, 90, 50, 85, 80);
            Player playerAway8 = new Player("Depay", PlayerType.LeftMidfielderAway, _playground, 8, 70, 90, 50, 85, 80);
            Player playerAway9 = new Player("Valencia", PlayerType.RightMidfielderAway, _playground, 9, 76, 90, 50, 85, 80);
            Player playerAway10 = new Player("Mata", PlayerType.AttackMidfielderAway, _playground, 10, 74, 80, 70, 85, 80);
            Player playerAway11 = new Player("Rooney", PlayerType.CentralForwardAway, _playground, 11, 72, 85, 60, 85, 80);
            List<Player> awayTeamPlayers = new List<Player>();
            awayTeamPlayers.Add(playerAway1); awayTeamPlayers.Add(playerAway2); awayTeamPlayers.Add(playerAway3); awayTeamPlayers.Add(playerAway4);
            awayTeamPlayers.Add(playerAway5); awayTeamPlayers.Add(playerAway6); awayTeamPlayers.Add(playerAway7); awayTeamPlayers.Add(playerAway8);
            awayTeamPlayers.Add(playerAway9); awayTeamPlayers.Add(playerAway10); awayTeamPlayers.Add(playerAway11);

            Team homeTeam = new Team(TeamScheme.Home451, new BalancedStrategy(), homeTeamPlayers);
            Team awayTeam = new Team(TeamScheme.Away451, new BalancedStrategy(), awayTeamPlayers);
            Ball ball = new Ball();
            ball.Owner = playerHome7;//playerHome7;
            ball.AttachObserver(homeTeam);
            ball.AttachObserver(awayTeam);
            Game game = new Game(homeTeam, awayTeam, ball, _playground);
            return game;
        }

        #region Commands

        private Command ChangePlayerCommand;
        public void ChangePlayer(object param)
        {
            _currentSelectedPlayer = GameEngine.CurrentGame.TeamHome.Players.Where(p => p.IsSelected).FirstOrDefault();
            if (!GameEngine.CurrentGame.Ball.Owner.Equals(_currentSelectedPlayer))
            {
                Player nearestPlayer = _currentSelectedPlayer.FindNearestPlayer();
                _currentSelectedPlayer.IsSelected = false;
                nearestPlayer.IsSelected = true;
            }
        }

        public void MoveSelectedPlayer(bool isUp = false, bool isDown = false, bool isRight = false, bool isLeft = false)
        {
            _currentSelectedPlayer = GameEngine.CurrentGame.TeamHome.Players.Where(p => p.IsSelected).FirstOrDefault();
            _currentSelectedPlayer.SetMoveDirection(isUp, isDown, isRight, isLeft);
        }

        public void ActionSelectedPlayer(
            bool isShoot, 
            bool isPass,
            bool isPick,
            bool isUp = false, 
            bool isDown = false, 
            bool isRight = false, 
            bool isLeft = false)
        {
            _currentSelectedPlayer = GameEngine.CurrentGame.TeamHome.Players.Where(p => p.IsSelected).FirstOrDefault();
            if (isPass)
                _currentSelectedPlayer.Pass(GameEngine.CurrentGame.Ball, _currentSelectedPlayer.FindPartnerForPass(isUp,isDown,isRight,isLeft));
            if (isShoot)
                _currentSelectedPlayer.Shoot(GameEngine.CurrentGame.Ball);
            if (isPick)
                _currentSelectedPlayer.Pick(GameEngine.CurrentGame.Ball);
        }

        #endregion

       void Ball_GoalScored(object sender, EventArgs e)
        {
            if ((e as GoalScoredEventArgs).IsHomeScored)
                GameEngine.CurrentGame.GoalsHome++;
            else
                GameEngine.CurrentGame.GoalsAway++;
            WhoScored = (e as GoalScoredEventArgs).WhoScored;
            NotifyPropertyChanged("WhoScored");
            NotifyPropertyChanged("Score");
            _game.ResetPositionsAfterGoal((e as GoalScoredEventArgs).IsHomeScored);
            OnPlayerPositionChanged();
        }

        void Ball_OutOfPlayground(object sender, EventArgs e)
        {
            _game.ResetPositionsAfterOut((e as OutOfPlaygroundEventArgs).IsHomeSideOut);
            OnPlayerPositionChanged();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region INotifyPropertyChanged

        protected void NotifyPropertyChanged(string property)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion
    }
}
