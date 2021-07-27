using BusinessLayer;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace MoneyCalculatorUnitTests
{
    [TestClass]
    public class BusinessMoneyCalculatorUnitTests
    {
        

        [TestInitialize]
        public void Initialize()
        {
            moneyCalculator = new();
            // Create valid monies in GBP
            _MockIMoneyGBP1 = new Mock<IMoney>();
            _MockIMoneyGBP1.Setup(_ => _.Amount).Returns(100.100m);
            _MockIMoneyGBP1.Setup(_ => _.Currency).Returns("GBP");
            _MockIMoneyGBP2 = new Mock<IMoney>();
            _MockIMoneyGBP2.Setup(_ => _.Amount).Returns(50.100m);
            _MockIMoneyGBP2.Setup(_ => _.Currency).Returns("GBP");
            _MockIMoneyGBP3 = new Mock<IMoney>();
            _MockIMoneyGBP3.Setup(_ => _.Amount).Returns(10.100m);
            _MockIMoneyGBP3.Setup(_ => _.Currency).Returns("GBP");

        }

        [TestCleanup]
        public void CleanUp()
        {
            moneyCalculator = null;
        }


        [TestMethod]
        public void TestMaxNullMonies()
        {
            MoneyCalculator moneyCalculator = new();
            Assert.ThrowsException<ApplicationException>(() => { moneyCalculator.Max(null); }, "The monies cannot be empty or null");
        }

        /// <summary>
        /// Test by providing valid monies in GPB currency
       /// </summary>
        [TestMethod]
        public void TestValidMoniesGBP()
        {
            var monies = new List<IMoney> { _MockIMoneyGBP1.Object, _MockIMoneyGBP2.Object, _MockIMoneyGBP3.Object };
            var result = moneyCalculator.Max(monies);

            // Verify if  100.100 is returned back which is the maximum
            Assert.AreEqual(100.100m, result.Amount);

            //verify the currency is GBP
            Assert.AreEqual("GBP", result.Currency);

        }


        #region private properties

        private Mock<IMoney> _MockIMoneyGBP1;
        private Mock<IMoney> _MockIMoneyGBP2;
        private Mock<IMoney> _MockIMoneyGBP3;

        private MoneyCalculator moneyCalculator { get; set; }
        #endregion


    }
}
