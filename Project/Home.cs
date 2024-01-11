using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Helpers;

namespace Project
{

    public partial class Home : Form
    {
        public Dictionary<string, object>[] topTData = new Dictionary<string, object>[10];

        public Dictionary<string, object>[] eaTopTData = new Dictionary<string, object>[10];
        public Dictionary<string, object>[] upcomingGameData = new Dictionary<string, object>[5];
        private int indexTopTen = 0; 
        private int indexUpGame = 0;
        private int count = 0;
        
        public Home()
        {
            InitializeComponent();
        }

  
        private void timeTransition_Tick(object sender, EventArgs e)
        {
            count++;
            if (count == 1)
            {
                ocpacityTransition.HideSync(bgPic1);
                bgPic1.Visible = false;

                ocpacityTransition.ShowSync(pic2);
                pic2.Visible = true;
            }
            else if (count == 2)
            {
                ocpacityTransition.HideSync(pic2);
                pic2.Visible = false;

                ocpacityTransition.ShowSync(pic3);
                pic3.Visible = true;

            }
            else if (count == 3)
            {
                ocpacityTransition.HideSync(pic3);
                pic3.Visible = false;

                ocpacityTransition.ShowSync(pic4);
                pic4.Visible = true;

            }

            else if (count == 4)
            {
                ocpacityTransition.HideSync(pic4);
                pic4.Visible = false;

                ocpacityTransition.ShowSync(pic5);
                pic5.Visible = true;

            }
            else if (count == 5)
            {
                ocpacityTransition.HideSync(pic5);
                pic5.Visible = false;

                ocpacityTransition.ShowSync(pic6);
                pic6.Visible = true;

            }
            else
            {
                count = 0;
                ocpacityTransition.HideSync(pic6);
                pic6.Visible = false;

                ocpacityTransition.ShowSync(bgPic1);
                bgPic1.Visible = true;

            }

        }

        private async void Home_Load(object sender, EventArgs e)
        {
            try { 
           
            // Get the current date and time
            DateTime currentDate = DateTime.Now;

            // Display the current day and date
            day.Text= currentDate.DayOfWeek.ToString();
            time.Text= currentDate.ToString("yyyy-MM-dd");
            
            bgPic1.Visible = true;
            pic2.Visible = false;
            pic3.Visible = false;
            pic4.Visible = false;
            pic5.Visible = false;
            pic6.Visible = false;

            //default Top Ten Game
            clearGameDataTopTen();
            timerTransition.Start();

                Task.Run(async () =>
                {
                    ManageAPIData apidataObj = new ManageAPIData();
                    topTData = await apidataObj.getTenData();
                    eaTopTData = await apidataObj.getTop10EAGames();
                    upcomingGameData = await apidataObj.getFiveUpcomingGames();
                    // Update UI on the UI thread after API call completes
                    this.Invoke((Action)delegate
                    {
                        try { 
                        changeTopTenData(topTData[indexTopTen]);
                        makeTenPanel();
                        changeUpcomigGameData(upcomingGameData[indexUpGame]);
                        // Subscribe to the Paint event
                        }catch(Exception e) { }
                    });
                });
            
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error !");
            }
        }

