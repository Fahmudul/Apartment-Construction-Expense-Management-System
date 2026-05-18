
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class EditCategoryForm
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtCategoryName;
    private TextBox txtCategoryIcon;
    private TextBox txtDescription;
    private Button btnSave;
    private Button btnClose;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent(string name, string icon, string? description)
    {
        Text = "Edit Category";
        ClientSize = new Size(460, 420);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        BackColor = UITheme.BgPage;

        var card = new Panel { Location = new Point(20, 20), Size = new Size(420, 380), BackColor = Color.White };
        card.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, 419, 379, 16);
        };

        // Header
        var iconBox = new Panel { Location = new Point(30, 26), Size = new Size(44, 44), BackColor = Color.Transparent };
        iconBox.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.FillRoundedRect(e.Graphics, new SolidBrush(UITheme.PrimaryLight), 0, 0, 44, 44, 10);
            e.Graphics.DrawString("🏷", new Font("Segoe UI Emoji", 20f),
                new SolidBrush(UITheme.Primary), new RectangleF(0, 4, 44, 36),
                new StringFormat { Alignment = StringAlignment.Center });
        };

        var hTitle = UITheme.MakeLabel("Edit Category", UITheme.FontH3, UITheme.TextPrimary, 84, 26);
        var hSub = UITheme.MakeLabel("Update category details", UITheme.FontSM, UITheme.TextMuted, 84, 52);
        hTitle.BackColor = hSub.BackColor = Color.Transparent;
        var divider = UITheme.MakeDivider(0, 82, 420);

        // Fields
        var lblName = UITheme.MakeLabel("CATEGORY NAME", UITheme.FontLabel, UITheme.TextSecond, 30, 100);
        txtCategoryName = UITheme.MakeTextBox(30, 120, 360);
        txtCategoryName.Text = name;

        var lblIcon = UITheme.MakeLabel("ICON (Emoji)", UITheme.FontLabel, UITheme.TextSecond, 30, 172);
        txtCategoryIcon = UITheme.MakeTextBox(30, 192, 360);
        txtCategoryIcon.Text = icon;

        var lblDesc = UITheme.MakeLabel("DESCRIPTION", UITheme.FontLabel, UITheme.TextSecond, 30, 244);
        txtDescription = UITheme.MakeTextBox(30, 264, 360);
        txtDescription.Text = description ?? "";

        lblName.BackColor = lblIcon.BackColor = lblDesc.BackColor = Color.Transparent;

        // Buttons
        btnClose = UITheme.MakeOutlineButton("Cancel", 30, 320, 170, 42);
        btnSave = UITheme.MakeButton("💾  Save Changes", UITheme.AccentGreen, Color.White, 220, 320, 170, 42);
        btnSave.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
        btnClose.Click += btnClose_Click;
        btnSave.Click += btnSave_Click;

        card.Controls.AddRange(new Control[]
        {
            iconBox, hTitle, hSub, divider,
            lblName, txtCategoryName,
            lblIcon, txtCategoryIcon,
            lblDesc, txtDescription,
            btnClose, btnSave
        });
        Controls.Add(card);
    }
}