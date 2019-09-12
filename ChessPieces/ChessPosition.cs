using Board;

namespace ChessPieces
{
    class ChessPosition
    {
        public char Collumn { get; set; }
        public int Line { get; set; }
        public ChessPosition(char collumn, int line)
        {
            Collumn = collumn;
            Line = line;
        }
        //  Convert game board positioning to array positioning
        public Position ToPosition()
        {
            //  8 - Line. It Inverts the line count from bottom to top.
            //  char 'a' = 1, 'b' = 2 and so on.
            //  The Collumn (for example: 2 - that is a value passed on constructor) -'a' or -1 = array[x, 1], the second collumn of the array
            return new Position(8 - Line, Collumn - 'a');
        }
        public override string ToString()
        {
            return "" + Collumn + Line;
        }
    }
}