// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields.TableType
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System.Xml.Serialization;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields
{
  public enum TableType
  {
    [XmlEnum(Name = "-1")] None,
    [XmlEnum(Name = "0")] TableTypeParent,
    [XmlEnum(Name = "1")] TableTypeCommodity,
    [XmlEnum(Name = "2")] TypeDocument,
    [XmlEnum(Name = "3")] TableTypeDangerousGoods,
    [XmlEnum(Name = "4")] TableTypeShipmentContents,
  }
}
