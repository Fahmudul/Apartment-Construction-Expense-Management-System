using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class AdminExpensesForm
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtSearch;
    private ComboBox cmbCategory;
    private DateTimePicker dtpFrom;
    private DateTimePicker dtpTo;
    private Button btnFilter;
    private Button btnAdd;
    private DataGridView dgvExpenses;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        BackColor  = UITheme.BgPage;
        AutoScroll = true;

        int x = 24, y = 24;

        // Header
        var title = UITheme.MakeLabel("All Expenses",    UITheme.FontH2, UITheme.TextPrimary, x, y);
        var sub   = UITheme.MakeLabel("Manage and review all project expenses",
            UITheme.FontBase, UITheme.TextSecond, x, y + 36);

        btnAdd = UITheme.MakeButton("+ Add Expense", UITheme.Primary, Color.White, 900, y + 4, 140, 38);
        btnAdd.Click += btnAdd_Click;
        Resize += (s, e) => btnAdd.Location = new Point(Width - 164, y + 4);

        Controls.AddRange(new Control[] { title, sub, btnAdd });
        y += 76;

        // Filter card
        var filterCard = new Panel { Location = new Point(x, y), Size = new Size(900, 64),
            BackColor = Color.White };
        filterCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, filterCard.Width - 1, filterCard.Height - 1, 12);
        };

        txtSearch = UITheme.MakeTextBox(12, 12, 190);
        txtSearch.PlaceholderText = "🔍  Search...";

        cmbCategory = UITheme.MakeComboBox(214, 12, 150);
        cmbCategory.Items.AddRange(new object[] { "All Categories", "Materials", "Labour", "Equipment", "Electrical" });
        cmbCategory.SelectedIndex = 0;

        dtpFrom = UITheme.MakeDatePicker(376, 12, 130);
        dtpTo   = UITheme.MakeDatePicker(518, 12, 130);

        btnFilter = UITheme.MakeButton("Filter", UITheme.Primary, Color.White, 660, 12, 80, 38);
        btnFilter.Click += btnFilter_Click;

        filterCard.Controls.AddRange(new Control[] { txtSearch, cmbCategory, dtpFrom, dtpTo, btnFilter });
        Controls.Add(filterCard);
        Resize += (s, e) => filterCard.Width = Width - 48;
        y += 80;

        // Table card
        var tableCard = new Panel { Location = new Point(x, y), Size = new Size(900, 420),
            BackColor = Color.White };
        tableCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, tableCard.Width - 1, tableCard.Height - 1, 12);
        };

        var tHeader = UITheme.MakeLabel("Expense Records", UITheme.FontH3, UITheme.TextPrimary, 20, 14);
        tHeader.BackColor = Color.Transparent;
        var tDiv    = UITheme.MakeDivider(0, 58, 900);
        tableCard.Controls.AddRange(new Control[] { tHeader, tDiv });

        dgvExpenses = UITheme.MakeDataGrid(0, 59, 900, 361);
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Title",      FillWeight = 28 });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Category",   FillWeight = 18 });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Amount (৳)", FillWeight = 14 });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date",       FillWeight = 12 });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Added By",   FillWeight = 16 });
        dgvExpenses.Columns.Add(new DataGridViewButtonColumn  { HeaderText = "Delete",     FillWeight = 12,
            Text = "Delete", UseColumnTextForButtonValue = true });

        dgvExpenses.Rows.Add("Cement bags - Block A", "Materials",  "45,000",  "2025-01-15", "Rahima");
        dgvExpenses.Rows.Add("Labour - Foundation",   "Labour",     "80,000",  "2025-01-16", "Karim");
        dgvExpenses.Rows.Add("Crane Rental",          "Equipment",  "1,20,000","2025-01-18", "Admin");
        dgvExpenses.Rows.Add("Wiring - 3rd Floor",    "Electrical", "32,500",  "2025-01-20", "Rahima");
        dgvExpenses.Rows.Add("Plumbing - Basement",   "Plumbing",   "55,000",  "2025-01-22", "Karim");

        tableCard.Controls.Add(dgvExpenses);
        Controls.Add(tableCard);

        Resize += (s, e) =>
        {
            tableCard.Width    = Width - 48;
            tDiv.Width         = tableCard.Width;
            dgvExpenses.Width  = tableCard.Width;
        };
    }
}
