// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.TabPageException
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class TabPageException : Exception
  {
    private TabPage _tabPage;
    private Control _control;
    private Error _error;

    public TabPage TabPage
    {
      get => this._tabPage;
      set => this._tabPage = value;
    }

    public Control Control
    {
      get => this._control;
      set => this._control = value;
    }

    public Error Error
    {
      get => this._error;
      set => this._error = value;
    }

    public TabPageException(TabPage t, Control c, Error e)
    {
      this._tabPage = t;
      this._control = c;
      this._error = e;
    }

    public TabPageException(TabPage t, Control c, int code)
    {
      this._tabPage = t;
      this._control = c;
      this._error = new Error();
      this._error.Code = code;
    }
  }
}
