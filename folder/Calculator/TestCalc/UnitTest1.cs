using Calculator;
namespace TestCalc
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1()
        {
            Calc calc = new Calc();

            Assert.AreEqual(calc.div(10, 5), 2);
        }

        [TestMethod]
        public void Test2()
        {
            Calc calc = new Calc();

            Assert.AreEqual(calc.sub(10, 5), 5);
        }

        [TestMethod]
        public void Test3()
        {
            Calc calc = new Calc();

            Assert.AreEqual(calc.mul(10, 5), 50);
        }
    }
}