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