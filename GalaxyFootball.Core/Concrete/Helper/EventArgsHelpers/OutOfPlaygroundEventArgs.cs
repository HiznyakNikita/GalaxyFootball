using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete.Helper.EventArgsHelpers
{
    public class OutOfPlaygroundEventArgs : EventArgs
    {
        // if ball is out of playground on home side
        public bool IsHomeSideOut { get; set; }

        public OutOfPlaygroundEventArgs(bool isHomeSideOut)
        {
            IsHomeSideOut = isHomeSideOut;
        }
    }
}
