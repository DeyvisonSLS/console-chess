using System;
using Board;

namespace xadrez_console
{
    class Screen
    {
        public static void PrintGameBoard(GameBoard gboard)
        {
            for(int i = 0; i < gboard.Lines; i++)
            {
                for(int j = 0; j < gboard.Collumns; j++)
                {
                    if(gboard.GetPiece(i, j) == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write(gboard.GetPiece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}