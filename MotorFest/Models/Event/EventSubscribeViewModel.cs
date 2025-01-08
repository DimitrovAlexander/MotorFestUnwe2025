using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotorFest.Models.Event
{
    public class EventSubscribeViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public int SelectedVehicleId { get; set; }
        public IEnumerable<SelectListItem> UserVehicles { get; set; }
    }
}
