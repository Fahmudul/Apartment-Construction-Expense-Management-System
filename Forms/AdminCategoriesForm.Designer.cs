using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class AdminCategoriesForm
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtCategoryName;
    private TextBox txtCategoryIcon;
    private Button btnAddCategory;
    internal DataGridView dgvCategories;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    
    private void InitializeComponent()
    {
        // Loaded inside pnlContent: 1160 x 736 (1120 usable width after padding)
        BackColor = UITheme.BgPage;
        int cx = 20, cy = 20, cw = 1120;

        var title = UITheme.MakeLabel("Categories", UITheme.FontH2, UITheme.TextPrimary, cx, cy);
        var sub = UITheme.MakeLabel("Manage expense categories for the project",
            UITheme.FontBase, UITheme.TextSecond, cx, cy + 36);
        Controls.AddRange(new Control[] { title, sub });
        cy += 74;

        // NEW BALANCED WIDTHS TO FILL 1120px
        int listW = 770;
        int formW = 330;
        int cardH = 736 - cy - 20; // 642px

        // List card
        var listCard = new Panel { Location = new Point(cx, cy), Size = new Size(listW, cardH), BackColor = Color.White };
        listCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1), 0, 0, listW - 1, cardH - 1, 12);
        };
        var lh = UITheme.MakeLabel("All Categories", UITheme.FontH3, UITheme.TextPrimary, 20, 14);
        lh.BackColor = Color.Transparent;
        var ld = UITheme.MakeDivider(0, 56, listW);
        listCard.Controls.AddRange(new Control[] { lh, ld });

        // DataGridView for categories (now stretches to 770px wide)
        dgvCategories = UITheme.MakeDataGrid(0, 57, listW, cardH - 57);
        dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Icon", FillWeight = 8, Name = "colIcon" });
        dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Category Name", FillWeight = 36, Name = "colName" });
        dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Description", FillWeight = 34, Name = "colDesc" });
        dgvCategories.Columns.Add(new DataGridViewButtonColumn
        {
            HeaderText = "Edit",
            FillWeight = 11,
            Name = "colEdit",
            Text = "Edit",
            UseColumnTextForButtonValue = true
        });
        dgvCategories.Columns.Add(new DataGridViewButtonColumn
        {
            HeaderText = "Delete",
            FillWeight = 11,
            Name = "colDelete",
            Text = "Delete",
            UseColumnTextForButtonValue = true
        });
        listCard.Controls.Add(dgvCategories);
        Controls.Add(listCard);

        // Form card (Positions exactly right at X = 20 + 770 + 20 = 810)
        var formCard = new Panel
        {
            Location = new Point(cx + listW + 20, cy),
            Size = new Size(formW, 290),
            BackColor = Color.White,
        };
        formCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1), 0, 0, formW - 1, 289, 12);
        };

        var fh = UITheme.MakeLabel("Add Category", UITheme.FontH3, UITheme.TextPrimary, 20, 18);
        var fs = UITheme.MakeLabel("Create a new expense category", UITheme.FontSM, UITheme.TextMuted, 20, 46);
        fh.BackColor = fs.BackColor = Color.Transparent;

        // Expanded input field widths from 270 to 290 to look clean inside the 330px card
        var nl2 = UITheme.MakeLabel("CATEGORY NAME", UITheme.FontLabel, UITheme.TextSecond, 20, 80);
        txtCategoryName = UITheme.MakeTextBox(20, 100, 290); txtCategoryName.PlaceholderText = "e.g. Plumbing";

        var il = UITheme.MakeLabel("ICON (Emoji)", UITheme.FontLabel, UITheme.TextSecond, 20, 150);
        txtCategoryIcon = UITheme.MakeTextBox(20, 170, 290); txtCategoryIcon.PlaceholderText = "🏷";

        btnAddCategory = UITheme.MakeButton("+ Add Category", UITheme.AccentGreen, Color.White, 20, 222, 290, 42);
        btnAddCategory.Click += btnAddCategory_Click;

        formCard.Controls.AddRange(new Control[] { fh, fs, nl2, txtCategoryName, il, txtCategoryIcon, btnAddCategory });
        Controls.Add(formCard);
    }



}
