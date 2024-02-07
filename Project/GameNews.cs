using NewAPIs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class GameNews : Form
    {
        public static int count = 1;
        public int mainNewsPanelFlag = 0;
        Dictionary<string, string>[] epicGameData = new Dictionary<string, string>[5];
        Dictionary<string, string>[] mmoGameData = new Dictionary<string, string>[5];
        NewsItem[] arrayvideoGamesData = new NewsItem[10];
        NewsItem[] arraysteamGamesData = new NewsItem[10];
        NewsItem[] mainNewsData = new NewsItem[10];
        int indexMainNews = 0;
        int indexEpicNews = 0;
        int indexEsportNews = 0;
        int indexSteamGame = 0;
        string mainPanelLink1;
        string mainPanelLink2;
        string epicNewsLink;
        string mmoNewsLink;

        public GameNews()
        {

            InitializeComponent();

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            mainNewsPanelFlag = 1;
            mainNewsData = arraysteamGamesData;
            indexMainNews = 0;
            cleardata();
            DisplayNewsData(arraysteamGamesData[indexSteamGame]);

            new1ShowPanel.FillColor = Color.FromArgb(27, 40, 56);
            heading.Text = "TOP 10 STEAM GAMES NEWS";
            titleSnew.ForeColor = Color.White;
            titleSnew.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label3.ForeColor = Color.White;
            label4.ForeColor = Color.Transparent;
            linkSite.BackColor = Color.Transparent;
            linkSite.LinkColor = Color.White;
            linkSource.BackColor = Color.Transparent;
            linkSource.LinkColor = Color.White;
        }

        private void NEWS_Click(object sender, EventArgs e)
        {
            mainNewsPanelFlag = 0;
            mainNewsData = arrayvideoGamesData;
            indexSteamGame = 0;
            cleardata();
            DisplayNewsData(mainNewsData[indexMainNews]);

            new1ShowPanel.FillColor = Color.Gainsboro;
            heading.Text = "TOP 10 GAMES NEWS";
            titleSnew.ForeColor = Color.Black;
            titleSnew.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Transparent;
            linkSite.BackColor = Color.Transparent;
            linkSite.LinkColor = Color.Black;
            linkSource.BackColor = Color.Transparent;
            linkSource.LinkColor = Color.Black;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void heading_Click(object sender, EventArgs e)
        {

        }



        private void label7_Click(object sender, EventArgs e)
        {

        }

        private async void GameNews_Load(object sender, EventArgs e)
        {
            cleardata();
            try
            {
                Task.Run(async () =>
                {
                    ManageNewsApi newsApiObj = new ManageNewsApi();
                    const string apiKey = "0ca9b51b9bmsh82d1c3bcb8e46cbp123647jsn092f49592018";
                    const string apiUrl = "https://real-time-news-data.p.rapidapi.com/search";

                    var mmoApiUrl = "https://mmo-games.p.rapidapi.com/latestnews";
                    var mmoData = await newsApiObj.ExecuteApiRequest(mmoApiUrl);

                    var epicApiUrl = "https://epic-games-store.p.rapidapi.com/getNews/locale/en/limit/30?trending=true";
                    var epicData = await newsApiObj.ExecuteApiRequest(epicApiUrl);
                    epicGameData = newsApiObj.ProcessEpicData(epicData, 5);
                    mmoGameData = newsApiObj.ProcessMMOData(mmoData, 5);

                    List<NewsItem> videoGamesData = await newsApiObj.GetNewsData(apiUrl, apiKey, "Video Games", "US", "en", 10);
                    List<NewsItem> steamGamesData = await newsApiObj.GetNewsData(apiUrl, apiKey, "Steam Games", "US", "en", 10);
                    arrayvideoGamesData = videoGamesData.ToArray();
                    arraysteamGamesData = steamGamesData.ToArray();

                    mainNewsData = arrayvideoGamesData;
                    // Update UI on the UI thread after API call completes
                    this.Invoke((Action)delegate
                    {
                        try
                        {
                            DisplayNewsData(mainNewsData[indexMainNews]);

                            DisplayDataEpic(epicGameData[indexEpicNews]);

                            DisplayDataMMO(mmoGameData[indexEsportNews]);
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

        public void DisplayDataMMO(Dictionary<string, string> data)
        {
            //doo work
            /*
             *  { "Title",title},
                { "ShortDescription",shortDescription },
                { "Thumbnail", thumbnail },
                { "MainImage", mainImage},
                { "ArticleContent",  articleContent },
                { "ArticleUrl",articleUrl}
             */
            titleMMO.Text = data["Title"];
            mmogameShort.Text = data["ShortDescription"];
            contentMMO.Text = data["ArticleContent"];
            mmoNewsMainPic.Text = "<img src=\" " + data["MainImage"] + "\" alt=\"new Pic\" width=\"150px\" height=\"150px\"/>";
            mmoNewsLink = data["ArticleUrl"];
        }
        public void DisplayDataEpic(Dictionary<string, string> data)
        {
            //doo work
            /*
             *  
                {"URL", url},
                { "Date", date },
                { "ShortDescription",shortDescription },
                { "Content",content },
                { "Title", title},
                { "Author", author}
             */

            epicNewsLink = data["URL"];
            authorEpic.Text = data["Author"];
            titleEpicGame.Text = data["Title"];
            contentEpic.Text = data["Content"];
            dateEpic.Text = data["Date"];

        }
        public void DisplayNewsData(NewsItem item)
        {
            cleardata();

            if (count == 1) { count++;

                titleSnew.Text = item.Title;
                mainPanelLink1 = item.Link;
                mainPanelLink2 = item.SourceUrl;
                mainNewsPic.Text = "<img src=\" " + item.PhotoUrl.ToString() + " \" alt=\"new Pic\" width=\"200px\" height=\"200px\"/>";
            }
            else
            {
                titleSnew.Text = item.Title;
                mainPanelLink1 = item.Link;
                mainPanelLink2 = item.SourceUrl;
                mainNewsPic.Text = "<img src=\" " + item.PhotoUrl.ToString() + " \" alt=\"new Pic\" width=\"200px\" height=\"200px\"/>";
            }
            count++;

        }
        public void cleardata()
        {
            titleSnew.Text = "";
            linkSite.Text = "click here";
            linkSource.Text = "click here";
            mainNewsPic.Text = "<img src=\" \"\" alt=\"new Pic\" width=\"200px\" height=\"200px\"/>"; 
        }
        private void btnSimpleNewsNext_Click(object sender, EventArgs e)
        {
            if (mainNewsPanelFlag == 0) { 
            if (indexMainNews > 9)
            {
                indexMainNews = 0;
            }
            else { indexMainNews++; }

            DisplayNewsData(mainNewsData[indexMainNews]);
            }
            else
            {
                if (indexSteamGame > 9)
                {
                    indexSteamGame = 0;
                }
                else { indexSteamGame++; }

                DisplayNewsData(arraysteamGamesData[indexSteamGame]);

            }
        }

        private void nextEpic_Click(object sender, EventArgs e)
        {
            if (indexEpicNews > 3)
            {
                indexEpicNews = 0;
            }
            else { indexEpicNews++; }
            DisplayDataEpic(epicGameData[indexEpicNews]);
        }

        private void btnMMONext_Click(object sender, EventArgs e)
        {

            if (indexEsportNews > 3)
            {
                indexEsportNews = 0;
            }
            else { indexEsportNews++; }
            DisplayDataMMO(mmoGameData[indexEsportNews]);
        }

        private void OpenUrlInBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening URL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrlInBrowser(mainPanelLink2);
        }

        private void linkSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrlInBrowser(mainPanelLink1);
        }

        private void epicLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrlInBrowser(epicNewsLink);
        }

        private void urlMMO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrlInBrowser(mmoNewsLink);
        }
    }
}
