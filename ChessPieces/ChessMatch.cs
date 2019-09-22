using System.Collections.Generic;
using Board;

namespace ChessPieces
{
    class ChessMatch
    {
        public GameBoard Gboard { get; set; }
        public bool MatchEnded { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        private HashSet<Piece> PiecesInGame;
        private HashSet<Piece> CapturedPieces;
        public ChessMatch()
        {
            Gboard = new GameBoard(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            MatchEnded = false;
            PiecesInGame = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
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
            //  It takes the pieces (now it is lifted up) in origin and in the destination.
            Piece pPicked = Gboard.LiftPiece(origin);
            //  Storing and adding to the hashset of captured pieces if it isn't null.
            Piece pCaptured = Gboard.LiftPiece(destination);
            if(pCaptured != null)
            {
                CapturedPieces.Add(pCaptured);
            }
            //  Putting piece in game board and changing the position inside the piece; after, increment the piece's move.
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
        public HashSet<Piece> GetCapturedPieces (Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in CapturedPieces)
            {
                if(x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Piece> GetPiecesInGame (Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in PiecesInGame)
            {
                if(x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(GetCapturedPieces(color));
            return aux;
        }
        public void PutNewPiece(char collumn, int line, Piece piece)
        {
            Gboard.PutPiece(piece, new ChessPosition(collumn, line).ToPosition());
            PiecesInGame.Add(piece);
        }
        //  Here the chessmatch organize the initial pieces set up.
        public void PutPieces()
        {
            PutNewPiece('c', 1, new Tower(Gboard, Color.White));
            PutNewPiece('c', 2, new Tower(Gboard, Color.White));
            PutNewPiece('d', 2, new Tower(Gboard, Color.White));
            PutNewPiece('e', 2, new Tower(Gboard, Color.White));
            PutNewPiece('e', 1, new Tower(Gboard, Color.White));
            PutNewPiece('d', 1, new King(Gboard, Color.White));

            PutNewPiece('c', 8, new Tower(Gboard, Color.Black));
            PutNewPiece('c', 7, new Tower(Gboard, Color.Black));
            PutNewPiece('d', 7, new Tower(Gboard, Color.Black));
            PutNewPiece('e', 7, new Tower(Gboard, Color.Black));
            PutNewPiece('e', 8, new Tower(Gboard, Color.Black));
            PutNewPiece('d', 8, new King(Gboard, Color.Black));
        }
    }
}