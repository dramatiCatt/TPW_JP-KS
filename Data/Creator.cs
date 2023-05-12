using System;
using System.Numerics;
using System.Text;

namespace Data
{
        public class Creator
        {
            private Random creator = new Random();
            public static int width = 800;
            public static int height = 400;
            private int x;
            private int y;
            private int radius = 15;

            public Creator() { }

            public Ball CreateBall()
            {
                float X = creator.Next(2 + radius, width - radius - 2);
                float Y = creator.Next(2 + radius, height - radius - 2);
                float mass = (float)creator.NextDouble() * 2;
                float velX = (float)creator.NextDouble() * (3 + 3) - 3;
                float velY = (float)creator.NextDouble() * (3 + 3) - 3;
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
                return new Ball(X, Y, Radius, mass, velocity);
            }

            public int X
            {
                get => x;
                set => x = value;
            }

            public int Y
            {
                get => y;
                set => y = value;
            }

            public int Radius
            {
                get => radius;
                set => radius = value;
            }
        }
}
