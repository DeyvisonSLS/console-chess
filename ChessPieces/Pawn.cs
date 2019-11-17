using System;
using System.Globalization;
using Board;

namespace ChessPieces
{
    class Pawn : Piece
    {
        public Pawn(GameBoard gameBoard, Color color) : base(gameBoard, color)
        {
        }
        // private bool CanMove(Position pos)
        // {
        //     Piece p = GameBoard.GetPiece(pos);
        //     return p == null || p.Color != this.Color;
        // }
        private bool isEmpty (Position pos)
        {
            return GameBoard.GetPiece(pos) == null;
        }
        private bool ThereIsEnemy (Position pos)
        {
            Piece p = GameBoard.GetPiece(pos);
            return p != null && p.Color != this.Color;
        }
        public override bool[,] PossibleMovements()
        {
            //  It creates the boolean matrix for posteriorly print the background with diferent colors wherever it's true.
            bool[,] mat = new bool[GameBoard.Lines, GameBoard.Collumns];
            //  Pos variable will be altered and reused along the definitions
            Position pos = new Position(0, 0);

            if(Color == Color.White)
            {
                //  Normal movemment
                //  Top (1)
                pos.DefineValues(Positioning.Line - 1, Positioning.Collumn);
                if(GameBoard.ValidPosition(pos) && isEmpty(pos))
                {
                    mat[pos.Line, pos.Collumn] = true;
                }
                //  Top (2)
                pos.DefineValues(Positioning.Line - 2, Positioning.Collumn);
                if(GameBoard.ValidPosition(pos) && isEmpty(pos) && qtdMoves == 0)
                {
                    mat[pos.Line, pos.Collumn] = true;
                }
                //  Test to attack
                pos.DefineValues(Positioning.Line - 1, Positioning.Collumn - 1);
                if(GameBoard.ValidPosition(pos) && GameBoard.GetPiece(pos) != null && ThereIsEnemy(pos))
                {
                    mat[pos.Line, pos.Collumn] = true;
                }
                pos.DefineValues(Positioning.Line - 1, Positioning.Collumn + 1);
                if(GameBoard.ValidPosition(pos) && GameBoard.GetPiece(pos) != null && ThereIsEnemy(pos))
                {
                    mat[pos.Line, pos.Collumn] = true;
                }
            }
            else
            {
                //  Normal movemment
                //  Top (1)
                pos.DefineValues(Positioning.Line + 1, Positioning.Collumn);
                if(GameBoard.ValidPosition(pos) && isEmpty(pos))
                {
                    mat[pos.Line, pos.Collumn] = true;
                }
                //  Top (2)
                pos.DefineValues(Positioning.Line + 2, Positioning.Collumn);
                if(GameBoard.ValidPosition(pos) && isEmpty(pos) && qtdMoves == 0)
                {
                    mat[pos.Line, pos.Collumn] = true;
                }
                //  Test to attack
                pos.DefineValues(Positioning.Line + 1, Positioning.Collumn - 1);
                if(GameBoard.ValidPosition(pos) && GameBoard.GetPiece(pos) != null && ThereIsEnemy(pos))
                {
                    mat[pos.Line, pos.Collumn] = true;
                }
                pos.DefineValues(Positioning.Line + 1, Positioning.Collumn + 1);
                if(GameBoard.ValidPosition(pos) && GameBoard.GetPiece(pos) != null && ThereIsEnemy(pos))
                {
                    mat[pos.Line, pos.Collumn] = true;
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
                return "P";
            }
            else if(CultureInfo.CurrentCulture.Name == "en-US")
            {
                return "P";
            }
            else
            {
                return "erro";
            }
        }
    }
}