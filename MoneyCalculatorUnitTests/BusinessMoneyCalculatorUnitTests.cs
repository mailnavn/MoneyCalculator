using BusinessLayer;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using ApplicationException = Common.ApplicationException;

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
            
            // Negative amount test data
            _MockIMoneyNegativeAmountGBP1 = new Mock<IMoney>();
            _MockIMoneyNegativeAmountGBP1.Setup(_ => _.Amount).Returns(-10.100m);
            _MockIMoneyNegativeAmountGBP1.Setup(_ => _.Currency).Returns("GBP");

            // Negative amount test data
            _MockIMoneyNegativeAmountGBP2 = new Mock<IMoney>();
            _MockIMoneyNegativeAmountGBP2.Setup(_ => _.Amount).Returns(-5.100m);
            _MockIMoneyNegativeAmountGBP2.Setup(_ => _.Currency).Returns("GBP");

            //Euro currenty
            _MockIMoneyEUR1 = new Mock<IMoney>();
            _MockIMoneyEUR1.Setup(_ => _.Amount).Returns(100.100m);
            _MockIMoneyEUR1.Setup(_ => _.Currency).Returns("EUR");


            //Empty currenty
            _MockIMoneyEmptyCurrency = new Mock<IMoney>();
            _MockIMoneyEmptyCurrency.Setup(_ => _.Amount).Returns(100.100m);
            _MockIMoneyEmptyCurrency.Setup(_ => _.Currency).Returns(String.Empty);

            //WhiteSpace currenty
            _MockIMoneyWhiteSpaceCurrency = new Mock<IMoney>();
            _MockIMoneyWhiteSpaceCurrency.Setup(_ => _.Amount).Returns(100.100m);
            _MockIMoneyWhiteSpaceCurrency.Setup(_ => _.Currency).Returns("   ");

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

        [TestMethod]
        public void TestMaxEmptyListMonies()
        {
            MoneyCalculator moneyCalculator = new();
            Assert.ThrowsException<ApplicationException>(() => { moneyCalculator.Max(new List<IMoney> { }); }, "The monies cannot be empty or null");
        }


        /// <summary>
        /// Test by providing valid monies in GPB currency and verify maxium is returned
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



        /// <summary>
        /// Test by providing valid monies in GPB with 2 IMoney objects have same amount and currency and verify maxium is returned
        /// </summary>
        [TestMethod]
        public void TestMaxEqualAmount()
        {
            // There are 2 equal max amounts added in test data
            var monies = new List<IMoney> { _MockIMoneyGBP1.Object, _MockIMoneyGBP1.Object, _MockIMoneyGBP3.Object };
            var result = moneyCalculator.Max(monies);

            // Verify if  100.100 is returned back which is the maximum
            Assert.AreEqual(100.100m, result.Amount);

            //verify the currency is GBP
            Assert.AreEqual("GBP", result.Currency);

        }


        /// <summary>
        /// Test by providing monies in GPB containing negative numbers for amount
        /// </summary>
        [TestMethod]
        public void TestMaxNegativeAmount()
        {
            // There are 2 equal max amounts added in test data
            var monies = new List<IMoney> { _MockIMoneyGBP1.Object, _MockIMoneyGBP2.Object, _MockIMoneyNegativeAmountGBP1.Object };
            var result = moneyCalculator.Max(monies);

            // Verify if  100.100 is returned back which is the maximum
            Assert.AreEqual(100.100m, result.Amount);

            //verify the currency is GBP
            Assert.AreEqual("GBP", result.Currency);

        }


        /// <summary>
        /// Test by providing monies in GPB containing only negative numbers for amount
        /// </summary>
        [TestMethod]
        public void TestMaxOnlyNegativeAmounts()
        {
            // There are 2 equal max amounts added in test data
            var monies = new List<IMoney> { _MockIMoneyNegativeAmountGBP1.Object, _MockIMoneyNegativeAmountGBP2.Object };
            var result = moneyCalculator.Max(monies);

            //verify -5.100m is returned which is maximum 
            Assert.AreEqual("GBP", result.Currency);

        }


        /// <summary>
        /// Test by providing different currencies as monies and ensure that argument exception is thrown
        /// </summary>
        [TestMethod]
        public void TestMaxDifferentCurrencies()
        {
            // There are 2 IMoney objects added with currencies GBP and EUR
            var monies = new List<IMoney> { _MockIMoneyGBP1.Object, _MockIMoneyEUR1.Object };
            Assert.ThrowsException<ArgumentException>(() => { moneyCalculator.Max(monies); }, "All monies are not in the same currency.");
        }


        /// <summary>
        /// Test by providing currency as empty and ensure exception is thrown
        /// </summary>
        [TestMethod]
        public void TestMaxEmptyCurrencyField()
        {
            // There are 2 IMoney objects added with currencies GBP and EUR
            var monies = new List<IMoney> { _MockIMoneyGBP1.Object,  };
            Assert.ThrowsException<ApplicationException>(() => { moneyCalculator.Max(null); }, "The currency cannot be empty or white space");
        }


        /// <summary>
        /// Test by providing currency as white space and ensure exception is thrown
        /// </summary>
        [TestMethod]
        public void TestMaxWhiteSpaceCurrencyField()
        {
            // There are 2 IMoney objects added with currencies GBP and EUR
            var monies = new List<IMoney> { _MockIMoneyGBP1.Object, };
            Assert.ThrowsException<ApplicationException>(() => { moneyCalculator.Max(null); }, "The currency cannot be empty or white space");
        }




        #region private properties

        private Mock<IMoney> _MockIMoneyGBP1;
        private Mock<IMoney> _MockIMoneyGBP2;
        private Mock<IMoney> _MockIMoneyGBP3;
        private Mock<IMoney> _MockIMoneyNegativeAmountGBP1;
        private Mock<IMoney> _MockIMoneyNegativeAmountGBP2;

        private Mock<IMoney> _MockIMoneyEUR1;

        private Mock<IMoney> _MockIMoneyEmptyCurrency;
        private Mock<IMoney> _MockIMoneyWhiteSpaceCurrency;


        private MoneyCalculator moneyCalculator { get; set; }
        #endregion


    }
}
