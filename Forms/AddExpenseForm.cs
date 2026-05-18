using System;
using System.Windows.Forms;
using ApartmentWinForms.Models;
using System.Collections.Generic;
using ApartmentWinForms.Services;
namespace ApartmentWinForms.Forms;

public partial class AddExpenseForm : Form
{
    public AddExpenseForm()
    {
        InitializeComponent();
        loadCategoryDropDownList();
    }

    private void loadCategoryDropDownList() {
        List<Category> dropdownlists = CategoryService.GetCategoryDropdown();

        cmbCategory.DisplayMember = "CategoryName";
        cmbCategory.ValueMember = "CategoryID";
        cmbCategory.DataSource = dropdownlists;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {

        string expenseTitle = txtTitle.Text;
        string expenseDate = dtpDate.Value.ToString("yyyy-MM-dd");
        Guid categoryID = (Guid)cmbCategory.SelectedValue;
        //string expenseAmount;
        if(!decimal.TryParse(txtAmount.Text, out decimal expenseAmount))
        {
            MessageBox.Show("Please provide a digit larger than 0!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (categoryID == Guid.Empty || string.IsNullOrWhiteSpace(expenseTitle) || expenseAmount <= 0)  {
            MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        //Console.WriteLine($"Title {expenseTitle}");
        //Console.WriteLine($"Amount {expenseAmount}");
        //Console.WriteLine($"Expense date {expenseDate}");
        //Console.WriteLine($"Title {categoryID}");

        bool success = ExpenseService.AddExpense(expenseTitle, categoryID, expenseAmount, expenseDate);
        if (success) {
            MessageBox.Show("Expense added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
            return;
        }
        else
        {
            MessageBox.Show("Failed to add expense", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
            return;
        }
        
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
