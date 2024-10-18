// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields.CommandActivation
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System.Xml.Serialization;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields
{
  public enum CommandActivation
  {
    [XmlEnum(Name = "-1")] None,
    [XmlEnum(Name = "0")] FastShipAuto,
    [XmlEnum(Name = "1")] FastShipEdit,
    [XmlEnum(Name = "2")] OAATAuto,
    [XmlEnum(Name = "3")] OAATEdit,
    [XmlEnum(Name = "4")] AfterShip,
    [XmlEnum(Name = "5")] EOD,
    [XmlEnum(Name = "6")] DeleteShipment,
    [XmlEnum(Name = "7")] ImportOnly,
    [XmlEnum(Name = "8")] ExportOnDemand,
  }
}
