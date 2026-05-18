using System;
using System.Drawing;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;
using ApartmentWinForms.Services;

namespace ApartmentWinForms.Forms;

public partial class AdminDashboardForm : Form
{
    public AdminDashboardForm()
    {
        InitializeComponent();
        ShowDashboardContent();
    }

    // ── Sidebar navigation ────────────────────────────────────
    private void btnDashboard_Click(object sender, EventArgs e) => ShowDashboardContent();
    private void btnExpenses_Click(object sender, EventArgs e)  => ShowPanel(new AdminExpensesForm());
    private void btnCategories_Click(object sender, EventArgs e)=> ShowPanel(new AdminCategoriesForm());
    private void btnUsers_Click(object sender, EventArgs e)     => ShowPanel(new AdminUsersForm());

    private void btnLogout_Click(object sender, EventArgs e)
    {
        new LoginForm().Show();
        Close();
    }

    private void ShowPanel(Form childForm)
    {
        pnlContent.Controls.Clear();
        childForm.TopLevel        = false;
        childForm.FormBorderStyle = FormBorderStyle.None;
        childForm.Dock            = DockStyle.Fill;
        pnlContent.Controls.Add(childForm);
        childForm.Show();
    }

    private void ShowDashboardContent()
    {
        // Retrieve all statistics

        // Retrive total number of categories
        string categoryCount = CategoryService.GetTotalCategoriesCount().ToString();
        
        // Retrive total number of users
        string totalUsers = UserService.GetTotalUsersCount().ToString();

        // Retrive total number pending users
        string pendingUsers = UserService.GetPendingUsersCount().ToString();

        // Retrive total expenses
        string totalExpenses = ExpenseService.GetTotalExpensesAmount().ToString();

        pnlContent.Controls.Clear();

        int x = 24, y = 24;
        int w = pnlContent.Width - 48;

        // Welcome banner
        var banner = new Panel { Location = new Point(x, y), Size = new Size(w, 90), BackColor = Color.Transparent };
        banner.Paint += (s, e) =>
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var br = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Point(0, 0), new Point(banner.Width, 0),
                Color.FromArgb(14, 73, 102), Color.FromArgb(14, 116, 144));
            UITheme.FillRoundedRect(g, br, 0, 0, banner.Width, banner.Height, 14);
        };
        var bTitle = UITheme.MakeLabel("Good morning, Admin 👋",
            new Font("Segoe UI", 18f, FontStyle.Bold), Color.White, 28, 18);
        bTitle.BackColor = Color.Transparent;
        var bSub = UITheme.MakeLabel("Here's what's happening with your construction project today.",
            UITheme.FontSM, Color.FromArgb(186, 230, 253), 28, 52);
        bSub.BackColor = Color.Transparent;
        banner.Controls.AddRange(new Control[] { bTitle, bSub });
        pnlContent.Controls.Add(banner);
        y += 110;

        // Stat cards
        int cw = (w - 48) / 3;
        AddStatCard(x,           y, cw, "💰", "TOTAL EXPENSES",  $"৳ {totalExpenses}", "▲ 12.4% vs last month", UITheme.PrimaryLight);
        AddStatCard(x + cw + 24, y, cw, "👤", "TOTAL USERS",     totalUsers,           $"{pendingUsers} pending approval",    Color.FromArgb(209, 250, 229));
        AddStatCard(x + cw*2+48, y, cw, "🏷", "CATEGORIES", categoryCount,            "Active categories",     Color.FromArgb(254, 243, 199));
        y += 140;

        // Recent expenses table
        int tw = w * 3 / 5 - 12;
        var tableCard = MakeCard(x, y, tw, 280);
        AddCardHeader(tableCard, "Recent Expenses", "Latest expense records");
        var dgv = UITheme.MakeDataGrid(0, 66, tableCard.Width, 214);
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Title",     FillWeight = 35 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Category",  FillWeight = 25 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Amount",    FillWeight = 20 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Added By",  FillWeight = 20 });
        dgv.Rows.Add("Cement bags - Block A", "Materials",  "৳ 45,000",  "Rahima");
        dgv.Rows.Add("Labour - Foundation",   "Labour",     "৳ 80,000",  "Karim");
        dgv.Rows.Add("Crane Rental",          "Equipment",  "৳ 1,20,000","Admin");
        dgv.Rows.Add("Wiring - 3rd Floor",    "Electrical", "৳ 32,500",  "Rahima");
        tableCard.Controls.Add(dgv);
        pnlContent.Controls.Add(tableCard);

        // Chart placeholder
        int chartX = x + tw + 24;
        int chartW = w - tw - 24;
        var chartCard = MakeCard(chartX, y, chartW, 280);
        AddCardHeader(chartCard, "By Category", "Current month");
        var chartPh = new Panel { Location = new Point(0, 66), Size = new Size(chartW, 130), BackColor = UITheme.BgPage };
        chartPh.Paint += (s, e) =>
        {
            e.Graphics.DrawString("📊  Chart Placeholder", UITheme.FontBase,
                new SolidBrush(UITheme.TextMuted),
                new RectangleF(0, 40, chartW, 40),
                new StringFormat { Alignment = System.Drawing.StringAlignment.Center });
        };
        chartCard.Controls.Add(chartPh);

        string[] legs  = { "Materials 42%", "Labour 28%", "Equipment 18%", "Other 12%" };
        Color[]  legCl = { UITheme.Primary, UITheme.AccentTeal, UITheme.AccentAmber, UITheme.BorderMedium };
        int ly = 202;
        for (int i = 0; i < legs.Length; i++)
        {
            var dot = new Panel { Location = new Point(16, ly + 3), Size = new Size(12, 12), BackColor = legCl[i] };
            var ll  = new Label { Text = legs[i], Font = UITheme.FontSM, ForeColor = UITheme.TextSecond,
                BackColor = Color.Transparent, Location = new Point(34, ly), AutoSize = true };
            chartCard.Controls.AddRange(new Control[] { dot, ll });
            ly += 18;
        }
        pnlContent.Controls.Add(chartCard);
    }

    private void AddStatCard(int x, int y, int w, string icon, string title, string val, string sub, Color iconBg)
    {
        var card = MakeCard(x, y, w, 120);

        var iconBox = new Panel { Location = new Point(w - 56, 18), Size = new Size(40, 40), BackColor = Color.Transparent };
        iconBox.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            UITheme.FillRoundedRect(e.Graphics, new SolidBrush(iconBg), 0, 0, 40, 40, 8);
            e.Graphics.DrawString(icon, new Font("Segoe UI Emoji", 16f),
                new SolidBrush(UITheme.Primary), new RectangleF(0, 2, 40, 36),
                new StringFormat { Alignment = System.Drawing.StringAlignment.Center });
        };

        var lTitle = UITheme.MakeLabel(title, UITheme.FontXS,  UITheme.TextMuted,   20, 18);
        var lVal   = UITheme.MakeLabel(val,   new Font("Segoe UI", 22f, FontStyle.Bold), UITheme.TextPrimary, 20, 42);
        var lSub   = UITheme.MakeLabel(sub,   UITheme.FontSM,  UITheme.TextMuted,   20, 88);
        lTitle.BackColor = Color.Transparent;
        lVal.BackColor   = Color.Transparent;
        lSub.BackColor   = Color.Transparent;

        card.Controls.AddRange(new Control[] { iconBox, lTitle, lVal, lSub });
        pnlContent.Controls.Add(card);
    }

    internal static Panel MakeCard(int x, int y, int w, int h)
    {
        var p = new Panel { Location = new Point(x, y), Size = new Size(w, h), BackColor = Color.White };
        p.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, p.Width - 1, p.Height - 1, 12);
        };
        return p;
    }

    internal static void AddCardHeader(Panel card, string title, string sub)
    {
        var t = UITheme.MakeLabel(title, UITheme.FontH3,  UITheme.TextPrimary, 20, 14);
        var s = UITheme.MakeLabel(sub,   UITheme.FontSM,  UITheme.TextMuted,   20, 40);
        var d = UITheme.MakeDivider(0, 62, card.Width);
        t.BackColor = Color.Transparent;
        s.BackColor = Color.Transparent;
        card.Controls.AddRange(new Control[] { t, s, d });
    }
}
