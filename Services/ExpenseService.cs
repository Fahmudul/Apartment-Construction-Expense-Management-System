namespace ApartmentWinForms.Services;

public static class ExpenseService
{
    // Called when AdminExpensesForm loads
    public static List<Expense> GetAllExpenses() { }

    // Called when UserDashboardForm and UserExpensesForm loads
    public static List<Expense> GetExpensesByUser(Guid userId) { }

    // Called by Filter button in AdminExpensesForm
    public static List<Expense> FilterExpenses(string? categoryName, DateTime? from, DateTime? to) { }

    // Called by Save button in AddExpenseForm
    public static bool AddExpense(string title, Guid categoryId, decimal amount, DateTime date) { }

    // Called by Edit button in UserExpensesForm
    public static bool UpdateExpense(Guid expenseId, string title, Guid categoryId, decimal amount, DateTime date) { }

    // Called by Delete button in expense tables
    public static bool DeleteExpense(Guid expenseId) { }

    // Called for Admin dashboard stat card
    public static decimal GetTotalExpensesAmount() { }

    // Called for User dashboard stat card
    public static decimal GetTotalExpensesByUser(Guid userId) { }

    // Called for Admin dashboard recent expenses table
    public static List<Expense> GetRecentExpenses(int count = 5) { }
}