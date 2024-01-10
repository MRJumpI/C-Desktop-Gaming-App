using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class RawgApiHelper
    {
        private readonly string apiKey;
        private readonly HttpClient httpClient;

        public RawgApiHelper(string apiKey)
        {
            this.apiKey = apiKey;
            this.httpClient = new HttpClient();
        }

        //get game data
        public async Task<RawgApiResult> GetGameData(string apiUrl)
        {
            try
            {
                // Adding the API key to the URL
                apiUrl += $"&key={apiKey}";

                // Making the API request
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Reading and deserializing the response content
                    string responseBody = await response.Content.ReadAsStringAsync();
                    RawgApiResult apiResult = Newtonsoft.Json.JsonConvert.DeserializeObject<RawgApiResult>(responseBody);
                    return apiResult;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
    }
}
