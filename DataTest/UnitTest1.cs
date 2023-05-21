using NUnit.Framework;
using System.ComponentModel.Design;
using Data;
using System.ComponentModel;

namespace DataTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void createBallTest()
    {
        Creator creator = new Creator();
        Ball ball = creator.CreateBall();
        Assert.True(ball.CurrentVector.X >= 2 + creator.Radius && ball.CurrentVector.X <= Manager._width - creator.Radius - 1);
        Assert.True(ball.CurrentVector.Y >= 2 + creator.Radius && ball.CurrentVector.Y <= Manager._height - creator.Radius - 1);
        //Assert.True(ball.Weight <= 2 && ball.Weight > 0);
        Assert.AreEqual(2, ball.Weight);
    }
}