using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class FrmSignUp : Form
    {
        public FrmSignUp()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGOback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form = new Form1();
            form.Show();    
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmLogin form = new FrmLogin();
            form.Show();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmLogin form = new FrmLogin();
            form.Show();
        }

        private void chkShowpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowpassword.Checked == true)
            {
                txtPassword.PasswordChar = '\0';
                txtPassConfirm.PasswordChar = '\0';
                label7.Visible = true;
            }
            else
            {
                txtPassword.PasswordChar = '●';
                txtPassConfirm.PasswordChar = '●';
                label7.Visible = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
