// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.CafeToolstripRenderer
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class CafeToolstripRenderer : ToolStripProfessionalRenderer
  {
    private CafeColorTable colors;

    public CafeToolstripRenderer(CafeColorTable colors)
      : base((ProfessionalColorTable) new CafeToolstripRenderer.CafeColorTableAdapter(colors))
    {
      this.colors = colors;
    }

    protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
    {
      base.OnRenderButtonBackground(e);
      Rectangle rectangle;
      ref Rectangle local = ref rectangle;
      Size size = e.Item.Size;
      int width = size.Width - 1;
      size = e.Item.Size;
      int height = size.Height - 1;
      local = new Rectangle(0, 0, width, height);
      if (e.Item is ToolStripButton toolStripButton && (toolStripButton.Selected || toolStripButton.Checked))
        return;
      using (Brush backColorBrush = this.GetBackColorBrush(rectangle))
        e.Graphics.FillRectangle(backColorBrush, rectangle);
      using (Pen pen = new Pen(this.colors.ButtonBorder))
        e.Graphics.DrawRectangle(pen, rectangle);
    }

    protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
    {
      base.OnRenderDropDownButtonBackground(e);
      Rectangle rectangle;
      ref Rectangle local = ref rectangle;
      Size size = e.Item.Size;
      int width = size.Width - 1;
      size = e.Item.Size;
      int height = size.Height - 1;
      local = new Rectangle(0, 0, width, height);
      if (e.Item is ToolStripDropDownButton stripDropDownButton && !stripDropDownButton.Pressed && stripDropDownButton.Selected)
        return;
      using (Brush backColorBrush = this.GetBackColorBrush(rectangle))
        e.Graphics.FillRectangle(backColorBrush, rectangle);
      using (Pen pen = new Pen(this.colors.ButtonBorder))
        e.Graphics.DrawRectangle(pen, rectangle);
    }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
      if (e.Item is ToolStripButton)
        e.TextColor = !((ToolStripButton) e.Item).Checked ? this.GetColorForBackground(this.colors.ButtonTextColor, this.colors.ButtonGradientEnd) : this.GetColorForBackground(this.colors.ButtonCheckedTextColor, this.colors.ButtonCheckedGradientEnd);
      else if (e.Item is ToolStripDropDownButton)
        e.TextColor = this.GetColorForBackground(this.colors.ButtonTextColor, this.colors.ButtonGradientEnd);
      else if (e.Item is ToolStripMenuItem && e.Item.Selected && e.Item.OwnerItem != null)
        e.TextColor = (double) this.ColorTable.MenuItemSelected.GetBrightness() > 0.5 ? Color.Black : Color.White;
      base.OnRenderItemText(e);
    }

    private Color GetColorForBackground(Color text, Color background)
    {
      if (!text.IsEmpty)
        return text;
      return (double) background.GetBrightness() <= 0.5 ? Color.White : Color.Black;
    }

    private Brush GetBackColorBrush(Rectangle bounds)
    {
      return (Brush) new LinearGradientBrush(bounds, this.colors.ButtonGradientBegin, this.colors.ButtonGradientEnd, LinearGradientMode.Vertical);
    }

    private class CafeColorTableAdapter : ProfessionalColorTable
    {
      private CafeColorTable table;

      public CafeColorTableAdapter(CafeColorTable table) => this.table = table;

      public override Color ButtonCheckedGradientBegin => this.table.ButtonCheckedGradientBegin;

      public override Color ButtonCheckedGradientMiddle => this.table.ButtonCheckedGradientMiddle;

      public override Color ButtonCheckedGradientEnd => this.table.ButtonCheckedGradientEnd;

      public override Color CheckBackground => this.table.ButtonCheckedGradientMiddle;

      public override Color CheckPressedBackground => this.table.ButtonCheckedGradientMiddle;

      public override Color CheckSelectedBackground => this.table.ButtonCheckedGradientMiddle;

      public override Color ButtonCheckedHighlight => this.table.ButtonCheckedGradientMiddle;

      public override Color ButtonCheckedHighlightBorder => this.table.ButtonCheckedBorder;

      public override Color ButtonPressedGradientBegin => this.table.ButtonPressedGradientBegin;

      public override Color ButtonPressedGradientMiddle => this.table.ButtonPressedGradientMiddle;

      public override Color ButtonPressedGradientEnd => this.table.ButtonPressedGradientEnd;

      public override Color ButtonPressedBorder => this.table.ButtonPressedBorder;

      public override Color ButtonPressedHighlight => this.table.ButtonPressedGradientMiddle;

      public override Color ButtonPressedHighlightBorder => this.table.ButtonPressedBorder;

      public override Color ButtonSelectedGradientBegin => this.table.ButtonSelectedGradientBegin;

      public override Color ButtonSelectedGradientMiddle => this.table.ButtonSelectedGradientMiddle;

      public override Color ButtonSelectedGradientEnd => this.table.ButtonSelectedGradientEnd;

      public override Color ButtonSelectedBorder => this.table.ButtonSelectedBorder;

      public override Color ButtonSelectedHighlight => this.table.MenuItemSelected;

      public override Color ButtonSelectedHighlightBorder => this.table.ButtonSelectedBorder;

      public override Color ImageMarginGradientBegin => this.table.ImageMarginGradientBegin;

      public override Color ImageMarginGradientMiddle => this.table.ImageMarginGradientMiddle;

      public override Color ImageMarginGradientEnd => this.table.ImageMarginGradientEnd;

      public override Color ImageMarginRevealedGradientBegin => this.table.ImageMarginGradientBegin;

      public override Color ImageMarginRevealedGradientMiddle
      {
        get => this.table.ImageMarginGradientMiddle;
      }

      public override Color ImageMarginRevealedGradientEnd => this.table.ImageMarginGradientEnd;

      public override Color ToolStripDropDownBackground => this.table.ToolstripDropDownBackground;

      public override Color ToolStripGradientBegin => this.table.ToolstripGradientBegin;

      public override Color ToolStripGradientMiddle => this.table.ToolstripGradientMiddle;

      public override Color ToolStripGradientEnd => this.table.ToolstripGradientEnd;

      public override Color MenuItemBorder
      {
        get
        {
          return !(this.table.MenuItemBorder != Color.Empty) ? base.MenuItemBorder : this.table.MenuItemBorder;
        }
      }

      public override Color MenuItemSelected
      {
        get
        {
          return !(this.table.MenuItemSelected != Color.Empty) ? base.MenuItemSelected : this.table.MenuItemSelected;
        }
      }

      public override Color MenuItemSelectedGradientBegin
      {
        get
        {
          return !(this.table.MenuItemSelected != Color.Empty) ? base.MenuItemSelectedGradientBegin : this.table.MenuItemSelected;
        }
      }

      public override Color MenuItemSelectedGradientEnd
      {
        get
        {
          return !(this.table.MenuItemSelected != Color.Empty) ? base.MenuItemSelectedGradientEnd : this.table.MenuItemSelected;
        }
      }
    }
  }
}
