using NUnit.Framework;
using Logic;
using System.Numerics;

namespace LogicTest
{
    public class BallTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BallConstructor()
        {
            LogicBall ball = new LogicBall(2, 1, 3, 7);
            Assert.True(ball.currentVector.X == 2);
            Assert.True(ball.currentVector.Y == 1);

            float whereX = ball.whereVector.X;
            float whereY = ball.whereVector.Y;

            Assert.True(whereX == 3 || whereX == Manager.width - 3 || whereY == 3 || whereY == Manager.height - 3);
            Assert.False(whereX == Manager.height - 2);
            Assert.True(ball.Diameter == 6);
            Assert.True(ball.Speed == 7);
        }

        [Test]
        public void GenerateDestinationTest()
        {
            LogicBall ball = new LogicBall(2, 33, 3, 7);
            ball.createNewVectorDestination();
            Assert.True(ball.whereVector.X > 2 && ball.whereVector.Y >= 2 && ball.whereVector.Y <= Manager.height - 2
                && ball.whereVector.X <= Manager.width - 2);
            
            LogicBall ball2 = new LogicBall(33, 1, 5, 2);
            ball2.createNewVectorDestination();
            Assert.True(ball2.whereVector.X >= 1 && ball2.whereVector.Y > 1 && ball2.whereVector.Y <= Manager.height - 1
                && ball2.whereVector.X <= Manager.width - 1);
            
            LogicBall ball3 = new LogicBall(Manager.width - 5, 33, 5, 2);
            ball3.createNewVectorDestination();
            Assert.True(ball3.whereVector.X >= 5 && ball3.whereVector.Y >= 5 && ball3.whereVector.Y <= Manager.height - 5
                && ball3.whereVector.X < Manager.width - 5);
            
            LogicBall ball4 = new LogicBall(33, Manager.height - 5, 5, 2);
            ball4.createNewVectorDestination();
            Assert.True(ball4.whereVector.X >= 5 && ball4.whereVector.Y >= 5 && ball4.whereVector.Y < Manager.height - 5
                && ball4.whereVector.X <= Manager.width - 5);

        }

        [Test]
        public void UpdatePositionTest()
        {
            LogicBall ball = new LogicBall(2, 10, 6, 2);
            Vector2 first = ball.currentVector;
            Vector2 second = new Vector2(20, 10);
            ball.whereVector = second;
            ball.UpdatePosition();
            Assert.AreEqual(Vector2.Distance(ball.currentVector, first), System.Math.Ceiling(2f));
            Assert.AreEqual(4, System.Math.Ceiling(ball.currentVector.X));
            Assert.AreEqual(10, ball.currentVector.Y);
        }
    }
}