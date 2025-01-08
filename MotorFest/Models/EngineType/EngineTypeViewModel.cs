using System.ComponentModel.DataAnnotations.Schema;
using MotorFest.Models.Vehicle;

namespace MotorFest.Models.EngineType
{
    public class EngineTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;


        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public virtual ICollection<VehicleViewModel> Vehicles { get; set; } = new List<VehicleViewModel>();
    }
}