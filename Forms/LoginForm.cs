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
        // --- Step 1: Get input values ---
        string email = txtEmail.Text.Trim().ToLower();
        string password = txtPassword.Text.Trim();

        // --- Step 2: Validate ---
        if ((email == "") || (password == ""))
        {
            MessageBox.Show("Please enter both email and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // --- Step 3: Database & Auth ---
        bool success = ApartmentWinForms.Services.AuthService.Login(email, password);

        // --- Step 4: UI Update / Navigation ---
        if (success == false)
        {
            MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (ApartmentWinForms.Services.AuthService.CurrentUser.Status == "Pending")
        {
            new PendingApprovalForm().Show();
            Hide();
            return;
        }
        
        if (ApartmentWinForms.Services.AuthService.CurrentUser.Status == "Blocked")
        {
            MessageBox.Show("Your account has been blocked.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (ApartmentWinForms.Services.AuthService.CurrentUser.Role == "Admin")
        {
            new AdminDashboardForm().Show();
        }
        else
        {
            new UserDashboardForm().Show();
        }
        Hide();
    }

    // ── Navigation ────────────────────────────────────────────
    private void btnRegister_Click(object sender, EventArgs e)
    {
        var register = new RegisterForm();
        register.Show();
        Hide();
    }
}
