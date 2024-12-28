namespace MotorFest.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }

        // Participant fields
        public string FullName { get; set; }
        public string EGN { get; set; }
        public string BloodGroup { get; set; }
        public string CorrespondenceAddress { get; set; }

        // Organizer fields
        public string EGNOrBulstat { get; set; }
        public string Name { get; set; }

        // Address selection/creation
        public int? CorrespondenceAddressId { get; set; }
        public string NewAddress { get; set; }
        public List<Address> Addresses { get; set; }

        public string NewAddressCity { get; set; } = null!;

        public string NewAddressFullAddress { get; set; } = null!;

        public string NewAddressMunicipality { get; set; } = null!;

    }

}
