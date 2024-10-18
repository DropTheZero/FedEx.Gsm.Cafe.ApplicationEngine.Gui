// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.FreightPrefDlg
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class FreightPrefDlg : HelpFormBase
  {
    private FShipDefl _preferenceObject;
    private Utility.FormOperation _eOperation;
    private IContainer components;
    private TabControlEx tabControlFreightPrefs;
    private TabPage tabPageFreightFieldPrefs;
    private FreightFieldPreferences FreightAllFieldPreferences;
    private TabPage tabPageOthersPrefs;
    private FreightOtherPreferences FreightOtherExpressPreferences;
    private Button btnCancel;
    private Button btnOk;
    private TabPage tabPageShipAlertPrefs;
    private FreightShipAlertPreferences FreightShipAlertPreferences;

    public FShipDefl PreferenceObject
    {
      get => this._preferenceObject;
      set => this._preferenceObject = value;
    }

    public FreightPrefDlg(FShipDefl preferenceObject, Utility.FormOperation eOperation)
    {
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this._preferenceObject = preferenceObject;
      this._eOperation = eOperation;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (!this.FreightAllFieldPreferences.OkToClose())
      {
        this.DialogResult = DialogResult.None;
      }
      else
      {
        this.FreightOtherExpressPreferences.ScreenToObject();
        if (!this.FreightShipAlertPreferences.OkToClose())
          this.DialogResult = DialogResult.None;
        else
          this.DialogResult = DialogResult.OK;
      }
    }

    private void FreightPrefDlg_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.FreightAllFieldPreferences.PreferenceObject = (ShipDefl) this._preferenceObject;
      this.FreightOtherExpressPreferences.InitOtherPrefs(this._preferenceObject);
      this.FreightShipAlertPreferences.PreferenceObject = (ShipDefl) this._preferenceObject;
      this.ObjectToScreen();
    }

    private void ObjectToScreen()
    {
      this.FreightAllFieldPreferences.ObjectToScreen();
      this.FreightOtherExpressPreferences.ObjectToScreen();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FreightPrefDlg));
      this.tabControlFreightPrefs = new TabControlEx();
      this.tabPageFreightFieldPrefs = new TabPage();
      this.FreightAllFieldPreferences = new FreightFieldPreferences();
      this.tabPageOthersPrefs = new TabPage();
      this.FreightOtherExpressPreferences = new FreightOtherPreferences();
      this.tabPageShipAlertPrefs = new TabPage();
      this.FreightShipAlertPreferences = new FreightShipAlertPreferences();
      this.btnCancel = new Button();
      this.btnOk = new Button();
      this.tabControlFreightPrefs.SuspendLayout();
      this.tabPageFreightFieldPrefs.SuspendLayout();
      this.tabPageOthersPrefs.SuspendLayout();
      this.tabPageShipAlertPrefs.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.tabControlFreightPrefs, "tabControlFreightPrefs");
      this.tabControlFreightPrefs.Controls.Add((Control) this.tabPageFreightFieldPrefs);
      this.tabControlFreightPrefs.Controls.Add((Control) this.tabPageOthersPrefs);
      this.tabControlFreightPrefs.Controls.Add((Control) this.tabPageShipAlertPrefs);
      this.tabControlFreightPrefs.DrawMode = TabDrawMode.OwnerDrawFixed;
      this.tabControlFreightPrefs.MnemonicEnabled = true;
      this.tabControlFreightPrefs.Multiline = true;
      this.tabControlFreightPrefs.Name = "tabControlFreightPrefs";
      this.tabControlFreightPrefs.SelectedIndex = 0;
      this.helpProvider1.SetShowHelp((Control) this.tabControlFreightPrefs, (bool) componentResourceManager.GetObject("tabControlFreightPrefs.ShowHelp"));
      this.tabControlFreightPrefs.UseIndexAsMnemonic = true;
      this.tabPageFreightFieldPrefs.Controls.Add((Control) this.FreightAllFieldPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageFreightFieldPrefs, "tabPageFreightFieldPrefs");
      this.tabPageFreightFieldPrefs.Name = "tabPageFreightFieldPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageFreightFieldPrefs, (bool) componentResourceManager.GetObject("tabPageFreightFieldPrefs.ShowHelp"));
      this.tabPageFreightFieldPrefs.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.FreightAllFieldPreferences, "FreightAllFieldPreferences");
      this.FreightAllFieldPreferences.BackColor = Color.White;
      this.FreightAllFieldPreferences.CurrentPrefTypeIndex = -1;
      this.FreightAllFieldPreferences.IsLoading = false;
      this.FreightAllFieldPreferences.Name = "FreightAllFieldPreferences";
      this.FreightAllFieldPreferences.PreferenceObject = (ShipDefl) null;
      this.FreightAllFieldPreferences.PrevPrefTypeIndex = -1;
      this.helpProvider1.SetShowHelp((Control) this.FreightAllFieldPreferences, (bool) componentResourceManager.GetObject("FreightAllFieldPreferences.ShowHelp"));
      this.tabPageOthersPrefs.BackColor = Color.White;
      this.tabPageOthersPrefs.Controls.Add((Control) this.FreightOtherExpressPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageOthersPrefs, "tabPageOthersPrefs");
      this.tabPageOthersPrefs.Name = "tabPageOthersPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageOthersPrefs, (bool) componentResourceManager.GetObject("tabPageOthersPrefs.ShowHelp"));
      this.tabPageOthersPrefs.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.FreightOtherExpressPreferences, "FreightOtherExpressPreferences");
      this.FreightOtherExpressPreferences.Name = "FreightOtherExpressPreferences";
      this.FreightOtherExpressPreferences.PreferenceObject = (FShipDefl) null;
      this.helpProvider1.SetShowHelp((Control) this.FreightOtherExpressPreferences, (bool) componentResourceManager.GetObject("FreightOtherExpressPreferences.ShowHelp"));
      this.tabPageShipAlertPrefs.Controls.Add((Control) this.FreightShipAlertPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageShipAlertPrefs, "tabPageShipAlertPrefs");
      this.tabPageShipAlertPrefs.Name = "tabPageShipAlertPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageShipAlertPrefs, (bool) componentResourceManager.GetObject("tabPageShipAlertPrefs.ShowHelp"));
      this.tabPageShipAlertPrefs.UseVisualStyleBackColor = true;
      this.FreightShipAlertPreferences.BackColor = Color.White;
      this.FreightShipAlertPreferences.CurrentPrefTypeIndex = -1;
      componentResourceManager.ApplyResources((object) this.FreightShipAlertPreferences, "FreightShipAlertPreferences");
      this.FreightShipAlertPreferences.IsLoading = false;
      this.FreightShipAlertPreferences.Name = "FreightShipAlertPreferences";
      this.FreightShipAlertPreferences.PreferenceObject = (ShipDefl) null;
      this.FreightShipAlertPreferences.PrevPrefTypeIndex = -1;
      this.helpProvider1.SetShowHelp((Control) this.FreightShipAlertPreferences, (bool) componentResourceManager.GetObject("FreightShipAlertPreferences.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Name = "btnCancel";
      this.helpProvider1.SetShowHelp((Control) this.btnCancel, (bool) componentResourceManager.GetObject("btnCancel.ShowHelp"));
      this.btnCancel.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.helpProvider1.SetShowHelp((Control) this.btnOk, (bool) componentResourceManager.GetObject("btnOk.ShowHelp"));
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.tabControlFreightPrefs);
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FreightPrefDlg);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Load += new EventHandler(this.FreightPrefDlg_Load);
      this.tabControlFreightPrefs.ResumeLayout(false);
      this.tabPageFreightFieldPrefs.ResumeLayout(false);
      this.tabPageOthersPrefs.ResumeLayout(false);
      this.tabPageShipAlertPrefs.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
