using BingoGame.Core.Utils;
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
        bool _isGameOngoing = false;
        List<WinStrategy> _winStrategies = new List<WinStrategy>();

        #region API

        public void StartGame()
        {
            _isGameOngoing = true;
        }

        public bool IsGameOngoing => _isGameOngoing;

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

            _releasedNumbers.Add(number);
            _allowedNumbers.Remove(number);

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

                table.NewNumber(number);

                foreach (var winStrat in _winStrategies)
                {
                    if (winStrat == WinStrategy.Horizontal)
                    {
                        if (MatrixUtils.AllBySecondDimention(table.Matrix, (x) => x.IsMatched))
                            table.IsTableSolved = true;
                    }
                    if (winStrat == WinStrategy.Vertical)
                    {
                        if (MatrixUtils.AllByFirstDimention(table.Matrix, (x) => x.IsMatched))
                            table.IsTableSolved = true;
                    }
                    if (winStrat == WinStrategy.Diagonal)
                    {
                        if (MatrixUtils.AllByDiagonals(table.Matrix, (x) => x.IsMatched))
                            table.IsTableSolved = true;
                    }
                    if (winStrat == WinStrategy.Whole)
                    {
                        if (MatrixUtils.All(table.Matrix, (x) => x.IsMatched))
                            table.IsTableSolved = true;
                    }

                    if (table.IsTableSolved)
                    {
                        _isGameOngoing = false;
                        break;
                    }
                }
            }

            NumberReleasedEvent?.Invoke(number);

            return number;

        }

        public IBingoTable CreateTable()
        {
            if (_isGameOngoing)
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

        internal void SetWinConditions(IEnumerable<WinStrategy> winStrategies)
        {
            _winStrategies = winStrategies.Distinct().ToList();
        }

        internal Func<IEnumerable<int>, int> NumberGenerator => _numberGenerator;
        internal (int col, int row) TableMeasure => (_tableColCount, _tableRowCount);

        internal int GenerateNumber(List<int> allowedNumbers)
        {
            if (_allowedNumbers.Count == 0)
                throw new Exception("Cannot generate new number when we not have allowed numbers");

            var number = _numberGenerator?.Invoke(allowedNumbers);

            if (!number.HasValue)
                throw new Exception("Number generator are invalid or missing");

            allowedNumbers.Remove(number.Value);

            return number.Value;
        }

        #endregion
    }
}
