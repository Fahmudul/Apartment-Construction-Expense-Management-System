using ApartmentWinForms.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace ApartmentWinForms.Services;

public static class ExpenseService
{
    // Called when AdminExpensesForm loads
    public static List<Expense> GetAllExpenses()
    {
        return new List<Expense>();
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