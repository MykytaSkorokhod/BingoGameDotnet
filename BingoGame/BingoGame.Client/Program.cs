using BingoGame.Client.Output;
using BingoGame.Core;

namespace BingoGame
{
    internal class Program
    {
        static IBingoGameInstance _bingoGameInstance;
        static BingoBoard _bingoBoard;

        static void Main(string[] args)
        {
            var bingoGameBuilder = new BingoGameBuilder();
            _bingoGameInstance = bingoGameBuilder.Configure(new BingoGameOptions
            {
                AllowedNumbersRange = (1, 52),
                TableMeasure = (5, 5)
            }).Build();

            _bingoBoard = new BingoBoard(_bingoGameInstance);

            Console.ReadKey();
        }
    }
}