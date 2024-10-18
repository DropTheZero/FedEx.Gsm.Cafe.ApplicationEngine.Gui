// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.CafeColorTable
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using System.Drawing;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class CafeColorTable
  {
    public static readonly Color PurpleDesaturated = Color.FromArgb(171, 154, 190);
    public static readonly Color BackgroundGray = Color.FromArgb(229, 229, 229);
    public static readonly Color BorderGray = Color.FromArgb(9, 9, 9);
    public static readonly Color BackgroundPurpleGray = Color.FromArgb(183, 166, 202);

    public static CafeColorTable FlatColorTable
    {
      get
      {
        return new CafeColorTable()
        {
          ToolstripGradientBegin = SystemColors.ButtonFace,
          ToolstripGradientMiddle = Color.LightGray,
          ToolstripGradientEnd = Color.LightGray,
          ToolstripDropDownBackground = Color.LightGray,
          ImageMarginGradientBegin = Color.WhiteSmoke,
          ImageMarginGradientMiddle = Color.WhiteSmoke,
          ImageMarginGradientEnd = Color.WhiteSmoke,
          ButtonTextColor = CafeColorTable.BorderGray,
          ButtonCheckedGradientBegin = FedExColors.Purple,
          ButtonCheckedGradientMiddle = FedExColors.Purple,
          ButtonCheckedGradientEnd = FedExColors.Purple,
          ButtonCheckedBorder = FedExColors.Purple,
          ButtonSelectedGradientBegin = CafeColorTable.BackgroundPurpleGray,
          ButtonSelectedGradientMiddle = CafeColorTable.BackgroundPurpleGray,
          ButtonSelectedGradientEnd = CafeColorTable.BackgroundPurpleGray,
          ButtonSelectedBorder = CafeColorTable.BorderGray,
          ButtonPressedGradientBegin = FedExColors.Purple,
          ButtonPressedGradientMiddle = FedExColors.Purple,
          ButtonPressedGradientEnd = FedExColors.Purple,
          ButtonPressedBorder = CafeColorTable.BorderGray,
          MenuItemBorder = Color.Indigo,
          MenuItemSelected = CafeColorTable.PurpleDesaturated
        };
      }
    }

    public Color ToolstripGradientBegin { get; set; }

    public Color ToolstripGradientMiddle { get; set; }

    public Color ToolstripGradientEnd { get; set; }

    public Color ToolstripDropDownBackground { get; set; }

    public Color ImageMarginGradientBegin { get; set; }

    public Color ImageMarginGradientMiddle { get; set; }

    public Color ImageMarginGradientEnd { get; set; }

    public Color ButtonGradientBegin { get; set; }

    public Color ButtonGradientMiddle { get; set; }

    public Color ButtonGradientEnd { get; set; }

    public Color ButtonBorder { get; set; }

    public Color ButtonTextColor { get; set; }

    public Color ButtonSelectedGradientBegin { get; set; }

    public Color ButtonSelectedGradientMiddle { get; set; }

    public Color ButtonSelectedGradientEnd { get; set; }

    public Color ButtonSelectedBorder { get; set; }

    public Color ButtonSelectedTextColor { get; set; }

    public Color ButtonPressedGradientBegin { get; set; }

    public Color ButtonPressedGradientMiddle { get; set; }

    public Color ButtonPressedGradientEnd { get; set; }

    public Color ButtonPressedBorder { get; set; }

    public Color ButtonCheckedGradientBegin { get; set; }

    public Color ButtonCheckedGradientMiddle { get; set; }

    public Color ButtonCheckedGradientEnd { get; set; }

    public Color ButtonCheckedBorder { get; set; }

    public Color ButtonCheckedTextColor { get; set; }

    public Color MenuItemSelected { get; set; }

    public Color MenuItemBorder { get; set; }
  }
}
