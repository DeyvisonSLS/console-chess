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
            //  Who initially places the pieces is the game board, so the positioning is null once the piece has been created but not positioned.
            Positioning = null;
            GameBoard = gameBoard;
            Color = color;
            qtdMoves = 0;
        }
        public void IncrementQtdMoves()
        {
            qtdMoves++;
        }
        public void DecrementQtdMoves()
        {
            qtdMoves--;
        }
        //  Sometimes the piece is blocked, it certifies if at least it has one possible position.
        public bool ExistsPossibleMovement()
        {
            bool[,] mat = PossibleMovements();
            for(int i = 0; i < GameBoard.Lines; i++)
            {
                for(int j = 0; j < GameBoard.Collumns; j++)
                {
                    if(mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //  It returns: true = Can move to "pos" -- or -- false = can't move to "pos"
        public bool CanMoveTo (Position pos)
        {
            return PossibleMovements()[pos.Line, pos.Collumn];
        }
        public abstract bool[,] PossibleMovements();
    }
}