using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ApartmentWinForms.Services;
using ApartmentWinForms.Models;
namespace ApartmentWinForms.Forms;

public partial class UserExpensesForm : Form
{
    public UserExpensesForm()
    {
        InitializeComponent();
        loadUserExpenses();
    }

    private void loadUserExpenses() {
        List<Expense> expenses = ExpenseService.GetExpensesByUser(AuthService.CurrentUser.UserID );
        dgvExpenses.Rows.Clear();
        foreach (Expense expense in expenses)
        {
            dgvExpenses.Rows.Add(
                expense.ExpenseID.ToString(),
                expense.Title,
                expense.CategoryName,
                expense.Amount,
                expense.ExpenseDate.ToString("dd MMM yyyy")
            );
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {

        var addExpenseForm = new AddExpenseForm();
        addExpenseForm.Show();
        addExpenseForm.FormClosed += (s, e) => loadUserExpenses();
       
    }

    private void dgvExpenses_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var row = dgvExpenses.Rows[e.RowIndex];
        if (dgvExpenses.Columns[e.ColumnIndex].Name.ToString() == "colDelete") {
            var title = row.Cells["colTitle"].Value.ToString();
            var confirm = MessageBox.Show($"Are you sure want to delete {title} expense!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes) {
                Guid expenseId = Guid.Parse(row.Cells["colExpenseID"].Value.ToString());
                bool success = ExpenseService.DeleteExpense(expenseId);
                if (success) {
                    MessageBox.Show("Expense deleted successfully!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    loadUserExpenses();
                    return;
                }
                else
                {
                    MessageBox.Show("Failed to delete expense!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            
            }
                       
        }
    }
}
