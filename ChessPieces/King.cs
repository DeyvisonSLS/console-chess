using System;
using System.Globalization;
using Board;

namespace ChessPieces
{
    class King : Piece
    {
        private ChessMatch ChessMatch;
        public King(ChessMatch chessmatch, GameBoard gameBoard, Color color) : base(gameBoard, color)
        {
            ChessMatch = chessmatch;
        }
        private bool CanMove(Position pos)
        {
            Piece p = GameBoard.GetPiece(pos);
            return p == null || p.Color != this.Color;
        }
        private bool CanCastling (Position pos)
        {
            Piece p = GameBoard.GetPiece(pos);
            return p != null && p.Color == Color && p.qtdMoves == 0 && p is Rook;
        }
        public override bool[,] PossibleMovements()
        {
            //  It creates the boolean matrix for posteriorly print the background with diferent colors wherever it's true.
            bool[,] mat = new bool[GameBoard.Lines, GameBoard.Collumns];
            //  Pos variable will be altered and reused along the definitions
            Position pos = new Position(0, 0);

            //  Upper position
            //  The vertical position (Line) is equal the current line - 1 (going up), keeping the collumn position in the same place.
            pos.DefineValues(Positioning.Line - 1, Positioning.Collumn);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //NE
            pos.DefineValues(Positioning.Line - 1, Positioning.Collumn + 1);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //Right
            pos.DefineValues(Positioning.Line, Positioning.Collumn + 1);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //SE
            pos.DefineValues(Positioning.Line + 1, Positioning.Collumn + 1);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //Bottom
            pos.DefineValues(Positioning.Line + 1, Positioning.Collumn);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //SO
            pos.DefineValues(Positioning.Line + 1, Positioning.Collumn - 1);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //Left
            pos.DefineValues(Positioning.Line, Positioning.Collumn - 1);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //NO
            pos.DefineValues(Positioning.Line - 1, Positioning.Collumn - 1);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //  #SpecialMovemment : Castling (1)
            if(qtdMoves == 0 && ChessMatch.Check == false)
            {
                Position rookPos = new Position(Positioning.Line, Positioning.Collumn + 3);
                if(CanCastling(rookPos))
                {
                    Position p1 = new Position(Positioning.Line, Positioning.Collumn + 1);
                    Position p2 = new Position(Positioning.Line, Positioning.Collumn + 2);
                    if(GameBoard.GetPiece(p1) == null && GameBoard.GetPiece(p2) == null)
                    {
                        mat[p1.Line, p1.Collumn] = true;
                        mat[p2.Line, p2.Collumn] = true;
                    }
                }
            }
            //  #SpecialMovemment : Castling (2)
            if(qtdMoves == 0 && ChessMatch.Check == false)
            {
                Position rookPos = new Position(Positioning.Line, Positioning.Collumn - 4);
                if(CanCastling(rookPos))
                {
                    Position p1 = new Position(Positioning.Line, Positioning.Collumn - 1);
                    Position p2 = new Position(Positioning.Line, Positioning.Collumn - 2);
                    if(GameBoard.GetPiece(p1) == null && GameBoard.GetPiece(p2) == null)
                    {
                        mat[p1.Line, p1.Collumn] = true;
                        mat[p2.Line, p2.Collumn] = true;
                    }
                }
            }

            //  Return the array of booleans
            return mat;
        }
        //  Return specifc letter depending on the country/language.
        public override string ToString()
        {
            if(CultureInfo.CurrentCulture.Name == "pt-BR")
            {
                return "R";
            }
            else if(CultureInfo.CurrentCulture.Name == "en-US")
            {
                return "K";
            }
            else
            {
                return "erro";
            }
        }
    }
}