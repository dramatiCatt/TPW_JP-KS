using System;
using System.Numerics;
using System.Text;

namespace Data
{
        public class Creator
        {
            private Random _creator = new Random();
            public static int width = 800;
            public static int height = 400;
            private int _x;
            private int _y;
            private int _radius = 15;

            public Creator() { }

            private Ball CreateBall()
            {
                float X = _creator.Next(2 + _radius, width - _radius - 2);
                float Y = _creator.Next(2 + _radius, height - _radius - 2);
                float weight = (float)_creator.NextDouble() * 2;
                float velX = (float)_creator.NextDouble() * (3 + 3) - 3;
                float velY = (float)_creator.NextDouble() * (3 + 3) - 3;
                if (velX > -1 && velX < 0)
                {
                    velX = -1;
                }
                if (velX > 0 && velX < 1)
                {
                    velX = 1;
                }
                if (velY > -1 && velY < 0)
                {
                    velY = -1;
                }
                if (velY > 0 && velY < 1)
                {
                    velY = 1;
                }
                Vector2 velocity = new Vector2(velX, velY);
                return new Ball(X, Y, Radius, weight, velocity);
            }

            public int X
            {
                get => _x;
                set => _x = value;
            }

            public int Y
            {
                get => _y;
                set => _y = value;
            }

            public int Radius
            {
                get => _radius;
                set => _radius = value;
            }
        }
}
