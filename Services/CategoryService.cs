namespace ApartmentWinForms.Services;

public static class CategoryService
{
    // Called when AdminCategoriesForm loads
    public static List<Category> GetAllCategories()
    {
        // This code is an example structure for how all the databaase releated function in this class should look like. You can replace the SQL query and parameters as needed for each function.
        var categories = new List<Category>();
        try
        {
            using var conn = DatabaseHelper.GetSqlConnection();
            conn.Open();
            // SQL query here
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
        return categories;
    }

    // Called to populate dropdown in AddExpenseForm
    public static List<Category> GetCategoryDropdown() { }

    // Called by Add Category button in AdminCategoriesForm
    public static bool AddCategory(string name, string icon, string? description) { }

    // Called by Edit button in AdminCategoriesForm
    public static bool UpdateCategory(Guid categoryId, string name, string icon, string? description) { }

    // Called by Delete button in AdminCategoriesForm
    public static bool DeleteCategory(Guid categoryId) { }

    // Called for Admin dashboard stat card
    public static int GetTotalCategoriesCount() { }
}