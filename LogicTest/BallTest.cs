using NUnit.Framework;
using Logic;
using System;


namespace LogicTest
{
    public class LogicTests
    {

            [SetUp]
            public void Setup()
            {
            }
            LogicAbstractApi logicApi = LogicAbstractApi.CreateApi();
            [Test]
            public void InstanceTest()
            {
                Assert.IsNotNull(logicApi);
            }
            [Test]
            public void AddBallTest()
            {
                Assert.IsNotNull(logicApi);
                logicApi.create(1);
                Assert.AreEqual(1, logicApi.getNum());
                logicApi.create(1);
                Assert.AreEqual(2, logicApi.getNum());
            }
            [Test]
            public void CheckCollisionsTest1()
            {
                LogicAbstractApi logicApi = LogicAbstractApi.CreateApi();
                logicApi.create(1);
                Assert.AreEqual(1, logicApi.getNum());
                double test_x = logicApi.getX(0);
                logicApi.LogicApiEvent += (sender, args) =>
                {
                    if (test_x > logicApi.getX(0))
                    {
                        Assert.IsTrue(logicApi.getX(0) < 450);
                        return;
                    }
                    test_x = logicApi.getX(0);
                };
            }
            [Test]
            public void CheckCollisionsTest2()
            {
                LogicAbstractApi logicApi = LogicAbstractApi.CreateApi();
                logicApi.create(2);
                Assert.AreEqual(2, logicApi.getNum());
                double testDis = Distance(logicApi.getX(0), logicApi.getX(1));
                int testFlag = 0;
                logicApi.LogicApiEvent += (sender, args) =>
                {
                    if (testFlag == 0 && testDis <= Distance(logicApi.getX(0), logicApi.getX(1)))
                    {
                        testFlag = 1;
                    }
                    if (testFlag == 1)
                    {
                        Assert.IsTrue(testDis > Distance(logicApi.getX(0), logicApi.getX(1)));
                        return;
                    }
                    testDis = Distance(logicApi.getX(0), logicApi.getX(1));
                };
            }
            private double Distance(double x1, double x2)
            {
                return Math.Abs(x1 - x2);
            }
        
    }
}
