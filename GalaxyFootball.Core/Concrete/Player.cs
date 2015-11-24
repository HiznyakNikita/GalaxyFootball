using GalaxyFootball.Core.Abstract;
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
        private PlayerState _state;
        private object _lock = new Object();
        private Point _startPosition;
        private Point _position;
        private bool _isSelected;
        private static Thread _actionThread;
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
            _state = PlayerState.Free;
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

        public PlayerState State 
        { 
            get
            {
                lock (_lock)
                {
                    return _state;
                }
            }
            set
            {
                lock (_lock)
                {
                    _state = value;
                }
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
                lock (_lock)
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
        }

        public Point StartPosition
        {
            get
            {
                return _startPosition;
            }
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
            catch(Exception e)
            {
                Console.Write("ll");
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
            _position = position;
            // do this for independent startPoint and point values
            _startPosition = new Point(position.X,position.Y);
        }

        #endregion

        #region Work with ball methods

        public void Shoot(Ball ball)
        {
            State = PlayerState.InAction;
            if (GameEngine.CurrentGame.Ball.State == BallState.Controlled)
            {
                _actionThread = new Thread(new ParameterizedThreadStart(ShootThreadMethod));
                _actionThread.Start(ball);
            }
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
                                            || (Type.ToString().Contains("Home") && ball.Position.X != 1030) && ball.State == BallState.Shootted)
                    {
                        ball.State = BallState.Shootted;
                        double yPos = ball.Position.Y > yFinishPos ? - ShootPowerPoints / 15 + ball.Position.Y : ShootPowerPoints / 15 + ball.Position.Y;
                        double xPos = Type.ToString().Contains("Home") ? ShootPowerPoints / 15 + ball.Position.X : -ShootPowerPoints / 15 + ball.Position.X;
                        ball.Position = new Point(xPos, yPos);
                    }
                }
                if (ShootAccuracyPoints > 50 && ShootAccuracyPoints < 70)
                {
                    double yFinishPos = r.Next(260, 430);
                    while ((ball.Position.X != 20 && Type.ToString().Contains("Away"))
                        || (Type.ToString().Contains("Home") && ball.Position.X != 1030) && ball.State == BallState.Shootted)
                    {
                        ball.State = BallState.Shootted;
                        double yPos = ball.Position.Y > yFinishPos ? - ShootPowerPoints / 15 + ball.Position.Y : ShootPowerPoints / 15 + ball.Position.Y;
                        double xPos = Type.ToString().Contains("Home") ? ShootPowerPoints / 15  + ball.Position.X: -ShootPowerPoints / 15 + ball.Position.X;
                        ball.Position = new Point(xPos, yPos);
                    }
                }
                if (ShootAccuracyPoints > 70 && ShootAccuracyPoints < 100)
                {
                    double yFinishPos = r.Next(233, 466);
                    while ((ball.Position.X != 20 && Type.ToString().Contains("Away"))
                        || (Type.ToString().Contains("Home") && ball.Position.X < 1015) && ball.State == BallState.Shootted)
                    {
                        ball.State = BallState.Shootted;
                        double yPos = ball.Position.Y > yFinishPos ? - ShootPowerPoints / 15 + ball.Position.Y: ShootPowerPoints / 15 + ball.Position.Y;
                        double xPos = Type.ToString().Contains("Home") ? ShootPowerPoints / 15  + ball.Position.X: - ShootPowerPoints / 15 + ball.Position.X;
                        ball.Position = new Point(xPos, yPos);
                    }
                }
            }
            ;
            State = PlayerState.Free;
        }

        public void Pick(Ball ball)
        {
            State = PlayerState.InAction;
            //if (_actionThread != null && _actionThread.ThreadState != ThreadState.Aborted)
            //    ;
            _actionThread = new Thread(new ParameterizedThreadStart(PickThreadMethod));
            _actionThread.Start(ball);
        }

        private void PickThreadMethod(object param)
        {
            Ball ball = param as Ball;
            if (ball.Owner != null && !ball.Owner.Equals(this) && ball.State != BallState.Picked && ball.IsCanPick(Position))
            {
                if (DefensePoints * r.NextDouble() > ball.Owner.DribblePoints * r.NextDouble())
                {
                    ball.State = BallState.Controlled;
                    ball.Owner.LoseBall();
                    ball.Owner = this;
                    ball.Pick();
                }
            }
            else if (ball.Owner == null && ball.IsCanPick(Position))
            {
                ball.State = BallState.Controlled;

                ball.Owner.LoseBall();
                ball.Owner = this;
                ball.Pick();
            }
            if (Type.ToString().Contains("Goalkeeper"))
            {
                _position = new Point(_startPosition.X, _startPosition.Y);
            }

            ;
            State = PlayerState.Free;
        }

        public void Pass(Ball ball, Player partner, bool isUp = false, bool isDown = false, bool isRight = false, bool isLeft = false)
        {
            State = PlayerState.InAction;
            //if(_actionThread != null && _actionThread.ThreadState != ThreadState.Aborted)
            //    ;
                _isUp = isUp;
                _isDown = isDown;
                _isRight = isRight;
                _isLeft = isLeft;
                if (GameEngine.CurrentGame.Ball.State == BallState.Controlled)
                {
                    _actionThread = new Thread(new ParameterizedThreadStart(PassThreadMethod));
                    _actionThread.Start(new Tuple<Ball, Player>(ball, partner));
                }
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
                        xPos = tuple.Item2.Position.X > tuple.Item1.Position.X ? 5 + tuple.Item1.Position.X 
                            : -5 + tuple.Item1.Position.X;
                    if (Math.Abs(tuple.Item2.Position.Y - yPos) >= 3)
                        yPos = tuple.Item2.Position.Y > tuple.Item1.Position.Y ? 5 + tuple.Item1.Position.Y : -5 + tuple.Item1.Position.Y;
                    tuple.Item1.Position = new Point(xPos, yPos);
                }
                if (Type.ToString().Contains("Home"))
                {
                    tuple.Item1.Owner = tuple.Item2;
                    tuple.Item2.IsSelected = true;
                    this.IsSelected = false;
                    tuple.Item1.State = BallState.Controlled;
                }
                else
                {
                    tuple.Item1.Owner = tuple.Item2;
                    tuple.Item1.State = BallState.Controlled;
                }
            }
           ;
           State = PlayerState.Free;
        }

        //public Point Control(
        //    Ball ball, 
        //    bool isVerticalUp = false, 
        //    bool isVerticalDown = false, 
        //    bool isHorizontalAttack = false, 
        //    bool isHorizontalDefend = false)
        //{
        //    _isUp = isVerticalUp;
        //    _isDown = isVerticalDown;
        //    _isLeft = isHorizontalDefend;
        //    _isRight = isHorizontalAttack;
        //    //_actionThread = new Thread(new ParameterizedThreadStart(ControlThreadMethod));
        //    //_actionThread.Start(ball);
        //   return ControlThreadMethod(ball);
        //}

        //private Point ControlThreadMethod(object param)
        //{
        //    State = PlayerState.InAction;

        //    Ball ball = param as Ball;
        //    ball.State = BallState.Controlled;
        //    if (ball.Owner != null && ball.Owner.Equals(this))
        //    {
        //        //control the ball while somebody doesn't picked it 
        //        //while (ball.Owner.Equals(this))
        //        //{
        //            if (_isRight)
        //            {
        //                Position = Type.ToString().Contains("Home") ? new Point(SpeedPoints / (Double)200 + Position.X, Position.Y)
        //                    : new Point(-SpeedPoints / (Double)200 + Position.X, Position.Y);
        //                ball.Position = new Point(Position.X, Position.Y);
        //                State = PlayerState.Free;

        //                return Position;
        //            }
        //            if (_isLeft)
        //            {
        //                Position = Type.ToString().Contains("Home") ? new Point(SpeedPoints / (Double)200 + Position.X, Position.Y)
        //                    : new Point(-SpeedPoints / (Double)200 + Position.X, Position.Y);
        //                ball.Position = new Point(Position.X, Position.Y);
        //                State = PlayerState.Free;

        //                return Position;
        //            }
        //            if (_isDown)
        //            {
        //                Position = new Point(Position.X, SpeedPoints / (Double)200 + Position.Y);
        //                ball.Position = new Point(Position.X, Position.Y);
        //                State = PlayerState.Free;

        //                return Position;
        //            }
        //            if (_isUp)
        //            {
        //                Position = new Point(Position.X, -SpeedPoints / (Double)200 + Position.Y);
        //                ball.Position = new Point(Position.X, Position.Y);
        //                State = PlayerState.Free;

        //                return Position;
        //            }
        //      //  }
        //    }
        //    State = PlayerState.Free;
        //    return Position;
        //}

        #endregion

        #region Collision methods

        public bool CollisionWithPlayer(Player player)
        {
            if ((Math.Abs(Position.X - player.Position.X) < 35 && Math.Abs(Position.Y - player.Position.Y) < 35) 
                && (Math.Abs(Position.X - player.Position.X) > 10 && Math.Abs(Position.Y - player.Position.Y) > 10))
                return true;
            else
                return false;
        }

        public bool CollisionWithPlayerClose(Player player)
        {
            if ((Math.Abs(Position.X - player.Position.X) < 10 && Math.Abs(Position.Y - player.Position.Y) < 10))
                return true;
            else
                return false;
        }

        #endregion

        #region Helper methods

        private void LoseBall()
        {
            if(GameEngine.CurrentGame.Ball.Owner != null && GameEngine.CurrentGame.Ball.Owner.Equals(this))
            {
                _position = new Point(_position.X-10, _position.Y-10);
            }
        }

        public Player FindPartnerForPass(
            bool isVerticalUp = false,
            bool isVerticalDown = false,
            bool isHorizontalRight = false,
            bool isHorizontalLeft = false)
        {
                if (Type.ToString().Contains("Home"))
                {
                    List<Player> results = new List<Player>();
                    foreach (var p in GameEngine.CurrentGame.TeamHome.Players)
                    {
                        if (isVerticalUp)
                        {
                            if (isHorizontalRight)
                            {
                                if (p.Position.X > Position.X && p.Position.Y < Position.Y && !CheckForIntersectionInZone(p))
                                    results.Add(p);
                            }
                            else if (isHorizontalLeft)
                            {
                                if (p.Position.X < Position.X && p.Position.Y < Position.Y && !CheckForIntersectionInZone(p))
                                    results.Add(p);
                            }
                            else if (p.Position.Y < Position.Y && !CheckForIntersectionInZone(p))
                                results.Add(p);
                        }
                        else if (isVerticalDown)
                        {
                            if (isHorizontalRight)
                            {
                                if (p.Position.X > Position.X && p.Position.Y > Position.Y && !CheckForIntersectionInZone(p))
                                    results.Add(p);
                            }
                            else if (isHorizontalLeft)
                            {
                                if (p.Position.X < Position.X && p.Position.Y > Position.Y && !CheckForIntersectionInZone(p))
                                    results.Add(p);
                            }
                            else if (p.Position.Y > Position.Y && !CheckForIntersectionInZone(p))
                                results.Add(p);
                        }
                        else if (isHorizontalRight)
                        {
                            if (p.Position.X > Position.X && !CheckForIntersectionInZone(p))
                                results.Add(p);
                        }
                        else if (isHorizontalLeft)
                        {
                            if (p.Position.X < Position.X && !CheckForIntersectionInZone(p))
                                results.Add(p);
                        }
                    }
                    //Select player with minium x and y 
                    Player resPlayer = results.Count > 0 ? results.Aggregate((curMin, x)
                        => (curMin == null || Math.Sqrt(Math.Abs(x.Position.X - Position.X) * Math.Abs(x.Position.X - Position.X)
                        + Math.Abs(x.Position.Y - Position.Y) * Math.Abs(x.Position.Y - Position.Y))
                        < Math.Sqrt(Math.Abs(curMin.Position.X - Position.X) * Math.Abs(curMin.Position.X - Position.X)
                        + Math.Abs(curMin.Position.Y - Position.Y) * Math.Abs(curMin.Position.Y - Position.Y)) ? x : curMin))
                        : GameEngine.CurrentGame.TeamHome.Players.Where(p => p.CheckForIntersectionInZone(this)).FirstOrDefault() != null
                        ? GameEngine.CurrentGame.TeamHome.Players.Where(p => p.CheckForIntersectionInZone(this)).FirstOrDefault()
                        : FindNearestPlayer();
                    return resPlayer;
                }
                else
                {
                    Player resPlayer = GameEngine.CurrentGame.TeamAway.Players.Aggregate((curMin, x)
                        => ((!x.Equals(this) && !curMin.Equals(this) && Math.Sqrt(Math.Abs(x.Position.X - Position.X) * Math.Abs(x.Position.X - Position.X)
                        + Math.Abs(x.Position.Y - Position.Y) * Math.Abs(x.Position.Y - Position.Y))
                        < Math.Sqrt(Math.Abs(curMin.Position.X - Position.X) * Math.Abs(curMin.Position.X - Position.X)
                        + Math.Abs(curMin.Position.Y - Position.Y) * Math.Abs(curMin.Position.Y - Position.Y))) ? x : curMin));
                    if (resPlayer == null || resPlayer.Equals(this))
                        resPlayer = FindNearestPlayer();
                    return resPlayer;
                }
        }

        public Player FindNearestPlayer()
        {
            Player res = this;
            double xDif = 400;
            double yDif = 400;
            if (Type.ToString().Contains("Home"))
            {
                foreach (var p in GameEngine.CurrentGame.TeamHome.Players)
                {
                    if (p != this && Math.Abs(p.Position.X - Position.X) < xDif && Math.Abs(p.Position.Y - Position.Y) < yDif)
                        res = p;

                }
            }
            else
            {
                foreach (var p in GameEngine.CurrentGame.TeamAway.Players)
                {
                    if (p != this && Math.Abs(p.Position.X - Position.X) < xDif && Math.Abs(p.Position.Y - Position.Y) < yDif)
                        res = p;

                }
            }
                return res;
        }

        public void AbortCurrentAction()
        {
            _position = new Point(_position.X, _position.Y);
        }

        public void Reset()
        {
            _position = new Point(_startPosition.X, _startPosition.Y);
            _isSelected = false;
            NotifyPropertyChanged("IsSelected");
        }

        public bool CheckForIntersectionInZone(Player player)
        {
            if (Math.Abs(Position.X - player.Position.X) < 5 && Math.Abs(Position.Y - player.Position.Y) < 5 && !player.Equals(this))
                return true;
            else
                return false;
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
