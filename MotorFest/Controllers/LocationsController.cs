using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotorFest;
using MotorFest.Data;
using MotorFest.Models.Location;
using MotorFest.Services.LocationService;

using MotorFest.Services.LocationService;
using X.PagedList.Extensions;

namespace MotorFest.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILocationService addressService;

        public LocationsController(ILocationService addressService)
        {
            this.addressService = addressService;
        }

        // GET:
        // es
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            ViewData["CurrentFilter"] = searchString;

            var locations = addressService.GetAll();

            // Filter by search string
            if (!string.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(l =>
                    l.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    l.City.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    l.Municipality.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Paginate the results
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var pagedLocations = locations.ToPagedList(pageNumber, pageSize);

            return View(pagedLocations);
        }

        // GET: Locationes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await addressService.GetById(id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Locationes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locationes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationViewModel address)
        {
            if (ModelState.IsValid)
            {
                await addressService.Create(address);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Locationes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await addressService.GetById(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,City,FullAddress,Municipality,LastUpdate")] LocationViewModel address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   addressService.Update(id,address);
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await addressService.GetById(id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await addressService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
