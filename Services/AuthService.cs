using ApartmentWinForms.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;

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
}