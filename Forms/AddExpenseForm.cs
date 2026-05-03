using System;
using System.Windows.Forms;

namespace ApartmentWinForms.Forms;

public partial class AddExpenseForm : Form
{
    public AddExpenseForm()
    {
        InitializeComponent();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        // Placeholder: call ExpenseService.AddExpense() here
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
