using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ApartmentWinForms.Helpers;

namespace ApartmentWinForms.Controls;

public class SidebarButton : Control
{
    private bool _isActive;
    private bool _isHovered;
    private string _emoji = "";
    private string _navLabel = "";

    public string Emoji
    {
        get => _emoji;
        set { _emoji = value; Invalidate(); }
    }

    public string NavLabel
    {
        get => _navLabel;
        set { _navLabel = value; Invalidate(); }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsActive
    {
        get => _isActive;
        set { _isActive = value; Invalidate(); }
    }

    public SidebarButton()
    {
        SetStyle(ControlStyles.UserPaint |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer, true);
        Cursor = Cursors.Hand;
        Size   = new Size(224, 46);
    }

    protected override void OnMouseEnter(EventArgs e) { _isHovered = true;  Invalidate(); base.OnMouseEnter(e); }
    protected override void OnMouseLeave(EventArgs e) { _isHovered = false; Invalidate(); base.OnMouseLeave(e); }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(UITheme.BgSidebar);

        var bg = _isActive  ? UITheme.BgSidebarAct :
                 _isHovered ? UITheme.BgSidebarHov :
                              UITheme.BgSidebar;

        using var brush = new SolidBrush(bg);
        UITheme.FillRoundedRect(g, brush, 0, 0, Width, Height, 8);

        var fg = _isActive ? Color.White : UITheme.TextSidebarI;

        using var emojiFont = new Font("Segoe UI Emoji", 14f);
        using var textFont  = new Font("Segoe UI", 12f,
            _isActive ? FontStyle.Bold : FontStyle.Regular);
        using var fgBrush   = new SolidBrush(fg);

        g.DrawString(_emoji,    emojiFont, fgBrush, new PointF(14, 12));
        g.DrawString(_navLabel, textFont,  fgBrush, new PointF(46, 14));
    }
}
