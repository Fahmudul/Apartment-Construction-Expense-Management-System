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
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();

            string query = "SELECT COUNT(*) FROM Categories";

            using var cmd = new SqlCommand(query, conn);
            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                return new List<Category>();
            }
            else
            {
                return new List<Category>();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
   

        return new List<Category>();
    }

    // Called by Add Category button in AdminCategoriesForm
    public static bool AddCategory(string name, string icon, string? description="")
    {

        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();

            conn.Open();
            Console.WriteLine($"Name {name} icon {icon}");
            string query = "INSERT INTO Categories (CategoryName, Icon, Description) Values (@CategoryName, @Icon, @Description)";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CategoryName", name);
            cmd.Parameters.AddWithValue("@Icon", icon);
            cmd.Parameters.AddWithValue("@Description", description);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Category added!");
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

    // Called by Edit button in AdminCategoriesForm
    public static bool UpdateCategory(Guid categoryId, string name, string icon, string? description)
    {
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();

            conn.Open();
            

            string query = "UPDATE Categories SET CategoryName = @CategoryName, Icon = @Icon, Description = @Description WHERE CategoryID = @CategoryID";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CategoryName", name);
            cmd.Parameters.AddWithValue("@Icon", icon);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@CategoryID", categoryId);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Category udated!");
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

    // Called by Delete button in AdminCategoriesForm
    public static bool DeleteCategory(Guid categoryId)
    {
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();

            conn.Open();

            string query = "DELETE FROM Categories WHERE CategoryID = @CategoryID";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CategoryID", categoryId);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Category deleted!");
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
    public static int GetTotalCategoriesCount()
    {
        try {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();

            string query = "SELECT COUNT(*) FROM Categories";

            using var cmd = new SqlCommand(query, conn);
            int count = (int)cmd.ExecuteScalar();

            if (count > 0) {
                return count;
            } 
        } catch (Exception e) {
            MessageBox.Show("Error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return 0;
     }
}