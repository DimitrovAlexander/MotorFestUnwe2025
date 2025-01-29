using MotorFest.Models.Event;
using MotorFest.Models.User;

namespace MotorFest.Services.EventsViewService
{
    public class EventsViewService : IEventsViewService
    {
        public ICollection<EventsViewViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UsersViewViewModel> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
