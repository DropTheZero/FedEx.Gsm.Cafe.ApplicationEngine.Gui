// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SmartPostConfiguration.SmartPostConfigurationUserControl
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.SmartPostConfiguration
{
  public class SmartPostConfigurationUserControl : UserControlHelpEx
  {
    private Account _account;
    private const int MINPURGE_MIN = 30;
    private const int MINPURGE_DEFAULT = 120;
    private const int MAXPURGE_DEFAULT = 365;
    private bool hubIdReset;
    private static readonly TimeSpan AutocloseMinTime = new TimeSpan(0, 1, 0);
    private static readonly TimeSpan AutocloseMaxTime = new TimeSpan(23, 59, 59);
    private IContainer components;
    private CheckBox chkSmartPostEnabled;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private GroupBox gbxOptions;
    private GroupBox gbxPostalClasses;
    private Panel panel1;
    private TextBox ConfirmationNumCurrent;
    private TextBox ConfirmationNumEnd;
    private TextBox ConfirmationNumStart;
    private FdxMaskedEdit DistributionCenterTextBox;
    private FdxMaskedEdit DUNSTextBox;
    private FdxMaskedEdit CustomerIDTextBox;
    private CheckBox MediaMailCheckBox;
    private CheckBox BoundPrinterMatterCheckBox;
    private CheckBox StandardBCheckBox;
    private CheckBox StandardMailCheckBox;
    private FolderBrowserDialog folderBrowserDialog1;
    private OpenFileDialog openFileDialog1;
    private Label label12;
    private ComboBox HUB_ID_comboBox1;
    private GroupBox gbxSpecialServices;
    private Label lblPickupCarrier;
    private ComboBox cboPickupCarrier;
    private CheckBox chkNoDeliveryConfirmation;

    private Account CurrentAccount => this._account;

    private bool SmartPostEnabled
    {
      get => this.CurrentAccount != null && this.CurrentAccount.IsSmartPostEnabled;
    }

    public SmartPostConfigurationUserControl() => this.InitializeComponent();

    public SmartPostConfigurationUserControl(Account account)
    {
      this.InitializeComponent();
      this.SetupAccountSettings(account);
    }

    private void SetupAccountSettings(Account account)
    {
      if (account == null)
        throw new ArgumentNullException(nameof (account));
      if (string.IsNullOrEmpty(account.AccountNumber))
        throw new ArgumentException("AccountNumber cannot be null or empty");
      this._account = !string.IsNullOrEmpty(account.MeterNumber) ? account : throw new ArgumentException("MeterNumber cannot be null or empty");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    internal void SetupUpDialog(Account account)
    {
      this.SetupAccountSettings(account);
      this.chkSmartPostEnabled.Checked = account.IsSmartPostEnabled;
      this.ObjectToScreen(account);
    }

    private void ObjectToScreen(Account account)
    {
      this.DistributionCenterTextBox.Text = account.SPDistributionCtr;
      this.DUNSTextBox.Text = account.SPDunsNumber;
      this.CustomerIDTextBox.Text = account.SPCustomerID;
      this.MediaMailCheckBox.Checked = account.SPMediaMailEnabled;
      this.BoundPrinterMatterCheckBox.Checked = account.SPBoundPrintedMatterEnabled;
      this.StandardBCheckBox.Checked = account.SPParcelPostEnabled || account.SPStandardBEnabled;
      this.StandardMailCheckBox.Checked = account.SPStandardAEnabled;
      this.HUB_ID_comboBox1.DataSource = (object) this.PopulateHubIdCombo(account);
      this.HUB_ID_comboBox1.DisplayMember = "CodeDescription";
      this.HUB_ID_comboBox1.ValueMember = "Code";
      this.PopulateCombos();
      if (account.SPPickupCarrier == Shipment.CarrierType.SmartPost)
        this.cboPickupCarrier.SelectedValue = (object) 1;
      else if (account.SPPickupCarrier == Shipment.CarrierType.Ground)
        this.cboPickupCarrier.SelectedValue = (object) 2;
      this.chkNoDeliveryConfirmation.Checked = account.DisableSmartPostDeliveryConfirmation;
      TrackingNumber output;
      if (GuiData.AppController.ShipEngine.Retrieve<TrackingNumber>(new TrackingNumber()
      {
        MeterNumber = account.MeterNumber,
        FedExAcctNbr = account.AccountNumber,
        CarrierType = Shipment.CarrierType.SmartPost,
        CurrentCodes = TrackingNumber.TrackingNumberRangeStatus.Current,
        HasIMPBFormat = true
      }, out output, out Error _) != 1)
        return;
      this.ConfirmationNumCurrent.Text = output.NextTrackingNumber;
      this.ConfirmationNumEnd.Text = output.LastTrackingNumber;
      this.ConfirmationNumStart.Text = output.FirstTrackingNumber;
    }

    private DataTable PopulateHubIdCombo(Account settings)
    {
      DataTable dataTable = new DataTable();
      DataColumn dataColumn = dataTable.Columns.Add("Code", typeof (string));
      dataTable.Columns.Add("Description", typeof (string));
      dataTable.Columns.Add("CodeDescription", typeof (string), "Code + ' - ' + Description");
      dataTable.PrimaryKey = new DataColumn[1]{ dataColumn };
      if (settings != null && settings.ListSmartPostHub != null)
      {
        foreach (SmartPostHub smartPostHub in settings.ListSmartPostHub)
        {
          try
          {
            dataTable.Rows.Add((object) smartPostHub.HubId, (object) smartPostHub.HubCity);
          }
          catch (Exception ex)
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "SmartPostConfigurationUserControl.PopulateHubIdCombo", "Error adding hub " + smartPostHub.HubId + ": " + ex.ToString());
          }
        }
      }
      return dataTable;
    }

    protected override void OnLoad(EventArgs e)
    {
      if (!this.DesignMode)
      {
        this.PopulateCombos();
        this.ResetHubIdCombo();
        this.SetupUpDialog(this.CurrentAccount);
      }
      base.OnLoad(e);
    }

    private void ResetHubIdCombo()
    {
      if (this.hubIdReset)
        return;
      Point screen = this.HUB_ID_comboBox1.Parent.PointToScreen(this.HUB_ID_comboBox1.Location);
      this.HUB_ID_comboBox1.Parent = (Control) this;
      this.HUB_ID_comboBox1.Location = this.PointToClient(screen);
      this.HUB_ID_comboBox1.BringToFront();
      this.hubIdReset = true;
    }

    private void PopulateCombos()
    {
      Utility.SetDisplayAndValue(this.cboPickupCarrier, Utility.GetDataTable(Utility.ListTypes.SmartPostCarriers), "Description", "Code", false);
    }

    public static int ValidateSettings(Account account, out string message)
    {
      message = string.Empty;
      DateTime minValue = DateTime.MinValue;
      if (!account.SPAutoCloseEnabled || SmartPostConfigurationUserControl.ValidateAutoCloseTime(account.SPAutoCloseTime))
        return 1;
      message = GuiData.Languafier.TranslateError(9564);
      return 9564;
    }

    private static bool ValidateAutoCloseTime(DateTime autoclose)
    {
      return autoclose.TimeOfDay >= SmartPostConfigurationUserControl.AutocloseMinTime && autoclose.TimeOfDay <= SmartPostConfigurationUserControl.AutocloseMaxTime;
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SmartPostConfigurationUserControl));
      this.chkSmartPostEnabled = new CheckBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.label5 = new Label();
      this.label6 = new Label();
      this.label7 = new Label();
      this.gbxOptions = new GroupBox();
      this.lblPickupCarrier = new Label();
      this.cboPickupCarrier = new ComboBox();
      this.HUB_ID_comboBox1 = new ComboBox();
      this.label12 = new Label();
      this.ConfirmationNumCurrent = new TextBox();
      this.ConfirmationNumEnd = new TextBox();
      this.ConfirmationNumStart = new TextBox();
      this.DistributionCenterTextBox = new FdxMaskedEdit();
      this.DUNSTextBox = new FdxMaskedEdit();
      this.CustomerIDTextBox = new FdxMaskedEdit();
      this.gbxPostalClasses = new GroupBox();
      this.MediaMailCheckBox = new CheckBox();
      this.BoundPrinterMatterCheckBox = new CheckBox();
      this.StandardBCheckBox = new CheckBox();
      this.StandardMailCheckBox = new CheckBox();
      this.panel1 = new Panel();
      this.gbxSpecialServices = new GroupBox();
      this.chkNoDeliveryConfirmation = new CheckBox();
      this.folderBrowserDialog1 = new FolderBrowserDialog();
      this.openFileDialog1 = new OpenFileDialog();
      this.gbxOptions.SuspendLayout();
      this.gbxPostalClasses.SuspendLayout();
      this.panel1.SuspendLayout();
      this.gbxSpecialServices.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      this.chkSmartPostEnabled.AutoCheck = false;
      componentResourceManager.ApplyResources((object) this.chkSmartPostEnabled, "chkSmartPostEnabled");
      this.helpProvider1.SetHelpKeyword((Control) this.chkSmartPostEnabled, componentResourceManager.GetString("chkSmartPostEnabled.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkSmartPostEnabled, (HelpNavigator) componentResourceManager.GetObject("chkSmartPostEnabled.HelpNavigator"));
      this.chkSmartPostEnabled.Name = "chkSmartPostEnabled";
      this.helpProvider1.SetShowHelp((Control) this.chkSmartPostEnabled, (bool) componentResourceManager.GetObject("chkSmartPostEnabled.ShowHelp"));
      this.chkSmartPostEnabled.UseVisualStyleBackColor = true;
      this.label1.AccessibleRole = AccessibleRole.TitleBar;
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      componentResourceManager.ApplyResources((object) this.label3, "label3");
      this.label3.Name = "label3";
      componentResourceManager.ApplyResources((object) this.label4, "label4");
      this.label4.Name = "label4";
      componentResourceManager.ApplyResources((object) this.label5, "label5");
      this.label5.Name = "label5";
      componentResourceManager.ApplyResources((object) this.label6, "label6");
      this.label6.Name = "label6";
      componentResourceManager.ApplyResources((object) this.label7, "label7");
      this.label7.Name = "label7";
      this.gbxOptions.Controls.Add((Control) this.lblPickupCarrier);
      this.gbxOptions.Controls.Add((Control) this.cboPickupCarrier);
      this.gbxOptions.Controls.Add((Control) this.HUB_ID_comboBox1);
      this.gbxOptions.Controls.Add((Control) this.label12);
      this.gbxOptions.Controls.Add((Control) this.ConfirmationNumCurrent);
      this.gbxOptions.Controls.Add((Control) this.ConfirmationNumEnd);
      this.gbxOptions.Controls.Add((Control) this.ConfirmationNumStart);
      this.gbxOptions.Controls.Add((Control) this.DistributionCenterTextBox);
      this.gbxOptions.Controls.Add((Control) this.DUNSTextBox);
      this.gbxOptions.Controls.Add((Control) this.CustomerIDTextBox);
      this.gbxOptions.Controls.Add((Control) this.label1);
      this.gbxOptions.Controls.Add((Control) this.label7);
      this.gbxOptions.Controls.Add((Control) this.label2);
      this.gbxOptions.Controls.Add((Control) this.label6);
      this.gbxOptions.Controls.Add((Control) this.label3);
      this.gbxOptions.Controls.Add((Control) this.label5);
      this.gbxOptions.Controls.Add((Control) this.label4);
      componentResourceManager.ApplyResources((object) this.gbxOptions, "gbxOptions");
      this.gbxOptions.Name = "gbxOptions";
      this.gbxOptions.TabStop = false;
      componentResourceManager.ApplyResources((object) this.lblPickupCarrier, "lblPickupCarrier");
      this.lblPickupCarrier.Name = "lblPickupCarrier";
      this.cboPickupCarrier.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboPickupCarrier.FormattingEnabled = true;
      componentResourceManager.ApplyResources((object) this.cboPickupCarrier, "cboPickupCarrier");
      this.cboPickupCarrier.Name = "cboPickupCarrier";
      this.HUB_ID_comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.HUB_ID_comboBox1.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.HUB_ID_comboBox1, componentResourceManager.GetString("HUB_ID_comboBox1.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.HUB_ID_comboBox1, (HelpNavigator) componentResourceManager.GetObject("HUB_ID_comboBox1.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.HUB_ID_comboBox1, "HUB_ID_comboBox1");
      this.HUB_ID_comboBox1.Name = "HUB_ID_comboBox1";
      this.helpProvider1.SetShowHelp((Control) this.HUB_ID_comboBox1, (bool) componentResourceManager.GetObject("HUB_ID_comboBox1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label12, "label12");
      this.label12.Name = "label12";
      this.helpProvider1.SetShowHelp((Control) this.label12, (bool) componentResourceManager.GetObject("label12.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.ConfirmationNumCurrent, componentResourceManager.GetString("ConfirmationNumCurrent.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.ConfirmationNumCurrent, (HelpNavigator) componentResourceManager.GetObject("ConfirmationNumCurrent.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.ConfirmationNumCurrent, "ConfirmationNumCurrent");
      this.ConfirmationNumCurrent.Name = "ConfirmationNumCurrent";
      this.helpProvider1.SetShowHelp((Control) this.ConfirmationNumCurrent, (bool) componentResourceManager.GetObject("ConfirmationNumCurrent.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.ConfirmationNumEnd, componentResourceManager.GetString("ConfirmationNumEnd.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.ConfirmationNumEnd, (HelpNavigator) componentResourceManager.GetObject("ConfirmationNumEnd.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.ConfirmationNumEnd, "ConfirmationNumEnd");
      this.ConfirmationNumEnd.Name = "ConfirmationNumEnd";
      this.helpProvider1.SetShowHelp((Control) this.ConfirmationNumEnd, (bool) componentResourceManager.GetObject("ConfirmationNumEnd.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.ConfirmationNumStart, componentResourceManager.GetString("ConfirmationNumStart.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.ConfirmationNumStart, (HelpNavigator) componentResourceManager.GetObject("ConfirmationNumStart.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.ConfirmationNumStart, "ConfirmationNumStart");
      this.ConfirmationNumStart.Name = "ConfirmationNumStart";
      this.helpProvider1.SetShowHelp((Control) this.ConfirmationNumStart, (bool) componentResourceManager.GetObject("ConfirmationNumStart.ShowHelp"));
      this.DistributionCenterTextBox.Allow = "";
      this.DistributionCenterTextBox.Disallow = "";
      this.DistributionCenterTextBox.eMask = eMasks.maskCustom;
      this.DistributionCenterTextBox.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.DistributionCenterTextBox, componentResourceManager.GetString("DistributionCenterTextBox.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.DistributionCenterTextBox, (HelpNavigator) componentResourceManager.GetObject("DistributionCenterTextBox.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.DistributionCenterTextBox, "DistributionCenterTextBox");
      this.DistributionCenterTextBox.Mask = "99";
      this.DistributionCenterTextBox.Name = "DistributionCenterTextBox";
      this.helpProvider1.SetShowHelp((Control) this.DistributionCenterTextBox, (bool) componentResourceManager.GetObject("DistributionCenterTextBox.ShowHelp"));
      this.DUNSTextBox.Allow = "";
      this.DUNSTextBox.Disallow = "";
      this.DUNSTextBox.eMask = eMasks.maskCustom;
      this.DUNSTextBox.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.DUNSTextBox, componentResourceManager.GetString("DUNSTextBox.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.DUNSTextBox, (HelpNavigator) componentResourceManager.GetObject("DUNSTextBox.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.DUNSTextBox, "DUNSTextBox");
      this.DUNSTextBox.Mask = "999999999";
      this.DUNSTextBox.Name = "DUNSTextBox";
      this.helpProvider1.SetShowHelp((Control) this.DUNSTextBox, (bool) componentResourceManager.GetObject("DUNSTextBox.ShowHelp"));
      this.CustomerIDTextBox.Allow = "";
      this.CustomerIDTextBox.Disallow = "";
      this.CustomerIDTextBox.eMask = eMasks.maskCustom;
      this.CustomerIDTextBox.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.CustomerIDTextBox, componentResourceManager.GetString("CustomerIDTextBox.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.CustomerIDTextBox, (HelpNavigator) componentResourceManager.GetObject("CustomerIDTextBox.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.CustomerIDTextBox, "CustomerIDTextBox");
      this.CustomerIDTextBox.Mask = "AAAAA";
      this.CustomerIDTextBox.Name = "CustomerIDTextBox";
      this.helpProvider1.SetShowHelp((Control) this.CustomerIDTextBox, (bool) componentResourceManager.GetObject("CustomerIDTextBox.ShowHelp"));
      this.gbxPostalClasses.Controls.Add((Control) this.MediaMailCheckBox);
      this.gbxPostalClasses.Controls.Add((Control) this.BoundPrinterMatterCheckBox);
      this.gbxPostalClasses.Controls.Add((Control) this.StandardBCheckBox);
      this.gbxPostalClasses.Controls.Add((Control) this.StandardMailCheckBox);
      componentResourceManager.ApplyResources((object) this.gbxPostalClasses, "gbxPostalClasses");
      this.gbxPostalClasses.Name = "gbxPostalClasses";
      this.gbxPostalClasses.TabStop = false;
      componentResourceManager.ApplyResources((object) this.MediaMailCheckBox, "MediaMailCheckBox");
      this.helpProvider1.SetHelpKeyword((Control) this.MediaMailCheckBox, componentResourceManager.GetString("MediaMailCheckBox.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.MediaMailCheckBox, (HelpNavigator) componentResourceManager.GetObject("MediaMailCheckBox.HelpNavigator"));
      this.MediaMailCheckBox.Name = "MediaMailCheckBox";
      this.helpProvider1.SetShowHelp((Control) this.MediaMailCheckBox, (bool) componentResourceManager.GetObject("MediaMailCheckBox.ShowHelp"));
      this.MediaMailCheckBox.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.BoundPrinterMatterCheckBox, "BoundPrinterMatterCheckBox");
      this.helpProvider1.SetHelpKeyword((Control) this.BoundPrinterMatterCheckBox, componentResourceManager.GetString("BoundPrinterMatterCheckBox.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.BoundPrinterMatterCheckBox, (HelpNavigator) componentResourceManager.GetObject("BoundPrinterMatterCheckBox.HelpNavigator"));
      this.BoundPrinterMatterCheckBox.Name = "BoundPrinterMatterCheckBox";
      this.helpProvider1.SetShowHelp((Control) this.BoundPrinterMatterCheckBox, (bool) componentResourceManager.GetObject("BoundPrinterMatterCheckBox.ShowHelp"));
      this.BoundPrinterMatterCheckBox.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.StandardBCheckBox, componentResourceManager.GetString("StandardBCheckBox.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.StandardBCheckBox, (HelpNavigator) componentResourceManager.GetObject("StandardBCheckBox.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.StandardBCheckBox, "StandardBCheckBox");
      this.StandardBCheckBox.Name = "StandardBCheckBox";
      this.helpProvider1.SetShowHelp((Control) this.StandardBCheckBox, (bool) componentResourceManager.GetObject("StandardBCheckBox.ShowHelp"));
      this.StandardBCheckBox.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.StandardMailCheckBox, componentResourceManager.GetString("StandardMailCheckBox.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.StandardMailCheckBox, (HelpNavigator) componentResourceManager.GetObject("StandardMailCheckBox.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.StandardMailCheckBox, "StandardMailCheckBox");
      this.StandardMailCheckBox.Name = "StandardMailCheckBox";
      this.helpProvider1.SetShowHelp((Control) this.StandardMailCheckBox, (bool) componentResourceManager.GetObject("StandardMailCheckBox.ShowHelp"));
      this.StandardMailCheckBox.UseVisualStyleBackColor = true;
      this.panel1.AllowDrop = true;
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.BorderStyle = BorderStyle.Fixed3D;
      this.panel1.Controls.Add((Control) this.gbxSpecialServices);
      this.panel1.Controls.Add((Control) this.gbxPostalClasses);
      this.panel1.Controls.Add((Control) this.gbxOptions);
      this.panel1.Controls.Add((Control) this.chkSmartPostEnabled);
      this.panel1.Name = "panel1";
      this.gbxSpecialServices.Controls.Add((Control) this.chkNoDeliveryConfirmation);
      componentResourceManager.ApplyResources((object) this.gbxSpecialServices, "gbxSpecialServices");
      this.gbxSpecialServices.Name = "gbxSpecialServices";
      this.gbxSpecialServices.TabStop = false;
      componentResourceManager.ApplyResources((object) this.chkNoDeliveryConfirmation, "chkNoDeliveryConfirmation");
      this.chkNoDeliveryConfirmation.Name = "chkNoDeliveryConfirmation";
      this.chkNoDeliveryConfirmation.UseVisualStyleBackColor = true;
      this.folderBrowserDialog1.ShowNewFolderButton = false;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.panel1);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (SmartPostConfigurationUserControl);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.gbxOptions.ResumeLayout(false);
      this.gbxOptions.PerformLayout();
      this.gbxPostalClasses.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.gbxSpecialServices.ResumeLayout(false);
      this.gbxSpecialServices.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
