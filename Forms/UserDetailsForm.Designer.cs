using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class UserDetailsForm
{
    private System.ComponentModel.IContainer components = null;
    private Button btnApprove;
    private Button btnReject;
    private Button btnClose;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent(string name, string email, string joined, string status)
    {
        Text            = "User Details";
        ClientSize      = new Size(480, 520);
        StartPosition   = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox     = false;
        MinimizeBox     = false;
        BackColor       = UITheme.BgPage;

        var card = new Panel { Location = new Point(20, 20), Size = new Size(440, 480), BackColor = Color.White };
        card.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1), 0, 0, 439, 479, 16);
        };

        var avatarColor = status == "Approved" ? UITheme.AccentGreen :
                          status == "Blocked"  ? UITheme.AccentRed : UITheme.AccentAmber;

        var avatar = new Panel { Location = new Point(184, 28), Size = new Size(72, 72), BackColor = Color.Transparent };
        avatar.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(new SolidBrush(avatarColor), 0, 0, 72, 72);
            e.Graphics.DrawString(name[0].ToString(), new Font("Segoe UI", 28f, FontStyle.Bold),
                new SolidBrush(Color.White), new RectangleF(0, 4, 72, 64),
                new StringFormat { Alignment = StringAlignment.Center });
        };

        var nameLbl = new Label { Text = name, Font = new Font("Segoe UI", 16f, FontStyle.Bold),
            ForeColor = UITheme.TextPrimary, BackColor = Color.Transparent,
            Location = new Point(0, 110), Size = new Size(440, 30), TextAlign = ContentAlignment.MiddleCenter };

        var (stBg, stFg) = status == "Approved" ? (UITheme.StatusApprBg, UITheme.StatusApprFg) :
                           status == "Blocked"  ? (UITheme.StatusBlokBg, UITheme.StatusBlokFg) :
                                                  (UITheme.StatusPendBg, UITheme.StatusPendFg);

        var statusBadge = new Label { Text = status, Font = UITheme.FontSMBold,
            ForeColor = stFg, BackColor = stBg,
            Location = new Point(176, 150), AutoSize = true, Padding = new Padding(10, 4, 10, 4) };

        // Details box
        var detailBox = new Panel { Location = new Point(20, 188), Size = new Size(400, 130), BackColor = UITheme.BgPage };
        detailBox.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.FillRoundedRect(e.Graphics, new SolidBrush(UITheme.BgPage), 0, 0, 400, 130, 10);
        };

        var details = new[]
        {
            ("EMAIL",  email,    10, 14),
            ("JOINED", joined,  210, 14),
            ("ROLE",   "User",   10, 74),
            ("STATUS", status,  210, 74),
        };
        foreach (var (lbl, val, dx, dy) in details)
        {
            var l = UITheme.MakeLabel(lbl, UITheme.FontXS,    UITheme.TextMuted,    dx, dy);
            var v = UITheme.MakeLabel(val, UITheme.FontSMBold, UITheme.TextPrimary, dx, dy + 18);
            l.BackColor = v.BackColor = Color.Transparent;
            detailBox.Controls.AddRange(new Control[] { l, v });
        }

        // Action buttons — row at y=338
        btnApprove = UITheme.MakeButton("✓ Approve", UITheme.AccentGreen, Color.White,  20, 338, 120, 42);
        btnReject  = UITheme.MakeButton("✗ Reject",  UITheme.AccentRed,   Color.White, 156, 338, 110, 42);
        btnClose   = UITheme.MakeOutlineButton("Close",                               282, 338, 110, 42);

        btnApprove.Click += btnApprove_Click;
        btnReject.Click  += btnReject_Click;
        btnClose.Click   += btnClose_Click;

        card.Controls.AddRange(new Control[]
        { avatar, nameLbl, statusBadge, detailBox, btnApprove, btnReject, btnClose });
        Controls.Add(card);
    }
}
