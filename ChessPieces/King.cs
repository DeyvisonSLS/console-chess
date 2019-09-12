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