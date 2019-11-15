using System;
using System.Globalization;
using Board;

namespace ChessPieces
{
    class Rook : Piece
    {
        public Rook(GameBoard gameBoard, Color color) : base(gameBoard, color)
        {
        }
        private bool CanMove(Position pos)
        {
            Piece p = GameBoard.GetPiece(pos);
            //  It returns true if the posiitoning (pos) reported value is null in the game board matrix.
            return p == null || p.Color != this.Color;
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
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                //  If the position is valid and the piece can move (the place has no piece or your own pieces), in the position write true.
                mat[pos.Line, pos.Collumn] = true;
                //  If there is a piece in this current position and it's a oponent, break the while, the tower just comes here.
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Line--;
            }
            //Right
            pos.DefineValues(Positioning.Line, Positioning.Collumn + 1);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                //  If there is a piece in this current position and it's a oponent, break the while, the tower just comes here.
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Collumn++;
            }
            //Bottom
            pos.DefineValues(Positioning.Line + 1, Positioning.Collumn);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                //  If there is a piece in this current position and it's a oponent, break the while, the tower just comes here.
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Line++;
            }
            //Left
            pos.DefineValues(Positioning.Line, Positioning.Collumn - 1);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                //  If there is a piece in this current position and it's a oponent, break the while, the tower just comes here.
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Collumn--;
            }
            return mat;
        }
        public override string ToString()
        {
            if(CultureInfo.CurrentCulture.Name == "pt-BR")
            {
                return "T";
            }
            else if(CultureInfo.CurrentCulture.Name == "en-US")
            {
                return "R";
            }
            else
            {
                return "erro";
            }
        }
    }
}