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
        public Piece VulnerableEnPassant { get; private set; }
        public ChessMatch()
        {
            Gboard = new GameBoard(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            MatchEnded = true;
            PiecesInGame = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            VulnerableEnPassant = null;
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

            Piece p = Gboard.GetPiece(destination);
            // #SpecialMovement : En passant
            if(p is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
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

            //  #SpecialMovement : Castling (1)
            if(pPicked is King && destination.Collumn == origin.Collumn + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Collumn + 3);
                Position rookDestination = new Position(origin.Line, origin.Collumn + 1);
                //  It takes the pieces (now it is lifted up) in origin and in the destination.
                Piece p = Gboard.LiftPiece(rookDestination);
                Gboard.PutPiece(p, rookOrigin);
                p.Positioning = rookDestination;
                p.DecrementQtdMoves();
            }
            //  #SpecialMovement : Castling (1)
            if(pPicked is King && destination.Collumn == origin.Collumn - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Collumn - 4);
                Position rookDestination = new Position(origin.Line, origin.Collumn - 1);
                //  It takes the pieces (now it is lifted up) in origin and in the destination.
                Piece pos = Gboard.LiftPiece(rookDestination);
                Gboard.PutPiece(pos, rookOrigin);
                pos.Positioning = rookDestination;
                pos.DecrementQtdMoves();
            }
            //  #SpecialMovement : En passant
            if(pPicked is Pawn)
            {
                if(origin.Collumn != destination.Collumn && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Gboard.LiftPiece(destination);
                    Position posCaptured;
                    if(pPicked.Color == Color.White)
                    {
                        posCaptured = new Position(3, destination.Collumn);
                    }
                    else
                    {
                        posCaptured = new Position(4, destination.Collumn);
                    }
                    Gboard.PutPiece(pawn, posCaptured);
                }
            }
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

            //  #SpecialMovement : Castling (1)
            if(pPicked is King && destination.Collumn == origin.Collumn + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Collumn + 3);
                Position rookDestination = new Position(origin.Line, origin.Collumn + 1);
                //  It takes the pieces (now it is lifted up) in origin and in the destination.
                Piece p = Gboard.LiftPiece(rookOrigin);
                Gboard.PutPiece(p, rookDestination);
                p.Positioning = rookDestination;
                p.IncrementQtdMoves();
            }
            //  #SpecialMovement : Castling (2)
            if(pPicked is King && destination.Collumn == origin.Collumn - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Collumn - 4);
                Position rookDestination = new Position(origin.Line, origin.Collumn - 1);
                //  It takes the pieces (now it is lifted up) in origin and in the destination.
                Piece p = Gboard.LiftPiece(rookOrigin);
                Gboard.PutPiece(p, rookDestination);
                p.Positioning = rookDestination;
                p.IncrementQtdMoves();
            }
            //  #SpecialMovement : En passant
            if(pPicked is Pawn)
            {
                if(origin.Collumn != destination.Collumn && pCaptured == null)
                {
                    Position posPawn;
                    if(pPicked.Color == Color.White)
                    {
                        posPawn = new Position(destination.Line + 1, destination.Collumn);
                    }
                    else
                    {
                        posPawn = new Position(destination.Line - 1, destination.Collumn);
                    }
                    pCaptured = Gboard.LiftPiece(posPawn);
                    if(pCaptured != null)
                    {
                        CapturedPieces.Add(pCaptured);
                    }
                }
            }

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
                            //  If after test the Movement, the [InCheck = true] becomes [InCheck = false], there is at least one Movement that save the king
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
            //  Black pieces
            PutNewPiece('a', 8, new Rook(Gboard, Color.Black));
            PutNewPiece('b', 8, new Knight(Gboard, Color.Black));
            PutNewPiece('c', 8, new Bishop(Gboard, Color.Black));
            PutNewPiece('d', 8, new Queen(Gboard, Color.Black));
            PutNewPiece('e', 8, new King(this, Gboard, Color.Black));
            PutNewPiece('f', 8, new Bishop(Gboard, Color.Black));
            PutNewPiece('g', 8, new Knight(Gboard, Color.Black));
            PutNewPiece('h', 8, new Rook(Gboard, Color.Black));
            //  Black pawn
            PutNewPiece('a', 7, new Pawn(this, Gboard, Color.Black));
            PutNewPiece('b', 7, new Pawn(this, Gboard, Color.Black));
            PutNewPiece('c', 7, new Pawn(this, Gboard, Color.Black));
            PutNewPiece('d', 7, new Pawn(this, Gboard, Color.Black));
            PutNewPiece('e', 7, new Pawn(this, Gboard, Color.Black));
            PutNewPiece('f', 7, new Pawn(this, Gboard, Color.Black));
            PutNewPiece('g', 7, new Pawn(this, Gboard, Color.Black));
            PutNewPiece('h', 7, new Pawn(this, Gboard, Color.Black));
            //
            //  White pieces
            PutNewPiece('a', 1, new Rook(Gboard, Color.White));
            // PutNewPiece('b', 1, new Knight(Gboard, Color.White));
            // PutNewPiece('c', 1, new Bishop(Gboard, Color.White));
            // PutNewPiece('d', 1, new Queen(Gboard, Color.White));
            PutNewPiece('e', 1, new King(this, Gboard, Color.White));
            // PutNewPiece('f', 1, new Bishop(Gboard, Color.White));
            // PutNewPiece('g', 1, new Knight(Gboard, Color.White));
            PutNewPiece('h', 1, new Rook(Gboard, Color.White));
            //  White pawn
            PutNewPiece('a', 2, new Pawn(this, Gboard, Color.White));
            PutNewPiece('b', 2, new Pawn(this, Gboard, Color.White));
            PutNewPiece('c', 2, new Pawn(this, Gboard, Color.White));
            PutNewPiece('d', 2, new Pawn(this, Gboard, Color.White));
            PutNewPiece('e', 2, new Pawn(this, Gboard, Color.White));
            PutNewPiece('f', 2, new Pawn(this, Gboard, Color.White));
            PutNewPiece('g', 2, new Pawn(this, Gboard, Color.White));
            PutNewPiece('h', 2, new Pawn(this, Gboard, Color.White));
        }
    }
}