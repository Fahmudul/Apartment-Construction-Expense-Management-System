using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class AdminUsersForm
{
    private System.ComponentModel.IContainer components = null;

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

        var title = UITheme.MakeLabel("User Management", UITheme.FontH2, UITheme.TextPrimary, x, y);
        var sub   = UITheme.MakeLabel("Manage team access and permissions",
            UITheme.FontBase, UITheme.TextSecond, x, y + 36);
        Controls.AddRange(new Control[] { title, sub });

        // Pending badge
        var pendBg = new Panel { Size = new Size(210, 38), BackColor = Color.Transparent };
        pendBg.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.FillRoundedRect(e.Graphics, new SolidBrush(UITheme.StatusPendBg), 0, 0, 210, 38, 10);
        };
        var pendTxt = new Label { Text = "⚠️  3 pending approvals",
            Font = UITheme.FontSMBold, ForeColor = UITheme.StatusPendFg,
            BackColor = Color.Transparent, Location = new Point(12, 10), AutoSize = true };
        pendBg.Controls.Add(pendTxt);
        Resize += (s, e) => pendBg.Location = new Point(Width - 234, y + 4);
        Controls.Add(pendBg);
        y += 76;

        // Table card
        var tableCard = new Panel { Location = new Point(x, y), Size = new Size(960, 440),
            BackColor = Color.White };
        tableCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, tableCard.Width - 1, tableCard.Height - 1, 12);
        };

        // Column header row
        var headerRow = new Panel { Location = new Point(0, 0), Size = new Size(960, 44),
            BackColor = UITheme.BgTableHdr };
        string[] hdrs   = { "NAME", "EMAIL", "JOINED", "STATUS", "ACTIONS" };
        int[]    hWidths = { 200, 220, 110, 120, 310 };
        int hx = 16;
        foreach (var (h, hw) in System.Linq.Enumerable.Zip(hdrs, hWidths))
        {
            var hl = new Label { Text = h, Font = UITheme.FontSMBold, ForeColor = UITheme.TextSecond,
                BackColor = Color.Transparent, Location = new Point(hx, 14), Width = hw };
            headerRow.Controls.Add(hl);
            hx += hw;
        }
        var hDiv = UITheme.MakeDivider(0, 43, 960);
        tableCard.Controls.AddRange(new Control[] { headerRow, hDiv });

        // User rows
        var users = new[]
        {
            ("J", "John Smith",   "john@company.com",  "Jan 15, 2025", "Pending",
             UITheme.AccentAmber, UITheme.StatusPendBg, UITheme.StatusPendFg),
            ("R", "Rahima Begum", "rahima@corp.bd",    "Dec 02, 2024", "Approved",
             UITheme.AccentGreen, UITheme.StatusApprBg, UITheme.StatusApprFg),
            ("K", "Karim Ahmed",  "karim@site.com",    "Nov 20, 2024", "Blocked",
             UITheme.AccentRed,   UITheme.StatusBlokBg, UITheme.StatusBlokFg),
        };

        int ry = 44; bool alt = false;
        foreach (var (init, name, email, joined, status, avColor, stBg, stFg) in users)
        {
            var row = new Panel { Location = new Point(0, ry), Size = new Size(960, 66),
                BackColor = status == "Pending" ? Color.FromArgb(255, 251, 235) :
                            alt ? UITheme.BgTableAlt : Color.White };
            row.Paint += (s, e) =>
                e.Graphics.DrawLine(new Pen(UITheme.BorderLight, 1), 0, 65, 960, 65);

            // Avatar
            var av = new Panel { Location = new Point(16, 15), Size = new Size(36, 36), BackColor = Color.Transparent };
            av.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(avColor), 0, 0, 36, 36);
                e.Graphics.DrawString(init, new Font("Segoe UI", 14f, FontStyle.Bold),
                    new SolidBrush(Color.White), new RectangleF(0, 2, 36, 32),
                    new StringFormat { Alignment = StringAlignment.Center });
            };

            var nL = UITheme.MakeLabel(name,   UITheme.FontSemi, UITheme.TextPrimary, 60, 14);
            nL.BackColor = Color.Transparent;
            var eL = UITheme.MakeLabel(email,  UITheme.FontBase, UITheme.TextSecond,  216, 22);
            eL.BackColor = Color.Transparent;
            var jL = UITheme.MakeLabel(joined, UITheme.FontSM,   UITheme.TextMuted,   436, 24);
            jL.BackColor = Color.Transparent;

            var stBadge = new Label { Text = status, Font = UITheme.FontSM,
                ForeColor = stFg, BackColor = stBg,
                Location = new Point(546, 20), AutoSize = true, Padding = new Padding(8, 3, 8, 3) };

            int bx = 666;
            if (status == "Pending")
            {
                var appBtn = UITheme.MakeButton("✓ Approve", UITheme.StatusApprBg, UITheme.StatusApprFg, bx,     17, 94, 32);
                var rejBtn = UITheme.MakeButton("✗ Reject",  UITheme.StatusBlokBg, UITheme.AccentRed,    bx+98,  17, 84, 32);
                var detBtn = UITheme.MakeButton("Details",   UITheme.PrimaryLight,  UITheme.Primary,      bx+186, 17, 76, 32);
                appBtn.Font = rejBtn.Font = detBtn.Font = UITheme.FontSM;
                appBtn.Click += btnApprove_Click;
                rejBtn.Click += btnReject_Click;
                detBtn.Click += btnDetails_Click;
                row.Controls.AddRange(new Control[] { appBtn, rejBtn, detBtn });
            }
            else if (status == "Approved")
            {
                var blkBtn = UITheme.MakeButton("Block",   UITheme.StatusPendBg, UITheme.AccentAmber, bx,    17, 82, 32);
                var detBtn = UITheme.MakeButton("Details", UITheme.PrimaryLight,  UITheme.Primary,    bx+90, 17, 76, 32);
                blkBtn.Font = detBtn.Font = UITheme.FontSM;
                blkBtn.Click += btnBlock_Click;
                detBtn.Click += btnDetails_Click;
                row.Controls.AddRange(new Control[] { blkBtn, detBtn });
            }
            else
            {
                var unblkBtn = UITheme.MakeButton("Unblock", UITheme.StatusApprBg, UITheme.StatusApprFg, bx,    17, 86, 32);
                var detBtn   = UITheme.MakeButton("Details", UITheme.PrimaryLight,  UITheme.Primary,    bx+94, 17, 76, 32);
                unblkBtn.Font = detBtn.Font = UITheme.FontSM;
                unblkBtn.Click += btnUnblock_Click;
                detBtn.Click   += btnDetails_Click;
                row.Controls.AddRange(new Control[] { unblkBtn, detBtn });
            }

            row.Controls.AddRange(new Control[] { av, nL, eL, jL, stBadge });
            tableCard.Controls.Add(row);
            ry += 66; alt = !alt;
        }

        Controls.Add(tableCard);
        Resize += (s, e) =>
        {
            tableCard.Width = Width - 48;
            headerRow.Width = tableCard.Width;
            hDiv.Width      = tableCard.Width;
        };
    }
}
