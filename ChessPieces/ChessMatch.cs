using Board;

namespace ChessPieces
{
    class ChessMatch
    {
        public GameBoard Gboard { get; set; }
        public bool MatchEnded { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public ChessMatch()
        {
            Gboard = new GameBoard(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            MatchEnded = false;
            PutPieces();
        }
        public void DoTheMove(Position origin, Position destination)
        {
            MovePiece(origin, destination);
            turn++;
            ChangePlayer();
        }
        public void ValidateOriginPosition(Position pos)
        {
            if(Gboard.GetPiece(pos) == null)
            {
                throw new GameBoardException("There is no piece in the specified location.");
            }
            if(Gboard.GetPiece(pos).Color != currentPlayer)
            {
                throw new GameBoardException("That piece is not yours.");
            }
            if(!Gboard.GetPiece(pos).ExistsPossibleMovement())
            {
                throw new GameBoardException("That piece is blocked and there is no possible movements.");
            }
        }
        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if(!Gboard.GetPiece(origin).CanMoveTo(destination))
            {
                throw new GameBoardException("Impossible movement.");
            }
        }
        public void MovePiece(Position origin, Position destination)
        {
            //  It takes the pieces (now it is lifted up) in origin and in the destination
            Piece pPicked = Gboard.LiftPiece(origin);
            //  For a while i'm storing but not using the piece captured
            Piece pCaptured = Gboard.LiftPiece(destination);
            //  Putting piece in game board and changing the position inside the piece; after, increment the piece's move
            Gboard.PutPiece(pPicked, destination);
            pPicked.Positioning = destination;
            pPicked.IncrementQtdMoves();
        }
        private void ChangePlayer()
        {
            if(currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }
        //  Here the chessmatch organize the initial pieces set up.
        public void PutPieces()
        {
            Gboard.PutPiece(new Tower(Gboard, Color.White), new ChessPosition('c', 1).ToPosition());
            Gboard.PutPiece(new Tower(Gboard, Color.White), new ChessPosition('c', 2).ToPosition());
            Gboard.PutPiece(new Tower(Gboard, Color.White), new ChessPosition('d', 2).ToPosition());
            Gboard.PutPiece(new Tower(Gboard, Color.White), new ChessPosition('e', 2).ToPosition());
            Gboard.PutPiece(new Tower(Gboard, Color.White), new ChessPosition('e', 1).ToPosition());
            Gboard.PutPiece(new King(Gboard, Color.White), new ChessPosition('d', 1).ToPosition());
            
            Gboard.PutPiece(new Tower(Gboard, Color.Black), new ChessPosition('c', 8).ToPosition());
            Gboard.PutPiece(new Tower(Gboard, Color.Black), new ChessPosition('c', 7).ToPosition());
            Gboard.PutPiece(new Tower(Gboard, Color.Black), new ChessPosition('d', 7).ToPosition());
            Gboard.PutPiece(new Tower(Gboard, Color.Black), new ChessPosition('e', 7).ToPosition());
            Gboard.PutPiece(new Tower(Gboard, Color.Black), new ChessPosition('e', 8).ToPosition());
            Gboard.PutPiece(new King(Gboard, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}