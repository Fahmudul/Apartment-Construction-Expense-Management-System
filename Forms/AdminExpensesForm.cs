using System;
using System.Windows.Forms;

namespace ApartmentWinForms.Forms;

public partial class AdminExpensesForm : Form
{
    public AdminExpensesForm()
    {
        InitializeComponent();
    }

    private void btnFilter_Click(object sender, EventArgs e)
    {
        // Placeholder: call ExpenseService.FilterExpenses() here
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        var addForm = new AddExpenseForm();
        addForm.ShowDialog();
    }
}
