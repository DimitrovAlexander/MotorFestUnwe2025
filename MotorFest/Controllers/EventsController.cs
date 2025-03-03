using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MotorFest;
using MotorFest.Data;
using MotorFest.Data.Entities;
using MotorFest.Models.Event;
using MotorFest.Models.Vehicle;
using MotorFest.Services.EngineTypeService;
using MotorFest.Services.EventService;
using MotorFest.Services.EventsService;
using MotorFest.Services.EventsViewService;
using MotorFest.Services.LocationService;
using MotorFest.Services.UsersViewService;
using MotorFest.Services.VehicleCategoryService;
using MotorFest.Services.VehiclesService;
using X.PagedList.Extensions;

namespace MotorFest.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService eventService;
        private readonly IVehicleCategoryService vehicleCategoryService;
        private readonly IVehicleService vehicleService;
        private readonly ILocationService locationService;
        private readonly UserManager<MFUser> _userManager;
        private readonly IEngineTypeService engineTypeService;
        private readonly IEventsViewService eventsViewService;

        public EventsController(IEventService eventService, IVehicleCategoryService vehicleCategoryService, ILocationService locationService, UserManager<MFUser> userManager, IVehicleService vehicleService, IEngineTypeService engineTypeService, IEventsViewService eventsViewService)
        {
            this.eventService = eventService;
            this.vehicleCategoryService = vehicleCategoryService;
            this.locationService = locationService;
            _userManager = userManager;
            this.vehicleService = vehicleService;
            this.engineTypeService = engineTypeService;
            this.eventsViewService = eventsViewService;
        }

        [Authorize(Roles = "Administrator,Organizer")]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            ViewData["ShowLoadingScreen"] = true;
            ICollection<EventViewModel> events = GetEvents();

            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                           e.Location.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                           e.EventDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                            e.VehicleCategories.Any(vc => vc.CategoryName != null && vc.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||

                                           e.EngineTypes.Any(et => et.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                          ).ToList();
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var pagedEvents = events.ToPagedList(pageNumber, pageSize);

            ViewData["CurrentFilter"] = searchString;
            return View(pagedEvents);
        }

        private ICollection<EventViewModel> GetEvents()
        {
            if (User.IsInRole("Administrator"))
            {
                return eventService.GetAll();
            }
            else
            {
                return eventService.GetAllByOrganizer(User.Claims.FirstOrDefault().Value);
            }
        }

        [Authorize(Roles = "Administrator,Organizer")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var categories = vehicleCategoryService.GetAll();
            var engineTypes = engineTypeService.GetAll();
            var user = await _userManager.GetUserAsync(User);
            var model = new EventViewModel
            {
                VehicleCategories = categories.Select(c => new CheckBoxItem
                {
                    Id = c.Id,
                    CategoryName = c.Name,
                    IsChecked = false
                }).ToList(),
                EngineTypes = engineTypes.Select(e => new CheckBoxItem
                {
                    Id = e.Id,
                    CategoryName = e.Name,
                    IsChecked = false
                }).ToList()

            };
            ViewData["allLocations"] = locationService.GetAll()
       .Select(c => new SelectListItem
       {
           Value = c.Id.ToString(),
           Text = c.Name
       }).ToList();
            return View(model);
        }
        [Authorize(Roles = "Administrator,Organizer")]
        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model, IFormFile eventLogoFile, List<IFormFile> eventPhotoFiles)
        {
            ModelState.Remove("Location");
            ModelState.Remove("Organizer");
            ModelState.Remove("OrganizerId");
            ModelState.Remove("HasVehicles");
            ModelState.Remove("eventLogoFile");
            ModelState.Remove("EventLogo");
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            // Запазване на логото
            if (eventLogoFile != null && eventLogoFile.Length > 0)
            {
                string logoDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/eventLogos");
                Directory.CreateDirectory(logoDirectory);

                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(eventLogoFile.FileName)}";
                string filePath = Path.Combine(logoDirectory, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await eventLogoFile.CopyToAsync(fileStream);
                }

                model.EventLogo = $"/images/eventLogos/{uniqueFileName}";
            }

            // Запазване на снимките от събитието
            if (eventPhotoFiles != null && eventPhotoFiles.Any())
            {
                string photosDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/eventPhotos");
                Directory.CreateDirectory(photosDirectory);

                foreach (var photo in eventPhotoFiles)
                {
                    if (photo.Length > 0)
                    {
                        string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
                        string filePath = Path.Combine(photosDirectory, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await photo.CopyToAsync(fileStream);
                        }

                        model.EventPhotos.Add($"/images/eventPhotos/{uniqueFileName}");
                    }
                }
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

            var userId = _userManager.GetUserId(User);
            var isOrganizer = eventDetails.OrganizerId == userId;
            var isAdmin = User.IsInRole("Administrator");

            if (isOrganizer || isAdmin)
            {
                var registeredVehicles = eventService.GetRegisteredVehiclesForEvent(id);
                ViewData["RegisteredVehicles"] = registeredVehicles;
            }

            bool isRegistered = eventService.IsUserRegisteredForEvent(userId, id);
            ViewData["IsRegistered"] = isRegistered;

            return View(eventDetails);
        }

        [Authorize(Roles = "Administrator,Organizer")]
        public async Task<IActionResult> Delete(int id)
        {
            var mfEvent = await eventService.GetById(id);
            return View(mfEvent);
        }
        [Authorize(Roles = "Administrator,Organizer")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id, EventViewModel eventViewModel)
        {
            await eventService.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Calendar()
        {
            var events = eventService.GetAll().Where(x => x.IsCanceled == false); // Fetch all events using the service
            return View(events);
        }
        // GET: Events/Edit/5
        [Authorize(Roles = "Administrator,Organizer")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = vehicleCategoryService.GetAll();
            var engineTypes = engineTypeService.GetAll();
            var mfEvent = await eventService.GetById(id);
            if (mfEvent == null)
            {
                return NotFound();
            }
            mfEvent.VehicleCategories = categories.Select(c => new CheckBoxItem
            {
                Id = c.Id,
                CategoryName = c.Name,
                IsChecked =mfEvent.VehicleCategories.Any(x=>x.CategoryName==c.Name)
                
            }).ToList();
            mfEvent.EngineTypes = engineTypes.Select(e => new CheckBoxItem
            {
                Id = e.Id,
                CategoryName = e.Name,
                IsChecked= mfEvent.EngineTypes.Any(x => x.CategoryName == e.Name)

            }).ToList();
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
        [Authorize(Roles = "Administrator,Organizer")]
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
                    var succeed = await eventService.Update(id, eventViewModel);
                    if (succeed)
                    {
                        return RedirectToAction(nameof(Index));

                    }
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
        [Authorize(Roles = "Administrator,Participant")]
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
        [Authorize(Roles = "Administrator,Participant")]
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
        [Authorize(Roles = "Administrator,Participant")]
        public IActionResult MyEvents()
        {


            var userId = _userManager.GetUserId(User);
            var events = eventService.GetAllByUserParticipating(userId);
            return View(events);
        }
        // GET: UsersViewController
        [Authorize(Roles = "Administrator,Organizer")]

        public async Task<IActionResult> Statistics(string searchString, int? page)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["ShowLoadingScreen"] = true;
            List<EventsViewViewModel> events = new List<EventsViewViewModel>();
            if (User.IsInRole("Organizer"))
            {
                events = eventsViewService.EventsByOrganizer(User.Claims.FirstOrDefault().Value).ToList();

            }
            else
            {
                events = eventsViewService.GetAll().ToList();

            }

            // Filter by search string
            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(e =>
                    $"{e.EntranceFee}".Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    e.EventDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    e.ExpectedRevenue.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    e.EventId.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    e.AllowedEngineTypesCount.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    e.AllowedVehicleCategoriesCount.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    e.OrganizerFullName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    e.EventName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Paginate the results
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var pagedLocations = events.ToPagedList(pageNumber, pageSize);

            return View(pagedLocations);
        }
        public async Task<IActionResult> Cancel(int id)
        {
            await eventService.Cancel(id);
            return RedirectToAction("Index");
        }
    }
}
