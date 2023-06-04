using System;
using System.Collections.Generic;
using System.Numerics;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
        public abstract void create(int num);
        public abstract void stop();
        public abstract int getNum();
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract float getX(int number);
        public abstract float getY(int number);
        public abstract IBall GetBall(int num);

        public abstract event EventHandler BallEvent;

    }

    public class DataApi : DataAbstractApi
    {
        private Logger _logger;
        private List<IBall> Balls { get; }
        public override int Width { get; }
        public override int Height { get; }
        public DataApi() {
            Balls = new List<IBall>();
            Width = 400;
            Height = 800;
            _logger = new Logger();
        }
        
        public override event EventHandler BallEvent;

        public override void stop()
        {
            Balls.Clear();
        }
        public override void create(int num)
        {
            if (num > 0)
            {
                Random rnd = new Random();
                int counter = Balls.Count;
                for (int i = 0; i < num; i++)
                {
                    float x, y;
                    x = (float)(rnd.NextDouble() * (3 + 3) - 3);
                    y = (float)(rnd.NextDouble() * (3 + 3) - 3);
                    Vector2 vel = new Vector2(x, y);
                    Ball ball = new Ball((rnd.Next(100, 300)), rnd.Next(100, 300), 15, rnd.Next(1, 5), vel, i + counter);
                    Balls.Add(ball);
                    ball.PositionChanged += Ball_PositionChanged;
                }
            }
        }

        private void Ball_PositionChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                BallEvent?.Invoke(sender, EventArgs.Empty);
            }
        }
        public override int getNum()
        {
            return Balls.Count;
        }
        public override float getX(int number)
        {
            return Balls[number].CurrentVector.X;
        }
        public override float getY(int number)
        {
            return Balls[number].CurrentVector.Y;
        }
        public override IBall GetBall(int number)
        {
            return Balls[number];
        }
    }

}
