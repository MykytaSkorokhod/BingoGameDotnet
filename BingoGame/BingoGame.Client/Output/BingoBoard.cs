using BingoGame.Client.ConsoleUI;
using BingoGame.Client.Resources;
using BingoGame.Client.Utils;
using BingoGame.Core;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Client.Output
{
    internal class BingoBoard
    {
        const string pressKeyToContinue = "Press Enter to roll new number";
        Action _iterationCallBack;

        IBingoTable _bingoTable;

        public BingoBoard(IBingoTable bingoTable, Action iterationCallBack)
        {
            _bingoTable = bingoTable;
            _iterationCallBack = iterationCallBack;

            _bingoTable.NewNumberEvent += NumberReleasedCallBack;
        }

        #region API

        public void Run()
        {
            WriteTableFrame();

            while (_bingoTable.IsTableInGame)
            {
                Console.ReadLine();
                _iterationCallBack?.Invoke();
            }

            if (_bingoTable.IsTableSolved)
                WriteWinGameMessage();
            else
                WriteLostGameMessage();
        }

        #endregion

        #region UI

        void WriteTableFrame()
        {
            ConsoleBoardUI.WriteBoardUI(_bingoTable);
            Console.Write($"\n{pressKeyToContinue}");
        }

        void WriteWinGameMessage()
        {
            Console.Write("\n\n");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Write($"{Localization.winGameMessage}");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write($"\n{Localization.returnToMenuMessage} ");
            Console.ReadKey();
        }

        void WriteLostGameMessage()
        {
            Console.Write("\n\n");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"{Localization.lostGameMessage}");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write($"\n{Localization.returnToMenuMessage} ");
            Console.ReadKey();
        }

        #endregion

        #region CallBacks

        void NumberReleasedCallBack(int obj)
        {
            WriteTableFrame();
        }

        #endregion
    }
}
