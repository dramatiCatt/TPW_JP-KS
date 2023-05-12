using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Data
{
    public class Ball : INotifyPropertyChanged {
        public Vector2 currentVector;
        public Vector2 velocity;
        public float weight;
        public int radius;
        public bool canMove;

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Ball()
        {
        }

        public Ball(Vector2 vector)
        {
            currentVector = vector;
            radius = 15;
        }

        public Ball(float x, float y, int radius, float weight, Vector2 velocity)
        {
            currentVector.X = x;
            currentVector.Y = y;
            radius = radius;
            weight = weight;
            velocity = velocity;
        }

        public void UpdatePosition()
        {
            if (canMove)
            {
                currentVector += velocity;
                canMove = false;
                RaisePropertyChanged("CurrentVector");
            }
        }

        public Vector2 CurrentVector
        {
            get => currentVector;
            set => currentVector = value;
        }
        public Vector2 Velocity
        {
            get => velocity;
            set => velocity = value;
        }
        public float Weight
        {
            get => weight;
            set
            {
                weight = value;
            }
        }
        public int Radius
        {
            get => radius;
        }

        public bool CanMove
        {
            get => canMove;
            set { canMove = value; }
        }

        public float X
        {
            get => currentVector.X;
            set
            {
                currentVector.X = value;
                RaisePropertyChanged("X");
            }
        }

        public float Y
        {
            get => currentVector.Y;
            set
            {
                currentVector.Y = value;
                RaisePropertyChanged("Y");
            }
        }
        public float velX
        {
            get => velocity.X;
            set
            {
                if (value > 5)
                    value = 5;
                else if (value < -5)
                    value = -5;
                velocity.X = value;
            }
        }

        public float velY
        {
            get => velocity.Y;
            set
            {
                if (value > 5)
                    value = 5;
                else if (value < -5)
                    value = -5;
               velocity.Y = value;
            }
        }

    }

}
