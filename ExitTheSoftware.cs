// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.ExitTheSoftware
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class ExitTheSoftware : HelpFormBase
  {
    private ExitTheSoftware.ExitAction _userExitAction;
    private IContainer components;
    private ColorGroupBox colorGroupBox1;
    private Button btnYes;
    private Button btnNo;
    private RadioButton rdbExit;
    private RadioButton rdbReboot;
    private RadioButton rdbLogoff;

    public ExitTheSoftware.ExitAction UserExitAction
    {
      get => this._userExitAction;
      set => this._userExitAction = value;
    }

    public ExitTheSoftware()
    {
      this.InitializeComponent();
      if (GuiData.CurrentAccount != null)
        this.rdbLogoff.Enabled = GuiData.CurrentAccount.RequireUserLogin;
      else
        this.rdbLogoff.Enabled = false;
      if (this.rdbLogoff.Enabled)
        this.rdbLogoff.Checked = true;
      else
        this.rdbExit.Checked = true;
    }

    protected override void OnClosed(EventArgs e)
    {
      this._userExitAction = !this.rdbLogoff.Checked ? (!this.rdbReboot.Checked ? (!this.rdbExit.Checked ? ExitTheSoftware.ExitAction.None : ExitTheSoftware.ExitAction.Exit) : ExitTheSoftware.ExitAction.Reboot) : ExitTheSoftware.ExitAction.Logoff;
      base.OnClosed(e);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExitTheSoftware));
      this.colorGroupBox1 = new ColorGroupBox();
      this.rdbExit = new RadioButton();
      this.rdbReboot = new RadioButton();
      this.rdbLogoff = new RadioButton();
      this.btnYes = new Button();
      this.btnNo = new Button();
      this.colorGroupBox1.SuspendLayout();
      this.SuspendLayout();
      this.colorGroupBox1.BorderThickness = 1f;
      this.colorGroupBox1.Controls.Add((Control) this.rdbExit);
      this.colorGroupBox1.Controls.Add((Control) this.rdbReboot);
      this.colorGroupBox1.Controls.Add((Control) this.rdbLogoff);
      this.colorGroupBox1.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.colorGroupBox1, "colorGroupBox1");
      this.colorGroupBox1.Name = "colorGroupBox1";
      this.colorGroupBox1.RoundCorners = 5;
      this.colorGroupBox1.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdbExit, componentResourceManager.GetString("rdbExit.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbExit, (HelpNavigator) componentResourceManager.GetObject("rdbExit.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbExit, "rdbExit");
      this.rdbExit.Name = "rdbExit";
      this.helpProvider1.SetShowHelp((Control) this.rdbExit, (bool) componentResourceManager.GetObject("rdbExit.ShowHelp"));
      this.rdbExit.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdbReboot, componentResourceManager.GetString("rdbReboot.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbReboot, (HelpNavigator) componentResourceManager.GetObject("rdbReboot.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbReboot, "rdbReboot");
      this.rdbReboot.Name = "rdbReboot";
      this.helpProvider1.SetShowHelp((Control) this.rdbReboot, (bool) componentResourceManager.GetObject("rdbReboot.ShowHelp"));
      this.rdbReboot.UseVisualStyleBackColor = true;
      this.rdbLogoff.Checked = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdbLogoff, componentResourceManager.GetString("rdbLogoff.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbLogoff, (HelpNavigator) componentResourceManager.GetObject("rdbLogoff.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbLogoff, "rdbLogoff");
      this.rdbLogoff.Name = "rdbLogoff";
      this.helpProvider1.SetShowHelp((Control) this.rdbLogoff, (bool) componentResourceManager.GetObject("rdbLogoff.ShowHelp"));
      this.rdbLogoff.TabStop = true;
      this.rdbLogoff.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnYes, "btnYes");
      this.btnYes.DialogResult = DialogResult.Yes;
      this.btnYes.Name = "btnYes";
      this.btnYes.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnNo, "btnNo");
      this.btnNo.DialogResult = DialogResult.No;
      this.btnNo.Name = "btnNo";
      this.btnNo.UseVisualStyleBackColor = true;
      this.AcceptButton = (IButtonControl) this.btnYes;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnNo;
      this.ControlBox = false;
      this.Controls.Add((Control) this.btnNo);
      this.Controls.Add((Control) this.btnYes);
      this.Controls.Add((Control) this.colorGroupBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ExitTheSoftware);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowInTaskbar = false;
      this.colorGroupBox1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public enum ExitAction
    {
      None,
      Logoff,
      Reboot,
      Exit,
    }
  }
}
