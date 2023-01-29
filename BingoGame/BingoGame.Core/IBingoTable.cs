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

        event Action<int> NumberMatchEvent;
        IBingoCell[,] Matrix { get; }
        bool IsTableInGame { get; }
        event Action AllNumbersMatchedEvent;
    }
}
