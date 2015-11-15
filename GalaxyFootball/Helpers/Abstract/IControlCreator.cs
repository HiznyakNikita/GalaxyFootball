using GalaxyFootball.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Helpers
{
    public interface IControlCreator
    {
        IControl CreateControl(IModel model);
    }
}
