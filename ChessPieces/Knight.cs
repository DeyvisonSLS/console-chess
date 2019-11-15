using System;
using System.Globalization;
using Board;

namespace ChessPieces
{
    class Knight : Piece
    {
        public Knight(GameBoard gameBoard, Color color) : base(gameBoard, color)
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

            //  NE (1)
            pos.DefineValues(Positioning.Line - 2, Positioning.Collumn + 1);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //  NE (2)
            pos.DefineValues(Positioning.Line - 1, Positioning.Collumn + 2);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //
            //  SE (1)
            pos.DefineValues(Positioning.Line + 1, Positioning.Collumn + 2);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //  SE (2)
            pos.DefineValues(Positioning.Line + 2, Positioning.Collumn + 1);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //
            //  SW (1)
            pos.DefineValues(Positioning.Line + 1, Positioning.Collumn - 2);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //  SW (2)
            pos.DefineValues(Positioning.Line + 2, Positioning.Collumn - 1);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //
            //  NW (1)
            pos.DefineValues(Positioning.Line - 2, Positioning.Collumn - 1);
            if(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
            }
            //  NW (2)
            pos.DefineValues(Positioning.Line - 1, Positioning.Collumn - 2);
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
                return "C";
            }
            else if(CultureInfo.CurrentCulture.Name == "en-US")
            {
                return "N";
            }
            else
            {
                return "erro";
            }
        }
    }
}