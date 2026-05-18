using System;
using System.Windows.Forms;
using ApartmentWinForms.Services;

namespace ApartmentWinForms.Forms;

public partial class EditCategoryForm : Form
{
    private Guid _categoryId;

    public EditCategoryForm(Guid categoryId, string name, string icon, string? description)
    {
        InitializeComponent(name, icon, description);
        _categoryId = categoryId;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string name = txtCategoryName.Text.Trim();
        string icon = txtCategoryIcon.Text.Trim();
        string? desc = string.IsNullOrEmpty(txtDescription.Text.Trim())
                       ? null
                       : txtDescription.Text.Trim();

        Console.WriteLine($"Catgory name {name}");
        Console.WriteLine($"Icon {icon}");
        Console.WriteLine($"description {desc}");

        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("Category name is required.");
            return;
        }

        bool success = CategoryService.UpdateCategory(_categoryId, name, icon, desc);
        if (success)
        {
            MessageBox.Show("Category updated successfully!");
            Close();
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}