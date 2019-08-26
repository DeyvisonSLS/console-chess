using System;
using System.Globalization;
using Board;

namespace ChessPieces
{
    class King : Piece
    {
        public King(GameBoard gameBoard, Color color) : base(gameBoard, color)
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

            //Upper position based on the actual piece position.
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
            return mat;
        }
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