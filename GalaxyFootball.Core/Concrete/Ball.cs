using GalaxyFootball.Core.Abstract;
using GalaxyFootball.Core.Concrete.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core
{
    public class Ball : ISubject
    {
        private readonly List<IObserver> _observers;
        private Point _position;

        public Ball()
        {

        }

        #region Properties

        public Point Position 
        { 
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                Notify();
            }
        }

        #endregion


        public void AttachObserver(IObserver observer)
        {
            if(observer != null)
            {
                _observers.Add(observer);
            }
        }

        public void DetachObserver(IObserver observer)
        {
            if(observer != null)
            {
                _observers.Remove(observer);
            }
        }

        public void Notify()
        {
            foreach(var o in _observers)
            {
                o.Update();
            }
        }
    }
}
