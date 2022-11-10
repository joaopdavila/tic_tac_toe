using BoardLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace TicTacGUI
{
    public partial class Form1 : Form
    {

        Board game = new Board();
        Button[] buttons = new Button[9];
        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            game = new Board();

            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;
            buttons[6] = button7;
            buttons[7] = button8;
            buttons[8] = button9;

            // add a common click event to each button

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Click += handleButtonClick;
                buttons[i].Tag = i;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateBoard();
        }

        private void updateBoard()
        {
            // assign an x or o to the text of each button based on the values in Board
            for (int i = 0; i < game.Grid.Length; i++)
            {
                if (game.Grid[i] == 0)
                {
                    buttons[i].Text = "";
                    buttons[i].Enabled = true;
                    buttons[i].ForeColor = Color.Black;
                }
                else if (game.Grid[i] == 1)
                {
                    buttons[i].Text = "X";
                    buttons[i].ForeColor = Color.Red;
                    buttons[i].Enabled = false;
                }
                else if (game.Grid[i] == 2)
                {
                    buttons[i].Text = "O";
                    buttons[i].ForeColor = Color.Blue;
                    buttons[i].Enabled = false;
                }

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            game = new Board();
            enableAllBoards();

        }

        private void enableAllBoards()
        {
            foreach (var item in buttons)
            {
                item.Enabled = true;
            }
            updateBoard();
        }

        private void handleButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            int gameSquareNumber = (int)clickedButton.Tag;

            game.Grid[gameSquareNumber] = 1;

            updateBoard();

            if (game.isBoardFull())
            {
                MessageBox.Show("Tie. Game Over");
                disableAllButtons();
            }
            else if (game.checkForWinner() == 1)
            {
                MessageBox.Show("Human wins!");
                disableAllButtons();
            }

            computerChoose();
        }

        private void disableAllButtons()
        {
            foreach (var item in buttons)
            {
                item.Enabled = false;
            }
        }

        private void computerChoose()
        {
            // computer pickas a random number. update game.Grid to reflect the choice
            int computerTurn = rand.Next(9);
            while (computerTurn == -1 || game.Grid[computerTurn] != 0)
            {
                computerTurn = rand.Next(8);
                Console.WriteLine("Computer chooses " + computerTurn);
            }
            game.Grid[computerTurn] = 2;
            updateBoard();

            if (game.isBoardFull())
            {
                MessageBox.Show("Tie. Game Over");
                disableAllButtons();
            }
            else if (game.checkForWinner() == 2)
            {
                MessageBox.Show("Computer wins!");
                disableAllButtons();
            }
        }
    }
}
