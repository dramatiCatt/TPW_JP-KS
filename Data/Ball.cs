using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace Data
{
    public interface IBall{
        Vector2 CurrentVector { get; }
        int MoveTime { get; }
        const int Radius = 15;
        float Weight { get; }
        Vector2 Velocity { get; set; }
        bool CanMove { get; set; }
    }

    internal class Ball : IBall{
        private int _moveTime;
        private Vector2 _currentVector;
        private Vector2 _velocity;
        private float _weight;
        private int _radius;
        private bool _canMove = true;


        public Ball(float x, float y, int radius, float weight, Vector2 velocity)
        {
            Random rnd = new Random();
            _currentVector.X = x;
            _currentVector.Y = y;
            Velocity = new Vector2(x, y) {
                X = (float)(rnd.NextDouble() * (3 + 3) - 3),
                Y = (float)(rnd.NextDouble() * (3 + 3) - 3)
            };
            //position oni tu mają
            _radius = radius;
            _weight = weight;
            _velocity = velocity;
            MoveTime = 1000 / 60;
            RunTask();
        }

        public int MoveTime {
            get => _moveTime;
            private set
            {
                _moveTime = value;
            }
        }

        public void Move()
        {
            CurrentVector += Velocity * MoveTime;
            OnPositionChanged();
        }

        private void RunTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    Move();
                    await Task.Delay(MoveTime);
                }
            });
        }

        internal event EventHandler PositionChanged;

        internal void OnPositionChanged()
        {
            PositionChanged?.Invoke(this, EventArgs.Empty);
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


    }

}
