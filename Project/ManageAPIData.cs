using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class ManageAPIData
    {

        
        public async Task<Dictionary<string, object>[]> getTenData()
        {
            try
            {
                List<Dictionary<string, object>> toptenGamesData = new List<Dictionary<string, object>>();

                // Use this space to test and access the parsed data.
                // Example usage:
                string apiKey = "1db5901aac8e4f32b06bd29e31a675d7";
                // Get the current date
                DateTime currentDate = DateTime.Now;

                // Calculate the start date as 2 years before the current date
                DateTime startDate = currentDate.AddYears(-2);

                // Format the date parameters for the API URL
                string dateRange = $"{startDate.Year}-{startDate.Month:D2}-{startDate.Day:D2},{currentDate.Year}-{currentDate.Month:D2}-{currentDate.Day:D2}";

                // Construct the API URL with the dynamic date range
                string apiUrl = $"https://api.rawg.io/api/games?dates={dateRange}&ordering=-rating&page=1&page_size=10&key={apiKey}";

                RawgApiHelper apiHelper = new RawgApiHelper(apiKey);
                RawgApiResult apiResult = await apiHelper.GetGameData(apiUrl);

                Console.WriteLine("API Call Successful");


                if (apiResult != null)
                {
                    // Iterate only over the first 10 games
                    for (int i = 0; i < 10 && i < apiResult.Games.Count; i++)
                    {
                        var game = apiResult.Games[i];

                        // Initialize variables for each iteration
                        List<string> platformsGame = new List<string>();
                        List<string> storesGame = new List<string>();
                        List<string> screeshotsGame = new List<string>();

                        //getting name
                        string nameGame = game.Name;

                        foreach (var platform in game.Platforms)
                        {
                            //getting PlatForms
                            platformsGame.Add(platform.PlatformDetail.Name + " - ");
                        }

                        if (game.Stores != null && game.Stores.Count > 0)
                        {
                            foreach (var storeItem in game.Stores)
                            {
                                // Accessing store information
                                var store = storeItem.Store;
                                storesGame.Add(store.Name);
                            }
                        }
                        else
                        {
                            storesGame.Add("No Store");
                        }

                        string releaseDateGame = game.Released.ToString();
                        string ratingGame = game.Rating.ToString();

                        foreach (var screenshot in game.ShortScreenshots)
                        {
                            screeshotsGame.Add(screenshot.Image);
                        }

                        // Create a dictionary with the current data
                        Dictionary<string, object> gameData = new Dictionary<string, object>
                        {
                            { "Name", nameGame },
                            { "Platforms", string.Join("", platformsGame) },
                            { "rating", ratingGame },
                            { "Stores", storesGame.ToArray() },
                            { "releaseDate", releaseDateGame },
                            { "screenshots", screeshotsGame.ToArray() }
                        };

                        // Add the current game's data to the list
                        toptenGamesData.Add(gameData);
                    }

                    // Convert the list to an array before returning
                    return toptenGamesData.ToArray();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show("Sorry Mate! There issue in your internet or back sever not Getting Data!!");
                return new Dictionary<string, object>[0];
            }

            // If an exception occurs, return an empty array or handle it appropriately
            return new Dictionary<string, object>[0];
        }

        // Top 10 EA GAMES
        public async Task<Dictionary<string, object>[]> getTop10EAGames()
        {
            List<Dictionary<string, object>> eaTopTenGamesData = new List<Dictionary<string, object>>();
            try {
               

                // Example usage:
                string apiKey = "1db5901aac8e4f32b06bd29e31a675d7";
            string apiUrl = "https://api.rawg.io/api/games?ordering=-rating&developers=109&key=" + apiKey;

            RawgApiHelper apiHelper = new RawgApiHelper(apiKey);
            RawgApiResult apiResult = apiHelper.GetGameData(apiUrl).Result;

            if (apiResult != null)
            {
                Console.WriteLine("Top 10 EA Games:");
                // Limit the loop to iterate only over the first 10 games
                for (int i = 0; i < 10 && i < apiResult.Games.Count; i++)
                {
                        var game = apiResult.Games[i];

                        // Initialize variables for each iteration
                        List<string> platformsGame = new List<string>();
                        List<string> screeshotsGame = new List<string>();

                        //getting name
                        string nameGame = game.Name.ToString();

                        foreach (var platform in game.Platforms)
                        {
                            //getting PlatForms
                            platformsGame.Add(platform.PlatformDetail.Name.ToString() + " - ");
                        }


                        string ratingGame = game.Rating.ToString();

                        foreach (var screenshot in game.ShortScreenshots)
                        {
                            screeshotsGame.Add(screenshot.Image.ToString());
                        }

                        // Create a dictionary with the current data
                        Dictionary<string, object> gameData = new Dictionary<string, object>
                            {
                                { "Name", nameGame },
                                { "Platforms", string.Join("", platformsGame) },
                                { "rating", ratingGame },
                                { "screenshots", screeshotsGame.ToArray() }
                            };

                        // Add the current game's data to the list
                        eaTopTenGamesData.Add(gameData);
                    }

                    // Convert the list to an array before returning
                    return eaTopTenGamesData.ToArray();
                }
            

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show("Sorry Mate! There issue in your internet or back sever not Getting Data!!");
                return eaTopTenGamesData.ToArray();
            }

            // If an exception occurs, return an empty array or handle it appropriately
            return eaTopTenGamesData.ToArray();

        }


        public async Task<Dictionary<string, object>[]> getFiveUpcomingGames()
        {
            List<Dictionary<string, object>> fiveUpcomingGames = new List<Dictionary<string, object>>();
            try
            {
                

                string apiKey = "1db5901aac8e4f32b06bd29e31a675d7";
                string apiUrl;
                DateTime currentDate = DateTime.Now;

                // Calculate the start date as the first day of the next month
                DateTime startDate = currentDate.AddMonths(1);
                startDate = new DateTime(startDate.Year, startDate.Month, 1);

                // Calculate the end date as six months from the start date
                DateTime endDate = startDate.AddMonths(6).AddDays(-1);

                // Format the date parameters for the API URL
                string dateRange = $"{startDate.Year}-{startDate.Month:D2}-{startDate.Day:D2},{endDate.Year}-{endDate.Month:D2}-{endDate.Day:D2}";

                // Construct the API URL with the dynamic date range
                apiUrl = $"https://api.rawg.io/api/games?dates={dateRange}&ordering=-added&rating_top=3&key={apiKey}";


                RawgApiHelper apiHelper = new RawgApiHelper(apiKey);
                RawgApiResult apiResult = apiHelper.GetGameData(apiUrl).Result;

                if (apiResult != null)
                {
                    // Display information about 5 new upcoming games
                    for (int i = 0; i < 5 && i < apiResult.Games.Count; i++)
                    {
                        var game = apiResult.Games[i];
                        List<string> platformsGame = new List<string>();
                        string nameGame=game.Name.ToString();
                        foreach (var platform in game.Platforms)
                        {
                            platformsGame.Add(platform.PlatformDetail.Name.ToString()+" - ");
                        }
                        string releasedDate=game.Released.ToString();
                        string backgroundImage=game.BackgroundImage.ToString();
                        string rating=game.Rating.ToString();

                        // Create a dictionary with the current data
                        Dictionary<string, object> gameData = new Dictionary<string, object>
                            {
                                { "Name", nameGame },
                                { "Platforms", string.Join("", platformsGame) },
                                { "Releaseddate", releasedDate },
                                { "BackgroundImage", backgroundImage }
                            };

                        // Add the current game's data to the list
                        fiveUpcomingGames.Add(gameData);
                    }

                    // Convert the list to an array before returning
                    return fiveUpcomingGames.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show("Sorry Mate! There issue in your internet or back sever not Getting Data!!");
                return fiveUpcomingGames.ToArray();
            }

            // If an exception occurs, return an empty array or handle it appropriately
            return fiveUpcomingGames.ToArray();
        }

    }
}
