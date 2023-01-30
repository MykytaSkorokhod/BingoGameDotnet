using BingoGame.Core.Instance;
using BingoGame.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core
{
    public class BingoGameBuilder
    {
        BingoGameInstance currentInstance;

        public BingoGameBuilder()
        {
            currentInstance = new BingoGameInstance();
        }

        public BingoGameBuilder Configure(BingoGameOptions bingoGameOptions)
        {
            ThrowIfOptionsAreValid(bingoGameOptions);

            currentInstance.SetNumberGenerator(bingoGameOptions.NumberGenerator);
            currentInstance.SetAllowedNumbers(
                CollectionUtils.GetAllowedNumbersFromRange(bingoGameOptions.AllowedNumbersRange.min, bingoGameOptions.AllowedNumbersRange.max));
            currentInstance.SetTableMeasure(bingoGameOptions.TableMeasure.col, bingoGameOptions.TableMeasure.row);
            currentInstance.SetWinConditions(bingoGameOptions.WinCondition);

            return this;
        }

        public IBingoGameInstance Build()
        {
            try
            {
                return currentInstance;
            }
            finally
            {
                currentInstance = null;
            }
        }

        public void ThrowIfOptionsAreValid(BingoGameOptions bingoGameOptions)
        {
            var allowedNumbersCount = bingoGameOptions.AllowedNumbersRange.max - bingoGameOptions.AllowedNumbersRange.min;
            var tableCellsCount = bingoGameOptions.TableMeasure.col * bingoGameOptions.TableMeasure.row;

            if (tableCellsCount > allowedNumbersCount)
                throw new Exception($"Table too big for range of allowed numbers\n" +
                    $"Allowed numbers count: {allowedNumbersCount}\n" +
                    $"Table cells count: {tableCellsCount}");
        }
    }
}
