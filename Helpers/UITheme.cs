using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ApartmentWinForms.Helpers;

public static class UITheme
{
    // ── Colors ────────────────────────────────────────────────
    public static readonly Color BgPage       = Color.FromArgb(240, 244, 248);
    public static readonly Color BgSidebar    = Color.FromArgb(15,  30,  46);
    public static readonly Color BgSidebarHov = Color.FromArgb(26,  51,  71);
    public static readonly Color BgSidebarAct = Color.FromArgb(14, 116, 144);
    public static readonly Color BgCard       = Color.White;
    public static readonly Color BgInput      = Color.FromArgb(248, 250, 252);
    public static readonly Color BgTableHdr   = Color.FromArgb(248, 250, 252);
    public static readonly Color BgTableAlt   = Color.FromArgb(250, 251, 252);

    public static readonly Color Primary      = Color.FromArgb(14, 116, 144);
    public static readonly Color PrimaryDark  = Color.FromArgb(10,  92, 115);
    public static readonly Color PrimaryLight = Color.FromArgb(224, 242, 254);
    public static readonly Color AccentTeal   = Color.FromArgb(20, 184, 166);
    public static readonly Color AccentGreen  = Color.FromArgb(16, 185, 129);
    public static readonly Color AccentRed    = Color.FromArgb(239,  68,  68);
    public static readonly Color AccentAmber  = Color.FromArgb(245, 158,  11);

    public static readonly Color TextPrimary  = Color.FromArgb(15,  23,  42);
    public static readonly Color TextSecond   = Color.FromArgb(71,  85, 105);
    public static readonly Color TextMuted    = Color.FromArgb(148, 163, 184);
    public static readonly Color TextOnDark   = Color.FromArgb(226, 232, 240);
    public static readonly Color TextSidebarI = Color.FromArgb(148, 163, 184);

    public static readonly Color BorderLight  = Color.FromArgb(226, 232, 240);
    public static readonly Color BorderMedium = Color.FromArgb(203, 213, 225);

    public static readonly Color StatusPendBg = Color.FromArgb(254, 243, 199);
    public static readonly Color StatusPendFg = Color.FromArgb(146,  64,  14);
    public static readonly Color StatusApprBg = Color.FromArgb(209, 250, 229);
    public static readonly Color StatusApprFg = Color.FromArgb(  6,  95,  70);
    public static readonly Color StatusBlokBg = Color.FromArgb(254, 226, 226);
    public static readonly Color StatusBlokFg = Color.FromArgb(153,  27,  27);

    // ── Fonts ─────────────────────────────────────────────────
    public static readonly Font FontH1     = new("Segoe UI", 22f, FontStyle.Bold);
    public static readonly Font FontH2     = new("Segoe UI", 18f, FontStyle.Bold);
    public static readonly Font FontH3     = new("Segoe UI", 14f, FontStyle.Bold);
    public static readonly Font FontBase   = new("Segoe UI", 12f, FontStyle.Regular);
    public static readonly Font FontSemi   = new("Segoe UI", 12f, FontStyle.Bold);
    public static readonly Font FontSM     = new("Segoe UI", 10f, FontStyle.Regular);
    public static readonly Font FontSMBold = new("Segoe UI", 10f, FontStyle.Bold);
    public static readonly Font FontXS     = new("Segoe UI",  9f, FontStyle.Regular);
    public static readonly Font FontLabel  = new("Segoe UI", 10f, FontStyle.Bold);
    public static readonly Font FontNav    = new("Segoe UI", 12f, FontStyle.Regular);

    // ── Drawing Helpers ───────────────────────────────────────
    public static GraphicsPath RoundedRect(int x, int y, int w, int h, int r)
    {
        var path = new GraphicsPath();
        path.AddArc(x,             y,             r * 2, r * 2, 180, 90);
        path.AddArc(x + w - r * 2, y,             r * 2, r * 2, 270, 90);
        path.AddArc(x + w - r * 2, y + h - r * 2, r * 2, r * 2,   0, 90);
        path.AddArc(x,             y + h - r * 2, r * 2, r * 2,  90, 90);
        path.CloseFigure();
        return path;
    }

