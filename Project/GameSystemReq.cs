using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Project
{
    

    public partial class GameSystemReq : Form
    {
        public Dictionary<string, object> searchGameData = new Dictionary<string, object>();
        private XDocument xmlDoc;
        public GameSystemReq()
        {
            InitializeComponent();
        }

        private void GameSystemReq_Load(object sender, EventArgs e)
        {
            LoadXmlData();
       
        }
        private void setGameInfo(Dictionary<string, object> searchGameData)
        {
            Name.Text= (string) searchGameData["Name"];
            releaseDate.Text = (string)searchGameData["Releaseddate"];
            platforms.Text = (string)searchGameData["Platforms"];
            stores.Text = (string)searchGameData["stores"];
            bacgroundImage.Text = "<img src=\"" + (string)searchGameData["BackgroundImage"] +"\" alt=\"imagGame\" width=200px height=200px/>";
            string[] images = (string[])searchGameData["ScreenShots"];
            int index = 0;
            foreach (string image in images) { 
                switch(index)
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

        private void LoadXmlData()
        {
            // Replace 'your_xml_file_path' with the actual path to your XML file
            string xmlFilePath = "C:\\Users\\musma\\source\\repos\\Project\\Project\\Resources\\GameSysReqData.xml";

            try
            {
                xmlDoc = XDocument.Load(xmlFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Data ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                XElement gameElement = xmlDoc.Descendants("Game")
                    .FirstOrDefault(game => game.Attribute("name")?.Value == gameName);

                if (gameElement != null)
                {
                    DisplaySystemRequirements(gameElement, "minimum_system_requirements");
                    DisplaySystemRequirements(gameElement, "recommended_system_requirements");
                }
                else
                {
                    MessageBox.Show($"Game '{gameName}' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void DisplaySystemRequirements(XElement gameElement, string requirementType)
        {
            XElement requirementsElement = gameElement.Element(requirementType);

            if (requirementsElement != null)
            {
                if (requirementType == "minimum_system_requirements")
                {
                    mincpu.Text = requirementsElement.Element("CPU")?.Value;
                    mingpu.Text = requirementsElement.Element("GPU")?.Value;
                    minRam.Text = requirementsElement.Element("RAM")?.Value;
                    minSpace.Text = requirementsElement.Element("STO")?.Value;
                    minSound.Text = requirementsElement.Element("Sound")?.Value;
                    minDX.Text = requirementsElement.Element("DX")?.Value;
                    minOS.Text = requirementsElement.Element("OS")?.Value;
                }
                else if (requirementType == "recommended_system_requirements")
                {
                    recCPU.Text = requirementsElement.Element("CPU")?.Value;
                    recGPU.Text = requirementsElement.Element("GPU")?.Value;
                    recRam.Text = requirementsElement.Element("RAM")?.Value;
                    recSpace.Text = requirementsElement.Element("STO")?.Value;
                    recSpace.Text = requirementsElement.Element("Sound")?.Value;
                    recDX.Text = requirementsElement.Element("DX")?.Value;
                    recOS.Text = requirementsElement.Element("OS")?.Value;
                }
            }
        }

    }
}
