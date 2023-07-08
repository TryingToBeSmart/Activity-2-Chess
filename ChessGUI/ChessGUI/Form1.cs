using ChessLibrary;
using System.Security.Policy;
using System.Windows.Forms;

namespace ChessGUI
{
    public partial class Form1 : Form
    {
        static readonly Board board = new Board(8);
        public Button[,] buttonGrid = new Button[board.Size, board.Size];
        static private string selectedPiece;
        public Form1()
        {
            InitializeComponent();
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            //fill panel1 with buttons
            int buttonSize = panel1.Width / board.Size;// calculate the width of each button
            panel1.Height = panel1.Width; // make square grid

            //loop to create adn place buttons in the panel
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    buttonGrid[row, col] = new Button();
                    //square buttons
                    buttonGrid[row, col].Width = buttonSize;
                    buttonGrid[row, col].Height = buttonSize;

                    buttonGrid[row, col].Click += Grid_Button_Click;// add click event to each button
                    panel1.Controls.Add(buttonGrid[row, col]);// place button on panel
                    buttonGrid[row, col].Location = new Point(buttonSize * row, buttonSize * col);// position buttons

                    // Use Tag attribute to hold row, col numbers in a string
                    buttonGrid[row, col].Tag = row.ToString() + "|" + col.ToString();
                }
            }
        }

        private void Grid_Button_Click(object? sender, EventArgs e)
        {
            //The method has an important
            //parameter called object sender. This
            //refers to the control that caused this
            //method to be called. We can refer to
            //this parameter later as (sender as
            //Button)

            //make sure a piece is selected
            if (selectedPiece == null || selectedPiece == "")
            {
                MessageBox.Show("Select a chess piece");
                return;
            }

            else
            {
                // get the row and col number of the button just clicked
                string[] stringArray = (sender as Button).Tag.ToString().Split('|');
                int row = int.Parse(stringArray[0]);
                int col = int.Parse(stringArray[1]);

                // reset background color of all buttons to default color
                ResetCells();

                // label all legal moves
                Cell currentCell = board.Grid[row, col];
                currentCell.CurrentlyOccupied = true;
                board.MarkNextLegalMoves(currentCell, selectedPiece);
                updateButtonLabels();

                // set the clicked button background color
                (sender as Button).BackColor = Color.Cornsilk;
            }
        }

        private void updateButtonLabels()
        {
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    buttonGrid[row, col].Text = "";
                    if (board.Grid[row, col].CurrentlyOccupied) buttonGrid[row, col].Text = selectedPiece;
                    if (board.Grid[row, col].LegalNextMove) buttonGrid[row, col].Text = "Legal";
                }
            }
        }

        //change piece to selected item
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if new piece selected
            if (comboBox1.GetItemText(comboBox1.SelectedItem) != selectedPiece)
            {
                ResetCells();
                selectedPiece = comboBox1.GetItemText(comboBox1.SelectedItem);
            }
            
        }

        //reset all the cells
        private void ResetCells()
        {
            foreach (var cell in board.Grid)
            {
                cell.CurrentlyOccupied = false;
                buttonGrid[cell.RowNumber, cell.ColumnNumber].Text = "";
                buttonGrid[cell.RowNumber, cell.ColumnNumber].BackColor = default(Color);
            }
        }
    }
}