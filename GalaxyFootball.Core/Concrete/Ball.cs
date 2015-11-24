using GalaxyFootball.Core.Abstract;
using GalaxyFootball.Core.Concrete;
using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using GalaxyFootball.Core.Concrete.Helper.EventArgsHelpers;
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
        private bool _isOutOfPlayground = false;

        public Ball()
        {
            //set start position - playground center
            _position = new Point(525, 349);
            State = BallState.Controlled;
        }

        public event EventHandler PositionChanged;
        public event EventHandler GoalScored;
        public event EventHandler OutOfPlayground;

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
                        {
                            if (value.Y < 396 && value.Y > 304)
                            {
                                State = BallState.Outed;
                                OnGoalScored();
                                _isOutOfPlayground = true;
                            }
                            else
                            {
                                State = BallState.Outed;
                                OnOutOfPlayground();
                                _isOutOfPlayground = true;
                            }
                        }
                        else
                        {
                            _isOutOfPlayground = false;
                            _position.X = value.X;
                        }
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
                        if (value.X > 1010)
                        {
                            if (value.Y < 396 && value.Y > 304)
                            {
                                State = BallState.Outed;
                                OnGoalScored();
                                _isOutOfPlayground = true;
                            }
                            else
                            {
                                State = BallState.Outed;
                                OnOutOfPlayground();
                                _isOutOfPlayground = true;
                            }
                        }
                        else
                        {
                            _isOutOfPlayground = false;
                            _position.X = value.X;
                        }
                    }
                    if (!_isOutOfPlayground)
                    {
                        _thread = new Thread(new ThreadStart(Notify));
                        try
                        {
                            if (_thread.ThreadState == ThreadState.Aborted || _thread.ThreadState == ThreadState.Unstarted)
                                _thread.Start();
                        }
                        catch (ThreadStateException)
                        {
                            int a = 0;
                        }
                        catch (ThreadStartException)
                        {
                            int a = 0;
                        }
                        OnPositionChanged();
                        NotifyPropertyChanged("Position");
                    }
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

        protected virtual void OnGoalScored()
        {
            if (GoalScored != null)
            {
                bool isHomeScored = false;
                if (Owner.Type.ToString().Contains("Home") && Owner.Type == PlayerType.GoalkeeperHome)
                    isHomeScored = false;
                else if (Owner.Type.ToString().Contains("Home") && Owner.Type != PlayerType.GoalkeeperHome)
                    isHomeScored = true;
                else
                    isHomeScored = false;
                GoalScored(this, new GoalScoredEventArgs(isHomeScored,Owner));
            }
        }

        protected virtual void OnOutOfPlayground()
        {
            if (OutOfPlayground != null)
            {
                bool isHomeSideOut = true;
                if (Owner.Type.ToString().Contains("Home") && Owner.Type == PlayerType.GoalkeeperHome)
                    isHomeSideOut = true;
                else if (Owner.Type.ToString().Contains("Home") && Owner.Type != PlayerType.GoalkeeperHome)
                    isHomeSideOut = false;
                else
                    isHomeSideOut = true;
                OutOfPlayground(this, new OutOfPlaygroundEventArgs(isHomeSideOut));
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
            Thread.Sleep(100);
            foreach(var o in _observers)
            {
                o.Update();
            }
            //if(_thread != null && _thread.ThreadState != ThreadState.Aborted)
            //    _thread.Abort();
        }

        public bool IsCanPick(Point position)
        {
            if ((Math.Abs(position.X - _position.X) < 9) && (Math.Abs(position.Y - _position.Y) < 9))
                return true;
            else
                return false;
        }

        public void Reset()
        {
            _position = new Point(510, 349);
            Owner = null;
            _thread.Abort();
        }

        public void Pick()
        {
            foreach (var p in GameEngine.CurrentGame.TeamHome.Players)
                p.AbortCurrentAction();
            foreach (var p in GameEngine.CurrentGame.TeamHome.Players)
                p.AbortCurrentAction();
            State = BallState.Controlled;
            _position = new Point(Owner.Position.X, Owner.Position.Y);
            _thread.Abort();

            Notify();
            OnPositionChanged();
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
