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
        Assert.True(ball.currentVector.X >= 2 + creator.Radius && ball.currentVector.X <= Manager.width - creator.Radius - 1);
        Assert.True(ball.currentVector.Y >= 2 + creator.Radius && ball.currentVector.Y <= Manager.height - creator.Radius - 1);
        //Assert.True(ball.Weight <= 2 && ball.Weight > 0);
        Assert.AreEqual(2, ball.Weight);
    }
}