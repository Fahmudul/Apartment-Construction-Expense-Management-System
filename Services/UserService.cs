using ApartmentWinForms.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace ApartmentWinForms.Services;

public static class UserService
{
    // Called when AdminUsersForm loads
    public static List<User> GetAllUsers()
    {
        return new List<User>();
    }

    // Called for filter tabs in AdminUsersForm
    public static List<User> GetUsersByStatus(string status)
    {
        return new List<User>();
    }

    // Called by Approve button in AdminUsersForm
    public static bool ApproveUser(Guid userId)
    {
        return false;
    }

    // Called by Reject button in AdminUsersForm
    public static bool RejectUser(Guid userId)
    {
        return false;
    }

    // Called by Block button in AdminUsersForm
    public static bool BlockUser(Guid userId)
    {
        return false;
    }

    // Called by Unblock button in AdminUsersForm
    public static bool UnblockUser(Guid userId)
    {
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