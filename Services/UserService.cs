using ApartmentWinForms.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Services;

public static class UserService
{
    // Called when AdminUsersForm loads
    public static List<User> GetAllUsers()
    {

        var users = new List<User>();

        try {
            using var conn = DatabaseHelper.GetSqlConnection();

            conn.Open();

            string query = "SELECT UserID, Name, Email, Role, Status, JoinedAt FROM Users";
                
            using var cmd = new SqlCommand(query, conn);

            using var response = cmd.ExecuteReader();
            while (response.Read()) {
                User user = new User
                {
                    UserID = response.GetGuid(response.GetOrdinal("UserID")),
                    Name = response.GetString(response.GetOrdinal("Name")),
                    Role = response.GetString(response.GetOrdinal("Role")),
                    Email = response.GetString(response.GetOrdinal("Email")),
                    Status = response.GetString(response.GetOrdinal("Status")),
                    JoinedAt = response.GetDateTime(response.GetOrdinal("JoinedAt"))

                };

                users.Add(user);
            
            }

        }catch(Exception e) {
            MessageBox.Show($"Error occured {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return users;
    }

    // Called for filter tabs in AdminUsersForm
    public static List<User> GetUsersByStatus(string status)
    {
        //try { 
            
        //} catch (Exception e) {
        //    MessageBox.Show("Error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        //}
        return null;
    }

    public static bool UpdateStatus(Guid userId, string status)
    {
        try {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();

            string query;
            SqlCommand cmd;
            if (status == "Rejected") {
                query = "DELETE FROM Users WHERE UserID = @UserID AND Status = @Status";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@Status", "Pending");

                int deletedRows= cmd.ExecuteNonQuery();
                if (deletedRows > 0) {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            query = "UPDATE Users SET Status = @Status WHERE UserID = @UserID";

            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@UserID", userId);
            int updatedRows = cmd.ExecuteNonQuery();

            if (updatedRows != 0) {
                return true;
            } else
            {            
                return false;
            }

        }catch(Exception e) {
            MessageBox.Show($"Error occured: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return false;
    }

    
    // Called for Admin dashboard stat card
    public static int GetTotalUsersCount()
    {
        return 0;
    }

    // Called for pending badge in AdminUsersForm
    public static int GetPendingUsersCount()
    {
        return 0;
    }



    // Called by Details button in AdminUsersForm
    public static User? GetUserById(Guid userId)
    {
        return null;
    }
}