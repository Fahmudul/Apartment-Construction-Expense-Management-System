using System;
using System.Windows.Forms;

namespace ApartmentWinForms.Forms;

public partial class RegisterForm : Form
{
    public RegisterForm()
    {
        InitializeComponent();
    }

    // ── Event Handlers ────────────────────────────────────────
    private void btnCreate_Click(object sender, EventArgs e)
    {
        // --- Step 1: Get input values ---
        string name = txtName.Text.Trim();
        string email = txtEmail.Text.Trim().ToLower();
        string password = txtPassword.Text.Trim();
        string confirmPassword = txtConfirm.Text.Trim();

        // --- Step 2: Validate ---
        if (name == "" || email == "" || password == "")
        {
            MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (password != confirmPassword)
        {
            MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // --- Step 3: Database ---
        bool success = ApartmentWinForms.Services.AuthService.Register(name, email, password);

        // --- Step 4: UI Update / Navigation ---
        if (success == true)
        {
            MessageBox.Show("Registration successful. Please wait for admin approval.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var login = new LoginForm();
            login.Show();
            Hide();
        }
        else
        {
            MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // ── Navigation ────────────────────────────────────────────
    private void btnSignIn_Click(object sender, EventArgs e)
    {
        var login = new LoginForm();
        login.Show();
        Hide();
    }
}
