using BingoGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Client.ConsoleUI
{
    internal static class ConsoleBoardUI
    {
        static string crossingRowAndColBorders = "+";
        static string upperRowFramePerColl = $"{crossingRowAndColBorders}----";
        static string leftRightColBorder = "|";

        public static void WriteBoardUI(IBingoTable bingoTable)
        {
            Console.Clear();

            Console.Write("Released numbers: ");
            var releasedNumbersList = bingoTable.GameInstance.ReleasedNumbers.ToList();
            for (int i = 0; i < releasedNumbersList.Count; i++)
            {
                if (i + 1 == releasedNumbersList.Count)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.Write($"{releasedNumbersList[i]}");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                    Console.Write($"{releasedNumbersList[i]}, ");
            }
            Console.Write("\n");
            Console.Write("\n");

            var col = bingoTable.Matrix.GetLength(0);
            var row = bingoTable.Matrix.GetLength(1);


            for (int r = 0; r < row; r++)
            {
                Console.Write(string.Concat(Enumerable.Repeat(upperRowFramePerColl, col)));
                Console.Write(crossingRowAndColBorders);
                Console.Write("\n");

                for (int c = 0; c < col; c++)
                {
                    var currentValue = bingoTable.Matrix[c, r].Value;

                    Console.Write(leftRightColBorder);
                    if (bingoTable.Matrix[c, r].IsMatched)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    // Value have 1 char
                    if (currentValue > 0 && currentValue < 10)
                        Console.Write($"  {currentValue} ");
                    // Value have 2 chars
                    if ((currentValue > -10 && currentValue < 0) || (currentValue >= 10 && currentValue < 100))
                        Console.Write($" {currentValue} ");
                    // Value have 3 chars
                    if ((currentValue <= -10 && currentValue > -100) || (currentValue >= 100 && currentValue < 1000))
                        Console.Write($" {currentValue}");
                    // Value have 4 chars
                    if ((currentValue <= -100 && currentValue > -1000) || (currentValue >= 1000 && currentValue < 10000))
                        Console.Write($"{currentValue}");

                    if (bingoTable.Matrix[c, r].IsMatched)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }

                Console.Write(leftRightColBorder);

                Console.Write("\n");
            }

            Console.Write(string.Concat(Enumerable.Repeat(upperRowFramePerColl, col)));
            Console.Write(crossingRowAndColBorders);
            Console.Write("\n");
        }
    }
}
