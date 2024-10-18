// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields.AvailableFields
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields
{
  [XmlType("AvailableFields")]
  [Serializable]
  public class AvailableFields
  {
    [XmlElement("Category")]
    public List<Category> Catagories { get; set; }

    public AvailableFields() => this.Catagories = new List<Category>();
  }
}
