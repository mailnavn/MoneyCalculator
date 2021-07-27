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
            if (monies == null)
                throw new ApplicationException($"The monies cannot be null", ProductErrorCode.MONEYNULL);

            throw new NotImplementedException();
        }


        /// <summary>
        /// Return one <see cref="IMoney"/> per currency with the sum of all monies of the same currency.
        /// </summary>
        /// <example>{GBP10, GBP20, GBP50} => {GBP80}</example>
        /// <example>{GBP10, GBP20, EUR50} => {GBP30, EUR50}</example>
        /// <example>{GBP10, USD20, EUR50} => {GBP10, USD20, EUR50}</example>
        public IEnumerable<IMoney> SumPerCurrency(IEnumerable<IMoney> monies)
        {
            throw new NotImplementedException();
        }
    }
}
