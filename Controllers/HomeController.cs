using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoRSSExample.Bus.Commands;
using MongoRSSExample.Bus.DTOs;
using MongoRSSExample.Bus.Queries;
using MongoRSSExample.Models;
using MongoRSSExample.Services;

namespace MongoRSSExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly RSSService _rssService;
        public HomeController(ILogger<HomeController> logger, RSSService rssService)
        {
            _logger = logger;
            _rssService = rssService;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult GetRSS()
        {
            //Create an object
            RSSFeed rss = MyRSS.Instance(_rssService);
            // Create a command for the object
            RSSCommand command = new RSSCommand(rss);
            // Invoke the command 
            var commandInvoker = new CommandInvoker(command);
            // URL goes from app settings or a database         
            return PartialView("_RSSPartialView", commandInvoker.Read(DateTime.Now.AddDays(-1)));
        }
    }
}
