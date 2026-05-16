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
        // Loaded inside pnlContent: 1040 x 736
        BackColor = UITheme.BgPage;
        int cx = 20, cy = 20, cw = 1120;

        var title = UITheme.MakeLabel("My Expenses",            UITheme.FontH2, UITheme.TextPrimary, cx, cy);
        var sub   = UITheme.MakeLabel("Your submitted expense records",
            UITheme.FontBase, UITheme.TextSecond, cx, cy + 36);
        btnAdd = UITheme.MakeButton("+ Add Expense", UITheme.Primary, Color.White, 1000, cy + 6, 140, 38);
        btnAdd.Click += btnAdd_Click;
        Controls.AddRange(new Control[] { title, sub, btnAdd });
        cy += 74;

        int tableH = 736 - cy - 20;
        var tableCard = new Panel { Location = new Point(cx, cy), Size = new Size(cw, tableH), BackColor = Color.White };
        tableCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1), 0, 0, cw - 1, tableH - 1, 12);
        };

        var th = UITheme.MakeLabel("All My Expenses", UITheme.FontH3, UITheme.TextPrimary, 20, 14);
        th.BackColor = Color.Transparent;
        var td = UITheme.MakeDivider(0, 56, cw);
        tableCard.Controls.AddRange(new Control[] { th, td });

        dgvExpenses = UITheme.MakeDataGrid(0, 57, cw, tableH - 57);
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Title",      FillWeight = 30, Name = "colTitle" });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Category",   FillWeight = 20, Name = "colCategory" });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Amount (৳)", FillWeight = 16, Name = "colAmount" });
        dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date",       FillWeight = 14, Name = "colDate" });
        dgvExpenses.Columns.Add(new DataGridViewButtonColumn  { HeaderText = "Edit",       FillWeight = 10, Name = "colEdit",
            Text = "Edit", UseColumnTextForButtonValue = true });
        dgvExpenses.Columns.Add(new DataGridViewButtonColumn  { HeaderText = "Delete",     FillWeight = 10, Name = "colDelete",
            Text = "Delete", UseColumnTextForButtonValue = true });
        dgvExpenses.CellClick += dgvExpenses_CellClick;

        tableCard.Controls.Add(dgvExpenses);
        Controls.Add(tableCard);
    }
}
