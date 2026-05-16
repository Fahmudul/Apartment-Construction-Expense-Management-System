using System;
using System.Windows.Forms;
using ApartmentWinForms.Forms;
using DotNetEnv;
using ApartmentWinForms.Helpers;
namespace ApartmentWinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        // Load .env
        Env.Load();

        DatabaseHelper.TestConnection();

        ApplicationConfiguration.Initialize();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Open all screens at once for review
        //new LoginForm().Show();
        //new RegisterForm().Show();
        //new PendingApprovalForm().Show();
        new AdminDashboardForm().Show();
        //new AdminExpensesForm().Show();
        //new AdminCategoriesForm().Show();
        //new AdminUsersForm().Show();
        new UserDashboardForm().Show();
        //new AddExpenseForm().Show();
        //new PendingApprovalForm().Show();
        //new UserDetailsForm("John Smith", "john@company.com", "Jan 15, 2025", "Pending").Show();

        // Keep app running until all windows closed
        Application.Run();
    }
}
