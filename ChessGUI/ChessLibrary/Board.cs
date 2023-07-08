using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Board
    {
        //Square size
        public int Size { get; set; }

        //2d array of Cell objects
        public Cell[,] Grid;

        public Board(int size)
        {
            this.Size = size;

            //initialize array of cells
            Grid = new Cell[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j] = new Cell(i, j);
                }
            }
        }

        public void MarkNextLegalMoves(Cell currentCell, string chessPiece)
        {
            //clear LegalMoves from previous turn
            for (int r = 0; r < Size; r++)
            {
                for (int i = 0; i < Size; i++)
                {
                    Grid[r, i].LegalNextMove = false;
                }
            }

            //find legal moves and mark the squares
            switch (chessPiece)
            {
                case "Knight":
                    KnightMoves(currentCell);
                    break;

                case "King":
                    KingMoves(currentCell);
                    break;

                case "Rook":
                    RookMoves(currentCell);
                    break;

                case "Bishop":
                    BishopMoves(currentCell);
                    break;

                case "Queen":
                    //moves like Rook and Bishop combined
                    RookMoves(currentCell);
                    BishopMoves(currentCell);
                    break;

            }

            //moves in L shape
            void KnightMoves(Cell currentCell)
            {
                try { Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber - 1].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber + 1].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 2].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 2].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber + 1].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber - 1].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 2].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 2].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }
            }


            //moves to any surrounding cell
            void KingMoves(Cell currentCell)
            {
                try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 1].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 1].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }

                try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].LegalNextMove = true; }
                catch (IndexOutOfRangeException) { }
            }

            //moves horizontal or vertical
            void RookMoves(Cell currentCell)
            {
                //list vertical moves
                for (int row = 0; row < Size; row++)
                {
                    if (row != currentCell.RowNumber)
                    {
                        Grid[row, currentCell.ColumnNumber].LegalNextMove = true;
                    }
                }

                //list horizontal moves
                for (int col = 0; col < Size; col++)
                {
                    if (col != currentCell.ColumnNumber)
                    {
                        Grid[currentCell.RowNumber, col].LegalNextMove = true;
                    }
                }
            }

            //moves diagonal
            void BishopMoves(Cell currentCell)
            {

                for (int i = 1; i < Size; i++)
                {
                    //down and right
                    if (currentCell.RowNumber + i < Size && currentCell.ColumnNumber + i < Size)//check out of bounds
                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].LegalNextMove = true;

                    //up and left
                    if (currentCell.RowNumber - i >= 0 && currentCell.ColumnNumber - i >= 0)//check out of bounds
                        Grid[currentCell.RowNumber - i, currentCell.ColumnNumber - i].LegalNextMove = true;

                    //up and right
                    if (currentCell.RowNumber - i >= 0 && currentCell.ColumnNumber + i < Size)//check out of bounds
                        Grid[currentCell.RowNumber - i, currentCell.ColumnNumber + i].LegalNextMove = true;

                    //down and left
                    if (currentCell.RowNumber + i < Size && currentCell.ColumnNumber - i >= 0)//check out of bounds
                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber - i].LegalNextMove = true;
                }
            }
        }
    }
}
