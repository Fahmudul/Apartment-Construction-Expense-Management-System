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
    internal DataGridView dgvExpenses;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        // This form is loaded INSIDE pnlContent (1040 x 736)
        // So all positions are relative to 0,0 of this form
        BackColor = UITheme.BgPage;

        // Content area width = 1040, height = 736
        // Margins: left=20, right=20 → usable width = 1000
        int cx = 20; // content x start
        int cy = 20; // content y start
        int cw = 1120; // content width

        // Header row
        var title = UITheme.MakeLabel("All Expenses", UITheme.FontH2, UITheme.TextPrimary, cx, cy);
        var sub   = UITheme.MakeLabel("Manage and review all project expenses",
            UITheme.FontBase, UITheme.TextSecond, cx, cy + 36);
        btnAdd = UITheme.MakeButton("+ Add Expense", UITheme.Primary, Color.White, 1000, cy + 6, 140, 38);
        btnAdd.Click += btnAdd_Click;
        Controls.AddRange(new Control[] { title, sub, btnAdd });
        cy += 74;

        // Filter card: y=94, h=60
        var filterCard = new Panel { Location = new Point(cx, cy), Size = new Size(cw, 60), BackColor = Color.White };
        filterCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1), 0, 0, cw - 1, 59, 12);
        };
        txtSearch   = UITheme.MakeTextBox(12, 11, 180);   txtSearch.PlaceholderText = "🔍 Search...";
        cmbCategory = UITheme.MakeComboBox(204, 11, 160);
        cmbCategory.Items.AddRange(new object[] { "All Categories","Materials","Labour","Equipment","Electrical" });
        cmbCategory.SelectedIndex = 0;
        dtpFrom   = UITheme.MakeDatePicker(376, 11, 140);
        dtpTo     = UITheme.MakeDatePicker(528, 11, 140);
        btnFilter = UITheme.MakeButton("Filter", UITheme.Primary, Color.White, 680, 11, 80, 38);
        btnFilter.Click += btnFilter_Click;
        filterCard.Controls.AddRange(new Control[] { txtSearch, cmbCategory, dtpFrom, dtpTo, btnFilter });
        Controls.Add(filterCard);
        cy += 76;

        // Table card: y=170, h=736-170-20=546
        int tableH = 736 - cy - 20;
        var tableCard = new Panel { Location = new Point(cx, cy), Size = new Size(cw, tableH), BackColor = Color.White };
        tableCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1), 0, 0, cw - 1, tableH - 1, 12);
        };

        var tHeader = UITheme.MakeLabel("Expense Records", UITheme.FontH3, UITheme.TextPrimary, 20, 14);
        tHeader.BackColor = Color.Transparent;
        var tDiv = UITheme.MakeDivider(0, 58, cw);
        tableCard.Controls.AddRange(new Control[] { tHeader, tDiv });

        // DataGridView fills rest of table card
        dgvExpenses = UITheme.MakeDataGrid(0, 59, cw, tableH - 59);
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Title",      FillWeight = 28, Name = "colTitle" });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Category",   FillWeight = 18, Name = "colCategory" });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Amount (৳)", FillWeight = 14, Name = "colAmount" });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date",       FillWeight = 12, Name = "colDate" });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Added By",   FillWeight = 16, Name = "colAddedBy" });
        dgvExpenses.Columns.Add(new DataGridViewButtonColumn  { HeaderText = "Delete",     FillWeight = 12, Name = "colDelete",
            Text = "Delete", UseColumnTextForButtonValue = true });

        tableCard.Controls.Add(dgvExpenses);
        Controls.Add(tableCard);
    }
}
