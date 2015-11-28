using GalaxyFootball.Helpers.Abstract;
using GalaxyFootball.UserControls;
using GalaxyFootball.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Helpers
{
    public class ControlCreator : IControlCreator
    {
        public IControl CreateControl(IModel model)
        {
            //if (model is PlayerMarkerVM)
            //{
            //    if ((model as PlayerMarkerVM).Player.Type.ToString().Contains("Home"))
            //        return new PlayerMarkerRed(model);
            //    else
            //        return new PlayerMarkerBlack(model);
            //}
           
            return null;
        }
    }
}
