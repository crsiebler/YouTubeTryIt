using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using System.IO;
using System.Text;
using System.Data;

using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YouTube_API
{
    public partial class YouTubeSearch : System.Web.UI.Page
    {
        /// <summary>
        /// Page Load Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        [WebMethod]
        public static Video[] GetVideos(string topic)
        {
            /*
            // Initialize the List of News Articles
            List<Video> videos = new List<Video>();

            // Initialize the Google News RSS Feed request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                "https://www.googleapis.com/youtube/v3/search?part=snippet&q=" + topic.Replace(" ", "+") + "&type=video&maxResults=10&key=AIzaSyBAKxqcK17_ngThmKCHFLw4g1_aB1eOHDY"
            );

            // Specify GET Method Request
            request.Method = "GET";

            // Perform the Request
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Check the Response State Code for Success
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                // Check the Character Set of the Response
                if (response.CharacterSet == "")
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                // Convert the Stream in a JSON string
                string data = readStream.ReadToEnd();

                // Initialize a DataSet to store the parsed information
                DataSet ds = new DataSet();
                StringReader reader = new StringReader(data);
                ds.ReadXml(reader);
                DataTable articleTable = new DataTable();

                // Make sure the response is not empty
                if (ds.Tables.Count > 0)
                {
                    articleTable = ds.Tables["item"];

                    // Loop through each RSS element and store it as a NewsArticle
                    foreach (DataRow row in articleTable.Rows)
                    {
                        Video video = new Video();
                        video.id = row["id"]["videoId"].ToString(); 
                        video.title = row["snippet"]["title"].ToString();
                        video.date = row["snippet"]["publishedAt"].ToString();
                        video.description = row["snippet"]["description"].ToString();
                        videos.Add(video);
                    }
                }
            }

            return videos.ToArray();
            */
            List<Video> videos = new List<Video>();

            var youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBAKxqcK17_ngThmKCHFLw4g1_aB1eOHDY",
                ApplicationName = "YouTube Search TryIt"
            });

            var searchListRequest = youTubeService.Search.List("snippet");
            searchListRequest.Q = topic.Replace(" ", "+");
            searchListRequest.MaxResults = 10;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            searchListRequest.Type = "video";

            SearchListResponse response = searchListRequest.Execute();

            foreach (SearchResult result in response.Items)
            {
                Video video = new Video();
                video.id = result.Id.VideoId;
                video.title = result.Snippet.Title;
                video.date = result.Snippet.PublishedAt.ToString();
                video.description = result.Snippet.Description;
                videos.Add(video);
            }

            return videos.ToArray();
        }

        public class Video
        {
            public string title { get; set; }
            public string date { get; set; }
            public string id { get; set; }
            public string description { get; set; }
        }
    }
}