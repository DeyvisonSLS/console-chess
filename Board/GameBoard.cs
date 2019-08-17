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
    }
}