namespace TaskCircle.UserManagerApi.Models;

public class User
{
    public int IdUser { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public Gender? Gender { get; set; }
    public int? IdGender { get; set; }
}
