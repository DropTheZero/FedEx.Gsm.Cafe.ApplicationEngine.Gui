// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.ErrorException
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class ErrorException : Exception
  {
    private Error _cafeError;
    private Control _offendingControl;

    public ErrorException(int code)
    {
      this._cafeError = new Error();
      this._cafeError.Code = code;
    }

    public ErrorException(int code, Control offendingControl)
      : this(code)
    {
      this._offendingControl = offendingControl;
    }

    public ErrorException(int code, string message)
      : this(code)
    {
      if (this._cafeError == null)
        this._cafeError = new Error();
      this._cafeError.Message = message;
    }

    public ErrorException(Error error) => this._cafeError = error;

    public Control OffendingControl
    {
      get => this._offendingControl;
      set => this._offendingControl = value;
    }

    public Error CafeError
    {
      get => this._cafeError;
      set => this._cafeError = value;
    }
  }
}
