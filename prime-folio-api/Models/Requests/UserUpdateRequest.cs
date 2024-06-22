using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    public class UserUpdateRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
    }
}