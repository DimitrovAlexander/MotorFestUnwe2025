using System.ComponentModel.DataAnnotations.Schema;
using MotorFest.Models.Event;
using MotorFest.Models.Vehicle;

namespace MotorFest.Models.VehicleCategories
{
    public class VehicleCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;


        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public virtual ICollection<EventViewModel> Events { get; set; } = new List<EventViewModel>();

        public virtual ICollection<VehicleViewModel> Vehicles { get; set; } = new List<VehicleViewModel>();
    }
}