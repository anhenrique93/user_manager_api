namespace TaskCircle.UserManagerApi.Models;

/// <summary>
/// This clas represent an User
/// </summary>
public class User
{
    /// <summary>
    /// User ID
    /// </summary>
    public int IdUser { get; set; }

    /// <summary>
    /// User First Name
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// User Last Name
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// User Email
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// User Phone
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// User Address
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// User Geder
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// User Gender Id
    /// 1 - Masculine
    /// 2 - Feminine
    /// </summary>
    public int? IdGender { get; set; }
}
