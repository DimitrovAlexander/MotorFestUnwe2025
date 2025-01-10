using System.ComponentModel.DataAnnotations;
using MotorFest.Data.Entities;
using MotorFest.Models.EventRegistration;
using MotorFest.Models.EventVehicleCategory;
using MotorFest.Models.Location;
using MotorFest.Models.VehicleCategories;

namespace MotorFest.Models.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително.")]
        [MinLength(3, ErrorMessage = "Името трябва да бъде минимум 3 символа.")]
        public string Name { get; set; }

        public string OrganizerId { get; set; }

        public int LocationId { get; set; }

        [Required(ErrorMessage = "Датата на събитието е задължителна.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(EventViewModel), nameof(ValidateEventDate))]
        public DateTime EventDate { get; set; } = DateTime.Now;

        [Range(0, double.MaxValue, ErrorMessage = "Входната такса трябва да бъде 0 или положително число.")]
        public decimal EntranceFee { get; set; }

        public bool HasVehicles { get; set; }

        [Range(1800, int.MaxValue, ErrorMessage = "Минималната година на производство трябва да бъде поне 1800.")]
        public int? MinYearOfManufacture { get; set; }

        [Range(1800, int.MaxValue, ErrorMessage = "Максималната година на производство трябва да бъде максимум настоящата година.")]
        [CustomValidation(typeof(EventViewModel), nameof(ValidateMaxYearOfManufacture))]
        public int? MaxYearOfManufacture { get; set; }

        [Required(ErrorMessage = "Трябва да има поне една маркирана категория.")]
        public List<CheckBoxItem> VehicleCategories { get; set; } = new List<CheckBoxItem>();

        [Required(ErrorMessage = "Трябва да има поне един тип гориво.")]
        public List<CheckBoxItem> EngineTypes { get; set; } = new List<CheckBoxItem>();

        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public virtual LocationViewModel Location { get; set; } = null!;
        public virtual MFUser Organizer { get; set; } = null!;
        public virtual ICollection<EventRegistrationViewModel> EventRegistration { get; set; } = new HashSet<EventRegistrationViewModel>();

        // Custom validation methods
        public static ValidationResult ValidateEventDate(DateTime eventDate, ValidationContext context)
        {
            if (eventDate < DateTime.Now)
            {
                return new ValidationResult("Датата на събитието не може да бъде отминала.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateMaxYearOfManufacture(int? maxYear, ValidationContext context)
        {
            if (maxYear.HasValue && maxYear > DateTime.Now.Year)
            {
                return new ValidationResult($"Максималната година на производство не може да бъде по-голяма от {DateTime.Now.Year}.");
            }
            return ValidationResult.Success;
        }
    }
}
