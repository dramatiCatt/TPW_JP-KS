using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;

namespace Data
{
    public class Ball : INotifyPropertyChanged {
        private Vector2 _currentVector;
        private Vector2 _velocity;
        private float _weight;
        private int _radius;
        private bool _canMove = true;

        public Ball()
        {
        }

        public Ball(Vector2 vector)
        {
            _currentVector = vector;
            _radius = 15;
        }

        public Ball(float x, float y, int radius, float weight, Vector2 velocity)
        {
            _currentVector.X = x;
            _currentVector.Y = y;
            _radius = radius;
            _weight = weight;
            _velocity = velocity;
        }

        public void UpdatePosition()
        {
            if (_canMove)
            {
                _currentVector += _velocity;
                _canMove = false;
                RaisePropertyChanged("CurrentVector");
            }
        }

        public Vector2 CurrentVector
        {
            get => _currentVector;
            set => _currentVector = value;
        }
        public Vector2 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }
        public float Weight
        {
            get => _weight;
            set
            {
                _weight = value;
            }
        }
        public int Radius
        {
            get => _radius;
        }

        public bool CanMove
        {
            get => _canMove;
            set { _canMove = value; }
        }

        public float X
        {
            get => _currentVector.X;
            set
            {
                _currentVector.X = value;
                RaisePropertyChanged("X");
            }
        }

        public float Y
        {
            get => _currentVector.Y;
            set
            {
                _currentVector.Y = value;
                RaisePropertyChanged("Y");
            }
        }
        public float velX
        {
            get => _velocity.X;
            set
            {
                if (value > 5)
                    value = 5;
                else if (value < -5)
                    value = -5;
                _velocity.X = value;
            }
        }

        public float velY
        {
            get => _velocity.Y;
            set
            {
                if (value > 5)
                    value = 5;
                else if (value < -5)
                    value = -5;
               _velocity.Y = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
