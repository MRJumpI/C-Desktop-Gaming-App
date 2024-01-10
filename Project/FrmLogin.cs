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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void chkShowpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowpassword.Checked == true)
            {
                txtPassword.PasswordChar = '\0';
                label7.Visible = true;
            }
            else
            {
                txtPassword.PasswordChar = '●';
                label7.Visible = false;
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnGOback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form = new Form1();
            form.Show();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmSignUp form = new FrmSignUp();
            form.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmSplashScreen form = new FrmSplashScreen();
            form.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
