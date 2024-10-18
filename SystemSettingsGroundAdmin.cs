// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SystemSettingsGroundAdmin
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.SmartPostConfiguration;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.DataAccess;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class SystemSettingsGroundAdmin : UserControlHelpEx
  {
    private const int EpicGroundAccountThreshold = 9999999;
    private bool _sendTransaction;
    private bool _bEventsInitialized;
    private bool _bInsideObjectsToScreen;
    private SystemSettings _myParent;
    private IContainer components;
    private CheckBox chkEnableGroundShipping;
    private ColorGroupBox colorGroupBox26;
    private TableLayoutPanel tableLayoutPanel5;
    private ColorGroupBox colorGroupBox1;
    private Button btnEnableSmartPost;
    private ColorGroupBox colorGroupBox29;
    private ColorGroupBox colorGroupBox27;
    private Label label15;
    private TextBox edtAddress2;
    private Label label13;
    private TextBox edtAddress1;
    private Label label12;
    private TextBox edtCompany;
    private Label label11;
    private TextBox edtGroundAccountNum;
    private FdxMaskedEdit edtZipPostal;
    private TextBox edtStateProvince;
    private TextBox edtCity;
    private CheckBox chkHomeDelivery;
    private CheckBox chkCommercial;
    private CheckBox chkResidential;
    private ColorGroupBox colorGroupBox3;
    private CheckBox chkEnableAutomaticClose;
    private CheckBox chkEpdiBanner;
    private CheckBox chkEnableCloseMultiweightOptimization;
    private CheckBox chkCollect;
    private Panel panel1;
    private Button btnClearAccountNum;
    private ColorGroupBox colorGroupBox28;
    private CheckBox chkAOD;
    private ColorGroupBox grpIGDD;
    private CheckBox chkIGDDShipper;
    private FocusExtender focusExtender1;
    private CheckBox chkNeverDownloadGroundDiscounts;
    private FlowLayoutPanel flpButtons;

    public bool SendTransaction
    {
      get => this._sendTransaction;
      set => this._sendTransaction = value;
    }

    internal SystemSettings MyParent
    {
      get => this._myParent;
      set => this._myParent = value;
    }

    private bool EpicGroundOperationalReadiness => true;

    private bool HasEpicAccountNumber => this.IsAccountNumberEpic(this.edtGroundAccountNum.Text);

    internal bool IsAccountNumberEpic(string account)
    {
      int result;
      return int.TryParse(account, out result) && result > 9999999;
    }

    internal bool GroundEnabled => this.chkEnableGroundShipping.Checked;

    internal bool FedEx1DBarcodeEnabled => true;

    private bool HasActiveSmartpostShipments
    {
      get
      {
        if (this.MyParent.Settings == null || this.MyParent.Settings.AccountObject == null)
          return false;
        List<GsmFilter> filterList = new List<GsmFilter>();
        filterList.Add(new GsmFilter("CarrierType", "=", (object) "6"));
        filterList.Add(new GsmFilter("ClosedStatus", "=", (object) Shipment.ClosedStatus.Open.ToString("d")));
        filterList.Add(new GsmFilter("DeletedInd", "=", (object) "0"));
        filterList.Add(new GsmFilter("AccountNumber", "=", (object) this.MyParent.Settings.AccountObject.AccountNumber));
        filterList.Add(new GsmFilter("MeterNumber", "=", (object) this.MyParent.Settings.AccountObject.MeterNumber));
        filterList.Add(new GsmFilter("IsMasterPackage", "=", (object) true));
        Error error = new Error();
        DataTable output;
        return GuiData.AppController.ShipEngine.GetDataList((object) null, GsmDataAccess.ListSpecification.DomesticShippedList, out output, filterList, (List<GsmSort>) null, (List<string>) null, error) == 1 && output != null && output.Rows.Count != 0;
      }
    }

    public SystemSettingsGroundAdmin()
    {
      this.InitializeComponent();
      this.grpIGDD.Visible = false;
      this.chkIGDDShipper.Visible = false;
    }

    public void OnSystemSettingsCountryChanged(object sender, CountryChangedEventArgs args)
    {
      this.ShowHomeDelivery(args.CountryCode);
      this.EnableGroundCheckBox(this.MyParent.Settings.AccountObject);
      if (args.CountryCode == "US")
        this.edtZipPostal.SetMask(eMasks.maskZipCodeNine);
      else if (args.CountryCode == "CA")
        this.edtZipPostal.SetMask(eMasks.maskCanadianZipCode);
      else if (args.CountryCode == "MX")
        this.edtZipPostal.SetMask(eMasks.maskMXZipCode);
      else if (args.CountryCode == "BR")
        this.edtZipPostal.SetMask(eMasks.maskBRZipCode);
      else
        this.edtZipPostal.SetMask(eMasks.maskIntlZipCode);
      this.grpIGDD.Visible = args.CountryCode == "US" && !this.MyParent.Settings.AccountObject.NIAGARA2_Initiative_Enabled;
      this.chkIGDDShipper.Visible = args.CountryCode == "US" && !this.MyParent.Settings.AccountObject.NIAGARA2_Initiative_Enabled;
      this.btnEnableSmartPost.Enabled = args.CountryCode == "US" && this.MyParent.Settings.AccountObject.IsSmartPostEnabled && this.MyParent.Operation == Utility.FormOperation.ViewEdit;
    }

    public void ScreenToObjects(SystemSettingsObject settings)
    {
      if (settings.AccountObject != null)
      {
        if (settings.AccountObject.IsGroundEnabled != this.chkEnableGroundShipping.Checked || settings.AccountObject.PrintEPDIBanner != this.chkEpdiBanner.Checked || settings.AccountObject.CollectBillingOption != this.chkCollect.Checked)
          this._sendTransaction = true;
        settings.AccountObject.IsGroundEnabled = this.chkEnableGroundShipping.Checked;
        if (this.chkEnableGroundShipping.Checked)
          settings.AccountObject.IsGroundEverEnabled = true;
        settings.AccountObject.GroundAccountNumber = this.edtGroundAccountNum.Text;
        if (settings.AccountObject.GroundAddress == null)
          settings.AccountObject.GroundAddress = new Address();
        settings.AccountObject.GroundAddress.Company = this.edtCompany.Text;
        settings.AccountObject.GroundAddress.Addr1 = this.edtAddress1.Text;
        settings.AccountObject.GroundAddress.Addr2 = this.edtAddress2.Text;
        settings.AccountObject.PrintEPDIBanner = this.chkEpdiBanner.Checked;
        settings.AccountObject.IsGroundAutoCloseEnabled = this.chkEnableAutomaticClose.Checked;
        settings.AccountObject.CollectBillingOption = this.chkCollect.Checked;
        if (!this.MyParent.Settings.AccountObject.NIAGARA2_Initiative_Enabled)
          settings.AccountObject.IGDDShipper = this.chkIGDDShipper.Checked;
        if (settings.AccountObject.GroundServices == null)
          settings.AccountObject.GroundServices = new GroundServices();
        settings.AccountObject.GroundServices.Residential = this.chkResidential.Checked;
        settings.AccountObject.GroundServices.Commercial = this.chkCommercial.Checked;
        settings.AccountObject.GroundServices.HomeDelivery = this.chkHomeDelivery.Checked;
        if (settings.AccountObject.GroundSpecialServices == null)
          settings.AccountObject.GroundSpecialServices = new GroundSpecialServices();
        settings.AccountObject.GroundSpecialServices.AckOfDelivery = this.chkAOD.Checked;
      }
      if (settings.RegistrySettings == null)
        return;
      settings.RegistrySettings["MWEODOptimization"].Value = this.chkEnableCloseMultiweightOptimization.Checked ? (object) "Y" : (object) "N";
      settings.RegistrySettings["NeverDownloadGroundDiscounts"].Value = (object) "N";
    }

    public int ObjectsToScreen(SystemSettingsObject settings, Utility.FormOperation op)
    {
      this._bInsideObjectsToScreen = true;
      if (this.MyParent != null)
      {
        string country = this.MyParent.ExpressAdminPage.Country;
        this.grpIGDD.Visible = country == "US" && !this.MyParent.Settings.AccountObject.NIAGARA2_Initiative_Enabled;
        this.chkIGDDShipper.Visible = country == "US" && !this.MyParent.Settings.AccountObject.NIAGARA2_Initiative_Enabled;
        this.EnableGroundCheckBox(settings.AccountObject);
      }
      if (settings.AccountObject != null)
      {
        if (!settings.AccountObject.IsGroundEverEnabled)
        {
          this.chkResidential.Checked = true;
          this.chkCommercial.Checked = true;
          if (this.chkHomeDelivery.Visible)
            this.chkHomeDelivery.Checked = true;
          this.chkAOD.Checked = true;
          this.chkEnableAutomaticClose.Checked = true;
          this.chkCollect.Checked = true;
        }
        else
        {
          this.chkCollect.Checked = settings.AccountObject.CollectBillingOption;
          this.chkEnableAutomaticClose.Checked = settings.AccountObject.IsGroundAutoCloseEnabled;
          if (settings.AccountObject.GroundServices != null)
          {
            this.chkIGDDShipper.Checked = settings.AccountObject.IGDDShipper;
            this.chkResidential.Checked = settings.AccountObject.GroundServices.Residential;
            this.chkCommercial.Checked = settings.AccountObject.GroundServices.Commercial;
            this.chkHomeDelivery.Checked = settings.AccountObject.GroundServices.HomeDelivery;
          }
          if (settings.AccountObject.GroundSpecialServices != null)
            this.chkAOD.Checked = settings.AccountObject.GroundSpecialServices.AckOfDelivery;
        }
        if (settings.AccountObject.Address != null)
        {
          if (settings.AccountObject.GroundAddress != null)
          {
            this.edtCompany.Text = settings.AccountObject.GroundAddress.Company;
            this.edtAddress1.Text = settings.AccountObject.GroundAddress.Addr1;
            this.edtAddress2.Text = settings.AccountObject.GroundAddress.Addr2;
          }
          this.edtCity.Text = settings.AccountObject.Address.City;
          this.edtStateProvince.Text = settings.AccountObject.Address.StateProvince;
          this.edtZipPostal.Text = settings.AccountObject.Address.PostalCode;
        }
        this.edtGroundAccountNum.Text = settings.AccountObject.GroundAccountNumber;
        this.chkEpdiBanner.Checked = settings.AccountObject.PrintEPDIBanner;
        this.btnEnableSmartPost.Enabled = Utility.IsUS50(settings.AccountObject.Address) && this.MyParent.Settings.AccountObject.IsSmartPostEnabled && this.MyParent.Operation == Utility.FormOperation.ViewEdit;
        this.chkNeverDownloadGroundDiscounts.Checked = false;
        this.chkEnableCloseMultiweightOptimization.Checked = settings.RegistrySettings["MWEODOptimization"].Value.ToString() == "Y";
        bool flag = !this.IsAccountNumberEpic(settings.AccountObject.GroundAccountNumber) || this.EpicGroundOperationalReadiness;
        this.chkEnableGroundShipping.Checked = settings.AccountObject.IsGroundEnabled & flag;
        this.chkEnableGroundShipping.Enabled = flag;
      }
      else
      {
        this.chkEnableAutomaticClose.Checked = true;
        this.chkEnableGroundShipping.Checked = false;
      }
      this._bInsideObjectsToScreen = false;
      return 0;
    }

    public bool HandleError(Error error)
    {
      Utility.DisplayError(error);
      return error.Code != 1 && this.HandleError(error.Code);
    }

    public bool HandleError(int error)
    {
      bool flag = true;
      Control control = (Control) null;
      switch (error)
      {
        case 3431:
          control = (Control) this.edtCompany;
          break;
        case 3432:
          control = (Control) this.edtAddress1;
          break;
        case 3747:
          control = (Control) this.chkEnableGroundShipping;
          break;
        default:
          flag = false;
          break;
      }
      if (flag && control != null && this.MyParent != null)
      {
        if (this.MyParent.SystemSettingsTabControl.SelectedTab != this.Parent)
          this.MyParent.SystemSettingsTabControl.SelectedTab = this.Parent as TabPage;
        control.Focus();
        if (!control.Enabled)
          control.Enabled = true;
      }
      return flag;
    }

    private bool ShowHomeDelivery(string country)
    {
      bool flag = false;
      if (country == "US")
      {
        SystemSettings parent = this.Parent.Parent.Parent as SystemSettings;
        flag = Utility.IsUS50(new Address()
        {
          StateProvince = parent.ExpressAdminPage.StateProvince,
          CountryCode = country
        });
      }
      this.chkHomeDelivery.Visible = flag;
      return flag;
    }

    private void chkEnableGroundShipping_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.chkEnableGroundShipping.Checked && this.MyParent.Settings.AccountObject.IsSmartPostEnabled && this.MyParent.Settings.AccountObject.SPPickupCarrier == Shipment.CarrierType.Ground && this.HasActiveSmartpostShipments)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, GuiData.Languafier.TranslateError(2762), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.chkEnableGroundShipping.Checked = !this.HasEpicAccountNumber || this.EpicGroundOperationalReadiness;
      }
      if (this.chkEnableGroundShipping.Checked)
      {
        this.EnableFields(true);
        if (!this.chkHomeDelivery.Visible)
          return;
        this.chkHomeDelivery.Checked = true;
      }
      else
        this.EnableFields(false);
    }

    private void EnableFields(bool bEnable)
    {
      this.chkEpdiBanner.Enabled = bEnable;
      this.chkEnableAutomaticClose.Enabled = bEnable;
      this.edtCompany.Enabled = bEnable;
      this.edtAddress1.Enabled = bEnable;
      this.edtAddress2.Enabled = bEnable;
      this.chkCollect.Enabled = bEnable;
      this.chkAOD.Enabled = bEnable;
      this.chkEnableCloseMultiweightOptimization.Enabled = bEnable;
      this.chkResidential.Enabled = bEnable;
      this.chkCommercial.Enabled = bEnable;
      this.chkIGDDShipper.Enabled = bEnable;
      this.grpIGDD.Enabled = bEnable;
      this.chkHomeDelivery.Enabled = bEnable;
      this.btnClearAccountNum.Enabled = bEnable;
    }

    private void SystemSettingsGroundAdmin_Load(object sender, EventArgs e)
    {
      if (this.MyParent != null && this.MyParent.Settings.AccountObject.IsGroundEnabled && (!this.HasEpicAccountNumber || this.EpicGroundOperationalReadiness))
        this.EnableFields(true);
      else
        this.EnableFields(false);
      if (!this.DesignMode)
        this.ShowHomeDelivery(this.MyParent.ExpressAdminPage.Country);
      this.edtGroundAccountNum.Enabled = Utility.DevMode;
      this.edtGroundAccountNum.ReadOnly = !Utility.DevMode;
    }

    private void btnEnableSmartPost_Click(object sender, EventArgs e)
    {
      using (SmartPostConfigurationForm configurationForm = new SmartPostConfigurationForm(this.MyParent.Settings.AccountObject))
      {
        int num = (int) configurationForm.ShowDialog((IWin32Window) this);
      }
    }

    private void chkEnableAutomaticClose_CheckedChanged(object sender, EventArgs e)
    {
      this.MyParent.UserPage.ShowGroundAutoClose((sender as CheckBox).Checked);
    }

    private void btnClearAccountNum_Click(object sender, EventArgs e)
    {
      this.edtGroundAccountNum.Text = string.Empty;
      this._sendTransaction = true;
    }

    internal void SetupEvents()
    {
      if (this.MyParent != null)
        this.MyParent.SystemSettingsEventBroker.GetTopic("SystemSettingsCountryChanged")?.AddSubscriber((object) this, "OnSystemSettingsCountryChanged");
      this._bEventsInitialized = true;
    }

    internal bool EnableGroundCheckBox()
    {
      return this.EnableGroundCheckBox(this.MyParent.Settings.AccountObject);
    }

    private bool EnableGroundCheckBox(Account acct)
    {
      Address address = new Address();
      address.CountryCode = this.MyParent.ExpressAdminPage.Country;
      address.StateProvince = this.MyParent.ExpressAdminPage.StateProvince;
      bool flag1;
      if (string.IsNullOrEmpty(address.CountryCode) || string.IsNullOrEmpty(address.StateProvince) && this.MyParent.ExpressAdminPage.Country != "MX" || Utility.IsPuertoRico(address))
      {
        this.chkEnableGroundShipping.Enabled = false;
        flag1 = false;
      }
      else
      {
        bool flag2 = acct == null || !this.IsAccountNumberEpic(acct.GroundAccountNumber) || this.EpicGroundOperationalReadiness;
        this.chkEnableGroundShipping.Enabled = flag2;
        flag1 = flag2;
      }
      return flag1;
    }

    internal void UpdateGroundAccountNumber(string number)
    {
      this.edtGroundAccountNum.Text = number;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SystemSettingsGroundAdmin));
      this.chkEnableGroundShipping = new CheckBox();
      this.colorGroupBox26 = new ColorGroupBox();
      this.colorGroupBox3 = new ColorGroupBox();
      this.chkNeverDownloadGroundDiscounts = new CheckBox();
      this.chkEnableAutomaticClose = new CheckBox();
      this.chkEpdiBanner = new CheckBox();
      this.chkEnableCloseMultiweightOptimization = new CheckBox();
      this.edtZipPostal = new FdxMaskedEdit();
      this.edtStateProvince = new TextBox();
      this.edtCity = new TextBox();
      this.label15 = new Label();
      this.edtAddress2 = new TextBox();
      this.label13 = new Label();
      this.edtAddress1 = new TextBox();
      this.label12 = new Label();
      this.edtCompany = new TextBox();
      this.label11 = new Label();
      this.edtGroundAccountNum = new TextBox();
      this.colorGroupBox27 = new ColorGroupBox();
      this.chkHomeDelivery = new CheckBox();
      this.chkCommercial = new CheckBox();
      this.chkResidential = new CheckBox();
      this.colorGroupBox29 = new ColorGroupBox();
      this.chkCollect = new CheckBox();
      this.colorGroupBox1 = new ColorGroupBox();
      this.flpButtons = new FlowLayoutPanel();
      this.btnClearAccountNum = new Button();
      this.btnEnableSmartPost = new Button();
      this.tableLayoutPanel5 = new TableLayoutPanel();
      this.panel1 = new Panel();
      this.colorGroupBox28 = new ColorGroupBox();
      this.chkAOD = new CheckBox();
      this.grpIGDD = new ColorGroupBox();
      this.chkIGDDShipper = new CheckBox();
      this.focusExtender1 = new FocusExtender();
      Label label1 = new Label();
      Label label2 = new Label();
      Label label3 = new Label();
      this.colorGroupBox26.SuspendLayout();
      this.colorGroupBox3.SuspendLayout();
      this.colorGroupBox27.SuspendLayout();
      this.colorGroupBox29.SuspendLayout();
      this.colorGroupBox1.SuspendLayout();
      this.flpButtons.SuspendLayout();
      this.tableLayoutPanel5.SuspendLayout();
      this.panel1.SuspendLayout();
      this.colorGroupBox28.SuspendLayout();
      this.grpIGDD.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) label1, "lblZipPostal");
      label1.Name = "lblZipPostal";
      componentResourceManager.ApplyResources((object) label2, "lblStateProvince");
      label2.Name = "lblStateProvince";
      componentResourceManager.ApplyResources((object) label3, "lblCity");
      label3.Name = "lblCity";
      this.helpProvider1.SetHelpKeyword((Control) this.chkEnableGroundShipping, componentResourceManager.GetString("chkEnableGroundShipping.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkEnableGroundShipping, (HelpNavigator) componentResourceManager.GetObject("chkEnableGroundShipping.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkEnableGroundShipping, "chkEnableGroundShipping");
      this.chkEnableGroundShipping.Name = "chkEnableGroundShipping";
      this.helpProvider1.SetShowHelp((Control) this.chkEnableGroundShipping, (bool) componentResourceManager.GetObject("chkEnableGroundShipping.ShowHelp"));
      this.chkEnableGroundShipping.UseVisualStyleBackColor = true;
      this.chkEnableGroundShipping.CheckedChanged += new EventHandler(this.chkEnableGroundShipping_CheckedChanged);
      this.colorGroupBox26.BorderThickness = 1f;
      this.tableLayoutPanel5.SetColumnSpan((Control) this.colorGroupBox26, 3);
      this.colorGroupBox26.Controls.Add((Control) this.colorGroupBox3);
      this.colorGroupBox26.Controls.Add((Control) label1);
      this.colorGroupBox26.Controls.Add((Control) this.edtZipPostal);
      this.colorGroupBox26.Controls.Add((Control) label2);
      this.colorGroupBox26.Controls.Add((Control) this.edtStateProvince);
      this.colorGroupBox26.Controls.Add((Control) label3);
      this.colorGroupBox26.Controls.Add((Control) this.edtCity);
      this.colorGroupBox26.Controls.Add((Control) this.label15);
      this.colorGroupBox26.Controls.Add((Control) this.edtAddress2);
      this.colorGroupBox26.Controls.Add((Control) this.label13);
      this.colorGroupBox26.Controls.Add((Control) this.edtAddress1);
      this.colorGroupBox26.Controls.Add((Control) this.label12);
      this.colorGroupBox26.Controls.Add((Control) this.edtCompany);
      this.colorGroupBox26.Controls.Add((Control) this.label11);
      this.colorGroupBox26.Controls.Add((Control) this.edtGroundAccountNum);
      componentResourceManager.ApplyResources((object) this.colorGroupBox26, "colorGroupBox26");
      this.colorGroupBox26.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox26.Name = "colorGroupBox26";
      this.colorGroupBox26.RoundCorners = 5;
      this.colorGroupBox26.TabStop = false;
      this.colorGroupBox3.BorderThickness = 1f;
      this.colorGroupBox3.Controls.Add((Control) this.chkNeverDownloadGroundDiscounts);
      this.colorGroupBox3.Controls.Add((Control) this.chkEnableAutomaticClose);
      this.colorGroupBox3.Controls.Add((Control) this.chkEpdiBanner);
      this.colorGroupBox3.Controls.Add((Control) this.chkEnableCloseMultiweightOptimization);
      this.colorGroupBox3.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.colorGroupBox3, "colorGroupBox3");
      this.colorGroupBox3.Name = "colorGroupBox3";
      this.colorGroupBox3.RoundCorners = 5;
      this.colorGroupBox3.TabStop = false;
      componentResourceManager.ApplyResources((object) this.chkNeverDownloadGroundDiscounts, "chkNeverDownloadGroundDiscounts");
      this.chkNeverDownloadGroundDiscounts.Name = "chkNeverDownloadGroundDiscounts";
      this.chkNeverDownloadGroundDiscounts.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkEnableAutomaticClose, componentResourceManager.GetString("chkEnableAutomaticClose.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkEnableAutomaticClose, (HelpNavigator) componentResourceManager.GetObject("chkEnableAutomaticClose.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkEnableAutomaticClose, "chkEnableAutomaticClose");
      this.chkEnableAutomaticClose.Name = "chkEnableAutomaticClose";
      this.helpProvider1.SetShowHelp((Control) this.chkEnableAutomaticClose, (bool) componentResourceManager.GetObject("chkEnableAutomaticClose.ShowHelp"));
      this.chkEnableAutomaticClose.UseVisualStyleBackColor = true;
      this.chkEnableAutomaticClose.CheckedChanged += new EventHandler(this.chkEnableAutomaticClose_CheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.chkEpdiBanner, componentResourceManager.GetString("chkEpdiBanner.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkEpdiBanner, (HelpNavigator) componentResourceManager.GetObject("chkEpdiBanner.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkEpdiBanner, "chkEpdiBanner");
      this.chkEpdiBanner.Name = "chkEpdiBanner";
      this.helpProvider1.SetShowHelp((Control) this.chkEpdiBanner, (bool) componentResourceManager.GetObject("chkEpdiBanner.ShowHelp"));
      this.chkEpdiBanner.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkEnableCloseMultiweightOptimization, componentResourceManager.GetString("chkEnableCloseMultiweightOptimization.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkEnableCloseMultiweightOptimization, (HelpNavigator) componentResourceManager.GetObject("chkEnableCloseMultiweightOptimization.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkEnableCloseMultiweightOptimization, "chkEnableCloseMultiweightOptimization");
      this.chkEnableCloseMultiweightOptimization.Name = "chkEnableCloseMultiweightOptimization";
      this.helpProvider1.SetShowHelp((Control) this.chkEnableCloseMultiweightOptimization, (bool) componentResourceManager.GetObject("chkEnableCloseMultiweightOptimization.ShowHelp"));
      this.chkEnableCloseMultiweightOptimization.UseVisualStyleBackColor = true;
      this.edtZipPostal.Allow = "";
      this.edtZipPostal.Disallow = "";
      this.edtZipPostal.eMask = eMasks.maskCustom;
      this.edtZipPostal.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtZipPostal, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.edtZipPostal, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtZipPostal, componentResourceManager.GetString("edtZipPostal.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtZipPostal, (HelpNavigator) componentResourceManager.GetObject("edtZipPostal.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtZipPostal, "edtZipPostal");
      this.edtZipPostal.Mask = "";
      this.edtZipPostal.Name = "edtZipPostal";
      this.edtZipPostal.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.edtZipPostal, (bool) componentResourceManager.GetObject("edtZipPostal.ShowHelp"));
      this.edtZipPostal.TabStop = false;
      this.focusExtender1.SetFocusBackColor((Control) this.edtStateProvince, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.edtStateProvince, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtStateProvince, componentResourceManager.GetString("edtStateProvince.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtStateProvince, (HelpNavigator) componentResourceManager.GetObject("edtStateProvince.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtStateProvince, "edtStateProvince");
      this.edtStateProvince.Name = "edtStateProvince";
      this.edtStateProvince.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.edtStateProvince, (bool) componentResourceManager.GetObject("edtStateProvince.ShowHelp"));
      this.edtStateProvince.TabStop = false;
      this.focusExtender1.SetFocusBackColor((Control) this.edtCity, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.edtCity, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtCity, componentResourceManager.GetString("edtCity.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtCity, (HelpNavigator) componentResourceManager.GetObject("edtCity.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtCity, "edtCity");
      this.edtCity.Name = "edtCity";
      this.edtCity.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.edtCity, (bool) componentResourceManager.GetObject("edtCity.ShowHelp"));
      this.edtCity.TabStop = false;
      componentResourceManager.ApplyResources((object) this.label15, "label15");
      this.label15.Name = "label15";
      this.focusExtender1.SetFocusBackColor((Control) this.edtAddress2, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtAddress2, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtAddress2, componentResourceManager.GetString("edtAddress2.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtAddress2, (HelpNavigator) componentResourceManager.GetObject("edtAddress2.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtAddress2, "edtAddress2");
      this.edtAddress2.Name = "edtAddress2";
      this.helpProvider1.SetShowHelp((Control) this.edtAddress2, (bool) componentResourceManager.GetObject("edtAddress2.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label13, "label13");
      this.label13.Name = "label13";
      this.focusExtender1.SetFocusBackColor((Control) this.edtAddress1, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtAddress1, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtAddress1, componentResourceManager.GetString("edtAddress1.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtAddress1, (HelpNavigator) componentResourceManager.GetObject("edtAddress1.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtAddress1, "edtAddress1");
      this.edtAddress1.Name = "edtAddress1";
      this.helpProvider1.SetShowHelp((Control) this.edtAddress1, (bool) componentResourceManager.GetObject("edtAddress1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label12, "label12");
      this.label12.Name = "label12";
      this.focusExtender1.SetFocusBackColor((Control) this.edtCompany, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtCompany, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtCompany, componentResourceManager.GetString("edtCompany.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtCompany, (HelpNavigator) componentResourceManager.GetObject("edtCompany.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtCompany, "edtCompany");
      this.edtCompany.Name = "edtCompany";
      this.helpProvider1.SetShowHelp((Control) this.edtCompany, (bool) componentResourceManager.GetObject("edtCompany.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label11, "label11");
      this.label11.Name = "label11";
      this.focusExtender1.SetFocusBackColor((Control) this.edtGroundAccountNum, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.edtGroundAccountNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtGroundAccountNum, componentResourceManager.GetString("edtGroundAccountNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtGroundAccountNum, (HelpNavigator) componentResourceManager.GetObject("edtGroundAccountNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtGroundAccountNum, "edtGroundAccountNum");
      this.edtGroundAccountNum.Name = "edtGroundAccountNum";
      this.edtGroundAccountNum.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.edtGroundAccountNum, (bool) componentResourceManager.GetObject("edtGroundAccountNum.ShowHelp"));
      this.edtGroundAccountNum.TabStop = false;
      this.colorGroupBox27.BorderThickness = 1f;
      this.colorGroupBox27.Controls.Add((Control) this.chkHomeDelivery);
      this.colorGroupBox27.Controls.Add((Control) this.chkCommercial);
      this.colorGroupBox27.Controls.Add((Control) this.chkResidential);
      componentResourceManager.ApplyResources((object) this.colorGroupBox27, "colorGroupBox27");
      this.colorGroupBox27.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox27.Name = "colorGroupBox27";
      this.colorGroupBox27.RoundCorners = 5;
      this.tableLayoutPanel5.SetRowSpan((Control) this.colorGroupBox27, 3);
      this.colorGroupBox27.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkHomeDelivery, componentResourceManager.GetString("chkHomeDelivery.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkHomeDelivery, (HelpNavigator) componentResourceManager.GetObject("chkHomeDelivery.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkHomeDelivery, "chkHomeDelivery");
      this.chkHomeDelivery.Name = "chkHomeDelivery";
      this.helpProvider1.SetShowHelp((Control) this.chkHomeDelivery, (bool) componentResourceManager.GetObject("chkHomeDelivery.ShowHelp"));
      this.chkHomeDelivery.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkCommercial, componentResourceManager.GetString("chkCommercial.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkCommercial, (HelpNavigator) componentResourceManager.GetObject("chkCommercial.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkCommercial, "chkCommercial");
      this.chkCommercial.Name = "chkCommercial";
      this.helpProvider1.SetShowHelp((Control) this.chkCommercial, (bool) componentResourceManager.GetObject("chkCommercial.ShowHelp"));
      this.chkCommercial.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkResidential, componentResourceManager.GetString("chkResidential.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkResidential, (HelpNavigator) componentResourceManager.GetObject("chkResidential.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkResidential, "chkResidential");
      this.chkResidential.Name = "chkResidential";
      this.helpProvider1.SetShowHelp((Control) this.chkResidential, (bool) componentResourceManager.GetObject("chkResidential.ShowHelp"));
      this.chkResidential.UseVisualStyleBackColor = true;
      this.colorGroupBox29.BorderThickness = 1f;
      this.colorGroupBox29.Controls.Add((Control) this.chkCollect);
      this.colorGroupBox29.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.colorGroupBox29, "colorGroupBox29");
      this.colorGroupBox29.Name = "colorGroupBox29";
      this.colorGroupBox29.RoundCorners = 5;
      this.colorGroupBox29.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkCollect, componentResourceManager.GetString("chkCollect.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkCollect, (HelpNavigator) componentResourceManager.GetObject("chkCollect.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkCollect, "chkCollect");
      this.chkCollect.Name = "chkCollect";
      this.helpProvider1.SetShowHelp((Control) this.chkCollect, (bool) componentResourceManager.GetObject("chkCollect.ShowHelp"));
      this.chkCollect.UseVisualStyleBackColor = true;
      this.colorGroupBox1.BorderThickness = 1f;
      this.tableLayoutPanel5.SetColumnSpan((Control) this.colorGroupBox1, 3);
      this.colorGroupBox1.Controls.Add((Control) this.flpButtons);
      componentResourceManager.ApplyResources((object) this.colorGroupBox1, "colorGroupBox1");
      this.colorGroupBox1.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox1.Name = "colorGroupBox1";
      this.colorGroupBox1.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox1, (bool) componentResourceManager.GetObject("colorGroupBox1.ShowHelp"));
      this.colorGroupBox1.TabStop = false;
      componentResourceManager.ApplyResources((object) this.flpButtons, "flpButtons");
      this.flpButtons.Controls.Add((Control) this.btnClearAccountNum);
      this.flpButtons.Controls.Add((Control) this.btnEnableSmartPost);
      this.flpButtons.Name = "flpButtons";
      this.helpProvider1.SetShowHelp((Control) this.flpButtons, (bool) componentResourceManager.GetObject("flpButtons.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnClearAccountNum, "btnClearAccountNum");
      this.helpProvider1.SetHelpKeyword((Control) this.btnClearAccountNum, componentResourceManager.GetString("btnClearAccountNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnClearAccountNum, (HelpNavigator) componentResourceManager.GetObject("btnClearAccountNum.HelpNavigator"));
      this.btnClearAccountNum.Name = "btnClearAccountNum";
      this.helpProvider1.SetShowHelp((Control) this.btnClearAccountNum, (bool) componentResourceManager.GetObject("btnClearAccountNum.ShowHelp"));
      this.btnClearAccountNum.UseVisualStyleBackColor = true;
      this.btnClearAccountNum.Click += new EventHandler(this.btnClearAccountNum_Click);
      componentResourceManager.ApplyResources((object) this.btnEnableSmartPost, "btnEnableSmartPost");
      this.helpProvider1.SetHelpKeyword((Control) this.btnEnableSmartPost, componentResourceManager.GetString("btnEnableSmartPost.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnEnableSmartPost, (HelpNavigator) componentResourceManager.GetObject("btnEnableSmartPost.HelpNavigator"));
      this.btnEnableSmartPost.Name = "btnEnableSmartPost";
      this.helpProvider1.SetShowHelp((Control) this.btnEnableSmartPost, (bool) componentResourceManager.GetObject("btnEnableSmartPost.ShowHelp"));
      this.btnEnableSmartPost.UseVisualStyleBackColor = true;
      this.btnEnableSmartPost.Click += new EventHandler(this.btnEnableSmartPost_Click);
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel5, "tableLayoutPanel5");
      this.tableLayoutPanel5.Controls.Add((Control) this.colorGroupBox1, 0, 5);
      this.tableLayoutPanel5.Controls.Add((Control) this.colorGroupBox26, 0, 1);
      this.tableLayoutPanel5.Controls.Add((Control) this.panel1, 0, 0);
      this.tableLayoutPanel5.Controls.Add((Control) this.colorGroupBox27, 0, 2);
      this.tableLayoutPanel5.Controls.Add((Control) this.colorGroupBox29, 2, 2);
      this.tableLayoutPanel5.Controls.Add((Control) this.colorGroupBox28, 1, 2);
      this.tableLayoutPanel5.Controls.Add((Control) this.grpIGDD, 2, 3);
      this.tableLayoutPanel5.Name = "tableLayoutPanel5";
      this.panel1.Controls.Add((Control) this.chkEnableGroundShipping);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      this.colorGroupBox28.BorderThickness = 1f;
      this.colorGroupBox28.Controls.Add((Control) this.chkAOD);
      componentResourceManager.ApplyResources((object) this.colorGroupBox28, "colorGroupBox28");
      this.colorGroupBox28.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox28.Name = "colorGroupBox28";
      this.colorGroupBox28.RoundCorners = 5;
      this.tableLayoutPanel5.SetRowSpan((Control) this.colorGroupBox28, 3);
      this.colorGroupBox28.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkAOD, componentResourceManager.GetString("chkAOD.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAOD, (HelpNavigator) componentResourceManager.GetObject("chkAOD.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAOD, "chkAOD");
      this.chkAOD.Name = "chkAOD";
      this.helpProvider1.SetShowHelp((Control) this.chkAOD, (bool) componentResourceManager.GetObject("chkAOD.ShowHelp"));
      this.chkAOD.UseVisualStyleBackColor = true;
      this.grpIGDD.BorderThickness = 1f;
      this.grpIGDD.Controls.Add((Control) this.chkIGDDShipper);
      this.grpIGDD.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.grpIGDD, "grpIGDD");
      this.grpIGDD.Name = "grpIGDD";
      this.grpIGDD.RoundCorners = 5;
      this.grpIGDD.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkIGDDShipper, componentResourceManager.GetString("chkIGDDShipper.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkIGDDShipper, (HelpNavigator) componentResourceManager.GetObject("chkIGDDShipper.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkIGDDShipper, "chkIGDDShipper");
      this.chkIGDDShipper.Name = "chkIGDDShipper";
      this.helpProvider1.SetShowHelp((Control) this.chkIGDDShipper, (bool) componentResourceManager.GetObject("chkIGDDShipper.ShowHelp"));
      this.chkIGDDShipper.UseVisualStyleBackColor = true;
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanel5);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (SystemSettingsGroundAdmin);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.SystemSettingsGroundAdmin_Load);
      this.colorGroupBox26.ResumeLayout(false);
      this.colorGroupBox26.PerformLayout();
      this.colorGroupBox3.ResumeLayout(false);
      this.colorGroupBox27.ResumeLayout(false);
      this.colorGroupBox29.ResumeLayout(false);
      this.colorGroupBox1.ResumeLayout(false);
      this.colorGroupBox1.PerformLayout();
      this.flpButtons.ResumeLayout(false);
      this.flpButtons.PerformLayout();
      this.tableLayoutPanel5.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.colorGroupBox28.ResumeLayout(false);
      this.grpIGDD.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
