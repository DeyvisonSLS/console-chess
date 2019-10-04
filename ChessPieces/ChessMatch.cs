using System.Collections.Generic;
using System;
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
        public bool Check { get; set; }
        public ChessMatch()
        {
            Gboard = new GameBoard(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            MatchEnded = true;
            PiecesInGame = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            PutPieces();
        }
        public void ResetMatch()
        {
            MatchEnded = false;
        }
        //  Here we make the tests
        public void DoTheMove(Position origin, Position destination)
        {
            Piece capturedPiece = MovePiece(origin, destination);

            //  Current player getting itself in check
            if(Check == true)
            {
                if(InCheck(currentPlayer))
                {
                    UndoMovement(origin, destination, capturedPiece);
                    throw new GameBoardException("You can not let your own king in check, try to save him!");
                }
            }
            else
            {
                if(InCheck(currentPlayer))
                {
                    UndoMovement(origin, destination, capturedPiece);
                    throw new GameBoardException("You must not put your own king in check!");
                }
            }
            
            //  Current player cheking his adversary
            if(InCheck(AdversaryOf(currentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if(CheckMate(AdversaryOf(currentPlayer)))
            {
                MatchEnded = true;
            }
            else
            {
                turn++;
                ChangePlayer();
            }
        }
        public void UndoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            //  Lift up the piece at destination and storing it to use after
            Piece pPicked = Gboard.LiftPiece(destination);
            //
            if(capturedPiece != null)
            {
                //  Give captured piece back to previous location (the pPicked destination)
                Gboard.PutPiece(capturedPiece, destination);
                //  Remove it from the captured pieces set
                CapturedPieces.Remove(capturedPiece);
            }
            //  Using the lifted up piece to place it as it was before
            Gboard.PutPiece(pPicked, origin);
            pPicked.Positioning = origin;
            pPicked.DecrementQtdMoves();
        }
        public Piece MovePiece(Position origin, Position destination)
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

            return pCaptured;
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
        public Color AdversaryOf(Color color)
        {
            if(color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
        private Piece FindKingOf(Color color)
        {
            foreach(Piece x in GetPiecesInGame(color))
            {
                if(x is King)
                {
                    return x;
                }
            }
            return null;
        }
        public bool InCheck(Color color)
        {
            Piece king = FindKingOf(color);
            if(king == null)
            {
                throw new GameBoardException("There is no king of the color " + color + " in the game board.");
            }
            foreach(Piece x in GetPiecesInGame(AdversaryOf(color)))
            {
                if(x.ExistsPossibleMovement())
                {
                    bool[,] mat = x.PossibleMovements();
                    //  If in the positioning of our king, the adversary can move, so we are in check.
                    if(mat[king.Positioning.Line, king.Positioning.Collumn])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CheckMate(Color color)
        {
            if(!InCheck(color))
            {
                return false;
            }
            //  Only enter if the king is in check
            //  InCheck = true
            foreach(Piece x in GetPiecesInGame(color))
            {
                bool[,] mat = x.PossibleMovements();
                for(int i = 0; i < Gboard.Lines; i++)
                {
                    for(int j = 0; j < Gboard.Collumns; j++)
                    {
                        if(mat[i, j])
                        {
                            Position origin = x.Positioning;
                            Position destination = new Position(i, j);
                            //  Make a movement and return a piece for test effects
                            Piece capturedPiece = MovePiece(origin, destination);
                            bool testCheck = InCheck(color);
                            //  Undoing things, we have use it before just for tests
                            UndoMovement(origin, destination, capturedPiece);
                            //  If after test the movemment, the [InCheck = true] becomes [InCheck = false], there is at least one movemment that save the king
                            if(!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            //  If in any scenarios the InCheck have no modification to false (is not in check), then there is no movement to save the king.
            return true;
        }
        public void PutNewPiece(char collumn, int line, Piece piece)
        {
            Gboard.PutPiece(piece, new ChessPosition(collumn, line).ToPosition());
            PiecesInGame.Add(piece);
        }
        //  Here the chessmatch organize the initial pieces set up.
        public void PutPieces()
        {
            PutNewPiece('a', 8, new King(Gboard, Color.Black));
            PutNewPiece('b', 8, new Tower(Gboard, Color.Black));

            PutNewPiece('d', 1, new King(Gboard, Color.White));
            PutNewPiece('c', 1, new Tower(Gboard, Color.White));
            PutNewPiece('h', 7, new Tower(Gboard, Color.White));

            // PutNewPiece('c', 1, new Tower(Gboard, Color.White));
            // PutNewPiece('c', 2, new Tower(Gboard, Color.White));
            // PutNewPiece('d', 2, new Tower(Gboard, Color.White));
            // PutNewPiece('e', 2, new Tower(Gboard, Color.White));
            // PutNewPiece('e', 1, new Tower(Gboard, Color.White));
            // PutNewPiece('d', 1, new King(Gboard, Color.White));

            // PutNewPiece('c', 8, new Tower(Gboard, Color.Black));
            // PutNewPiece('c', 7, new Tower(Gboard, Color.Black));
            // PutNewPiece('d', 7, new Tower(Gboard, Color.Black));
            // PutNewPiece('e', 7, new Tower(Gboard, Color.Black));
            // PutNewPiece('e', 8, new Tower(Gboard, Color.Black));
            // PutNewPiece('d', 8, new King(Gboard, Color.Black));
        }
    }
}