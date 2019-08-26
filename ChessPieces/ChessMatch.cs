using System;
using Board;

namespace ChessPieces
{
    class ChessMatch
    {
        public GameBoard Gboard { get; set; }
        public bool MatchEnded { get; private set; }
        private int turn;
        private Color currentPlayer;
        public ChessMatch()
        {
            Gboard = new GameBoard(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            MatchEnded = false;
            PutPieces();
        }
        public void MovePiece(Position origin, Position destination)
        {
            //Pick the piece (now it is lifted up) in origin and in the destination
            Piece pPicked = Gboard.LiftPiece(origin);
            Piece pCaptured = Gboard.LiftPiece(destination);
            //Put piece un game board and alter the position inside the piece; after, increment the piece's move
            Gboard.PutPiece(pPicked, destination);
            pPicked.Positioning = destination;
            pPicked.IncrementQtdMoves();
        }
        public void PutPieces()
        {
            Gboard.PutPiece(new Tower(Gboard, Color.Black), new ChessPosition('c', 3).ToPosition());
        }
    }
}