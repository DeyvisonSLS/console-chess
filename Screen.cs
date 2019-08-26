using System;
using Board;
using ChessPieces;

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
                    PrintPiece(gboard.GetPiece(i,j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PrintGameBoard(GameBoard gboard, bool[,] possiblePositions)
        {
            ConsoleColor black = Console.BackgroundColor;
            ConsoleColor gray = ConsoleColor.DarkGray;

            for(int i = 0; i < gboard.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for(int j = 0; j < gboard.Collumns; j++)
                {
                    if(possiblePositions[i,j])
                    {
                        Console.BackgroundColor = gray;
                    }
                    else
                    {
                        Console.BackgroundColor = black;
                    }
                    PrintPiece(gboard.GetPiece(i,j));
                }
                Console.BackgroundColor = black;
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = black;
        }
        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char collumn = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(collumn, line);
        }
        public static void PrintPiece(Piece piece)
        {
            if(piece == null)
            {
                Console.Write("_ ");
            }
            else
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
                Console.Write(" ");
            }
        }
    }
}