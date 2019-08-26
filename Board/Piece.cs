using System;
using Board;

namespace Board
{
    abstract class Piece
    {
        public Position Positioning { get; set; }
        public Color Color { get; protected set; }
        public int qtdMoves { get; protected set; }
        public GameBoard GameBoard { get; protected set; }
        public Piece(GameBoard gameBoard, Color color)
        {
            //quem coloca a posição da peça é o tabuleiro, sendo assim o positioning é nulo no momento de criação
            Positioning = null;
            GameBoard = gameBoard;
            Color = color;
            qtdMoves = 0;
        }
        public void IncrementQtdMoves()
        {
            qtdMoves++;
        }
        public abstract bool[,] PossibleMovements();
    }
}