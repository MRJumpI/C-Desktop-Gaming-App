using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Project
{
    public partial class GameList : Form
    {
        public Dictionary<string, string>[] gameListData = new Dictionary<string, string>[120];
        public Dictionary<string, string>[] oldGameData = new Dictionary<string, string>[10];
        public Dictionary<string, object>[] lovedGameData = new Dictionary<string, object>[5];
        public Dictionary<string, object>[] newGameData = new Dictionary<string, object>[5];
        private int indexgameList = 0;
        private int endIndex = 6;
        private int startingIndex = 0;

        private int indexoldGame = 0;
        private int indexlovedGame = 0;
        private int indexnewGame = 0;

        public Dictionary<string, object> searchGameData = new Dictionary<string, object>();
        public int gamePageCount = -1;
        public GameList()
        {
            InitializeComponent();
        }

        private async void GameList_Load(object sender, EventArgs e)
        {
            try
            {
                Task.Run(async () =>
                {
                    ManageAPIData apidataObj = new ManageAPIData();
                    gameListData = await apidataObj.GetAllGameData();
                    oldGameData = await apidataObj.getOldGameData();
                    lovedGameData = await apidataObj.getTop5LovedandNewGames(0);
                    newGameData = await apidataObj.getTop5LovedandNewGames(1);

                    // Update UI on the UI thread after API call completes
                    this.Invoke((Action)delegate
                    {
                        try
                        {
                            changeOldGameData(oldGameData[indexoldGame]);
                            setLovedData(lovedGameData[indexlovedGame]);
                            setNewData(newGameData[indexnewGame]);
                            UpdateGamePanels();
                            // Subscribe to the Paint event
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.Message + " Game List");
                        }
                    });
                });
            }
            catch (Exception ex) { }
        }

        private async void UpdateGamePanels()
        {

           
                for (int i = startingIndex; i < gameListData.Length ; i++)
                {
                    if (i == 0) { 
                    gameName1.Text = gameListData[i]["Name"];
                    newPic1GameList.Text = "<img src=\" " + gameListData[i]["BackgroundImage"] + " \" alt=\"imagGame\" width=130px height=130px/>";
                    gameReleaseDate1.Text = gameListData[i]["Released"];    
                }
                    else
                    {
                        Guna2Panel newPanel = new Guna2Panel();
                        newPanel.Width = gameListpanel.Width;
                        newPanel.Height = gameListpanel.Height;
                        newPanel.BackColor=gameListpanel.BackColor;
                        newPanel.BorderRadius= gameListpanel.BorderRadius;
                        newPanel.FillColor=gameListpanel.FillColor;

                        Label nameLabel = new Label();
                        nameLabel.Width = gameName1.Width;
                        nameLabel.Height = gameName1.Height;
                        nameLabel.AutoSize=gameName1.AutoSize;
                        nameLabel.Font=gameName1.Font;
                        nameLabel.ForeColor=gameName1.ForeColor;
                        nameLabel.BackColor=gameName1.BackColor;
                        nameLabel.Text = gameListData[i]["Name"];
                        nameLabel.Location = gameName1.Location;

                    Label releaseLabel = new Label();
                    releaseLabel.Width = gameReleaseDate1.Width;
                    releaseLabel.Height = gameReleaseDate1.Height;
                    releaseLabel.AutoSize = gameReleaseDate1.AutoSize;
                    releaseLabel.Font = gameReleaseDate1.Font;
                    releaseLabel.ForeColor = gameReleaseDate1.ForeColor;
                    releaseLabel.BackColor = gameReleaseDate1.BackColor;
                    releaseLabel.Text = gameListData[i]["Released"];
                    releaseLabel.Location = gameReleaseDate1.Location;
                    releaseLabel.BringToFront();

                    Guna2HtmlLabel picLabel = new Guna2HtmlLabel();
                        picLabel.Width=loadGame1pic.Width;
                        picLabel.Height=loadGame1pic.Height;
                        picLabel.Text = "<img src=\" " + gameListData[i]["BackgroundImage"] + " \" alt=\"imagGame\" width=130px height=130px/>";
                        picLabel.Location = loadGame1pic.Location;

                        newPanel.Controls.Add(picLabel);
                        newPanel.Controls.Add(releaseLabel);
                        newPanel.Controls.Add(nameLabel);
                        


                        int newX = gameListpanel.Location.X ;
                        int newY = gameListpanel.Location.Y + i * 190;
                        newPanel.Location = new System.Drawing.Point(newX, newY);
                        
                        panelGame.Controls.Add(newPanel);
                
                }

            }
            }
        

            private void setLovedData(Dictionary<string, object> dictionary)
        {
            if (indexlovedGame > 4)
            {
                indexlovedGame = 4;
            }else if(indexlovedGame < 0) { indexlovedGame= 0; }
            /*
             *      { "Name", name },
                                    { "Platforms", string.Join("", platformsGame) },
                                    { "Rating", rating },
                                    { "BackgroundImage", bg }
             * */
            lovedGameName.Text =(string) dictionary["Name"];
            lovedGamePlatform.Text =(string)dictionary["Platforms"];
            lovedgamePic.Text = "<img src=\" " + (string)dictionary["BackgroundImage"] +"\" alt=\"Game BG\" width=\"200\" height=\"100\" />";
            lovedGameRating.Text = (string)dictionary["Rating"];

        }
        private void setNewData(Dictionary<string, object> dictionary)
        {
            if (indexnewGame > 4)
            {
                indexnewGame = 4;
            }
            else if (indexnewGame < 0) { indexnewGame = 0; }
            /*
             *      { "Name", name },
                                    { "Platforms", string.Join("", platformsGame) },
                                    { "Rating", rating },
                                    { "BackgroundImage", bg }
             * */
            newGameName.Text = (string)dictionary["Name"];
            newGamePlat.Text = (string)dictionary["Platforms"];
            newGamePic.Text = "<img src=\" " + (string)dictionary["BackgroundImage"] + "\" alt=\"Game BG\" width=\"200\" height=\"100\" />";
            newGameRating.Text = (string)dictionary["Rating"];

        }

        private void changeOldGameData(Dictionary<string, string> dictionary)
        {
            /*
             *  { "Name", name },
                                    { "Released", released },
                                    { "BackgroundImage", bg }
             * */
            if (indexoldGame > 9)
            {
                indexoldGame = 9;
            }
            else if (indexoldGame < 0)
            {
                indexoldGame = 0;
            }
            oldGameName.Text = (string)dictionary["Name"];
            oldGamereleaseDate.Text = (string)dictionary["Released"];
            string oldPic = dictionary["BackgroundImage"];
            oldGamePic.Text = "<img src=\" " + oldPic + "\"alt=\"imagGame\" width=150px height=150px/>";

        }

        
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string gameName = txtGameName.Text.Trim();

            if (string.IsNullOrEmpty(gameName))
            {
                MessageBox.Show("Please enter a game name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Start the API call on a background thread
                searchGameData = await Task.Run(() => GetGameInfoAsync(gameName));

                // Continue on the UI thread to update the UI
                setGameInfo(searchGameData);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<Dictionary<string, object>> GetGameInfoAsync(string gameName)
        {
            ManageAPIData apidataObj = new ManageAPIData();
            return await apidataObj.getGameSysReqBySearch(gameName);
        }
        private void setGameInfo(Dictionary<string, object> searchGameData)
        {
            gameName.Text = (string)searchGameData["Name"];
            releaseDatae.Text = (string)searchGameData["Releaseddate"];
            platforms.Text = (string)searchGameData["Platforms"];
            stores.Text = (string)searchGameData["stores"];
            bacgroundImage.Text = "<img src=\"" + (string)searchGameData["BackgroundImage"] + "\" alt=\"imagGame\" width=200px height=200px/>";
            string[] images = (string[])searchGameData["ScreenShots"];
            int index = 0;
            foreach (string image in images)
            {
                switch (index)
                {
                    case 0:
                        screenshot1.Text = "<img src=\"" + image + "\" alt=\"imagGame\" width=100px height=100px/>";
                        break;
                    case 1:
                        screenshot2.Text = "<img src=\"" + image + "\" alt=\"imagGame\" width=100px height=100px/>";

                        break;
                    case 2:
                        screenshot3.Text = "<img src=\"" + image + "\" alt=\"imagGame\" width=100px height=100px/>";

                        break;
                    case 3:
                        screenshot4.Text = "<img src=\"" + image + "\" alt=\"imagGame\" width=100px height=100px/>";

                        break;
                    default: break;
                }
                index++;
            }
        }

        private void btnNextOldGame_Click(object sender, EventArgs e)
        {
            indexoldGame++;
            if (indexoldGame > 9) indexoldGame = 0;
            changeOldGameData(oldGameData[indexoldGame]);

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            
            indexoldGame--;
            if (indexoldGame < 0) indexoldGame = 9;
            changeOldGameData(oldGameData[indexoldGame]);
        }

        private void btnNextLovedGame_Click(object sender, EventArgs e)
        {
            indexlovedGame++;
            if (indexlovedGame >= 5) { indexlovedGame = 0; }
            setLovedData(lovedGameData[indexlovedGame]);
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {

            indexnewGame++;

            if (indexnewGame >= 5) { indexnewGame = 0; }
            setNewData(newGameData[indexnewGame]);
        }
    }
}
