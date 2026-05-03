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

        var title = UITheme.MakeLabel("Categories", UITheme.FontH2, UITheme.TextPrimary, x, y);
        var sub   = UITheme.MakeLabel("Manage expense categories for the project",
            UITheme.FontBase, UITheme.TextSecond, x, y + 36);
        Controls.AddRange(new Control[] { title, sub });
        y += 76;

        // Category list card
        var listCard = new Panel { Location = new Point(x, y), Size = new Size(560, 380),
            BackColor = Color.White };
        listCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, listCard.Width - 1, listCard.Height - 1, 12);
        };

        var lh = UITheme.MakeLabel("All Categories", UITheme.FontH3, UITheme.TextPrimary, 20, 14);
        lh.BackColor = Color.Transparent;
        var ld = UITheme.MakeDivider(0, 56, 560);
        listCard.Controls.AddRange(new Control[] { lh, ld });

        var cats = new[]
        {
            ("🧱", "Materials",  "42 expenses", UITheme.PrimaryLight),
            ("👷", "Labour",     "28 expenses", Color.FromArgb(209, 250, 229)),
            ("🔧", "Equipment",  "18 expenses", Color.FromArgb(254, 243, 199)),
            ("⚡", "Electrical", "15 expenses", Color.FromArgb(254, 226, 226)),
            ("🚿", "Plumbing",   "10 expenses", Color.FromArgb(224, 242, 254)),
        };

        int ry = 58; bool alt = false;
        foreach (var (icon, name, count, iconBg) in cats)
        {
            var row = new Panel { Location = new Point(0, ry), Size = new Size(560, 60),
                BackColor = alt ? UITheme.BgTableAlt : Color.White };
            row.Paint += (s, e) =>
                e.Graphics.DrawLine(new Pen(UITheme.BorderLight, 1), 0, 59, 560, 59);

            var ib = new Panel { Location = new Point(16, 10), Size = new Size(40, 40), BackColor = Color.Transparent };
            ib.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                UITheme.FillRoundedRect(e.Graphics, new SolidBrush(iconBg), 0, 0, 40, 40, 8);
                e.Graphics.DrawString(icon, new Font("Segoe UI Emoji", 16f),
                    new SolidBrush(UITheme.TextPrimary), new RectangleF(0, 4, 40, 32),
                    new StringFormat { Alignment = StringAlignment.Center });
            };

            var nl = UITheme.MakeLabel(name,  UITheme.FontSemi, UITheme.TextPrimary, 68, 10);
            nl.BackColor = Color.Transparent;
            var cl = UITheme.MakeLabel(count, UITheme.FontSM,   UITheme.TextMuted,   68, 34);
            cl.BackColor = Color.Transparent;

            var editBtn = UITheme.MakeButton("Edit",   UITheme.PrimaryLight, UITheme.Primary,  384, 14, 68, 32);
            editBtn.Font = UITheme.FontSM;
            var delBtn  = UITheme.MakeButton("Delete", UITheme.StatusBlokBg, UITheme.AccentRed, 462, 14, 76, 32);
            delBtn.Font  = UITheme.FontSM;

            row.Controls.AddRange(new Control[] { ib, nl, cl, editBtn, delBtn });
            listCard.Controls.Add(row);
            ry += 60; alt = !alt;
        }

        Controls.Add(listCard);

        // Add form card
        var formCard = new Panel { Location = new Point(x + 584, y), Size = new Size(320, 300),
            BackColor = Color.White };
        formCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, formCard.Width - 1, formCard.Height - 1, 12);
        };

        var fh = UITheme.MakeLabel("Add Category",               UITheme.FontH3, UITheme.TextPrimary, 20, 18);
        var fs = UITheme.MakeLabel("Create a new expense category", UITheme.FontSM, UITheme.TextMuted, 20, 46);
        fh.BackColor = Color.Transparent;
        fs.BackColor = Color.Transparent;

        var nl2 = UITheme.MakeLabel("CATEGORY NAME", UITheme.FontLabel, UITheme.TextSecond, 20, 78);
        txtCategoryName = UITheme.MakeTextBox(20, 98, 280);
        txtCategoryName.PlaceholderText = "e.g. Plumbing";

        var il = UITheme.MakeLabel("ICON (Emoji)", UITheme.FontLabel, UITheme.TextSecond, 20, 148);
        txtCategoryIcon = UITheme.MakeTextBox(20, 168, 280);
        txtCategoryIcon.PlaceholderText = "🏷";

        btnAddCategory = UITheme.MakeButton("+ Add Category", UITheme.AccentGreen, Color.White, 20, 220, 280, 42);
        btnAddCategory.Click += btnAddCategory_Click;

        formCard.Controls.AddRange(new Control[]
        { fh, fs, nl2, txtCategoryName, il, txtCategoryIcon, btnAddCategory });
        Controls.Add(formCard);
    }
}
