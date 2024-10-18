// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields.Group
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields
{
  [XmlType("Group")]
  [Serializable]
  public class Group
  {
    [XmlAttribute("ShipmentName")]
    public string ShipmentName { get; set; }

    [XmlAttribute("ReturnName")]
    public string ReturnName { get; set; }

    [XmlAttribute("IntegrationType")]
    [DefaultValue(IntegrationType.None)]
    public IntegrationType IntegrationType { get; set; }

    [XmlAttribute("ScreenType")]
    [DefaultValue(ScreenType.None)]
    public ScreenType ScreenType { get; set; }

    [XmlElement("Field")]
    public List<Field> Fields { get; set; }
  }
}
