using System;
using System.Globalization;
using Board;

namespace ChessPieces
{
    class Tower : Piece
    {
        public Tower(GameBoard gameBoard, Color color) : base(gameBoard, color)
        {
        }
        private bool CanMove(Position pos)
        {
            Piece p = GameBoard.GetPiece(pos);
            return p == null || p.Color != this.Color;
        }
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[GameBoard.Lines, GameBoard.Collumns];

            Position pos = new Position(0, 0);

            //Upper position
            pos.DefineValues(Positioning.Line - 1, Positioning.Collumn);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
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
            return "T";
        }
    }
}