using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;



namespace MVA20QuestionWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       private void txtNumeric_TextChanged(object sender, EventArgs e)
        {
        //    if (!Regex.IsMatch("[0-9]", txtNumeric.Text))
        //    {
        //        MessageBox.Show("Please enter only numbers.");
        //        txtNumeric.Text.Remove(txtNumeric.Text.Length - 1);
        //    }
        }

        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar); //&& !char.IsControl(e.KeyChar);
        }

    }
}
