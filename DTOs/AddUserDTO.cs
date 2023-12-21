using System.ComponentModel.DataAnnotations;

namespace TaskCircle.UserManagerApi.DTOs
{
    public class AddUserDTO
    {
        [Required(ErrorMessage = "The First Name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The Email address is required")]
        [EmailAddress(ErrorMessage = "It has to be an email")]
        public string? Email { get; set; }

        [MinLength(12)]
        [MaxLength(12)]
        public string? Phone { get; set; }

        [MinLength(5)]
        [MaxLength(255)]
        public string? Address { get; set; }

        [Range(1, 2)]
        public int? IdGender { get; set; }
    }
}