using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlLeft;
    private Panel pnlCard;
    private Label lblAppName;
    private Label lblAppSub;
    private Label lblWelcome;
    private Label lblSub;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblPassword;
    private TextBox txtPassword;
    private Button btnLogin;
    private Label lblOr;
    private Label lblNoAccount;
    private Button btnRegister;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Text            = "ApartmentExpense — Sign In";
        ClientSize      = new Size(1280, 800);
        StartPosition   = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox     = false;
        MinimizeBox     = true;
        BackColor       = UITheme.BgPage;
        Font            = UITheme.FontBase;

        // ── Left brand panel (420 wide, full height) ──────────
        pnlLeft = new Panel
        {
            Location  = new Point(0, 0),
            Size      = new Size(460, 800),
            BackColor = UITheme.BgSidebar,
        };
        pnlLeft.Paint += (s, e) =>
        {
            var g = e.Graphics;
            using var br = new LinearGradientBrush(
                new Point(0, 0), new Point(460, 800),
                UITheme.BgSidebar, Color.FromArgb(14, 116, 144));
            g.FillRectangle(br, 0, 0, 460, 800);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using var cb = new SolidBrush(Color.FromArgb(15, 255, 255, 255));
            g.FillEllipse(cb, 300, -80, 280, 280);
            g.FillEllipse(cb, -80, 660, 200, 200);
        };

        var logoLabel = new Label
        {
            Text = "🏗️", Font = new Font("Segoe UI Emoji", 28f),
            ForeColor = Color.White, BackColor = UITheme.AccentTeal,
            Location = new Point(198, 200), Size = new Size(64, 64),
            TextAlign = ContentAlignment.MiddleCenter,
        };

        lblAppName = new Label
        {
            Text = "ApartmentExpense",
            Font = new Font("Segoe UI", 22f, FontStyle.Bold),
            ForeColor = Color.White, BackColor = Color.Transparent,
            Location = new Point(60, 278), Size = new Size(340, 40),
            TextAlign = ContentAlignment.MiddleCenter,
        };
        lblAppSub = new Label
        {
            Text = "Construction Expense Management",
            Font = UITheme.FontSM, ForeColor = Color.FromArgb(148, 163, 184),
            BackColor = Color.Transparent,
            Location = new Point(60, 320), Size = new Size(340, 24),
            TextAlign = ContentAlignment.MiddleCenter,
        };

        string[] features =
        {
            "Track construction expenses in real-time",
            "Role-based access for teams",
            "Categorize and analyze spending",
            "Admin approval workflow",
        };
        int fy = 380;
        foreach (var feature in features)
        {
            var dot = new Panel { Location = new Point(60, fy + 6), Size = new Size(8, 8) };
            dot.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(UITheme.AccentTeal), 0, 0, 8, 8);
                ((Panel)s!).BackColor = Color.Transparent;
            };
            var fl = new Label
            {
                Text = feature, Font = UITheme.FontSM,
                ForeColor = Color.FromArgb(203, 213, 225),
                BackColor = Color.Transparent,
                Location = new Point(78, fy), Size = new Size(320, 22),
            };
            pnlLeft.Controls.AddRange(new Control[] { dot, fl });
            fy += 30;
        }
        pnlLeft.Controls.AddRange(new Control[] { logoLabel, lblAppName, lblAppSub });
        Controls.Add(pnlLeft);

        // ── Right login card (centered in remaining 820px) ─────
        // Remaining width = 1280 - 460 = 820, card width = 420
        // Card x = 460 + (820 - 420) / 2 = 460 + 200 = 660
        // Card height = 460, y = (800 - 460) / 2 = 170
        pnlCard = new Panel
        {
            Location  = new Point(640, 160),
            Size      = new Size(440, 480),
            BackColor = Color.White,
        };
        pnlCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, pnlCard.Width - 1, pnlCard.Height - 1, 16);
        };

        lblWelcome  = UITheme.MakeLabel("Welcome back", UITheme.FontH2, UITheme.TextPrimary, 40, 36);
        lblSub      = UITheme.MakeLabel("Sign in to your account to continue", UITheme.FontBase, UITheme.TextSecond, 40, 74);
        lblEmail    = UITheme.MakeLabel("EMAIL ADDRESS", UITheme.FontLabel, UITheme.TextSecond, 40, 124);
        txtEmail    = UITheme.MakeTextBox(40, 146, 360); txtEmail.PlaceholderText = "admin@example.com";
        lblPassword = UITheme.MakeLabel("PASSWORD", UITheme.FontLabel, UITheme.TextSecond, 40, 202);
        txtPassword = UITheme.MakeTextBox(40, 224, 360, isPassword: true); txtPassword.PlaceholderText = "••••••••";

        btnLogin = UITheme.MakeButton("Sign In", UITheme.Primary, Color.White, 40, 290, 360, 48);
        btnLogin.Font   = new Font("Segoe UI", 13f, FontStyle.Bold);
        btnLogin.Click += btnLogin_Click;

        lblOr       = UITheme.MakeLabel("— or —", UITheme.FontSM, UITheme.TextMuted, 186, 356);
        lblNoAccount = UITheme.MakeLabel("Don't have an account?", UITheme.FontBase, UITheme.TextSecond, 72, 388);

        btnRegister = new Button
        {
            Text = "Register here", Font = UITheme.FontBase, ForeColor = UITheme.Primary,
            BackColor = Color.White, FlatStyle = FlatStyle.Flat,
            Location = new Point(280, 385), AutoSize = true, Cursor = Cursors.Hand,
        };
        btnRegister.FlatAppearance.BorderSize = 0;
        btnRegister.Click += btnRegister_Click;

        pnlCard.Controls.AddRange(new Control[]
        { lblWelcome, lblSub, lblEmail, txtEmail, lblPassword, txtPassword,
          btnLogin, lblOr, lblNoAccount, btnRegister });
        Controls.Add(pnlCard);
    }
}
