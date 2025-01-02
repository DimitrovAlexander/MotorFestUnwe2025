using MotorFest.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int OrganizerId { get; set; }

        public int LocationId { get; set; }

        public DateTime EventDate { get; set; }


        public decimal EntranceFee { get; set; }


        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public virtual Location Location { get; set; } = null!;

        public virtual MFUser Organizer { get; set; } = null!;
        public ICollection<EventVehicleCategory> VehicleCategories { get; set; } = new List<EventVehicleCategory>();
    }
}
