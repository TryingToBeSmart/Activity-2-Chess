using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Cell
    {
        //cell's location on the grid
        public int RowNumber {  get; set; }
        public int ColumnNumber { get; set; }

        //chess piece on this cell
        public bool CurrentlyOccupied { get; set; }

        //legal move for the chess piece
        public bool LegalNextMove { get; set; }
        public Cell(int r, int c) 
        {
            RowNumber = r;
            ColumnNumber = c;
        }
    }
}
