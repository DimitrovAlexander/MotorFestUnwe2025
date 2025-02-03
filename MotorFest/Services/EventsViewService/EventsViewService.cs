using MotorFest.Data;
using MotorFest.Models.Event;
using MotorFest.Models.User;

namespace MotorFest.Services.EventsViewService
{
    public class EventsViewService : IEventsViewService
    {

        private readonly MotorFestDbContext dbContext;

        public EventsViewService(MotorFestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<EventsViewViewModel> EventsByOrganizer(string id)
        {

            return dbContext.EventsView
                .Where(x => x.OrganizerId == id)
            .Select(evnt => new EventsViewViewModel
            {
                EventId = evnt.EventId,
                OrganizerId = evnt.OrganizerId,
                OrganizerFullName = evnt.OrganizerFullName,
                EventDate = evnt.EventDate,
                EntranceFee = evnt.EntranceFee,
                EventName = evnt.EventName,
                ExpectedRevenue = evnt.ExpectedRevenue,
                AllowedEngineTypesCount = evnt.AllowedEngineTypesCount,
                AllowedVehicleCategoriesCount = evnt.AllowedVehicleCategoriesCount,
                LocationName = evnt.LocationName,
                RegisteredParticipantsCount = evnt.RegisteredParticipantsCount

            }).ToList();
        }

        public ICollection<EventsViewViewModel> GetAll()
        {

            return dbContext.EventsView
            .Select(evnt => new EventsViewViewModel
            {
                EventId = evnt.EventId,
                OrganizerId = evnt.OrganizerId,
                OrganizerFullName = evnt.OrganizerFullName,
                EventDate = evnt.EventDate,
                EntranceFee = evnt.EntranceFee,
                EventName = evnt.EventName,
                ExpectedRevenue = evnt.ExpectedRevenue,
                AllowedEngineTypesCount = evnt.AllowedEngineTypesCount,
                AllowedVehicleCategoriesCount = evnt.AllowedVehicleCategoriesCount,
                LocationName = evnt.LocationName,
                RegisteredParticipantsCount = evnt.RegisteredParticipantsCount

            }).ToList();
        }

        public async Task<EventsViewViewModel> GetById(string id)
        {
            var evnt = dbContext.EventsView.FirstOrDefault(x => x.OrganizerId == id);

            return new EventsViewViewModel
            {
                EventId = evnt.EventId,
                OrganizerId = evnt.OrganizerId,
                OrganizerFullName = evnt.OrganizerFullName,
                EventDate = evnt.EventDate,
                EntranceFee = evnt.EntranceFee,
                EventName = evnt.EventName,
                ExpectedRevenue = evnt.ExpectedRevenue,
                AllowedEngineTypesCount = evnt.AllowedEngineTypesCount,
                AllowedVehicleCategoriesCount = evnt.AllowedVehicleCategoriesCount,
                LocationName = evnt.LocationName,
                RegisteredParticipantsCount = evnt.RegisteredParticipantsCount

            };
        }


    }
}
