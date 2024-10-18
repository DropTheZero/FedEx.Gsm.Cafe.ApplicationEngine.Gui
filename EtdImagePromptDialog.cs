// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.EtdImagePromptDialog
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class EtdImagePromptDialog : Form
  {
    private Account _account;
    private IContainer components;
    private Label lblInformationIcon;
    private Label lblEtdDescription;
    private Button btnIntlPreferences;
    private Button btnProvideLater;

    public event TopicDelegate ShippingPreferencesChanged;

    public EtdImagePromptDialog(Account a)
    {
      this._account = a ?? GuiData.CurrentAccount;
      this.InitializeComponent();
      this.lblInformationIcon.Image = (Image) SystemIcons.Information.ToBitmap();
      this.SetupEvents();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      if (disposing)
        this.RemoveEvents();
      base.Dispose(disposing);
    }

    private void SetupEvents()
    {
      GuiData.EventBroker.AddPublisher(EventBroker.Events.ShippingPreferencesChanged, (object) this, "ShippingPreferencesChanged");
    }

    private void RemoveEvents()
    {
      try
      {
        GuiData.EventBroker.RemovePublisher(EventBroker.Events.ShippingPreferencesChanged, (object) this, "ShippingPreferencesChanged");
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Verbose, FxLogger.AppCode_GUI, "frmHoldFileList.Dispose()", ex.ToString());
      }
    }

    private void btnIntlPreferences_Click(object sender, EventArgs e)
    {
      IShipDefl filter = new IShipDefl();
      filter.ProfileCode = "DEFAULT";
      filter.MeterNum = this._account.MeterNumber;
      filter.AccountNum = this._account.AccountNumber;
      IShipDefl output;
      if (GuiData.AppController.ShipEngine.Retrieve<IShipDefl>(filter, out output, out Error _) != 1)
        return;
      IntlPrefDlg intlPrefDlg = new IntlPrefDlg(this._account, output, Utility.FormOperation.ViewEdit);
      intlPrefDlg.SwitchToCustomsTab();
      if (intlPrefDlg.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.Modify<IShipDefl>(output);
      if (serviceResponse.Error.Code != 1)
      {
        Utility.DisplayError(serviceResponse.Error);
      }
      else
      {
        ShippingPreferencesChangedEventArgs args = new ShippingPreferencesChangedEventArgs((ShipDefl) output);
        if (this.ShippingPreferencesChanged == null)
          return;
        this.ShippingPreferencesChanged((object) this, (EventArgs) args);
      }
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EtdImagePromptDialog));
      this.lblInformationIcon = new Label();
      this.lblEtdDescription = new Label();
      this.btnIntlPreferences = new Button();
      this.btnProvideLater = new Button();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.lblInformationIcon, "lblInformationIcon");
      this.lblInformationIcon.Name = "lblInformationIcon";
      componentResourceManager.ApplyResources((object) this.lblEtdDescription, "lblEtdDescription");
      this.lblEtdDescription.Name = "lblEtdDescription";
      componentResourceManager.ApplyResources((object) this.btnIntlPreferences, "btnIntlPreferences");
      this.btnIntlPreferences.DialogResult = DialogResult.OK;
      this.btnIntlPreferences.Name = "btnIntlPreferences";
      this.btnIntlPreferences.UseVisualStyleBackColor = true;
      this.btnIntlPreferences.Click += new EventHandler(this.btnIntlPreferences_Click);
      componentResourceManager.ApplyResources((object) this.btnProvideLater, "btnProvideLater");
      this.btnProvideLater.DialogResult = DialogResult.Cancel;
      this.btnProvideLater.Name = "btnProvideLater";
      this.btnProvideLater.UseVisualStyleBackColor = true;
      this.AcceptButton = (IButtonControl) this.btnIntlPreferences;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnProvideLater;
      this.ControlBox = false;
      this.Controls.Add((Control) this.btnProvideLater);
      this.Controls.Add((Control) this.btnIntlPreferences);
      this.Controls.Add((Control) this.lblEtdDescription);
      this.Controls.Add((Control) this.lblInformationIcon);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (EtdImagePromptDialog);
      this.ResumeLayout(false);
    }
  }
}
