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
                Console.Write(8 - i + " ");
                for(int j = 0; j < gboard.Collumns; j++)
                {
                    if(gboard.GetPiece(i, j) == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        PrintPiece(gboard.GetPiece(i,j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PrintPiece(Piece piece)
        {
            if(piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}