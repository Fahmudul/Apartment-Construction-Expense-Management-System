using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;
using ApartmentWinForms.Services;

namespace ApartmentWinForms.Forms;

partial class AdminUsersForm
{
    private System.ComponentModel.IContainer components = null;
    internal DataGridView dgvUsers;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        string pendingUsers = UserService.GetPendingUsersCount().ToString();
        // Loaded inside pnlContent: 1040 x 736
        BackColor = UITheme.BgPage;
        int cx = 20, cy = 20, cw = 1120;

        var title = UITheme.MakeLabel("User Management", UITheme.FontH2, UITheme.TextPrimary, cx, cy);
        var sub   = UITheme.MakeLabel("Manage team access and permissions",
            UITheme.FontBase, UITheme.TextSecond, cx, cy + 36);
        Controls.AddRange(new Control[] { title, sub });

        // Pending badge — top right
        var pendBg = new Panel { Location = new Point(920, cy + 4), Size = new Size(220, 38), BackColor = Color.Transparent };
        pendBg.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.FillRoundedRect(e.Graphics, new SolidBrush(UITheme.StatusPendBg), 0, 0, 220, 38, 10);
        };
        var pendTxt = new Label { Text = $"⚠️  {pendingUsers} pending approvals",
            Font = UITheme.FontSMBold, ForeColor = UITheme.StatusPendFg,
            BackColor = Color.Transparent, Location = new Point(12, 10), AutoSize = true };
        pendBg.Controls.Add(pendTxt);
        Controls.Add(pendBg);
        cy += 74;

        // Table card
        int tableH = 736 - cy - 20;
        var tableCard = new Panel { Location = new Point(cx, cy), Size = new Size(cw, tableH), BackColor = Color.White };
        tableCard.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            UITheme.DrawRoundedRect(e.Graphics, new Pen(UITheme.BorderLight, 1), 0, 0, cw - 1, tableH - 1, 12);
        };

        var th = UITheme.MakeLabel("All Users", UITheme.FontH3, UITheme.TextPrimary, 20, 14);
        th.BackColor = Color.Transparent;
        var td = UITheme.MakeDivider(0, 56, cw);
        tableCard.Controls.AddRange(new Control[] { th, td });

        dgvUsers = UITheme.MakeDataGrid(0, 57, cw, tableH - 57);

        // Data columns
        dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "ID",
            Name = "colID",
            Visible = false  // hidden from user
        });
        dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", FillWeight = 18, Name = "colName" });
        dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", FillWeight = 22, Name = "colEmail" });
        dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Joined", FillWeight = 13, Name = "colJoined" });
        dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Status", FillWeight = 10, Name = "colStatus" });
        dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Role", FillWeight = 10, Name = "colRole" });
                dgvUsers.Columns.Add(new DataGridViewButtonColumn
        {
            HeaderText = "",
            FillWeight = 13,
            Name = "colDetails",
            Text = "Details",
            UseColumnTextForButtonValue = true
        });

        tableCard.Controls.Add(dgvUsers);

        Controls.Add(tableCard);
    }
}
