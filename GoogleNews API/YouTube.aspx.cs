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

/// Name:   Cory Siebler
/// ASUID:  1000832292
/// Email:  csiebler@asu.edu
/// Class:  ASU CSE 445 (#11845)
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