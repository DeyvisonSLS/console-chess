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
        public Position ToPosition()
        {
            return new Position(8 - Line, Collumn - 'a');
        }
        public override string ToString()
        {
            return "" + Collumn + Line;
        }
    }
}