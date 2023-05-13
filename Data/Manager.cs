using Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class Manager 
    {
        public static int width = 800;
        public static int height = 400;
        private Creator creator = new Creator();
        private ObservableCollection<Ball> balls = new ObservableCollection<Ball>();
        private List<Task> tasks = new List<Task>();
        CancellationTokenSource tokenSource;
        CancellationToken token;
        private object locked = new object();

        public Manager()
        {
        }
        public int Width { get => width; }
        public int Height { get => height; }
        public Manager GetManager()
        {
            return this;
        }
        
        public void add(Ball ball)
        {
            balls.Add(ball);
        }

        public void remove(Ball ball)
        {
            balls.Remove(ball);
        }

        public void create(int num)
        {
            if (num > 0)
            {
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                for (int i = 0; i < num; i++)
                {
                    Ball ball = creator.CreateBall();
                    add(ball);
                }
            }
            moving();
        }

        public void stop()
        {
            if (tokenSource != null && !tokenSource.IsCancellationRequested)
            {
                tokenSource.Cancel();
                tasks.Clear();
                balls.Clear();
            }
        }

        public async void moving()
        {
            foreach (Ball ball in balls)
            {
                Task task = Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(8);
                        lock(locked)
                        {
                            ball.UpdatePosition();
                            while (ball.canMove == false) { }
                        }
                        ball.UpdatePosition();
                        try { token.ThrowIfCancellationRequested(); }
                        catch (System.OperationCanceledException) { break; } //OperationCanceledException if cancel
                    }
                });
                tasks.Add(task);
            }
        }

        public Creator Creator
        {
            get => creator;
        }

        public ObservableCollection<Ball> Balls
        {
            get => balls;
        }

        public List<Task> Tasks
        {
            get => tasks;
        }
    }
}