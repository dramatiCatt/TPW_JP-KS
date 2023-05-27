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
        //public List<LogicBall> ballOperators;
        public abstract void create(int num);
        public abstract void stop();
        public abstract int getNum();
        public abstract float getX(int number);
        public abstract float getY(int number);
        public abstract event EventHandler LogicApiEvent;
        public static LogicAbstractApi CreateApi(DataAbstractApi data = default)
        {
            return new LogicApi(data ?? DataAbstractApi.CreateApi());
        }//u nicg logicapi 13
        public static int _width = 800;
        public static int _height = 400;
        public static int _radius = 15;
        private class LogicApi : LogicAbstractApi
        {
            private readonly DataAbstractApi data;//nie readonly

            public LogicApi(DataAbstractApi dataAbstractApi)
            {
                data = dataAbstractApi;
                data.BallEvent += Ball_PositionChanged;
            }
            public override event EventHandler LogicApiEvent;
            public override void create(int num)
            {
                data.create(num);  
            }
            private int _width;
            private int _height;
            private int _radius;
            object _lock = new object();
            public int Width { get => _width; }
            public int Height { get => _height; }
            public int Radius { get => _radius; }   
            public override void stop() => data.stop();
            public override int getNum()
            {
                return data.getNum();
            }
            public override float getX(int number)
            {
                return data.getX(number);
            }
            public override float getY(int number)
            {
                return data.getY(number);
            }

            private void Ball_PositionChanged(object sender, EventArgs e)
            {
                IBall ball = (IBall)sender;
                if (ball != null)
                {
                    Collision(_width, _height, 15, ball);
                    LogicApiEvent?.Invoke(this, EventArgs.Empty);
                }
            }

            public async void Collision(int width, int height, int radius, IBall ball)
            {
                Vector2 speed = new Vector2(ball.Velocity.X, ball.Velocity.Y);
                lock (_lock)
                {
                    for (int i = 0; i < data.getNum(); i++)
                    {
                        IBall testBall = data.GetBall(i);
                        if(testBall != ball)
                        {
                            float distance = Vector2.Distance(ball.CurrentVector, testBall.CurrentVector);
                            if (distance <= 30)
                            {
                                if (Vector2.Distance(ball.CurrentVector, testBall.CurrentVector)
                                - Vector2.Distance(ball.CurrentVector + ball.Velocity, testBall.CurrentVector + testBall.Velocity) > 0)
                                {
                                    BallCrash(ball, testBall);
                                }
                            }
                        }
                    }
                }
                
                if (ball.Velocity.X + ball.CurrentVector.X > width - radius)
                {
                    speed.X = ball.Velocity.X * (-1);
                }
                else if (ball.Velocity.X + ball.CurrentVector.X < radius)
                {
                    speed.X = ball.Velocity.X * (-1);
                }
                else if (ball.Velocity.Y + ball.CurrentVector.Y > height - radius)
                {
                    speed.Y = ball.Velocity.Y * (-1);
                }
                else if (ball.Velocity.Y + ball.CurrentVector.Y < radius)
                {
                    speed.Y = ball.Velocity.Y * (-1);
                }
            }
            public void checkMovement(object sender, PropertyChangedEventArgs e)
            {
                IBall b = (IBall)sender;
                b.CanMove = false;
                if (e.PropertyName == "CurrentVector")
                {
                    Collision(Width, Height, 15, b);
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
}
