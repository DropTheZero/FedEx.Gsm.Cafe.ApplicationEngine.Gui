// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.PassportUpgradeDowngradePreferences
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
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
  public class PassportUpgradeDowngradePreferences : UserControlHelpEx
  {
    private IContainer components;
    private ColorGroupBox gbxUpgradeDowngradePrefs;
    private ColorGroupBox gbxStandardOvernight;
    private RadioButton rdoStdNone;
    private RadioButton rdoStdDowngradeTo2Day;
    private RadioButton rdoStdUpgradeToPO;
    private ColorGroupBox gbxThurs;
    private RadioButton rdoThursNone;
    private RadioButton rdoThursDowngradeForMon;
    private RadioButton rdoThursUpgradeForSat;
    private ColorGroupBox gbxPO;
    private RadioButton rdoPONone;
    private RadioButton rdoPODowngradeForMon;

    public PassportUpgradeDowngradePreferences() => this.InitializeComponent();

    public void ObjectToScreen(DShipDefl domPrefs)
    {
      this.rdoStdUpgradeToPO.Checked = domPrefs.UpgradeDowngradeOptions.PassPortStdOvernightUpgDwnGrdSelection == DShipDefl.StdOvernightOptions.UpgradeToPriorityOvernight;
      this.rdoStdDowngradeTo2Day.Checked = domPrefs.UpgradeDowngradeOptions.PassPortStdOvernightUpgDwnGrdSelection == DShipDefl.StdOvernightOptions.DowngradeTo2Day;
      this.rdoStdNone.Checked = domPrefs.UpgradeDowngradeOptions.PassPortStdOvernightUpgDwnGrdSelection == DShipDefl.StdOvernightOptions.None;
      this.rdoThursUpgradeForSat.Checked = domPrefs.UpgradeDowngradeOptions.PassPortThr2DayForSatDeliverySelection == DShipDefl.Thr2DayForSatDeliveryOptions.UpgradeToPriorityOvernight;
      this.rdoThursDowngradeForMon.Checked = domPrefs.UpgradeDowngradeOptions.PassPortThr2DayForSatDeliverySelection == DShipDefl.Thr2DayForSatDeliveryOptions.DowngradeTo2DayWithMondayDelivery;
      this.rdoThursNone.Checked = domPrefs.UpgradeDowngradeOptions.PassPortThr2DayForSatDeliverySelection == DShipDefl.Thr2DayForSatDeliveryOptions.None;
      this.rdoPODowngradeForMon.Checked = domPrefs.UpgradeDowngradeOptions.PassPortPOForSatDeliverySelection == DShipDefl.POForSatDeliveryOptions.DowngradeToPriorityOverightWithMondayDelievery;
      this.rdoPONone.Checked = domPrefs.UpgradeDowngradeOptions.PassPortPOForSatDeliverySelection == DShipDefl.POForSatDeliveryOptions.None;
    }

    public void ScreenToObject(DShipDefl domPrefs)
    {
      domPrefs.UpgradeDowngradeOptions.PassPortStdOvernightUpgDwnGrdSelection = !this.rdoStdUpgradeToPO.Checked ? (!this.rdoStdDowngradeTo2Day.Checked ? DShipDefl.StdOvernightOptions.None : DShipDefl.StdOvernightOptions.DowngradeTo2Day) : DShipDefl.StdOvernightOptions.UpgradeToPriorityOvernight;
      domPrefs.UpgradeDowngradeOptions.PassPortThr2DayForSatDeliverySelection = !this.rdoThursUpgradeForSat.Checked ? (!this.rdoThursDowngradeForMon.Checked ? DShipDefl.Thr2DayForSatDeliveryOptions.None : DShipDefl.Thr2DayForSatDeliveryOptions.DowngradeTo2DayWithMondayDelivery) : DShipDefl.Thr2DayForSatDeliveryOptions.UpgradeToPriorityOvernight;
      if (this.rdoPODowngradeForMon.Checked)
        domPrefs.UpgradeDowngradeOptions.PassPortPOForSatDeliverySelection = DShipDefl.POForSatDeliveryOptions.DowngradeToPriorityOverightWithMondayDelievery;
      else
        domPrefs.UpgradeDowngradeOptions.PassPortPOForSatDeliverySelection = DShipDefl.POForSatDeliveryOptions.None;
    }

    private void PassportUpgradeDowngradePreferences_Load(object sender, EventArgs e)
    {
      if (this.DesignMode || !(GuiData.CurrentAccount.Address.CountryCode == "MX"))
        return;
      this.gbxPO.Visible = false;
      this.gbxThurs.Visible = false;
      this.rdoStdDowngradeTo2Day.Visible = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PassportUpgradeDowngradePreferences));
      this.gbxUpgradeDowngradePrefs = new ColorGroupBox();
      this.gbxPO = new ColorGroupBox();
      this.rdoPONone = new RadioButton();
      this.rdoPODowngradeForMon = new RadioButton();
      this.gbxThurs = new ColorGroupBox();
      this.rdoThursNone = new RadioButton();
      this.rdoThursDowngradeForMon = new RadioButton();
      this.rdoThursUpgradeForSat = new RadioButton();
      this.gbxStandardOvernight = new ColorGroupBox();
      this.rdoStdNone = new RadioButton();
      this.rdoStdDowngradeTo2Day = new RadioButton();
      this.rdoStdUpgradeToPO = new RadioButton();
      this.gbxUpgradeDowngradePrefs.SuspendLayout();
      this.gbxPO.SuspendLayout();
      this.gbxThurs.SuspendLayout();
      this.gbxStandardOvernight.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      this.gbxUpgradeDowngradePrefs.BorderThickness = 1f;
      this.gbxUpgradeDowngradePrefs.Controls.Add((Control) this.gbxPO);
      this.gbxUpgradeDowngradePrefs.Controls.Add((Control) this.gbxThurs);
      this.gbxUpgradeDowngradePrefs.Controls.Add((Control) this.gbxStandardOvernight);
      this.gbxUpgradeDowngradePrefs.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxUpgradeDowngradePrefs, "gbxUpgradeDowngradePrefs");
      this.gbxUpgradeDowngradePrefs.Name = "gbxUpgradeDowngradePrefs";
      this.gbxUpgradeDowngradePrefs.RoundCorners = 5;
      this.gbxUpgradeDowngradePrefs.TabStop = false;
      this.gbxPO.BorderThickness = 1f;
      this.gbxPO.Controls.Add((Control) this.rdoPONone);
      this.gbxPO.Controls.Add((Control) this.rdoPODowngradeForMon);
      this.gbxPO.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxPO, "gbxPO");
      this.gbxPO.Name = "gbxPO";
      this.gbxPO.RoundCorners = 5;
      this.gbxPO.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoPONone, componentResourceManager.GetString("rdoPONone.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoPONone, (HelpNavigator) componentResourceManager.GetObject("rdoPONone.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoPONone, "rdoPONone");
      this.rdoPONone.Name = "rdoPONone";
      this.helpProvider1.SetShowHelp((Control) this.rdoPONone, (bool) componentResourceManager.GetObject("rdoPONone.ShowHelp"));
      this.rdoPONone.TabStop = true;
      this.rdoPONone.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoPODowngradeForMon, componentResourceManager.GetString("rdoPODowngradeForMon.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoPODowngradeForMon, (HelpNavigator) componentResourceManager.GetObject("rdoPODowngradeForMon.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoPODowngradeForMon, "rdoPODowngradeForMon");
      this.rdoPODowngradeForMon.Name = "rdoPODowngradeForMon";
      this.helpProvider1.SetShowHelp((Control) this.rdoPODowngradeForMon, (bool) componentResourceManager.GetObject("rdoPODowngradeForMon.ShowHelp"));
      this.rdoPODowngradeForMon.TabStop = true;
      this.rdoPODowngradeForMon.UseVisualStyleBackColor = true;
      this.gbxThurs.BorderThickness = 1f;
      this.gbxThurs.Controls.Add((Control) this.rdoThursNone);
      this.gbxThurs.Controls.Add((Control) this.rdoThursDowngradeForMon);
      this.gbxThurs.Controls.Add((Control) this.rdoThursUpgradeForSat);
      this.gbxThurs.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxThurs, "gbxThurs");
      this.gbxThurs.Name = "gbxThurs";
      this.gbxThurs.RoundCorners = 5;
      this.gbxThurs.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoThursNone, componentResourceManager.GetString("rdoThursNone.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoThursNone, (HelpNavigator) componentResourceManager.GetObject("rdoThursNone.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoThursNone, "rdoThursNone");
      this.rdoThursNone.Name = "rdoThursNone";
      this.helpProvider1.SetShowHelp((Control) this.rdoThursNone, (bool) componentResourceManager.GetObject("rdoThursNone.ShowHelp"));
      this.rdoThursNone.TabStop = true;
      this.rdoThursNone.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoThursDowngradeForMon, componentResourceManager.GetString("rdoThursDowngradeForMon.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoThursDowngradeForMon, (HelpNavigator) componentResourceManager.GetObject("rdoThursDowngradeForMon.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoThursDowngradeForMon, "rdoThursDowngradeForMon");
      this.rdoThursDowngradeForMon.Name = "rdoThursDowngradeForMon";
      this.helpProvider1.SetShowHelp((Control) this.rdoThursDowngradeForMon, (bool) componentResourceManager.GetObject("rdoThursDowngradeForMon.ShowHelp"));
      this.rdoThursDowngradeForMon.TabStop = true;
      this.rdoThursDowngradeForMon.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoThursUpgradeForSat, componentResourceManager.GetString("rdoThursUpgradeForSat.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoThursUpgradeForSat, (HelpNavigator) componentResourceManager.GetObject("rdoThursUpgradeForSat.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoThursUpgradeForSat, "rdoThursUpgradeForSat");
      this.rdoThursUpgradeForSat.Name = "rdoThursUpgradeForSat";
      this.helpProvider1.SetShowHelp((Control) this.rdoThursUpgradeForSat, (bool) componentResourceManager.GetObject("rdoThursUpgradeForSat.ShowHelp"));
      this.rdoThursUpgradeForSat.TabStop = true;
      this.rdoThursUpgradeForSat.UseVisualStyleBackColor = true;
      this.gbxStandardOvernight.BorderThickness = 1f;
      this.gbxStandardOvernight.Controls.Add((Control) this.rdoStdNone);
      this.gbxStandardOvernight.Controls.Add((Control) this.rdoStdDowngradeTo2Day);
      this.gbxStandardOvernight.Controls.Add((Control) this.rdoStdUpgradeToPO);
      this.gbxStandardOvernight.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxStandardOvernight, "gbxStandardOvernight");
      this.gbxStandardOvernight.Name = "gbxStandardOvernight";
      this.gbxStandardOvernight.RoundCorners = 5;
      this.gbxStandardOvernight.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoStdNone, componentResourceManager.GetString("rdoStdNone.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoStdNone, (HelpNavigator) componentResourceManager.GetObject("rdoStdNone.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoStdNone, "rdoStdNone");
      this.rdoStdNone.Name = "rdoStdNone";
      this.helpProvider1.SetShowHelp((Control) this.rdoStdNone, (bool) componentResourceManager.GetObject("rdoStdNone.ShowHelp"));
      this.rdoStdNone.TabStop = true;
      this.rdoStdNone.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoStdDowngradeTo2Day, componentResourceManager.GetString("rdoStdDowngradeTo2Day.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoStdDowngradeTo2Day, (HelpNavigator) componentResourceManager.GetObject("rdoStdDowngradeTo2Day.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoStdDowngradeTo2Day, "rdoStdDowngradeTo2Day");
      this.rdoStdDowngradeTo2Day.Name = "rdoStdDowngradeTo2Day";
      this.helpProvider1.SetShowHelp((Control) this.rdoStdDowngradeTo2Day, (bool) componentResourceManager.GetObject("rdoStdDowngradeTo2Day.ShowHelp"));
      this.rdoStdDowngradeTo2Day.TabStop = true;
      this.rdoStdDowngradeTo2Day.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoStdUpgradeToPO, componentResourceManager.GetString("rdoStdUpgradeToPO.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoStdUpgradeToPO, (HelpNavigator) componentResourceManager.GetObject("rdoStdUpgradeToPO.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoStdUpgradeToPO, "rdoStdUpgradeToPO");
      this.rdoStdUpgradeToPO.Name = "rdoStdUpgradeToPO";
      this.helpProvider1.SetShowHelp((Control) this.rdoStdUpgradeToPO, (bool) componentResourceManager.GetObject("rdoStdUpgradeToPO.ShowHelp"));
      this.rdoStdUpgradeToPO.TabStop = true;
      this.rdoStdUpgradeToPO.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.gbxUpgradeDowngradePrefs);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (PassportUpgradeDowngradePreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.PassportUpgradeDowngradePreferences_Load);
      this.gbxUpgradeDowngradePrefs.ResumeLayout(false);
      this.gbxPO.ResumeLayout(false);
      this.gbxThurs.ResumeLayout(false);
      this.gbxStandardOvernight.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
