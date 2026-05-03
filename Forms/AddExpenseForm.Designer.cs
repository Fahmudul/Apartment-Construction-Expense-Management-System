using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class AddExpenseForm
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtTitle;
    private ComboBox cmbCategory;
    private TextBox txtAmount;
    private DateTimePicker dtpDate;
    private Button btnSave;
    private Button btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Text            = "Add Expense";
        Size            = new Size(520, 560);
        StartPosition   = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox     = false;
        MinimizeBox     = false;
        BackColor       = UITheme.BgPage;

        var card = new Panel { Location = new Point(20, 20), Size = new Size(460, 500),
            BackColor = Color.White };
        card.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, card.Width - 1, card.Height - 1, 16);
        };

        // Header
        var iconBox = new Panel { Location = new Point(30, 26), Size = new Size(44, 44),
            BackColor = Color.Transparent };
        iconBox.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.FillRoundedRect(e.Graphics, new SolidBrush(UITheme.PrimaryLight), 0, 0, 44, 44, 10);
            e.Graphics.DrawString("💳", new Font("Segoe UI Emoji", 20f),
                new SolidBrush(UITheme.Primary), new RectangleF(0, 4, 44, 36),
                new StringFormat { Alignment = StringAlignment.Center });
        };

        var hTitle = UITheme.MakeLabel("Add New Expense", UITheme.FontH3, UITheme.TextPrimary, 84, 26);
        var hSub   = UITheme.MakeLabel("Fill in the details below", UITheme.FontSM, UITheme.TextMuted, 84, 52);
        hTitle.BackColor = hSub.BackColor = Color.Transparent;

        var divider = UITheme.MakeDivider(0, 82, 460);

        // Fields
        var lblTitle = UITheme.MakeLabel("EXPENSE TITLE",   UITheme.FontLabel, UITheme.TextSecond, 30, 100);
        txtTitle     = UITheme.MakeTextBox(30, 120, 400);
        txtTitle.PlaceholderText = "e.g. Cement bags - Block A";

        var lblCat = UITheme.MakeLabel("CATEGORY",          UITheme.FontLabel, UITheme.TextSecond, 30, 172);
        cmbCategory = UITheme.MakeComboBox(30, 192, 400);
        cmbCategory.Items.AddRange(new object[]
        {
            "🧱  Materials", "👷  Labour",   "🔧  Equipment",
            "⚡  Electrical","🪟  Fixtures", "🚿  Plumbing",
            "🚛  Transport", "📋  Other",
        });
        cmbCategory.SelectedIndex = 0;

        var lblCatNote = UITheme.MakeLabel(
            "Select from list only — cannot type a custom category",
            UITheme.FontXS, UITheme.TextMuted, 30, 234);
        lblCatNote.BackColor = Color.Transparent;

        var lblAmt = UITheme.MakeLabel("AMOUNT (৳)",        UITheme.FontLabel, UITheme.TextSecond, 30, 258);
        txtAmount  = UITheme.MakeTextBox(30, 278, 400);
        txtAmount.PlaceholderText = "0.00";

        var lblDate = UITheme.MakeLabel("EXPENSE DATE",     UITheme.FontLabel, UITheme.TextSecond, 30, 330);
        dtpDate     = UITheme.MakeDatePicker(30, 350, 400);

        lblTitle.BackColor = lblCat.BackColor = lblAmt.BackColor = lblDate.BackColor = Color.Transparent;

        // Buttons
        btnCancel = UITheme.MakeOutlineButton("Cancel",         30, 412, 190, 46);
        btnSave   = UITheme.MakeButton("💾  Save Expense",
            UITheme.AccentGreen, Color.White,             240, 412, 190, 46);
        btnSave.Font   = new Font("Segoe UI", 12f, FontStyle.Bold);
        btnCancel.Click += btnCancel_Click;
        btnSave.Click   += btnSave_Click;

        card.Controls.AddRange(new Control[]
        {
            iconBox, hTitle, hSub, divider,
            lblTitle, txtTitle,
            lblCat, cmbCategory, lblCatNote,
            lblAmt, txtAmount,
            lblDate, dtpDate,
            btnCancel, btnSave,
        });
        Controls.Add(card);
    }
}
