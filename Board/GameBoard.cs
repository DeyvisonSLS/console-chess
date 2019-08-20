namespace Board
{
    class GameBoard
    {
        public int Lines { get; set; }
        public int Collumns { get; set; }
        private Piece[,] Pieces;
        public GameBoard(int lines, int collumns)
        {
            Lines = lines;
            Collumns = collumns;
            Pieces = new Piece[lines, collumns];
        }
        public Piece GetPiece(int line, int collumn)
        {
            return Pieces[line, collumn];
        }
        public Piece GetPiece(Position pos)
        {
            return Pieces[pos.Line, pos.Collumn];
        }
        public bool ExistsPiece(Position pos)
        {
            ValidatePos(pos);
            return GetPiece(pos) != null;
        }
        public void PutPiece(Piece p, Position pos)
        {
            if(ExistsPiece(pos))
            {
                throw new GameBoardException("There is already a piece in this location.");
            }
            Pieces[pos.Line, pos.Collumn] = p;
            //Após ser posicionada, a peça recebe a informação do seu posicionamento no tabuleiro
            p.Positioning = pos;
        }
        public bool ValidPosition(Position pos)
        {
            if(pos.Line < 0 || pos.Line >= Lines || pos.Collumn < 0 || pos.Collumn >= Collumns)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void ValidatePos(Position pos)
        {
            if(!ValidPosition(pos))
            {
                throw new GameBoardException("Invalid position.");
            }
        }
    }
}