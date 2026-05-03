using System;
using System.Windows.Forms;

namespace ApartmentWinForms.Forms;

public partial class UserExpensesForm : Form
{
    public UserExpensesForm()
    {
        InitializeComponent();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        new AddExpenseForm().ShowDialog();
    }

    private void dgvExpenses_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (dgvExpenses.Columns[e.ColumnIndex].HeaderText == "Edit")
            new AddExpenseForm().ShowDialog();
        // Delete: call ExpenseService.DeleteExpense() here
    }
}
