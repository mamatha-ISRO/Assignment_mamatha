using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using Nancy.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;


namespace abcalbums.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        // This getAlbums will get any album that contains the search string in it's title
        // or all albums if there is no search string
        [HttpGet]
        public async Task<List<AlbumFormat>> getAlbums(string searchString)
        {
            // If there is no search string return the whole list of albums
            if (searchString == null)
            {
                // Using HttpClient to get the albums from JSONplaceholder.typicode.com 
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(@"https://jsonplaceholder.typicode.com");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string storageString = await client.GetStringAsync("/albums");

                    var albumArray = JsonConvert.DeserializeObject<List<AlbumFormat>>(storageString);


                    return albumArray;
                }
            }
            // If there is a search string, get the list of albums and filter for albums with the search string in their title
            else
            {
                // Using HttpClient to get the albums from JSONplaceholder.typicode.com 
                using (HttpClient client = new HttpClient())
                {
                    string uriString = "https://jsonplaceholder.typicode.com";

                    client.BaseAddress = new Uri(uriString);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // store the gathered information in storageString until we can filter it out.
                    string storageString = await client.GetStringAsync("/albums");

                    var albumArray = JsonConvert.DeserializeObject<List<AlbumFormat>>(storageString);


                    // Filter the array of albums by the search query (case insensitive)

                    //{
                    // Create a new list of AlbumFormat so that albums that contain the searchString can be added to it during the filtering process
                    var filteredAlbumArray = new List<AlbumFormat>();


                    // This foreach loop detects whether the seachString is contained in the title of the album
                    // THIS DOES MEAN THAT IF YOU SEARCH FOR SOMETHING THAT IS CONTAINED IN A LARGER WORD, THE LARGER WORD WILL ALSO BE FILTERED IN
                    //(i.e. search "we" and "welcome" gets filtered in)
                    foreach (AlbumFormat album in albumArray)
                    {
                        bool stringPresent = album.title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0;
                        if (stringPresent == true)
                        {
                            // If the searchString is present in the title then add that album to the filtered array
                            filteredAlbumArray.Add(album);
                        }
                    }
                    //}
                    // Return the filtered array to the user
                    return filteredAlbumArray;
                }
            }
        }


        public class AlbumFormat
        {
            public string userId { get; set; }
            public string id { get; set; }
            public string title { get; set; }

        }
    }
}
