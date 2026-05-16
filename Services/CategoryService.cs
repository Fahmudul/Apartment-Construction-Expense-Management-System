using ApartmentWinForms.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Services;

public static class CategoryService
{
    // Called when AdminCategoriesForm loads
    public static List<Category> GetAllCategories()
    {
        var categories = new List<Category>();

        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }

        return categories;
    }

    // Called to populate dropdown in AddExpenseForm
    public static List<Category> GetCategoryDropdown()
    {
        return new List<Category>();
    }

    // Called by Add Category button in AdminCategoriesForm
    public static bool AddCategory(string name, string icon, string? description)
    {
        return false;
    }

    // Called by Edit button in AdminCategoriesForm
    public static bool UpdateCategory(Guid categoryId, string name, string icon, string? description)
    {
        return false;
    }

    // Called by Delete button in AdminCategoriesForm
    public static bool DeleteCategory(Guid categoryId)
    {
        return false;
    }

    // Called for Admin dashboard stat card
    public static int GetTotalCategoriesCount()
    {
        return 0;
    }
}