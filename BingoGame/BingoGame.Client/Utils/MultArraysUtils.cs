using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Client.Utils
{
    public static class MultArraysUtils<T>
    {
        public static IEnumerable<T?> GetEmunFromMultArrayByFirstIndex(T?[,] multArray, int firstIndex)
        {
            List<T?> result = new List<T?>();

            var secondDimentionLenth = multArray.GetLength(1);

            for (int i = 0; i < secondDimentionLenth; i++)
            {
                result.Add(multArray[firstIndex, i]);
            }

            return result;
        }
        public static IEnumerable<T?> GetEmunFromMultArrayBySecondIndex(T?[,] multArray, int secondIndex)
        {
            List<T?> result = new List<T?>();

            var firstDimentionLenth = multArray.GetLength(0);

            for (int i = 0; i < firstDimentionLenth; i++)
            {
                result.Add(multArray[i, secondIndex]);
            }

            return result;
        }
    }
}
