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
        public override string ToString()
        {
            return "T";
        }
    }
}