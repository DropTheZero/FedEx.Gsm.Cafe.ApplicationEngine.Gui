// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields.Field
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields
{
  [XmlType("Field")]
  [Serializable]
  public class Field
  {
    [XmlAttribute("ShipmentName")]
    public string ShipmentName { get; set; }

    [XmlAttribute("ReturnName")]
    public string ReturnName { get; set; }

    [XmlAttribute("Key")]
    public string Key { get; set; }

    [XmlAttribute("Required")]
    public bool Required { get; set; }

    [XmlAttribute("IntegrationType")]
    [DefaultValue(IntegrationType.None)]
    public IntegrationType IntegrationType { get; set; }

    [XmlAttribute("ScreenType")]
    [DefaultValue(ScreenType.None)]
    public ScreenType ScreenType { get; set; }

    [XmlElement("ID")]
    public string ID { get; set; }

    [XmlElement("Type")]
    public string Type { get; set; }

    [XmlElement("Size")]
    public string Size { get; set; }

    [XmlElement("ShipmentLevel")]
    public string ShipmentLevel { get; set; }

    [XmlElement("PackageLevel")]
    public string PackageLevel { get; set; }

    [XmlElement("Shipment")]
    public Shipment Shipment { get; set; }

    [XmlElement("Return")]
    public Return Return { get; set; }

    [XmlElement("MPSCollection")]
    [DefaultValue("False")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string MpsCollectionAsString
    {
      get => !this.MpsCollection ? bool.FalseString : bool.TrueString;
      set
      {
        bool result;
        if (!bool.TryParse(value, out result))
          return;
        this.MpsCollection = result;
      }
    }

    [XmlIgnore]
    public bool MpsCollection { get; set; }

    [XmlIgnore]
    public bool AutoMatched { get; set; }
  }
}
