namespace ApartmentWinForms.Models;

public class User
{
    public Guid UserID { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string Role { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime JoinedAt { get; set; }
}