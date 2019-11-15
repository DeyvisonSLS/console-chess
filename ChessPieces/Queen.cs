using System;
using System.Globalization;
using Board;

namespace ChessPieces
{
    class Queen : Piece
    {
        public Queen(GameBoard gameBoard, Color color) : base(gameBoard, color)
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

            //  Top
            pos.DefineValues(Positioning.Line - 1, Positioning.Collumn);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Line--;
            }
            //  NE
            pos.DefineValues(Positioning.Line - 1, Positioning.Collumn + 1);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line - 1, pos.Collumn + 1);
            }
            //  Right
            pos.DefineValues(Positioning.Line, Positioning.Collumn + 1);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Collumn++;
            }
            //  SE
            pos.DefineValues(Positioning.Line + 1, Positioning.Collumn + 1);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line + 1, pos.Collumn + 1);
            }
            //  Bottom
            pos.DefineValues(Positioning.Line + 1, Positioning.Collumn);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Line++;
            }
            //  SW
            pos.DefineValues(Positioning.Line + 1, Positioning.Collumn - 1);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line + 1, pos.Collumn - 1);
            }
            // Left
            pos.DefineValues(Positioning.Line, Positioning.Collumn - 1);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Collumn--;
            }
            //  NW
            pos.DefineValues(Positioning.Line - 1, Positioning.Collumn - 1);
            while(GameBoard.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collumn] = true;
                if(GameBoard.GetPiece(pos) != null && GameBoard.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line - 1, pos.Collumn - 1);
            }

            //  Return the array of booleans
            return mat;
        }
        //  Return specifc letter depending on the country/language.
        public override string ToString()
        {
            if(CultureInfo.CurrentCulture.Name == "pt-BR")
            {
                return "D";
            }
            else if(CultureInfo.CurrentCulture.Name == "en-US")
            {
                return "Q";
            }
            else
            {
                return "erro";
            }
        }
    }
}