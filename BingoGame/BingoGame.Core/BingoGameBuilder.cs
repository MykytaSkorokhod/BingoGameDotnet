using BingoGame.Core.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Core
{
    public class BingoGameBuilder
    {
        BingoGameInstance? currentInstance;

        public BingoGameBuilder()
        {
            currentInstance = new BingoGameInstance();
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
    }
}