    public static void DrawRoundedRect(Graphics g, Pen pen, int x, int y, int w, int h, int r)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using var path = RoundedRect(x, y, w, h, r);
        g.DrawPath(pen, path);
    }

    public static void FillRoundedRect(Graphics g, Brush brush, int x, int y, int w, int h, int r)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using var path = RoundedRect(x, y, w, h, r);
        g.FillPath(brush, path);
    }

    // ── Factory Methods ───────────────────────────────────────
    public static Button MakeButton(string text, Color bg, Color fg, int x, int y, int w, int h = 42)
    {
        var btn = new Button
        {
            Text      = text,
            Location  = new Point(x, y),
            Size      = new Size(w, h),
            Font      = FontSemi,
            ForeColor = fg,
            BackColor = bg,
            FlatStyle = FlatStyle.Flat,
            Cursor    = Cursors.Hand,
        };
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = ControlPaint.Dark(bg, 0.08f);
        btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(bg, 0.18f);
        return btn;
    }

    public static Button MakeOutlineButton(string text, int x, int y, int w, int h = 42)
    {
        var btn = new Button
        {
            Text      = text,
            Location  = new Point(x, y),
            Size      = new Size(w, h),
            Font      = FontSemi,
            ForeColor = Primary,
            BackColor = BgCard,
            FlatStyle = FlatStyle.Flat,
            Cursor    = Cursors.Hand,
        };
        btn.FlatAppearance.BorderSize  = 1;
        btn.FlatAppearance.BorderColor = Primary;
        btn.FlatAppearance.MouseOverBackColor = PrimaryLight;
        return btn;
    }

    public static TextBox MakeTextBox(int x, int y, int w, bool isPassword = false)
    {
        var tb = new TextBox
        {
            Location              = new Point(x, y),
            Size                  = new Size(w, 38),
            Font                  = FontBase,
            BackColor             = BgInput,
            ForeColor             = TextPrimary,
            BorderStyle           = BorderStyle.FixedSingle,
            UseSystemPasswordChar = isPassword,
        };
        return tb;
    }

    public static ComboBox MakeComboBox(int x, int y, int w)
    {
        var cb = new ComboBox
        {
            Location      = new Point(x, y),
            Size          = new Size(w, 38),
            Font          = FontBase,
            BackColor     = BgInput,
            ForeColor     = TextPrimary,
            DropDownStyle = ComboBoxStyle.DropDownList,
            FlatStyle     = FlatStyle.Flat,
        };
        return cb;
    }

    public static DateTimePicker MakeDatePicker(int x, int y, int w)
    {
        return new DateTimePicker
        {
            Location = new Point(x, y),
            Size     = new Size(w, 38),
            Font     = FontBase,
            Format   = DateTimePickerFormat.Short,
            CalendarTitleBackColor = Primary,
        };
    }

    public static Label MakeLabel(string text, Font font, Color color, int x, int y, int w = 0, int h = 0)
    {
        var lbl = new Label
        {
            Text      = text,
            Font      = font,
            ForeColor = color,
            BackColor = Color.Transparent,
            Location  = new Point(x, y),
            AutoSize  = (w == 0),
        };
        if (w > 0) lbl.Size = new Size(w, h == 0 ? 24 : h);
        return lbl;
    }

    public static DataGridView MakeDataGrid(int x, int y, int w, int h)
    {
        var dgv = new DataGridView
        {
            Location              = new Point(x, y),
            Size                  = new Size(w, h),
            BackgroundColor       = BgCard,
            BorderStyle           = BorderStyle.None,
            CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal,
            GridColor             = BorderLight,
            RowHeadersVisible     = false,
            AllowUserToAddRows    = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly              = true,
            SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect           = false,
            AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
            Font                  = FontBase,
            ColumnHeadersHeight   = 44,
            EnableHeadersVisualStyles = false,
        };
        dgv.RowTemplate.Height = 50;

        dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor  = BgTableHdr,
            ForeColor  = TextSecond,
            Font       = FontSMBold,
            Padding    = new Padding(8, 0, 0, 0),
            Alignment  = DataGridViewContentAlignment.MiddleLeft,
        };
        dgv.DefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor         = BgCard,
            ForeColor         = TextPrimary,
            SelectionBackColor = PrimaryLight,
            SelectionForeColor = TextPrimary,
            Padding           = new Padding(8, 0, 0, 0),
            Alignment         = DataGridViewContentAlignment.MiddleLeft,
        };
        dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor         = BgTableAlt,
            ForeColor         = TextPrimary,
            SelectionBackColor = PrimaryLight,
            SelectionForeColor = TextPrimary,
        };
        return dgv;
    }

    public static Panel MakeDivider(int x, int y, int w)
    {
        return new Panel
        {
            Location  = new Point(x, y),
            Size      = new Size(w, 1),
            BackColor = BorderLight,
        };
    }
}
