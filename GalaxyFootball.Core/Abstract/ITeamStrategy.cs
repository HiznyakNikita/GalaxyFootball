using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Abstract
{
    public interface ITeamStrategy
    {
        Point ChangePlayerPosition(Player player);
    }
}
