using System;
using System.Collections.Generic;
using System.Text;
using Board;
using ChessPieces;

namespace xadrez_console
{
    class Screen
    {
        // Prints the gameboard without the possible movements; without change the background color
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
        //
        public static void PrintCapturedPieces(ChessMatch chessMatch)
        {
            Console.WriteLine("Captured Pieces:");
            //
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Black:");
            Console.WriteLine(PrintHashSet(chessMatch.GetCapturedPieces(Color.Black)));
            Console.ForegroundColor = aux;
            //
            Console.Write("White:");
            Console.WriteLine(PrintHashSet(chessMatch.GetCapturedPieces(Color.White)));
            if(chessMatch.Check)
            {
                Console.WriteLine();
                Console.WriteLine(chessMatch.currentPlayer.ToString() + " king is in check!");
            }
        }
        public static string PrintHashSet(HashSet<Piece> hashset)
        {
            StringBuilder str = new StringBuilder();
            str.Append("[");
            foreach(Piece x in hashset)
            {
                str.Append(x + " ");
            }
            str.Append("]");
            return str.ToString();
        }
        public static void PrintGameBoard(GameBoard gboard, bool[,] possiblePositions)
        {
            ConsoleColor black = Console.BackgroundColor;
            ConsoleColor gray = ConsoleColor.DarkGray;

            //  Line's printing (vertical)
            for(int i = 0; i < gboard.Lines; i++)
            {
                Console.Write(8 - i + " ");
                //  Collumn's printing (horizontal)
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
                //  If the last collumn's position stay in gray, the next place in the next line will have the background
                //  color gray until that the "for" find in the array a false value.
                //
                //  So, resetting the background color whenever that we reach the last collumn, should solve the problem
                Console.BackgroundColor = black;
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            //  Last background color reset before get out the gameboard printing
            Console.BackgroundColor = black;
        }
        public static ChessPosition ReadChessPosition()
        {

            string s = Console.ReadLine();
            char collumn;
            int line;
            if(s.Length != 2)
            {
                throw new GameBoardException("Looks like you entered a wrong origin.");
            }
            //  Checking the s[0] type; it needs to be a value that can be parsed to char.
            bool isChar = char.TryParse(s[0] + "", out char c);
            if(isChar)
            {
                collumn = c;
            }
            else
            {
                throw new GameBoardException("Looks like you entered a wrong origin.");
            }
            //  Checking s[1] type; it needs to be a value that can be parsed to Int.
            bool isNumeric = int.TryParse(s[1] + "", out int n); //  + "" no lugar de .ToString()
            if(isNumeric)
            {
                line = n;
            }
            else
            {
                throw new GameBoardException("Looks like you entered a wrong origin.");
            }
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