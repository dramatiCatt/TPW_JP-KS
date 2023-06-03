using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Data
{
    public interface IBall{
        int ID { get; }
        [JsonConverter(typeof(Vector2Converter))]
        Vector2 CurrentVector { get; }
        const int Radius = 15;
        [JsonIgnore]
        float Weight { get; }
        [JsonConverter(typeof(Vector2Converter))]
        Vector2 Velocity { get; set; }
    }

    internal class Ball : IBall
    {
        private int _moveTime;
        private Vector2 _currentVector;
        private Vector2 _velocity;
        private float _weight;
        private int _radius;
        private bool _canMove = true;
        private Stopwatch _stopwatch;
        public int ID { get; }


        public Ball(float x, float y, int radius, float weight, Vector2 velocity, int id)
        {
            Random rnd = new Random();
            _stopwatch = new Stopwatch();
            ID = id;
            _currentVector.X = x;
            _currentVector.Y = y;
            Velocity = new Vector2(x, y) {
                X = (float)(rnd.NextDouble() * (3 + 3) - 3),
                Y = (float)(rnd.NextDouble() * (3 + 3) - 3)
            };
            //position tu ?
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
                int delay = 0;
                while (true)
                {
                    _stopwatch.Restart();
                    _stopwatch.Start();
                    Move();
                    _stopwatch.Stop();
                    if (MoveTime - _stopwatch.ElapsedMilliseconds < 0)
                    {
                        delay = 0;
                    }
                    else
                    {
                        delay = MoveTime - (int)_stopwatch.ElapsedMilliseconds;
                    }
                    await Task.Delay(MoveTime);
                }
            });
        }

        internal event EventHandler PositionChanged;
        internal class Vector2Converter : JsonConverter<Vector2>
        {
            public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                writer.WriteNumber("X", value.X);
                writer.WriteNumber("Y", value.Y);
                writer.WriteEndObject();
            }
        }

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
