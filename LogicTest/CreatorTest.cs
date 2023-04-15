using NUnit.Framework;
using Logic;
using System.Numerics;

namespace LogicTest
{
    public class CreatorTests
    {

        [Test]
        public void generateBallTest()
        {
            Creator creator = new Creator();
            Ball ball = creator.CreateBall();
            Assert.True(ball.currentVector.X >= 2 + creator.radius && ball.currentVector.X <= Manager.width - creator.radius - 1);
            Assert.True(ball.currentVector.Y >= 2 + creator.radius && ball.currentVector.Y <= Manager.height - creator.radius - 1);
            Assert.True(ball.Diameter / 2 == creator.radius);
            Assert.True(ball.speed == creator.speed);
        }
    }
}