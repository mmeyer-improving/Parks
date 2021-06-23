using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Parks.Models;

namespace Parks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ParkSearcher _searcher;
        static readonly HttpClient client = new HttpClient();
        private IMemoryCache _cache;

        public HomeController(ILogger<HomeController> logger, ParkSearcher searcher, IMemoryCache memoryCache)
        {
            _logger = logger;
            _searcher = searcher;
            _cache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ParkData(string? search)
        {
            List<Park> parkList;

            // Look for cache key.
            if (!_cache.TryGetValue("_ParkList", out parkList))
            {
                // Key not in cache, so get data.
                parkList = _searcher.GetParks(client);

                // Save data in cache and set the relative expiration time to one day
                _cache.Set("_ParkList", parkList, TimeSpan.FromSeconds(15));
            }


            if (String.IsNullOrEmpty(search))
            {
                ViewBag.Data = parkList;
            }
            else 
            {
                ViewBag.Data = _searcher.GetParks(parkList, search);
            }

            return View("ParkData");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
