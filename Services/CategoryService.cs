using ApartmentWinForms.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Services;


// Create Read Update Delete
public static class CategoryService
{
    // Called when AdminCategoriesForm loads
    public static List<Category> GetAllCategories()
    {
        var categories = new List<Category>();

        try {
            using var conn = DatabaseHelper.GetSqlConnection();

            conn.Open();

            string query = "SELECT CategoryID, CategoryName, Description from Categories";

            using var cmd = new SqlCommand(query, conn);

            using var response = cmd.ExecuteReader();

            while (response.Read()) {
                var category = new Category { 
                    CategoryID = response.GetGuid(response.GetOrdinal("CategoryID")),
                    CategoryName = response.GetString(response.GetOrdinal("CategoryName")),
                    Description = response.IsDBNull(response.GetOrdinal("Description"))
                    ? null :
                    response.GetString(response.GetOrdinal("Description"))
                };

                categories.Add(category);

                Console.WriteLine($"Category name {category.CategoryName} description {category.Description}");
            
            }
    

        }
        catch (Exception e) {
            MessageBox.Show($"Error occured {e.Message}");
        
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