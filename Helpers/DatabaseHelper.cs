using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace ApartmentWinForms.Helpers;

public static class DatabaseHelper
{
    private static readonly string ConnectionStringDev =
        $"Data Source = {Environment.GetEnvironmentVariable("DB_SERVER_DEV")};" +
        $"Initial Catalog = {Environment.GetEnvironmentVariable("DB_NAME_DEV")};" +
        $"Integrated Security = True; Trust Server Certificate=True";
    

    private static readonly string ConnectionStringProd =
        $"Server=tcp:{Environment.GetEnvironmentVariable("DB_SERVER")};" +
        $"Initial Catalog={Environment.GetEnvironmentVariable("DB_NAME")};" +
        $"User ID={Environment.GetEnvironmentVariable("DB_USER")};" +
        $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
        "Persist Security Info=False;" +
        "MultipleActiveResultSets=False;" +
        "Encrypt=True;" +
        "TrustServerCertificate=False;" +
        "Connection Timeout=30;";
    
    // No readonly here — it's a property not a field
    private static string ActiveConnectionString =>
        Environment.GetEnvironmentVariable("ENVIRONMENT") == "PROD"
            ? ConnectionStringProd
            : ConnectionStringDev;

    public static SqlConnection GetSqlConnection()
    {
        return new SqlConnection(ActiveConnectionString);
    }

    public static bool TestConnection()
    {
        try
        {
            using var conn = GetSqlConnection();
            conn.Open();
            MessageBox.Show(
                "✅ Database connection successful!\n" +
                $"Environment: {Environment.GetEnvironmentVariable("ENVIRONMENT")}",
                "Connection Test",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return true;
        }
        catch (Exception e)
        {
            MessageBox.Show(
                $"❌ Connection failed: {e.Message}",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return false;
        }
    }
}