// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields.ConditionType
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System.Xml.Serialization;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields
{
  public enum ConditionType
  {
    [XmlEnum(Name = "-1")] None,
    [XmlEnum(Name = "0")] Parent,
    [XmlEnum(Name = "1")] Commodity,
    [XmlEnum(Name = "2")] Document,
    [XmlEnum(Name = "3")] DangerousGoods,
    [XmlEnum(Name = "4")] ShipmentContents,
  }
}
