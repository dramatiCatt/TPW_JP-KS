using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class Ball : INotifyPropertyChanged
    {
        public Vector2 currentVector;
        public Vector2 whereVector;
        public float speed;
        public int radius;


        public Ball(Vector2 vector)
        {
            currentVector = vector;
            radius = 10;
        }

        public Ball(int x, int y, int radius, float speed)
        {
            currentVector.X = x;
            currentVector.Y = y;
            this.radius = radius;
            this.speed = speed;
            System.Random random = new System.Random();
            int edge = random.Next(1, 5);// 4 - down, 1 - right, 3 - up, 2 - left
            if (edge == 1)
            {
                whereVector.X = Manager.width - this.radius;
                whereVector.Y = random.Next(this.radius, Manager.height - this.radius);
            }
            else if (edge == 2)
            {
                whereVector.X = this.radius;
                whereVector.Y = random.Next(this.radius, Manager.height - this.radius);
            }
            else if (edge == 3)
            {
                whereVector.Y = Manager.height - this.radius;
                whereVector.X = random.Next(this.radius, Manager.width - this.radius);
            }
            else
            {
                whereVector.Y = this.radius;
                whereVector.X = random.Next(this.radius, Manager.width - this.radius);
            }
        }

        public void createNewVectorDestination()
        {
            System.Random random = new System.Random();
            int edge; // 4 - down, 1 - right, 3 - up, 2 - left
            if (currentVector.X == radius)
                edge = 2;
            
            else if (currentVector.X == Manager.width - radius)
                edge = 1;
 
            else if (currentVector.Y == radius)
                edge = 4;
            
            else
                edge = 3;
            
            int destinationWall;
            int wall = random.Next(1, 4);
            if (edge > wall)
                destinationWall = wall;
            
            else if (wall == 3)
                destinationWall = 4;
            
            else
                destinationWall = edge + wall;
            float XCoordinate;
            float YCoordinate;
            if (destinationWall < 3)
            {
                YCoordinate = random.Next(radius, Manager.height - radius);
                XCoordinate = (destinationWall % 2) * (Manager.width - 2 * radius) + radius;
            }
            else
            {
                XCoordinate = random.Next(radius, Manager.width - radius);
                YCoordinate = ((destinationWall - 2) % 2) * (Manager.height - 2 * radius) + radius;
            }
            whereVector.X = XCoordinate;
            whereVector.Y = YCoordinate;
            double changesCounter = System.Math.Sqrt((System.Math.Pow(currentVector.X - whereVector.X, 2) + System.Math.Pow(currentVector.Y - whereVector.Y, 2))) / speed;

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdatePosition()
        {
            if (currentVector == whereVector)
            {
                createNewVectorDestination();
            }
            double changesCounter = System.Math.Sqrt((System.Math.Pow(currentVector.X - whereVector.X, 2) + System.Math.Pow(currentVector.Y - whereVector.Y, 2))) / speed;
            if (changesCounter < 1)
            {
                currentVector = whereVector;
            }
            else
            {
                currentVector.X += (float)((whereVector.X - currentVector.X) / changesCounter);
                currentVector.Y += (float)((whereVector.Y - currentVector.Y) / changesCounter);
            }
            RaisePropertyChanged(nameof(X));
            RaisePropertyChanged(nameof(Y));
        }

        public Vector2 VectorCurrent
        {
            get => currentVector;
            set => currentVector = value;
        }

        public Vector2 WhereVector
        {
            get => whereVector;
            set => whereVector = value;
        }

        public int Diameter
        {
            get => 2 * radius;
        }

        public float X
        {
            get => currentVector.X;
        }

        public float Y
        {
            get => currentVector.Y;
        }

        public float Speed
        {
            get => speed;
        }
    }
}