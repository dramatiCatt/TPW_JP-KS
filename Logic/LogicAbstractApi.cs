﻿using Data;
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
        public abstract void create(int numer);
        public abstract void stop();
        public abstract ObservableCollection<Ball> Balls { get; }
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
        public override void create(int number)
        {
            ballOperators = new List<LogicBall>();
            data.create(number);
            foreach (Ball ball in data.GetBall())
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
        public override ObservableCollection<Ball> Balls => data.GetBall();
        public override int Width => data.Width;
        public override int Height => data.Height;

        public async void Collision(int width, int height, int radius, Ball ball)
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
            Ball b = (Ball)sender;
            if (e.PropertyName == "VectorCurrent")
            {
                Collision(Width, Height, b.Radius, b);
                b.CanMove = true;
            }
        }
        public void BallCrash(Ball b1, Ball b2)
        {
            Vector2 newVelocity1 = (b1.Velocity * (b1.weight - b2.weight) + b2.Velocity * 2 * b2.weight) / (b1.weight + b2.weight);
            Vector2 newVelocity2 = (b2.Velocity * (b2.weight - b1.weight) + b1.Velocity * 2 * b1.weight) / (b1.weight + b2.weight);
            if (newVelocity1.X > 5) newVelocity1.X = 5;
            if (newVelocity1.Y > 5) newVelocity1.Y = 5;
            if (newVelocity1.Y < -5) newVelocity1.Y = -5;
            if (newVelocity1.X < -5) newVelocity1.X = -5;
            b1.Velocity = newVelocity1;
            b2.Velocity = newVelocity2;
        }
    }
}