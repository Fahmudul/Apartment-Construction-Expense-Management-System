namespace ApartmentWinForms.Models;

public class Category
{
    public Guid CategoryID { get; set; }
    public string CategoryName { get; set; } = "";
    public string Icon { get; set; } = "";
    public string? Description { get; set; }
}

