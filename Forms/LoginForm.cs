using System;
using System.Drawing;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
    }

    // ── Event Handlers ────────────────────────────────────────
    private void btnLogin_Click(object sender, EventArgs e)
    {
        // Placeholder navigation — replace with real AuthService.Login() later
        Console.WriteLine(Environment.GetEnvironmentVariable("DB_SERVER"));
        string email = txtEmail.Text.Trim().ToLower();
        if (email.Contains("admin"))
        {
            var dashboard = new AdminDashboardForm();
            dashboard.Show();
            Hide();
        }
        else
        {
            var dashboard = new UserDashboardForm();
            dashboard.Show();
            Hide();
        }
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        var register = new RegisterForm();
        register.Show();
        Hide();
    }
}
