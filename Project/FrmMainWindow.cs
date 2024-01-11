using Guna.UI2.WinForms;
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

        private void timerTransition_Tick(object sender, EventArgs e)
        { }

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
                        dashboardPanel.Width -= 15;
                        homePanel.Width -= 15;
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
        public void ChangeActive(int pressedID)
        {
            // Reset all buttons to default colors and images
            ResetAllButtons();

            // Set the active button based on the pressedID
            switch (pressedID)
            {
                case 1:
                    SetActiveButton(btnDashboard, Color.FromArgb(224, 224, 224), Color.Black, Color.FromArgb(224, 224, 224), "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\black_dashboard.png",label11);
                    break;
                case 2:
                    SetActiveButton(btnHome, Color.FromArgb(224, 224, 224), Color.Black, Color.FromArgb(224, 224, 224), "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\home.png",label10);
                    break;
                case 3:
                    SetActiveButton(btnGame, Color.FromArgb(224, 224, 224), Color.Black, Color.FromArgb(224, 224, 224), "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\black_games.png",label9);
                    break;
                case 4:
                    SetActiveButton(btnSysReq, Color.FromArgb(224, 224, 224), Color.Black ,Color.FromArgb(224, 224, 224), "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\blac_gamesystemReq.png",label8);
                    break;
                case 5:
                    SetActiveButton(btnEsport, Color.FromArgb(224, 224, 224), Color.Black, Color.FromArgb(224, 224, 224), "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\black_esport.png",label6);
                    break;
                // Add more cases as needed
                default:
                    break;
            }
        }

     

            public void ResetAllButtons()
            {
                //label6,8,9,10,11
                unActiveInGame();
                // Reset all buttons to their default colors and images
                SetActiveButton(btnDashboard, Color.Transparent, Color.White, Color.FromArgb(31, 31, 31), "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_dashboard.png", label11);
                SetActiveButton(btnHome, Color.Transparent, Color.White, Color.FromArgb(31,31,31), "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\home-icon-silhouette-white.png", label10);
                SetActiveButton(btnGame, Color.Transparent, Color.White, Color.FromArgb(31,31,31), "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_games.png", label9);
                SetActiveButton(btnSysReq, Color.Transparent, Color.White, Color.FromArgb(31,31,31), "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_gamesystemReq.png", label8);
                SetActiveButton(btnEsport, Color.Transparent, Color.White, Color.FromArgb(31,31,31), "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_esport.png",label6);
                // Add more buttons as needed
            }

            public void SetActiveButton(Guna2Button button, Color fillColor, Color foreColor ,Color hoverFillColor, string imagePath,Label L)
            {
                // Set the background color and hover state color for the given button
                button.FillColor = fillColor;
                button.HoverState.FillColor = hoverFillColor;
                L.BackColor = fillColor;
                L.ForeColor = foreColor;
                // Set the button image from the provided file path
                if (!string.IsNullOrEmpty(imagePath))
                {
                    button.Image = Image.FromFile(imagePath);
                }
            }

        // Add more buttons as needed


        public void unActiveInGame()
        {
                btnList.FillColor = Color.Transparent;
                btnList.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnNews.FillColor = Color.Transparent;
                btnNews.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnTodo.FillColor = Color.Transparent;
                btnTodo.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnPlay.FillColor = Color.Transparent;
                btnPlay.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnList.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_gameList.png");

                btnTodo.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_todoList.png");
                btnPlay.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_playGame.png");
                btnNews.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_GameNews.png");
                
                btnList.ForeColor= Color.White;
                btnNews.ForeColor = Color.White;
                btnTodo.ForeColor = Color.White;
                btnPlay.ForeColor = Color.White;

        }

        public void changActiveInGame(int gameBtnID)
        {
            if (gameBtnID == 0)
            {
                btnList.FillColor = Color.FromArgb(224, 224, 224);
                btnList.HoverState.FillColor = Color.FromArgb(224, 224, 224);

                btnNews.FillColor = Color.Transparent;
                btnNews.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnTodo.FillColor = Color.Transparent;
                btnTodo.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnPlay.FillColor = Color.Transparent;
                btnPlay.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnList.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\black_gameList.png");

                btnTodo.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_todoList.png");
                btnPlay.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_playGame.png");
                btnNews.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_GameNews.png");

                btnList.ForeColor = Color.Black;
                btnNews.ForeColor = Color.White;
                btnTodo.ForeColor = Color.White;
                btnPlay.ForeColor = Color.White;


            }
            else if (gameBtnID == 1)
            {
                btnNews.FillColor = Color.FromArgb(224, 224, 224);
                btnNews.HoverState.FillColor = Color.FromArgb(224, 224, 224);

                btnList.FillColor = Color.Transparent;
                btnList.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnTodo.FillColor = Color.Transparent;
                btnTodo.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnPlay.FillColor = Color.Transparent;
                btnPlay.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnList.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_gameList.png");

                btnTodo.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_todoList.png");
                btnPlay.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_playGame.png");
                btnNews.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\black_GameNews.png");

                btnList.ForeColor = Color.White;
                btnNews.ForeColor = Color.Black;
                btnTodo.ForeColor = Color.White;
                btnPlay.ForeColor = Color.White;


            }
            else if (gameBtnID == 2)
            {
                btnTodo.FillColor = Color.FromArgb(224, 224, 224);
                btnTodo.HoverState.FillColor = Color.FromArgb(224, 224, 224);

                btnNews.FillColor = Color.Transparent;
                btnNews.HoverState.FillColor = Color.FromArgb(31, 31, 31);
                btnList.FillColor = Color.Transparent;
                btnList.HoverState.FillColor = Color.FromArgb(31, 31, 31);
                btnPlay.FillColor = Color.Transparent;
                btnPlay.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnList.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_gameList.png");
                btnTodo.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\todoList.png");

                btnTodo.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_playGame.png");
                btnNews.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_GameNews.png");

                btnList.ForeColor = Color.White;
                btnNews.ForeColor = Color.White;
                btnTodo.ForeColor = Color.Black;
                btnPlay.ForeColor = Color.White;

            }
            else
            {
                btnPlay.FillColor = Color.FromArgb(224, 224, 224);
                btnPlay.HoverState.FillColor = Color.FromArgb(224, 224, 224);

                btnNews.FillColor = Color.Transparent;
                btnNews.HoverState.FillColor = Color.FromArgb(31, 31, 31);
                btnTodo.FillColor = Color.Transparent;
                btnTodo.HoverState.FillColor = Color.FromArgb(31, 31, 31);
                btnList.FillColor = Color.Transparent;
                btnList.HoverState.FillColor = Color.FromArgb(31, 31, 31);

                btnList.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_gameList.png");

                btnTodo.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_todoList.png");
                btnPlay.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\black_playGame.png");
                btnNews.Image = Image.FromFile("C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\icon\\white_GameNews.png");
                btnList.ForeColor = Color.White;
                btnNews.ForeColor = Color.White;
                btnTodo.ForeColor = Color.White;
                btnPlay.ForeColor = Color.Black;

            }
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }


        private void btnGame_Click_1(object sender, EventArgs e)
        {
            if (subMenu.Visible == true)
            {
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
            ChangeActive(2);
            loadform(new Home());

        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {

            ChangeActive(1);
            loadform(new DashBoard());
          
        }

        private void btnList_Click(object sender, EventArgs e)
        {

            ChangeActive(3);
            changActiveInGame(0);
            loadform(new GameList());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            ChangeActive(3);
            changActiveInGame(1);
            loadform(new GameNews());
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

            ChangeActive(3);
            changActiveInGame(2);
            loadform(new GameTodo());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            ChangeActive(3);
            changActiveInGame(3);
            loadform(new GamePlay());
        }

        private void btnSysReq_Click(object sender, EventArgs e)
        {
            ChangeActive(4);
            loadform(new GameSystemReq());
        }

        private void btnEsport_Click(object sender, EventArgs e)
        {
            ChangeActive(5);
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

        private void sidebarMenuPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
