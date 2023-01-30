using BingoGame.Client.Output;
using BingoGame.Core;

namespace BingoGame
{
    internal class Program
    {
        static IBingoGameInstance _bingoGameInstance;
        static BingoBoard _bingoBoard;
        static BingoMenu _bingoMenu;

        static void Main(string[] args)
        {
            _bingoMenu = new BingoMenu();

            // Menu can exit application
            while (true)
            {
                try
                {
                    // Menu give user chose what he wants. At end of menu lifecycle we grab options from menu state and start game
                    _bingoMenu.IsMenuActive = true;
                    _bingoMenu.Run();
                    var conf = _bingoMenu.GetBingoGameOptions();

                    // Init game instance
                    var bingoGameBuilder = new BingoGameBuilder();
                    _bingoGameInstance = bingoGameBuilder.Configure(conf).Build();

                    // Create table for user and run game
                    _bingoBoard = new BingoBoard(_bingoGameInstance.CreateTable(), () => BoardIteration());
                    _bingoGameInstance.StartGame();
                    _bingoBoard.Run();
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.Clear();
                    Console.WriteLine("Some problems:\n");
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("\n\nPress any key to continue");
                    Console.ReadKey();
#endif
                    // Usially here we log

                    // Clear all menu state
                    _bingoMenu = new BingoMenu();
                }
            }
        }

        /// <summary>
        /// User on board chose continue game
        /// </summary>
        static void BoardIteration()
        {
            _bingoGameInstance.ReleaseNewNumber();
        }
    }
}