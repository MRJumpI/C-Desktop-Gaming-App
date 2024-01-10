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
    public partial class FrmSplashScreen : Form
    {
        public FrmSplashScreen()
        {
            InitializeComponent();
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Time_Tick1(object sender, EventArgs e)
        {
            loadPanel.Width += 3;
            if (loadPanel.Width >= 560) { 
                timer1.Stop();
                this.Close();
                FrmMainWindow form = new FrmMainWindow();
                form.Show();
            }
        }
    }
}
