using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = Common.ApplicationException;

namespace BusinessLayer
{
    public class MoneyCalculator : IMoneyCalculator
    {
        /// <summary>
        /// Find the largest amount of money.
        /// </summary>
        /// <returns>The <see cref="IMoney"/> instance having the largest amount.</returns>
        /// <exception cref="ArgumentException">All monies are not in the same currency.</exception>
        /// <example>{GBP10, GBP20, GBP50} => {GBP50}</example>
        /// <example>{GBP10, GBP20, EUR50} => exception</example>
        public IMoney Max(IEnumerable<IMoney> monies)
        {
            ValidateNullMonies(monies);

            // If monies are in different currencies, throw argument exception
            if (monies.GroupBy(money => money.Currency).Count() > 1)
                throw new ArgumentException($"All monies are not in the same currency.");

            ValidateEmptyCurrencies(monies);
            var result = monies.FirstOrDefault(money => money.Currency == monies.Max(x => x.Currency));
            return result;
        }

        /// <summary>
        /// Return one <see cref="IMoney"/> per currency with the sum of all monies of the same currency.
        /// </summary>
        /// <example>{GBP10, GBP20, GBP50} => {GBP80}</example>
        /// <example>{GBP10, GBP20, EUR50} => {GBP30, EUR50}</example>
        /// <example>{GBP10, USD20, EUR50} => {GBP10, USD20, EUR50}</example>
        public IEnumerable<IMoney> SumPerCurrency(IEnumerable<IMoney> monies)
        {
            return new List<IMoney>();
        }

        /// <summary>
        /// Validates if the monies is null or empty
        /// </summary>
        /// <param name="monies"></param>
        private void ValidateNullMonies(IEnumerable<IMoney> monies)
        {
            if (monies == null || monies.Count() == 0)
                throw new ApplicationException($"The monies cannot be empty or null", ProductErrorCode.MONEYNULL);
        }

        /// <summary>
        /// Valiate for blank or empty currency
        /// </summary>
        /// <param name="monies"></param>
        private void ValidateEmptyCurrencies(IEnumerable<IMoney> monies)
        {
            // If monies contains currency that is empty or blank, throw application exception
            if (monies.Any(x => string.IsNullOrWhiteSpace(x.Currency)))
                throw new ApplicationException($"The currency cannot be empty or white space", ProductErrorCode.CURRENCYEMPTYORNULL);
        }
    }
}
