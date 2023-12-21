using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace TaskCircle.UserManagerApi.DTOs
{
    public class UpdateUserDTO
    {
        [JsonIgnore]
        [XmlIgnore]
        public int IdUser { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "It has to be an email")]
        public string? Email { get; set; }

        [MinLength(13)]
        [MaxLength(13)]
        public string? Phone { get; set; }

        [MinLength(5)]
        [MaxLength(255)]
        public string? Address { get; set; }

        [Range(1,2)]
        public int? IdGender { get; set; }
    }
}