// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.PassportOtherPreferences
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class PassportOtherPreferences : UserControlHelpEx
  {
    private IContainer components;
    private FlowLayoutPanel flowLayoutPanelPassportOtherPrefs;
    private ColorGroupBox gbxBatchIndiv;
    private ColorGroupBox gbxBatchEdit;
    private ColorGroupBox gbxDefaultProcessing;
    private CheckBox chkBatchProcess;
    private CheckBox chkSingleProcess;
    private CheckBox chkSetFocusToShipButton;
    private RadioButton rdoBatchEditIndividual;
    private RadioButton rdoBatchEdit;
    private RadioButton rdoBatchIndividual;
    private RadioButton rdoBatchAll;
    private ColorGroupBox gbxBatchAll;
    private CheckBox chkStopProcessingOnFail;

    public PassportOtherPreferences() => this.InitializeComponent();

    public void ObjectToScreen(DShipDefl domPrefs)
    {
      this.chkSingleProcess.Checked = domPrefs.SingleProcess;
      this.chkBatchProcess.Checked = domPrefs.BatchDisplay;
      this.chkSetFocusToShipButton.Checked = domPrefs.SetFocusToShipButton;
      this.chkStopProcessingOnFail.Checked = domPrefs.StopProcessingOnFailure;
      this.rdoBatchAll.Checked = domPrefs.DefaultProcessingMode == DShipDefl.DefaultPassPortMode.BatchAll;
      this.rdoBatchEdit.Checked = domPrefs.DefaultProcessingMode == DShipDefl.DefaultPassPortMode.BatchEdit;
      this.rdoBatchEditIndividual.Checked = domPrefs.DefaultProcessingMode == DShipDefl.DefaultPassPortMode.BatchEditIndividual;
      this.rdoBatchIndividual.Checked = domPrefs.DefaultProcessingMode == DShipDefl.DefaultPassPortMode.BatchIndividual;
    }

    public void ScreenToObject(DShipDefl domPrefs)
    {
      domPrefs.SingleProcess = this.chkSingleProcess.Checked;
      domPrefs.BatchDisplay = this.chkBatchProcess.Checked;
      domPrefs.SetFocusToShipButton = this.chkSetFocusToShipButton.Checked;
      domPrefs.StopProcessingOnFailure = this.chkStopProcessingOnFail.Checked;
      if (this.rdoBatchAll.Checked)
        domPrefs.DefaultProcessingMode = DShipDefl.DefaultPassPortMode.BatchAll;
      else if (this.rdoBatchEdit.Checked)
        domPrefs.DefaultProcessingMode = DShipDefl.DefaultPassPortMode.BatchEdit;
      else if (this.rdoBatchEditIndividual.Checked)
      {
        domPrefs.DefaultProcessingMode = DShipDefl.DefaultPassPortMode.BatchEditIndividual;
      }
      else
      {
        if (!this.rdoBatchIndividual.Checked)
          return;
        domPrefs.DefaultProcessingMode = DShipDefl.DefaultPassPortMode.BatchIndividual;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PassportOtherPreferences));
      this.flowLayoutPanelPassportOtherPrefs = new FlowLayoutPanel();
      this.gbxBatchIndiv = new ColorGroupBox();
      this.chkBatchProcess = new CheckBox();
      this.chkSingleProcess = new CheckBox();
      this.gbxBatchEdit = new ColorGroupBox();
      this.chkSetFocusToShipButton = new CheckBox();
      this.gbxDefaultProcessing = new ColorGroupBox();
      this.rdoBatchEditIndividual = new RadioButton();
      this.rdoBatchEdit = new RadioButton();
      this.rdoBatchIndividual = new RadioButton();
      this.rdoBatchAll = new RadioButton();
      this.gbxBatchAll = new ColorGroupBox();
      this.chkStopProcessingOnFail = new CheckBox();
      this.flowLayoutPanelPassportOtherPrefs.SuspendLayout();
      this.gbxBatchIndiv.SuspendLayout();
      this.gbxBatchEdit.SuspendLayout();
      this.gbxDefaultProcessing.SuspendLayout();
      this.gbxBatchAll.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      this.flowLayoutPanelPassportOtherPrefs.Controls.Add((Control) this.gbxBatchIndiv);
      this.flowLayoutPanelPassportOtherPrefs.Controls.Add((Control) this.gbxBatchEdit);
      this.flowLayoutPanelPassportOtherPrefs.Controls.Add((Control) this.gbxDefaultProcessing);
      this.flowLayoutPanelPassportOtherPrefs.Controls.Add((Control) this.gbxBatchAll);
      componentResourceManager.ApplyResources((object) this.flowLayoutPanelPassportOtherPrefs, "flowLayoutPanelPassportOtherPrefs");
      this.flowLayoutPanelPassportOtherPrefs.Name = "flowLayoutPanelPassportOtherPrefs";
      this.gbxBatchIndiv.BorderThickness = 1f;
      this.gbxBatchIndiv.Controls.Add((Control) this.chkBatchProcess);
      this.gbxBatchIndiv.Controls.Add((Control) this.chkSingleProcess);
      this.gbxBatchIndiv.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxBatchIndiv, "gbxBatchIndiv");
      this.gbxBatchIndiv.Name = "gbxBatchIndiv";
      this.gbxBatchIndiv.RoundCorners = 5;
      this.gbxBatchIndiv.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkBatchProcess, componentResourceManager.GetString("chkBatchProcess.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkBatchProcess, (HelpNavigator) componentResourceManager.GetObject("chkBatchProcess.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkBatchProcess, "chkBatchProcess");
      this.chkBatchProcess.Name = "chkBatchProcess";
      this.helpProvider1.SetShowHelp((Control) this.chkBatchProcess, (bool) componentResourceManager.GetObject("chkBatchProcess.ShowHelp"));
      this.chkBatchProcess.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkSingleProcess, componentResourceManager.GetString("chkSingleProcess.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkSingleProcess, (HelpNavigator) componentResourceManager.GetObject("chkSingleProcess.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkSingleProcess, "chkSingleProcess");
      this.chkSingleProcess.Name = "chkSingleProcess";
      this.helpProvider1.SetShowHelp((Control) this.chkSingleProcess, (bool) componentResourceManager.GetObject("chkSingleProcess.ShowHelp"));
      this.chkSingleProcess.UseVisualStyleBackColor = true;
      this.gbxBatchEdit.BorderThickness = 1f;
      this.gbxBatchEdit.Controls.Add((Control) this.chkSetFocusToShipButton);
      this.gbxBatchEdit.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxBatchEdit, "gbxBatchEdit");
      this.gbxBatchEdit.Name = "gbxBatchEdit";
      this.gbxBatchEdit.RoundCorners = 5;
      this.gbxBatchEdit.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkSetFocusToShipButton, componentResourceManager.GetString("chkSetFocusToShipButton.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkSetFocusToShipButton, (HelpNavigator) componentResourceManager.GetObject("chkSetFocusToShipButton.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkSetFocusToShipButton, "chkSetFocusToShipButton");
      this.chkSetFocusToShipButton.Name = "chkSetFocusToShipButton";
      this.helpProvider1.SetShowHelp((Control) this.chkSetFocusToShipButton, (bool) componentResourceManager.GetObject("chkSetFocusToShipButton.ShowHelp"));
      this.chkSetFocusToShipButton.UseVisualStyleBackColor = true;
      this.gbxDefaultProcessing.BorderThickness = 1f;
      this.gbxDefaultProcessing.Controls.Add((Control) this.rdoBatchEditIndividual);
      this.gbxDefaultProcessing.Controls.Add((Control) this.rdoBatchEdit);
      this.gbxDefaultProcessing.Controls.Add((Control) this.rdoBatchIndividual);
      this.gbxDefaultProcessing.Controls.Add((Control) this.rdoBatchAll);
      this.gbxDefaultProcessing.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxDefaultProcessing, "gbxDefaultProcessing");
      this.gbxDefaultProcessing.Name = "gbxDefaultProcessing";
      this.gbxDefaultProcessing.RoundCorners = 5;
      this.gbxDefaultProcessing.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoBatchEditIndividual, componentResourceManager.GetString("rdoBatchEditIndividual.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoBatchEditIndividual, (HelpNavigator) componentResourceManager.GetObject("rdoBatchEditIndividual.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoBatchEditIndividual, "rdoBatchEditIndividual");
      this.rdoBatchEditIndividual.Name = "rdoBatchEditIndividual";
      this.helpProvider1.SetShowHelp((Control) this.rdoBatchEditIndividual, (bool) componentResourceManager.GetObject("rdoBatchEditIndividual.ShowHelp"));
      this.rdoBatchEditIndividual.TabStop = true;
      this.rdoBatchEditIndividual.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoBatchEdit, componentResourceManager.GetString("rdoBatchEdit.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoBatchEdit, (HelpNavigator) componentResourceManager.GetObject("rdoBatchEdit.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoBatchEdit, "rdoBatchEdit");
      this.rdoBatchEdit.Name = "rdoBatchEdit";
      this.helpProvider1.SetShowHelp((Control) this.rdoBatchEdit, (bool) componentResourceManager.GetObject("rdoBatchEdit.ShowHelp"));
      this.rdoBatchEdit.TabStop = true;
      this.rdoBatchEdit.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoBatchIndividual, componentResourceManager.GetString("rdoBatchIndividual.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoBatchIndividual, (HelpNavigator) componentResourceManager.GetObject("rdoBatchIndividual.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoBatchIndividual, "rdoBatchIndividual");
      this.rdoBatchIndividual.Name = "rdoBatchIndividual";
      this.helpProvider1.SetShowHelp((Control) this.rdoBatchIndividual, (bool) componentResourceManager.GetObject("rdoBatchIndividual.ShowHelp"));
      this.rdoBatchIndividual.TabStop = true;
      this.rdoBatchIndividual.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoBatchAll, componentResourceManager.GetString("rdoBatchAll.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoBatchAll, (HelpNavigator) componentResourceManager.GetObject("rdoBatchAll.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoBatchAll, "rdoBatchAll");
      this.rdoBatchAll.Name = "rdoBatchAll";
      this.helpProvider1.SetShowHelp((Control) this.rdoBatchAll, (bool) componentResourceManager.GetObject("rdoBatchAll.ShowHelp"));
      this.rdoBatchAll.TabStop = true;
      this.rdoBatchAll.UseVisualStyleBackColor = true;
      this.gbxBatchAll.BorderThickness = 1f;
      this.gbxBatchAll.Controls.Add((Control) this.chkStopProcessingOnFail);
      this.gbxBatchAll.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxBatchAll, "gbxBatchAll");
      this.gbxBatchAll.Name = "gbxBatchAll";
      this.gbxBatchAll.RoundCorners = 5;
      this.gbxBatchAll.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkStopProcessingOnFail, componentResourceManager.GetString("chkStopProcessingOnFail.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkStopProcessingOnFail, (HelpNavigator) componentResourceManager.GetObject("chkStopProcessingOnFail.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkStopProcessingOnFail, "chkStopProcessingOnFail");
      this.chkStopProcessingOnFail.Name = "chkStopProcessingOnFail";
      this.helpProvider1.SetShowHelp((Control) this.chkStopProcessingOnFail, (bool) componentResourceManager.GetObject("chkStopProcessingOnFail.ShowHelp"));
      this.chkStopProcessingOnFail.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.flowLayoutPanelPassportOtherPrefs);
      this.Name = nameof (PassportOtherPreferences);
      this.flowLayoutPanelPassportOtherPrefs.ResumeLayout(false);
      this.gbxBatchIndiv.ResumeLayout(false);
      this.gbxBatchEdit.ResumeLayout(false);
      this.gbxDefaultProcessing.ResumeLayout(false);
      this.gbxBatchAll.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
