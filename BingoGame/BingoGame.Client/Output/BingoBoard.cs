using BingoGame.Client.ConsoleUI;
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

        IBingoGameInstance _gameInstance;
        IBingoTable _bingoTable;

        public BingoBoard(IBingoGameInstance gameInstance)
        {
            _gameInstance = gameInstance;
            _bingoTable = gameInstance.CreateTable();

            _gameInstance.NumberReleasedEvent += NumberReleasedCallBack;
            _bingoTable.NumberMatchEvent += NumberMatchCallback;
            _bingoTable.AllNumbersMatchedEvent += AllNumbersMatchedCallback;

            UpdateFrame();
            _gameInstance.StartGame();

            while (_bingoTable.IsTableInGame)
            {
                Console.ReadLine();
                _gameInstance.ReleaseNewNumber();
            }
        }

        #region API

        #endregion

        #region UI

        void UpdateFrame()
        {
            ConsoleBoardUI.WriteBoardUI(_bingoTable);
            Console.Write($"\n{pressKeyToContinue}");
        }

        #endregion

        #region CallBacks

        void NumberReleasedCallBack(int obj)
        {
            UpdateFrame();
        }

        void NumberMatchCallback(int obj)
        {

        }

        void AllNumbersMatchedCallback()
        {

        }

        #endregion
    }
}
