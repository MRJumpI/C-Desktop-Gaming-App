using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class FrmMainWindow : Form
    {
       
        private int sidemenu = 0; // 0 is close 1 is open

        public FrmMainWindow()
        {
            InitializeComponent();
        }

        private void FrmMainWindow_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);
            loadform(new Home());
        }
      
        private void timerTransition_Tick(object sender,EventArgs e)
        {}

        private void guna2ShadowPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pic2_Click(object sender, EventArgs e)
        {

        }

        private void bgPic1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuTransition_Tick(object sender, EventArgs e)
        {
            sidemenu++;
            if (sidemenu <= 1)
            {
                sidemenu = 1;
                sidebarMenuPanel.Width = 195;
                dashboardPanel.Width = 150;
                homePanel.Width = 150;
                gamePanel.Width = 150;
                sysreqPanel.Width = 150;
                esportPanel.Width = 150;
                logoutPanel.Width = 150;
                btnDashboard.Width = 139;
                btnEsport.Width = 139;
                btnGame.Width = 139;
                btnHome.Width = 139;
                btnLogout.Width = 139;
                btnSysReq.Width = 139;

                subMenu.Location = new Point(220, 347);                 
                menuTransition.Stop();
                

            }
            else
            {
                
                if (sidebarMenuPanel.Width >= 110)
                {
                    
                    sidemenu = 1;
                    sidebarMenuPanel.Width -= 15;
                  if (dashboardPanel.Width >= 60)
                    {
                        dashboardPanel.Width -=15;
                        homePanel.Width -=15;
                        gamePanel.Width -= 15;
                        sysreqPanel.Width -= 15;
                        esportPanel.Width -= 15;
                        logoutPanel.Width -= 15;
                    }

                    if (btnDashboard.Width >= 50)
                    {
                        btnDashboard.Width -= 15;
                        btnEsport.Width -= 15;
                        btnGame.Width -= 15;
                        btnHome.Width -= 15;
                        btnLogout.Width -= 15;
                        btnSysReq.Width -= 15;
                    }
                    
                }
                else
                {
                    sidemenu = 0;
                    sidebarMenuPanel.Width -= 5;
                    subMenu.Location = new Point(124, 347);
                    menuTransition.Stop();
                }

            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

       
        private void btnGame_Click_1(object sender, EventArgs e)
        {
            if (subMenu.Visible == true) { 
                subMenu.Visible = false;
                beautifulTransition.HideSync(subMenu);
            }
            else
            {
                subMenu.Visible = true;
                beautifulTransition.ShowSync(subMenu);
            }
        }
        public void loadform(object Form)
        {
            if (this.defaultpanel.Controls.Count > 0)
                this.defaultpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.defaultpanel.Controls.Add(f);
            this.defaultpanel.Tag = f;
            f.Show();
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            loadform(new Home());
            
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            loadform(new DashBoard());
            btnDashboard.FillColor = Color.FromArgb(224, 224, 224);
            btnDashboard.HoverState.FillColor=Color.FromArgb(255, 255, 255);
            //label 6,7,9,10,11
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            loadform(new GameList());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            loadform(new GameNews());
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            loadform(new GameTodo());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            loadform(new GamePlay());
        }

        private void btnSysReq_Click(object sender, EventArgs e)
        {
            loadform(new GameSystemReq());
        }

        private void btnEsport_Click(object sender, EventArgs e)
        {
            loadform(new Esport());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
