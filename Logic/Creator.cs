using System;

namespace Logic
{
    public class Creator
    {
        private Random creator = new Random();
        private int x;
        private int y;
        public int radius = 10;
        public float speed = 1f;

        public Creator() { }

        public Ball CreateBall()
        {
            CreateXY();
            return new Ball(X, Y, Radius, Speed);
        }

        public void CreateXY()
        {
            this.X = creator.Next(2 + radius, Manager.width - radius - 2);
            this.Y = creator.Next(2 + radius, Manager.height - radius - 2);
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public int Radius
        {
            get => radius;
            set => radius = value;
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

    }
}
