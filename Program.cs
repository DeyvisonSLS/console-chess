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
                ChessMatch chessMatch = new ChessMatch();

                while(!chessMatch.MatchEnded)
                {
                    Console.Clear();
                    Screen.PrintGameBoard(chessMatch.Gboard);

                    Console.WriteLine("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    
                    bool[,] possiblePositions = chessMatch.Gboard.GetPiece(origin).PossibleMovements(); 

                    Console.Clear();
                    Screen.PrintGameBoard(chessMatch.Gboard, possiblePositions);

                    Console.WriteLine("Destination: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();

                    chessMatch.MovePiece(origin, destination);
                }
            }
            catch(GameBoardException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
