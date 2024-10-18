// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SmartPostConfiguration.SmartPostConfigurationForm
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.SmartPostConfiguration
{
  public class SmartPostConfigurationForm : HelpFormBase
  {
    private IContainer components;
    private SmartPostConfigurationUserControl smartPostConfigurationUserControl1;
    private Button btnCancel;
    private Button btnOk;

    public SmartPostConfigurationForm(Account account)
    {
      this.InitializeComponent();
      this.smartPostConfigurationUserControl1.SetupUpDialog(account);
    }

    private void btnOk_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

    public static int ValidateSettings(Account settings, out string message)
    {
      return SmartPostConfigurationUserControl.ValidateSettings(settings, out message);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SmartPostConfigurationForm));
      this.btnCancel = new Button();
      this.btnOk = new Button();
      this.smartPostConfigurationUserControl1 = new SmartPostConfigurationUserControl();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      componentResourceManager.ApplyResources((object) this.smartPostConfigurationUserControl1, "smartPostConfigurationUserControl1");
      this.smartPostConfigurationUserControl1.Name = "smartPostConfigurationUserControl1";
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.smartPostConfigurationUserControl1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (SmartPostConfigurationForm);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ResumeLayout(false);
    }
  }
}
