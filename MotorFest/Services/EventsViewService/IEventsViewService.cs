using MotorFest.Models.Event;

namespace MotorFest.Services.EventsViewService
{
    public interface IEventsViewService :IGenericViewModelService<EventsViewViewModel, string>
    {
        ICollection<EventsViewViewModel> EventsByOrganizer(string id);
    }
}
