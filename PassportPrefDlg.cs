// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.PassportPrefDlg
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
  public class PassportPrefDlg : HelpFormBase
  {
    private DShipDefl _domPrefs;
    private IContainer components;
    private TabControlEx tabControlPassportPrefs;
    private TabPage tabPagePassportOtherPrefs;
    private TabPage tabPagePassportUpgradeDowngradePrefs;
    private Button btnOk;
    private Button btnCancel;
    private PassportOtherPreferences passportOtherPreferences;
    private PassportUpgradeDowngradePreferences passportUpgradeDowngradePreferences;

    public PassportPrefDlg(DShipDefl domPrefs)
    {
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this._domPrefs = domPrefs;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      this.passportOtherPreferences.ScreenToObject(this._domPrefs);
      this.passportUpgradeDowngradePreferences.ScreenToObject(this._domPrefs);
      this.DialogResult = DialogResult.OK;
    }

    private void PassportPrefDlg_Load(object sender, EventArgs e)
    {
      this.passportOtherPreferences.ObjectToScreen(this._domPrefs);
      this.passportUpgradeDowngradePreferences.ObjectToScreen(this._domPrefs);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PassportPrefDlg));
      this.tabControlPassportPrefs = new TabControlEx();
      this.tabPagePassportOtherPrefs = new TabPage();
      this.passportOtherPreferences = new PassportOtherPreferences();
      this.tabPagePassportUpgradeDowngradePrefs = new TabPage();
      this.passportUpgradeDowngradePreferences = new PassportUpgradeDowngradePreferences();
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.tabControlPassportPrefs.SuspendLayout();
      this.tabPagePassportOtherPrefs.SuspendLayout();
      this.tabPagePassportUpgradeDowngradePrefs.SuspendLayout();
      this.SuspendLayout();
      this.tabControlPassportPrefs.Controls.Add((Control) this.tabPagePassportOtherPrefs);
      this.tabControlPassportPrefs.Controls.Add((Control) this.tabPagePassportUpgradeDowngradePrefs);
      this.tabControlPassportPrefs.DrawMode = TabDrawMode.OwnerDrawFixed;
      componentResourceManager.ApplyResources((object) this.tabControlPassportPrefs, "tabControlPassportPrefs");
      this.tabControlPassportPrefs.MnemonicEnabled = true;
      this.tabControlPassportPrefs.Name = "tabControlPassportPrefs";
      this.tabControlPassportPrefs.SelectedIndex = 0;
      this.tabControlPassportPrefs.UseIndexAsMnemonic = true;
      this.tabPagePassportOtherPrefs.Controls.Add((Control) this.passportOtherPreferences);
      componentResourceManager.ApplyResources((object) this.tabPagePassportOtherPrefs, "tabPagePassportOtherPrefs");
      this.tabPagePassportOtherPrefs.Name = "tabPagePassportOtherPrefs";
      this.tabPagePassportOtherPrefs.UseVisualStyleBackColor = true;
      this.passportOtherPreferences.BackColor = Color.White;
      componentResourceManager.ApplyResources((object) this.passportOtherPreferences, "passportOtherPreferences");
      this.passportOtherPreferences.Name = "passportOtherPreferences";
      this.tabPagePassportUpgradeDowngradePrefs.Controls.Add((Control) this.passportUpgradeDowngradePreferences);
      componentResourceManager.ApplyResources((object) this.tabPagePassportUpgradeDowngradePrefs, "tabPagePassportUpgradeDowngradePrefs");
      this.tabPagePassportUpgradeDowngradePrefs.Name = "tabPagePassportUpgradeDowngradePrefs";
      this.tabPagePassportUpgradeDowngradePrefs.UseVisualStyleBackColor = true;
      this.passportUpgradeDowngradePreferences.BackColor = Color.White;
      componentResourceManager.ApplyResources((object) this.passportUpgradeDowngradePreferences, "passportUpgradeDowngradePreferences");
      this.passportUpgradeDowngradePreferences.Name = "passportUpgradeDowngradePreferences";
      this.btnOk.DialogResult = DialogResult.OK;
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.Name = "btnOk";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.btnCancel.DialogResult = DialogResult.Cancel;
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.tabControlPassportPrefs);
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (PassportPrefDlg);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Load += new EventHandler(this.PassportPrefDlg_Load);
      this.tabControlPassportPrefs.ResumeLayout(false);
      this.tabPagePassportOtherPrefs.ResumeLayout(false);
      this.tabPagePassportUpgradeDowngradePrefs.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
