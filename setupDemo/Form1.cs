using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace setupDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool currentTurn, fourCorner, isCaps = false;       
        int turnCount, Xrow1, Xrow2, Xrow3, XcolA, XcolB, XcolC, Xdiagonal1, Xdiagonal2, 
                       Orow1, Orow2, Orow3, OcolA, OcolB, OcolC, Odiagonal1, Odiagonal2;
        int[] checkX, checkO;
        string[] checkXName, checkOName;
        Button[] corners, nonCorners;
        Dictionary<string, Button> buttonGroup = new Dictionary<string, Button>();

        private void Form1_Load(object sender, EventArgs e)
        {//TODO:look into the addition of buttons to the dictionary

            TickTackToeScreen ts = new TickTackToeScreen();
            this.Controls.Add(ts);
            
            //buttonGroup.Add("a1", a1Button);
            //buttonGroup.Add("a2", a2Button);
            //buttonGroup.Add("a3", a3Button);
            //buttonGroup.Add("b1", b1Button);
            //buttonGroup.Add("b2", b2Button);
            //buttonGroup.Add("b3", b3Button);
            //buttonGroup.Add("c1", c1Button);
            //buttonGroup.Add("c2", c2Button);
            //buttonGroup.Add("c3", c3Button);
            //corners = new[] { a1Button, a3Button, c1Button, c3Button };
            //nonCorners = new[] { b1Button, c2Button, b3Button, a2Button };
            //checkXName = new string[] { "Xrow1", "Xrow2", "Xrow3", "XcolA", "XcolB", "XcolC", "Xdiagonal1", "Xdiagonal2" };
            //checkOName = new string[] { "Orow1", "Orow2", "Orow3", "OcolA", "OcolB", "OcolC", "Odiagonal1", "Odiagonal2" };
            //Reset();
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            menuStrip.Show(menuButton, 0, menuButton.Height);
        }

        private void connect4Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("dsa");
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            double val1 = Convert.ToDouble(val1Box.Text);
            double val2 = Convert.ToDouble(val2Box.Text);
            double product = Multiply(val1, val2);

            productBox.Text = product.ToString();
        }
        /// <summary>
        /// Multiplies two numbers and returns the result
        /// </summary>
        /// <param name="x">The first double value to be multiplied</param>
        /// <param name="y">The second double value to be multiplied</param>
        /// <returns>Double returned of operator x * y</returns>
        public double Multiply(double x, double y)
        {
            double result = x * y;
            return result;
        }

        public void NewMove(ref bool turn, Button selectButton)
        {
            if (playerVPlayerBox.Checked == true || playerVCompBox.Checked == true)
            {
                AddVal(selectButton, turn, checkX, checkO);
                ChangeButton(selectButton, ref turn);
                turnCount++;
                EndCheck(checkX, checkO);

                if (playerVCompBox.Checked == true)
                {
                    CompMove(ref turn, selectButton);
                }
            }
        }

        public void AddVal(Button selectButton, bool turn, int[] checkX, int[] checkO)
        {
            int[] playerCheck;

            if (turn == false)
            {
                playerCheck = checkX;
            }
            else
            {
                playerCheck = checkO;
            }
            if (selectButton == a1Button || selectButton == b1Button || selectButton == c1Button)
            {
                playerCheck[0]++;
            }
            else if (selectButton == a2Button || selectButton == b2Button || selectButton == c2Button)
            {
                playerCheck[1]++;
            }
            else if (selectButton == a3Button || selectButton == b3Button || selectButton == c3Button)
            {
                playerCheck[2]++;
            }
            if (selectButton == a1Button || selectButton == a2Button || selectButton == a3Button)
            {
                playerCheck[3]++;
            }
            else if (selectButton == b1Button || selectButton == b2Button || selectButton == b3Button)
            {
                playerCheck[4]++;
            }
            else if (selectButton == c1Button || selectButton == c2Button || selectButton == c3Button)
            {
                playerCheck[5]++;
            }
            if (selectButton == a1Button || selectButton == b2Button || selectButton == c3Button)
            {
                playerCheck[6]++;
            }
            if (selectButton == a3Button || selectButton == b2Button || selectButton == c1Button)
            {
                playerCheck[7]++;
            }
            if(turn ==  false)
            {
                checkX = playerCheck;
            }
            else
            {
                checkO = playerCheck;
            }
        }

        public void ChangeButton(Button selectButton, ref bool turn)
        {
            if (turn)
            {
                selectButton.Text = "O";
                selectButton.Enabled = false;
                turn = false;
            }
            else
            {
                selectButton.Text = "X";
                selectButton.Enabled = false;
                turn = true;
            }
        }

        public void FindButton(string endString, ref bool turn)
        {
            Char c;
            string endVal = endString.Substring(endString.Length - 1);

            for(int i = 1; i <= 3; i++)
            {
                if(endString.Contains("w"))//it is a row
                {
                    c = (Char)((isCaps ? 65 : 97) + (i - 1)); //converts the current i value to a letter, ex. 1 = a, 2 = b ...
                    if (buttonGroup[c.ToString() + endVal].Enabled == true) //uses the value above and the specified row number(endVal) to search the dictionary for the corresponding button. If enable the button is the only left in the row that can be chosen.
                    {
                        AddVal(buttonGroup[c.ToString() + endVal], turn, checkX, checkO);
                        ChangeButton(buttonGroup[c.ToString() + endVal], ref turn);
                        break;
                    }
                }
                else if (endString.Contains("al"))//it is a diagonal
                {
                    c = (Char)((isCaps ? 65 : 97) + (i - 1));

                    if (endString.Last().ToString() == "1")//diagonal 1
                    {
                        endVal = i.ToString();
                        if (buttonGroup[c.ToString() + endVal].Enabled == true) // This goes from a1 to c3
                        {
                            AddVal(buttonGroup[c.ToString() + endVal], turn, checkX, checkO);
                            ChangeButton(buttonGroup[c.ToString() + endVal], ref turn);
                            break;
                        }
                    }
                    else//diagonal 2
                    {
                        endVal = (4 - i).ToString();
                        if (buttonGroup[c.ToString() + endVal].Enabled == true) // This goes from a3 to c1
                        {
                            AddVal(buttonGroup[c.ToString() + endVal], turn, checkX, checkO);
                            ChangeButton(buttonGroup[c.ToString() + endVal], ref turn);
                            break;
                        }
                    }
                }
                else//it is a column
                {
                    if (buttonGroup[endVal.ToLower() + i.ToString()].Enabled == true)
                    {
                        AddVal(buttonGroup[endVal.ToLower() + i.ToString()], turn, checkX, checkO);
                        ChangeButton(buttonGroup[endVal.ToLower() + i.ToString()], ref turn);
                    }
                }
            }
        }

        public void CompMove(ref bool turn, Button selectButton)
        {
            if (turnCount == 1)
            {
                if (a1Button.Enabled == false || c1Button.Enabled == false || a3Button.Enabled == false || c3Button.Enabled == false)
                {
                    fourCorner = true;//player chose one of the corners, important for counter stategy
                }
                if (b2Button.Enabled == true)//Opening move
                {
                    AddVal(b2Button, turn, checkX, checkO);
                    ChangeButton(b2Button, ref turn);
                }
                else
                {
                    AddVal(a1Button, turn, checkX, checkO);
                    ChangeButton(a1Button, ref turn);
                }
            }
            else
            {
                bool noThreat = false, compWin = false;
                string last3Char;

                for (int i = 0; i < checkX.Length; i++)//checks for each row/column/diagonal 
                {
                    if (checkO[i] == 2 && checkX[i] == 0)//checks all row/column for O 
                    {
                        last3Char = checkOName[i].Substring(checkOName[i].Length - 3);//takes part of the row/col/diagonal, starting from the third last letter and ehding on the last letter
                        FindButton(last3Char, ref turn);
                        compWin = true;
                        break;
                    }
                }
                if (compWin == false)
                {
                    for (int i = 0; i < checkX.Length; i++)//checks for each row/column/diagonal 
                    {
                        if (checkX[i] == 2 && checkO[i] == 0)//checks all row/column for 2 X  
                        {
                            last3Char = checkXName[i].Substring(checkXName[i].Length - 3);//takes part of the row/col/diagonal, starting from the third last letter and ehding on the last letter
                            FindButton(last3Char, ref turn);
                            noThreat = false;
                            break;
                        }
                        noThreat = true;
                    }
                    if (noThreat)//TODO:Work on computer going for the win, 2x2 square glitch
                    {
                        if (fourCorner)
                        {
                            CompPlayStyle(nonCorners, corners, ref turn);
                        }
                        else
                        {
                            CompPlayStyle(corners, nonCorners, ref turn);
                        }
                    }
                }
            }
            turnCount++;
            EndCheck(checkX, checkO);
        }

        public void CompPlayStyle(Button[] style1, Button[] style2, ref bool turn)
        {
            bool skip = false;
            for (int i = 0; i < style1.Length; i++)
            {
                if (style1[i].Enabled == true)
                {
                    AddVal(style1[i], turn, checkX, checkO);
                    ChangeButton(style1[i], ref turn);
                    skip = true;
                    break;
                }
            }
            if (skip == false)
            {
                for (int i = 0; i < style2.Length; i++)
                {
                    if (style2[i].Enabled == true)
                    {
                        AddVal(style2[i], turn, checkX, checkO);
                        ChangeButton(style2[i], ref turn);
                        break;
                    }
                }
            }
        }

        public void EndCheck(int[] checkX3, int[] checkO3)
        {
            int[] checkPlayer = checkX3;
            string player = "X";
            for (int i = 0; i < 2; i++)
            {
                if (i == 1)
                {
                    checkPlayer = checkO3;
                    player = "O";
                }
                if (checkPlayer.Contains(3))
                {
                    MessageBox.Show("Game Over. The player using : " + player + " won the game.");
                    Reset();
                    break;
                }
                if (turnCount % 9 == 0)
                {
                    MessageBox.Show("Game Over. It was a draw.");
                    Reset();
                    break;
                }
            }
        }

        public void Reset()
        {
            turnCount = Xrow1 = Xrow2 = Xrow3 = XcolA = XcolB = XcolC = Xdiagonal1 = Xdiagonal2 = Orow1 = Orow2 = Orow3 = OcolA = OcolB = OcolC = Odiagonal1 = Odiagonal2 = 0;
            currentTurn = fourCorner = playerVPlayerBox.Checked = playerVCompBox.Checked = false;
            a1Button.Text = a2Button.Text = a3Button.Text = b1Button.Text = b2Button.Text = b3Button.Text = c1Button.Text = c2Button.Text = c3Button.Text = "";
            a1Button.Enabled = a2Button.Enabled = a3Button.Enabled = b1Button.Enabled = b2Button.Enabled = b3Button.Enabled = c1Button.Enabled = c2Button.Enabled = c3Button.Enabled = playerVPlayerBox.Enabled = playerVCompBox.Enabled = true;
            checkX = new int[] { Xrow1, Xrow2, Xrow3, XcolA, XcolB, XcolC, Xdiagonal1, Xdiagonal2 };
            checkO = new int[] { Orow1, Orow2, Orow3, OcolA, OcolB, OcolC, Odiagonal1, Odiagonal2 };
        }

        private void a1Button_Click(object sender, EventArgs e)
        {
            NewMove(ref currentTurn, a1Button);
            multiplyButton.Focus();
        }

        private void b1Button_Click(object sender, EventArgs e)
        {
            NewMove(ref currentTurn, b1Button);
            multiplyButton.Focus();
        }

        private void c1Button_Click(object sender, EventArgs e)
        {
            NewMove(ref currentTurn, c1Button);
            multiplyButton.Focus();
        }

        private void a2Button_Click(object sender, EventArgs e)
        {
            NewMove(ref currentTurn, a2Button);
            multiplyButton.Focus();
        }

        private void b2Button_Click(object sender, EventArgs e)
        {
            NewMove(ref currentTurn, b2Button);
            multiplyButton.Focus();
        }

        private void c2Button_Click(object sender, EventArgs e)
        {
            NewMove(ref currentTurn, c2Button);
            multiplyButton.Focus();
        }

        private void a3Button_Click(object sender, EventArgs e)
        {
            NewMove(ref currentTurn, a3Button);
            multiplyButton.Focus();
        }

        private void b3Button_Click(object sender, EventArgs e)
        {
            NewMove(ref currentTurn, b3Button);
            multiplyButton.Focus();
        }

        private void c3Button_Click(object sender, EventArgs e)
        {
            NewMove(ref currentTurn, c3Button);
            multiplyButton.Focus();
        }

        private void playerVPlayerBox_CheckedChanged(object sender, EventArgs e)
        {
            playerVCompBox.Checked = playerVPlayerBox.Enabled = playerVCompBox.Enabled = false;
        }

        private void playerVCompBox_CheckedChanged(object sender, EventArgs e)
        {
            playerVPlayerBox.Checked = playerVPlayerBox.Enabled = playerVCompBox.Enabled = false;
        }
    }
}