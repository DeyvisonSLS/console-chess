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
            //  Instantiate the bidimensional array from the lines and collumns passed in the parameters.
            Pieces = new Piece[lines, collumns];
        }
        public Piece GetPiece(int line, int collumn)
        {
            //  It returns the reference to a particular piece in the game board.
            return Pieces[line, collumn];
        }
        public Piece GetPiece(Position pos)
        {
            //  It returns the reference to a particular piece in the game board.
            return Pieces[pos.Line, pos.Collumn];
        }
        public void PutPiece(Piece p, Position pos)
        {
            //  There is any peace in this position?
            if(ExistsPiece(pos))
            {
                throw new GameBoardException("There is already a piece in this location.");
            }
            Pieces[pos.Line, pos.Collumn] = p;
            //  After being positioned, the piece also receives the informations of its positionings in the game board.
            p.Positioning = pos;
        }
        //  Lift and pick the piece to use after.
        public Piece LiftPiece(Position pos)
        {
            if(GetPiece(pos) == null)
            {
                return null;
            }
            //  It stores a reference of an existing piece in a variable before nullify the matrix position it was, so we don't lose it and it can be returned after.
            Piece aux = GetPiece(pos);
            aux.Positioning = null;
            Pieces[pos.Line, pos.Collumn] = null;
            return aux;
        }
        //  Checking the existence of a piece in a particular position; TRUE = the is a piece.
        public bool ExistsPiece(Position pos)
        {
            ValidatePos(pos);
            return GetPiece(pos) != null;
        }
        //  Checking if the position passed is a game board valid position
        public void ValidatePos(Position pos)
        {
            if(!ValidPosition(pos))
            {
                throw new GameBoardException("Invalid position.");
            }
        }
        //  Method that check the limits of the gameboard compared to the passed position.
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
    }
}