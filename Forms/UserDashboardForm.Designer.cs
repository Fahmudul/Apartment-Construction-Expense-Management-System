using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Controls;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class UserDashboardForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlSidebar;
    private Panel pnlTopBar;
    internal Panel pnlContent;
    private SidebarButton btnDashboard;
    private SidebarButton btnMyExpenses;
    private SidebarButton btnAddExpense;
    private Button btnLogout;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Text          = "ApartmentExpense — My Dashboard";
        Size          = new Size(1280, 820);
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize   = new Size(1000, 600);
        BackColor     = UITheme.BgPage;
        Font          = UITheme.FontBase;

        // Sidebar
        pnlSidebar = new Panel { Dock = DockStyle.Left, Width = 240, BackColor = UITheme.BgSidebar };

        var logoBg = new Panel { Location = new Point(0, 0), Size = new Size(240, 76), BackColor = UITheme.BgSidebar };
        logoBg.Paint += (s, e) =>
            e.Graphics.DrawLine(new Pen(Color.FromArgb(26, 51, 71), 1), 0, 75, 240, 75);
        var logoIcon = new Label { Text = "🏗", Font = new Font("Segoe UI Emoji", 18f),
            ForeColor = Color.White, BackColor = UITheme.AccentTeal,
            Location = new Point(20, 18), Size = new Size(40, 40), TextAlign = ContentAlignment.MiddleCenter };
        var logoName = new Label { Text = "ApartmentExp", Font = UITheme.FontSemi,
            ForeColor = Color.White, BackColor = Color.Transparent, Location = new Point(70, 18), AutoSize = true };
        var logoSub  = new Label { Text = "My Expenses", Font = UITheme.FontXS,
            ForeColor = Color.FromArgb(100, 116, 139), BackColor = Color.Transparent,
            Location = new Point(70, 40), AutoSize = true };
        logoBg.Controls.AddRange(new Control[] { logoIcon, logoName, logoSub });
        pnlSidebar.Controls.Add(logoBg);

        var sectionLbl = new Label { Text = "NAVIGATION", Font = UITheme.FontXS,
            ForeColor = Color.FromArgb(51, 65, 85), BackColor = Color.Transparent,
            Location = new Point(28, 90), AutoSize = true };
        pnlSidebar.Controls.Add(sectionLbl);

        btnDashboard  = new SidebarButton { Emoji = "⊞", NavLabel = "Dashboard",   Location = new Point(8, 108), IsActive = true };
        btnMyExpenses = new SidebarButton { Emoji = "📋", NavLabel = "My Expenses", Location = new Point(8, 158) };
        btnAddExpense = new SidebarButton { Emoji = "➕", NavLabel = "Add Expense", Location = new Point(8, 208) };
        btnDashboard.Click  += btnDashboard_Click;
        btnMyExpenses.Click += btnMyExpenses_Click;
        btnAddExpense.Click += btnAddExpense_Click;
        pnlSidebar.Controls.AddRange(new Control[] { btnDashboard, btnMyExpenses, btnAddExpense });

        var badgeBg = new Panel { Location = new Point(16, 280), Size = new Size(208, 44), BackColor = Color.Transparent };
        badgeBg.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.FillRoundedRect(e.Graphics, new SolidBrush(Color.FromArgb(26, 51, 71)), 0, 0, 208, 44, 10);
        };
        var badgePill = new Label { Text = "USER", Font = UITheme.FontXS, ForeColor = Color.White,
            BackColor = Color.FromArgb(100, 116, 139), Location = new Point(12, 12),
            AutoSize = true, Padding = new Padding(6, 2, 6, 2) };
        var badgeTxt  = new Label { Text = "Team Member", Font = UITheme.FontSM,
            ForeColor = Color.FromArgb(203, 213, 225), BackColor = Color.Transparent,
            Location = new Point(64, 14), AutoSize = true };
        badgeBg.Controls.AddRange(new Control[] { badgePill, badgeTxt });
        pnlSidebar.Controls.Add(badgeBg);

        var divider = new Panel { Location = new Point(0, 679), Size = new Size(240, 1),
            BackColor = Color.FromArgb(26, 51, 71) };
        pnlSidebar.Controls.Add(divider);

        btnLogout = new Button { Text = "⎋  Logout", Font = UITheme.FontNav,
            ForeColor = UITheme.AccentRed, BackColor = UITheme.BgSidebar, FlatStyle = FlatStyle.Flat,
            Location = new Point(8, 688), Size = new Size(224, 46),
            TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(12, 0, 0, 0), Cursor = Cursors.Hand };
        btnLogout.FlatAppearance.BorderSize = 0;
        btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(26, 51, 71);
        btnLogout.Click += btnLogout_Click;
        pnlSidebar.Controls.Add(btnLogout);
        Controls.Add(pnlSidebar);

        // Top bar
        pnlTopBar = new Panel { Dock = DockStyle.Top, Height = 64, BackColor = UITheme.BgCard };
        pnlTopBar.Paint += (s, e) =>
            e.Graphics.DrawLine(new Pen(UITheme.BorderLight, 1),
                0, pnlTopBar.Height - 1, pnlTopBar.Width, pnlTopBar.Height - 1);

        var pageTitle = UITheme.MakeLabel("My Dashboard", UITheme.FontH3, UITheme.TextPrimary, 24, 20);
        pnlTopBar.Controls.Add(pageTitle);

        var avatar = new Panel { Size = new Size(36, 36), BackColor = Color.Transparent };
        avatar.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(new SolidBrush(UITheme.AccentGreen), 0, 0, 36, 36);
            e.Graphics.DrawString("R", new Font("Segoe UI", 14f, FontStyle.Bold),
                new SolidBrush(Color.White), new RectangleF(0, 2, 36, 32),
                new StringFormat { Alignment = StringAlignment.Center });
        };
        var nameLbl  = UITheme.MakeLabel("Rahima Begum", UITheme.FontSemi, UITheme.TextPrimary, 0, 22);
        var rolePill = new Label { Text = "USER", Font = UITheme.FontXS, ForeColor = Color.White,
            BackColor = Color.FromArgb(100, 116, 139), AutoSize = true, Padding = new Padding(8, 3, 8, 3) };
        pnlTopBar.Resize += (s, e) =>
        {
            rolePill.Location = new Point(pnlTopBar.Width - rolePill.Width - 24, 22);
            nameLbl.Location  = new Point(pnlTopBar.Width - rolePill.Width - nameLbl.Width - 50, 22);
            avatar.Location   = new Point(pnlTopBar.Width - rolePill.Width - nameLbl.Width - 96, 14);
        };
        pnlTopBar.Controls.AddRange(new Control[] { avatar, nameLbl, rolePill });
        Controls.Add(pnlTopBar);

        // Content
        pnlContent = new Panel { Dock = DockStyle.Fill, BackColor = UITheme.BgPage, AutoScroll = true };
        Controls.Add(pnlContent);
    }
}
