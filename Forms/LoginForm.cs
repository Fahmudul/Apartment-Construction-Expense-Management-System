using System;
using System.Drawing;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;
using ApartmentWinForms.Services;

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
        
        string email = txtEmail.Text.Trim().ToLower();
        string password = txtPassword.Text.Trim().ToLower();
        // Check if email and password are not empty
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) { 
            MessageBox.Show("Email or Password can't be empty!","Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
        }

        // check if user exists in DB
        var user = AuthService.GetUserByEmail(email);

        if (user == null)
        {
            MessageBox.Show($"User not registered with {email}","Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
         
        }
        else
        {
            if (user.Password != password) {

                MessageBox.Show($"Incorrect Password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Console.WriteLine($"Password {user.Status}");

            if (user.Status == "Approved")
            {
                if (user.Role == "Admin")
                {
                    var adminDashboard = new AdminDashboardForm();
                    adminDashboard.Show();
                    Hide();
                }
                else
                {
                    var userDashboard = new UserDashboardForm();
                    userDashboard.Show();
                    Hide();

                }

            }
            else if (user.Status == "Pending")
            {
                MessageBox.Show($"Wait for admin approval!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else {
                MessageBox.Show($"You are blocked by admin!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
        }
    }

    // ── Navigation ────────────────────────────────────────────
    private void btnRegister_Click(object sender, EventArgs e)
    {
        var register = new RegisterForm();
        register.Show();
        Hide();
    }
}
