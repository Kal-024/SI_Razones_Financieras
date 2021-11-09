using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FITD_Demo.Method
{
    public class ValidateTextBox
    {

        private bool IsNumber(string inputvalue)
        {
            Regex isnumber = new Regex(@"^-?[0-9]+(\.?[0-9]+)?$");
            return isnumber.IsMatch(inputvalue);
        }

        public void ValidateForKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back

               || e.KeyChar == (char)Keys.Delete

               || e.KeyChar == (char)Keys.Left

               || e.KeyChar == (char)Keys.Right

               || IsNumber(e.KeyChar.ToString())

               )
            {

                KryptonTextBox tbtmp = sender as KryptonTextBox;
                if (e.KeyChar == '.' && tbtmp.Text.IndexOf('.') > 0)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        public void ValidateForTextChanged(object sender, EventArgs e)
        {
            KryptonTextBox tbtmp = sender as KryptonTextBox;

            if (tbtmp.Text.Length > 1 && tbtmp.Text[0] == '.')
            {
                int select = tbtmp.SelectionStart;
                tbtmp.Text = tbtmp.Text.Insert(0, "0");
                tbtmp.SelectionStart = ++select;
            }
        }

    }
}
