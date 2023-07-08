
using ChessLibrary;
using System.Drawing;

namespace ConsoleChess
{
    class Program
    {
        static Board board = new Board(8);

        static void Main(string[] args)
        {
            bool continueLoop = true;
            string piece;
            //show the empty chessboard
            PrintGrid(board);

            while (continueLoop)
            {
                //get piece
                while (true)
                {
                    Console.WriteLine("What piece?");
                    piece = Console.ReadLine();
                    if (piece == "Knight" || piece == "King" || piece == "Rook" || piece == "Bishop" || piece == "Queen") break;
                }

                //get the location of the chess piece
                Cell currentLocation = SetCurrentCell();

                //calculate and mark the cells where legal moves are possible
                board.MarkNextLegalMoves(currentLocation, piece);

                //show the chessboard and use "." for an empty square, "X" for the piece location
                //and "+" for a possible legal move.
                PrintGrid(board);

                //ask for another round
                Console.WriteLine("Again? Y");
                if (Console.ReadLine() != "Y") continueLoop = false; 
            }
        }

        //get the location of the chess piece
        private static Cell SetCurrentCell()
        {
            int currentRow = -1;
            int currentCol = -1;

            while (currentRow < 0 || currentRow >= board.Size)
            {
                Console.WriteLine("Current Row: (0 - {0})", board.Size - 1);
                if (int.TryParse(Console.ReadLine(), out currentRow));
                else currentRow = -1;
            }

            while (currentCol < 0 || currentCol >= board.Size)
            {
                Console.WriteLine("Current Col: (0 - {0})", board.Size - 1);
                if (int.TryParse(Console.ReadLine(), out currentCol));
                else currentCol = -1;
            }
            
            //reset cells
            foreach (var cell in board.Grid)
            {
                cell.CurrentlyOccupied = false;
            }

            //set current cell as occupied
            board.Grid[currentRow, currentCol].CurrentlyOccupied = true;
            return board.Grid[currentRow, currentCol];
        }

        //show the chessboard and use "." for an empty square, "X" for the piece location
        //and "+" for a possible legal move.
        private static void PrintGrid(Board board)
        {
            // Print column numbers
            Console.Write("   ");
            for (int col = 0; col < board.Size; col++)
            {
                Console.Write(" " + col + "  ");
            }
            Console.WriteLine();

            // Print top border line
            Console.WriteLine("  +" + string.Join("+", Enumerable.Repeat("---", board.Size)) + "+");

            for (int row = 0; row < board.Size; row++)
            {
                Console.Write(row + " |"); // Print row number and left border
                for (int col = 0; col < board.Size; col++)
                {
                    if (board.Grid[row, col].CurrentlyOccupied)
                    {
                        Console.Write(" X |");
                    }
                    else if (board.Grid[row, col].LegalNextMove)
                    {
                        Console.Write(" + |");
                    }
                    else
                    {
                        Console.Write("   |");
                    }
                }
                //next row
                Console.WriteLine();
                // Print inner horizontal line
                Console.WriteLine("  +" + string.Join("+", Enumerable.Repeat("---", board.Size)) + "+");
            }
        }
    }
}