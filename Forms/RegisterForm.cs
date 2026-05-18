using System;
using System.Windows.Forms;

namespace ApartmentWinForms.Forms;

public partial class RegisterForm : Form
{
    public RegisterForm()
    {
        InitializeComponent();
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
        var pending = new PendingApprovalForm();
        pending.Show();
        Hide();
    }

    private void btnSignIn_Click(object sender, EventArgs e)
    {
        var login = new LoginForm();
        login.Show();
        Hide();
    }
}
