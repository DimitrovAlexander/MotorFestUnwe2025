using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Identifier { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}