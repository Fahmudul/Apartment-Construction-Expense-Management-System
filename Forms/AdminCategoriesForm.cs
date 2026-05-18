using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ApartmentWinForms.Services;
using ApartmentWinForms.Models;
namespace ApartmentWinForms.Forms;



public partial class AdminCategoriesForm : Form
{
    

    public AdminCategoriesForm()
    {
        InitializeComponent();

        loadCategories();
        dgvCategories.CellClick += dgvCategoriesCellClick;
    }

    private EditCategoryForm? _editForm = null;

    private void loadCategories() {
        List<Category> categories = CategoryService.GetAllCategories();

        dgvCategories.Rows.Clear();

        foreach (Category category in categories)
        {

            dgvCategories.Rows.Add(
                category.CategoryID,
                category.Icon,
                category.CategoryName,
                category.Description
            );
        }
    }

    private void dgvCategoriesCellClick(Object send, DataGridViewCellEventArgs e) {
        
        if (e.RowIndex < 0) return;
        var row = dgvCategories.Rows[e.RowIndex];
        var id = row.Cells["colCategoryID"].Value.ToString();
        var name = row.Cells["colName"].Value.ToString();
        var icon = row.Cells["colIcon"].Value.ToString();
        var description = row.Cells["colDesc"].Value.ToString();
        Guid categoryId = Guid.Parse(id);

        string actionBtn = dgvCategories.Columns[e.ColumnIndex].Name.ToString();
        //Console.WriteLine($"category Id {categoryId} action button {actionBtn}");

        switch (actionBtn) {
            case "colEdit":
                _editForm?.Close();
                _editForm = new EditCategoryForm(categoryId, name, icon, description);
                _editForm.FormClosed += (s, e) => loadCategories();
                _editForm.Show();
                break;
            case "colDelete":
                bool success = CategoryService.DeleteCategory(categoryId);
                if (!success) {
                    MessageBox.Show($"Failed to delete category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show($"Category deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadCategories();
                break;
            default: break;
        }
        
    }


    private void btnAddCategory_Click(object sender, EventArgs e)
    {
        string categoryName = txtCategoryName.Text;
        string categoryIcon = txtCategoryIcon.Text;

        if (!string.IsNullOrWhiteSpace(categoryName) && !string.IsNullOrWhiteSpace(categoryIcon))
        {
            bool success = CategoryService.AddCategory(categoryName, categoryIcon);
            if (success)
            {
                MessageBox.Show($"{categoryName} added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadCategories();
                return;
            }
            else
            {
                MessageBox.Show($"Failed to add category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        else
        {
            MessageBox.Show("Name and Icon are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

    }
}
