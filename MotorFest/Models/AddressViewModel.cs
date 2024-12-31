using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest.Models
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Име")]
        public string Name { get; set; }
        [Display(Name = "Град")]
        public string City { get; set; } = null!;
        [Display(Name = "Пълен адрес")]

        public string FullAddress { get; set; } = null!;
        [Display(Name = "Община")]

        public string Municipality { get; set; } = null!;

        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public virtual ICollection<EventViewModel> Events { get; set; } = new List<EventViewModel>();
    }
}
