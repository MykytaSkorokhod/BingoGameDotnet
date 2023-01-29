using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core
{
    public interface IBingoGameInstance
    {
        IEnumerable<int> ReleasedNumbers { get; }
        IEnumerable<IBingoTable> ReleasedTables { get; }
        IBingoTable CreateTable();
        int ReleaseNewNumber();
        event Action<int> NumberReleasedEvent;
        bool IsGameStarted { get; }
        void StartGame();
    }
}
