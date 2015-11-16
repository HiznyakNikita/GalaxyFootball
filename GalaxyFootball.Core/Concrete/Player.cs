using GalaxyFootball.Core.Abstract;
using GalaxyFootball.Core.Concrete.Helper;
using GalaxyFootball.Core.Concrete.Helper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Concrete
{
    public class Player
    {
        private Playground _playground;
        private Thread _thread;
        private Point _position;
        private Random r = new Random();

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
            _thread = new Thread(new ParameterizedThreadStart(ChangePosition));
            ChangePosition(strategy);
        }

        private void ChangePosition(object strategy)
        {
            Position = ((ITeamStrategy)strategy).ChangePlayerPosition(this);
        }

        public void SetStartPosition(Point position)
        {
            _position = StartPosition = position;
        }

        #endregion

        #region Work with ball methods

        public void Shoot(Ball ball)
        {
            if (ball.Owner != null && ball.Owner.Equals(this))
            {
                if(ShootAccuracyPoints>0 && ShootAccuracyPoints < 50)
                {
                    double yFinishPos = r.Next(300,398);
                    while ((ball.Position.X != 20 && Type.ToString().Contains("Away"))
                                            || (Type.ToString().Contains("Home") && ball.Position.X != 1030))
                    {
                        double yPos = ball.Position.Y > yFinishPos ? ball.Position.Y - ShootPowerPoints / 10 : ball.Position.Y + ShootPowerPoints / 10;
                        double xPos = Type.ToString().Contains("Home") ? ball.Position.X + ShootPowerPoints / 10 : ball.Position.X - ShootPowerPoints / 10;
                        ball.Position = new Point(xPos,yPos);
                    }
                }
                if (ShootAccuracyPoints > 50 && ShootAccuracyPoints < 70)
                {
                    double yFinishPos = r.Next(260, 430);
                    while ((ball.Position.X != 20 && Type.ToString().Contains("Away")) 
                        || (Type.ToString().Contains("Home") && ball.Position.X != 1030))
                    {
                        double yPos = ball.Position.Y > yFinishPos ? ball.Position.Y - ShootPowerPoints / 10 : ball.Position.Y + ShootPowerPoints / 10;
                        double xPos = Type.ToString().Contains("Home") ? ball.Position.X + ShootPowerPoints / 10 : ball.Position.X - ShootPowerPoints / 10;
                        ball.Position = new Point(xPos, yPos);
                    }
                }
                if (ShootAccuracyPoints > 70 && ShootAccuracyPoints < 100)
                {
                    double yFinishPos = r.Next(233, 466);
                    while ((ball.Position.X != 20 && Type.ToString().Contains("Away")) 
                        || (Type.ToString().Contains("Home") && ball.Position.X != 1030))
                    {
                        double yPos = ball.Position.Y > yFinishPos ? ball.Position.Y - ShootPowerPoints / 10 : ball.Position.Y + ShootPowerPoints / 10;
                        double xPos = Type.ToString().Contains("Home") ? ball.Position.X + ShootPowerPoints / 10 : ball.Position.X - ShootPowerPoints / 10;
                        ball.Position = new Point(xPos, yPos);
                    }
                }
            }
        }

        public void Pick(Ball ball)
        {
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
            double xPos = 0;
            double yPos = 0;
            if(ball.Owner != null && ball.Owner.Equals(this))
            {
                while (!ball.IsCanPick(partner.Position))
                {
                    if(Math.Abs(partner.Position.X-xPos)>=3)
                        xPos = partner.Position.X > ball.Position.X ? ball.Position.X + 2 : ball.Position.X - 2;
                    if (Math.Abs(partner.Position.Y - yPos) >= 3)
                        yPos = partner.Position.Y > ball.Position.Y ? ball.Position.Y + 2 : ball.Position.Y - 2;
                    ball.Position = new Point(xPos,yPos);
                }
                ball.Owner = partner;
            }
        }

        public void Control(Ball ball)
        {
            if (ball.Owner != null && ball.Owner.Equals(this))
            {
                //control the ball while somebody doesn't picked it 
                while (ball.Owner.Equals(this))
                    ball.Position = new Point(Position.X + 2, Position.Y + 2);
            }
        }

        #endregion
    }
}
