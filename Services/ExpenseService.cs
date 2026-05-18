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
        return new List<Expense>();
    }

    // Called by Filter button in AdminExpensesForm
    public static List<Expense> FilterExpenses(string? categoryName, DateTime? from, DateTime? to)
    {
        return new List<Expense>();
    }

    // Called by Save button in AddExpenseForm
    public static bool AddExpense(string title, Guid categoryId, decimal amount, DateTime date)
    {
        return false;
    }

    // Called by Edit button in UserExpensesForm
    public static bool UpdateExpense(Guid expenseId, string title, Guid categoryId, decimal amount, DateTime date)
    {
        return false;
    }

    // Called by Delete button in expense tables
    public static bool DeleteExpense(Guid expenseId)
    {
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
    public static decimal GetTotalExpensesByUser(Guid userId)
    {

       
        return 0;

    }

    

    // Called for Admin dashboard recent expenses table
    public static List<Expense> GetRecentExpenses(int count = 5)
    {
        return new List<Expense>();
    }
}