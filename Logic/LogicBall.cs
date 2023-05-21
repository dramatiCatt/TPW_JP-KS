using Data;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class LogicBall : INotifyPropertyChanged
    {
        private readonly Ball _ball;
        public LogicBall(Ball ball)
        {
            _ball = ball;
            ball.PropertyChanged += ChangedBallData;
        }

        public void ChangedBallData(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("CurrentVector");
        }

        public Ball Ball { get => _ball; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public float X
        {
            get => _ball.X;
            set
            {
                _ball.X = value;
                RaisePropertyChanged(nameof(X));
            }
        }
        public float Y
        {
            get => _ball.Y;
            set
            {
                _ball.Y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }
        public int R
        {
            get => _ball.Radius;
        }

        public float VelX
        {
            get => _ball.velX;
            set
            {
                _ball.velX = value;
            }
        }

        public float VY
        {
            get => _ball.velY;
            set
            {
               _ball.velY = value;
            }
        }
    }
    }