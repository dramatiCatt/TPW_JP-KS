using NUnit.Framework;
using Logic;
using System.Numerics;

namespace LogicTest
{
    public class ManagerTests
    {

        [Test]
        public void CreateTest()
        {
            Manager manager = new Manager();
            int numBalls = manager.Balls.Count;
            Assert.True(numBalls == 0);
            manager.create(0);
            Assert.True(numBalls == manager.Balls.Count);
            manager.create(5);
            Assert.False(numBalls == manager.Balls.Count);
            Assert.True(5 == manager.Balls.Count);
            manager.create(1);
            Assert.True(6 == manager.Balls.Count);
        }

        [Test]
        public void StopTest()
        {
            Manager manager = new Manager();
            manager.create(2);
            Assert.True(2 == manager.Balls.Count);
            manager.stop();
            Assert.True(0 == manager.Balls.Count);
            manager.create(3);
            manager.create(4);
            Assert.True(7 == manager.Balls.Count);
            manager.stop();
            Assert.True(0 == manager.Balls.Count);
        }

        [Test]
        public void MovingTest()
        {
            Manager manager = new Manager();
            manager.create(2);
            float possitionX = manager.Balls[0].currentVector.X;
            float possitionY = manager.Balls[0].currentVector.Y;
            float possition1X = manager.Balls[1].currentVector.X;
            float possition1Y = manager.Balls[1].currentVector.Y;
            System.Threading.Thread.Sleep(1000);
            Assert.True(possitionX == manager.Balls[0].currentVector.X);
            Assert.True(possitionY == manager.Balls[0].currentVector.Y);
            Assert.True(possition1X == manager.Balls[1].currentVector.X);
            Assert.True(possition1Y == manager.Balls[1].currentVector.Y);
            manager.moving();
            System.Threading.Thread.Sleep(1000);
            Assert.True(possitionX != manager.Balls[0].currentVector.X);
            Assert.True(possitionY != manager.Balls[0].currentVector.Y);
            Assert.True(possition1X != manager.Balls[1].currentVector.X);
            Assert.True(possition1Y != manager.Balls[1].currentVector.Y);

        }
    }
}