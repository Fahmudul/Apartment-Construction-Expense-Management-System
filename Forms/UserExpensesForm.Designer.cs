using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class UserExpensesForm
{
    private System.ComponentModel.IContainer components = null;
    internal DataGridView dgvExpenses;
    private Button btnAdd;

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

        var title = UITheme.MakeLabel("My Expenses",            UITheme.FontH2, UITheme.TextPrimary, x, y);
        var sub   = UITheme.MakeLabel("Your submitted expense records",
            UITheme.FontBase, UITheme.TextSecond, x, y + 36);

        btnAdd = UITheme.MakeButton("+ Add Expense", UITheme.Primary, Color.White, 900, y + 4, 140, 38);
        btnAdd.Click += btnAdd_Click;
        Resize += (s, e) => btnAdd.Location = new Point(Width - 164, y + 4);
        Controls.AddRange(new Control[] { title, sub, btnAdd });
        y += 76;

        var tableCard = new Panel { Location = new Point(x, y), Size = new Size(900, 420),
            BackColor = Color.White };
        tableCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, tableCard.Width - 1, tableCard.Height - 1, 12);
        };

        var th = UITheme.MakeLabel("All My Expenses", UITheme.FontH3, UITheme.TextPrimary, 20, 14);
        th.BackColor = Color.Transparent;
        var td = UITheme.MakeDivider(0, 58, 900);
        tableCard.Controls.AddRange(new Control[] { th, td });

        dgvExpenses = UITheme.MakeDataGrid(0, 59, 900, 361);
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Title",      FillWeight = 30 });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Category",   FillWeight = 20 });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Amount (৳)", FillWeight = 16 });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date",       FillWeight = 14 });
        dgvExpenses.Columns.Add(new DataGridViewButtonColumn  { HeaderText = "Edit",       FillWeight = 10,
            Text = "Edit", UseColumnTextForButtonValue = true });
        dgvExpenses.Columns.Add(new DataGridViewButtonColumn  { HeaderText = "Delete",     FillWeight = 10,
            Text = "Delete", UseColumnTextForButtonValue = true });

        dgvExpenses.Rows.Add("Cement bags - Block A", "Materials",  "45,000",  "2025-01-15");
        dgvExpenses.Rows.Add("Labour - Foundation",   "Labour",     "80,000",  "2025-01-16");
        dgvExpenses.Rows.Add("Wiring - 3rd Floor",    "Electrical", "32,500",  "2025-01-20");
        dgvExpenses.Rows.Add("Plumbing - Basement",   "Plumbing",   "55,000",  "2025-01-22");
        dgvExpenses.CellClick += dgvExpenses_CellClick;

        tableCard.Controls.Add(dgvExpenses);
        Controls.Add(tableCard);

        Resize += (s, e) =>
        {
            tableCard.Width   = Width - 48;
            td.Width          = tableCard.Width;
            dgvExpenses.Width = tableCard.Width;
        };
    }
}
