using System;
using Board;

namespace Board
{
    class Piece
    {
        public Position Positioning { get; set; }
        public Color Color { get; protected set; }
        public int qtdMoves { get; protected set; }
        public GameBoard GameBoard { get; protected set; }
        public Piece(Position positioning, GameBoard gameBoard, Color color)
        {
            Positioning = positioning;
            GameBoard = gameBoard;
            Color = color;
            qtdMoves = 0;
        }
    }
}