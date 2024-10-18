// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields.Return
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System;
using System.Xml.Serialization;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields
{
  [XmlType("Return")]
  [Serializable]
  public class Return
  {
    [XmlElement("Category")]
    public string CategoryName { get; set; }

    [XmlElement("Group")]
    public string GroupName { get; set; }

    [XmlElement("ConversionGroup")]
    public string ConversionGroup { get; set; }
  }
}
