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
using MotorFest.Services.LocationService;
using MotorFest.Services.VehicleCategoryService;
using MotorFest.Services.VehiclesService;
using X.PagedList.Extensions;

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
        public async Task<IActionResult> Index(string searchString, int? page)
        {

            var allVehicles = vehicleService.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                allVehicles = allVehicles.Where(v =>
                    v.Manufacturer.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    v.Model.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    v.YearOfManufacture.ToString().Contains(searchString) ||
                    v.Category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    v.EngineType.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            Dictionary<int, bool> isVehicleInEvent = new Dictionary<int, bool>();
            foreach (var vehicle in allVehicles)
            {
                isVehicleInEvent.Add(vehicle.Id, vehicleService.IsParticipatingInFutureEvents(vehicle.Id));
            }
            ViewData["carDictionary"] = isVehicleInEvent;

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(allVehicles.ToPagedList(pageNumber, pageSize));
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
        public async Task<IActionResult> Create(VehicleViewModel vehicle, IFormFile photo)
        {
            ModelState.Remove("Owner");
            ModelState.Remove("OwnerId");
            ModelState.Remove("Category");
            ModelState.Remove("EngineType");
            ModelState.Remove("Photo");
            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    vehicle.Photo = "/images/" + fileName;
                }
                else
                {
                    ModelState.AddModelError("Photo", "Снимката е задължителна.");
                    return View(vehicle);
                }
                var user = await _userManager.GetUserAsync(User);
                vehicle.OwnerId = user.Id;
                if(await vehicleService.Create(vehicle))
                {
                    TempData.Add("successMessage", "Успешно добавихте превозно средство");
                return RedirectToAction(nameof(Index));
                };
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,EngineTypeId,Manufacturer,Model,YearOfManufacture,Photo")] VehicleViewModel vehicle,IFormFile photo)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    vehicle.Photo = "/images/" + fileName;
                }
                else if (string.IsNullOrEmpty(vehicle.Photo))
                {
                    ModelState.AddModelError("Photo", "Снимката е задължителна.");
                    return View(vehicle);
                }

                try
                {
                await vehicleService.Update(id, vehicle);
                TempData["successMessage"] = "Успешно редактирахте превозното средство";
                return RedirectToAction(nameof(Index));

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
