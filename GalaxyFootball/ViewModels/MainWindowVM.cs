using GalaxyFootball.Core;
using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using GalaxyFootball.Core.Concrete.TeamStrategies;
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
        public MainWindowVM()
        {
            _game = InitGameStub();
            _game.Ball.PositionChanged += Ball_PositionChanged;
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
            Player playerHome1 = new Player("Mignolet", PlayerType.GoalkeeperHome, _playground);
            Player playerHome2 = new Player("Moreno", PlayerType.LeftDefenderHome, _playground);
            Player playerHome3 = new Player("Clyne", PlayerType.RightDefenderHome, _playground);
            Player playerHome4 = new Player("Skrtel", PlayerType.CentralDefenderHomeRight, _playground);
            Player playerHome5 = new Player("Sakho", PlayerType.CentralDefenderHomeLeft, _playground);
            Player playerHome6 = new Player("Can", PlayerType.DefensiveMidfielderHome, _playground);
            Player playerHome7 = new Player("Henderson", PlayerType.CentralMidfielderHome, _playground);
            Player playerHome8 = new Player("Milner", PlayerType.RightMidfielderHome, _playground);
            Player playerHome9 = new Player("Coutinho", PlayerType.LeftMidfielderHome, _playground);
            Player playerHome10 = new Player("Lallana", PlayerType.AttackMidfielderHome, _playground);
            Player playerHome11 = new Player("Benteke", PlayerType.CentralForwardHome, _playground);
            List<Player> homeTeamPlayers = new List<Player>();
            homeTeamPlayers.Add(playerHome1); homeTeamPlayers.Add(playerHome2); homeTeamPlayers.Add(playerHome3); homeTeamPlayers.Add(playerHome4);
            homeTeamPlayers.Add(playerHome5); homeTeamPlayers.Add(playerHome6); homeTeamPlayers.Add(playerHome7); homeTeamPlayers.Add(playerHome8);
            homeTeamPlayers.Add(playerHome9); homeTeamPlayers.Add(playerHome10); homeTeamPlayers.Add(playerHome11);
            Player playerAway1 = new Player("De Gea", PlayerType.GoalkeeperAway, _playground);
            Player playerAway2 = new Player("Blind", PlayerType.LeftDefenderAway, _playground);
            Player playerAway3 = new Player("Darmian", PlayerType.RightDefenderAway, _playground);
            Player playerAway4 = new Player("Rojo", PlayerType.CentralDefenderAwayLeft, _playground);
            Player playerAway5 = new Player("Smalling", PlayerType.CentralDefenderAwayRight, _playground);
            Player playerAway6 = new Player("Carrick", PlayerType.DefensiveMidfielderAway, _playground);
            Player playerAway7 = new Player("Herrera", PlayerType.CentralMidfielderAway, _playground);
            Player playerAway8 = new Player("Depay", PlayerType.LeftMidfielderAway, _playground);
            Player playerAway9 = new Player("Valencia", PlayerType.RightMidfielderAway, _playground);
            Player playerAway10 = new Player("Mata", PlayerType.AttackMidfielderAway, _playground);
            Player playerAway11 = new Player("Rooney", PlayerType.CentralForwardAway, _playground);
            List<Player> awayTeamPlayers = new List<Player>();
            awayTeamPlayers.Add(playerAway1); awayTeamPlayers.Add(playerAway2); awayTeamPlayers.Add(playerAway3); awayTeamPlayers.Add(playerAway4);
            awayTeamPlayers.Add(playerAway5); awayTeamPlayers.Add(playerAway6); awayTeamPlayers.Add(playerAway7); awayTeamPlayers.Add(playerAway8);
            awayTeamPlayers.Add(playerAway9); awayTeamPlayers.Add(playerAway10); awayTeamPlayers.Add(playerAway11);

            Team homeTeam = new Team(TeamScheme.Home451, new BalancedStrategy(), homeTeamPlayers);
            Team awayTeam = new Team(TeamScheme.Away451, new BalancedStrategy(), awayTeamPlayers);
            Ball ball = new Ball();
            ball.AttachObserver(homeTeam);
            ball.AttachObserver(awayTeam);
            Game game = new Game(homeTeam, awayTeam, ball, _playground);
            return game;
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
