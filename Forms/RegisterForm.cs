using System;
using System.Windows.Forms;
using ApartmentWinForms.Services;

namespace ApartmentWinForms.Forms;

public partial class RegisterForm : Form
{
    public RegisterForm()
    {
        InitializeComponent();
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {

        Console.WriteLine($"Username {txtName.Text}");
        Console.WriteLine($"Email {txtEmail.Text}");
        Console.WriteLine($"Password {txtPassword.Text}");
        Console.WriteLine($"Confirm Password {txtConfirm.Text}");

        string name = txtName.Text;
        string email = txtEmail.Text;
        string password = txtPassword.Text;
        string confirmPassword = txtConfirm.Text;

        // ToDo: Input Validation before sending to DB
        // 1. Check if all fields are non empty
        // 2. Check if valid email format
        // 3. Check if password matched with confirm password
        // 4. Is user already exists with this email
        // 5. Check Username, Email, password size is not more than given range

        // 1. If all fields are non empty
        if (!string.IsNullOrWhiteSpace(name) &&
            !string.IsNullOrWhiteSpace(email) &&
            !string.IsNullOrWhiteSpace(password) &&
            !string.IsNullOrWhiteSpace(confirmPassword))      
        {
            Console.WriteLine($"Username {name}");
            Console.WriteLine($"Email {email}");
            Console.WriteLine($"Password {password}");
            Console.WriteLine($"Confirm Password {confirmPassword}");
        }
        else
        {
            Console.WriteLine("All fields are required.");
            MessageBox.Show("All fields are required!");
            return;
        }

        // 3. Check if password and confirm password are not same
        if (password != confirmPassword) {
            MessageBox.Show("Confirm password should match with password!");
            return;
        }


        // 4. Check if user already exists with this email
        bool isFound = AuthService.IfUserAleadyExists(email);
        if (isFound) {
            MessageBox.Show($"An user with {email} already exists!");
            return;
        }

        // Call Authservice register function to register this user
        bool sucess = AuthService.Register(name, email, password);
        if (sucess) {
            var pending = new PendingApprovalForm();
            pending.Show();
            Hide();
        }

    }

    private void btnSignIn_Click(object sender, EventArgs e)
    {
        var login = new LoginForm();
        login.Show();
        Hide();
    }
}
