using BingoGame.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core
{
    public class BingoGameOptions
    {
        static (int min, int max) DefaultAllowedNumbersRange = (1, 52);
        static (int col, int row) DefaultTableMeasure = (5, 5);
        static Func<IEnumerable<int>, int> DefaultNumberGenerator = (allowedNumbers) =>
        {
            var index = Random.Shared.Next(0, allowedNumbers.Count() - 1);
            return allowedNumbers.ToArray()[index];

        };

        static IEnumerable<int> DefaultAllowedCollection =>
            CollectionUtils.GetAllowedNumbersFromRange(DefaultAllowedNumbersRange.min, DefaultAllowedNumbersRange.max);

        public BingoGameOptions()
        {
        }

        Func<IEnumerable<int>, int> numberGenerator = DefaultNumberGenerator;
        public Func<IEnumerable<int>, int> NumberGenerator
        {
            get => numberGenerator;
            set
            {
                if (numberGenerator == value)
                    return;

                if (numberGenerator == null ||
                    !DefaultAllowedCollection.Contains(value.Invoke(DefaultAllowedCollection)))
                    throw new Exception("Invalid generator");

                numberGenerator = value;
            }
        }

        (int min, int max) allowedNumbersRange = DefaultAllowedNumbersRange;
        public (int min, int max) AllowedNumbersRange
        {
            get => allowedNumbersRange;
            set
            {
                if (allowedNumbersRange == value)
                    return;

                if (value.min >= value.max)
                    throw new Exception("Invalid range");

                allowedNumbersRange = value;
            }
        }

        (int col, int row) tableMeasure = DefaultTableMeasure;
        public (int col, int row) TableMeasure
        {
            get => tableMeasure;
            set
            {
                if (tableMeasure == value)
                    return;

                if (value.col <= 0 || value.row <= 0)
                    throw new Exception("Cannot be less than 0");

                tableMeasure = value;
            }
        }
    }
}
