namespace MotorFest.Models.User
{
    public class UsersViewViewModel
    {

        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public int? VehicleCount { get; set; }
        public int? EventCount { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UserId { get; set; }
    }
}
