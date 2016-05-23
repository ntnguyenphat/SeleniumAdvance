using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeleniumAdvance.Common
{
    public class CommonMethods
    {
        /// <summary>
        /// Gets the unique string.
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueString()
        {
           return DateTime.Now.ToString("ddMMMyyHHmmssfff");
           
        }
    }
}
