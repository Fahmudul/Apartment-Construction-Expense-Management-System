using System;
using System.Windows.Forms;

namespace ApartmentWinForms.Forms;

public partial class PendingApprovalForm : Form
{
    public PendingApprovalForm()
    {
        InitializeComponent();
    }

    // ── Navigation ────────────────────────────────────────────
    private void btnBack_Click(object sender, EventArgs e)
    {
        // --- Step 1: UI Update / Navigation ---
        var login = new LoginForm();
        login.Show();
        Hide();
    }
}
