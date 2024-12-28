using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;

namespace MotorFest.Data.Entities
{
    public class MFUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Identifier { get; set; }
        [Column("21180022_LastUpdate")]

        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}