        private void changeUpcomigGameData(Dictionary<string, object> dictionary)
        {
            upcomingGameName.Text = (string)dictionary["Name"];
            upcomingGameReleaseDate.Text = (string)dictionary["Releaseddate"];
            upcommingGamePlatform.Text = (string)dictionary["Platforms"];
            upcomingGameBG.Text = "<img src=\" "+ (string)dictionary["BackgroundImage"] + "\" alt=\"Game BG\" width=\"200\" height=\"100\" />";
        }
        private async void makeTenPanel()
        {
            try
            {
              

            // Specify the number of panels you want to add
            int panelCount = 10;
            int numberOfList = 2;
                for (int i = 0; i < 10; i++)
                {
                    string[] images = (string[])eaTopTData[i]["screenshots"];
                    if (i == 0)
                    {
                        EAname.Text = (string)eaTopTData[i]["Name"];
                        //EARating.Text = (string)eaTopTData[i]["rating"];
                        EAPlatform.Text = (string)eaTopTData[i]["Platforms"];
                        eaPic1.Text = "<img src=\" " + images[1] + " \" alt=\"Game BG\" width=\"60\" height=\"65\" />";
                        Guna.UI2.WinForms.Guna2HtmlLabel newGunalabel = new Guna2HtmlLabel();
                        newGunalabel.Text = "<img src=\" " + images[2] + " \" alt=\"Game BG\" width=\"60\" height=\"65\" />";
                        newGunalabel.Location = eaPic2.Location;
                    }
                    else { 
                    
                    // Clone the original GunaPanel
                    Guna.UI2.WinForms.Guna2Panel newGunaPanel = new Guna2Panel();
                    newGunaPanel.FillColor = EADataPanel.FillColor;
                    newGunaPanel.Width = EADataPanel.Width;
                    newGunaPanel.Height = EADataPanel.Height;
                    newGunaPanel.BorderRadius = EADataPanel.BorderRadius;
                    
                    //num
                    Label num = new Label();
                    num.ForeColor = EAGameNum.ForeColor;
                    num.Text = numberOfList.ToString();
                    num.Width = EAGameNum.Width;
                    num.Height = EAGameNum.Height;
                    num.BackColor = Color.Transparent;
                    num.Font = EAGameNum.Font;
                    num.Location = new Point(-10, -6);

                    //Game Name
                    Label name = new Label();
                    name.ForeColor = EAname.ForeColor;
                    name.Text = (string)eaTopTData[i]["Name"];
                    name.Width = EAname.Width;
                    name.Height = EAname.Height;
                    name.BackColor = Color.Transparent;
                    name.Font = EAname.Font;
                    name.Location = EAname.Location;

                    //Rating
                    Label rating = new Label();
                    rating.ForeColor = EARating.ForeColor;
                    EARating.Text = (string)eaTopTData[i]["rating"];
                    rating.BackColor = Color.Transparent;
                    rating.Font = EARating.Font;
                    rating.Location = EARating.Location;

                    //Platforms
                    Label platforms = new Label();
                    platforms.ForeColor = EAPlatform.ForeColor;
                    EAPlatform.Text = (string)eaTopTData[i]["Platforms"];
                    platforms.BackColor = Color.Transparent;
                    platforms.Font = EAPlatform.Font;
                    platforms.Location = EAPlatform.Location;

                    //gunaPicture
                    Guna.UI2.WinForms.Guna2HtmlLabel pic1 = new Guna2HtmlLabel();
                    pic1.ForeColor = eaPic1.ForeColor;
                    pic1.Text = "<img src=\" " + images[0] + " \" alt=\"Game BG\" width=\"60\" height=\"65\" />";
                    pic1.Width = eaPic1.Width;
                    pic1.Height = eaPic1.Height;
                    pic1.BackColor = eaPic1.BackColor;
                    pic1.Font = eaPic1.Font;
                    pic1.Location = eaPic1.Location;

                    Guna.UI2.WinForms.Guna2HtmlLabel pic2 = new Guna2HtmlLabel();
                    pic2.ForeColor = eaPic2.ForeColor;
                    pic2.Text = eaPic2.Text = "< img src =\" " + images[1] + "\" alt = \"Game BG\" width = \"60\" height = \"50\" />";
                    pic2.Width = eaPic2.Width;
                    pic2.Height = eaPic2.Height;
                    pic2.BackColor = eaPic2.BackColor;
                    pic2.Font = eaPic2.Font;
                    pic2.Location = eaPic2.Location;


                    newGunaPanel.Controls.Add(pic1);
                    newGunaPanel.Controls.Add(pic2);
                    newGunaPanel.Controls.Add(platforms);
                    newGunaPanel.Controls.Add(rating);
                    newGunaPanel.Controls.Add(name);
                    newGunaPanel.Controls.Add(num);
                    // Set the position of the new GunaPanel
                    int yOffset = ((i-1) + 1) * (newGunaPanel.Height + 10);
                    newGunaPanel.Location = new System.Drawing.Point(EADataPanel.Location.X, EADataPanel.Location.Y + yOffset);
                    EAMainPanel.Controls.Add(newGunaPanel);


                        numberOfList++;
                    }

                }

            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            
        }
      

    
        private void changeTopTenData(Dictionary<string, object> dictionary)
        {
            try { 
            clearGameDataTopTen();    
            gameName.Text = (string)dictionary["Name"];
            nameUnderLine.Width = gameName.Width - 32;
            gameDate.Text = (string)dictionary["releaseDate"];
            platformName.Text = (string)dictionary["Platforms"];
            rating.Text = (string)dictionary["rating"];
            
            string[] storesArray = (string[])dictionary["Stores"];

            for (int i = 0; i < storesArray.Length; i++)
            {
                string storeName = storesArray[i];
                storesname.Text += storeName+" - ";
            }
            string[] images = (string[])dictionary["screenshots"];
            for(int i = 0; i < images.Length; i++)
            {

                switch (i)
                {
                    case 0:
                        screenshot1.Text = "<img src=\" " + images[i] +" \" alt=\"Game BG\" width=\"150\" height=\"150\" />";
                        break;
                    case 1:
                        screenshot2.Text = "<img src=\" " + images[i] + " \" alt=\"Game BG\" width=\"150\" height=\"150\" />";
                        break;
                    case 2:
                        screenshot3.Text = "<img src=\" " + images[i] + " \" alt=\"Game BG\" width=\"150\" height=\"150\" />";
                        break;
                    case 3:
                        screenshot4.Text = "<img src=\" " + images[i] + " \" alt=\"Game BG\" width=\"150\" height=\"150\" />";

                        break;
                    default:
                        break;
                }
            }

            platformsPanel.Width = platformName.Width + 20;
            storesPanel.Width = storesname.Width + 20;

            //animation
            
            }
            catch(Exception e) { }
        }

        private void clearGameDataTopTen()
        {
            gameName.Text = " ";
            gameDate.Text = "";
            storesname.Text = "";
            platformName.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (indexTopTen < 9)
                indexTopTen++;
            else
                indexTopTen = 0;

            changeTopTenData(topTData[indexTopTen]);

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            int change = EAMainPanel.VerticalScroll.Value - EAMainPanel.VerticalScroll.SmallChange * 30;
            EAMainPanel.AutoScrollPosition = new Point(0,change);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int change = EAMainPanel.VerticalScroll.Value + EAMainPanel.VerticalScroll.SmallChange * 30;
            EAMainPanel.AutoScrollPosition = new Point(0, change);
        }

        private void btnUpcommingNext_Click(object sender, EventArgs e)
        {
            if (indexUpGame < 4)
            {
                indexUpGame++;
            }
            else indexUpGame = 0;

            changeUpcomigGameData(upcomingGameData[indexUpGame]);
        }
    }
}