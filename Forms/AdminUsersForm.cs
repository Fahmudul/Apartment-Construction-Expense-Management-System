using System;
using System.Windows.Forms;

namespace ApartmentWinForms.Forms;

public partial class AdminUsersForm : Form
{
    public AdminUsersForm()
    {
        InitializeComponent();
    }

    private void btnApprove_Click(object sender, EventArgs e)
    {
        // Placeholder: call AuthService.ApproveUser() here
    }

    private void btnReject_Click(object sender, EventArgs e)
    {
        // Placeholder: call AuthService.RejectUser() here
    }

    private void btnBlock_Click(object sender, EventArgs e)
    {
        // Placeholder: call AuthService.BlockUser() here
    }

    private void btnUnblock_Click(object sender, EventArgs e)
    {
        // Placeholder: call AuthService.UnblockUser() here
    }

    private void btnDetails_Click(object sender, EventArgs e)
    {
        var details = new UserDetailsForm("John Smith", "john@company.com", "Jan 15, 2025", "Pending");
        details.ShowDialog();
    }
}
