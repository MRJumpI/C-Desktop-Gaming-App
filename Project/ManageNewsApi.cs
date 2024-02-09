using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using NewAPIs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Project
{
    public class ManageNewsApi
    {

      

      

        public async Task<string> ExecuteApiRequest(string apiUrl)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "fbca8abe67msh4f1cf18d3c923dap1692c1jsne0b5dd319506");

                var response = await httpClient.GetStringAsync(apiUrl);
                return response;
            }
        }

        public Dictionary<string,string>[] ProcessEpicData(string epicData, int recordCount)
        {
            List<Dictionary<string, string>> epicGameData = new List<Dictionary<string, string>>();

            var epicArray = JArray.Parse(epicData);

            for (int i = 0; i < Math.Min(recordCount, epicArray.Count); i++)
            {
                epicGameData.Add(PrintEpicEntry(epicArray[i]));
            }

            return epicGameData.ToArray();
        }

        public  Dictionary<string, string>[] ProcessMMOData(string mmoData, int recordCount)
        {
            List<Dictionary<string,string>> mmoGameData = new List<Dictionary<string, string>>();
            var mmoArray = JArray.Parse(mmoData);

            for (int i = 0; i < Math.Min(recordCount, mmoArray.Count); i++)
            {
                mmoGameData.Add(PrintMMOEntry(mmoArray[i]));
            }

            return mmoGameData.ToArray();
        }

        //change this
        public static Dictionary<string, string> PrintEpicEntry(JToken entry)
        {
            Dictionary<string,string> entryData = new Dictionary<string, string>();
            var url = entry["url"].ToString();
            var date = entry["date"].ToString();
            var shortDescription = RemoveHtmlTags(entry["short"].ToString());
            var content = RemoveHtmlTags(entry["content"].ToString());
            var title = RemoveHtmlTags(entry["title"].ToString());
            var author = RemoveHtmlTags(entry["author"].ToString());

            entryData = new Dictionary<string, string>{
                
                {"URL", url},
                { "Date", date },
                { "ShortDescription",shortDescription },
                { "Content",content },
                { "Title", title},
                { "Author", author}
            };
            return entryData;
        }

        public static Dictionary<string, string> PrintMMOEntry(JToken entry)
        {
            Dictionary<string,string> entryData = new Dictionary<string, string>();
            var id = entry["id"].ToString();
            var title = RemoveHtmlTags(entry["title"].ToString());
            var shortDescription = RemoveHtmlTags(entry["short_description"].ToString());
            var thumbnail = entry["thumbnail"].ToString();
            var mainImage = entry["main_image"].ToString();
            var articleContent = RemoveHtmlTags(entry["article_content"].ToString());
            var articleUrl = entry["article_url"].ToString();

            entryData =new Dictionary<string, string>{
                { "Title",title},
                { "ShortDescription",shortDescription },
                { "Thumbnail", thumbnail },
                { "MainImage", mainImage},
                { "ArticleContent",  articleContent },
                { "ArticleUrl",articleUrl}

            };
            
            return entryData;
        }

        public static string RemoveHtmlTags(string input)
        {
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(input);

            // Use HtmlAgilityPack's Text property to get only the text content without HTML tags
            string textContent = htmlDocument.DocumentNode.InnerText.Trim();

            // Optionally, you can further clean up by replacing multiple whitespaces with a single space
            textContent = Regex.Replace(textContent, @"\s+", " ");

            return textContent;
        }

      
        
        public async Task<List<NewsItem>> GetNewsData(string apiUrl, string apiKey, string query, string country, string lang, int count)
        {
            List<NewsItem> newsItems = new List<NewsItem>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "real-time-news-data.p.rapidapi.com");

                var queryParams = new
                {
                    query,
                    country,
                    lang
                };

                string queryString = string.Join("&",
                    queryParams.GetType().GetProperties()
                        .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(queryParams).ToString())}")
                );

                string requestUrl = $"{apiUrl}?{queryString}&count={count}";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(responseBody);

                    if (jsonResponse["status"].ToString() == "OK")
                    {
                        JArray data = (JArray)jsonResponse["data"];
                        foreach (var item in data)
                        {
                            NewsItem newsItem = new NewsItem
                            {
                                Title = item["title"].ToString(),
                                Link = item["link"].ToString(),
                                PhotoUrl = item["photo_url"].ToString(),
                                PublishDate = item["published_datetime_utc"].ToString(),
                                SourceUrl = item["source_url"].ToString()
                            };

                            newsItems.Add(newsItem);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {jsonResponse["status"].ToString()}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP Request Error: {ex.Message}");
                }
            }

            return newsItems;
        }


      
    }
}
