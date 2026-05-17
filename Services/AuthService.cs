using ApartmentWinForms.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Services;

public static class AuthService
{
    // Currently logged in user — shared across all forms
    public static User? CurrentUser { get; set; }

    // Called by Login button in LoginForm
    public static User? Login(string email, string password)
    {
        return null;
    }

    // Called by Register button in RegisterForm
    public static bool Register(string name, string email, string password)
    {
        try {
            using var conn = DatabaseHelper.GetSqlConnection();

            conn.Open();

            string query = "INSERT INTO Users (Name, Email, Password) Values (@Name, @Email, @Password)";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0) {
                //Console.WriteLine("User registered!");
                return true;
            }       
        }
        catch (Exception e) {
            MessageBox.Show($"Error occured {e.Message}");
        }

        return false;
    }

    // Called by Logout button in any form
    public static void Logout()
    {
        CurrentUser = null;
    }

    // Called on startup or after login to check status
    public static bool IsApproved(string email)
    {
        return false;
    }

    // Called to check if logged in user is admin
    public static bool IsAdmin()
    {
        return false;
    }

    public static bool IfUserAleadyExists(string email)
    {
        try {
            using var conn = DatabaseHelper.GetSqlConnection();

            conn.Open();

            string query = "SELECT Email FROM Users WHERE Email LIKE @Email";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);

            object found = cmd.ExecuteScalar();
            if (found != null) { 
                return true;
            }
        
        } catch (Exception e) {
            MessageBox.Show($"Error occured {e.Message}");
        
        }

        return false;
    
    }
}