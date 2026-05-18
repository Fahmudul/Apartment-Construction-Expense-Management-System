using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ApartmentWinForms.Models;
using ApartmentWinForms.Services;
namespace ApartmentWinForms.Forms;

public partial class AdminExpensesForm : Form
{
    public AdminExpensesForm()
    {
        InitializeComponent();
        loadAllExpenses();
    }

    private void loadAllExpenses() { 
        List<Expense> expenses = ExpenseService.GetAllExpenses();
        dgvExpenses.Rows.Clear();
        foreach (Expense expense in expenses) {
            dgvExpenses.Rows.Add(
                expense.ExpenseID.ToString(),       
                expense.Title,                          
                expense.CategoryName,                   
                expense.UserName,                       
                expense.Amount,                         
                expense.ExpenseDate.ToString("dd MMM yyyy")
            );
        }

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
