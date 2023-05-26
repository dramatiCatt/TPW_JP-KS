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
        public static int _width = 800;
        public static int _height = 400;
        private Creator _creator = new Creator();
        private ObservableCollection<Ball> _balls = new ObservableCollection<Ball>();
        private List<Task> _tasks = new List<Task>();
        CancellationTokenSource tokenSource;
        CancellationToken token;
        private object _locked = new object();

        public Manager()
        {
        }
        public int Width { get => _width; }
        public int Height { get => _height; }
        public Manager GetManager()
        {
            return this;
        }
        
        public void add(Ball ball)
        {
            _balls.Add(ball);
        }

        public void remove(Ball ball)
        {
            _balls.Remove(ball);
        }

        public void create(int num)
        {
            if (num > 0)
            {
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                for (int i = 0; i < num; i++)
                {
                    Ball ball = _creator.CreateBall();
                    add(ball);
                    ball.PositionChanged += Ball_PositionChanged;
                }
            }
            //moving();
        }

        private void Ball_PositionChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                BallEvent?.Invoke(sender, EventArgs.Empty);
            }
        }

        public void stop()
        {
            if (tokenSource != null && !tokenSource.IsCancellationRequested)
            {
                tokenSource.Cancel();
                _tasks.Clear();
                _balls.Clear();
            }
        }

        public Creator Creator
        {
            get => _creator;
        }

        public ObservableCollection<Ball> Balls
        {
            get => _balls;
        }

        public List<Task> Tasks
        {
            get => _tasks;
        }
    }
}