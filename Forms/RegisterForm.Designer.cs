using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Forms;

partial class RegisterForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlLeft;
    private Panel pnlCard;
    private TextBox txtName;
    private TextBox txtEmail;
    private TextBox txtPassword;
    private TextBox txtConfirm;
    private Button btnCreate;
    private Button btnSignIn;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Text            = "ApartmentExpense — Register";
        ClientSize      = new Size(1280, 800);
        StartPosition   = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox     = false;
        BackColor       = UITheme.BgPage;

        // Left panel
        pnlLeft = new Panel { Location = new Point(0, 0), Size = new Size(460, 800), BackColor = UITheme.BgSidebar };
        pnlLeft.Paint += (s, e) =>
        {
            using var br = new LinearGradientBrush(new Point(0, 0), new Point(460, 800),
                UITheme.BgSidebar, Color.FromArgb(14, 116, 144));
            e.Graphics.FillRectangle(br, 0, 0, 460, 800);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var cb = new SolidBrush(Color.FromArgb(15, 255, 255, 255));
            e.Graphics.FillEllipse(cb, 300, -80, 280, 280);
        };

        var logo = new Label { Text = "🏗️", Font = new Font("Segoe UI Emoji", 28f),
            ForeColor = Color.White, BackColor = UITheme.AccentTeal,
            Location = new Point(198, 160), Size = new Size(64, 64), TextAlign = ContentAlignment.MiddleCenter };

        var appName = new Label { Text = "ApartmentExpense",
            Font = new Font("Segoe UI", 22f, FontStyle.Bold), ForeColor = Color.White,
            BackColor = Color.Transparent, Location = new Point(60, 238),
            Size = new Size(340, 40), TextAlign = ContentAlignment.MiddleCenter };

        var appSub = new Label { Text = "Join your team to manage\nconstruction expenses efficiently.",
            Font = UITheme.FontSM, ForeColor = Color.FromArgb(148, 163, 184),
            BackColor = Color.Transparent, Location = new Point(60, 282),
            Size = new Size(340, 46), TextAlign = ContentAlignment.MiddleCenter };

        var infoBox = new Panel { Location = new Point(40, 370), Size = new Size(380, 80), BackColor = Color.Transparent };
        infoBox.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.FillRoundedRect(e.Graphics, new SolidBrush(Color.FromArgb(26, 51, 71)), 0, 0, 380, 80, 12);
        };
        var infoIcon  = new Label { Text = "⚠️", Font = new Font("Segoe UI Emoji", 14f),
            BackColor = Color.Transparent, ForeColor = UITheme.AccentAmber, Location = new Point(14, 16), AutoSize = true };
        var infoTitle = new Label { Text = "Approval Required", Font = UITheme.FontSMBold,
            ForeColor = Color.White, BackColor = Color.Transparent, Location = new Point(44, 16), AutoSize = true };
        var infoText  = new Label { Text = "New accounts need admin approval.",
            Font = UITheme.FontSM, ForeColor = Color.FromArgb(148, 163, 184),
            BackColor = Color.Transparent, Location = new Point(44, 38), Size = new Size(320, 22) };
        infoBox.Controls.AddRange(new Control[] { infoIcon, infoTitle, infoText });
        pnlLeft.Controls.AddRange(new Control[] { logo, appName, appSub, infoBox });
        Controls.Add(pnlLeft);

        // Right card — x=640, y=80, size=440x620
        pnlCard = new Panel { Location = new Point(640, 80), Size = new Size(440, 640), BackColor = Color.White };
        pnlCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1),
                0, 0, pnlCard.Width - 1, pnlCard.Height - 1, 16);
        };

        var title   = UITheme.MakeLabel("Create Account", UITheme.FontH2, UITheme.TextPrimary, 40, 30);
        var sub     = UITheme.MakeLabel("Fill in your details to request access", UITheme.FontBase, UITheme.TextSecond, 40, 68);
        var lblName = UITheme.MakeLabel("FULL NAME",        UITheme.FontLabel, UITheme.TextSecond, 40, 108);
        txtName     = UITheme.MakeTextBox(40, 128, 360); txtName.PlaceholderText = "John Smith";
        var lblEmail = UITheme.MakeLabel("EMAIL ADDRESS",   UITheme.FontLabel, UITheme.TextSecond, 40, 178);
        txtEmail     = UITheme.MakeTextBox(40, 198, 360); txtEmail.PlaceholderText = "john@company.com";
        var lblPass  = UITheme.MakeLabel("PASSWORD",        UITheme.FontLabel, UITheme.TextSecond, 40, 248);
        txtPassword  = UITheme.MakeTextBox(40, 268, 360, isPassword: true); txtPassword.PlaceholderText = "••••••••";
        var lblConf  = UITheme.MakeLabel("CONFIRM PASSWORD",UITheme.FontLabel, UITheme.TextSecond, 40, 318);
        txtConfirm   = UITheme.MakeTextBox(40, 338, 360, isPassword: true); txtConfirm.PlaceholderText = "••••••••";

        var warnBox = new Panel { Location = new Point(40, 390), Size = new Size(360, 44), BackColor = UITheme.StatusPendBg };
        warnBox.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.FillRoundedRect(e.Graphics, new SolidBrush(UITheme.StatusPendBg), 0, 0, 360, 44, 8);
            e.Graphics.DrawLine(new Pen(UITheme.AccentAmber, 3), 0, 4, 0, 40);
        };
        var warnTxt = new Label { Text = "Account will require admin approval before login.",
            Font = UITheme.FontSM, ForeColor = UITheme.StatusPendFg,
            BackColor = Color.Transparent, Location = new Point(12, 13), Size = new Size(336, 20) };
        warnBox.Controls.Add(warnTxt);

        btnCreate = UITheme.MakeButton("Create Account", UITheme.AccentGreen, Color.White, 40, 448, 360, 48);
        btnCreate.Font   = new Font("Segoe UI", 13f, FontStyle.Bold);
        btnCreate.Click += btnCreate_Click;

        var lblHave = UITheme.MakeLabel("Already have an account?", UITheme.FontBase, UITheme.TextSecond, 72, 512);
        btnSignIn = new Button { Text = "Sign in", Font = UITheme.FontBase, ForeColor = UITheme.Primary,
            BackColor = Color.White, FlatStyle = FlatStyle.Flat,
            Location = new Point(310, 509), AutoSize = true, Cursor = Cursors.Hand };
        btnSignIn.FlatAppearance.BorderSize = 0;
        btnSignIn.Click += btnSignIn_Click;

        pnlCard.Controls.AddRange(new Control[]
        { title, sub, lblName, txtName, lblEmail, txtEmail,
          lblPass, txtPassword, lblConf, txtConfirm,
          warnBox, btnCreate, lblHave, btnSignIn });
        Controls.Add(pnlCard);
    }
}
