using GalaxyFootball.Core.Abstract;
using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalaxyFootball.Core
{
    public class Ball : ISubject, INotifyPropertyChanged
    {
        private readonly List<IObserver> _observers = new List<IObserver>();
        private object _lock = new Object();
        private Thread _thread;
        private Point _position;

        public Ball()
        {
            //set start position - playground center
            _position = new Point(510, 349);
            State = BallState.Controlled;
        }

        public event EventHandler PositionChanged;

        #region Properties

        public BallState State
        {
            get;
            set;
        }

        public Player Owner 
        { 
            get; 
            set; 
        }

        public Point Position 
        { 
            get
            {
                return _position;
            }
            set
            {
                if (value.X < 20 || value.Y < 20)
                    {
                        if (value.X < 20)
                            _position.X = 20;
                        else
                            _position.X = value.X;
                        if (value.Y < 20)
                            _position.Y = 20;
                        else
                            _position.Y = value.Y;
                    }
                    else
                    {
                        if (value.Y > 680)
                            _position.Y = 680;
                        else
                            _position.Y = value.Y;
                        if (value.X > 1030)
                            _position.X = 1030;
                        else
                            _position.X = value.X;
                    }
                    _thread = new Thread(new ThreadStart(Notify));
                    _thread.Start();
                    OnPositionChanged();
                    NotifyPropertyChanged("Position");
                }
        }

        #endregion

        protected virtual void OnPositionChanged()
        {
            if (PositionChanged != null)
            {
                PositionChanged(this, EventArgs.Empty);
            }
        }

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

        public bool IsCanPick(Point position)
        {
            if ((Math.Abs(position.X - _position.X) < 3) && (Math.Abs(position.Y - _position.Y) < 3))
                return true;
            else
                return false;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string property)
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
