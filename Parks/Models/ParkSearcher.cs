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
        private List<Park> _parkList = new List<Park>();

        public ParkSearcher(HttpClient client)
        {

        }

        public async Task<List<Park>> GetParks (HttpClient client)
        {
            var jObject = await client.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks");
            List<Park> parkList = JsonConvert.DeserializeObject<List<Park>>(jObject);
        }

        public List<Park> GetEntityReferences()
        {
            

            return parkList;
        }
    }
}
