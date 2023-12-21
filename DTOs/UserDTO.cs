using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskCircle.UserManagerApi.Models;

namespace TaskCircle.UserManagerApi.DTOs;

public class UserDTO
{
    public int IdUser { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    [JsonIgnore]
    public int IdGeder { get; set; }

    public Gender? Gender { get; set; }
}
