using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Threading;
using System.Diagnostics;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public List<LogicBall> ballOperators;
        public static LogicAbstractApi CreateApi(DataAbstractApi data = default)
        {
            return new LogicApi(data ?? DataAbstractApi.CreateApi());
        }
        public abstract void create(int num);
        public abstract void stop();
        public abstract ObservableCollection<IBall> Balls { get; }
        public abstract List<LogicBall> GetBall();
        public abstract int Height { get; }
        public abstract int Width { get; }
    }
    public class LogicApi : LogicAbstractApi
    {
        private readonly DataAbstractApi data;

        public LogicApi(DataAbstractApi dataAbstractApi)
        {
            data = dataAbstractApi;
        }
        public override void create(int num)
        {
            ballOperators = new List<LogicBall>();
            data.create(num);
            foreach (IBall ball in data.GetBall())
            {
                ballOperators.Add(new LogicBall(ball));
                ball.PropertyChanged += checkMovement;
            }
        }
        public override void stop() => data.stop();
        public override List<LogicBall> GetBall()
        {
            return ballOperators;
        }
        public override ObservableCollection<IBall> Balls => data.GetBall();
        public override int Width => data.Width;
        public override int Height => data.Height;

        public async void Collision(int width, int height, int radius, IBall ball)
        {
            foreach (LogicBall thisBall in ballOperators)
            {
                if (thisBall.Ball == ball)
                {
                    continue;
                }
                thisBall.Ball.CanMove = false;
                float distance = Vector2.Distance(ball.CurrentVector, thisBall.Ball.CurrentVector);
                if (distance <= (ball.Radius + thisBall.Ball.Radius))
                {
                    if (Vector2.Distance(ball.CurrentVector, thisBall.Ball.CurrentVector)
                    - Vector2.Distance(ball.CurrentVector + ball.Velocity, thisBall.Ball.CurrentVector + thisBall.Ball.Velocity) > 0)
                    {
                        BallCrash(ball, thisBall.Ball);
                    }
                }
                thisBall.Ball.CanMove = true;
            }
            if (ball.X + ball.velX > Width - radius)
            {
                ball.velX = ball.velX * (-1);
            }
            else if (ball.X + ball.velX < radius)
            {
                ball.velX = ball.velX * (-1);
            }
            else if (ball.Y + ball.velY > height - radius)
            {
                ball.velY = ball.velY * (-1);
            }
            else if (ball.Y + ball.velY < radius)
            {
                ball.velY = ball.velY * (-1);
            }
        }
        public void checkMovement(object sender, PropertyChangedEventArgs e)
        {
            IBall b = (IBall)sender;
            if (e.PropertyName == "CurrentVector")
            {
                Collision(Width, Height, b.Radius, b);
                b.CanMove = true;
            }
        }
        public void BallCrash(IBall b1, IBall b2)
        {
            Vector2 newVelocity1 = (b1.Velocity * (b1.Weight - b2.Weight) + b2.Velocity * 2 * b2.Weight) / (b1.Weight + b2.Weight);
            Vector2 newVelocity2 = (b2.Velocity * (b2.Weight - b1.Weight) + b1.Velocity * 2 * b1.Weight) / (b1.Weight + b2.Weight);
            if (newVelocity1.X > 5) newVelocity1.X = 5;
            if (newVelocity1.Y > 5) newVelocity1.Y = 5;
            if (newVelocity1.Y < -5) newVelocity1.Y = -5;
            if (newVelocity1.X < -5) newVelocity1.X = -5;
            b1.Velocity = newVelocity1;
            b2.Velocity = newVelocity2;
        }
    }
}
