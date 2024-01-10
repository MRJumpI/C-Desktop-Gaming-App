using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class GameDetails
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("playtime")]
        public int Playtime { get; set; }

        [JsonProperty("platforms")]
        public List<Platform> Platforms { get; set; }

        [JsonProperty("stores")]
        public List<StoreItem> Stores { get; set; } = new List<StoreItem>();


        [JsonProperty("released")]
        public DateTime Released { get; set; }

        [JsonProperty("background_image")]
        public string BackgroundImage { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("ratings_count")]
        public int RatingsCount { get; set; }

        [JsonProperty("reviews_text_count")]
        public int ReviewsTextCount { get; set; }

        [JsonProperty("short_screenshots")]
        public List<ShortScreenshot> ShortScreenshots { get; set; }

        // Add more properties as needed
    }

}
