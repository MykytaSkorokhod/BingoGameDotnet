using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core
{
    public interface IBingoTable
    {
        IBingoGameInstance GameInstance { get; }

        //event Action<int> NumberMatchEvent;
        event Action<int> NewNumberEvent;
        IBingoCell[,] Matrix { get; }
        bool IsTableInGame { get; }
        event Action AllNumbersMatchedEvent;
        bool IsTableSolved { get; }
    }
}
