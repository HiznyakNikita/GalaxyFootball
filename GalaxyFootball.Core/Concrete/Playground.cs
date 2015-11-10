using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    public class Playground
    {
        private readonly List<PlaygroundZone> _zones;

        public Playground(string name)
        {
            Name = name;
            InitializeZones();
        }

        #region Properties

        public string Name
        {
            get;
            private set;
        }

        #endregion

        private void InitializeZones()
        {
            //TODO Init zones from xml settings
        }
    }
}
