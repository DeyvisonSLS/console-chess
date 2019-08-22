using System;
using Board;
using ChessPieces;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition chessPos = new ChessPosition('c', 7);
            // Console.WriteLine(chessPos);
            // Console.WriteLine(chessPos.ToPosition());

            GameBoard gboard = new GameBoard(8, 8);
            gboard.PutPiece(new King(gboard, Color.Black), chessPos.ToPosition());

            Screen.PrintGameBoard(gboard);
        }
    }
}
