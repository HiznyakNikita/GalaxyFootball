using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete.Helper.EventArgsHelpers
{
    public class GoalScoredEventArgs: EventArgs
    {
        public bool IsHomeScored { get; set; }
        public Player WhoScored { get; set; }

        public GoalScoredEventArgs(bool isHomeScored, Player whoScored)
        {
            IsHomeScored = isHomeScored;
            WhoScored = whoScored; 
        }
    }
}
