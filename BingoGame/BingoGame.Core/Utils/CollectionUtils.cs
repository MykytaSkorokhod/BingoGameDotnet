using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core.Utils
{
    internal static class CollectionUtils
    {
        /// <summary>
        /// Set allowed numbers
        /// </summary>
        /// <param name="from">Minimal allowed number</param>
        /// <param name="to">Maximal allowed number</param>
        /// <exception cref="ArgumentException">'from' parameter cannot be lesser than 'to' parameter</exception>
        internal static List<int> GetAllowedNumbersFromRange(int from, int to)
        {
            if (from >= to)
                throw new ArgumentException("'from' parameter must be lesser than 'to'");

            List<int> result = new List<int>();

            for (int i = from; i <= to; i++)
                result.Add(i);

            return result;
        }
    }
}
