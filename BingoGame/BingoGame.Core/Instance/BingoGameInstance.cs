using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core.Instance
{
    internal class BingoGameInstance : IBingoGameInstance
    {
        internal List<int> _releasedNumbers = new List<int>();
        internal List<int> _allowedNumbers = new List<int>();
        Func<IEnumerable<int>, int> _numberGenerator;
        List<BingoTable> _releasedTables = new List<BingoTable>();
        int _tableColCount;
        int _tableRowCount;
        bool _gameStarted = false;

        #region API

        public void StartGame()
        {
            _gameStarted = true;
        }

        public bool IsGameStarted => _gameStarted;

        public IEnumerable<int> ReleasedNumbers => _releasedNumbers;

        public IEnumerable<IBingoTable> ReleasedTables => _releasedTables.Select(x => (IBingoTable)x);

        public event Action<int>? NumberReleasedEvent;

        /// <summary>
        /// Create and Release new number in game
        /// </summary>
        /// <returns></returns>
        public int ReleaseNewNumber()
        {
            var number = GenerateNumber(_allowedNumbers);

            foreach (var table in _releasedTables)
            {
                for (int c = 0; c < _tableColCount; c++)
                {
                    for (int r = 0; r < _tableRowCount; r++)
                    {
                        if (table.Matrix[c, r].Value == number)
                        {
                            table.NumberMatch(table.Matrix[c, r]);
                        }
                    }
                }
            }

            _releasedNumbers.Add(number);
            _allowedNumbers.Remove(number);

            _gameStarted = true;
            NumberReleasedEvent?.Invoke(number);

            return number;

        }

        public IBingoTable CreateTable()
        {
            if (_gameStarted)
                throw new Exception("Game already started");

            var table = new BingoTable(this);

            _releasedTables.Add(table);

            return table;
        }

        #endregion

        #region Internal

        /// <summary>
        /// Set allowed numbers
        /// </summary>
        /// <param name="allowedNumbers">Collection of allowed numbers. Can be any int's in any order</param>
        internal void SetAllowedNumbers(IEnumerable<int> allowedNumbers)
        {
            _allowedNumbers = allowedNumbers.ToList();
        }

        /// <summary>
        /// Install new number generator.
        /// </summary>
        /// <param name="generator">
        /// Func is a fuction of getting number from allowed
        /// IEnumerable<int> is a list of all allowed results
        /// int is a result
        /// </param>
        internal void SetNumberGenerator(Func<IEnumerable<int>, int> generator)
        {
            _numberGenerator = generator;
        }

        internal void SetTableMeasure(int col, int row)
        {
            _tableColCount = col;
            _tableRowCount = row;
        }

        internal Func<IEnumerable<int>, int> NumberGenerator => _numberGenerator;
        internal (int col, int row) TableMeasure => (_tableColCount, _tableRowCount);

        internal int GenerateNumber(List<int> allowedNumbers)
        {
            var number = _numberGenerator?.Invoke(allowedNumbers);

            if (!number.HasValue)
                throw new Exception("Number generator are invalid or missing");

            allowedNumbers.Remove(number.Value);

            return number.Value;
        }

        #endregion
    }
}
