using NUnit.Framework;
using Data;
using System.Numerics;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System;

namespace DataTest
{

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        DataAbstractApi api = new DataApi();
        [Test]
        public void CreateBallsTest()
        {
            Assert.IsNotNull(api);
            api.create(3);
            Assert.AreEqual(3, api.getNum());
        }
        [Test]
        public void MoveTest()
        {
            Assert.IsNotNull(api);
            api.create(1);
            Assert.AreEqual(4, api.getNum());
            double prev_x = api.getX(0);
            double prev_y = api.getX(0);
            api.BallEvent += (sender, args) =>
            {
                Assert.AreNotEqual(prev_x, api.getX(0));
                Assert.AreNotEqual(prev_y, api.getY(0));
            };
        }

    }
}