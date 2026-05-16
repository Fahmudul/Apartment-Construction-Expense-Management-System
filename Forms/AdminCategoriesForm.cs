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

        List<Category> categories = CategoryService.GetAllCategories() ;

        dgvCategories.Rows.Clear();

        foreach(Category category in categories) {

            dgvCategories.Rows.Add(
                
                category.Icon,
                category.CategoryName,
                category.Description
            );
        
        }

        Console.WriteLine($"Total category {categories.Count}");


    }

    



    private void btnAddCategory_Click(object sender, EventArgs e)
    {
        // Placeholder: call CategoryService.AddCategory() here
    }
}
