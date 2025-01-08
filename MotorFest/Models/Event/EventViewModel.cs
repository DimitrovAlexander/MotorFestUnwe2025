using MotorFest.Data.Entities;
using MotorFest.Models.EventVehicleCategory;
using MotorFest.Models.Location;
using MotorFest.Models.VehicleCategories;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest.Models.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string OrganizerId { get; set; }

        public int LocationId { get; set; }

        public DateTime EventDate { get; set; } = DateTime.Now;


        public decimal EntranceFee { get; set; }


        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public virtual LocationViewModel Location { get; set; } = null!;

        public virtual MFUser Organizer { get; set; } = null!;
        public List<CheckBoxItem> VehicleCategories { get; set; } = new List<CheckBoxItem>();
        public bool HasVehicles { get; set; }

    }
}
