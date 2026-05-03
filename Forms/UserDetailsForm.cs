using System;
using System.Windows.Forms;

namespace ApartmentWinForms.Forms;

public partial class UserDetailsForm : Form
{
    public UserDetailsForm(string name, string email, string joined, string status)
    {
        InitializeComponent(name, email, joined, status);
    }

    private void btnApprove_Click(object sender, EventArgs e)
    {
        // Placeholder: call AuthService.ApproveUser() here
        Close();
    }

    private void btnReject_Click(object sender, EventArgs e)
    {
        // Placeholder: call AuthService.RejectUser() here
        Close();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
