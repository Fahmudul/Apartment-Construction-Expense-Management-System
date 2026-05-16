using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class PendingApprovalForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlCard;
    private Button btnBack;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Text            = "ApartmentExpense — Awaiting Approval";
        ClientSize      = new Size(540, 460);
        StartPosition   = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox     = false;
        MinimizeBox     = false;
        BackColor       = UITheme.BgPage;

        pnlCard = new Panel { Location = new Point(40, 40), Size = new Size(460, 380), BackColor = Color.White };
        pnlCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, pnlCard.Width - 1, pnlCard.Height - 1, 16);
        };

        var iconBg = new Panel { Location = new Point(190, 28), Size = new Size(80, 80), BackColor = Color.Transparent };
        iconBg.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(new SolidBrush(UITheme.StatusPendBg), 0, 0, 80, 80);
        };
        var iconLbl = new Label { Text = "⏳", Font = new Font("Segoe UI Emoji", 28f),
            BackColor = Color.Transparent, ForeColor = Color.Black, Location = new Point(12, 14), AutoSize = true };
        iconBg.Controls.Add(iconLbl);

        var title = new Label { Text = "Pending Approval",
            Font = new Font("Segoe UI", 18f, FontStyle.Bold), ForeColor = UITheme.TextPrimary,
            BackColor = Color.Transparent, Location = new Point(0, 122), Size = new Size(460, 34),
            TextAlign = ContentAlignment.MiddleCenter };

        var msg1 = new Label { Text = "Your account is waiting for admin approval",
            Font = UITheme.FontBase, ForeColor = UITheme.TextSecond, BackColor = Color.Transparent,
            Location = new Point(0, 162), Size = new Size(460, 24), TextAlign = ContentAlignment.MiddleCenter };

        var msg2 = new Label { Text = "You'll receive access once an administrator reviews your account.",
            Font = UITheme.FontSM, ForeColor = UITheme.TextMuted, BackColor = Color.Transparent,
            Location = new Point(20, 192), Size = new Size(420, 36), TextAlign = ContentAlignment.MiddleCenter };

        var statusRow = new Panel { Location = new Point(28, 244), Size = new Size(404, 44), BackColor = Color.Transparent };
        statusRow.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.FillRoundedRect(e.Graphics, new SolidBrush(Color.FromArgb(255, 251, 235)), 0, 0, 404, 44, 10);
            e.Graphics.FillEllipse(new SolidBrush(UITheme.AccentAmber), 14, 17, 10, 10);
        };
        var statusTxt = new Label { Text = "Account Status: Under Review",
            Font = UITheme.FontSMBold, ForeColor = UITheme.StatusPendFg,
            BackColor = Color.Transparent, Location = new Point(34, 13), AutoSize = true };
        statusRow.Controls.Add(statusTxt);

        btnBack = UITheme.MakeOutlineButton("Back to Sign In", 28, 306, 404, 42);
        btnBack.Click += btnBack_Click;

        pnlCard.Controls.AddRange(new Control[] { iconBg, title, msg1, msg2, statusRow, btnBack });
        Controls.Add(pnlCard);
    }
}
