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
using MotorFest.Models.Event;
using MotorFest.Models.Vehicle;
using MotorFest.Services.EngineTypeService;
using MotorFest.Services.EventService;
using MotorFest.Services.EventsService;
using MotorFest.Services.LocationService;
using MotorFest.Services.VehicleCategoryService;
using MotorFest.Services.VehiclesService;

namespace MotorFest.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService eventService;
        private readonly IVehicleCategoryService vehicleCategoryService;
        private readonly IVehicleService vehicleService;
        private readonly ILocationService locationService;
        private readonly UserManager<MFUser> _userManager;
        public EventsController(IEventService eventService, IVehicleCategoryService vehicleCategoryService, ILocationService locationService, UserManager<MFUser> userManager, IVehicleService vehicleService)
        {
            this.eventService = eventService;
            this.vehicleCategoryService = vehicleCategoryService;
            this.locationService = locationService;
            _userManager = userManager;
            this.vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            var events = eventService.GetAll();
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = vehicleCategoryService.GetAll();
            var user = await _userManager.GetUserAsync(User);
            var model = new EventViewModel
            {
                VehicleCategories = categories.Select(c => new CheckBoxItem
                {
                    Id = c.Id,
                    CategoryName = c.Name,
                    IsChecked = false
                }).ToList(),


            };
            ViewData["allLocations"] = locationService.GetAll()
       .Select(c => new SelectListItem
       {
           Value = c.Id.ToString(),
           Text = c.Name
       }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            ModelState.Remove("Location");
            ModelState.Remove("Organizer");
            ModelState.Remove("OrganizerId");
            ModelState.Remove("HasVehicles");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            model.OrganizerId = user.Id;
            await eventService.Create(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventDetails = await eventService.GetById(id);
            if (eventDetails == null) return NotFound();
            return View(eventDetails);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await eventService.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Calendar()
        {
            var events = eventService.GetAll(); // Fetch all events using the service
            return View(events);
        }
        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mfEvent = await eventService.GetById(id);
            if (mfEvent == null)
            {
                return NotFound();
            }
            ViewData["allLocations"] = locationService.GetAll()
      .Select(c => new SelectListItem
      {
          Value = c.Id.ToString(),
          Text = c.Name
      }).ToList();


            return View(mfEvent);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventViewModel eventViewModel)
        {
            if (id != eventViewModel.Id)
            {
                return NotFound();
            }
            ModelState.Remove("HasVehicles");
            ModelState.Remove("Location");
            ModelState.Remove("Organizer");
            ModelState.Remove("OrganizerId");
            if (ModelState.IsValid)
            {
                try
                {
                    await eventService.Update(id, eventViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (eventService.GetById(id) == null)
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


            ViewData["allLocations"] = locationService.GetAll()
     .Select(c => new SelectListItem
     {
         Value = c.Id.ToString(),
         Text = c.Name
     }).ToList();
            return View(eventViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Subscribe(int id)
        {
            // Намираме събитието
            EventViewModel evnt = await eventService.GetById(id);
            if (evnt == null)
            {
                return NotFound("Събитието не е намерено.");
            }

            // Взимаме превозните средства на потребителя
            var userId = _userManager.GetUserId(User); // Предполагаем метод за вземане на ID на текущия потребител
            List<VehicleViewModel> vehicles = vehicleService.GetByUserId(userId).ToList();

            // Създаваме ViewModel
            var viewModel = new EventSubscribeViewModel
            {
                EventId = evnt.Id,
                EventName = evnt.Name,
                UserVehicles = vehicles.Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = $"{v.Manufacturer} {v.Model} ({v.YearOfManufacture})"
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(EventSubscribeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Проверяваме дали превозното средство е валидно за потребителя
            var userId = _userManager.GetUserId(User);
            VehicleViewModel vehicle = await vehicleService.GetById(model.SelectedVehicleId);
            if (vehicle == null || vehicle.OwnerId != userId)
            {
                ModelState.AddModelError("", "Избраното превозно средство не е валидно.");
                return View(model);
            }

            // Добавяме връзка между събитието и превозното средство
            var success = eventService.RegisterVehicleForEvent(model.EventId, vehicle.Id);
            if (!success)
            {
                ModelState.AddModelError("", "Неуспешно записване за събитието.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Успешно се записахте за събитието!";
            return RedirectToAction("Details", new { id = model.EventId });
        }

        public IActionResult MyEvents()
        {
            var userId = _userManager.GetUserId(User);
            var events = eventService.GetAllByUserParticipating(userId); 
            return View(events);
        }
    }
}
