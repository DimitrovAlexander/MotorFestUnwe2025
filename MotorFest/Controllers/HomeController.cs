using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotorFest.Models;
using MotorFest.Services.EventService;

namespace MotorFest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventService eventService;
        public HomeController(ILogger<HomeController> logger, IEventService eventService)
        {
            _logger = logger;
            this.eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["ShowLoadingScreen"] = true;

            ViewData["UpcomingEvents"] =  eventService.GetUpcomingEvent();

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
