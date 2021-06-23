using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parks.Models;

namespace Parks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ParkSearcher _searcher;
        static readonly HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger, ParkSearcher searcher)
        {
            _logger = logger;
            _searcher = searcher;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ParkData(string? search)
        {
            if (String.IsNullOrEmpty(search))
            {
                ViewBag.Data = _searcher.GetParks(client);
            }
            else 
            {
                ViewBag.Data = _searcher.GetParks(client, search);
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
