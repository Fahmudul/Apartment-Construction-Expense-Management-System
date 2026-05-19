using ApartmentWinForms.Models;
using Microsoft.Data.SqlClient;
using ApartmentWinForms.Helpers;
using System.Collections.Generic;
using System;

namespace ApartmentWinForms.Services;

public static class ExpenseService
{
    // Called when AdminExpensesForm loads
    public static List<Expense> GetAllExpenses()
    {
        List<Expense> expenses = new List<Expense>();
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();

            string query = @"
            SELECT 
                e.ExpenseID,
                e.Title,
                e.Amount,
                e.ExpenseDate,
                e.CreatedAt,
                c.CategoryName,
                u.Name AS UserName
            FROM Expenses e
            INNER JOIN Categories c ON e.CategoryID = c.CategoryID
            INNER JOIN Users u      ON e.UserID     = u.UserID";

            using var cmd = new SqlCommand(query, conn);
            using var response = cmd.ExecuteReader();

            while (response.Read()) {
                expenses.Add(new Expense
                {
                    ExpenseID = response.GetGuid(response.GetOrdinal("ExpenseID")),
                    Title = response.GetString(response.GetOrdinal("Title")),
                    Amount = response.GetDecimal(response.GetOrdinal("Amount")),
                    CategoryName = response.GetString(response.GetOrdinal("CategoryName")),
                    ExpenseDate = response.GetDateTime(response.GetOrdinal("ExpenseDate")),
                    UserName = response.GetString(response.GetOrdinal("UserName")),
                });
            }

        }
        catch (Exception e)
        {
            MessageBox.Show("Error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return expenses;
    }

    // Called when UserDashboardForm and UserExpensesForm loads
    public static List<Expense> GetExpensesByUser(Guid userId)
    {
        List<Expense> expenses = new List<Expense>();
        try
        {
            //Console.WriteLine($"User id {userId}");
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();

            string query = @"
                            SELECT 
                            e.ExpenseID,
                            e.Title,
                            c.CategoryName,
                            e.Amount,
                            e.ExpenseDate 
                            FROM Expenses e 
                            INNER JOIN Categories c ON e.CategoryID = c.CategoryID
                            WHERE e.UserID = @UserID";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserID", userId);
            using var response = cmd.ExecuteReader();

            while (response.Read())
            {
                expenses.Add(new Expense
                {
                    ExpenseID = response.GetGuid(response.GetOrdinal("ExpenseID")),
                    Title = response.GetString(response.GetOrdinal("Title")),
                    Amount = response.GetDecimal(response.GetOrdinal("Amount")),
                    CategoryName = response.GetString(response.GetOrdinal("CategoryName")),
                    ExpenseDate = response.GetDateTime(response.GetOrdinal("ExpenseDate")),
                });
            }

        }
        catch (Exception e)
        {
            MessageBox.Show("Error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return expenses;
    }

    // Called by Filter button in AdminExpensesForm
    public static List<Expense> FilterExpenses(string? categoryName, DateTime? from, DateTime? to)
    {
        return new List<Expense>();
    }

    // Called by Save button in AddExpenseForm
    public static bool AddExpense(string title, Guid categoryId, decimal amount, string date)
    {
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();

            conn.Open();
            //Console.WriteLine($"Name {title} icon {categoryId}");
            string query = @"INSERT INTO Expenses (Title, Amount, ExpenseDate, CategoryID, UserID)
                            Values (@Title, @Amount, @ExpenseDate, @CategoryID, @UserID)";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@ExpenseDate", date);
            cmd.Parameters.AddWithValue("@CategoryID", categoryId);
            cmd.Parameters.AddWithValue("@UserID", AuthService.CurrentUser.UserID);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                //Console.WriteLine("Expense added!");
                return true;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"Error occured: {e.Message}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        return false;
    }

   
    // Called by Delete button in expense tables
    public static bool DeleteExpense(Guid expenseId)
    {
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();

            conn.Open();

            string query = "DELETE FROM Expenses WHERE ExpenseID = @ExpenseID";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ExpenseID", expenseId);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return true;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"Error occured: {e.Message}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        return false;
    }

    // Called for Admin dashboard stat card
    public static decimal GetTotalExpensesAmount()
    {

        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();

            string query = "SELECT SUM(Amount) AS TOTAL FROM Expenses";

            using var cmd = new SqlCommand(query, conn);
            decimal totalExpense = (decimal)cmd.ExecuteScalar();

            if (totalExpense > 0)
            {
                return totalExpense;
            }

        }
        catch (Exception e)
        {
            MessageBox.Show("Error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return 0;
    }

    // Called for User dashboard stat card
    public static (decimal total, int count) GetTotalExpensesByUser(Guid userId)
    {
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();

            string query = "SELECT SUM(Amount) AS Total, COUNT(*) as ExpenseCount FROM Expenses WHERE UserID = @UserID";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserID", userId);
            using  var response= cmd.ExecuteReader();

            if (response.Read())
            {
                decimal total = response.IsDBNull(response.GetOrdinal("Total")) ? 0 : response.GetDecimal(response.GetOrdinal("Total"));
                int count = response.GetInt32(response.GetOrdinal("ExpenseCount"));
                return (total, count);
            }

        }
        catch (Exception e)
        {
            MessageBox.Show("Error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return (0,0);

    }

    
    

    // Called for Admin dashboard recent expenses table
    public static List<Expense> GetRecentExpenses(int count = 5)
    {
        var expenses = new List<Expense>();

        try
        {    
          
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();

            string query = @" SELECT TOP (@count) e.ExpenseID, e.Title, c.CategoryName, e.Amount,
                             u.Name FROM Expenses e INNER JOIN Users u ON e.UserID = u.UserID  
                             INNER JOIN Categories c ON c.CategoryID = e.CategoryID WHERE e.ExpenseDate >= DATEADD(Day, -5, GETDATE()) ORDER BY e.ExpenseDate DESC      ";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@count", count);

            using var response = cmd.ExecuteReader();

            while (response.Read())
            {
                Expense expense = new Expense
                {
                    
                    ExpenseID = response.GetGuid(response.GetOrdinal("ExpenseID")),
                    Title = response.GetString(response.GetOrdinal("Title")),
                    Amount = response.GetDecimal(response.GetOrdinal("Amount")),
                    CategoryName = response.GetString(response.GetOrdinal("CategoryName")),
                    UserName = response.GetString(response.GetOrdinal("Name")),
                };

                expenses.Add(expense);
            }
            
        }
        
        catch (Exception e)
        {

            MessageBox.Show($"Error occured: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return expenses;

    }


    public static List<Expense> GetRecentExpensesByUser(int count = 5)
    {
        var expenses = new List<Expense>();

        try
        {

            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();

            string query = @" SELECT TOP (@count) e.ExpenseID, e.Title, c.CategoryName, e.Amount,
                             e.ExpenseDate FROM Expenses e INNER JOIN Users u ON e.UserID = u.UserID  
                             INNER JOIN Categories c ON c.CategoryID = e.CategoryID WHERE e.UserID = @UserID AND e.ExpenseDate >= DATEADD(Day, -5, GETDATE()) ORDER BY e.ExpenseDate DESC      ";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@count", count);
            cmd.Parameters.AddWithValue("@UserID", AuthService.CurrentUser.UserID);

            using var response = cmd.ExecuteReader();

            while (response.Read())
            {
                Expense expense = new Expense
                {

                    ExpenseID = response.GetGuid(response.GetOrdinal("ExpenseID")),
                    Title = response.GetString(response.GetOrdinal("Title")),
                    Amount = response.GetDecimal(response.GetOrdinal("Amount")),
                    CategoryName = response.GetString(response.GetOrdinal("CategoryName")),
                    ExpenseDate = response.GetDateTime(response.GetOrdinal("ExpenseDate")),
                };

                expenses.Add(expense);
            }

        }

        catch (Exception e)
        {

            MessageBox.Show($"Error occured: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return expenses;

    }
}