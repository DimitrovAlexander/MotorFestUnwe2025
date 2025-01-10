using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotorFest.Models;

namespace MotorFest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

        // Пример за използване
        bool isPasswordValid = VerifyPassword("AQAAAAIAAYagAAAAEEa4wODr+uoRihazwhLHiWVCQD+XxIzgc6G+rjeUxZbWrLXd29UjOID1F5jtNSL9MA==", "123123");
            return View();
        }
        private bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();
            var result = passwordHasher.VerifyHashedPassword(new IdentityUser(), hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
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
