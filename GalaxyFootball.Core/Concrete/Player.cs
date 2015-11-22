﻿using GalaxyFootball.Core.Abstract;
using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    public class Player: INotifyPropertyChanged
    {
        private Playground _playground;
        private Point _position;
        private bool _isSelected;
        private Random r = new Random();
        private bool _isUp = false;
        private bool _isDown = false;
        private bool _isRight = false;
        private bool _isLeft = false;

        public Player(
            string name, 
            PlayerType type, 
            Playground playground, 
            int number,
            int speedPoints,
            int defensePoints,
            int dribblePoints,
            int shootPowerPoints,
            int shootAccuracyPoints
            )
        {
            Name = name;
            Type = type;
            _playground = playground;
            Number = number;
            SpeedPoints = speedPoints;
            DefensePoints = defensePoints;
            DribblePoints = dribblePoints;
            ShootAccuracyPoints = shootAccuracyPoints;
            ShootPowerPoints = shootPowerPoints;
        }

        #region Properties

        public string Name
        {
            get;
            private set;
        }

        public bool IsSelected 
        { 
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        public int Number
        {
            get;
            private set;
        }

        public int SpeedPoints 
        { 
            get; 
            private set; 
        }

        public int DefensePoints 
        { 
            get; 
            private set; 
        }

        public int DribblePoints 
        { 
            get; 
            private set; 
        }

        public int ShootPowerPoints 
        { 
            get; 
            private set; 
        }

        public int ShootAccuracyPoints 
        { 
            get; 
            private set; 
        }

        public bool IsUserControlled 
        { 
            get; 
            private set; 
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
            }
        }

        public Point StartPosition
        {
            get;
            private set;
        }

        public PlayerType Type
        {
            get;
            private set;
        }

        public PlaygroundZone DefaultZone
        {
            get;
            set;
        }

        #endregion

        #region Update position methods

        public void Update(ITeamStrategy strategy)
        {
            try
            {
                ChangePosition(strategy);
            }
            catch(Exception)
            {

            }
        }

        public void ChangePosition(object strategy)
        {
            Position = ((ITeamStrategy)strategy).ChangePlayerPosition(this,_isUp,_isDown,_isRight,_isLeft);
        }

        public void SetMoveDirection(bool isUp = false, bool isDown = false, bool isRight = false, bool isLeft = false)
        {
            _isUp = isUp;
            _isDown = isDown;
            _isRight = isRight;
            _isLeft = isLeft;
            ChangePosition(GameEngine.CurrentGame.TeamHome.TeamStrategy);
        }

        public void SetStartPosition(Point position)
        {
            _position = StartPosition = position;
        }

        #endregion

        #region Work with ball methods

        public void Shoot(Ball ball)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(ShootThreadMethod));
            thread.Start(ball);
        }

        private void ShootThreadMethod(object param)
        {
            Ball ball = param as Ball;
            ball.State = BallState.Shootted;
            if (ball.Owner != null && ball.Owner.Equals(this))
            {
                if (ShootAccuracyPoints > 0 && ShootAccuracyPoints < 50)
                {
                    double yFinishPos = r.Next(300, 398);
                    while ((ball.Position.X != 20 && Type.ToString().Contains("Away"))
                                            || (Type.ToString().Contains("Home") && ball.Position.X != 1030))
                    {
                        double yPos = ball.Position.Y > yFinishPos ? - ShootPowerPoints / 10 + ball.Position.Y : ShootPowerPoints / 10 + ball.Position.Y;
                        double xPos = Type.ToString().Contains("Home") ? ShootPowerPoints / 10 + ball.Position.X : -ShootPowerPoints / 10 + ball.Position.X;
                        ball.Position = new Point(xPos, yPos);
                    }
                }
                if (ShootAccuracyPoints > 50 && ShootAccuracyPoints < 70)
                {
                    double yFinishPos = r.Next(260, 430);
                    while ((ball.Position.X != 20 && Type.ToString().Contains("Away"))
                        || (Type.ToString().Contains("Home") && ball.Position.X != 1030))
                    {
                        double yPos = ball.Position.Y > yFinishPos ? - ShootPowerPoints / 10 + ball.Position.Y : ShootPowerPoints / 10 + ball.Position.Y;
                        double xPos = Type.ToString().Contains("Home") ? ShootPowerPoints / 10  + ball.Position.X: -ShootPowerPoints / 10 + ball.Position.X;
                        ball.Position = new Point(xPos, yPos);
                    }
                }
                if (ShootAccuracyPoints > 70 && ShootAccuracyPoints < 100)
                {
                    double yFinishPos = r.Next(233, 466);
                    while ((ball.Position.X != 20 && Type.ToString().Contains("Away"))
                        || (Type.ToString().Contains("Home") && ball.Position.X != 1030))
                    {
                        double yPos = ball.Position.Y > yFinishPos ? - ShootPowerPoints / 10 + ball.Position.Y: ShootPowerPoints / 10 + ball.Position.Y;
                        double xPos = Type.ToString().Contains("Home") ? ShootPowerPoints / 10  + ball.Position.X: - ShootPowerPoints / 10 + ball.Position.X;
                        ball.Position = new Point(xPos, yPos);
                    }
                }
            }
        }

        public void Pick(Ball ball)
        {
            ball.State = BallState.Controlled;
            if (ball.Owner != null && !ball.Owner.Equals(this) && ball.IsCanPick(Position))
            {
                if (DefensePoints * r.NextDouble() > ball.Owner.DribblePoints * r.NextDouble())
                    ball.Owner = this;
            }
            else if (ball.Owner == null && ball.IsCanPick(Position))
                ball.Owner = this;
        }

        public void Pass(Ball ball, Player partner)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(PassThreadMethod));
            thread.Start(new Tuple<Ball, Player>(ball, partner));
        }

        private void PassThreadMethod(object param)
        {
            Tuple<Ball,Player> tuple = param as Tuple<Ball,Player>;
            //double proportion = ((Double)Math.Abs(GameEngine.CurrentGame.Ball.Position.X - tuple.Item2.Position.X) == 0 ? 1 : (Double)Math.Abs(GameEngine.CurrentGame.Ball.Position.X - tuple.Item2.Position.X))
            //    / ((Double)Math.Abs(GameEngine.CurrentGame.Ball.Position.Y - tuple.Item2.Position.Y) == 0 ? 1 : (Double)Math.Abs(GameEngine.CurrentGame.Ball.Position.Y - tuple.Item2.Position.Y));
            //double xIncrementer = proportion > 1 ? 5*proportion : 5;
            //double yIncrementer = proportion > 1 ? 5 : 5 * proportion;
            tuple.Item1.State = BallState.Passed;
            double xPos = 0;
            double yPos = 0;
            if (tuple.Item1.Owner != null && tuple.Item1.Owner.Equals(this))
            {
                while (!tuple.Item1.IsCanPick(tuple.Item2.Position))
                {
                    if (Math.Abs(tuple.Item2.Position.X - xPos) >= 3)
                        xPos = tuple.Item2.Position.X > tuple.Item1.Position.X ? 10 + tuple.Item1.Position.X: -10 + tuple.Item1.Position.X;
                    if (Math.Abs(tuple.Item2.Position.Y - yPos) >= 3)
                        yPos = tuple.Item2.Position.Y > tuple.Item1.Position.Y ? 10 + tuple.Item1.Position.Y : -10 + tuple.Item1.Position.Y;
                    tuple.Item1.Position = new Point(xPos, yPos);
                }
                tuple.Item1.Owner = tuple.Item2;
                tuple.Item2.IsSelected = true;
                this.IsSelected = false;
                tuple.Item1.State = BallState.Controlled;
            }
        }

        public void Control(
            Ball ball, 
            bool isVerticalUp = false, 
            bool isVerticalDown = false, 
            bool isHorizontalAttack = false, 
            bool isHorizontalDefend = false)
        {
            ball.State = BallState.Controlled;
            if (ball.Owner != null && ball.Owner.Equals(this))
            {
                //control the ball while somebody doesn't picked it 
                while (ball.Owner.Equals(this))
                {
                    if (isHorizontalAttack)
                        ball.Position = Type.ToString().Contains("Home") ? new Point(SpeedPoints / 10 + Position.X, Position.Y) 
                            : new Point(- SpeedPoints / 10 + Position.X, Position.Y);
                    if (isHorizontalDefend)
                        ball.Position = Type.ToString().Contains("Home") ? new Point(- SpeedPoints / 10 + Position.X, Position.Y) 
                            : new Point(SpeedPoints / 10 + Position.X, Position.Y);
                    if (isVerticalDown)
                        ball.Position = new Point(Position.X, SpeedPoints / 10 + Position.Y);
                    if (isVerticalUp)
                    {
                        ball.Position = new Point(Position.X, - SpeedPoints / 10 + Position.Y);
                    }
                }
            }
        }

        #endregion

        #region Collision methods

        public bool CollisionWithPlayer(Player player)
        {
            if (Math.Abs(Position.X - player.Position.X) < 15 && Math.Abs(Position.Y - player.Position.Y) < 15)
                return true;
            else
                return false;
        }

        #endregion

        #region Helper methods

        public Player FindPartnerForPass()
        {
            if(Type.ToString().Contains("Home"))
            {
                foreach(var p in GameEngine.CurrentGame.TeamHome.Players)
                {
                    if (p.Position.X > Position.X)
                        return p;
                }
                return GameEngine.CurrentGame.TeamHome.Players.FirstOrDefault();
            }
            else
            {
                foreach (var p in GameEngine.CurrentGame.TeamAway.Players)
                {
                    if (p.Position.X < Position.X)
                        return p;
                }
                return GameEngine.CurrentGame.TeamAway.Players.FirstOrDefault();
            }
        }

        public Player FindNearestPlayer()
        {
            Player res = this;
            double xDif = Position.X+100;
            double yDif = Position.Y+100;
            foreach(var p in GameEngine.CurrentGame.TeamHome.Players)
            {
                if (p != this && Math.Abs(p.Position.X - Position.X) < xDif && Math.Abs(p.Position.Y - Position.Y) < yDif)
                    res = p;
            }
            return res;
        }

        #endregion

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
