using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorFest.Services.LocationService;
using MotorFest.Services.UsersViewService;
using X.PagedList.Extensions;

namespace MotorFest.Controllers
{
    public class UsersViewController : Controller
    {
        private readonly IUsersViewService usersViewService;

        public UsersViewController(IUsersViewService usersViewService)
        {
            this.usersViewService = usersViewService;
        }



        // GET: UsersViewController
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var pagedLocations = users.ToPagedList(pageNumber, pageSize);

            return View(pagedLocations);
        }

        // GET: UsersViewController/Details/5
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


    }
}
