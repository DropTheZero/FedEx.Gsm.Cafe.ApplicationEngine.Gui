// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.CountryProfile
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System.Xml.Serialization;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data
{
  public class CountryProfile
  {
    [XmlAttribute("code")]
    public string Code { get; set; }

    [XmlAttribute("domestic")]
    public string IsDomesticString
    {
      get => this.FormatBool(this.IsDomestic);
      set
      {
      }
    }

    [XmlIgnore]
    public bool IsDomestic { get; set; }

    [XmlAttribute("international")]
    public string IsInternationalString
    {
      get => this.FormatBool(this.IsInternational);
      set
      {
      }
    }

    [XmlIgnore]
    public bool IsInternational { get; set; }

    private string FormatBool(bool boolean) => !boolean ? "N" : "Y";
  }
}
