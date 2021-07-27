using BusinessLayer;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoneyCalculatorUnitTests
{
    [TestClass]
    public class BusinessMoneyCalculatorUnitTests
    {


        [TestInitialize]
        public void Initialize()
        {

        }

        [TestCleanup]
        public void CleanUp()
        {

        }


        [TestMethod]
        public void TestMaxNullMonies()
        {
            MoneyCalculator moneyCalculator = new();
            Assert.ThrowsException<ApplicationException>(() => { moneyCalculator.Max(null); }, "The monies cannot be null");
        }
    }
}
