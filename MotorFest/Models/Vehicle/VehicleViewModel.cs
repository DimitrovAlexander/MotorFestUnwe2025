using MotorFest.Data.Entities;
using MotorFest.Models.EngineType;
using MotorFest.Models.User;
using MotorFest.Models.VehicleCategories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest.Models.Vehicle
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Собственик")]
        public string OwnerId { get; set; }
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        [Display(Name = "Тип на двигателя")]
        public int EngineTypeId { get; set; }
        [Display(Name = "Производител")]
        [Required(ErrorMessage = "Производителят е задължителен.")]
        [MinLength(4, ErrorMessage = "Производителят трябва да бъде минимум 4 символа.")]
        public string Manufacturer { get; set; }

        [Display(Name = "Модел")]
        [Required(ErrorMessage = "Моделът е задължителен.")]
        [MinLength(3, ErrorMessage = "Моделът трябва да бъде минимум 3 символа.")]
        public string Model { get; set; }

        [Display(Name = "Година на производство")]
        [Range(1800, int.MaxValue, ErrorMessage = "Годината на производство трябва да бъде между 1800 и настоящата година.")]
        public int YearOfManufacture { get; set; }

        [Display(Name = "Снимка на МПС")]
        [Required(ErrorMessage = "Снимката е задължителна.")]
        public string Photo { get; set; }
        [Display(Name = "Последна промяна")]

        public DateTime LastUpdate { get; set; } = DateTime.Now;
        [Display(Name = "Категория")]
        public virtual VehicleCategoryViewModel Category { get; set; } = null!;
        [Display(Name = "Тип на двигателя")]

        public virtual EngineTypeViewModel EngineType { get; set; } = null!;

        public virtual UserViewModel Owner { get; set; } = null!;
    }
}
