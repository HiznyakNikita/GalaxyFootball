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
    public class Playground
    {
        private  List<PlaygroundZone> _zones;

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

        public IReadOnlyCollection<PlaygroundZone> Zones
        {
            get 
            {
                return _zones.AsReadOnly();
            }
        }

        #endregion

        private void InitializeZones()
        {
            try
            {
                using (Stream stream = File.Open("playgroundZones.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    _zones = (List<PlaygroundZone>)bin.Deserialize(stream);
                }
            }
            catch (IOException)
            {
            }
        }
    }
}
