using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Parks.Models
{
    public class ParkSearcher
    {
        private IMemoryCache _cache;
        static readonly HttpClient client = new HttpClient();

        public ParkSearcher(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }


        public async Task<List<Park>> GetParks ()
        {
            List<Park> parkList;

            // Look for cache key.
            if (!_cache.TryGetValue("_ParkList", out parkList))
            {
                // Key not in cache, so get data.
                var response = client.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks").Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                parkList = JsonConvert.DeserializeObject<List<Park>>(responseBody);
                

                // Save data in cache and set the relative expiration time to one week
                _cache.Set("_ParkList", parkList, TimeSpan.FromDays(7));
            }

            return parkList;
        }

        public List<Park> GetParks (List<Park> parkList, string searchTerm)
        {
            var filteredList = from Park park in parkList
                           where park.ParkName.Contains(searchTerm) || park.Description.Contains(searchTerm)
                           select park;
            return filteredList.ToList();
        }
    }
}
