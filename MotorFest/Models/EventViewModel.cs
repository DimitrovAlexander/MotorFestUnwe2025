using MotorFest.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public int OrganizerId { get; set; }

        public int AddressId { get; set; }

        public DateTime EventDate { get; set; }

        public int CategoryId { get; set; }

        public decimal EntranceFee { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public virtual AddressViewModel Address { get; set; } = null!;

        public virtual VehicleCategoryViewModel Category { get; set; } = null!;

        public virtual UserViewModel Organizer { get; set; } = null!;
    }
}
