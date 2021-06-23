using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Parks.Models
{
    public class ParkSearcher
    {
        public List<Park> GetParks (HttpClient client)
        {
            var response = client.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            List<Park> parkList = JsonConvert.DeserializeObject<List<Park>>(responseBody);
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
