using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotorFest.Data.Entities;
using MotorFest.Models.Event;
using MotorFest.Models.Vehicle;
using MotorFest.Services.EngineTypeService;
using MotorFest.Services.EventService;
using MotorFest.Services.EventsViewService;
using MotorFest.Services.LocationService;
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

            int pageSize = 6;
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
           Text = $"{c.Name} - {c.FullAddress}"
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
                ViewData["allLocations"] = locationService.GetAll()
      .Select(c => new SelectListItem
      {
          Value = c.Id.ToString(),
          Text = c.Name
      }).ToList();

                return View(model);

            }

          
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
            TempData["successMessage"] = "Успешно добавихте събитие";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventDetails = await eventService.GetById(id);
            if (eventDetails == null) return NotFound();

            var user = _userManager.GetUserId(User);
            if (user!=null)
            {

            var isOrganizer = eventDetails.OrganizerId == user;
            var isAdmin = User.IsInRole("Administrator");
            var isParticipant = User.IsInRole("Participant");
                var registeredVehicles = eventService.GetRegisteredVehiclesForEvent(id);
            if (isOrganizer || isAdmin || isParticipant)
            {
                ViewData["RegisteredVehicles"] = registeredVehicles;
            }

            ViewData["HasCompatibleVehicles"] = false;
            bool isRegistered = eventService.IsUserRegisteredForEvent(user, id);
            if (isRegistered)
            {
                VehicleViewModel registeredVehicle = registeredVehicles.Where(x => x.Owner.Id == user).FirstOrDefault();
                ViewData["ParticipantVehicle"] = registeredVehicle;

            }
            else
            {
                List<VehicleViewModel> vehicles = vehicleService.GetByUserId(user).ToList();
                bool valid = false;
                foreach (var vehicle in vehicles)
                {
                    valid = eventService.CheckVehicleCompatibility(id, vehicle.Id);
                    if (valid)
                    {

                        break;
                    }
                }
                ViewData["HasCompatibleVehicles"] = valid;

            }
            ViewData["IsRegistered"] = isRegistered;

            return View(eventDetails);
            }
            ViewData["RegisteredVehicles"] = null;
            ViewData["ParticipantVehicle"] = null;
            ViewData["HasCompatibleVehicles"] = false;
            ViewData["IsRegistered"] = false;
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
            TempData["successMessage"] = "Успешно изтрихте събитие";
            return RedirectToAction("Index");
        }
        public IActionResult Calendar()
        {
            var events = eventService.GetAll().Where(x => x.IsCanceled == false); 
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
                IsChecked = mfEvent.VehicleCategories.Any(x => x.CategoryName == c.Name)

            }).ToList();
            mfEvent.EngineTypes = engineTypes.Select(e => new CheckBoxItem
            {
                Id = e.Id,
                CategoryName = e.Name,
                IsChecked = mfEvent.EngineTypes.Any(x => x.CategoryName == e.Name)

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
            ModelState.Remove("EventLogo");
            if (ModelState.IsValid)
            {
                try
                {
                    var succeed = await eventService.Update(id, eventViewModel);
                    if (succeed)
                    {
                        TempData["successMessage"] = "Успешно редактирахте събитието";
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
            
            EventViewModel evnt = await eventService.GetById(id);
            if (evnt == null)
            {
                return NotFound("Събитието не е намерено.");
            }

           
            var userId = _userManager.GetUserId(User); 
            List<VehicleViewModel> vehicles = vehicleService.GetByUserId(userId).ToList();

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

            
            var userId = _userManager.GetUserId(User);
            VehicleViewModel vehicle = await vehicleService.GetById(model.SelectedVehicleId);
            if (vehicle == null || vehicle.OwnerId != userId)
            {
                ModelState.AddModelError("", "Избраното превозно средство не е валидно.");
                return View(model);
            }

           
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
        // GET: UsersController
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

          
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var pagedLocations = events.ToPagedList(pageNumber, pageSize);

            return View(pagedLocations);
        }
        [Authorize(Roles = "Administrator,Participant")]
        public async Task<IActionResult> ExportCsv()
        {
            var userId = _userManager.GetUserId(User);
            var csvData = eventService.GenerateCsvForUserEvents(userId);

            var fileName = $"MyEvents_{DateTime.Now:yyyyMMddHHmmss}.csv";
            var fileContent = System.Text.Encoding.UTF8.GetPreamble().Concat(System.Text.Encoding.UTF8.GetBytes(csvData)).ToArray();


            return File(fileContent, "text/csv", fileName);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await eventService.Cancel(id);
            return RedirectToAction("Index");
        }
    }
}
