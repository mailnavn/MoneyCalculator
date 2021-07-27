using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ApplicationException : Exception
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="productErrorCode">The product error code</param>
        public ApplicationException(string message, int productErrorCode) : base(message)
        {
            Code = productErrorCode;
        }


        /// <summary>
        /// The product specific error code
        /// </summary>
        public int Code { get; set; }

    }
}
