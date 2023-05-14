using Data;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class LogicBall : INotifyPropertyChanged
    {
        private readonly Ball ball;
        public LogicBall(Ball ball)
        {
            ball = ball;
            ball.PropertyChanged += ChangedBallData;
        }

        public void ChangedBallData(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("VectorCurrent");
        }

        public Ball Ball { get => ball; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public float X
        {
            get => ball.X;
            set
            {
                ball.X = value;
                RaisePropertyChanged(nameof(X));
            }
        }
        public float Y
        {
            get => ball.Y;
            set
            {
                ball.Y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }
        public int R
        {
            get => ball.Radius;
        }

        public float VelX
        {
            get => ball.velX;
            set
            {
                ball.velX = value;
            }
        }

        public float VY
        {
            get => ball.velY;
            set
            {
                ball.velY = value;
            }
        }
    }
    }