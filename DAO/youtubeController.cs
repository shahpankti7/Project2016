using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project2016.DAO
{
    public class youtubeController : ApiController
    {
    }
}

//---------.net---------------

/*

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.Samples
{
   /// <summary>
   /// YouTube Data API v3 sample: search by keyword.
   /// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
   /// See https://developers.google.com/api-client-library/dotnet/get_started
   ///
   /// Set ApiKey to the API key value from the APIs & auth > Registered apps tab of
   ///   https://cloud.google.com/console
   /// Please ensure that you have enabled the YouTube Data API for your project.
   /// </summary>
   internal class Search
   {
       [STAThread]
       static void Main(string[] args)
       {
           Console.WriteLine("YouTube Data API: Search");
           Console.WriteLine("========================");

           try
           {
               new Search().Run().Wait();
           }
           catch (AggregateException ex)
           {
               foreach (var e in ex.InnerExceptions)
               {
                   Console.WriteLine("Error: " + e.Message);
               }
           }

           Console.WriteLine("Press any key to continue...");
           Console.ReadKey();
       }

       private async Task Run()
       {
           var youtubeService = new YouTubeService(new BaseClientService.Initializer()
           {
               ApiKey = "REPLACE_ME",
               ApplicationName = this.GetType().ToString()
           });

           var searchListRequest = youtubeService.Search.List("snippet");
           searchListRequest.Q = "Google"; // Replace with your search term.
           searchListRequest.MaxResults = 50;

           // Call the search.list method to retrieve results matching the specified query term.
           var searchListResponse = await searchListRequest.ExecuteAsync();

           List<string> videos = new List<string>();
           List<string> channels = new List<string>();
           List<string> playlists = new List<string>();

           // Add each result to the appropriate list, and then display the lists of
           // matching videos, channels, and playlists.
           foreach (var searchResult in searchListResponse.Items)
           {
               switch (searchResult.Id.Kind)
               {
                   case "youtube#video":
                       videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                       break;

                   case "youtube#channel":
                       channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                       break;

                   case "youtube#playlist":
                       playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                       break;
               }
           }

           Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
           Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
           Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
       }
   }
}*/


//------------------script searchByKeyword---------------
/**
 * This function searches for videos related to the keyword 'dogs'. The video IDs and titles
 * of the search results are logged to Apps Script's log.
 *
 * Note that this sample limits the results to 25. To return more results, pass
 * additional parameters as documented here:
 *   https://developers.google.com/youtube/v3/docs/search/list
 */
/**function searchByKeyword()
{
    var results = YouTube.Search.list('id,snippet', { q: 'dogs', maxResults: 25});
  for(var i in results.items)
    {
        var item = results.items[i];
        Logger.log('[%s] Title: %s', item.id.videoId, item.snippet.title);
    }
}
**/

//--------------------script searchByTopic------------------

/**
* This function searches for videos that are associated with a particular Freebase
* topic, logging their video IDs and titles to the Apps Script log. This example uses
* the topic ID for Google Apps Script.
*
* Note that this sample limits the results to 25. To return more results, pass
* additional parameters as documented here:
*   https://developers.google.com/youtube/v3/docs/search/list
*/
/**function searchByTopic()
{
    var mid = '/m/0gjf126';
    var results = YouTube.Search.list('id,snippet', { topicId: mid, maxResults: 25});
  for(var i in results.items)
    {
        var item = results.items[i];
        Logger.log('[%s] Title: %s', item.id.videoId, item.snippet.title);
    }
}**/
