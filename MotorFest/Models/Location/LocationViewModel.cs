using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MotorFest.Models.Event;

namespace MotorFest.Models.Location
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Име")]
        public string Name { get; set; }
        [Display(Name = "Град")]
        public string City { get; set; } = null!;
        [Display(Name = "Пълен адрес")]

        public string FullAddress { get; set; } = null!;
        [Display(Name = "Държава")]

        public string Country { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public virtual ICollection<EventViewModel> Events { get; set; } = new List<EventViewModel>();
    }
}
