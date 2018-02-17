using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //Important for buttons

namespace setupDemo
{
    class NewMove
    {

        public NewMove(ref bool turn, Button selectButton)
        {
            if (turn)
            {
                selectButton.Text = "X";
                selectButton.Enabled = false;
                turn = false;
            }
            else
            {
                selectButton.Text = "O";
                selectButton.Enabled = false;
                turn = true;
            }
        }
    }
}
