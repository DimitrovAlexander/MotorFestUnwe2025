using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorFest.Models.User;
using MotorFest.Services.LocationService;
using MotorFest.Services.UsersViewService;
using X.PagedList.Extensions;

namespace MotorFest.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersViewService usersViewService;
        public UsersController(IUsersViewService usersViewService)
        {
            this.usersViewService = usersViewService;
        }



        // GET: UsersController

    [Route("Users")]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["ShowLoadingScreen"] = true;
            var users = usersViewService.GetAll();

            // Filter by search string
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(l =>
                    l.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    l.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    l.RoleName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Paginate the results
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var pagedLocations = users.ToPagedList(pageNumber, pageSize);

            return View(pagedLocations);
        }

        // GET: UsersController/Details/5
        [Route("Users/Details")]

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await usersViewService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Route("Users/Delete")]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await usersViewService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.RoleName == "Administrator")
            {
                RedirectToAction(nameof(Index));
            }

            return View(user);
        }
        [Route("Users/Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(string UserId, UsersViewViewModel usersViewViewModel)
        {
            if (UserId == null)
            {
                return NotFound();
            }

           
            if (usersViewViewModel == null)
            {
                return NotFound();
            }
            usersViewService.Delete(UserId, usersViewViewModel);
            TempData["successMessage"] = "Успешно изтрихте потребител";
            return RedirectToAction(nameof(Index));
        }
    }
}
