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
            List<Park> parkList = _searcher.GetParks();


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

        /*public Task<IActionResult> ParkDataJS(string? search)
        {
            var data = await _searcher.getParks();

            return Json(data);
        }*/

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
