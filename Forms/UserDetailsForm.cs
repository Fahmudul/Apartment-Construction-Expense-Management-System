using System;
using System.Windows.Forms;
using ApartmentWinForms.Services;
namespace ApartmentWinForms.Forms;

public partial class UserDetailsForm : Form
{
    private Guid _userID;
    public UserDetailsForm(string name, string email, string joined, string status, Guid userID, string role)
    {
      
        InitializeComponent(name, email, joined, status, role);
        _userID = userID;
    }

    private void btnApprove_Click(object sender, EventArgs e)
    {
        bool success = UserService.UpdateStatus(_userID, "Approved");
        if (success)
        {
            MessageBox.Show("User Approved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        Close();
    }

    private void btnReject_Click(object sender, EventArgs e)
    {
        bool success = UserService.UpdateStatus(_userID, "Rejected");
        if (success)
        {
            MessageBox.Show("User Rejected.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        Close();
    }

    private void btnBlock_Click(object sender, EventArgs e)
    {
        bool success = UserService.UpdateStatus(_userID, "Blocked");
        if (success) { 
            MessageBox.Show("User blocked.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        Close();
    }

    private void btnUnblock_Click(object sender, EventArgs e)
    {
        bool success = UserService.UpdateStatus(_userID, "Approved");
        if (success) { 
            MessageBox.Show("User unblocked.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            Close(); 
        }
        Close();
    }


    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
