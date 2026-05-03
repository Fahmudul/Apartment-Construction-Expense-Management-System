using System;
using System.Windows.Forms;

namespace ApartmentWinForms.Forms;

public partial class UserDashboardForm : Form
{
    public UserDashboardForm()
    {
        InitializeComponent();
        ShowDashboardContent();
    }

    private void btnDashboard_Click(object sender, EventArgs e)  => ShowDashboardContent();
    private void btnMyExpenses_Click(object sender, EventArgs e) => ShowPanel(new UserExpensesForm());
    private void btnAddExpense_Click(object sender, EventArgs e) => new AddExpenseForm().ShowDialog();

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
        pnlContent.Controls.Clear();

        int x = 24, y = 24;
        int w = pnlContent.Width - 48;

        // Stat card
        var statCard = AdminDashboardForm.MakeCard(x, y, 300, 120);
        var iconBox  = new System.Windows.Forms.Panel
        {
            Location  = new System.Drawing.Point(244, 18),
            Size      = new System.Drawing.Size(40, 40),
            BackColor = System.Drawing.Color.Transparent,
        };
        iconBox.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Helpers.UITheme.FillRoundedRect(e.Graphics, new System.Drawing.SolidBrush(Helpers.UITheme.PrimaryLight), 0, 0, 40, 40, 8);
            e.Graphics.DrawString("💳", new System.Drawing.Font("Segoe UI Emoji", 16f),
                new System.Drawing.SolidBrush(Helpers.UITheme.Primary),
                new System.Drawing.RectangleF(0, 2, 40, 36),
                new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center });
        };
        var stTitle = Helpers.UITheme.MakeLabel("MY TOTAL EXPENSES", Helpers.UITheme.FontXS, Helpers.UITheme.TextMuted,   20, 18);
        var stVal   = Helpers.UITheme.MakeLabel("৳ 3,24,800", new System.Drawing.Font("Segoe UI", 22f, System.Drawing.FontStyle.Bold), Helpers.UITheme.TextPrimary, 20, 42);
        var stSub   = Helpers.UITheme.MakeLabel("Across 18 expense entries", Helpers.UITheme.FontSM, Helpers.UITheme.TextMuted, 20, 88);
        stTitle.BackColor = stVal.BackColor = stSub.BackColor = System.Drawing.Color.Transparent;
        statCard.Controls.AddRange(new System.Windows.Forms.Control[] { iconBox, stTitle, stVal, stSub });
        pnlContent.Controls.Add(statCard);
        y += 140;

        // Recent table
        var tableCard = AdminDashboardForm.MakeCard(x, y, w, 320);
        AdminDashboardForm.AddCardHeader(tableCard, "My Recent Expenses", "Your latest submitted expenses");

        var addBtn = Helpers.UITheme.MakeButton("+ Add New", Helpers.UITheme.Primary, System.Drawing.Color.White,
            tableCard.Width - 124, 12, 100, 36);
        addBtn.Click += (s, e) => new AddExpenseForm().ShowDialog();
        tableCard.Controls.Add(addBtn);

        var dgv = Helpers.UITheme.MakeDataGrid(0, 66, tableCard.Width, 254);
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Title",      FillWeight = 30 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Category",   FillWeight = 20 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Amount (৳)", FillWeight = 18 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date",       FillWeight = 14 });
        dgv.Columns.Add(new DataGridViewButtonColumn  { HeaderText = "Edit",       FillWeight = 9,
            Text = "Edit", UseColumnTextForButtonValue = true });
        dgv.Columns.Add(new DataGridViewButtonColumn  { HeaderText = "Delete",     FillWeight = 9,
            Text = "Delete", UseColumnTextForButtonValue = true });
        dgv.Rows.Add("Cement bags - Block A", "Materials",  "45,000",  "2025-01-15");
        dgv.Rows.Add("Labour - Foundation",   "Labour",     "80,000",  "2025-01-16");
        dgv.Rows.Add("Wiring - 3rd Floor",    "Electrical", "32,500",  "2025-01-20");
        tableCard.Controls.Add(dgv);
        pnlContent.Controls.Add(tableCard);

        pnlContent.Resize += (s, e) =>
        {
            tableCard.Width = pnlContent.Width - 48;
            dgv.Width       = tableCard.Width;
            addBtn.Location = new System.Drawing.Point(tableCard.Width - 124, 12);
        };
    }
}
