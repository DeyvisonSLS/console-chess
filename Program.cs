using System;
using Board;
using ChessPieces;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GameBoard gboard = new GameBoard(8, 8);

                gboard.PutPiece(new King(gboard, Color.Black), new Position(0, 0));
                gboard.PutPiece(new Tower(gboard, Color.Black), new Position(1, 7));
                gboard.PutPiece(new King(gboard, Color.White), new Position(0, 7));

                Screen.PrintGameBoard(gboard);
            }
            catch(GameBoardException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
