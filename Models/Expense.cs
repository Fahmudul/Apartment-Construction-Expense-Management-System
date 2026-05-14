namespace ApartmentWinForms.Models;

public class Expense
{
    public Guid ExpenseID { get; set; }
    public string Title { get; set; } = "";
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
    public Guid CategoryID { get; set; }
    public string CategoryName { get; set; } = "";
    public Guid UserID { get; set; }
    public string UserName { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}