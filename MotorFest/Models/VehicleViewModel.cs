using MotorFest.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest.Models
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
        public string Manufacturer { get; set; } = null!;
        [Display(Name = "Модел")]
        public string Model { get; set; } = null!;
        [Display(Name = "Година на производство")]
        public int YearOfManufacture { get; set; }
        [Display(Name = "Снимка на МПС")]
        public string? Photo { get; set; }
        [Display(Name = "Последна промяна")]

        public DateTime LastUpdate { get; set; } = DateTime.Now;
        [Display(Name = "Категория")]
        public virtual VehicleCategoryViewModel Category { get; set; } = null!;
        [Display(Name = "Тип на двигателя")]

        public virtual EngineTypeViewModel EngineType { get; set; } = null!;

        public virtual UserViewModel Owner { get; set; } = null!;
    }
}
