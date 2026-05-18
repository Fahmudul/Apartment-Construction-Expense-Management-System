using System;
using System.Windows.Forms;

namespace ApartmentWinForms.Forms;

public partial class PendingApprovalForm : Form
{
    public PendingApprovalForm()
    {
        InitializeComponent();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        var login = new LoginForm();
        login.Show();
        Hide();
    }
}
