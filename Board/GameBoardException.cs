using System;

namespace Board
{
    class GameBoardException : ApplicationException
    {
        public GameBoardException(string message) : base(message)
        {
            
        }
    }
}