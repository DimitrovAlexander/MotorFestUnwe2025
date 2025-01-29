namespace MotorFest.Models.User
{
    public class UsersViewViewModel
    {

        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public int? VehicleCount { get; set; }
        public int? ParticipatedEventsCount { get; set; }
        public int? OrganizedEventsCount { get; set; }
        public string UserId { get; set; }
    }
}
