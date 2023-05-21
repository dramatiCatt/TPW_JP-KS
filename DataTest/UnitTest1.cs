using NUnit.Framework;
using Data;
using System.Numerics;
using System.Collections.ObjectModel;
using System.Reflection.Emit;

namespace DataTest
{

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void dataApiTest()
        {
            DataAbstractApi data = new DataApi();
            Assert.True(data.Height == 400);
            Assert.True(data.Width == 800);
            data.create(2);
            ObservableCollection<Ball> balls = data.GetManager().Balls;
            Assert.True(balls.Count == 2);
            Assert.True(balls[0].X < data.Width);
            Assert.True(balls[0].Y < data.Height);
            Assert.True(balls[0].X > 0);
            Assert.True(balls[0].Y > 0);
            data.stop();
            Assert.True(balls.Count == 0);
        }

        [Test]
        public void generateBallTest()
        {
            Creator generator = new Creator();
            Ball ball = generator.CreateBall();
            Assert.True(ball.CurrentVector.X >= 2 + generator.Radius && ball.CurrentVector.X <= Manager._width - generator.Radius - 1);
            Assert.True(ball.CurrentVector.Y >= 2 + generator.Radius && ball.CurrentVector.Y <= Manager._height - generator.Radius - 1);
            Assert.True(ball.Weight <= 2 && ball.Weight > 0);
        }

        [Test]
        public void BallConstructor()
        {
            Vector2 velocity = new Vector2(1, 4);
            Ball ball = new Ball(2, 3, 5, 2, velocity);
            Assert.True(ball.CurrentVector.X == 2);
            Assert.True(ball.CurrentVector.Y == 3);
            Assert.True(ball.Radius == 5);
            Assert.True(ball.Weight == 2);
            Assert.True(ball.velX == 1);
            Assert.True(ball.velY == 4);
        }

        [Test]
        public void StorageCreateBallsTest()
        {
            Manager storage = new Manager();
            int numberBalls0 = storage.Balls.Count;
            Assert.True(numberBalls0 == 0);
            storage.create(0);
            Assert.True(numberBalls0 == storage.Balls.Count);
            storage.create(2);
            Assert.False(numberBalls0 == storage.Balls.Count);
            Assert.True(2 == storage.Balls.Count);
            storage.create(3);
            Assert.True(5 == storage.Balls.Count);
        }

        [Test]
        public void StorageStopBallsTest()
        {
            Manager storage = new Manager();
            storage.create(3);
            Assert.True(3 == storage.Balls.Count);
            storage.stop();
            Assert.True(0 == storage.Balls.Count);
            storage.create(2);
            storage.create(2);
            Assert.True(4 == storage.Balls.Count);
            storage.stop();
            Assert.True(0 == storage.Balls.Count);
        }


    }
}