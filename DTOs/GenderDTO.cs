using System.Text.Json.Serialization;
using System.Xml.Serialization;
using TaskCircle.UserManagerApi.Models;

namespace TaskCircle.UserManagerApi.DTOs
{
    public class GenderDTO
    {
        [JsonIgnore]
        [XmlIgnore]
        public int IdGender { get; set; }
        public string? Name { get; set; }
    }
}
