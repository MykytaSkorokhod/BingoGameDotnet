using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core.Instance
{
    internal class BingoCell : IBingoCell
    {
        public BingoCell(int value)
        {
            Value = value;
            IsMatched = false;
        }

        public int Value { get; }

        public bool IsMatched { get; private set; }

        internal void Match()
        {
            IsMatched = true;
        }
        internal void UnMatch()
        {
            IsMatched = false;
        }
    }
}
