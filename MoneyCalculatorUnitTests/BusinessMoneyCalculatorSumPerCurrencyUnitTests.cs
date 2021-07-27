using BusinessLayer;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationException = Common.ApplicationException;

namespace MoneyCalculatorUnitTests
{
    [TestClass]
    public class BusinessMoneyCalculatorSumPerCurrencyUnitTests : BusinessMoneyCalculatorMaxUnitTests
    {

        /// <summary>
        /// Test by passing in null for monies object in the SumPerCurrency()
        /// </summary>
        [TestMethod]
        public void TestSumPerCurrencyNullMonies()
        {
            var monies = new List<IMoney> { _MockIMoneyGBP1.Object, _MockIMoneyGBP2.Object, _MockIMoneyEUR1.Object, _MockIMoneyEUR2.Object, };
            var result = moneyCalculator.SumPerCurrency(null);

            Assert.ThrowsException<ApplicationException>(() => { moneyCalculator.Max(null); }, "The monies cannot be empty or null");
        }

        /// <summary>
        /// Test by passing in empty monies for the SumPerCurrency()
        /// </summary>
        [TestMethod]
        public void TestSumPerCurrencyEmptyMonies()
        {
            var monies = new List<IMoney> {};
            var result = moneyCalculator.SumPerCurrency(null);
            Assert.ThrowsException<ApplicationException>(() => { moneyCalculator.Max(null); }, "The monies cannot be empty or null");
        }


        /// <summary>
        /// Test for valid input containing GBP and EUR monies
        /// </summary>
        [TestMethod]
        public void TestValidSumPerCurrency()
        {
            var monies = new List<IMoney> { _MockIMoneyGBP1.Object, _MockIMoneyGBP2.Object, _MockIMoneyEUR1.Object, _MockIMoneyEUR2.Object, };
            var result = moneyCalculator.SumPerCurrency(monies);

            // Verify for GBP , sum = 100.100 + 50.100
            Assert.AreEqual(150.200, result.FirstOrDefault(_ => _.Currency.Contains("GBP")).Amount);

            // Verify for EUR , sum = 100.100 + 300.100
            Assert.AreEqual(400.200, result.FirstOrDefault(_ => _.Currency.Contains("GBP")).Amount);

            // verify that the are only 2 monies in the list
            Assert.AreEqual(2, result.Count());

        }

        /// <summary>
        /// Test when there is are monies with only 1 type of currencies
        /// </summary>

        [TestMethod]
        public void TestSumePerCurrencyWithOnlyOneCurrencyType()
        {
            var monies = new List<IMoney> { _MockIMoneyGBP1.Object, _MockIMoneyGBP2.Object };
            var result = moneyCalculator.SumPerCurrency(monies);

            // Verify for GBP , sum = 100.100 + 50.100
            Assert.AreEqual(150.200, result.FirstOrDefault(_ => _.Currency.Contains("GBP")).Amount);

            // verify that the are only 2 monies in the list
            Assert.AreEqual(1, result.Count());

        }

        /// <summary>
        /// Test when there are monies with currency field empty
        /// ASSUMPTION: I am throwing application exception in case of blank currency type
        /// </summary>
        [TestMethod]
        public void TestSumePerCurrencyWithEmptyCurrentyType()
        {
            var monies = new List<IMoney> { _MockIMoneyGBP1.Object, _MockIMoneyEmptyCurrency.Object };
            Assert.ThrowsException<ApplicationException>(() => { moneyCalculator.SumPerCurrency(monies); }, "The currency cannot be empty or white space");
        }


    }
}
