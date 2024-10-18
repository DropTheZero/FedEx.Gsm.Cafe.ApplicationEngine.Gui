// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SystemSettingsUser
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.DatabaseForms;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Properties;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.DataAccess;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class SystemSettingsUser : UserControlHelpEx
  {
    private bool _bEventsInitialized;
    private SystemSettings _myParent;
    private const string monthDayYear = "mm/dd/yyyy";
    private const string dayMonthYear = "dd/mmm/yyyy";
    private bool _languageChanged;
    private bool _fontSizeChanged;
    private bool _etdChanged;
    private bool _returnsChanged;
    private bool _showEtdImagePrompt;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private ColorGroupBox gbxFegCloseSettings;
    private ColorGroupBox gbxLabelInformation;
    private ColorGroupBox gbxCommOverride;
    private ColorGroupBox gbxShipDbPurge;
    private ColorGroupBox gbxInterfacePrefs;
    private ColorGroupBox gbxExpressIntlSettings;
    private ColorGroupBox gbxSystemSettings;
    private CheckBox chkDisplayPickupTab;
    private Label label3;
    private ComboBox cboDateFormat;
    private Label label2;
    private ComboBox cboLanguage;
    private Label label1;
    private Label label5;
    private CheckBox chkAutoTab;
    private CheckBox chkNonResiIimporterStatus;
    private CheckBox chkScaleIsAttached;
    private CheckBox chkRequireLogin;
    private TextBox edtOverrideCode;
    private Label label9;
    private Label label6;
    private CheckBox chkDownloadFromEEI;
    private Label txtSampleText;
    private ColorDialog colorDialogFieldBackColor;
    private Button btnFieldColor;
    private ToolTip toolTip1;
    private FdxMaskedEdit edtCurrentMaxPurge;
    private FdxMaskedEdit edtCurrentMinPurge;
    private ColorGroupBox gbxCloseSettings;
    private FdxMaskedEdit edtGroundAutoCloseTime;
    private Label lblGroundClose;
    private FdxMaskedEdit edtExpressAutoCloseTime;
    private Label lblExpressClose;
    private FocusExtender focusExtender1;
    private FlowLayoutPanel flwLeftColumn;
    private FlowLayoutPanel flwRightColumn;
    private ColorGroupBox gbxElectronicTradeDocuments;
    private CheckBox chkActivateEtd;
    private LinkLabelEx llblEtdUploadDescription;
    private Label lblEtdCommunicationTitle;
    private RadioButton rdbEtdUploadAtShipment;
    private RadioButton rdbUploadEtdHourly;
    private FdxMaskedEdit edtSmartpostAutoCloseTime;
    private Label lblSmartpostClose;
    private FlowLayoutPanel pnlEtdInformation;
    private CheckBox chkPreReadScale;
    private Panel pnlGroundClose;
    private Panel panelFreightPurge;
    private Label lblFreightMax;
    private FdxMaskedEdit edtFreightMinPurge;
    private FdxMaskedEdit edtFreightMaxPurge;
    private Label lblFreightMin;
    private FlowLayoutPanel pnlEtdSettings;
    private ColorGroupBox gbxSmartpostReturns;
    private LinkLabelEx lnkAddSmartpostReturnAddresses;
    private CheckBox chkAddSmartpostReturnAddresses;
    private CheckBox chkEnableReturnShipping;
    private Label lblFontSize;
    private ComboBox cboFontSize;
    private Label lblScaleTimeout;
    private ComboBox cboScaleTimeout;
    private PanelEx pnlPrintGroundReadableBarcode;
    private LinkLabel lnkPrintGroundReadableBarcode;
    private CheckBox chkPrintGroundReadableBarcode;
    private Panel pnlGCTCommunicationsSettings;
    private RadioButton rdbGCTuploadHourly;
    private RadioButton rdbGCTuploadAtShiptime;
    private Label lblGCTcommunicationsHeader;
    private Label lblGCTcommunicationDescription;
    private Panel pnlOneRateDisplay;
    private CheckBox chkOneRateStatus;
    private PanelEx pnlHoldLocation;
    private LinkLabel lnkHoldLocations;
    private CheckBox chkFedExHoldLocations;

    public SystemSettings MyParent
    {
      get => this._myParent;
      set => this._myParent = value;
    }

    public DateTime ExpressAutoCloseTime
    {
      set
      {
        this.edtExpressAutoCloseTime.Text = value.ToString("hh:mm tt", (IFormatProvider) CultureInfo.InvariantCulture.DateTimeFormat);
      }
    }

    internal bool HasLanguageChanged => this._languageChanged;

    internal bool HasFontSizeChanged => this._fontSizeChanged;

    internal bool IsPreReadChecked => this.chkPreReadScale.Checked;

    internal bool EtdChanged => this._etdChanged;

    internal bool ReturnsChanged => this._returnsChanged;

    internal bool ShowEtdImagePrompt => this._showEtdImagePrompt;

    private bool IsFreightEnabled
    {
      get
      {
        bool bVal;
        return GuiData.ConfigManager.GetProfileBool("SHIPNET2000/GUI/SETTINGS", "EnableFreight", out bVal) & bVal;
      }
    }

    public SystemSettingsUser()
    {
      this.InitializeComponent();
      this.cboDateFormat.Items.Add((object) "mm/dd/yyyy");
      this.cboDateFormat.Items.Add((object) "dd/mmm/yyyy");
      this.edtCurrentMaxPurge.SetMask("999");
      this.edtCurrentMinPurge.SetMask("999");
    }

    public void ScreenToObjects(SystemSettingsObject settings)
    {
      if (this.edtOverrideCode.Text.Length > 0)
      {
        CommOverrideResponse overrideResponse = GuiData.AppController.ShipEngine.ValidateCommOverrideCode(settings.AccountObject.MeterNumber, this.edtOverrideCode.Text);
        switch (overrideResponse.OverrideType)
        {
          case CommOverrideResponse.OverrideCodeType.Invalid:
            throw new TabPageException(this.Parent as TabPage, (Control) this.edtOverrideCode, overrideResponse.Error);
          case CommOverrideResponse.OverrideCodeType.Daily:
            settings.RegistrySettings["DTChkReactivateDate"].Value = (object) DateTime.Now.AddHours(48.0);
            break;
          case CommOverrideResponse.OverrideCodeType.Disaster:
            settings.RegistrySettings["DTChkReactivateDate"].Value = (object) DateTime.Now.AddDays(7.0);
            break;
        }
      }
      if (settings.AccountObject != null)
      {
        settings.AccountObject.Culture = (string) this.cboLanguage.SelectedValue;
        settings.AccountObject.DateFormat = this.cboDateFormat.Text ?? "mm/dd/yyyy";
        short result1 = 0;
        short.TryParse(this.edtCurrentMinPurge.Raw, out result1);
        settings.AccountObject.MinHistoryPurgeDays = result1;
        short.TryParse(this.edtCurrentMaxPurge.Raw, out result1);
        settings.AccountObject.MaxHistoryPurgeDays = result1;
        settings.AccountObject.UserSelectShippingCurrency = this.MyParent.ExpressAdminPage.Country == "US";
        settings.AccountObject.RequireUserLogin = this.chkRequireLogin.Checked;
        settings.AccountObject.DownLoadFromFedExSEDTool = this.chkDownloadFromEEI.Checked;
        settings.AccountObject.DCSShipper = false;
        settings.AccountObject.IsPickupEnabled = this.chkDisplayPickupTab.Checked;
        settings.AccountObject.IsNonResidentImporter = this.chkNonResiIimporterStatus.Checked;
        if (settings.AccountObject.BoxInfo == null)
          settings.AccountObject.BoxInfo = new Box();
        settings.AccountObject.BoxInfo.ScaleAttached = this.chkScaleIsAttached.Checked;
        DateTime result2 = DateTime.Now;
        if (!DateTime.TryParse(this.edtExpressAutoCloseTime.Text, out result2))
          throw new TabPageException(this.Parent as TabPage, (Control) this.edtExpressAutoCloseTime, 3610);
        settings.AccountObject.AutoCloseTime = result2;
        DateTime result3 = DateTime.Now;
        if (!DateTime.TryParse(this.edtGroundAutoCloseTime.Text, out result3))
          throw new TabPageException(this.Parent as TabPage, (Control) this.edtGroundAutoCloseTime, 13761);
        settings.AccountObject.GroundAutoCloseTime = result3;
        if (settings.AccountObject.IsSmartPostEnabled)
        {
          result3 = DateTime.Now;
          if (DateTime.TryParse(this.edtSmartpostAutoCloseTime.Text, out result3))
          {
            settings.AccountObject.SPAutoCloseEnabled = true;
            settings.AccountObject.SPAutoCloseTime = result3;
          }
          else if (!string.IsNullOrEmpty(this.edtSmartpostAutoCloseTime.Text))
          {
            settings.AccountObject.SPAutoCloseEnabled = true;
            settings.AccountObject.SPAutoCloseTime = new DateTime(1, 1, 1, 0, 0, 0);
          }
        }
        this._returnsChanged = this.chkEnableReturnShipping.Checked != settings.AccountObject.ReturnShippingEnabled;
        settings.AccountObject.ReturnShippingEnabled = this.chkEnableReturnShipping.Checked;
        if (this.chkAddSmartpostReturnAddresses.Visible)
          settings.AccountObject.SPReturnAddressEntryEnabled = this.chkAddSmartpostReturnAddresses.Checked;
        settings.AccountObject.GCTUploadTime = !this.rdbGCTuploadHourly.Checked ? GCTUploadTimeOption.AtShipmentTime : GCTUploadTimeOption.AtRegularHourlyUpload;
        short.TryParse(this.edtFreightMinPurge.Raw, out result1);
        settings.AccountObject.FreightLTLPurgeMin = (int) result1;
        short.TryParse(this.edtFreightMaxPurge.Raw, out result1);
        settings.AccountObject.FreightLTLPurgeMax = (int) result1;
        this._etdChanged = this.chkActivateEtd.Checked != settings.AccountObject.ETDEnabled;
        this._showEtdImagePrompt = this.chkActivateEtd.Checked && !settings.AccountObject.ETDEnabled;
        settings.AccountObject.ETDEnabled = this.chkActivateEtd.Checked;
        settings.AccountObject.UploadAtShipTime = this.rdbEtdUploadAtShipment.Checked;
        settings.AccountObject.NotifyETDFailures = true;
        settings.AccountObject.PrintGroundHReadableBarcodeInd = this.chkPrintGroundReadableBarcode.Checked;
        settings.AccountObject.StaffedHAL = this.chkFedExHoldLocations.Checked;
      }
      if (settings.RegistrySettings == null)
        return;
      settings.RegistrySettings["PreReadScale"].Value = this.chkPreReadScale.Checked ? (object) "Y" : (object) "N";
      settings.RegistrySettings["FieldColor"].Value = (object) this.txtSampleText.BackColor.ToArgb().ToString();
      settings.RegistrySettings["AutoTab"].Value = this.chkAutoTab.Checked ? (object) "Y" : (object) "N";
      this._languageChanged = !string.Equals(settings.RegistrySettings["Language"].Value as string, this.cboLanguage.SelectedValue as string);
      settings.RegistrySettings["Language"].Value = (object) (this.cboLanguage.SelectedValue as string);
      settings.RegistrySettings["SCALETIMEOUT"].Value = (object) (this.cboScaleTimeout.SelectedValue as string);
      this._fontSizeChanged = !string.Equals(settings.RegistrySettings["FontSize"].Value as string, this.cboFontSize.SelectedIndex.ToString());
      settings.RegistrySettings["FontSize"].Value = (object) this.cboFontSize.SelectedIndex.ToString();
      FocusExtender.FocusInfo.FocusBackColor = this.txtSampleText.BackColor;
    }

    public void ObjectsToScreen(SystemSettingsObject settings, Utility.FormOperation op)
    {
      if (settings == null)
        return;
      if (settings.AccountObject != null)
      {
        this.pnlGroundClose.Visible = settings.AccountObject.IsGroundAutoCloseEnabled;
        this.chkDisplayPickupTab.Checked = settings.AccountObject.IsPickupEnabled;
        this.MyParent.ProcessAutoCloseTimes(settings.AccountObject);
        this.edtGroundAutoCloseTime.Text = settings.AccountObject.GroundAutoCloseTime.ToString("hh:mm tt", (IFormatProvider) CultureInfo.InvariantCulture.DateTimeFormat);
        if (settings.AccountObject.Culture.Length == 3)
          settings.AccountObject.Culture = Utility.OldLangCodeToCulture(settings.AccountObject.Culture);
        this.cboLanguage.SelectedValue = settings.RegistrySettings["Language"].Value;
        this.cboDateFormat.SelectedItem = (object) settings.AccountObject.DateFormat;
        if (op == Utility.FormOperation.AddFirst || op == Utility.FormOperation.Add || op == Utility.FormOperation.AddByDup)
          this.chkRequireLogin.Enabled = false;
        else
          this.chkRequireLogin.Enabled = true;
        if (op == Utility.FormOperation.ViewEdit)
        {
          this.chkRequireLogin.Checked = settings.AccountObject.RequireUserLogin;
        }
        else
        {
          this.chkRequireLogin.Checked = false;
          this.chkRequireLogin.Text += GuiData.Languafier.Translate("UnavailableOnInitialInstall");
        }
        try
        {
          this.edtCurrentMinPurge.Text = settings.AccountObject.MinHistoryPurgeDays.ToString();
          this.edtCurrentMaxPurge.Text = settings.AccountObject.MaxHistoryPurgeDays.ToString();
          FdxMaskedEdit edtFreightMinPurge = this.edtFreightMinPurge;
          int num = settings.AccountObject.FreightLTLPurgeMin;
          string str1 = num.ToString();
          edtFreightMinPurge.Text = str1;
          FdxMaskedEdit edtFreightMaxPurge = this.edtFreightMaxPurge;
          num = settings.AccountObject.FreightLTLPurgeMax;
          string str2 = num.ToString();
          edtFreightMaxPurge.Text = str2;
        }
        catch
        {
        }
        this.chkNonResiIimporterStatus.Checked = settings.AccountObject.IsNonResidentImporter;
        bool flag = false;
        if (settings.RegistrySettings.ContainsKey("FedexWSED"))
        {
          if (settings.RegistrySettings["FedexWSED"].Value.ToString().ToUpper() != "Y")
          {
            this.chkDownloadFromEEI.Hide();
            this.gbxExpressIntlSettings.Visible = false;
          }
        }
        else if (settings.AccountObject.Address != null && settings.AccountObject.Address.CountryCode == "US")
        {
          flag = settings.AccountObject.DownLoadFromFedExSEDTool;
          this.chkDownloadFromEEI.Visible = true;
          this.gbxExpressIntlSettings.Visible = true;
        }
        else
        {
          this.chkDownloadFromEEI.Visible = false;
          this.gbxExpressIntlSettings.Visible = false;
        }
        this.chkDownloadFromEEI.Checked = flag;
        Color fieldColor = Utility.GetFieldColor(settings.AccountObject.MeterNumber, settings.AccountObject.AccountNumber);
        this.txtSampleText.BackColor = Color.FromArgb((int) byte.MaxValue, (int) fieldColor.R, (int) fieldColor.G, (int) fieldColor.B);
        int result;
        if (int.TryParse(settings.RegistrySettings["FontSize"].Value as string, out result))
          this.cboFontSize.SelectedIndex = result;
        if (settings.AccountObject.BoxInfo != null)
        {
          this.chkScaleIsAttached.Checked = settings.AccountObject.BoxInfo.ScaleAttached;
          this.chkPreReadScale.Checked = "Y".Equals(settings.RegistrySettings["PreReadScale"].Value as string, StringComparison.InvariantCultureIgnoreCase);
          this.chkPreReadScale.Enabled = this.chkScaleIsAttached.Checked;
        }
        else
        {
          this.chkScaleIsAttached.Checked = false;
          this.chkPreReadScale.Checked = false;
          this.chkPreReadScale.Enabled = false;
        }
        if (settings.AccountObject.Address != null)
          this.chkDisplayPickupTab.Visible = Utility.IsLacCountry(settings.AccountObject.Address.CountryCode) || Utility.IsPuertoRico(settings.AccountObject.Address);
        this.chkActivateEtd.Checked = settings.AccountObject.ETDEnabled;
        if (settings.AccountObject.ETDEnabled)
        {
          this.rdbEtdUploadAtShipment.Checked = settings.AccountObject.UploadAtShipTime;
          this.rdbUploadEtdHourly.Checked = !settings.AccountObject.UploadAtShipTime;
          if (op == Utility.FormOperation.RestoreMaster)
            new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI).SetProfileValue("SETTINGS", "ShowETD", (object) "True");
        }
        if (settings.AccountObject.IsSmartPostEnabled)
        {
          this.edtSmartpostAutoCloseTime.Enabled = true;
          if (settings.AccountObject.SPAutoCloseTime != DateTime.MinValue)
            this.edtSmartpostAutoCloseTime.Text = settings.AccountObject.SPAutoCloseTime.ToString("hh:mm tt", (IFormatProvider) DateTimeFormatInfo.InvariantInfo);
          else
            this.edtSmartpostAutoCloseTime.Text = "07:00 PM";
        }
        this.chkEnableReturnShipping.Checked = settings.AccountObject.ReturnShippingEnabled;
        if (settings.AccountObject.Address != null && settings.AccountObject.Address.CountryCode == "US")
        {
          this.ShowSmartpostReturnSettings(true);
          this.chkAddSmartpostReturnAddresses.Checked = settings.AccountObject.SPReturnAddressEntryEnabled;
        }
        else
        {
          this.chkAddSmartpostReturnAddresses.Checked = false;
          this.ShowSmartpostReturnSettings(false);
        }
        switch (settings.AccountObject.GCTUploadTime)
        {
          case GCTUploadTimeOption.AtRegularHourlyUpload:
            this.rdbGCTuploadHourly.Checked = true;
            break;
          default:
            this.rdbGCTuploadAtShiptime.Checked = true;
            break;
        }
        if (this.MyParent.Operation == Utility.FormOperation.ViewEdit && settings.AccountObject.GSNE_Masking_Initiative_Enabled)
        {
          this.pnlPrintGroundReadableBarcode.IsVisible = true;
          this.gbxLabelInformation.Visible = true;
        }
        else
        {
          this.pnlPrintGroundReadableBarcode.IsVisible = false;
          this.gbxLabelInformation.Visible = false;
        }
        this.gbxFegCloseSettings.Visible = !settings.AccountObject.is_ZENITH_CHANGES_INIT;
        this.chkPrintGroundReadableBarcode.Checked = settings.AccountObject.PrintGroundHReadableBarcodeInd;
        this.chkFedExHoldLocations.Checked = settings.AccountObject.StaffedHAL;
      }
      this.chkAutoTab.Checked = settings.RegistrySettings["AutoTab"].Value.ToString().ToUpper() == "Y";
      this.cboScaleTimeout.SelectedValue = (object) settings.RegistrySettings["SCALETIMEOUT"].Value.ToString();
    }

    public void OnSystemSettingsCountryChanged(object sender, CountryChangedEventArgs args)
    {
      SystemSettings parent = this.Parent.Parent.Parent as SystemSettings;
      string country = parent.ExpressAdminPage.Country;
      if (Utility.IsLacCountry(country) || Utility.IsPuertoRico(new Address()
      {
        CountryCode = country,
        StateProvince = parent.ExpressAdminPage.StateProvince
      }))
      {
        this.chkDisplayPickupTab.Visible = true;
        if (parent.Operation == Utility.FormOperation.AddFirst || parent.Operation == Utility.FormOperation.Add || parent.Operation == Utility.FormOperation.AddByDup)
          this.chkDisplayPickupTab.Checked = true;
      }
      else
      {
        this.chkDisplayPickupTab.Visible = false;
        this.chkDisplayPickupTab.Checked = false;
      }
      this.ShowSmartpostReturnSettings(args.CountryCode == "US");
      if (this.MyParent.Operation == Utility.FormOperation.AddFirst || this.MyParent.Operation == Utility.FormOperation.Add || this.MyParent.Operation == Utility.FormOperation.AddByDup || this.MyParent.Settings.AccountObject.ETD_ENHANCE_POSTSHIP_UPLOAD_Initiative_Enabled)
      {
        this.gbxElectronicTradeDocuments.Visible = true;
        this.pnlEtdSettings.Visible = true;
      }
      else
      {
        bool flag = GuiData.AppController.ShipEngine.IsETDAllowedForCountry(country, ETDAvailabilityData.CountryType.Sender);
        this.gbxElectronicTradeDocuments.Visible = flag;
        this.pnlEtdSettings.Visible = flag;
        this.chkActivateEtd.Checked &= flag;
      }
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
        case 2445:
        case 2656:
        case 3613:
        case 3614:
          control = (Control) this.edtCurrentMaxPurge;
          break;
        case 2602:
          control = (Control) this.edtOverrideCode;
          break;
        case 2768:
        case 3451:
          control = (Control) this.edtFreightMinPurge;
          break;
        case 3452:
          control = (Control) this.edtFreightMaxPurge;
          break;
        case 3610:
          control = (Control) this.edtExpressAutoCloseTime;
          break;
        case 3612:
        case 3615:
          control = (Control) this.edtCurrentMinPurge;
          break;
        case 9564:
        case 20655:
          control = (Control) this.edtSmartpostAutoCloseTime;
          break;
        case 13761:
        case 13762:
          control = (Control) this.edtGroundAutoCloseTime;
          break;
        default:
          flag = false;
          break;
      }
      if (flag && control != null && this.Parent.Parent.Parent is SystemSettings parent)
      {
        if (parent.SystemSettingsTabControl.SelectedTab != this.Parent)
          parent.SystemSettingsTabControl.SelectedTab = this.Parent as TabPage;
        control.Focus();
        if (!control.Enabled)
          control.Enabled = true;
      }
      return flag;
    }

    public void ShowGroundAutoClose(bool bShow) => this.pnlGroundClose.Visible = bShow;

    private void ShowSmartpostReturnSettings(bool bShow)
    {
      this.lnkAddSmartpostReturnAddresses.Visible = bShow;
      this.chkAddSmartpostReturnAddresses.Visible = bShow;
      if (bShow)
        return;
      this.chkAddSmartpostReturnAddresses.Checked = bShow;
    }

    public void SetupEvents()
    {
      if (this._myParent == null)
        return;
      this._myParent.SystemSettingsEventBroker.GetTopic("SystemSettingsCountryChanged")?.AddSubscriber((object) this, "OnSystemSettingsCountryChanged");
      this._myParent.SystemSettingsEventBroker.AddSubscriber("OneRateEnabledChanged", (object) this, "OnOneRateEnabledChanged");
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.DesignMode)
        return;
      this.PopulateCombos();
      this.gbxElectronicTradeDocuments.Visible = true;
      this.panelFreightPurge.Visible = this.IsFreightEnabled;
    }

    private void PopulateCombos()
    {
      Utility.SetDisplayAndValue(this.cboLanguage, Utility.GetDataTable(Utility.ListTypes.Languages), "Description", "Code", false);
      Utility.SetDisplayAndValue(this.cboScaleTimeout, Utility.GetDataTable(Utility.ListTypes.ScaleTimeout), "Description", "Code", false, false);
    }

    private void chkScaleIsAttached_CheckedChanged(object sender, EventArgs e)
    {
      this.chkPreReadScale.Enabled = (sender as CheckBox).Checked;
      if (this.chkPreReadScale.Enabled)
        return;
      this.chkPreReadScale.Checked = false;
    }

    private void btnFieldColor_Click(object sender, EventArgs e)
    {
      if (this.colorDialogFieldBackColor.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      this.txtSampleText.BackColor = this.colorDialogFieldBackColor.Color;
    }

    private void txtSampleText_BackColorChanged(object sender, EventArgs e)
    {
      this.txtSampleText.ForeColor = (double) this.txtSampleText.BackColor.GetBrightness() > 0.5 ? Color.Black : Color.White;
    }

    private void chkRequireLogin_CheckedChanged(object sender, EventArgs e)
    {
      if (!(sender as CheckBox).Checked)
        return;
      DataTable output;
      GuiData.AppController.ShipEngine.GetDataList(GsmDataAccess.ListSpecification.User_List, out output, new Error());
      if (output.Rows != null && output.Rows.Count != 0)
        return;
      if (MessageBox.Show(GuiData.Languafier.TranslateMessage(38199), "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        if (new UserEdit((User) null, Utility.FormOperation.Add).ShowDialog((IWin32Window) this) == DialogResult.OK)
          return;
        int num = (int) MessageBox.Show(GuiData.Languafier.TranslateMessage(38200), "Message");
        this.chkRequireLogin.Checked = false;
      }
      else
        this.chkRequireLogin.Checked = false;
    }

    private void lnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Utility.DisplayLinkLabelHelp(sender, e);
    }

    private void chkPreReadScale_Click(object sender, EventArgs e)
    {
      CheckBox checkBox = sender as CheckBox;
      checkBox.Checked = !checkBox.Checked;
      bool bVal;
      if (!checkBox.Checked || !this.MyParent.IntegrationPage.IsIntegrationChecked && !(GuiData.ConfigManager.GetProfileBool("SHIPNET2000/GUI/SETTINGS/INTEGRATION", "AnyFXIAProfilesInUse", out bVal) & bVal))
        return;
      int num = (int) MessageBox.Show(GuiData.Languafier.TranslateMessage(43336), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private void chkActivateEcd_Click(object sender, EventArgs e)
    {
      if (!this.chkActivateEtd.Checked)
      {
        if (new TermsAndConditionsDialog(GuiData.Languafier.Translate("ETDTERMSTITLE"), GuiData.Languafier.Translate("ETDTERMSCONDITIONS"), true).ShowDialog((IWin32Window) this) != DialogResult.OK)
          return;
        this.chkActivateEtd.Checked = true;
      }
      else
        this.chkActivateEtd.Checked = false;
    }

    private void chkActivateEcd_CheckedChanged(object sender, EventArgs e)
    {
      this.pnlEtdInformation.Enabled = this.chkActivateEtd.Checked;
    }

    private void lnkAddSmartpostReturnAddresses_LinkClicked(
      object sender,
      LinkLabelLinkClickedEventArgs e)
    {
      Utility.DisplayLinkLabelHelp(sender, e);
    }

    private void chkEnableReturnShipping_Click(object sender, EventArgs e)
    {
      if (!this.chkEnableReturnShipping.Checked)
      {
        if (new TermsAndConditionsDialog(GuiData.Languafier.Translate("RETURNSTERMSTITLE"), GuiData.Languafier.Translate("RETURNTERMSCONDITIONS"), false).ShowDialog((IWin32Window) this) != DialogResult.OK)
          return;
        this.chkEnableReturnShipping.Checked = true;
      }
      else
        this.chkEnableReturnShipping.Checked = false;
    }

    private void chkPrintGroundReadableBarcode_Click(object sender, EventArgs e)
    {
      if (this.MyParent.Settings.AccountObject.GSNE_Masking_Initiative_Enabled && this.MyParent.GroundAdminPage.IsAccountNumberEpic(this.MyParent.Settings.AccountObject.GroundAccountNumber) && this.MyParent.GroundAdminPage.FedEx1DBarcodeEnabled)
      {
        if (!this.chkPrintGroundReadableBarcode.Checked)
        {
          if (MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate("GroundHumanReadableBarcodeEnablePrompt"), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          {
            CloseInProgress closeInProgress = new CloseInProgress();
            closeInProgress.Show();
            closeInProgress.Refresh();
            Account groundData = new Account(this.MyParent.Settings.AccountObject);
            ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.SyncGroundAccountData(ref groundData);
            closeInProgress.Hide();
            if (!serviceResponse.IsOperationOk)
            {
              Utility.DisplayError(serviceResponse.Error);
            }
            else
            {
              this.MyParent.GroundAdminPage.UpdateGroundAccountNumber(groundData.GroundAccountNumber);
              this.chkPrintGroundReadableBarcode.Checked = true;
            }
          }
          else
          {
            this.chkPrintGroundReadableBarcode.Checked = false;
            this.chkPrintGroundReadableBarcode.Select();
          }
        }
        else
          this.chkPrintGroundReadableBarcode.Checked = false;
      }
      else
        this.chkPrintGroundReadableBarcode.Checked = !this.chkPrintGroundReadableBarcode.Checked;
    }

    public void OnOneRateEnabledChanged(object sender, BoolEventArgs e)
    {
      this.pnlOneRateDisplay.Visible = e.Flag;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SystemSettingsUser));
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.flwLeftColumn = new FlowLayoutPanel();
      this.gbxSystemSettings = new ColorGroupBox();
      this.lblScaleTimeout = new Label();
      this.cboScaleTimeout = new ComboBox();
      this.chkPreReadScale = new CheckBox();
      this.chkAutoTab = new CheckBox();
      this.chkScaleIsAttached = new CheckBox();
      this.chkRequireLogin = new CheckBox();
      this.gbxExpressIntlSettings = new ColorGroupBox();
      this.chkDownloadFromEEI = new CheckBox();
      this.gbxFegCloseSettings = new ColorGroupBox();
      this.chkNonResiIimporterStatus = new CheckBox();
      this.gbxCommOverride = new ColorGroupBox();
      this.edtOverrideCode = new TextBox();
      this.label9 = new Label();
      this.gbxElectronicTradeDocuments = new ColorGroupBox();
      this.pnlEtdSettings = new FlowLayoutPanel();
      this.chkActivateEtd = new CheckBox();
      this.pnlEtdInformation = new FlowLayoutPanel();
      this.lblEtdCommunicationTitle = new Label();
      this.llblEtdUploadDescription = new LinkLabelEx();
      this.rdbEtdUploadAtShipment = new RadioButton();
      this.rdbUploadEtdHourly = new RadioButton();
      this.gbxLabelInformation = new ColorGroupBox();
      this.pnlPrintGroundReadableBarcode = new PanelEx();
      this.lnkPrintGroundReadableBarcode = new LinkLabel();
      this.chkPrintGroundReadableBarcode = new CheckBox();
      this.pnlHoldLocation = new PanelEx();
      this.lnkHoldLocations = new LinkLabel();
      this.chkFedExHoldLocations = new CheckBox();
      this.pnlOneRateDisplay = new Panel();
      this.chkOneRateStatus = new CheckBox();
      this.flwRightColumn = new FlowLayoutPanel();
      this.gbxShipDbPurge = new ColorGroupBox();
      this.panelFreightPurge = new Panel();
      this.lblFreightMax = new Label();
      this.edtFreightMinPurge = new FdxMaskedEdit();
      this.edtFreightMaxPurge = new FdxMaskedEdit();
      this.lblFreightMin = new Label();
      this.edtCurrentMaxPurge = new FdxMaskedEdit();
      this.edtCurrentMinPurge = new FdxMaskedEdit();
      this.label6 = new Label();
      this.label5 = new Label();
      this.gbxSmartpostReturns = new ColorGroupBox();
      this.pnlGCTCommunicationsSettings = new Panel();
      this.lblGCTcommunicationDescription = new Label();
      this.rdbGCTuploadHourly = new RadioButton();
      this.rdbGCTuploadAtShiptime = new RadioButton();
      this.lblGCTcommunicationsHeader = new Label();
      this.chkEnableReturnShipping = new CheckBox();
      this.lnkAddSmartpostReturnAddresses = new LinkLabelEx();
      this.chkAddSmartpostReturnAddresses = new CheckBox();
      this.gbxInterfacePrefs = new ColorGroupBox();
      this.cboFontSize = new ComboBox();
      this.lblFontSize = new Label();
      this.btnFieldColor = new Button();
      this.txtSampleText = new Label();
      this.chkDisplayPickupTab = new CheckBox();
      this.label3 = new Label();
      this.cboDateFormat = new ComboBox();
      this.label2 = new Label();
      this.cboLanguage = new ComboBox();
      this.label1 = new Label();
      this.gbxCloseSettings = new ColorGroupBox();
      this.pnlGroundClose = new Panel();
      this.edtGroundAutoCloseTime = new FdxMaskedEdit();
      this.lblGroundClose = new Label();
      this.edtSmartpostAutoCloseTime = new FdxMaskedEdit();
      this.lblSmartpostClose = new Label();
      this.edtExpressAutoCloseTime = new FdxMaskedEdit();
      this.lblExpressClose = new Label();
      this.colorDialogFieldBackColor = new ColorDialog();
      this.toolTip1 = new ToolTip(this.components);
      this.focusExtender1 = new FocusExtender();
      this.tableLayoutPanel1.SuspendLayout();
      this.flwLeftColumn.SuspendLayout();
      this.gbxSystemSettings.SuspendLayout();
      this.gbxExpressIntlSettings.SuspendLayout();
      this.gbxFegCloseSettings.SuspendLayout();
      this.gbxCommOverride.SuspendLayout();
      this.gbxElectronicTradeDocuments.SuspendLayout();
      this.pnlEtdSettings.SuspendLayout();
      this.pnlEtdInformation.SuspendLayout();
      this.gbxLabelInformation.SuspendLayout();
      this.pnlPrintGroundReadableBarcode.SuspendLayout();
      this.pnlHoldLocation.SuspendLayout();
      this.pnlOneRateDisplay.SuspendLayout();
      this.flwRightColumn.SuspendLayout();
      this.gbxShipDbPurge.SuspendLayout();
      this.panelFreightPurge.SuspendLayout();
      this.gbxSmartpostReturns.SuspendLayout();
      this.pnlGCTCommunicationsSettings.SuspendLayout();
      this.gbxInterfacePrefs.SuspendLayout();
      this.gbxCloseSettings.SuspendLayout();
      this.pnlGroundClose.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.flwLeftColumn, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.flwRightColumn, 1, 0);
      this.helpProvider1.SetHelpKeyword((Control) this.tableLayoutPanel1, componentResourceManager.GetString("tableLayoutPanel1.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.tableLayoutPanel1, (HelpNavigator) componentResourceManager.GetObject("tableLayoutPanel1.HelpNavigator"));
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.helpProvider1.SetShowHelp((Control) this.tableLayoutPanel1, (bool) componentResourceManager.GetObject("tableLayoutPanel1.ShowHelp"));
      this.flwLeftColumn.Controls.Add((Control) this.gbxSystemSettings);
      this.flwLeftColumn.Controls.Add((Control) this.gbxExpressIntlSettings);
      this.flwLeftColumn.Controls.Add((Control) this.gbxFegCloseSettings);
      this.flwLeftColumn.Controls.Add((Control) this.gbxCommOverride);
      this.flwLeftColumn.Controls.Add((Control) this.gbxElectronicTradeDocuments);
      this.flwLeftColumn.Controls.Add((Control) this.gbxLabelInformation);
      this.flwLeftColumn.Controls.Add((Control) this.pnlHoldLocation);
      this.flwLeftColumn.Controls.Add((Control) this.pnlOneRateDisplay);
      componentResourceManager.ApplyResources((object) this.flwLeftColumn, "flwLeftColumn");
      this.flwLeftColumn.Name = "flwLeftColumn";
      this.gbxSystemSettings.BorderThickness = 1f;
      this.gbxSystemSettings.Controls.Add((Control) this.lblScaleTimeout);
      this.gbxSystemSettings.Controls.Add((Control) this.cboScaleTimeout);
      this.gbxSystemSettings.Controls.Add((Control) this.chkPreReadScale);
      this.gbxSystemSettings.Controls.Add((Control) this.chkAutoTab);
      this.gbxSystemSettings.Controls.Add((Control) this.chkScaleIsAttached);
      this.gbxSystemSettings.Controls.Add((Control) this.chkRequireLogin);
      this.gbxSystemSettings.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxSystemSettings, "gbxSystemSettings");
      this.gbxSystemSettings.Name = "gbxSystemSettings";
      this.gbxSystemSettings.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxSystemSettings, (bool) componentResourceManager.GetObject("gbxSystemSettings.ShowHelp"));
      this.gbxSystemSettings.TabStop = false;
      componentResourceManager.ApplyResources((object) this.lblScaleTimeout, "lblScaleTimeout");
      this.lblScaleTimeout.Name = "lblScaleTimeout";
      this.cboScaleTimeout.DropDownStyle = ComboBoxStyle.DropDownList;
      this.focusExtender1.SetFocusBackColor((Control) this.cboScaleTimeout, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboScaleTimeout, SystemColors.WindowText);
      this.cboScaleTimeout.FormattingEnabled = true;
      componentResourceManager.ApplyResources((object) this.cboScaleTimeout, "cboScaleTimeout");
      this.cboScaleTimeout.Name = "cboScaleTimeout";
      this.chkPreReadScale.AutoCheck = false;
      this.helpProvider1.SetHelpNavigator((Control) this.chkPreReadScale, (HelpNavigator) componentResourceManager.GetObject("chkPreReadScale.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkPreReadScale, "chkPreReadScale");
      this.chkPreReadScale.Name = "chkPreReadScale";
      this.helpProvider1.SetShowHelp((Control) this.chkPreReadScale, (bool) componentResourceManager.GetObject("chkPreReadScale.ShowHelp"));
      this.chkPreReadScale.UseVisualStyleBackColor = true;
      this.chkPreReadScale.Click += new EventHandler(this.chkPreReadScale_Click);
      this.helpProvider1.SetHelpKeyword((Control) this.chkAutoTab, componentResourceManager.GetString("chkAutoTab.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAutoTab, (HelpNavigator) componentResourceManager.GetObject("chkAutoTab.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAutoTab, "chkAutoTab");
      this.chkAutoTab.Name = "chkAutoTab";
      this.helpProvider1.SetShowHelp((Control) this.chkAutoTab, (bool) componentResourceManager.GetObject("chkAutoTab.ShowHelp"));
      this.chkAutoTab.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkScaleIsAttached, componentResourceManager.GetString("chkScaleIsAttached.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkScaleIsAttached, (HelpNavigator) componentResourceManager.GetObject("chkScaleIsAttached.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkScaleIsAttached, "chkScaleIsAttached");
      this.chkScaleIsAttached.Name = "chkScaleIsAttached";
      this.helpProvider1.SetShowHelp((Control) this.chkScaleIsAttached, (bool) componentResourceManager.GetObject("chkScaleIsAttached.ShowHelp"));
      this.chkScaleIsAttached.UseVisualStyleBackColor = true;
      this.chkScaleIsAttached.CheckedChanged += new EventHandler(this.chkScaleIsAttached_CheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.chkRequireLogin, componentResourceManager.GetString("chkRequireLogin.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkRequireLogin, (HelpNavigator) componentResourceManager.GetObject("chkRequireLogin.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkRequireLogin, "chkRequireLogin");
      this.chkRequireLogin.Name = "chkRequireLogin";
      this.helpProvider1.SetShowHelp((Control) this.chkRequireLogin, (bool) componentResourceManager.GetObject("chkRequireLogin.ShowHelp"));
      this.chkRequireLogin.UseVisualStyleBackColor = true;
      this.chkRequireLogin.CheckedChanged += new EventHandler(this.chkRequireLogin_CheckedChanged);
      this.gbxExpressIntlSettings.BorderThickness = 1f;
      this.gbxExpressIntlSettings.Controls.Add((Control) this.chkDownloadFromEEI);
      this.gbxExpressIntlSettings.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxExpressIntlSettings, "gbxExpressIntlSettings");
      this.gbxExpressIntlSettings.Name = "gbxExpressIntlSettings";
      this.gbxExpressIntlSettings.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxExpressIntlSettings, (bool) componentResourceManager.GetObject("gbxExpressIntlSettings.ShowHelp"));
      this.gbxExpressIntlSettings.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkDownloadFromEEI, componentResourceManager.GetString("chkDownloadFromEEI.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkDownloadFromEEI, (HelpNavigator) componentResourceManager.GetObject("chkDownloadFromEEI.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkDownloadFromEEI, "chkDownloadFromEEI");
      this.chkDownloadFromEEI.Name = "chkDownloadFromEEI";
      this.helpProvider1.SetShowHelp((Control) this.chkDownloadFromEEI, (bool) componentResourceManager.GetObject("chkDownloadFromEEI.ShowHelp"));
      this.chkDownloadFromEEI.UseVisualStyleBackColor = true;
      this.gbxFegCloseSettings.BorderThickness = 1f;
      this.gbxFegCloseSettings.Controls.Add((Control) this.chkNonResiIimporterStatus);
      this.gbxFegCloseSettings.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxFegCloseSettings, "gbxFegCloseSettings");
      this.gbxFegCloseSettings.Name = "gbxFegCloseSettings";
      this.gbxFegCloseSettings.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxFegCloseSettings, (bool) componentResourceManager.GetObject("gbxFegCloseSettings.ShowHelp"));
      this.gbxFegCloseSettings.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkNonResiIimporterStatus, componentResourceManager.GetString("chkNonResiIimporterStatus.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkNonResiIimporterStatus, (HelpNavigator) componentResourceManager.GetObject("chkNonResiIimporterStatus.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkNonResiIimporterStatus, "chkNonResiIimporterStatus");
      this.chkNonResiIimporterStatus.Name = "chkNonResiIimporterStatus";
      this.helpProvider1.SetShowHelp((Control) this.chkNonResiIimporterStatus, (bool) componentResourceManager.GetObject("chkNonResiIimporterStatus.ShowHelp"));
      this.chkNonResiIimporterStatus.UseVisualStyleBackColor = true;
      this.gbxCommOverride.BorderThickness = 1f;
      this.gbxCommOverride.Controls.Add((Control) this.edtOverrideCode);
      this.gbxCommOverride.Controls.Add((Control) this.label9);
      this.gbxCommOverride.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxCommOverride, "gbxCommOverride");
      this.gbxCommOverride.Name = "gbxCommOverride";
      this.gbxCommOverride.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxCommOverride, (bool) componentResourceManager.GetObject("gbxCommOverride.ShowHelp"));
      this.gbxCommOverride.TabStop = false;
      this.focusExtender1.SetFocusBackColor((Control) this.edtOverrideCode, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtOverrideCode, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtOverrideCode, componentResourceManager.GetString("edtOverrideCode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtOverrideCode, (HelpNavigator) componentResourceManager.GetObject("edtOverrideCode.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtOverrideCode, "edtOverrideCode");
      this.edtOverrideCode.Name = "edtOverrideCode";
      this.helpProvider1.SetShowHelp((Control) this.edtOverrideCode, (bool) componentResourceManager.GetObject("edtOverrideCode.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label9, "label9");
      this.label9.Name = "label9";
      this.gbxElectronicTradeDocuments.BorderThickness = 1f;
      this.gbxElectronicTradeDocuments.Controls.Add((Control) this.pnlEtdSettings);
      this.gbxElectronicTradeDocuments.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxElectronicTradeDocuments, "gbxElectronicTradeDocuments");
      this.gbxElectronicTradeDocuments.Name = "gbxElectronicTradeDocuments";
      this.gbxElectronicTradeDocuments.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxElectronicTradeDocuments, (bool) componentResourceManager.GetObject("gbxElectronicTradeDocuments.ShowHelp"));
      this.gbxElectronicTradeDocuments.TabStop = false;
      componentResourceManager.ApplyResources((object) this.pnlEtdSettings, "pnlEtdSettings");
      this.pnlEtdSettings.BackColor = Color.Transparent;
      this.pnlEtdSettings.Controls.Add((Control) this.chkActivateEtd);
      this.pnlEtdSettings.Controls.Add((Control) this.pnlEtdInformation);
      this.pnlEtdSettings.Name = "pnlEtdSettings";
      this.chkActivateEtd.AutoCheck = false;
      componentResourceManager.ApplyResources((object) this.chkActivateEtd, "chkActivateEtd");
      this.chkActivateEtd.Name = "chkActivateEtd";
      this.chkActivateEtd.UseVisualStyleBackColor = true;
      this.chkActivateEtd.CheckedChanged += new EventHandler(this.chkActivateEcd_CheckedChanged);
      this.chkActivateEtd.Click += new EventHandler(this.chkActivateEcd_Click);
      componentResourceManager.ApplyResources((object) this.pnlEtdInformation, "pnlEtdInformation");
      this.pnlEtdInformation.Controls.Add((Control) this.lblEtdCommunicationTitle);
      this.pnlEtdInformation.Controls.Add((Control) this.llblEtdUploadDescription);
      this.pnlEtdInformation.Controls.Add((Control) this.rdbEtdUploadAtShipment);
      this.pnlEtdInformation.Controls.Add((Control) this.rdbUploadEtdHourly);
      this.pnlEtdInformation.Name = "pnlEtdInformation";
      componentResourceManager.ApplyResources((object) this.lblEtdCommunicationTitle, "lblEtdCommunicationTitle");
      this.lblEtdCommunicationTitle.Name = "lblEtdCommunicationTitle";
      componentResourceManager.ApplyResources((object) this.llblEtdUploadDescription, "llblEtdUploadDescription");
      this.llblEtdUploadDescription.Name = "llblEtdUploadDescription";
      this.llblEtdUploadDescription.TabStop = true;
      this.llblEtdUploadDescription.Tag = (object) "4827";
      this.llblEtdUploadDescription.UseCompatibleTextRendering = true;
      this.llblEtdUploadDescription.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
      componentResourceManager.ApplyResources((object) this.rdbEtdUploadAtShipment, "rdbEtdUploadAtShipment");
      this.rdbEtdUploadAtShipment.Checked = true;
      this.rdbEtdUploadAtShipment.Name = "rdbEtdUploadAtShipment";
      this.rdbEtdUploadAtShipment.TabStop = true;
      this.rdbEtdUploadAtShipment.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.rdbUploadEtdHourly, "rdbUploadEtdHourly");
      this.rdbUploadEtdHourly.Name = "rdbUploadEtdHourly";
      this.rdbUploadEtdHourly.UseVisualStyleBackColor = true;
      this.gbxLabelInformation.BorderThickness = 1f;
      this.gbxLabelInformation.Controls.Add((Control) this.pnlPrintGroundReadableBarcode);
      this.gbxLabelInformation.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxLabelInformation, "gbxLabelInformation");
      this.gbxLabelInformation.Name = "gbxLabelInformation";
      this.gbxLabelInformation.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxLabelInformation, (bool) componentResourceManager.GetObject("gbxLabelInformation.ShowHelp"));
      this.gbxLabelInformation.TabStop = false;
      this.pnlPrintGroundReadableBarcode.Controls.Add((Control) this.lnkPrintGroundReadableBarcode);
      this.pnlPrintGroundReadableBarcode.Controls.Add((Control) this.chkPrintGroundReadableBarcode);
      this.pnlPrintGroundReadableBarcode.IsVisible = true;
      componentResourceManager.ApplyResources((object) this.pnlPrintGroundReadableBarcode, "pnlPrintGroundReadableBarcode");
      this.pnlPrintGroundReadableBarcode.Name = "pnlPrintGroundReadableBarcode";
      this.helpProvider1.SetShowHelp((Control) this.pnlPrintGroundReadableBarcode, (bool) componentResourceManager.GetObject("pnlPrintGroundReadableBarcode.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lnkPrintGroundReadableBarcode, "lnkPrintGroundReadableBarcode");
      this.lnkPrintGroundReadableBarcode.Name = "lnkPrintGroundReadableBarcode";
      this.lnkPrintGroundReadableBarcode.TabStop = true;
      this.lnkPrintGroundReadableBarcode.Tag = (object) "4518";
      this.lnkPrintGroundReadableBarcode.UseCompatibleTextRendering = true;
      this.lnkPrintGroundReadableBarcode.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
      this.chkPrintGroundReadableBarcode.AutoCheck = false;
      componentResourceManager.ApplyResources((object) this.chkPrintGroundReadableBarcode, "chkPrintGroundReadableBarcode");
      this.chkPrintGroundReadableBarcode.Name = "chkPrintGroundReadableBarcode";
      this.chkPrintGroundReadableBarcode.UseVisualStyleBackColor = true;
      this.chkPrintGroundReadableBarcode.Click += new EventHandler(this.chkPrintGroundReadableBarcode_Click);
      this.pnlHoldLocation.Controls.Add((Control) this.lnkHoldLocations);
      this.pnlHoldLocation.Controls.Add((Control) this.chkFedExHoldLocations);
      this.pnlHoldLocation.IsVisible = true;
      componentResourceManager.ApplyResources((object) this.pnlHoldLocation, "pnlHoldLocation");
      this.pnlHoldLocation.Name = "pnlHoldLocation";
      this.helpProvider1.SetShowHelp((Control) this.pnlHoldLocation, (bool) componentResourceManager.GetObject("pnlHoldLocation.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lnkHoldLocations, "lnkHoldLocations");
      this.lnkHoldLocations.Name = "lnkHoldLocations";
      this.helpProvider1.SetShowHelp((Control) this.lnkHoldLocations, (bool) componentResourceManager.GetObject("lnkHoldLocations.ShowHelp"));
      this.lnkHoldLocations.TabStop = true;
      this.lnkHoldLocations.Tag = (object) "4520";
      this.lnkHoldLocations.UseCompatibleTextRendering = true;
      this.lnkHoldLocations.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
      componentResourceManager.ApplyResources((object) this.chkFedExHoldLocations, "chkFedExHoldLocations");
      this.chkFedExHoldLocations.Name = "chkFedExHoldLocations";
      this.helpProvider1.SetShowHelp((Control) this.chkFedExHoldLocations, (bool) componentResourceManager.GetObject("chkFedExHoldLocations.ShowHelp"));
      this.pnlOneRateDisplay.Controls.Add((Control) this.chkOneRateStatus);
      componentResourceManager.ApplyResources((object) this.pnlOneRateDisplay, "pnlOneRateDisplay");
      this.pnlOneRateDisplay.Name = "pnlOneRateDisplay";
      componentResourceManager.ApplyResources((object) this.chkOneRateStatus, "chkOneRateStatus");
      this.chkOneRateStatus.Checked = true;
      this.chkOneRateStatus.CheckState = CheckState.Checked;
      this.chkOneRateStatus.Name = "chkOneRateStatus";
      this.chkOneRateStatus.UseVisualStyleBackColor = true;
      this.flwRightColumn.Controls.Add((Control) this.gbxShipDbPurge);
      this.flwRightColumn.Controls.Add((Control) this.gbxSmartpostReturns);
      this.flwRightColumn.Controls.Add((Control) this.gbxInterfacePrefs);
      this.flwRightColumn.Controls.Add((Control) this.gbxCloseSettings);
      componentResourceManager.ApplyResources((object) this.flwRightColumn, "flwRightColumn");
      this.flwRightColumn.Name = "flwRightColumn";
      this.gbxShipDbPurge.BorderThickness = 1f;
      this.gbxShipDbPurge.Controls.Add((Control) this.panelFreightPurge);
      this.gbxShipDbPurge.Controls.Add((Control) this.edtCurrentMaxPurge);
      this.gbxShipDbPurge.Controls.Add((Control) this.edtCurrentMinPurge);
      this.gbxShipDbPurge.Controls.Add((Control) this.label6);
      this.gbxShipDbPurge.Controls.Add((Control) this.label5);
      this.gbxShipDbPurge.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxShipDbPurge, "gbxShipDbPurge");
      this.gbxShipDbPurge.Name = "gbxShipDbPurge";
      this.gbxShipDbPurge.RoundCorners = 5;
      this.gbxShipDbPurge.TabStop = false;
      this.panelFreightPurge.Controls.Add((Control) this.lblFreightMax);
      this.panelFreightPurge.Controls.Add((Control) this.edtFreightMinPurge);
      this.panelFreightPurge.Controls.Add((Control) this.edtFreightMaxPurge);
      this.panelFreightPurge.Controls.Add((Control) this.lblFreightMin);
      componentResourceManager.ApplyResources((object) this.panelFreightPurge, "panelFreightPurge");
      this.panelFreightPurge.Name = "panelFreightPurge";
      this.helpProvider1.SetShowHelp((Control) this.panelFreightPurge, (bool) componentResourceManager.GetObject("panelFreightPurge.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblFreightMax, "lblFreightMax");
      this.lblFreightMax.Name = "lblFreightMax";
      this.helpProvider1.SetShowHelp((Control) this.lblFreightMax, (bool) componentResourceManager.GetObject("lblFreightMax.ShowHelp"));
      this.edtFreightMinPurge.Allow = "";
      this.edtFreightMinPurge.Disallow = "";
      this.edtFreightMinPurge.eMask = eMasks.maskCustom;
      this.edtFreightMinPurge.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtFreightMinPurge, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtFreightMinPurge, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtFreightMinPurge, componentResourceManager.GetString("edtFreightMinPurge.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtFreightMinPurge, (HelpNavigator) componentResourceManager.GetObject("edtFreightMinPurge.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtFreightMinPurge, "edtFreightMinPurge");
      this.edtFreightMinPurge.Mask = "";
      this.edtFreightMinPurge.Name = "edtFreightMinPurge";
      this.helpProvider1.SetShowHelp((Control) this.edtFreightMinPurge, (bool) componentResourceManager.GetObject("edtFreightMinPurge.ShowHelp"));
      this.edtFreightMaxPurge.Allow = "";
      this.edtFreightMaxPurge.Disallow = "";
      this.edtFreightMaxPurge.eMask = eMasks.maskCustom;
      this.edtFreightMaxPurge.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtFreightMaxPurge, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtFreightMaxPurge, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtFreightMaxPurge, componentResourceManager.GetString("edtFreightMaxPurge.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtFreightMaxPurge, (HelpNavigator) componentResourceManager.GetObject("edtFreightMaxPurge.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtFreightMaxPurge, "edtFreightMaxPurge");
      this.edtFreightMaxPurge.Mask = "";
      this.edtFreightMaxPurge.Name = "edtFreightMaxPurge";
      this.helpProvider1.SetShowHelp((Control) this.edtFreightMaxPurge, (bool) componentResourceManager.GetObject("edtFreightMaxPurge.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblFreightMin, "lblFreightMin");
      this.lblFreightMin.Name = "lblFreightMin";
      this.helpProvider1.SetShowHelp((Control) this.lblFreightMin, (bool) componentResourceManager.GetObject("lblFreightMin.ShowHelp"));
      this.edtCurrentMaxPurge.Allow = "";
      this.edtCurrentMaxPurge.Disallow = "";
      this.edtCurrentMaxPurge.eMask = eMasks.maskCustom;
      this.edtCurrentMaxPurge.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtCurrentMaxPurge, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtCurrentMaxPurge, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtCurrentMaxPurge, componentResourceManager.GetString("edtCurrentMaxPurge.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtCurrentMaxPurge, (HelpNavigator) componentResourceManager.GetObject("edtCurrentMaxPurge.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtCurrentMaxPurge, "edtCurrentMaxPurge");
      this.edtCurrentMaxPurge.Mask = "";
      this.edtCurrentMaxPurge.Name = "edtCurrentMaxPurge";
      this.helpProvider1.SetShowHelp((Control) this.edtCurrentMaxPurge, (bool) componentResourceManager.GetObject("edtCurrentMaxPurge.ShowHelp"));
      this.edtCurrentMinPurge.Allow = "";
      this.edtCurrentMinPurge.Disallow = "";
      this.edtCurrentMinPurge.eMask = eMasks.maskCustom;
      this.edtCurrentMinPurge.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtCurrentMinPurge, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtCurrentMinPurge, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtCurrentMinPurge, componentResourceManager.GetString("edtCurrentMinPurge.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtCurrentMinPurge, (HelpNavigator) componentResourceManager.GetObject("edtCurrentMinPurge.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtCurrentMinPurge, "edtCurrentMinPurge");
      this.edtCurrentMinPurge.Mask = "";
      this.edtCurrentMinPurge.Name = "edtCurrentMinPurge";
      this.helpProvider1.SetShowHelp((Control) this.edtCurrentMinPurge, (bool) componentResourceManager.GetObject("edtCurrentMinPurge.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label6, "label6");
      this.label6.Name = "label6";
      componentResourceManager.ApplyResources((object) this.label5, "label5");
      this.label5.Name = "label5";
      this.gbxSmartpostReturns.BorderThickness = 1f;
      this.gbxSmartpostReturns.Controls.Add((Control) this.pnlGCTCommunicationsSettings);
      this.gbxSmartpostReturns.Controls.Add((Control) this.chkEnableReturnShipping);
      this.gbxSmartpostReturns.Controls.Add((Control) this.lnkAddSmartpostReturnAddresses);
      this.gbxSmartpostReturns.Controls.Add((Control) this.chkAddSmartpostReturnAddresses);
      this.gbxSmartpostReturns.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxSmartpostReturns, "gbxSmartpostReturns");
      this.gbxSmartpostReturns.Name = "gbxSmartpostReturns";
      this.gbxSmartpostReturns.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxSmartpostReturns, (bool) componentResourceManager.GetObject("gbxSmartpostReturns.ShowHelp"));
      this.gbxSmartpostReturns.TabStop = false;
      this.pnlGCTCommunicationsSettings.Controls.Add((Control) this.lblGCTcommunicationDescription);
      this.pnlGCTCommunicationsSettings.Controls.Add((Control) this.rdbGCTuploadHourly);
      this.pnlGCTCommunicationsSettings.Controls.Add((Control) this.rdbGCTuploadAtShiptime);
      this.pnlGCTCommunicationsSettings.Controls.Add((Control) this.lblGCTcommunicationsHeader);
      componentResourceManager.ApplyResources((object) this.pnlGCTCommunicationsSettings, "pnlGCTCommunicationsSettings");
      this.pnlGCTCommunicationsSettings.Name = "pnlGCTCommunicationsSettings";
      this.helpProvider1.SetShowHelp((Control) this.pnlGCTCommunicationsSettings, (bool) componentResourceManager.GetObject("pnlGCTCommunicationsSettings.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblGCTcommunicationDescription, "lblGCTcommunicationDescription");
      this.lblGCTcommunicationDescription.Name = "lblGCTcommunicationDescription";
      componentResourceManager.ApplyResources((object) this.rdbGCTuploadHourly, "rdbGCTuploadHourly");
      this.rdbGCTuploadHourly.Name = "rdbGCTuploadHourly";
      this.helpProvider1.SetShowHelp((Control) this.rdbGCTuploadHourly, (bool) componentResourceManager.GetObject("rdbGCTuploadHourly.ShowHelp"));
      this.rdbGCTuploadHourly.UseVisualStyleBackColor = true;
      this.rdbGCTuploadAtShiptime.Checked = true;
      componentResourceManager.ApplyResources((object) this.rdbGCTuploadAtShiptime, "rdbGCTuploadAtShiptime");
      this.rdbGCTuploadAtShiptime.Name = "rdbGCTuploadAtShiptime";
      this.helpProvider1.SetShowHelp((Control) this.rdbGCTuploadAtShiptime, (bool) componentResourceManager.GetObject("rdbGCTuploadAtShiptime.ShowHelp"));
      this.rdbGCTuploadAtShiptime.TabStop = true;
      this.rdbGCTuploadAtShiptime.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.lblGCTcommunicationsHeader, "lblGCTcommunicationsHeader");
      this.lblGCTcommunicationsHeader.Name = "lblGCTcommunicationsHeader";
      this.helpProvider1.SetShowHelp((Control) this.lblGCTcommunicationsHeader, (bool) componentResourceManager.GetObject("lblGCTcommunicationsHeader.ShowHelp"));
      this.chkEnableReturnShipping.AutoCheck = false;
      componentResourceManager.ApplyResources((object) this.chkEnableReturnShipping, "chkEnableReturnShipping");
      this.chkEnableReturnShipping.Name = "chkEnableReturnShipping";
      this.chkEnableReturnShipping.UseVisualStyleBackColor = true;
      this.chkEnableReturnShipping.Click += new EventHandler(this.chkEnableReturnShipping_Click);
      componentResourceManager.ApplyResources((object) this.lnkAddSmartpostReturnAddresses, "lnkAddSmartpostReturnAddresses");
      this.lnkAddSmartpostReturnAddresses.Name = "lnkAddSmartpostReturnAddresses";
      this.lnkAddSmartpostReturnAddresses.TabStop = true;
      this.lnkAddSmartpostReturnAddresses.Tag = (object) "4895";
      this.lnkAddSmartpostReturnAddresses.UseCompatibleTextRendering = true;
      this.lnkAddSmartpostReturnAddresses.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
      componentResourceManager.ApplyResources((object) this.chkAddSmartpostReturnAddresses, "chkAddSmartpostReturnAddresses");
      this.chkAddSmartpostReturnAddresses.Name = "chkAddSmartpostReturnAddresses";
      this.chkAddSmartpostReturnAddresses.UseVisualStyleBackColor = true;
      this.gbxInterfacePrefs.BorderThickness = 1f;
      this.gbxInterfacePrefs.Controls.Add((Control) this.cboFontSize);
      this.gbxInterfacePrefs.Controls.Add((Control) this.lblFontSize);
      this.gbxInterfacePrefs.Controls.Add((Control) this.btnFieldColor);
      this.gbxInterfacePrefs.Controls.Add((Control) this.txtSampleText);
      this.gbxInterfacePrefs.Controls.Add((Control) this.chkDisplayPickupTab);
      this.gbxInterfacePrefs.Controls.Add((Control) this.label3);
      this.gbxInterfacePrefs.Controls.Add((Control) this.cboDateFormat);
      this.gbxInterfacePrefs.Controls.Add((Control) this.label2);
      this.gbxInterfacePrefs.Controls.Add((Control) this.cboLanguage);
      this.gbxInterfacePrefs.Controls.Add((Control) this.label1);
      this.gbxInterfacePrefs.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxInterfacePrefs, "gbxInterfacePrefs");
      this.gbxInterfacePrefs.Name = "gbxInterfacePrefs";
      this.gbxInterfacePrefs.RoundCorners = 5;
      this.gbxInterfacePrefs.TabStop = false;
      this.cboFontSize.DropDownStyle = ComboBoxStyle.DropDownList;
      this.focusExtender1.SetFocusBackColor((Control) this.cboFontSize, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboFontSize, SystemColors.WindowText);
      this.cboFontSize.FormattingEnabled = true;
      this.cboFontSize.Items.AddRange(new object[3]
      {
        (object) componentResourceManager.GetString("cboFontSize.Items"),
        (object) componentResourceManager.GetString("cboFontSize.Items1"),
        (object) componentResourceManager.GetString("cboFontSize.Items2")
      });
      componentResourceManager.ApplyResources((object) this.cboFontSize, "cboFontSize");
      this.cboFontSize.Name = "cboFontSize";
      componentResourceManager.ApplyResources((object) this.lblFontSize, "lblFontSize");
      this.lblFontSize.Name = "lblFontSize";
      this.helpProvider1.SetHelpKeyword((Control) this.btnFieldColor, componentResourceManager.GetString("btnFieldColor.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnFieldColor, (HelpNavigator) componentResourceManager.GetObject("btnFieldColor.HelpNavigator"));
      this.btnFieldColor.Image = (Image) Resources.FieldColor;
      componentResourceManager.ApplyResources((object) this.btnFieldColor, "btnFieldColor");
      this.btnFieldColor.Name = "btnFieldColor";
      this.helpProvider1.SetShowHelp((Control) this.btnFieldColor, (bool) componentResourceManager.GetObject("btnFieldColor.ShowHelp"));
      this.toolTip1.SetToolTip((Control) this.btnFieldColor, componentResourceManager.GetString("btnFieldColor.ToolTip"));
      this.btnFieldColor.UseVisualStyleBackColor = true;
      this.btnFieldColor.Click += new EventHandler(this.btnFieldColor_Click);
      this.txtSampleText.BackColor = SystemColors.Window;
      this.txtSampleText.BorderStyle = BorderStyle.Fixed3D;
      this.txtSampleText.ForeColor = SystemColors.WindowText;
      this.helpProvider1.SetHelpKeyword((Control) this.txtSampleText, componentResourceManager.GetString("txtSampleText.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.txtSampleText, (HelpNavigator) componentResourceManager.GetObject("txtSampleText.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.txtSampleText, "txtSampleText");
      this.txtSampleText.Name = "txtSampleText";
      this.helpProvider1.SetShowHelp((Control) this.txtSampleText, (bool) componentResourceManager.GetObject("txtSampleText.ShowHelp"));
      this.txtSampleText.BackColorChanged += new EventHandler(this.txtSampleText_BackColorChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.chkDisplayPickupTab, componentResourceManager.GetString("chkDisplayPickupTab.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkDisplayPickupTab, (HelpNavigator) componentResourceManager.GetObject("chkDisplayPickupTab.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkDisplayPickupTab, "chkDisplayPickupTab");
      this.chkDisplayPickupTab.Name = "chkDisplayPickupTab";
      this.helpProvider1.SetShowHelp((Control) this.chkDisplayPickupTab, (bool) componentResourceManager.GetObject("chkDisplayPickupTab.ShowHelp"));
      this.chkDisplayPickupTab.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.label3, "label3");
      this.label3.Name = "label3";
      componentResourceManager.ApplyResources((object) this.cboDateFormat, "cboDateFormat");
      this.cboDateFormat.DropDownStyle = ComboBoxStyle.DropDownList;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDateFormat, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDateFormat, SystemColors.WindowText);
      this.cboDateFormat.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDateFormat, componentResourceManager.GetString("cboDateFormat.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboDateFormat, (HelpNavigator) componentResourceManager.GetObject("cboDateFormat.HelpNavigator"));
      this.cboDateFormat.Name = "cboDateFormat";
      this.helpProvider1.SetShowHelp((Control) this.cboDateFormat, (bool) componentResourceManager.GetObject("cboDateFormat.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      componentResourceManager.ApplyResources((object) this.cboLanguage, "cboLanguage");
      this.cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
      this.focusExtender1.SetFocusBackColor((Control) this.cboLanguage, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboLanguage, SystemColors.WindowText);
      this.cboLanguage.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboLanguage, componentResourceManager.GetString("cboLanguage.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboLanguage, (HelpNavigator) componentResourceManager.GetObject("cboLanguage.HelpNavigator"));
      this.cboLanguage.Name = "cboLanguage";
      this.helpProvider1.SetShowHelp((Control) this.cboLanguage, (bool) componentResourceManager.GetObject("cboLanguage.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.gbxCloseSettings.BorderThickness = 1f;
      this.gbxCloseSettings.Controls.Add((Control) this.pnlGroundClose);
      this.gbxCloseSettings.Controls.Add((Control) this.edtSmartpostAutoCloseTime);
      this.gbxCloseSettings.Controls.Add((Control) this.lblSmartpostClose);
      this.gbxCloseSettings.Controls.Add((Control) this.edtExpressAutoCloseTime);
      this.gbxCloseSettings.Controls.Add((Control) this.lblExpressClose);
      this.gbxCloseSettings.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxCloseSettings, "gbxCloseSettings");
      this.gbxCloseSettings.Name = "gbxCloseSettings";
      this.gbxCloseSettings.RoundCorners = 5;
      this.gbxCloseSettings.TabStop = false;
      this.pnlGroundClose.Controls.Add((Control) this.edtGroundAutoCloseTime);
      this.pnlGroundClose.Controls.Add((Control) this.lblGroundClose);
      componentResourceManager.ApplyResources((object) this.pnlGroundClose, "pnlGroundClose");
      this.pnlGroundClose.Name = "pnlGroundClose";
      this.edtGroundAutoCloseTime.Allow = "";
      this.edtGroundAutoCloseTime.Disallow = "";
      this.edtGroundAutoCloseTime.eMask = eMasks.maskCustom;
      this.edtGroundAutoCloseTime.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtGroundAutoCloseTime, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtGroundAutoCloseTime, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtGroundAutoCloseTime, componentResourceManager.GetString("edtGroundAutoCloseTime.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtGroundAutoCloseTime, (HelpNavigator) componentResourceManager.GetObject("edtGroundAutoCloseTime.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtGroundAutoCloseTime, "edtGroundAutoCloseTime");
      this.edtGroundAutoCloseTime.Mask = "#0:00 >BB";
      this.edtGroundAutoCloseTime.Name = "edtGroundAutoCloseTime";
      this.helpProvider1.SetShowHelp((Control) this.edtGroundAutoCloseTime, (bool) componentResourceManager.GetObject("edtGroundAutoCloseTime.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblGroundClose, "lblGroundClose");
      this.lblGroundClose.Name = "lblGroundClose";
      this.edtSmartpostAutoCloseTime.Allow = "";
      this.edtSmartpostAutoCloseTime.Disallow = "";
      this.edtSmartpostAutoCloseTime.eMask = eMasks.maskCustom;
      componentResourceManager.ApplyResources((object) this.edtSmartpostAutoCloseTime, "edtSmartpostAutoCloseTime");
      this.edtSmartpostAutoCloseTime.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtSmartpostAutoCloseTime, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtSmartpostAutoCloseTime, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtSmartpostAutoCloseTime, componentResourceManager.GetString("edtSmartpostAutoCloseTime.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtSmartpostAutoCloseTime, (HelpNavigator) componentResourceManager.GetObject("edtSmartpostAutoCloseTime.HelpNavigator"));
      this.edtSmartpostAutoCloseTime.Mask = "99:99 >BB~/^[0-9]{0,4} ?([AP]M?)?$/~";
      this.edtSmartpostAutoCloseTime.Name = "edtSmartpostAutoCloseTime";
      this.helpProvider1.SetShowHelp((Control) this.edtSmartpostAutoCloseTime, (bool) componentResourceManager.GetObject("edtSmartpostAutoCloseTime.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblSmartpostClose, "lblSmartpostClose");
      this.lblSmartpostClose.Name = "lblSmartpostClose";
      this.helpProvider1.SetShowHelp((Control) this.lblSmartpostClose, (bool) componentResourceManager.GetObject("lblSmartpostClose.ShowHelp"));
      this.edtExpressAutoCloseTime.Allow = "";
      this.edtExpressAutoCloseTime.Disallow = "";
      this.edtExpressAutoCloseTime.eMask = eMasks.maskCustom;
      this.edtExpressAutoCloseTime.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtExpressAutoCloseTime, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtExpressAutoCloseTime, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtExpressAutoCloseTime, componentResourceManager.GetString("edtExpressAutoCloseTime.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtExpressAutoCloseTime, (HelpNavigator) componentResourceManager.GetObject("edtExpressAutoCloseTime.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtExpressAutoCloseTime, "edtExpressAutoCloseTime");
      this.edtExpressAutoCloseTime.Mask = "#0:00 >BB";
      this.edtExpressAutoCloseTime.Name = "edtExpressAutoCloseTime";
      this.helpProvider1.SetShowHelp((Control) this.edtExpressAutoCloseTime, (bool) componentResourceManager.GetObject("edtExpressAutoCloseTime.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblExpressClose, "lblExpressClose");
      this.lblExpressClose.Name = "lblExpressClose";
      this.colorDialogFieldBackColor.AllowFullOpen = false;
      this.colorDialogFieldBackColor.SolidColorOnly = true;
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.Name = nameof (SystemSettingsUser);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.flwLeftColumn.ResumeLayout(false);
      this.gbxSystemSettings.ResumeLayout(false);
      this.gbxExpressIntlSettings.ResumeLayout(false);
      this.gbxFegCloseSettings.ResumeLayout(false);
      this.gbxCommOverride.ResumeLayout(false);
      this.gbxCommOverride.PerformLayout();
      this.gbxElectronicTradeDocuments.ResumeLayout(false);
      this.gbxElectronicTradeDocuments.PerformLayout();
      this.pnlEtdSettings.ResumeLayout(false);
      this.pnlEtdSettings.PerformLayout();
      this.pnlEtdInformation.ResumeLayout(false);
      this.pnlEtdInformation.PerformLayout();
      this.gbxLabelInformation.ResumeLayout(false);
      this.pnlPrintGroundReadableBarcode.ResumeLayout(false);
      this.pnlPrintGroundReadableBarcode.PerformLayout();
      this.pnlHoldLocation.ResumeLayout(false);
      this.pnlHoldLocation.PerformLayout();
      this.pnlOneRateDisplay.ResumeLayout(false);
      this.pnlOneRateDisplay.PerformLayout();
      this.flwRightColumn.ResumeLayout(false);
      this.gbxShipDbPurge.ResumeLayout(false);
      this.gbxShipDbPurge.PerformLayout();
      this.panelFreightPurge.ResumeLayout(false);
      this.panelFreightPurge.PerformLayout();
      this.gbxSmartpostReturns.ResumeLayout(false);
      this.gbxSmartpostReturns.PerformLayout();
      this.pnlGCTCommunicationsSettings.ResumeLayout(false);
      this.pnlGCTCommunicationsSettings.PerformLayout();
      this.gbxInterfacePrefs.ResumeLayout(false);
      this.gbxCloseSettings.ResumeLayout(false);
      this.gbxCloseSettings.PerformLayout();
      this.pnlGroundClose.ResumeLayout(false);
      this.pnlGroundClose.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
