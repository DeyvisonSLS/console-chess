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
                // Instantiate a new chess match
                ChessMatch chessMatch = new ChessMatch();

                while(!chessMatch.MatchEnded)
                {
                    try
                    {
                        // Print the chess match
                        Console.Clear();
                        Screen.PrintGameBoard(chessMatch.Gboard);
                        Console.WriteLine();
                        Screen.PrintCapturedPieces(chessMatch);
                        Console.WriteLine();
                        Console.WriteLine("Turn: " + chessMatch.turn);
                        //  Set a string with the name of the current player (White or Black)
                        string currentPlayer = (chessMatch.currentPlayer == Color.White) ? "White" : "Black";
                        Console.WriteLine("It's the player's turn: " + currentPlayer);
                        Console.WriteLine();
                        Console.WriteLine("Origin: ");
                        //  Awaits te entry of the piece that the player intends to move and convert from ChessPosition ("a1", "b3") to Position ([0,7], [1,5]);
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        //  Validate the piece picking
                        chessMatch.ValidateOriginPosition(origin);
                        // Returns and stores a boolean's matrix of specific movements of the Gboard.GetPiece(origin)
                        bool[,] possibleMovements = chessMatch.Gboard.GetPiece(origin).PossibleMovements(); 
                        Console.Clear();
                        //  Reprint the gameboard taking the possible movements of the piece into account
                        Screen.PrintGameBoard(chessMatch.Gboard, possibleMovements);
                        Console.WriteLine("Destination: ");
                        //  Awaits te entry of the piece that the player intends to move and convert from ChessPosition ("a1", "b3") to Position ([0,7], [1,5]);
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        chessMatch.ValidateDestinationPosition(origin, destination);
                        //  Remove the piece from it's origin an nullify both the the position in the game board and the piece's positioning
                        //  Then put that in the destination place
                        chessMatch.DoTheMove(origin, destination);
                    }
                    catch(GameBoardException e)
                    {
                        Console.Write(e.Message);
                        Console.Write(" Press enter and try again.");
                        Console.ReadLine();
                    }
                    catch(IndexOutOfRangeException)
                    {
                        Console.WriteLine("You put a invalid position. Press enter and try again.");
                        Console.ReadLine();
                    }
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
