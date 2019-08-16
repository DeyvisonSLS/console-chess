using System;

namespace Board
{
    class Position
    {
        public int Line { get; set; }
        public int Collumn { get; set; }
        public Position(int line, int collumn)
        {
            Collumn = collumn;
            Line = line;
        }
        public override string ToString()
        {
            return Line
                   + ", "
                   + Collumn;
        }
    }
}