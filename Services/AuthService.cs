using ApartmentWinForms.Models;
using Microsoft.Data.SqlClient;
using System;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Services;

public static class AuthService
{
    public static User CurrentUser { get; set; }

    // ── Login ─────────────────────────────────────────────────
    public static bool Login(string email, string password)
    {
        // --- Step 1: Validate Inputs ---
        if ((email == "") || (password == "")) return false;

        // --- Step 2: Database Operation ---
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();
            string query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password); // Note: In production, hash passwords!

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                CurrentUser = new User
                {
                    UserID = (Guid)reader["UserID"],
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    Role = reader["Role"].ToString(),
                    Status = reader["Status"].ToString(),
                    JoinedAt = (DateTime)reader["JoinedAt"]
                };
                return true;
            }
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show("Login Error: " + ex.Message);
        }
        return false;
    }

    // ── Register ──────────────────────────────────────────────
    public static bool Register(string name, string email, string password)
    {
        // --- Step 1: Validate Inputs ---
        if (name == "" || email == "" || password == "") return false;

        // --- Step 2: Database Operation ---
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();
            string query = "INSERT INTO Users (Name, Email, Password, Role, Status) VALUES (@Name, @Email, @Password, 'User', 'Pending')";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show("Registration Error: " + ex.Message);
            return false;
        }
    }

    // ── Logout ────────────────────────────────────────────────
    public static void Logout()
    {
        CurrentUser = null;
    }

    // ── Check Approval Status ─────────────────────────────────
    public static bool IsApproved(string email)
    {
        if (email == "") return false;

        // --- Step 2: Database Operation ---
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();
            string query = "SELECT Status FROM Users WHERE Email = @Email";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);

            var result = cmd.ExecuteScalar();
            return result != null && result.ToString() == "Approved";
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show("Status Check Error: " + ex.Message);
            return false;
        }
    }

    // ── Admin Check ───────────────────────────────────────────
    public static bool IsAdmin()
    {
        return CurrentUser?.Role == "Admin";
    }
}