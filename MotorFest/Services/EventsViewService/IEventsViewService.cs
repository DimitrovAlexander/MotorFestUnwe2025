using MotorFest.Models.Event;

namespace MotorFest.Services.EventsViewService
{
    public interface IEventsViewService :IGet<EventsViewViewModel, string>
    {
        ICollection<EventsViewViewModel> EventsByOrganizer(string id);
    }
}
