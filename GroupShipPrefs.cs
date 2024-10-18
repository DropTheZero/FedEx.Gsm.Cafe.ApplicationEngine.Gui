// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.GroupShipPrefs
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
  public class GroupShipPrefs : UserControlHelpEx
  {
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private ColorGroupBox gbxGroupShipOptions;
    private ColorGroupBox gbxFirstOvernightOptions;
    private ColorGroupBox gbxPriorityOvernightForSatDeliveryOptions;
    private ColorGroupBox gbxFedEx2DayForSatDeliveryOptions;
    private ColorGroupBox gbxStandardOvernightOptions;
    private ColorGroupBox gbxHomeDeliveryOptions;
    private RadioButton rdoFONone;
    private RadioButton rdoFODowngradeToPO;
    private RadioButton rdoFriNone;
    private RadioButton rdoFriDowngradeToPO;
    private RadioButton rdoThursNone;
    private RadioButton rdoThursDowngradeTo2Day;
    private RadioButton rdoThursUpgradeToPO;
    private RadioButton rdoStdNone;
    private RadioButton rdoStdDowngradeTo2Day;
    private RadioButton rdoStdUpgradeToPO;
    private RadioButton rdoHomeNone;
    private RadioButton rdoHome;

    public GroupShipPrefs()
    {
      this.InitializeComponent();
      this.gbxFirstOvernightOptions.Visible = false;
    }

    public void SetForOriginCountry(string countryCode)
    {
      switch (countryCode)
      {
        case "MX":
          this.gbxFedEx2DayForSatDeliveryOptions.Visible = false;
          this.gbxPriorityOvernightForSatDeliveryOptions.Visible = false;
          this.gbxHomeDeliveryOptions.Visible = false;
          this.gbxFirstOvernightOptions.Visible = true;
          this.rdoStdDowngradeTo2Day.Visible = false;
          break;
        case "CA":
          this.gbxStandardOvernightOptions.Visible = true;
          this.gbxFedEx2DayForSatDeliveryOptions.Visible = false;
          this.gbxHomeDeliveryOptions.Visible = false;
          this.gbxFirstOvernightOptions.Visible = true;
          break;
      }
    }

    public void ObjectToScreen(DShipDefl domShipDefl)
    {
      this.rdoStdUpgradeToPO.Checked = domShipDefl.UpgradeDowngradeOptions.StdOvernightUpgDwnGrdSelection == DShipDefl.StdOvernightOptions.UpgradeToPriorityOvernight;
      this.rdoStdDowngradeTo2Day.Checked = domShipDefl.UpgradeDowngradeOptions.StdOvernightUpgDwnGrdSelection == DShipDefl.StdOvernightOptions.DowngradeTo2Day;
      this.rdoStdNone.Checked = domShipDefl.UpgradeDowngradeOptions.StdOvernightUpgDwnGrdSelection == DShipDefl.StdOvernightOptions.None;
      this.rdoThursUpgradeToPO.Checked = domShipDefl.UpgradeDowngradeOptions.Thr2DayForSatDeliverySelection == DShipDefl.Thr2DayForSatDeliveryOptions.UpgradeToPriorityOvernight;
      this.rdoThursDowngradeTo2Day.Checked = domShipDefl.UpgradeDowngradeOptions.Thr2DayForSatDeliverySelection == DShipDefl.Thr2DayForSatDeliveryOptions.DowngradeTo2DayWithMondayDelivery;
      this.rdoThursNone.Checked = domShipDefl.UpgradeDowngradeOptions.Thr2DayForSatDeliverySelection == DShipDefl.Thr2DayForSatDeliveryOptions.None;
      this.rdoFriDowngradeToPO.Checked = domShipDefl.UpgradeDowngradeOptions.POForSatDeliverySelection == DShipDefl.POForSatDeliveryOptions.DowngradeToPriorityOverightWithMondayDelievery;
      this.rdoFriNone.Checked = domShipDefl.UpgradeDowngradeOptions.POForSatDeliverySelection == DShipDefl.POForSatDeliveryOptions.None;
      this.rdoFODowngradeToPO.Checked = domShipDefl.UpgradeDowngradeOptions.FOUpgradeDwnGrdSelection == DShipDefl.FOOptions.DowngradeToPriorityOvernight;
      this.rdoFONone.Checked = domShipDefl.UpgradeDowngradeOptions.FOUpgradeDwnGrdSelection == DShipDefl.FOOptions.None;
      this.rdoHome.Checked = domShipDefl.UpgradeDowngradeOptions.HomeDeliveryOptions == DShipDefl.HomeDeliveryGroupOptions.ChangeHomeDeliveryToGround;
      this.rdoHomeNone.Checked = domShipDefl.UpgradeDowngradeOptions.HomeDeliveryOptions == DShipDefl.HomeDeliveryGroupOptions.None;
    }

    public void ScreenToObject(DShipDefl domShipDefl)
    {
      domShipDefl.UpgradeDowngradeOptions.StdOvernightUpgDwnGrdSelection = !this.rdoStdUpgradeToPO.Checked ? (!this.rdoStdDowngradeTo2Day.Checked ? DShipDefl.StdOvernightOptions.None : DShipDefl.StdOvernightOptions.DowngradeTo2Day) : DShipDefl.StdOvernightOptions.UpgradeToPriorityOvernight;
      domShipDefl.UpgradeDowngradeOptions.Thr2DayForSatDeliverySelection = !this.rdoThursUpgradeToPO.Checked ? (!this.rdoThursDowngradeTo2Day.Checked ? DShipDefl.Thr2DayForSatDeliveryOptions.None : DShipDefl.Thr2DayForSatDeliveryOptions.DowngradeTo2DayWithMondayDelivery) : DShipDefl.Thr2DayForSatDeliveryOptions.UpgradeToPriorityOvernight;
      domShipDefl.UpgradeDowngradeOptions.POForSatDeliverySelection = !this.rdoFriDowngradeToPO.Checked ? DShipDefl.POForSatDeliveryOptions.None : DShipDefl.POForSatDeliveryOptions.DowngradeToPriorityOverightWithMondayDelievery;
      domShipDefl.UpgradeDowngradeOptions.FOUpgradeDwnGrdSelection = !this.rdoFODowngradeToPO.Checked ? DShipDefl.FOOptions.None : DShipDefl.FOOptions.DowngradeToPriorityOvernight;
      if (this.rdoHomeNone.Checked)
        domShipDefl.UpgradeDowngradeOptions.HomeDeliveryOptions = DShipDefl.HomeDeliveryGroupOptions.None;
      else
        domShipDefl.UpgradeDowngradeOptions.HomeDeliveryOptions = DShipDefl.HomeDeliveryGroupOptions.ChangeHomeDeliveryToGround;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GroupShipPrefs));
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.gbxGroupShipOptions = new ColorGroupBox();
      this.gbxHomeDeliveryOptions = new ColorGroupBox();
      this.rdoHomeNone = new RadioButton();
      this.rdoHome = new RadioButton();
      this.gbxFirstOvernightOptions = new ColorGroupBox();
      this.rdoFONone = new RadioButton();
      this.rdoFODowngradeToPO = new RadioButton();
      this.gbxPriorityOvernightForSatDeliveryOptions = new ColorGroupBox();
      this.rdoFriNone = new RadioButton();
      this.rdoFriDowngradeToPO = new RadioButton();
      this.gbxFedEx2DayForSatDeliveryOptions = new ColorGroupBox();
      this.rdoThursNone = new RadioButton();
      this.rdoThursDowngradeTo2Day = new RadioButton();
      this.rdoThursUpgradeToPO = new RadioButton();
      this.gbxStandardOvernightOptions = new ColorGroupBox();
      this.rdoStdNone = new RadioButton();
      this.rdoStdDowngradeTo2Day = new RadioButton();
      this.rdoStdUpgradeToPO = new RadioButton();
      this.tableLayoutPanel1.SuspendLayout();
      this.gbxGroupShipOptions.SuspendLayout();
      this.gbxHomeDeliveryOptions.SuspendLayout();
      this.gbxFirstOvernightOptions.SuspendLayout();
      this.gbxPriorityOvernightForSatDeliveryOptions.SuspendLayout();
      this.gbxFedEx2DayForSatDeliveryOptions.SuspendLayout();
      this.gbxStandardOvernightOptions.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.gbxGroupShipOptions, 0, 4);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.gbxGroupShipOptions.BorderThickness = 1f;
      this.gbxGroupShipOptions.Controls.Add((Control) this.gbxHomeDeliveryOptions);
      this.gbxGroupShipOptions.Controls.Add((Control) this.gbxFirstOvernightOptions);
      this.gbxGroupShipOptions.Controls.Add((Control) this.gbxPriorityOvernightForSatDeliveryOptions);
      this.gbxGroupShipOptions.Controls.Add((Control) this.gbxFedEx2DayForSatDeliveryOptions);
      this.gbxGroupShipOptions.Controls.Add((Control) this.gbxStandardOvernightOptions);
      this.gbxGroupShipOptions.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxGroupShipOptions, "gbxGroupShipOptions");
      this.gbxGroupShipOptions.Name = "gbxGroupShipOptions";
      this.gbxGroupShipOptions.RoundCorners = 5;
      this.gbxGroupShipOptions.TabStop = false;
      this.gbxHomeDeliveryOptions.BorderThickness = 1f;
      this.gbxHomeDeliveryOptions.Controls.Add((Control) this.rdoHomeNone);
      this.gbxHomeDeliveryOptions.Controls.Add((Control) this.rdoHome);
      this.gbxHomeDeliveryOptions.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxHomeDeliveryOptions, "gbxHomeDeliveryOptions");
      this.gbxHomeDeliveryOptions.Name = "gbxHomeDeliveryOptions";
      this.gbxHomeDeliveryOptions.RoundCorners = 5;
      this.gbxHomeDeliveryOptions.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoHomeNone, componentResourceManager.GetString("rdoHomeNone.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoHomeNone, (HelpNavigator) componentResourceManager.GetObject("rdoHomeNone.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoHomeNone, "rdoHomeNone");
      this.rdoHomeNone.Name = "rdoHomeNone";
      this.helpProvider1.SetShowHelp((Control) this.rdoHomeNone, (bool) componentResourceManager.GetObject("rdoHomeNone.ShowHelp"));
      this.rdoHomeNone.TabStop = true;
      this.rdoHomeNone.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoHome, componentResourceManager.GetString("rdoHome.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoHome, (HelpNavigator) componentResourceManager.GetObject("rdoHome.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoHome, "rdoHome");
      this.rdoHome.Name = "rdoHome";
      this.helpProvider1.SetShowHelp((Control) this.rdoHome, (bool) componentResourceManager.GetObject("rdoHome.ShowHelp"));
      this.rdoHome.TabStop = true;
      this.rdoHome.UseVisualStyleBackColor = true;
      this.gbxFirstOvernightOptions.BorderThickness = 1f;
      this.gbxFirstOvernightOptions.Controls.Add((Control) this.rdoFONone);
      this.gbxFirstOvernightOptions.Controls.Add((Control) this.rdoFODowngradeToPO);
      this.gbxFirstOvernightOptions.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxFirstOvernightOptions, "gbxFirstOvernightOptions");
      this.gbxFirstOvernightOptions.Name = "gbxFirstOvernightOptions";
      this.gbxFirstOvernightOptions.RoundCorners = 5;
      this.gbxFirstOvernightOptions.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoFONone, componentResourceManager.GetString("rdoFONone.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoFONone, (HelpNavigator) componentResourceManager.GetObject("rdoFONone.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoFONone, "rdoFONone");
      this.rdoFONone.Name = "rdoFONone";
      this.helpProvider1.SetShowHelp((Control) this.rdoFONone, (bool) componentResourceManager.GetObject("rdoFONone.ShowHelp"));
      this.rdoFONone.TabStop = true;
      this.rdoFONone.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoFODowngradeToPO, componentResourceManager.GetString("rdoFODowngradeToPO.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoFODowngradeToPO, (HelpNavigator) componentResourceManager.GetObject("rdoFODowngradeToPO.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoFODowngradeToPO, "rdoFODowngradeToPO");
      this.rdoFODowngradeToPO.Name = "rdoFODowngradeToPO";
      this.helpProvider1.SetShowHelp((Control) this.rdoFODowngradeToPO, (bool) componentResourceManager.GetObject("rdoFODowngradeToPO.ShowHelp"));
      this.rdoFODowngradeToPO.TabStop = true;
      this.rdoFODowngradeToPO.UseVisualStyleBackColor = true;
      this.gbxPriorityOvernightForSatDeliveryOptions.BorderThickness = 1f;
      this.gbxPriorityOvernightForSatDeliveryOptions.Controls.Add((Control) this.rdoFriNone);
      this.gbxPriorityOvernightForSatDeliveryOptions.Controls.Add((Control) this.rdoFriDowngradeToPO);
      this.gbxPriorityOvernightForSatDeliveryOptions.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxPriorityOvernightForSatDeliveryOptions, "gbxPriorityOvernightForSatDeliveryOptions");
      this.gbxPriorityOvernightForSatDeliveryOptions.Name = "gbxPriorityOvernightForSatDeliveryOptions";
      this.gbxPriorityOvernightForSatDeliveryOptions.RoundCorners = 5;
      this.gbxPriorityOvernightForSatDeliveryOptions.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoFriNone, componentResourceManager.GetString("rdoFriNone.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoFriNone, (HelpNavigator) componentResourceManager.GetObject("rdoFriNone.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoFriNone, "rdoFriNone");
      this.rdoFriNone.Name = "rdoFriNone";
      this.helpProvider1.SetShowHelp((Control) this.rdoFriNone, (bool) componentResourceManager.GetObject("rdoFriNone.ShowHelp"));
      this.rdoFriNone.TabStop = true;
      this.rdoFriNone.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoFriDowngradeToPO, componentResourceManager.GetString("rdoFriDowngradeToPO.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoFriDowngradeToPO, (HelpNavigator) componentResourceManager.GetObject("rdoFriDowngradeToPO.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoFriDowngradeToPO, "rdoFriDowngradeToPO");
      this.rdoFriDowngradeToPO.Name = "rdoFriDowngradeToPO";
      this.helpProvider1.SetShowHelp((Control) this.rdoFriDowngradeToPO, (bool) componentResourceManager.GetObject("rdoFriDowngradeToPO.ShowHelp"));
      this.rdoFriDowngradeToPO.TabStop = true;
      this.rdoFriDowngradeToPO.UseVisualStyleBackColor = true;
      this.gbxFedEx2DayForSatDeliveryOptions.BorderThickness = 1f;
      this.gbxFedEx2DayForSatDeliveryOptions.Controls.Add((Control) this.rdoThursNone);
      this.gbxFedEx2DayForSatDeliveryOptions.Controls.Add((Control) this.rdoThursDowngradeTo2Day);
      this.gbxFedEx2DayForSatDeliveryOptions.Controls.Add((Control) this.rdoThursUpgradeToPO);
      this.gbxFedEx2DayForSatDeliveryOptions.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxFedEx2DayForSatDeliveryOptions, "gbxFedEx2DayForSatDeliveryOptions");
      this.gbxFedEx2DayForSatDeliveryOptions.Name = "gbxFedEx2DayForSatDeliveryOptions";
      this.gbxFedEx2DayForSatDeliveryOptions.RoundCorners = 5;
      this.gbxFedEx2DayForSatDeliveryOptions.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoThursNone, componentResourceManager.GetString("rdoThursNone.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoThursNone, (HelpNavigator) componentResourceManager.GetObject("rdoThursNone.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoThursNone, "rdoThursNone");
      this.rdoThursNone.Name = "rdoThursNone";
      this.helpProvider1.SetShowHelp((Control) this.rdoThursNone, (bool) componentResourceManager.GetObject("rdoThursNone.ShowHelp"));
      this.rdoThursNone.TabStop = true;
      this.rdoThursNone.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoThursDowngradeTo2Day, componentResourceManager.GetString("rdoThursDowngradeTo2Day.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoThursDowngradeTo2Day, (HelpNavigator) componentResourceManager.GetObject("rdoThursDowngradeTo2Day.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoThursDowngradeTo2Day, "rdoThursDowngradeTo2Day");
      this.rdoThursDowngradeTo2Day.Name = "rdoThursDowngradeTo2Day";
      this.helpProvider1.SetShowHelp((Control) this.rdoThursDowngradeTo2Day, (bool) componentResourceManager.GetObject("rdoThursDowngradeTo2Day.ShowHelp"));
      this.rdoThursDowngradeTo2Day.TabStop = true;
      this.rdoThursDowngradeTo2Day.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoThursUpgradeToPO, componentResourceManager.GetString("rdoThursUpgradeToPO.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoThursUpgradeToPO, (HelpNavigator) componentResourceManager.GetObject("rdoThursUpgradeToPO.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoThursUpgradeToPO, "rdoThursUpgradeToPO");
      this.rdoThursUpgradeToPO.Name = "rdoThursUpgradeToPO";
      this.helpProvider1.SetShowHelp((Control) this.rdoThursUpgradeToPO, (bool) componentResourceManager.GetObject("rdoThursUpgradeToPO.ShowHelp"));
      this.rdoThursUpgradeToPO.TabStop = true;
      this.rdoThursUpgradeToPO.UseVisualStyleBackColor = true;
      this.gbxStandardOvernightOptions.BorderThickness = 1f;
      this.gbxStandardOvernightOptions.Controls.Add((Control) this.rdoStdNone);
      this.gbxStandardOvernightOptions.Controls.Add((Control) this.rdoStdDowngradeTo2Day);
      this.gbxStandardOvernightOptions.Controls.Add((Control) this.rdoStdUpgradeToPO);
      this.gbxStandardOvernightOptions.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxStandardOvernightOptions, "gbxStandardOvernightOptions");
      this.gbxStandardOvernightOptions.Name = "gbxStandardOvernightOptions";
      this.gbxStandardOvernightOptions.RoundCorners = 5;
      this.gbxStandardOvernightOptions.TabStop = false;
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
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (GroupShipPrefs);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.tableLayoutPanel1.ResumeLayout(false);
      this.gbxGroupShipOptions.ResumeLayout(false);
      this.gbxHomeDeliveryOptions.ResumeLayout(false);
      this.gbxFirstOvernightOptions.ResumeLayout(false);
      this.gbxPriorityOvernightForSatDeliveryOptions.ResumeLayout(false);
      this.gbxFedEx2DayForSatDeliveryOptions.ResumeLayout(false);
      this.gbxStandardOvernightOptions.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
