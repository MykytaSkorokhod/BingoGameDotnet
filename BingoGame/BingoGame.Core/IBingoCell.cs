using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core
{
    public interface IBingoCell
    {
        int Value { get; }
        bool IsMatched { get; }
    }
}
