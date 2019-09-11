using System;
using Board;
using ChessPieces;

namespace xadrez_console
{
    class Screen
    {
        //  Printa o gameboard sem o auxílio da matriz de movimentos possíveis, sem alterar cor de fundo
        public static void PrintGameBoard(GameBoard gboard)
        {
            for(int i = 0; i < gboard.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for(int j = 0; j < gboard.Collumns; j++)
                {
                    PrintPiece(gboard.GetPiece(i,j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PrintGameBoard(GameBoard gboard, bool[,] possiblePositions)
        {
            ConsoleColor black = Console.BackgroundColor;
            ConsoleColor gray = ConsoleColor.DarkGray;

            //  Print das linhas (vertical)
            for(int i = 0; i < gboard.Lines; i++)
            {
                Console.Write(8 - i + " ");
                //  Print das colunas (horizontal)
                for(int j = 0; j < gboard.Collumns; j++)
                {
                    if(possiblePositions[i,j])
                    {
                        Console.BackgroundColor = gray;
                    }
                    else
                    {
                        Console.BackgroundColor = black;
                    }
                    PrintPiece(gboard.GetPiece(i,j));
                }
                //  Se a última posição da coluna permanecer em gray, as próximas casas terão o fundo gray até que o print das colunas ache uma
                //  casa que tenha o valor false na matriz possiblePositions[] e volte para black
                //
                //  Resetando o fundo para black sempre que sair da impressão das colunas (impressão horizontal)
                Console.BackgroundColor = black;
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            //  Último reset do fundo para black antes de sair da impressão do tabuleiro
            Console.BackgroundColor = black;
        }
        public static ChessPosition ReadChessPosition()
        {

            string s = Console.ReadLine();
            char collumn;
            int line;
            if(s.Length != 2)
            {
                throw new GameBoardException("Looks like you entered a wrong origin.");
            }
            //  Checking the s[0] type; it needs to be a value that can be parsed to char.
            bool isChar = char.TryParse(s[0] + "", out char c);
            if(isChar)
            {
                collumn = c;
            }
            else
            {
                throw new GameBoardException("Looks like you entered a wrong origin.");
            }
            //  Checking s[1] type; it needs to be a value that can be parsed to Int.
            bool isNumeric = int.TryParse(s[1] + "", out int n); //  + "" no lugar de .ToString()
            if(isNumeric)
            {
                line = n;
            }
            else
            {
                throw new GameBoardException("Looks like you entered a wrong origin.");
            }
            return new ChessPosition(collumn, line);
        }
        public static void PrintPiece(Piece piece)
        {
            if(piece == null)
            {
                Console.Write("_ ");
            }
            else
            {
                if(piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}