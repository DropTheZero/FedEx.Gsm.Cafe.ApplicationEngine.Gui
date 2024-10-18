// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SystemSettingsCustomerAdmin
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Properties;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
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
  public class SystemSettingsCustomerAdmin : UserControlHelpEx
  {
    private bool _initialExpiredRates;
    private bool _sendListRateToggle;
    private bool _bFormLoading;
    private SystemSettings _myParent;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel2;
    private ColorGroupBox colorGroupBox12;
    private ColorGroupBox colorGroupBox11;
    private ColorGroupBox colorGroupBox10;
    private TableLayoutPanel tableLayoutPanel3;
    private ColorGroupBox gbxIntlRatingOverride;
    private ColorGroupBox gbxDomRatingOverride;
    private Panel panel1;
    private ColorGroupBox colorGroupBox9;
    private ColorGroupBox colorGroupBox1;
    private TextBox edtContact;
    private TextBox edtZipPostal;
    private TextBox edtStateProvince;
    private TextBox edtCity;
    private TextBox edtAddress2;
    private TextBox edtAddress1;
    private TextBox edtCompany;
    private RadioButton rdbDomOverrideIndefinitely;
    private RadioButton rdbDomOverrideTodayOnly;
    private RadioButton rdbDomOverrideNever;
    private CheckBox chkAutoTabToWeight;
    private CheckBox chkEnableHoldFile;
    private CheckBox chkAutoPopulateCityState;
    private RadioButton rdbIntlOverrideIndefinitely;
    private RadioButton rdbIntlOverrideTodayOnly;
    private RadioButton rdbIntlOverrideNever;
    private CheckBox chkDisableRates;
    private FdxMaskedEdit edtIpdNonRevAcctNum;
    private Label label1;
    private CheckBox chkEnableAutoDownloadGroundDiscounts;
    private CheckBox chkCreateDefaultSender;
    private TextBox edtLabelPrinter;
    private Label label2;
    private Button btnLabelPrinter;
    private Button btnReportPrinter;
    private TextBox edtReportPrinter;
    private Label label4;
    private FdxMaskedEdit edtPhone;
    private FdxMaskedEdit edtPhoneExt;
    private Label lblPhoneExt;
    private ToolTip toolTip1;
    private CheckBox chkUseListRates;
    private Label label3;
    private FocusExtender focusExtender1;
    private FdxMaskedEdit edtCustomsId;
    private ColorGroupBox groupBox1;
    private CheckBox chkExcludeSPEarnedDisc;
    private CheckBox chkExcludeGrndEarnedDisc;
    private CheckBox chkExcludeExpEarnedDisc;
    private CheckBox chkExcludeBonusDisc;
    private ColorGroupBox gbxSupportInfo;
    private TextBox edtOtherEmail;
    private TextBox edtOtherContact;
    private CheckBox chkUseIntlListRates;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private TextBox edtPrimaryEmail;
    private TextBox edtPrimaryContact;
    private Label label11;
    private Label lblDynamicRatePreview;
    private ComboBox cboDynamicRatePreview;
    private CheckBox chkDisableMpsRateQuoteDialog;
    private TableLayoutPanel tableLayoutPanel1;
    private CheckBox chkExpiredRates;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string LabelPrinter
    {
      get
      {
        return !(this.edtLabelPrinter.Text == string.Empty) ? this.edtLabelPrinter.Text : (string) null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ReportPrinter
    {
      get
      {
        return !(this.edtReportPrinter.Text == string.Empty) ? this.edtReportPrinter.Text : (string) null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SystemSettings MyParent
    {
      get => this._myParent;
      set => this._myParent = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool ToggleListRates => this._sendListRateToggle;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool UseListRates => this.chkUseListRates.Checked;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool ExpiredRatesChanged => this.chkExpiredRates.Checked != this._initialExpiredRates;

    public SystemSettingsCustomerAdmin()
    {
      this.InitializeComponent();
      Utility.SetDisplayAndValue(this.cboDynamicRatePreview, Utility.GetDataTable(Utility.ListTypes.DynamicRatePreview), "Description", "Code", false);
    }

    protected override void OnLoad(EventArgs e) => base.OnLoad(e);

    public void ScreenToObjects(SystemSettingsObject settings)
    {
      if (settings == null)
        throw new ArgumentNullException(nameof (settings), "settings cannot be null");
      if (settings.AccountObject == null)
        return;
      settings.AccountObject.DomRateType = this.GetDomesticRateType();
      switch (settings.AccountObject.DomRateType)
      {
        case Account.OverrideType.TodayOnly:
          settings.AccountObject.DomDontRateStartDate = DateTime.Today;
          settings.AccountObject.DomDontRateEndDate = DateTime.Today;
          break;
        case Account.OverrideType.Indefinitely:
          settings.AccountObject.DomDontRateStartDate = DateTime.Today;
          settings.AccountObject.DomDontRateEndDate = DateTime.MaxValue;
          break;
        default:
          settings.AccountObject.DomDontRateStartDate = DateTime.MinValue;
          settings.AccountObject.DomDontRateEndDate = DateTime.MinValue;
          break;
      }
      settings.AccountObject.IntlRateType = this.GetInternationalRateType();
      switch (settings.AccountObject.IntlRateType)
      {
        case Account.OverrideType.TodayOnly:
          settings.AccountObject.IntlDontRateStartDate = DateTime.Today;
          settings.AccountObject.IntlDontRateEndDate = DateTime.Today;
          break;
        case Account.OverrideType.Indefinitely:
          settings.AccountObject.IntlDontRateStartDate = DateTime.Today;
          settings.AccountObject.IntlDontRateEndDate = DateTime.MaxValue;
          break;
        default:
          settings.AccountObject.IntlDontRateStartDate = DateTime.MinValue;
          settings.AccountObject.IntlDontRateEndDate = DateTime.MinValue;
          break;
      }
      if (settings.AccountObject.UseListRates != this.chkUseListRates.Checked)
        this._sendListRateToggle = true;
      settings.AccountObject.DynamicRatePreview = this.GetRatePreview(this.cboDynamicRatePreview.SelectedValue as string);
      settings.AccountObject.UseListRates = this.chkUseListRates.Checked;
      settings.AccountObject.UseIntlListRates = this.chkUseIntlListRates.Checked;
      settings.AccountObject.AutoPopulateCityState = this.chkAutoPopulateCityState.Checked;
      if (settings.AccountObject.Address == null)
        settings.AccountObject.Address = new Address();
      settings.AccountObject.Address.CountryCode = settings.AccountObject.Address.CountryCode;
      settings.AccountObject.Address.CountryNumber = settings.AccountObject.Address.CountryNumber;
      settings.AccountObject.Address.Contact = this.edtContact.Text;
      settings.AccountObject.Address.Company = this.edtCompany.Text;
      settings.AccountObject.Address.Addr1 = this.edtAddress1.Text;
      settings.AccountObject.Address.Addr2 = this.edtAddress2.Text;
      settings.AccountObject.NonRevenueAccountNumber = this.edtIpdNonRevAcctNum.Raw;
      settings.AccountObject.IsDisabledMPSRateQuote = this.chkDisableMpsRateQuoteDialog.Checked;
      settings.RegistrySettings["HoldFileEnabled"].Value = this.chkEnableHoldFile.Checked ? (object) "Y" : (object) "N";
      settings.RegistrySettings["NoGUIRating"].Value = this.chkDisableRates.Checked ? (object) "Y" : (object) "N";
      settings.RegistrySettings["FocusOnWeight"].Value = this.chkAutoTabToWeight.Checked ? (object) "Y" : (object) "N";
      settings.RegistrySettings["DownloadGroundRatesAtRecon"].Value = (object) "N";
      settings.AccountObject.ExcludeBonusDiscounts = this.chkExcludeBonusDisc.Checked;
      settings.AccountObject.ExcludeExpressEarnedDiscounts = this.chkExcludeExpEarnedDisc.Checked;
      settings.AccountObject.ExcludeGroundEarnedDiscounts = this.chkExcludeGrndEarnedDisc.Checked;
      settings.AccountObject.ExcludeSmartPostEarnedDiscounts = this.chkExcludeSPEarnedDisc.Checked;
      settings.AccountObject.PrimaryContact = this.edtPrimaryContact.Text;
      settings.AccountObject.PrimaryEmail = this.edtPrimaryEmail.Text;
      settings.AccountObject.OtherContact = this.edtOtherContact.Text;
      settings.AccountObject.OtherEmail = this.edtOtherEmail.Text;
      if (!this.ExpiredRatesChanged)
        return;
      GuiData.AppController.ShipEngine.SetExpiredRateSettingStatus(this.chkExpiredRates.Checked);
    }

    public void ObjectsToScreen(SystemSettingsObject settings, Utility.FormOperation op)
    {
      if (settings == null)
        return;
      this._bFormLoading = true;
      if (settings.AccountObject != null)
      {
        if (settings.AccountObject.Address != null)
        {
          this.edtContact.Text = settings.AccountObject.Address.Contact;
          this.edtCompany.Text = settings.AccountObject.Address.Company;
          this.edtAddress1.Text = settings.AccountObject.Address.Addr1;
          this.edtAddress2.Text = settings.AccountObject.Address.Addr2;
          this.edtCity.Text = settings.AccountObject.Address.City;
          this.edtStateProvince.Text = settings.AccountObject.Address.StateProvince;
          this.edtZipPostal.Text = settings.AccountObject.Address.PostalCode;
        }
        this.InitializeRatingOverrides(settings, op);
        this.chkAutoPopulateCityState.Checked = settings.AccountObject.AutoPopulateCityState;
        this.cboDynamicRatePreview.SelectedValue = (object) settings.AccountObject.DynamicRatePreview.ToString("d");
        this.edtIpdNonRevAcctNum.Text = settings.AccountObject.NonRevenueAccountNumber;
        bool flag = op == Utility.FormOperation.ViewEdit;
        this.chkUseListRates.Visible = flag;
        this.chkUseIntlListRates.Visible = flag;
        if (flag)
        {
          this.chkUseListRates.Checked = settings.AccountObject.UseListRates;
          this.chkUseIntlListRates.Checked = settings.AccountObject.UseIntlListRates;
        }
        this.chkDisableMpsRateQuoteDialog.Checked = settings.AccountObject.IsDisabledMPSRateQuote;
        this.chkExcludeSPEarnedDisc.Visible = settings.AccountObject.IsSmartPostEnabled;
        this.chkExcludeBonusDisc.Checked = settings.AccountObject.ExcludeBonusDiscounts;
        this.chkExcludeExpEarnedDisc.Checked = settings.AccountObject.ExcludeExpressEarnedDiscounts;
        this.chkExcludeGrndEarnedDisc.Checked = settings.AccountObject.ExcludeGroundEarnedDiscounts;
        this.chkExcludeSPEarnedDisc.Checked = settings.AccountObject.ExcludeSmartPostEarnedDiscounts;
        this.edtPrimaryContact.Text = settings.AccountObject.PrimaryContact;
        this.edtPrimaryEmail.Text = settings.AccountObject.PrimaryEmail;
        this.edtOtherContact.Text = settings.AccountObject.OtherContact;
        this.edtOtherEmail.Text = settings.AccountObject.OtherEmail;
      }
      if (settings.RegistrySettings != null)
      {
        this.chkAutoTabToWeight.Checked = settings.RegistrySettings["FocusOnWeight"].Value.ToString().ToUpper() == "Y";
        this.chkDisableRates.Checked = settings.RegistrySettings["NoGUIRating"].Value.ToString().ToUpper() == "Y";
        this.chkEnableHoldFile.Checked = settings.RegistrySettings["HoldFileEnabled"].Value.ToString().ToUpper() == "Y";
        this.chkEnableAutoDownloadGroundDiscounts.Checked = false;
      }
      this._initialExpiredRates = GuiData.AppController.ShipEngine.GetExpiredRateSettingStatus();
      this.chkExpiredRates.Checked = this._initialExpiredRates;
      this._bFormLoading = false;
    }

    public bool GetDefaultSender(out Sender s)
    {
      s = (Sender) null;
      if (this.chkCreateDefaultSender.Checked)
      {
        s = new Sender();
        s.Address = new Address();
        s.Address.Contact = this.edtContact.Text;
        s.Address.Company = this.edtCompany.Text;
        s.Address.Addr1 = this.edtAddress1.Text;
        s.Address.Addr2 = this.edtAddress2.Text;
        if (this._myParent != null)
        {
          s.Code = "SENDER" + this._myParent.SystemNumber;
          if (this._myParent.ExpressAdminPage != null)
          {
            s.Address.CountryCode = this._myParent.ExpressAdminPage.Country;
            s.Address.CountryNumber = this._myParent.ExpressAdminPage.CountryNumber;
            s.Address.City = this._myParent.ExpressAdminPage.City;
            s.Address.StateProvince = this._myParent.ExpressAdminPage.StateProvince;
            s.AccountNumber = this._myParent.ExpressAdminPage.AccountNumber;
            s.Address.PostalCode = this._myParent.ExpressAdminPage.PostalCode;
          }
        }
        s.Address.Phone = this.edtPhone.Raw;
        s.Address.PhoneExtension = this.edtPhoneExt.Raw;
        s.ForwardingAgent = 0;
        s.ALCNumber = (string) null;
        s.ReleaseNumber = (string) null;
        s.PONumber = (string) null;
        s.CustomsId = this.edtCustomsId.Text;
        s.SocialSecurityNumber = (string) null;
        s.DunsBradstreetNumber = (string) null;
        s.DomProfileCode = "DEFAULT";
        s.IntProfileCode = "DEFAULT";
        s.TDProfileCode = "DEFAULT";
        if (!"US".Equals(s.Address.CountryCode, StringComparison.InvariantCultureIgnoreCase))
          s.CustomsId = string.Empty;
        s.ElectronicSignature = this.edtContact.Text;
      }
      return this.chkCreateDefaultSender.Checked;
    }

    public bool HandleError(int error)
    {
      bool flag = true;
      Control control = (Control) null;
      switch (error)
      {
        case 3430:
          control = (Control) this.edtContact;
          break;
        case 3431:
          control = (Control) this.edtCompany;
          break;
        case 3432:
          control = (Control) this.edtAddress1;
          break;
        case 3433:
          control = (Control) this.edtAddress2;
          break;
        case 13637:
          control = (Control) this.edtIpdNonRevAcctNum;
          break;
        default:
          flag = false;
          break;
      }
      if (flag && control != null && this._myParent != null)
      {
        if (this._myParent.SystemSettingsTabControl.SelectedTab != this.Parent)
          this._myParent.SystemSettingsTabControl.SelectedTab = this.Parent as TabPage;
        control.Focus();
        if (!control.Enabled)
          control.Enabled = true;
      }
      return flag;
    }

    public void InitializeRatingOverrides(SystemSettingsObject settings, Utility.FormOperation op)
    {
      if (settings == null || settings.AccountObject == null)
        return;
      Account.OverrideType ot1 = Account.OverrideType.Never;
      Account.OverrideType ot2 = Account.OverrideType.Never;
      DateTime dateTime;
      if (Utility.IsPuertoRico(settings.AccountObject.Address))
      {
        ot1 = Account.OverrideType.Never;
        this.gbxDomRatingOverride.Enabled = false;
      }
      else if (settings.AccountObject.DomDontRateEndDate < DateTime.Today)
      {
        ot1 = Account.OverrideType.Never;
      }
      else
      {
        dateTime = settings.AccountObject.DomDontRateStartDate;
        DateTime date1 = dateTime.Date;
        dateTime = settings.AccountObject.DomDontRateEndDate;
        DateTime date2 = dateTime.Date;
        if (date1 == date2)
        {
          dateTime = settings.AccountObject.DomDontRateStartDate;
          DateTime date3 = dateTime.Date;
          dateTime = DateTime.Today;
          DateTime date4 = dateTime.Date;
          if (date3 == date4)
          {
            ot1 = Account.OverrideType.TodayOnly;
            goto label_10;
          }
        }
        dateTime = settings.AccountObject.DomDontRateStartDate;
        DateTime date5 = dateTime.Date;
        dateTime = DateTime.Today;
        DateTime date6 = dateTime.Date;
        if (date5 <= date6)
          ot1 = Account.OverrideType.Indefinitely;
      }
label_10:
      if (settings.AccountObject.IntlDontRateEndDate < DateTime.Today)
      {
        ot2 = Account.OverrideType.Never;
      }
      else
      {
        dateTime = settings.AccountObject.IntlDontRateStartDate;
        DateTime date7 = dateTime.Date;
        dateTime = settings.AccountObject.IntlDontRateEndDate;
        DateTime date8 = dateTime.Date;
        if (date7 == date8)
        {
          dateTime = settings.AccountObject.IntlDontRateStartDate;
          DateTime date9 = dateTime.Date;
          dateTime = DateTime.Today;
          DateTime date10 = dateTime.Date;
          if (date9 == date10)
          {
            ot2 = Account.OverrideType.TodayOnly;
            goto label_17;
          }
        }
        dateTime = settings.AccountObject.IntlDontRateStartDate;
        DateTime date11 = dateTime.Date;
        dateTime = DateTime.Today;
        DateTime date12 = dateTime.Date;
        if (date11 <= date12)
          ot2 = Account.OverrideType.Indefinitely;
      }
label_17:
      this.SetDomesticOverrideType(ot1);
      this.SetInternationalOverrideType(ot2);
    }

    public void OnSystemSettingsCountryChanged(object sender, CountryChangedEventArgs args)
    {
      if (!string.IsNullOrEmpty(args.CountryCode) && GuiData.AppController.ShipEngine.IsCountryInRegion(Country.Region.LAC, args.CountryCode).ErrorCode == 1)
      {
        this.SetMasks(args.CountryCode);
        this.edtPhone.SetMask(eMasks.maskIntlPhone);
        this.chkAutoPopulateCityState.Enabled = false;
        this.chkAutoPopulateCityState.Checked = false;
      }
      else
      {
        this.SetMasks(args.CountryCode);
        this.edtPhone.SetMask(eMasks.maskPhoneTen);
        this.chkAutoPopulateCityState.Enabled = true;
      }
    }

    public void SetupEvents()
    {
      if (this._myParent == null)
        return;
      this._myParent.SystemSettingsEventBroker.GetTopic("SystemSettingsCountryChanged")?.AddSubscriber((object) this, "OnSystemSettingsCountryChanged");
    }

    private Account.DynamicRatePreviewType GetRatePreview(string ratePreview)
    {
      try
      {
        return (Account.DynamicRatePreviewType) Enum.Parse(typeof (Account.DynamicRatePreviewType), ratePreview);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "SystemSettingsCustomerAdmin.GetRatePreview", "Exception mapping dynamic rate value " + ex.ToString());
      }
      return Account.DynamicRatePreviewType.Never;
    }

    private void GetListRates(bool domestic, bool international)
    {
      List<AdminDownloadRequest> requestList = new List<AdminDownloadRequest>();
      if (domestic)
      {
        AdminDownloadRequest adminDownloadRequest1 = new AdminDownloadRequest();
        adminDownloadRequest1.DownloadType = !(this.MyParent.ExpressAdminPage.Country != "US") ? AdminDownloadRequest.RequestType.ListRates : AdminDownloadRequest.RequestType.DomesticNonUsList;
        adminDownloadRequest1.Account.AccountNumber = this.MyParent.ExpressAdminPage.AccountNumber;
        adminDownloadRequest1.Account.MeterNumber = this.MyParent.MeterNumber;
        requestList.Add(adminDownloadRequest1);
        if (this.MyParent.Settings != null && this.MyParent.Settings.AccountObject != null && this.MyParent.Settings.AccountObject.IsSmartPostEnabled)
        {
          AdminDownloadRequest adminDownloadRequest2 = new AdminDownloadRequest();
          adminDownloadRequest2.DownloadType = AdminDownloadRequest.RequestType.SmartPostListRates;
          adminDownloadRequest2.Account.AccountNumber = this.MyParent.ExpressAdminPage.AccountNumber;
          adminDownloadRequest2.Account.MeterNumber = this.MyParent.MeterNumber;
          requestList.Add(adminDownloadRequest2);
        }
      }
      if (international)
      {
        AdminDownloadRequest adminDownloadRequest = new AdminDownloadRequest();
        adminDownloadRequest.DownloadType = AdminDownloadRequest.RequestType.IntlListRate;
        adminDownloadRequest.Account.AccountNumber = this.MyParent.ExpressAdminPage.AccountNumber;
        adminDownloadRequest.Account.MeterNumber = this.MyParent.MeterNumber;
        requestList.Add(adminDownloadRequest);
      }
      Error error = new Error();
      try
      {
        this.Cursor = Cursors.WaitCursor;
        ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.ProcessDownloadRequests(requestList, error);
        this.Cursor = Cursors.Arrow;
        if (!serviceResponse.IsOperationOk)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), serviceResponse.ErrorMessage);
          int num = (int) Utility.DisplayError(serviceResponse.ErrorMessage);
        }
        else
        {
          int num1 = (int) MessageBox.Show((IWin32Window) this, GuiData.Languafier.TranslateMessage("m38006"), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), ex.Message);
        int num = (int) Utility.DisplayError(ex.Message);
      }
    }

    private void chkCreateDefaultSender_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox checkBox = sender as CheckBox;
      this.edtPhone.Enabled = checkBox.Checked;
      this.edtPhoneExt.Enabled = checkBox.Checked;
      if (this.MyParent != null && this.MyParent.ExpressAdminPage != null)
      {
        if (this.MyParent.ExpressAdminPage.Country != null)
          this.SetMasks(this._myParent.ExpressAdminPage.Country);
        else
          this.SetMasks("US");
      }
      else
        this.SetMasks("US");
      this.edtCustomsId.Enabled = checkBox.Checked;
    }

    private void SetMasks(string country)
    {
      if ("US" == country)
        this.edtCustomsId.Mask = "a{18}~&-{2}";
      else
        Utility.DecorateNamedMaskedControl(this.edtCustomsId, country);
    }

    private void btnLabelPrinter_Click(object sender, EventArgs e)
    {
      string aprinter = this.GetAPrinter(FormSetting.FormSettingTypes.FORM_SETTINGS_LABEL);
      if (string.IsNullOrEmpty(aprinter))
        return;
      this.edtLabelPrinter.Text = aprinter;
    }

    private void btnReportPrinter_Click(object sender, EventArgs e)
    {
      string aprinter = this.GetAPrinter(FormSetting.FormSettingTypes.FORM_SETTINGS_REPORTS);
      if (string.IsNullOrEmpty(aprinter))
        return;
      this.edtReportPrinter.Text = aprinter;
    }

    private string GetAPrinter(FormSetting.FormSettingTypes formCode)
    {
      using (SelectPrinter selectPrinter = new SelectPrinter(formCode, string.Empty))
        return selectPrinter.ShowDialog((IWin32Window) this) == DialogResult.OK ? selectPrinter.SelectedDevice : (string) null;
    }

    private void demographicsChanged(object sender, EventArgs e)
    {
      this.MyParent.SendTransaction = true;
    }

    private void overrideDomesticRating_CheckChanged(object sender, EventArgs e)
    {
    }

    private void overrideInternationalRating_CheckChanged(object sender, EventArgs e)
    {
    }

    private void chkUseListRates_CheckedChanged(object sender, EventArgs e)
    {
      if (!this._bFormLoading && sender is CheckBox checkBox && checkBox.Checked && Utility.DisplayError(GuiData.Languafier.TranslateMessage(39859), Error.ErrorType.Question) == DialogResult.Yes)
      {
        this.Refresh();
        this.GetListRates(sender == this.chkUseListRates, sender == this.chkUseIntlListRates);
      }
      if (!(this.cboDynamicRatePreview.DataSource is DataTable dataSource))
        return;
      if (this.chkUseListRates.Checked || this.chkUseIntlListRates.Checked)
        dataSource.DefaultView.RowFilter = string.Empty;
      else
        dataSource.DefaultView.RowFilter = "Code <> 2";
    }

    private void SetInternationalOverrideType(Account.OverrideType ot)
    {
      this.rdbIntlOverrideNever.Checked = ot == Account.OverrideType.Never;
      this.rdbIntlOverrideTodayOnly.Checked = ot == Account.OverrideType.TodayOnly;
      this.rdbIntlOverrideIndefinitely.Checked = ot == Account.OverrideType.Indefinitely;
    }

    private void SetDomesticOverrideType(Account.OverrideType ot)
    {
      this.rdbDomOverrideNever.Checked = ot == Account.OverrideType.Never;
      this.rdbDomOverrideTodayOnly.Checked = ot == Account.OverrideType.TodayOnly;
      this.rdbDomOverrideIndefinitely.Checked = ot == Account.OverrideType.Indefinitely;
    }

    private Account.OverrideType GetDomesticRateType()
    {
      if (this.rdbDomOverrideIndefinitely.Checked)
        return Account.OverrideType.Indefinitely;
      return this.rdbDomOverrideTodayOnly.Checked ? Account.OverrideType.TodayOnly : Account.OverrideType.Never;
    }

    private Account.OverrideType GetInternationalRateType()
    {
      if (this.rdbIntlOverrideIndefinitely.Checked)
        return Account.OverrideType.Indefinitely;
      return this.rdbIntlOverrideTodayOnly.Checked ? Account.OverrideType.TodayOnly : Account.OverrideType.Never;
    }

    private bool HandleError(Error error)
    {
      Utility.DisplayError(error);
      return error.Code != 1 && this.HandleError(error.Code);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SystemSettingsCustomerAdmin));
      this.tableLayoutPanel2 = new TableLayoutPanel();
      this.colorGroupBox12 = new ColorGroupBox();
      this.edtCustomsId = new FdxMaskedEdit();
      this.edtPhoneExt = new FdxMaskedEdit();
      this.lblPhoneExt = new Label();
      this.edtPhone = new FdxMaskedEdit();
      this.chkCreateDefaultSender = new CheckBox();
      this.colorGroupBox1 = new ColorGroupBox();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.label2 = new Label();
      this.label3 = new Label();
      this.edtLabelPrinter = new TextBox();
      this.btnReportPrinter = new Button();
      this.btnLabelPrinter = new Button();
      this.edtReportPrinter = new TextBox();
      this.label4 = new Label();
      this.colorGroupBox11 = new ColorGroupBox();
      this.chkAutoTabToWeight = new CheckBox();
      this.chkEnableHoldFile = new CheckBox();
      this.chkAutoPopulateCityState = new CheckBox();
      this.colorGroupBox10 = new ColorGroupBox();
      this.tableLayoutPanel3 = new TableLayoutPanel();
      this.gbxIntlRatingOverride = new ColorGroupBox();
      this.rdbIntlOverrideIndefinitely = new RadioButton();
      this.rdbIntlOverrideTodayOnly = new RadioButton();
      this.rdbIntlOverrideNever = new RadioButton();
      this.gbxDomRatingOverride = new ColorGroupBox();
      this.rdbDomOverrideIndefinitely = new RadioButton();
      this.rdbDomOverrideTodayOnly = new RadioButton();
      this.rdbDomOverrideNever = new RadioButton();
      this.panel1 = new Panel();
      this.chkExpiredRates = new CheckBox();
      this.lblDynamicRatePreview = new Label();
      this.cboDynamicRatePreview = new ComboBox();
      this.chkUseIntlListRates = new CheckBox();
      this.edtIpdNonRevAcctNum = new FdxMaskedEdit();
      this.chkUseListRates = new CheckBox();
      this.chkDisableMpsRateQuoteDialog = new CheckBox();
      this.chkEnableAutoDownloadGroundDiscounts = new CheckBox();
      this.chkDisableRates = new CheckBox();
      this.label1 = new Label();
      this.groupBox1 = new ColorGroupBox();
      this.chkExcludeBonusDisc = new CheckBox();
      this.chkExcludeSPEarnedDisc = new CheckBox();
      this.chkExcludeGrndEarnedDisc = new CheckBox();
      this.chkExcludeExpEarnedDisc = new CheckBox();
      this.colorGroupBox9 = new ColorGroupBox();
      this.edtCompany = new TextBox();
      this.edtContact = new TextBox();
      this.edtZipPostal = new TextBox();
      this.edtStateProvince = new TextBox();
      this.edtCity = new TextBox();
      this.edtAddress2 = new TextBox();
      this.edtAddress1 = new TextBox();
      this.gbxSupportInfo = new ColorGroupBox();
      this.label11 = new Label();
      this.groupBox2 = new GroupBox();
      this.edtOtherEmail = new TextBox();
      this.edtOtherContact = new TextBox();
      this.groupBox3 = new GroupBox();
      this.edtPrimaryContact = new TextBox();
      this.edtPrimaryEmail = new TextBox();
      this.toolTip1 = new ToolTip(this.components);
      this.focusExtender1 = new FocusExtender();
      Label ctl1 = new Label();
      Label ctl2 = new Label();
      Label ctl3 = new Label();
      Label ctl4 = new Label();
      Label ctl5 = new Label();
      Label ctl6 = new Label();
      Label ctl7 = new Label();
      Label ctl8 = new Label();
      Label ctl9 = new Label();
      Label ctl10 = new Label();
      Label ctl11 = new Label();
      Label ctl12 = new Label();
      Label ctl13 = new Label();
      this.tableLayoutPanel2.SuspendLayout();
      this.colorGroupBox12.SuspendLayout();
      this.colorGroupBox1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.colorGroupBox11.SuspendLayout();
      this.colorGroupBox10.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      this.gbxIntlRatingOverride.SuspendLayout();
      this.gbxDomRatingOverride.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.colorGroupBox9.SuspendLayout();
      this.gbxSupportInfo.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) ctl1, "label6");
      ctl1.Name = "label6";
      this.helpProvider1.SetShowHelp((Control) ctl1, (bool) componentResourceManager.GetObject("label6.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl2, "label5");
      ctl2.Name = "label5";
      this.helpProvider1.SetShowHelp((Control) ctl2, (bool) componentResourceManager.GetObject("label5.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl3, "lblContact");
      ctl3.Name = "lblContact";
      this.helpProvider1.SetShowHelp((Control) ctl3, (bool) componentResourceManager.GetObject("lblContact.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl4, "lblZipPostal");
      ctl4.Name = "lblZipPostal";
      this.helpProvider1.SetShowHelp((Control) ctl4, (bool) componentResourceManager.GetObject("lblZipPostal.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl5, "lblStateProvince");
      ctl5.Name = "lblStateProvince";
      this.helpProvider1.SetShowHelp((Control) ctl5, (bool) componentResourceManager.GetObject("lblStateProvince.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl6, "lblCity");
      ctl6.Name = "lblCity";
      this.helpProvider1.SetShowHelp((Control) ctl6, (bool) componentResourceManager.GetObject("lblCity.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl7, "lblAddress2");
      ctl7.Name = "lblAddress2";
      this.helpProvider1.SetShowHelp((Control) ctl7, (bool) componentResourceManager.GetObject("lblAddress2.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl8, "lblAddress1");
      ctl8.Name = "lblAddress1";
      this.helpProvider1.SetShowHelp((Control) ctl8, (bool) componentResourceManager.GetObject("lblAddress1.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl9, "lblCompany");
      ctl9.Name = "lblCompany";
      this.helpProvider1.SetShowHelp((Control) ctl9, (bool) componentResourceManager.GetObject("lblCompany.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl10, "label9");
      ctl10.Name = "label9";
      this.helpProvider1.SetShowHelp((Control) ctl10, (bool) componentResourceManager.GetObject("label9.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl11, "label10");
      ctl11.Name = "label10";
      this.helpProvider1.SetShowHelp((Control) ctl11, (bool) componentResourceManager.GetObject("label10.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl12, "label7");
      ctl12.Name = "label7";
      this.helpProvider1.SetShowHelp((Control) ctl12, (bool) componentResourceManager.GetObject("label7.ShowHelp"));
      componentResourceManager.ApplyResources((object) ctl13, "label8");
      ctl13.Name = "label8";
      this.helpProvider1.SetShowHelp((Control) ctl13, (bool) componentResourceManager.GetObject("label8.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel2, "tableLayoutPanel2");
      this.tableLayoutPanel2.Controls.Add((Control) this.colorGroupBox12, 0, 3);
      this.tableLayoutPanel2.Controls.Add((Control) this.colorGroupBox11, 0, 1);
      this.tableLayoutPanel2.Controls.Add((Control) this.colorGroupBox10, 1, 0);
      this.tableLayoutPanel2.Controls.Add((Control) this.colorGroupBox9, 0, 0);
      this.tableLayoutPanel2.Controls.Add((Control) this.gbxSupportInfo, 0, 2);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.helpProvider1.SetShowHelp((Control) this.tableLayoutPanel2, (bool) componentResourceManager.GetObject("tableLayoutPanel2.ShowHelp"));
      this.colorGroupBox12.BorderThickness = 1f;
      this.tableLayoutPanel2.SetColumnSpan((Control) this.colorGroupBox12, 2);
      this.colorGroupBox12.Controls.Add((Control) this.edtCustomsId);
      this.colorGroupBox12.Controls.Add((Control) this.edtPhoneExt);
      this.colorGroupBox12.Controls.Add((Control) this.lblPhoneExt);
      this.colorGroupBox12.Controls.Add((Control) ctl1);
      this.colorGroupBox12.Controls.Add((Control) ctl2);
      this.colorGroupBox12.Controls.Add((Control) this.edtPhone);
      this.colorGroupBox12.Controls.Add((Control) this.chkCreateDefaultSender);
      this.colorGroupBox12.Controls.Add((Control) this.colorGroupBox1);
      componentResourceManager.ApplyResources((object) this.colorGroupBox12, "colorGroupBox12");
      this.colorGroupBox12.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox12.Name = "colorGroupBox12";
      this.colorGroupBox12.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox12, (bool) componentResourceManager.GetObject("colorGroupBox12.ShowHelp"));
      this.colorGroupBox12.TabStop = false;
      this.edtCustomsId.Allow = "";
      componentResourceManager.ApplyResources((object) this.edtCustomsId, "edtCustomsId");
      this.edtCustomsId.Disallow = "";
      this.edtCustomsId.eMask = eMasks.maskCustom;
      this.edtCustomsId.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtCustomsId, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtCustomsId, SystemColors.WindowText);
      this.edtCustomsId.Mask = "";
      this.edtCustomsId.Name = "edtCustomsId";
      this.helpProvider1.SetShowHelp((Control) this.edtCustomsId, (bool) componentResourceManager.GetObject("edtCustomsId.ShowHelp"));
      this.edtPhoneExt.Allow = "";
      this.edtPhoneExt.Disallow = "";
      this.edtPhoneExt.eMask = eMasks.maskCustom;
      componentResourceManager.ApplyResources((object) this.edtPhoneExt, "edtPhoneExt");
      this.edtPhoneExt.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtPhoneExt, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtPhoneExt, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtPhoneExt, componentResourceManager.GetString("edtPhoneExt.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtPhoneExt, (HelpNavigator) componentResourceManager.GetObject("edtPhoneExt.HelpNavigator"));
      this.edtPhoneExt.Mask = "~! ~999999";
      this.edtPhoneExt.Name = "edtPhoneExt";
      this.helpProvider1.SetShowHelp((Control) this.edtPhoneExt, (bool) componentResourceManager.GetObject("edtPhoneExt.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblPhoneExt, "lblPhoneExt");
      this.lblPhoneExt.Name = "lblPhoneExt";
      this.helpProvider1.SetShowHelp((Control) this.lblPhoneExt, (bool) componentResourceManager.GetObject("lblPhoneExt.ShowHelp"));
      this.edtPhone.Allow = "";
      this.edtPhone.Disallow = "";
      this.edtPhone.eMask = eMasks.maskPhoneTen;
      componentResourceManager.ApplyResources((object) this.edtPhone, "edtPhone");
      this.edtPhone.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtPhone, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtPhone, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtPhone, componentResourceManager.GetString("edtPhone.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtPhone, (HelpNavigator) componentResourceManager.GetObject("edtPhone.HelpNavigator"));
      this.edtPhone.Mask = "(999) 999-9999";
      this.edtPhone.Name = "edtPhone";
      this.helpProvider1.SetShowHelp((Control) this.edtPhone, (bool) componentResourceManager.GetObject("edtPhone.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkCreateDefaultSender, componentResourceManager.GetString("chkCreateDefaultSender.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkCreateDefaultSender, (HelpNavigator) componentResourceManager.GetObject("chkCreateDefaultSender.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkCreateDefaultSender, "chkCreateDefaultSender");
      this.chkCreateDefaultSender.Name = "chkCreateDefaultSender";
      this.helpProvider1.SetShowHelp((Control) this.chkCreateDefaultSender, (bool) componentResourceManager.GetObject("chkCreateDefaultSender.ShowHelp"));
      this.chkCreateDefaultSender.UseVisualStyleBackColor = true;
      this.chkCreateDefaultSender.CheckedChanged += new EventHandler(this.chkCreateDefaultSender_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.colorGroupBox1, "colorGroupBox1");
      this.colorGroupBox1.BorderThickness = 1f;
      this.colorGroupBox1.Controls.Add((Control) this.tableLayoutPanel1);
      this.colorGroupBox1.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox1.Name = "colorGroupBox1";
      this.colorGroupBox1.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox1, (bool) componentResourceManager.GetObject("colorGroupBox1.ShowHelp"));
      this.colorGroupBox1.TabStop = false;
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.label2, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.label3, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.edtLabelPrinter, 1, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.btnReportPrinter, 5, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.btnLabelPrinter, 2, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.edtReportPrinter, 4, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.label4, 3, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      this.helpProvider1.SetShowHelp((Control) this.label2, (bool) componentResourceManager.GetObject("label2.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label3, "label3");
      this.tableLayoutPanel1.SetColumnSpan((Control) this.label3, 6);
      this.label3.ForeColor = Color.Gray;
      this.label3.Name = "label3";
      this.helpProvider1.SetShowHelp((Control) this.label3, (bool) componentResourceManager.GetObject("label3.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.edtLabelPrinter, "edtLabelPrinter");
      this.focusExtender1.SetFocusBackColor((Control) this.edtLabelPrinter, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.edtLabelPrinter, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtLabelPrinter, componentResourceManager.GetString("edtLabelPrinter.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtLabelPrinter, (HelpNavigator) componentResourceManager.GetObject("edtLabelPrinter.HelpNavigator"));
      this.edtLabelPrinter.Name = "edtLabelPrinter";
      this.edtLabelPrinter.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.edtLabelPrinter, (bool) componentResourceManager.GetObject("edtLabelPrinter.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnReportPrinter, "btnReportPrinter");
      this.helpProvider1.SetHelpKeyword((Control) this.btnReportPrinter, componentResourceManager.GetString("btnReportPrinter.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnReportPrinter, (HelpNavigator) componentResourceManager.GetObject("btnReportPrinter.HelpNavigator"));
      this.btnReportPrinter.Image = (Image) Resources.Printer;
      this.btnReportPrinter.Name = "btnReportPrinter";
      this.helpProvider1.SetShowHelp((Control) this.btnReportPrinter, (bool) componentResourceManager.GetObject("btnReportPrinter.ShowHelp"));
      this.toolTip1.SetToolTip((Control) this.btnReportPrinter, componentResourceManager.GetString("btnReportPrinter.ToolTip"));
      this.btnReportPrinter.UseVisualStyleBackColor = true;
      this.btnReportPrinter.Click += new EventHandler(this.btnReportPrinter_Click);
      componentResourceManager.ApplyResources((object) this.btnLabelPrinter, "btnLabelPrinter");
      this.helpProvider1.SetHelpKeyword((Control) this.btnLabelPrinter, componentResourceManager.GetString("btnLabelPrinter.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnLabelPrinter, (HelpNavigator) componentResourceManager.GetObject("btnLabelPrinter.HelpNavigator"));
      this.btnLabelPrinter.Image = (Image) Resources.Printer;
      this.btnLabelPrinter.Name = "btnLabelPrinter";
      this.helpProvider1.SetShowHelp((Control) this.btnLabelPrinter, (bool) componentResourceManager.GetObject("btnLabelPrinter.ShowHelp"));
      this.toolTip1.SetToolTip((Control) this.btnLabelPrinter, componentResourceManager.GetString("btnLabelPrinter.ToolTip"));
      this.btnLabelPrinter.UseVisualStyleBackColor = true;
      this.btnLabelPrinter.Click += new EventHandler(this.btnLabelPrinter_Click);
      componentResourceManager.ApplyResources((object) this.edtReportPrinter, "edtReportPrinter");
      this.focusExtender1.SetFocusBackColor((Control) this.edtReportPrinter, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.edtReportPrinter, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtReportPrinter, componentResourceManager.GetString("edtReportPrinter.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtReportPrinter, (HelpNavigator) componentResourceManager.GetObject("edtReportPrinter.HelpNavigator"));
      this.edtReportPrinter.Name = "edtReportPrinter";
      this.edtReportPrinter.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.edtReportPrinter, (bool) componentResourceManager.GetObject("edtReportPrinter.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label4, "label4");
      this.label4.Name = "label4";
      this.helpProvider1.SetShowHelp((Control) this.label4, (bool) componentResourceManager.GetObject("label4.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.colorGroupBox11, "colorGroupBox11");
      this.colorGroupBox11.BorderThickness = 1f;
      this.colorGroupBox11.Controls.Add((Control) this.chkAutoTabToWeight);
      this.colorGroupBox11.Controls.Add((Control) this.chkEnableHoldFile);
      this.colorGroupBox11.Controls.Add((Control) this.chkAutoPopulateCityState);
      this.colorGroupBox11.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox11.Name = "colorGroupBox11";
      this.colorGroupBox11.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox11, (bool) componentResourceManager.GetObject("colorGroupBox11.ShowHelp"));
      this.colorGroupBox11.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkAutoTabToWeight, componentResourceManager.GetString("chkAutoTabToWeight.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAutoTabToWeight, (HelpNavigator) componentResourceManager.GetObject("chkAutoTabToWeight.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAutoTabToWeight, "chkAutoTabToWeight");
      this.chkAutoTabToWeight.Name = "chkAutoTabToWeight";
      this.helpProvider1.SetShowHelp((Control) this.chkAutoTabToWeight, (bool) componentResourceManager.GetObject("chkAutoTabToWeight.ShowHelp"));
      this.chkAutoTabToWeight.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkEnableHoldFile, componentResourceManager.GetString("chkEnableHoldFile.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkEnableHoldFile, (HelpNavigator) componentResourceManager.GetObject("chkEnableHoldFile.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkEnableHoldFile, "chkEnableHoldFile");
      this.chkEnableHoldFile.Name = "chkEnableHoldFile";
      this.helpProvider1.SetShowHelp((Control) this.chkEnableHoldFile, (bool) componentResourceManager.GetObject("chkEnableHoldFile.ShowHelp"));
      this.chkEnableHoldFile.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkAutoPopulateCityState, componentResourceManager.GetString("chkAutoPopulateCityState.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAutoPopulateCityState, (HelpNavigator) componentResourceManager.GetObject("chkAutoPopulateCityState.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAutoPopulateCityState, "chkAutoPopulateCityState");
      this.chkAutoPopulateCityState.Name = "chkAutoPopulateCityState";
      this.helpProvider1.SetShowHelp((Control) this.chkAutoPopulateCityState, (bool) componentResourceManager.GetObject("chkAutoPopulateCityState.ShowHelp"));
      this.chkAutoPopulateCityState.UseVisualStyleBackColor = true;
      this.colorGroupBox10.BorderThickness = 1f;
      this.colorGroupBox10.Controls.Add((Control) this.tableLayoutPanel3);
      componentResourceManager.ApplyResources((object) this.colorGroupBox10, "colorGroupBox10");
      this.colorGroupBox10.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox10.Name = "colorGroupBox10";
      this.colorGroupBox10.RoundCorners = 5;
      this.tableLayoutPanel2.SetRowSpan((Control) this.colorGroupBox10, 3);
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox10, (bool) componentResourceManager.GetObject("colorGroupBox10.ShowHelp"));
      this.colorGroupBox10.TabStop = false;
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel3, "tableLayoutPanel3");
      this.tableLayoutPanel3.Controls.Add((Control) this.gbxIntlRatingOverride, 0, 1);
      this.tableLayoutPanel3.Controls.Add((Control) this.gbxDomRatingOverride, 0, 0);
      this.tableLayoutPanel3.Controls.Add((Control) this.panel1, 0, 3);
      this.tableLayoutPanel3.Controls.Add((Control) this.groupBox1, 0, 2);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.helpProvider1.SetShowHelp((Control) this.tableLayoutPanel3, (bool) componentResourceManager.GetObject("tableLayoutPanel3.ShowHelp"));
      this.gbxIntlRatingOverride.BorderThickness = 1f;
      this.gbxIntlRatingOverride.Controls.Add((Control) this.rdbIntlOverrideIndefinitely);
      this.gbxIntlRatingOverride.Controls.Add((Control) this.rdbIntlOverrideTodayOnly);
      this.gbxIntlRatingOverride.Controls.Add((Control) this.rdbIntlOverrideNever);
      componentResourceManager.ApplyResources((object) this.gbxIntlRatingOverride, "gbxIntlRatingOverride");
      this.gbxIntlRatingOverride.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxIntlRatingOverride.Name = "gbxIntlRatingOverride";
      this.gbxIntlRatingOverride.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxIntlRatingOverride, (bool) componentResourceManager.GetObject("gbxIntlRatingOverride.ShowHelp"));
      this.gbxIntlRatingOverride.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdbIntlOverrideIndefinitely, componentResourceManager.GetString("rdbIntlOverrideIndefinitely.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbIntlOverrideIndefinitely, (HelpNavigator) componentResourceManager.GetObject("rdbIntlOverrideIndefinitely.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbIntlOverrideIndefinitely, "rdbIntlOverrideIndefinitely");
      this.rdbIntlOverrideIndefinitely.Name = "rdbIntlOverrideIndefinitely";
      this.helpProvider1.SetShowHelp((Control) this.rdbIntlOverrideIndefinitely, (bool) componentResourceManager.GetObject("rdbIntlOverrideIndefinitely.ShowHelp"));
      this.rdbIntlOverrideIndefinitely.TabStop = true;
      this.rdbIntlOverrideIndefinitely.UseVisualStyleBackColor = true;
      this.rdbIntlOverrideIndefinitely.CheckedChanged += new EventHandler(this.overrideInternationalRating_CheckChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbIntlOverrideTodayOnly, componentResourceManager.GetString("rdbIntlOverrideTodayOnly.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbIntlOverrideTodayOnly, (HelpNavigator) componentResourceManager.GetObject("rdbIntlOverrideTodayOnly.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbIntlOverrideTodayOnly, "rdbIntlOverrideTodayOnly");
      this.rdbIntlOverrideTodayOnly.Name = "rdbIntlOverrideTodayOnly";
      this.helpProvider1.SetShowHelp((Control) this.rdbIntlOverrideTodayOnly, (bool) componentResourceManager.GetObject("rdbIntlOverrideTodayOnly.ShowHelp"));
      this.rdbIntlOverrideTodayOnly.TabStop = true;
      this.rdbIntlOverrideTodayOnly.UseVisualStyleBackColor = true;
      this.rdbIntlOverrideTodayOnly.CheckedChanged += new EventHandler(this.overrideInternationalRating_CheckChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbIntlOverrideNever, componentResourceManager.GetString("rdbIntlOverrideNever.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbIntlOverrideNever, (HelpNavigator) componentResourceManager.GetObject("rdbIntlOverrideNever.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbIntlOverrideNever, "rdbIntlOverrideNever");
      this.rdbIntlOverrideNever.Name = "rdbIntlOverrideNever";
      this.helpProvider1.SetShowHelp((Control) this.rdbIntlOverrideNever, (bool) componentResourceManager.GetObject("rdbIntlOverrideNever.ShowHelp"));
      this.rdbIntlOverrideNever.TabStop = true;
      this.rdbIntlOverrideNever.UseVisualStyleBackColor = true;
      this.rdbIntlOverrideNever.CheckedChanged += new EventHandler(this.overrideInternationalRating_CheckChanged);
      this.gbxDomRatingOverride.BorderThickness = 1f;
      this.gbxDomRatingOverride.Controls.Add((Control) this.rdbDomOverrideIndefinitely);
      this.gbxDomRatingOverride.Controls.Add((Control) this.rdbDomOverrideTodayOnly);
      this.gbxDomRatingOverride.Controls.Add((Control) this.rdbDomOverrideNever);
      componentResourceManager.ApplyResources((object) this.gbxDomRatingOverride, "gbxDomRatingOverride");
      this.gbxDomRatingOverride.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxDomRatingOverride.Name = "gbxDomRatingOverride";
      this.gbxDomRatingOverride.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxDomRatingOverride, (bool) componentResourceManager.GetObject("gbxDomRatingOverride.ShowHelp"));
      this.gbxDomRatingOverride.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdbDomOverrideIndefinitely, componentResourceManager.GetString("rdbDomOverrideIndefinitely.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbDomOverrideIndefinitely, (HelpNavigator) componentResourceManager.GetObject("rdbDomOverrideIndefinitely.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbDomOverrideIndefinitely, "rdbDomOverrideIndefinitely");
      this.rdbDomOverrideIndefinitely.Name = "rdbDomOverrideIndefinitely";
      this.helpProvider1.SetShowHelp((Control) this.rdbDomOverrideIndefinitely, (bool) componentResourceManager.GetObject("rdbDomOverrideIndefinitely.ShowHelp"));
      this.rdbDomOverrideIndefinitely.TabStop = true;
      this.rdbDomOverrideIndefinitely.UseVisualStyleBackColor = true;
      this.rdbDomOverrideIndefinitely.CheckedChanged += new EventHandler(this.overrideDomesticRating_CheckChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbDomOverrideTodayOnly, componentResourceManager.GetString("rdbDomOverrideTodayOnly.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbDomOverrideTodayOnly, (HelpNavigator) componentResourceManager.GetObject("rdbDomOverrideTodayOnly.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbDomOverrideTodayOnly, "rdbDomOverrideTodayOnly");
      this.rdbDomOverrideTodayOnly.Name = "rdbDomOverrideTodayOnly";
      this.helpProvider1.SetShowHelp((Control) this.rdbDomOverrideTodayOnly, (bool) componentResourceManager.GetObject("rdbDomOverrideTodayOnly.ShowHelp"));
      this.rdbDomOverrideTodayOnly.TabStop = true;
      this.rdbDomOverrideTodayOnly.UseVisualStyleBackColor = true;
      this.rdbDomOverrideTodayOnly.CheckedChanged += new EventHandler(this.overrideDomesticRating_CheckChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbDomOverrideNever, componentResourceManager.GetString("rdbDomOverrideNever.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbDomOverrideNever, (HelpNavigator) componentResourceManager.GetObject("rdbDomOverrideNever.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbDomOverrideNever, "rdbDomOverrideNever");
      this.rdbDomOverrideNever.Name = "rdbDomOverrideNever";
      this.helpProvider1.SetShowHelp((Control) this.rdbDomOverrideNever, (bool) componentResourceManager.GetObject("rdbDomOverrideNever.ShowHelp"));
      this.rdbDomOverrideNever.TabStop = true;
      this.rdbDomOverrideNever.UseVisualStyleBackColor = true;
      this.rdbDomOverrideNever.CheckedChanged += new EventHandler(this.overrideDomesticRating_CheckChanged);
      this.panel1.Controls.Add((Control) this.chkExpiredRates);
      this.panel1.Controls.Add((Control) this.lblDynamicRatePreview);
      this.panel1.Controls.Add((Control) this.cboDynamicRatePreview);
      this.panel1.Controls.Add((Control) this.chkUseIntlListRates);
      this.panel1.Controls.Add((Control) this.edtIpdNonRevAcctNum);
      this.panel1.Controls.Add((Control) this.chkUseListRates);
      this.panel1.Controls.Add((Control) this.chkDisableMpsRateQuoteDialog);
      this.panel1.Controls.Add((Control) this.chkEnableAutoDownloadGroundDiscounts);
      this.panel1.Controls.Add((Control) this.chkDisableRates);
      this.panel1.Controls.Add((Control) this.label1);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      this.helpProvider1.SetShowHelp((Control) this.panel1, (bool) componentResourceManager.GetObject("panel1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.chkExpiredRates, "chkExpiredRates");
      this.chkExpiredRates.Name = "chkExpiredRates";
      this.chkExpiredRates.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.lblDynamicRatePreview, "lblDynamicRatePreview");
      this.lblDynamicRatePreview.Name = "lblDynamicRatePreview";
      this.cboDynamicRatePreview.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      this.cboDynamicRatePreview.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.cboDynamicRatePreview.DropDownStyle = ComboBoxStyle.DropDownList;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDynamicRatePreview, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDynamicRatePreview, SystemColors.WindowText);
      this.cboDynamicRatePreview.FormattingEnabled = true;
      componentResourceManager.ApplyResources((object) this.cboDynamicRatePreview, "cboDynamicRatePreview");
      this.cboDynamicRatePreview.Name = "cboDynamicRatePreview";
      this.helpProvider1.SetHelpKeyword((Control) this.chkUseIntlListRates, componentResourceManager.GetString("chkUseIntlListRates.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkUseIntlListRates, (HelpNavigator) componentResourceManager.GetObject("chkUseIntlListRates.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkUseIntlListRates, "chkUseIntlListRates");
      this.chkUseIntlListRates.Name = "chkUseIntlListRates";
      this.helpProvider1.SetShowHelp((Control) this.chkUseIntlListRates, (bool) componentResourceManager.GetObject("chkUseIntlListRates.ShowHelp"));
      this.chkUseIntlListRates.UseVisualStyleBackColor = true;
      this.chkUseIntlListRates.CheckedChanged += new EventHandler(this.chkUseListRates_CheckedChanged);
      this.edtIpdNonRevAcctNum.Allow = "";
      this.edtIpdNonRevAcctNum.Disallow = "";
      this.edtIpdNonRevAcctNum.eMask = eMasks.maskCustom;
      this.edtIpdNonRevAcctNum.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtIpdNonRevAcctNum, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtIpdNonRevAcctNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtIpdNonRevAcctNum, componentResourceManager.GetString("edtIpdNonRevAcctNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtIpdNonRevAcctNum, (HelpNavigator) componentResourceManager.GetObject("edtIpdNonRevAcctNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtIpdNonRevAcctNum, "edtIpdNonRevAcctNum");
      this.edtIpdNonRevAcctNum.Mask = "~! ~999999999";
      this.edtIpdNonRevAcctNum.Name = "edtIpdNonRevAcctNum";
      this.helpProvider1.SetShowHelp((Control) this.edtIpdNonRevAcctNum, (bool) componentResourceManager.GetObject("edtIpdNonRevAcctNum.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkUseListRates, componentResourceManager.GetString("chkUseListRates.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkUseListRates, (HelpNavigator) componentResourceManager.GetObject("chkUseListRates.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkUseListRates, "chkUseListRates");
      this.chkUseListRates.Name = "chkUseListRates";
      this.helpProvider1.SetShowHelp((Control) this.chkUseListRates, (bool) componentResourceManager.GetObject("chkUseListRates.ShowHelp"));
      this.chkUseListRates.UseVisualStyleBackColor = true;
      this.chkUseListRates.CheckedChanged += new EventHandler(this.chkUseListRates_CheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.chkDisableMpsRateQuoteDialog, componentResourceManager.GetString("chkDisableMpsRateQuoteDialog.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkDisableMpsRateQuoteDialog, (HelpNavigator) componentResourceManager.GetObject("chkDisableMpsRateQuoteDialog.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkDisableMpsRateQuoteDialog, "chkDisableMpsRateQuoteDialog");
      this.chkDisableMpsRateQuoteDialog.Name = "chkDisableMpsRateQuoteDialog";
      this.helpProvider1.SetShowHelp((Control) this.chkDisableMpsRateQuoteDialog, (bool) componentResourceManager.GetObject("chkDisableMpsRateQuoteDialog.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.chkEnableAutoDownloadGroundDiscounts, "chkEnableAutoDownloadGroundDiscounts");
      this.helpProvider1.SetHelpKeyword((Control) this.chkEnableAutoDownloadGroundDiscounts, componentResourceManager.GetString("chkEnableAutoDownloadGroundDiscounts.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkEnableAutoDownloadGroundDiscounts, (HelpNavigator) componentResourceManager.GetObject("chkEnableAutoDownloadGroundDiscounts.HelpNavigator"));
      this.chkEnableAutoDownloadGroundDiscounts.Name = "chkEnableAutoDownloadGroundDiscounts";
      this.helpProvider1.SetShowHelp((Control) this.chkEnableAutoDownloadGroundDiscounts, (bool) componentResourceManager.GetObject("chkEnableAutoDownloadGroundDiscounts.ShowHelp"));
      this.chkEnableAutoDownloadGroundDiscounts.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkDisableRates, componentResourceManager.GetString("chkDisableRates.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkDisableRates, (HelpNavigator) componentResourceManager.GetObject("chkDisableRates.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkDisableRates, "chkDisableRates");
      this.chkDisableRates.Name = "chkDisableRates";
      this.helpProvider1.SetShowHelp((Control) this.chkDisableRates, (bool) componentResourceManager.GetObject("chkDisableRates.ShowHelp"));
      this.chkDisableRates.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.helpProvider1.SetShowHelp((Control) this.label1, (bool) componentResourceManager.GetObject("label1.ShowHelp"));
      this.groupBox1.BorderThickness = 1f;
      this.groupBox1.Controls.Add((Control) this.chkExcludeBonusDisc);
      this.groupBox1.Controls.Add((Control) this.chkExcludeSPEarnedDisc);
      this.groupBox1.Controls.Add((Control) this.chkExcludeGrndEarnedDisc);
      this.groupBox1.Controls.Add((Control) this.chkExcludeExpEarnedDisc);
      componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
      this.groupBox1.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.RoundCorners = 5;
      this.groupBox1.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkExcludeBonusDisc, componentResourceManager.GetString("chkExcludeBonusDisc.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkExcludeBonusDisc, (HelpNavigator) componentResourceManager.GetObject("chkExcludeBonusDisc.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkExcludeBonusDisc, "chkExcludeBonusDisc");
      this.chkExcludeBonusDisc.Name = "chkExcludeBonusDisc";
      this.helpProvider1.SetShowHelp((Control) this.chkExcludeBonusDisc, (bool) componentResourceManager.GetObject("chkExcludeBonusDisc.ShowHelp"));
      this.chkExcludeBonusDisc.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkExcludeSPEarnedDisc, componentResourceManager.GetString("chkExcludeSPEarnedDisc.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkExcludeSPEarnedDisc, (HelpNavigator) componentResourceManager.GetObject("chkExcludeSPEarnedDisc.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkExcludeSPEarnedDisc, "chkExcludeSPEarnedDisc");
      this.chkExcludeSPEarnedDisc.Name = "chkExcludeSPEarnedDisc";
      this.helpProvider1.SetShowHelp((Control) this.chkExcludeSPEarnedDisc, (bool) componentResourceManager.GetObject("chkExcludeSPEarnedDisc.ShowHelp"));
      this.chkExcludeSPEarnedDisc.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkExcludeGrndEarnedDisc, componentResourceManager.GetString("chkExcludeGrndEarnedDisc.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkExcludeGrndEarnedDisc, (HelpNavigator) componentResourceManager.GetObject("chkExcludeGrndEarnedDisc.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkExcludeGrndEarnedDisc, "chkExcludeGrndEarnedDisc");
      this.chkExcludeGrndEarnedDisc.Name = "chkExcludeGrndEarnedDisc";
      this.helpProvider1.SetShowHelp((Control) this.chkExcludeGrndEarnedDisc, (bool) componentResourceManager.GetObject("chkExcludeGrndEarnedDisc.ShowHelp"));
      this.chkExcludeGrndEarnedDisc.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkExcludeExpEarnedDisc, componentResourceManager.GetString("chkExcludeExpEarnedDisc.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkExcludeExpEarnedDisc, (HelpNavigator) componentResourceManager.GetObject("chkExcludeExpEarnedDisc.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkExcludeExpEarnedDisc, "chkExcludeExpEarnedDisc");
      this.chkExcludeExpEarnedDisc.Name = "chkExcludeExpEarnedDisc";
      this.helpProvider1.SetShowHelp((Control) this.chkExcludeExpEarnedDisc, (bool) componentResourceManager.GetObject("chkExcludeExpEarnedDisc.ShowHelp"));
      this.chkExcludeExpEarnedDisc.UseVisualStyleBackColor = true;
      this.colorGroupBox9.BorderThickness = 1f;
      this.colorGroupBox9.Controls.Add((Control) this.edtCompany);
      this.colorGroupBox9.Controls.Add((Control) ctl3);
      this.colorGroupBox9.Controls.Add((Control) this.edtContact);
      this.colorGroupBox9.Controls.Add((Control) ctl4);
      this.colorGroupBox9.Controls.Add((Control) this.edtZipPostal);
      this.colorGroupBox9.Controls.Add((Control) ctl5);
      this.colorGroupBox9.Controls.Add((Control) this.edtStateProvince);
      this.colorGroupBox9.Controls.Add((Control) ctl6);
      this.colorGroupBox9.Controls.Add((Control) this.edtCity);
      this.colorGroupBox9.Controls.Add((Control) ctl7);
      this.colorGroupBox9.Controls.Add((Control) this.edtAddress2);
      this.colorGroupBox9.Controls.Add((Control) ctl8);
      this.colorGroupBox9.Controls.Add((Control) this.edtAddress1);
      this.colorGroupBox9.Controls.Add((Control) ctl9);
      componentResourceManager.ApplyResources((object) this.colorGroupBox9, "colorGroupBox9");
      this.colorGroupBox9.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox9.Name = "colorGroupBox9";
      this.colorGroupBox9.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox9, (bool) componentResourceManager.GetObject("colorGroupBox9.ShowHelp"));
      this.colorGroupBox9.TabStop = false;
      componentResourceManager.ApplyResources((object) this.edtCompany, "edtCompany");
      this.focusExtender1.SetFocusBackColor((Control) this.edtCompany, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtCompany, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtCompany, componentResourceManager.GetString("edtCompany.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtCompany, (HelpNavigator) componentResourceManager.GetObject("edtCompany.HelpNavigator"));
      this.edtCompany.Name = "edtCompany";
      this.helpProvider1.SetShowHelp((Control) this.edtCompany, (bool) componentResourceManager.GetObject("edtCompany.ShowHelp"));
      this.edtCompany.TextChanged += new EventHandler(this.demographicsChanged);
      componentResourceManager.ApplyResources((object) this.edtContact, "edtContact");
      this.focusExtender1.SetFocusBackColor((Control) this.edtContact, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtContact, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtContact, componentResourceManager.GetString("edtContact.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtContact, (HelpNavigator) componentResourceManager.GetObject("edtContact.HelpNavigator"));
      this.edtContact.Name = "edtContact";
      this.helpProvider1.SetShowHelp((Control) this.edtContact, (bool) componentResourceManager.GetObject("edtContact.ShowHelp"));
      this.edtContact.TextChanged += new EventHandler(this.demographicsChanged);
      componentResourceManager.ApplyResources((object) this.edtZipPostal, "edtZipPostal");
      this.focusExtender1.SetFocusBackColor((Control) this.edtZipPostal, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.edtZipPostal, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtZipPostal, componentResourceManager.GetString("edtZipPostal.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtZipPostal, (HelpNavigator) componentResourceManager.GetObject("edtZipPostal.HelpNavigator"));
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
      componentResourceManager.ApplyResources((object) this.edtCity, "edtCity");
      this.focusExtender1.SetFocusBackColor((Control) this.edtCity, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.edtCity, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtCity, componentResourceManager.GetString("edtCity.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtCity, (HelpNavigator) componentResourceManager.GetObject("edtCity.HelpNavigator"));
      this.edtCity.Name = "edtCity";
      this.edtCity.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.edtCity, (bool) componentResourceManager.GetObject("edtCity.ShowHelp"));
      this.edtCity.TabStop = false;
      componentResourceManager.ApplyResources((object) this.edtAddress2, "edtAddress2");
      this.focusExtender1.SetFocusBackColor((Control) this.edtAddress2, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtAddress2, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtAddress2, componentResourceManager.GetString("edtAddress2.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtAddress2, (HelpNavigator) componentResourceManager.GetObject("edtAddress2.HelpNavigator"));
      this.edtAddress2.Name = "edtAddress2";
      this.helpProvider1.SetShowHelp((Control) this.edtAddress2, (bool) componentResourceManager.GetObject("edtAddress2.ShowHelp"));
      this.edtAddress2.TextChanged += new EventHandler(this.demographicsChanged);
      componentResourceManager.ApplyResources((object) this.edtAddress1, "edtAddress1");
      this.focusExtender1.SetFocusBackColor((Control) this.edtAddress1, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtAddress1, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtAddress1, componentResourceManager.GetString("edtAddress1.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtAddress1, (HelpNavigator) componentResourceManager.GetObject("edtAddress1.HelpNavigator"));
      this.edtAddress1.Name = "edtAddress1";
      this.helpProvider1.SetShowHelp((Control) this.edtAddress1, (bool) componentResourceManager.GetObject("edtAddress1.ShowHelp"));
      this.edtAddress1.TextChanged += new EventHandler(this.demographicsChanged);
      this.gbxSupportInfo.BorderThickness = 1f;
      this.gbxSupportInfo.Controls.Add((Control) this.label11);
      this.gbxSupportInfo.Controls.Add((Control) this.groupBox2);
      this.gbxSupportInfo.Controls.Add((Control) this.groupBox3);
      componentResourceManager.ApplyResources((object) this.gbxSupportInfo, "gbxSupportInfo");
      this.gbxSupportInfo.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxSupportInfo.Name = "gbxSupportInfo";
      this.gbxSupportInfo.RoundCorners = 5;
      this.gbxSupportInfo.TabStop = false;
      componentResourceManager.ApplyResources((object) this.label11, "label11");
      this.label11.ForeColor = Color.Gray;
      this.label11.Name = "label11";
      this.helpProvider1.SetShowHelp((Control) this.label11, (bool) componentResourceManager.GetObject("label11.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
      this.groupBox2.Controls.Add((Control) ctl10);
      this.groupBox2.Controls.Add((Control) this.edtOtherEmail);
      this.groupBox2.Controls.Add((Control) ctl11);
      this.groupBox2.Controls.Add((Control) this.edtOtherContact);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.TabStop = false;
      componentResourceManager.ApplyResources((object) this.edtOtherEmail, "edtOtherEmail");
      this.focusExtender1.SetFocusBackColor((Control) this.edtOtherEmail, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtOtherEmail, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtOtherEmail, componentResourceManager.GetString("edtOtherEmail.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtOtherEmail, (HelpNavigator) componentResourceManager.GetObject("edtOtherEmail.HelpNavigator"));
      this.edtOtherEmail.Name = "edtOtherEmail";
      this.helpProvider1.SetShowHelp((Control) this.edtOtherEmail, (bool) componentResourceManager.GetObject("edtOtherEmail.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.edtOtherContact, "edtOtherContact");
      this.focusExtender1.SetFocusBackColor((Control) this.edtOtherContact, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtOtherContact, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtOtherContact, componentResourceManager.GetString("edtOtherContact.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtOtherContact, (HelpNavigator) componentResourceManager.GetObject("edtOtherContact.HelpNavigator"));
      this.edtOtherContact.Name = "edtOtherContact";
      this.helpProvider1.SetShowHelp((Control) this.edtOtherContact, (bool) componentResourceManager.GetObject("edtOtherContact.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
      this.groupBox3.Controls.Add((Control) this.edtPrimaryContact);
      this.groupBox3.Controls.Add((Control) ctl12);
      this.groupBox3.Controls.Add((Control) this.edtPrimaryEmail);
      this.groupBox3.Controls.Add((Control) ctl13);
      this.groupBox3.Name = "groupBox3";
      this.helpProvider1.SetShowHelp((Control) this.groupBox3, (bool) componentResourceManager.GetObject("groupBox3.ShowHelp"));
      this.groupBox3.TabStop = false;
      componentResourceManager.ApplyResources((object) this.edtPrimaryContact, "edtPrimaryContact");
      this.focusExtender1.SetFocusBackColor((Control) this.edtPrimaryContact, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtPrimaryContact, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtPrimaryContact, componentResourceManager.GetString("edtPrimaryContact.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtPrimaryContact, (HelpNavigator) componentResourceManager.GetObject("edtPrimaryContact.HelpNavigator"));
      this.edtPrimaryContact.Name = "edtPrimaryContact";
      this.helpProvider1.SetShowHelp((Control) this.edtPrimaryContact, (bool) componentResourceManager.GetObject("edtPrimaryContact.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.edtPrimaryEmail, "edtPrimaryEmail");
      this.focusExtender1.SetFocusBackColor((Control) this.edtPrimaryEmail, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtPrimaryEmail, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtPrimaryEmail, componentResourceManager.GetString("edtPrimaryEmail.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtPrimaryEmail, (HelpNavigator) componentResourceManager.GetObject("edtPrimaryEmail.HelpNavigator"));
      this.edtPrimaryEmail.Name = "edtPrimaryEmail";
      this.helpProvider1.SetShowHelp((Control) this.edtPrimaryEmail, (bool) componentResourceManager.GetObject("edtPrimaryEmail.ShowHelp"));
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanel2);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (SystemSettingsCustomerAdmin);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.tableLayoutPanel2.ResumeLayout(false);
      this.colorGroupBox12.ResumeLayout(false);
      this.colorGroupBox12.PerformLayout();
      this.colorGroupBox1.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.colorGroupBox11.ResumeLayout(false);
      this.colorGroupBox10.ResumeLayout(false);
      this.tableLayoutPanel3.ResumeLayout(false);
      this.gbxIntlRatingOverride.ResumeLayout(false);
      this.gbxDomRatingOverride.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.colorGroupBox9.ResumeLayout(false);
      this.colorGroupBox9.PerformLayout();
      this.gbxSupportInfo.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
