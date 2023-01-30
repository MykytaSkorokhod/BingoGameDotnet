using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core.Instance
{
    internal class BingoTable : IBingoTable
    {
        BingoGameInstance _gameInstance;
        IBingoCell[,] _matrix;

        /// <summary>
        /// Create table with game instance parameters
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <exception cref="ArgumentNullException">'gameInstance' parameter cannot be null</exception>
        internal BingoTable(BingoGameInstance gameInstance)
        {
            if (gameInstance == null)
                throw new ArgumentNullException("'gameInstance' parameter cannot be null");

            _gameInstance = gameInstance;

            var allowedNumbers = _gameInstance._allowedNumbers.ToList();

            _matrix = new IBingoCell[_gameInstance.TableMeasure.col, _gameInstance.TableMeasure.row];

            for (int i = 0; i < _gameInstance.TableMeasure.col; i++)
            {
                for (int t = 0; t < _gameInstance.TableMeasure.row; t++)
                {
                    _matrix[i, t] = new BingoCell(_gameInstance.GenerateNumber(allowedNumbers));
                }
            }
        }

        #region API

        public IBingoGameInstance GameInstance => _gameInstance;

        public IBingoCell[,] Matrix => _matrix;

        public bool IsTableInGame => GameInstance.IsGameOngoing;

        public event Action<int> NumberMatchEvent;

        public event Action<int> NewNumberEvent;

        public event Action AllNumbersMatchedEvent;

        public bool IsTableSolved { get; internal set; }

        #endregion

        #region Internal

        internal void NumberMatch(IBingoCell bingoCell)
        {
            ((BingoCell)bingoCell).Match();
            NumberMatchEvent?.Invoke(bingoCell.Value);
        }
        internal void NewNumber(int number)
        {
            NewNumberEvent?.Invoke(number);
        }

        #endregion
    }
}
