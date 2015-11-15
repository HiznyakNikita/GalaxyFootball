using GalaxyFootball.Core.Abstract;
using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    public class Team : IObserver
    {
        private static Dictionary<PlayerType, Point> _defaultPlayersPositions;
        private List<Player> _players;
        public Team(TeamScheme scheme, ITeamStrategy strategy, List<Player> players)
        {
            TeamScheme = scheme;
            TeamStrategy = strategy;
            _players = players;
            SetDefaultPlayersPositions();
            SetPlayersStartPositions();
        }

        public string Name 
        { 
            get; 
            private set; 
        }

        public IReadOnlyCollection<Player> Players
        {
            get
            {
                return _players.AsReadOnly();
            }
        }

        public ITeamStrategy TeamStrategy
        {
            get;
            private set;
        }

        public TeamScheme TeamScheme
        {
            get;
            private set;
        }

        public void SetDefaultPlayersPositions()
        {
            string teamScheme = CheckForTeamsScheme();
            try
            {
                using (Stream stream = File.Open(teamScheme, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    _defaultPlayersPositions= (Dictionary<PlayerType, Point>)bin.Deserialize(stream);
                }
            }
            catch (IOException)
            {
            }
        }

        private string CheckForTeamsScheme()
        {
            string schemeTeamName = "playersDefaultPositions";
            schemeTeamName += TeamScheme.ToString() + ".bin";
            return schemeTeamName;
        }

        private void SetPlayersStartPositions()
        {
            foreach(var p in _players)
            {
                p.SetStartPosition(_defaultPlayersPositions[p.Type]);
            }
        }

        public void Update()
        {
            foreach (var p in _players)
                p.Update(TeamStrategy);
        }

        

    }
}
