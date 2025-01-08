using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotorFest;
using MotorFest.Data;
using MotorFest.Data.Entities;
using MotorFest.Models.Vehicle;
using MotorFest.Services.EngineTypeService;
using MotorFest.Services.VehicleCategoryService;
using MotorFest.Services.VehiclesService;

namespace MotorFest.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleCategoryService categoryService;
        private readonly IEngineTypeService engineTypeService;
        private readonly IVehicleService vehicleService;
        private readonly UserManager<MFUser> _userManager;

        public VehiclesController(IVehicleCategoryService categoryService, IEngineTypeService engineTypeService, IVehicleService vehicleService, UserManager<MFUser> userManager)
        {
            this.categoryService = categoryService;
            this.engineTypeService = engineTypeService;
            this.vehicleService = vehicleService;
            _userManager = userManager;
        }




        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = vehicleService.GetAll();
            return View(applicationDbContext);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await vehicleService.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["allCategories"] = categoryService.GetAll()
        .Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();

            ViewData["allEngineTypes"] = engineTypeService.GetAll()
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList();
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel vehicle)
        {
            ModelState.Remove("Owner");
            ModelState.Remove("OwnerId");
            ModelState.Remove("Category");
            ModelState.Remove("EngineType");
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                vehicle.OwnerId = user.Id;
                await vehicleService.Create(vehicle);
                return RedirectToAction(nameof(Index));
            }
            ViewData["allCategories"] = categoryService.GetAll()
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),
             Text = c.Name
         }).ToList();

            ViewData["allEngineTypes"] = engineTypeService.GetAll()
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList();
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await vehicleService.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["allCategories"] = categoryService.GetAll()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

            ViewData["allEngineTypes"] = engineTypeService.GetAll()
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList();
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,EngineTypeId,Manufacturer,Model,YearOfManufacture,Photo")] VehicleViewModel vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await vehicleService.Update(id, vehicle);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (vehicleService.GetById(id)==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["allCategories"] = categoryService.GetAll()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

            ViewData["allEngineTypes"] = engineTypeService.GetAll()
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList();
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await vehicleService.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await vehicleService.GetById(id);
            if (vehicle != null)
            {
                await vehicleService.Delete(id);
            }

            
            return RedirectToAction(nameof(Index));
        }
       

    }
}
