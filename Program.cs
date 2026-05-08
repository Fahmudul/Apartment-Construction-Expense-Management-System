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
        
        Application.Run(new LoginForm());
    }
}
