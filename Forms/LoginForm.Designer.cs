using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;

    // Controls
    private Panel pnlLeft;
    private Panel pnlRight;
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
        Size            = new Size(1100, 720);
        StartPosition   = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox     = false;
        BackColor       = UITheme.BgPage;
        Font            = UITheme.FontBase;

        BuildLeftPanel();
        BuildRightPanel();
    }

    private void BuildLeftPanel()
    {
        pnlLeft = new Panel
        {
            Location  = new Point(0, 0),
            Size      = new Size(420, 720),
            BackColor = UITheme.BgSidebar,
        };
        pnlLeft.Paint += PnlLeft_Paint;

        var logoLabel = new Label
        {
            Text      = "🏗️",
            Font      = new Font("Segoe UI Emoji", 28f),
            ForeColor = Color.White,
            BackColor = UITheme.AccentTeal,
            Location  = new Point(178, 180),
            Size      = new Size(64, 64),
            TextAlign = ContentAlignment.MiddleCenter,
        };

        lblAppName = new Label
        {
            Text      = "ApartmentExpense",
            Font      = new Font("Segoe UI", 22f, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Location  = new Point(60, 260),
            Size      = new Size(300, 40),
            TextAlign = ContentAlignment.MiddleCenter,
        };

        lblAppSub = new Label
        {
            Text      = "Construction Expense Management",
            Font      = UITheme.FontSM,
            ForeColor = Color.FromArgb(148, 163, 184),
            BackColor = Color.Transparent,
            Location  = new Point(60, 302),
            Size      = new Size(300, 24),
            TextAlign = ContentAlignment.MiddleCenter,
        };

        string[] features =
        {
            "Track construction expenses in real-time",
            "Role-based access for teams",
            "Categorize and analyze spending",
            "Admin approval workflow",
        };

        int fy = 360;
        foreach (var feature in features)
        {
            var dot = new Panel
            {
                Location  = new Point(60, fy + 6),
                Size      = new Size(8, 8),
                BackColor = UITheme.AccentTeal,
            };
            dot.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(UITheme.AccentTeal), 0, 0, 8, 8);
                ((Panel)s!).BackColor = Color.Transparent;
            };

            var fl = new Label
            {
                Text      = feature,
                Font      = UITheme.FontSM,
                ForeColor = Color.FromArgb(203, 213, 225),
                BackColor = Color.Transparent,
                Location  = new Point(78, fy),
                Size      = new Size(300, 22),
            };
            pnlLeft.Controls.AddRange(new Control[] { dot, fl });
            fy += 30;
        }

        pnlLeft.Controls.AddRange(new Control[] { logoLabel, lblAppName, lblAppSub });
        Controls.Add(pnlLeft);
    }

    private void BuildRightPanel()
    {
        pnlCard = new Panel
        {
            Location  = new Point(490, 110),
            Size      = new Size(420, 480),
            BackColor = Color.White,
        };
        pnlCard.Paint += PnlCard_Paint;

        lblWelcome = UITheme.MakeLabel("Welcome back",
            UITheme.FontH2, UITheme.TextPrimary, 40, 36);

        lblSub = UITheme.MakeLabel("Sign in to your account to continue",
            UITheme.FontBase, UITheme.TextSecond, 40, 74);

        lblEmail = UITheme.MakeLabel("EMAIL ADDRESS",
            UITheme.FontLabel, UITheme.TextSecond, 40, 124);

        txtEmail = UITheme.MakeTextBox(40, 146, 340);
        txtEmail.PlaceholderText = "admin@example.com";

        lblPassword = UITheme.MakeLabel("PASSWORD",
            UITheme.FontLabel, UITheme.TextSecond, 40, 200);

        txtPassword = UITheme.MakeTextBox(40, 222, 340, isPassword: true);
        txtPassword.PlaceholderText = "••••••••";

        btnLogin = UITheme.MakeButton("Sign In",
            UITheme.Primary, Color.White, 40, 284, 340, 48);
        btnLogin.Font   = new Font("Segoe UI", 13f, FontStyle.Bold);
        btnLogin.Click += btnLogin_Click;

        lblOr = UITheme.MakeLabel("— or —",
            UITheme.FontSM, UITheme.TextMuted, 178, 350);

        lblNoAccount = UITheme.MakeLabel("Don't have an account?",
            UITheme.FontBase, UITheme.TextSecond, 78, 380);

        btnRegister = new Button
        {
            Text      = "Register here",
            Font      = UITheme.FontBase,
            ForeColor = UITheme.Primary,
            BackColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Location  = new Point(248, 377),
            AutoSize  = true,
            Cursor    = Cursors.Hand,
        };
        btnRegister.FlatAppearance.BorderSize = 0;
        btnRegister.Click += btnRegister_Click;

        pnlCard.Controls.AddRange(new Control[]
        {
            lblWelcome, lblSub,
            lblEmail, txtEmail,
            lblPassword, txtPassword,
            btnLogin, lblOr,
            lblNoAccount, btnRegister,
        });

        Controls.Add(pnlCard);
    }

    private void PnlLeft_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        using var br = new LinearGradientBrush(
            new Point(0, 0), new Point(420, 720),
            UITheme.BgSidebar, Color.FromArgb(14, 116, 144));
        g.FillRectangle(br, 0, 0, pnlLeft.Width, pnlLeft.Height);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using var cb = new SolidBrush(Color.FromArgb(15, 255, 255, 255));
        g.FillEllipse(cb, pnlLeft.Width - 150, -80, 280, 280);
        g.FillEllipse(cb, -80, pnlLeft.Height - 150, 200, 200);
    }

    private void PnlCard_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
            0, 0, pnlCard.Width - 1, pnlCard.Height - 1, 16);
    }
}
