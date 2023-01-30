using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core.Utils
{
    internal static class MatrixUtils
    {
        internal static bool AllByFirstDimention<T>(T[,] matrix, Func<T, bool> predicate)
        {
            var col = matrix.GetLength(0);
            var row = matrix.GetLength(1);
            List<List<T>> collections = new List<List<T>>(Enumerable.Range(0, col).Select(x => new List<T>()));

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    collections[c].Add(matrix[c, r]);
                }
            }

            return collections.Any(x => x.All(predicate));
        }

        internal static bool AllBySecondDimention<T>(T[,] matrix, Func<T, bool> predicate)
        {
            var col = matrix.GetLength(0);
            var row = matrix.GetLength(1);
            List<List<T>> collections = new List<List<T>>(Enumerable.Range(0, col).Select(x => new List<T>()));


            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    collections[r].Add(matrix[c, r]);
                }
            }

            return collections.Any(x => x.All(predicate));
        }

        internal static bool AllByDiagonals<T>(T[,] matrix, Func<T, bool> predicate)
        {
            var col = matrix.GetLength(0);
            var row = matrix.GetLength(1);

            if (col != row)
                throw new ArgumentException("For not square matrix not possible solve diagonals");

            List<T> firstDiagonal = new List<T>();
            List<T> secondDiagonal = new List<T>();

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (c == r)
                    {
                        firstDiagonal.Add(matrix[r, c]);
                    }

                    if (c + r == col - 1)
                    {
                        secondDiagonal.Add(matrix[r, c]);
                    }
                }
            }

            return firstDiagonal.All(predicate) || secondDiagonal.All(predicate);
        }

        internal static bool All<T>(T[,] matrix, Func<T, bool> predicate)
        {
            var col = matrix.GetLength(0);
            var row = matrix.GetLength(1);
            var coll = new List<T>();


            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    coll.Add(matrix[c, r]);
                }
            }

            return coll.All(predicate);
        }
    }
}
