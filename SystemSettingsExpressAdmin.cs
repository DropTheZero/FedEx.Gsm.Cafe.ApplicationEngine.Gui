// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SystemSettingsExpressAdmin
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.BackupRestore;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class SystemSettingsExpressAdmin : UserControlHelpEx
  {
    private bool _combosLoaded;
    private Utility.RestoreMode restoreMode;
    private SystemSettingsObject settings = new SystemSettingsObject();
    private string meterNumber;
    private string newMeterNumber;
    private string accountNumber;
    private string newAccountNumber;
    private string description;
    private bool bGroundEnabled;
    private bool bSmartPostEnabled;
    private string startGroundTrkNumber;
    private string nextGroundTrkNumber;
    private string endGroundTrkNumber;
    private string startSmartPostTrkNumber;
    private string nextSmartPostTrkNumber;
    private string endSmartPostTrkNumber;
    private SystemSettings _myParent;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel4;
    private ColorGroupBox gbxSerialNumbers;
    private ColorGroupBox colorGroupBox22;
    private ColorGroupBox colorGroupBox21;
    private ColorGroupBox colorGroupBox20;
    private ColorGroupBox colorGroupBox19;
    private ColorGroupBox colorGroupBox18;
    private ColorGroupBox colorGroupBox17;
    private ColorGroupBox colorGroupBox16;
    private FdxMaskedEdit edtZipPostal;
    private TextBox edtCity;
    private MaskedTextBox edtAccountNumber;
    private ComboBoxEx cboCountry;
    private Label txtOriginId;
    private Label label9;
    private Label txtStateProvince;
    private Label label7;
    private Label label6;
    private Label label4;
    private Label label3;
    private Label label2;
    private Label label1;
    private FdxMaskedEdit edtReceiveDownloadTime;
    private Button btnSetCommunications;
    private CheckBox chkLabelTransfer;
    private CheckBox chkAutoPassportListening;
    private CheckBox chkBatch;
    private CheckBox chkFedExProvidedLinehaul;
    private CheckBox chkEnableTd;
    private CheckBox chkNoUploadNoShip;
    private CheckBox chkMaskCustomsValue;
    private CheckBox chkActivateUnlimitedFutureDay;
    private Label label17;
    private TextBox edtReportPrinterNum;
    private Label label18;
    private TextBox edtValidatorNum;
    private Label label19;
    private TextBox edtWaybillPrinterNum;
    private Label label20;
    private TextBox edtPalPrinterNum;
    private Label label14;
    private TextBox edtScaleNum;
    private Label label15;
    private TextBox edtKeyboardNum;
    private Label label13;
    private TextBox edtMonitorNum;
    private Label label12;
    private TextBox edtBpuNum;
    private Label label11;
    private TextBox edtEmployeeNum;
    private Panel panel1;
    private Button btnRestoreMeterAccount;
    private PanelEx panelCommStuff;
    private NumericUpDown spnReconcileWindow;
    private NumericUpDown spnCloseHourlyUploadWindow;
    private Label label10;
    private Label label8;
    private CheckBox chkOverrideDialtoneCheck;
    private Label label21;
    private Label label16;
    private FocusExtender focusExtender1;
    private CheckBox chkDisableShortcuts;
    private ColorGroupBox colorGroupBox1;
    private CheckBox chkDisableAutoTrack;
    private CheckBox chkEnableOneRate;

    public string City => this.edtCity.Text;

    public string StateProvince => this.txtStateProvince.Text;

    public string PostalCode => this.edtZipPostal.Raw;

    public string Country => this.cboCountry.SelectedValue as string;

    public string CountryNumber
    {
      get
      {
        return this.cboCountry.SelectedItem == null ? (string) null : ((DataRowView) this.cboCountry.SelectedItem)[nameof (CountryNumber)].ToString();
      }
    }

    public string AccountNumber => this.edtAccountNumber.Text;

    public DateTime ReconcileTime
    {
      set
      {
        this.edtReceiveDownloadTime.Text = value.ToString("hh:mm tt", (IFormatProvider) CultureInfo.InvariantCulture.DateTimeFormat);
      }
    }

    public SystemSettings MyParent
    {
      get => this._myParent;
      set => this._myParent = value;
    }

    public event EventHandler<BoolEventArgs> OneRateEnabledChanged;

    public SystemSettingsExpressAdmin()
    {
      this.InitializeComponent();
      this.txtStateProvince.Text = string.Empty;
      this.txtOriginId.Text = string.Empty;
    }

    public void ScreenToObjects(SystemSettingsObject settings)
    {
      if (settings.AccountObject != null && settings.AccountObject.BoxInfo != null)
      {
        if (!Utility.SoftwareOnly)
        {
          settings.AccountObject.BoxInfo.BaseUnit = this.edtBpuNum.Text;
          settings.AccountObject.BoxInfo.Monitor = this.edtMonitorNum.Text;
          settings.AccountObject.BoxInfo.Keyboard = this.edtKeyboardNum.Text;
          settings.AccountObject.BoxInfo.Scale = this.edtScaleNum.Text;
          settings.AccountObject.BoxInfo.AirbillPrinter = this.edtWaybillPrinterNum.Text;
          settings.AccountObject.BoxInfo.Report = this.edtReportPrinterNum.Text;
          settings.AccountObject.BoxInfo.PalPrinter = this.edtPalPrinterNum.Text;
          settings.AccountObject.BoxInfo.Validator = this.edtValidatorNum.Text;
          settings.AccountObject.BoxInfo.SystemName = "1";
          settings.AccountObject.BoxInfo.SystemType = "1";
          settings.AccountObject.BoxInfo.SystemConfigType = "1";
          settings.AccountObject.EmployeeNumber = this.edtEmployeeNum.Text;
        }
        else
        {
          settings.AccountObject.BoxInfo.BaseUnit = "1";
          settings.AccountObject.BoxInfo.Monitor = "1";
          settings.AccountObject.BoxInfo.Keyboard = "1";
          settings.AccountObject.BoxInfo.Scale = "1";
          settings.AccountObject.BoxInfo.AirbillPrinter = "1";
          settings.AccountObject.BoxInfo.Report = "1";
          settings.AccountObject.BoxInfo.PalPrinter = "1";
          settings.AccountObject.BoxInfo.Validator = "1";
          settings.AccountObject.BoxInfo.SystemName = "1";
          settings.AccountObject.BoxInfo.SystemType = "1";
          settings.AccountObject.BoxInfo.SystemConfigType = "1";
          settings.AccountObject.EmployeeNumber = "1";
        }
      }
      if (settings.RegistrySettings != null)
      {
        settings.RegistrySettings["Auto_Passport_Listening"].Value = this.chkAutoPassportListening.Checked ? (object) "Y" : (object) "N";
        if (this.panelCommStuff.IsVisible)
        {
          settings.RegistrySettings["PermanentDTChkOverride"].Value = this.chkOverrideDialtoneCheck.Checked ? (object) "1" : (object) "0";
          settings.RegistrySettings["CloseForgivenessWindow"].Value = (object) this.spnCloseHourlyUploadWindow.Value.ToString("F0", (IFormatProvider) CultureInfo.InvariantCulture);
          settings.RegistrySettings["ReconForgivenessWindow"].Value = (object) this.spnReconcileWindow.Value.ToString("F0", (IFormatProvider) CultureInfo.InvariantCulture);
        }
        settings.RegistrySettings["DisableSpecSvcOnShipDetails"].Value = this.chkDisableShortcuts.Checked ? (object) "Y" : (object) "N";
        settings.RegistrySettings["DisableAutoTrack"].Value = this.chkDisableAutoTrack.Checked ? (object) "Y" : (object) "N";
      }
      if (settings.AccountObject == null)
        return;
      settings.AccountObject.OriginId = this.txtOriginId.Text;
      settings.AccountObject.AccountNumber = this.edtAccountNumber.Text;
      settings.AccountObject.PassPortEnabled = this.chkBatch.Checked ? 1 : 0;
      settings.AccountObject.IsLabelTransferEnabled = this.chkLabelTransfer.Checked;
      settings.AccountObject.UnlimitedFutureDayEnabled = this.chkActivateUnlimitedFutureDay.Checked;
      settings.AccountObject.ManualB13AFedExToStamp = false;
      settings.AccountObject.MaskCustomsValue = this.chkMaskCustomsValue.Checked;
      if (settings.AccountObject.Address == null)
        settings.AccountObject.Address = new Address();
      settings.AccountObject.Address.CountryCode = this.cboCountry.SelectedValue as string;
      try
      {
        settings.AccountObject.Address.CountryNumber = ((DataRowView) this.cboCountry.SelectedItem)["CountryNumber"].ToString();
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "SystemSettingsExpressAdmin.ScreenToObjects", "Exception getting country number : " + ex?.ToString());
      }
      settings.AccountObject.Address.PostalCode = this.edtZipPostal.Raw;
      settings.AccountObject.Address.StateProvince = this.txtStateProvince.Text;
      settings.AccountObject.Address.City = this.edtCity.Text;
      settings.AccountObject.IsNoUploadNoShip = this.chkNoUploadNoShip.Checked;
      DateTime dateTime = new DateTime();
      DateTime result = new DateTime();
      if (!DateTime.TryParse(this.edtReceiveDownloadTime.Text, out result))
        throw new TabPageException(this.Parent as TabPage, (Control) this.edtReceiveDownloadTime, 3611);
      settings.AccountObject.ReceiveDownloadTime = result;
      settings.AccountObject.UploadInterval = 3;
      settings.AccountObject.IsTDEnabled = this.chkEnableTd.Checked;
      settings.AccountObject.OneRateEnabled = this.chkEnableOneRate.Checked;
    }

    public int ObjectsToScreen(SystemSettingsObject settings, Utility.FormOperation op)
    {
      if (!this.DesignMode)
        this.PopulateCombos();
      if (!Utility.SoftwareOnly && settings.AccountObject.BoxInfo != null)
      {
        this.edtBpuNum.Text = settings.AccountObject.BoxInfo.BaseUnit;
        this.edtMonitorNum.Text = settings.AccountObject.BoxInfo.Monitor;
        this.edtKeyboardNum.Text = settings.AccountObject.BoxInfo.Keyboard;
        this.edtScaleNum.Text = settings.AccountObject.BoxInfo.Scale;
        this.edtWaybillPrinterNum.Text = settings.AccountObject.BoxInfo.Printer;
        this.edtPalPrinterNum.Text = settings.AccountObject.BoxInfo.PalPrinter;
        this.edtWaybillPrinterNum.Text = settings.AccountObject.BoxInfo.AirbillPrinter;
        this.edtReportPrinterNum.Text = settings.AccountObject.BoxInfo.Report;
        this.edtValidatorNum.Text = settings.AccountObject.BoxInfo.Validator;
        this.edtEmployeeNum.Text = settings.AccountObject.EmployeeNumber;
      }
      if (settings.AccountObject != null)
      {
        this.edtAccountNumber.Text = settings.AccountObject.AccountNumber;
        this.MyParent.ProcessAutoCloseTimes(settings.AccountObject);
        if (settings.AccountObject.IsTDAllowed)
        {
          this.chkEnableTd.Enabled = true;
          this.chkEnableTd.Checked = settings.AccountObject.IsTDEnabled;
        }
        else
        {
          this.chkEnableTd.Enabled = false;
          this.chkEnableTd.Checked = false;
        }
        this.chkFedExProvidedLinehaul.Checked = settings.AccountObject.UseLineHaul;
        if (op == Utility.FormOperation.Add || op == Utility.FormOperation.AddByDup)
        {
          if (!Utility.SoftwareOnly)
            this.chkNoUploadNoShip.Checked = false;
          else if (settings.AccountObject.Address != null && !string.IsNullOrEmpty(settings.AccountObject.Address.CountryCode))
            this.chkNoUploadNoShip.Checked = Utility.IsLacCountry(settings.AccountObject.Address.CountryCode) && !Utility.IsPuertoRico(settings.AccountObject.Address);
        }
        else
          this.chkNoUploadNoShip.Checked = settings.AccountObject.IsNoUploadNoShip;
        if (settings.AccountObject.Address != null)
        {
          if (Utility.IsLacCountry(settings.AccountObject.Address.CountryCode))
          {
            this.chkMaskCustomsValue.Enabled = false;
            this.chkMaskCustomsValue.Checked = false;
          }
          else
            this.chkMaskCustomsValue.Enabled = true;
        }
        this.chkMaskCustomsValue.Checked = op != Utility.FormOperation.AddFirst && op != Utility.FormOperation.Add && settings.AccountObject.MaskCustomsValue;
        this.chkActivateUnlimitedFutureDay.Checked = settings.AccountObject.UnlimitedFutureDayEnabled;
        this.chkBatch.Checked = settings.AccountObject.PassPortEnabled != 0;
        this.chkLabelTransfer.Checked = settings.AccountObject.IsLabelTransferEnabled;
        this.txtOriginId.Text = settings.AccountObject.OriginId;
        this.chkEnableOneRate.Checked = settings.AccountObject.OneRateEnabled;
        switch (op)
        {
          case Utility.FormOperation.Add:
            Account output;
            if (GuiData.AppController.ShipEngine.Retrieve<Account>(new Account()
            {
              IsMaster = true
            }, out output, out Error _) == 1)
            {
              if (Utility.IsUS50(output.Address))
              {
                this.cboCountry.Enabled = true;
                if (!Utility.DevMode)
                  Utility.PopulateCountryCombo((ComboBox) this.cboCountry, Utility.CountryComboType.USCA);
                if (settings.AccountObject.Address != null)
                {
                  this.cboCountry.SelectedValue = (object) settings.AccountObject.Address.CountryCode;
                  break;
                }
                break;
              }
              if (!Utility.DevMode)
                this.cboCountry.Enabled = false;
              if (op == Utility.FormOperation.AddByDup)
              {
                this.edtCity.Text = settings.AccountObject.Address.City;
                this.txtStateProvince.Text = settings.AccountObject.Address.StateProvince;
                this.edtZipPostal.Text = settings.AccountObject.Address.PostalCode;
              }
              this.cboCountry.SelectedValue = (object) output.Address.CountryCode;
              break;
            }
            break;
          case Utility.FormOperation.AddByDup:
            if (settings.AccountObject.IsMaster)
              break;
            goto case Utility.FormOperation.Add;
        }
        if (settings.AccountObject.Address != null)
        {
          if (op != Utility.FormOperation.Add && op != Utility.FormOperation.AddByDup)
          {
            this.edtCity.Text = settings.AccountObject.Address.City;
            this.txtStateProvince.Text = settings.AccountObject.Address.StateProvince;
            this.edtZipPostal.Text = settings.AccountObject.Address.PostalCode;
            this.cboCountry.SelectedValue = (object) settings.AccountObject.Address.CountryCode;
          }
          this.edtCity.Text = settings.AccountObject.Address.City;
          this.txtStateProvince.Text = settings.AccountObject.Address.StateProvince;
          this.edtZipPostal.Text = settings.AccountObject.Address.PostalCode;
        }
      }
      if (op == Utility.FormOperation.ViewEdit)
      {
        this.edtAccountNumber.ReadOnly = true;
        this.cboCountry.Enabled = false;
      }
      if (settings.RegistrySettings != null)
      {
        this.chkAutoPassportListening.Checked = settings.RegistrySettings["Auto_Passport_Listening"].Value.ToString() == "Y";
        try
        {
          this.chkDisableShortcuts.Checked = settings.RegistrySettings["DisableSpecSvcOnShipDetails"].Value.ToString() == "Y";
        }
        catch (KeyNotFoundException ex)
        {
          this.chkDisableShortcuts.Checked = false;
        }
        try
        {
          this.chkDisableAutoTrack.Checked = settings.RegistrySettings["DisableAutoTrack"].Value.ToString() == "Y";
        }
        catch (KeyNotFoundException ex)
        {
          this.chkDisableAutoTrack.Checked = false;
        }
      }
      string b = settings.RegistrySettings["PermanentDTChkOverride"].Value.ToString();
      if (!Utility.IsSoftwareOnly)
      {
        this.chkOverrideDialtoneCheck.Checked = string.IsNullOrEmpty(b) || !"0".Equals(b) && !string.Equals("N", b, StringComparison.InvariantCultureIgnoreCase) && !string.Equals("false", b, StringComparison.InvariantCultureIgnoreCase);
        try
        {
          Decimal result;
          if (Decimal.TryParse(settings.RegistrySettings["CloseForgivenessWindow"].Value.ToString(), out result))
          {
            if (result >= this.spnCloseHourlyUploadWindow.Minimum)
            {
              if (result <= this.spnCloseHourlyUploadWindow.Maximum)
                this.spnCloseHourlyUploadWindow.Value = result;
            }
          }
        }
        catch (ArgumentOutOfRangeException ex)
        {
        }
        try
        {
          Decimal result;
          if (Decimal.TryParse(settings.RegistrySettings["ReconForgivenessWindow"].Value.ToString(), out result))
          {
            if (result >= this.spnReconcileWindow.Minimum)
            {
              if (result <= this.spnReconcileWindow.Maximum)
                this.spnReconcileWindow.Value = result;
            }
          }
        }
        catch (ArgumentOutOfRangeException ex)
        {
        }
      }
      else
      {
        this.panelCommStuff.IsVisible = false;
        this.gbxSerialNumbers.Hide();
      }
      this.btnRestoreMeterAccount.Visible = op == Utility.FormOperation.Add || op == Utility.FormOperation.AddFirst;
      return 0;
    }

    public bool HandleError(int error)
    {
      bool flag = true;
      Control control = (Control) null;
      switch (error)
      {
        case 2444:
        case 3445:
        case 3607:
        case 3608:
          control = (Control) this.edtAccountNumber;
          break;
        case 3434:
          control = (Control) this.edtCity;
          break;
        case 3600:
          control = (Control) this.edtBpuNum;
          break;
        case 3601:
          control = (Control) this.edtMonitorNum;
          break;
        case 3602:
          control = (Control) this.edtKeyboardNum;
          break;
        case 3603:
          control = (Control) this.edtReportPrinterNum;
          break;
        case 3604:
          control = (Control) this.edtPalPrinterNum;
          break;
        case 3605:
        case 3606:
          control = (Control) this.edtEmployeeNum;
          break;
        case 3611:
          control = (Control) this.spnReconcileWindow;
          break;
        case 13728:
          control = (Control) this.edtReceiveDownloadTime;
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

    public void cboCountry_SelectedValueChanged(object sender, EventArgs e)
    {
      ComboBox comboBox = sender as ComboBox;
      if (comboBox.SelectedValue != null)
      {
        switch (comboBox.SelectedValue.ToString())
        {
          case "US":
            this.edtZipPostal.SetMask(eMasks.maskZipCodeNine);
            break;
          case "CA":
            this.edtZipPostal.SetMask(eMasks.maskCanadianZipCode);
            break;
          case "BR":
            this.edtZipPostal.SetMask(eMasks.maskBRZipCode);
            break;
          case "MX":
            this.edtZipPostal.SetMask(eMasks.maskMXZipCode);
            break;
          default:
            this.edtZipPostal.SetMask(eMasks.maskIntlZipCode);
            break;
        }
      }
      this.edtZipPostal.Enabled = comboBox.SelectedIndex != -1;
      string country = this.settings.AccountObject == null || this.settings.AccountObject.Address == null ? this.cboCountry.SelectedValue as string : this.settings.AccountObject.Address.CountryCode;
      if (country != null && !string.IsNullOrEmpty(country.Trim()) && Utility.IsLacCountry(country))
      {
        this.chkMaskCustomsValue.Enabled = false;
        this.chkMaskCustomsValue.Checked = false;
      }
      else
        this.chkMaskCustomsValue.Enabled = true;
      if (this._myParent == null)
        return;
      EventTopic topic = (EventTopic) this._myParent.SystemSettingsEventBroker.GetTopic("SystemSettingsCountryChanged");
      if (topic == null)
        return;
      string number = (string) null;
      if (comboBox.SelectedItem != null)
      {
        try
        {
          number = ((DataRowView) comboBox.SelectedItem)["CountryNumber"].ToString();
        }
        catch (Exception ex)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "SystemSettingsExpressAdmin.CountryChanged", "Exception firing SystemSettingsCountryChanged: " + ex?.ToString());
        }
      }
      CountryChangedEventArgs args = new CountryChangedEventArgs(comboBox.SelectedValue as string, number);
      topic.InjectEventArgs((EventArgs) args);
    }

    public void SetupEvents()
    {
      if (this._myParent == null)
        return;
      this._myParent.SystemSettingsEventBroker.GetTopic("SystemSettingsCountryChanged")?.AddPublisher((object) this.cboCountry, "SelectedValueChanged");
      this._myParent.SystemSettingsEventBroker.AddPublisher("OneRateEnabledChanged", (object) this, "OneRateEnabledChanged");
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.DesignMode)
        return;
      this.PopulateCombos();
    }

    private bool HandleError(Error error)
    {
      Utility.DisplayError(error);
      return error.Code != 1 && this.HandleError(error.Code);
    }

    private void PopulateCombos()
    {
      if (!this._combosLoaded)
        Utility.PopulateCountryCombo((ComboBox) this.cboCountry, Utility.CountryComboType.Origin);
      this._combosLoaded = true;
    }

    private void edtZipPostal_Leave(object sender, EventArgs e)
    {
      PostalInformationResponse validatedPostal = GuiData.AppController.ShipEngine.GetValidatedPostal(new Address()
      {
        CountryCode = this.cboCountry.SelectedValue.ToString(),
        CountryNumber = ((DataRowView) this.cboCountry.SelectedItem)["CountryNumber"].ToString(),
        City = this.edtCity.Text,
        PostalCode = this.edtZipPostal.Raw
      }, Address.AddressType.SystemSettings);
      if (validatedPostal.IsOperationOk || validatedPostal.HasWarning && !validatedPostal.HasError)
      {
        Address validatedAddress = validatedPostal.ValidatedAddress;
        if (validatedAddress.CountryCode != "MX")
          this.txtStateProvince.Text = validatedAddress.StateProvince;
        else
          this.txtStateProvince.Text = string.Empty;
        this.txtOriginId.Text = validatedAddress.URSALocID;
      }
      this._myParent.GroundAdminPage.EnableGroundCheckBox();
    }

    private void cboCountry_Leave(object sender, EventArgs e)
    {
      this.edtZipPostal.Enabled = this.cboCountry.SelectedIndex != -1;
    }

    private void btnSetCommunications_Click(object sender, EventArgs e)
    {
      try
      {
        Process.Start(new FedEx.Gsm.Common.ConfigManager.ConfigManager().InstallLocs.BinDirectory + "\\GsmCommSetup.exe");
      }
      catch (Exception ex)
      {
        int num = (int) Utility.DisplayError(GuiData.Languafier.Translate("ErrorStartingCommSetup"), Error.ErrorType.Failure);
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), ex.Message);
      }
    }

    private void SystemSettingsExpressAdmin_Load(object sender, EventArgs e)
    {
    }

    private void cboCountry_KeyPress(object sender, KeyPressEventArgs e) => this.OnKeyPress(e);

    private void cboCountry_KeyDown(object sender, KeyEventArgs e) => this.OnKeyDown(e);

    private void btnRestoreMeterAccount_Click(object sender, EventArgs e)
    {
      string str = "\\FSM_BACKUP";
      FedEx.Gsm.Common.Languafier.Languafier languafier = new FedEx.Gsm.Common.Languafier.Languafier();
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
      folderBrowserDialog.SelectedPath = str;
      if (folderBrowserDialog.ShowDialog((IWin32Window) this) == DialogResult.OK)
        str = folderBrowserDialog.SelectedPath;
      if (this._myParent.Operation == Utility.FormOperation.AddFirst)
      {
        this._myParent.Settings.AccountObject.IsMaster = true;
        this.restoreMode = Utility.RestoreMode.Master;
      }
      else
      {
        this._myParent.Settings.AccountObject.IsMaster = false;
        this.restoreMode = Utility.RestoreMode.Client;
      }
      DataTable dt = this.PopulateMeterList(str, this.restoreMode);
      SelectMeterAccount selectMeterAccount = new SelectMeterAccount();
      selectMeterAccount.PopulateDataGrid(dt);
      if (this.restoreMode == Utility.RestoreMode.Master)
        selectMeterAccount.Text = languafier.Translate("d29036");
      else
        selectMeterAccount.Text = languafier.Translate("d29037");
      if (selectMeterAccount.ShowDialog((IWin32Window) this) == DialogResult.Cancel)
        return;
      this.meterNumber = selectMeterAccount.MeterNumber;
      this.accountNumber = selectMeterAccount.AccountNumber;
      this.description = selectMeterAccount.Description;
      this.bGroundEnabled = selectMeterAccount.GroundEnabled;
      this.bSmartPostEnabled = selectMeterAccount.SmartPostEnabled;
      if (MessageBox.Show("Will the customer's meter/account number remain the same?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        this.newMeterNumber = "";
        this.newAccountNumber = "";
      }
      else
      {
        SelectNewMeterAccount selectNewMeterAccount = new SelectNewMeterAccount();
        selectNewMeterAccount.OldMeterNumber = this.meterNumber;
        selectNewMeterAccount.OldAccountNumber = this.accountNumber;
        selectNewMeterAccount.UpdateLabels();
        if (selectNewMeterAccount.ShowDialog((IWin32Window) this) == DialogResult.Cancel)
          return;
        this.newMeterNumber = selectNewMeterAccount.NewMeterNumber;
        this.newAccountNumber = selectNewMeterAccount.NewAccountNumber;
      }
      if (!new RestoreMeterAccountClass().RestoreMeterAccount(out this.settings, this.meterNumber, this.accountNumber, str) || !new RestoreMeterAccountClass().RestoreMeterAccount(out this.settings, this.meterNumber, this.accountNumber, str))
        return;
      if (!this.newMeterNumber.Equals(""))
      {
        this.settings.AccountObject.AccountNumber = this.newAccountNumber;
        this.settings.AccountObject.MeterNumber = this.newMeterNumber;
        this.settings.TrackingNumberObject.FedExAcctNbr = this.newAccountNumber;
        this.settings.TrackingNumberObject.MeterNumber = this.newMeterNumber;
      }
      if (this.bGroundEnabled)
      {
        this.settings.AccountObject.IsGroundEverEnabled = true;
        if (this.settings.TrackingNumberObject == null)
          this.settings.TrackingNumberObject = new TrackingNumber();
        this.settings.TrackingNumberObject.FirstTrackingNumber = this.startGroundTrkNumber;
        this.settings.TrackingNumberObject.NextTrackingNumber = this.nextGroundTrkNumber;
        this.settings.TrackingNumberObject.LastTrackingNumber = this.endGroundTrkNumber;
        this.settings.TrackingNumberObject.CarrierType = Shipment.CarrierType.Ground;
        this.settings.TrackingNumberObject.CurrentCodes = TrackingNumber.TrackingNumberRangeStatus.Current;
      }
      if (this.bSmartPostEnabled)
      {
        this.settings.AccountObject.IsSmartPostEnabled = true;
        if (this.settings.TrackingNumberObject == null)
          this.settings.TrackingNumberObject = new TrackingNumber();
        this.settings.TrackingNumberObject.FirstTrackingNumber = this.startSmartPostTrkNumber;
        this.settings.TrackingNumberObject.NextTrackingNumber = this.nextSmartPostTrkNumber;
        this.settings.TrackingNumberObject.LastTrackingNumber = this.endSmartPostTrkNumber;
        this.settings.TrackingNumberObject.CarrierType = Shipment.CarrierType.SmartPost;
        this.settings.TrackingNumberObject.CurrentCodes = TrackingNumber.TrackingNumberRangeStatus.Current;
      }
      if (this.restoreMode == Utility.RestoreMode.Master)
        this._myParent.Operation = Utility.FormOperation.RestoreMaster;
      else if (this.restoreMode == Utility.RestoreMode.Client)
        this._myParent.Operation = Utility.FormOperation.RestoreClient;
      this._myParent.SystemNumber = this.settings.AccountObject.MeterNumber;
      this._myParent.Description = this.settings.AccountObject.Description;
      this._myParent.UserPage.ObjectsToScreen(this.settings, this._myParent.Operation);
      this._myParent.CustomerAdminPage.ObjectsToScreen(this.settings, this._myParent.Operation);
      this._myParent.LoggingPage.ObjectsToScreen(this.settings, this._myParent.Operation);
      this._myParent.ExpressAdminPage.ObjectsToScreen(this.settings, this._myParent.Operation);
      this._myParent.GroundAdminPage.ObjectsToScreen(this.settings, this._myParent.Operation);
      this._myParent.IntegrationPage.ObjectsToScreen(this.settings, this._myParent.Operation);
      this._myParent.GroundAdminPage.ObjectsToScreen(this.settings, this._myParent.Operation);
    }

    private DataTable PopulateMeterList(
      string sRestoreFolderName,
      Utility.RestoreMode curentRestoreMode)
    {
      string str = "";
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("MeterNumber");
      dataTable.Columns.Add("AccountNumber");
      dataTable.Columns.Add("Description");
      dataTable.Columns.Add("GroundEnabled");
      dataTable.Columns.Add("SmartPostEnabled");
      FedEx.Gsm.Common.IniFile.IniFile iniFile = new FedEx.Gsm.Common.IniFile.IniFile("meters.ini", sRestoreFolderName);
      string[] sectionNames1 = new string[256];
      int sectionNames2 = iniFile.GetSectionNames(out sectionNames1, 256);
      if (sectionNames2 == 0)
        return dataTable;
      for (int index = 0; index < sectionNames2; ++index)
      {
        iniFile.Section = sectionNames1[index];
        if ((iniFile.GetIntVal("Master", 0) != 1 ? Utility.RestoreMode.Client : Utility.RestoreMode.Master) == curentRestoreMode)
        {
          DataRow row = dataTable.NewRow();
          row["MeterNumber"] = (object) sectionNames1[index];
          str = "";
          string strVal1 = iniFile.GetStrVal("Account", "NONE");
          row["AccountNumber"] = (object) strVal1;
          str = "";
          string strVal2 = iniFile.GetStrVal("Description", "NONE");
          row["Description"] = (object) strVal2;
          row["GroundEnabled"] = iniFile.GetIntVal("GroundEnabled", 0) == 1 ? (object) "true" : (object) "false";
          row["SmartPostEnabled"] = iniFile.GetIntVal("SmartPostEnabled", 0) == 1 ? (object) "true" : (object) "false";
          dataTable.Rows.Add(row);
        }
      }
      return dataTable;
    }

    private void chkEnableOneRate_CheckedChanged(object sender, EventArgs e)
    {
      EventHandler<BoolEventArgs> rateEnabledChanged = this.OneRateEnabledChanged;
      if (rateEnabledChanged == null)
        return;
      rateEnabledChanged((object) this.chkEnableOneRate, new BoolEventArgs(this.chkEnableOneRate.Checked));
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SystemSettingsExpressAdmin));
      this.tableLayoutPanel4 = new TableLayoutPanel();
      this.gbxSerialNumbers = new ColorGroupBox();
      this.label17 = new Label();
      this.edtReportPrinterNum = new TextBox();
      this.label18 = new Label();
      this.edtValidatorNum = new TextBox();
      this.label19 = new Label();
      this.edtWaybillPrinterNum = new TextBox();
      this.label20 = new Label();
      this.edtPalPrinterNum = new TextBox();
      this.label14 = new Label();
      this.edtScaleNum = new TextBox();
      this.label15 = new Label();
      this.edtKeyboardNum = new TextBox();
      this.label13 = new Label();
      this.edtMonitorNum = new TextBox();
      this.label12 = new Label();
      this.edtBpuNum = new TextBox();
      this.label11 = new Label();
      this.edtEmployeeNum = new TextBox();
      this.colorGroupBox22 = new ColorGroupBox();
      this.chkMaskCustomsValue = new CheckBox();
      this.colorGroupBox21 = new ColorGroupBox();
      this.chkActivateUnlimitedFutureDay = new CheckBox();
      this.colorGroupBox20 = new ColorGroupBox();
      this.chkFedExProvidedLinehaul = new CheckBox();
      this.chkEnableTd = new CheckBox();
      this.colorGroupBox19 = new ColorGroupBox();
      this.chkNoUploadNoShip = new CheckBox();
      this.colorGroupBox18 = new ColorGroupBox();
      this.panelCommStuff = new PanelEx();
      this.label21 = new Label();
      this.label16 = new Label();
      this.chkOverrideDialtoneCheck = new CheckBox();
      this.spnReconcileWindow = new NumericUpDown();
      this.spnCloseHourlyUploadWindow = new NumericUpDown();
      this.label10 = new Label();
      this.label8 = new Label();
      this.btnSetCommunications = new Button();
      this.colorGroupBox17 = new ColorGroupBox();
      this.chkLabelTransfer = new CheckBox();
      this.chkAutoPassportListening = new CheckBox();
      this.chkBatch = new CheckBox();
      this.colorGroupBox16 = new ColorGroupBox();
      this.txtOriginId = new Label();
      this.label9 = new Label();
      this.txtStateProvince = new Label();
      this.label7 = new Label();
      this.label6 = new Label();
      this.label4 = new Label();
      this.label3 = new Label();
      this.label2 = new Label();
      this.label1 = new Label();
      this.edtReceiveDownloadTime = new FdxMaskedEdit();
      this.edtZipPostal = new FdxMaskedEdit();
      this.edtCity = new TextBox();
      this.edtAccountNumber = new MaskedTextBox();
      this.cboCountry = new ComboBoxEx();
      this.panel1 = new Panel();
      this.colorGroupBox1 = new ColorGroupBox();
      this.chkEnableOneRate = new CheckBox();
      this.chkDisableAutoTrack = new CheckBox();
      this.chkDisableShortcuts = new CheckBox();
      this.btnRestoreMeterAccount = new Button();
      this.focusExtender1 = new FocusExtender();
      this.tableLayoutPanel4.SuspendLayout();
      this.gbxSerialNumbers.SuspendLayout();
      this.colorGroupBox22.SuspendLayout();
      this.colorGroupBox21.SuspendLayout();
      this.colorGroupBox20.SuspendLayout();
      this.colorGroupBox19.SuspendLayout();
      this.colorGroupBox18.SuspendLayout();
      this.panelCommStuff.SuspendLayout();
      this.spnReconcileWindow.BeginInit();
      this.spnCloseHourlyUploadWindow.BeginInit();
      this.colorGroupBox17.SuspendLayout();
      this.colorGroupBox16.SuspendLayout();
      this.panel1.SuspendLayout();
      this.colorGroupBox1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel4, "tableLayoutPanel4");
      this.tableLayoutPanel4.Controls.Add((Control) this.gbxSerialNumbers, 0, 4);
      this.tableLayoutPanel4.Controls.Add((Control) this.colorGroupBox22, 2, 3);
      this.tableLayoutPanel4.Controls.Add((Control) this.colorGroupBox21, 1, 2);
      this.tableLayoutPanel4.Controls.Add((Control) this.colorGroupBox20, 0, 3);
      this.tableLayoutPanel4.Controls.Add((Control) this.colorGroupBox19, 0, 2);
      this.tableLayoutPanel4.Controls.Add((Control) this.colorGroupBox18, 3, 1);
      this.tableLayoutPanel4.Controls.Add((Control) this.colorGroupBox17, 3, 0);
      this.tableLayoutPanel4.Controls.Add((Control) this.colorGroupBox16, 0, 0);
      this.tableLayoutPanel4.Controls.Add((Control) this.panel1, 3, 4);
      this.tableLayoutPanel4.Name = "tableLayoutPanel4";
      this.gbxSerialNumbers.BorderThickness = 1f;
      this.tableLayoutPanel4.SetColumnSpan((Control) this.gbxSerialNumbers, 3);
      this.gbxSerialNumbers.Controls.Add((Control) this.label17);
      this.gbxSerialNumbers.Controls.Add((Control) this.edtReportPrinterNum);
      this.gbxSerialNumbers.Controls.Add((Control) this.label18);
      this.gbxSerialNumbers.Controls.Add((Control) this.edtValidatorNum);
      this.gbxSerialNumbers.Controls.Add((Control) this.label19);
      this.gbxSerialNumbers.Controls.Add((Control) this.edtWaybillPrinterNum);
      this.gbxSerialNumbers.Controls.Add((Control) this.label20);
      this.gbxSerialNumbers.Controls.Add((Control) this.edtPalPrinterNum);
      this.gbxSerialNumbers.Controls.Add((Control) this.label14);
      this.gbxSerialNumbers.Controls.Add((Control) this.edtScaleNum);
      this.gbxSerialNumbers.Controls.Add((Control) this.label15);
      this.gbxSerialNumbers.Controls.Add((Control) this.edtKeyboardNum);
      this.gbxSerialNumbers.Controls.Add((Control) this.label13);
      this.gbxSerialNumbers.Controls.Add((Control) this.edtMonitorNum);
      this.gbxSerialNumbers.Controls.Add((Control) this.label12);
      this.gbxSerialNumbers.Controls.Add((Control) this.edtBpuNum);
      this.gbxSerialNumbers.Controls.Add((Control) this.label11);
      this.gbxSerialNumbers.Controls.Add((Control) this.edtEmployeeNum);
      componentResourceManager.ApplyResources((object) this.gbxSerialNumbers, "gbxSerialNumbers");
      this.gbxSerialNumbers.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxSerialNumbers.Name = "gbxSerialNumbers";
      this.gbxSerialNumbers.RoundCorners = 5;
      this.gbxSerialNumbers.TabStop = false;
      componentResourceManager.ApplyResources((object) this.label17, "label17");
      this.label17.Name = "label17";
      this.focusExtender1.SetFocusBackColor((Control) this.edtReportPrinterNum, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtReportPrinterNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtReportPrinterNum, componentResourceManager.GetString("edtReportPrinterNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtReportPrinterNum, (HelpNavigator) componentResourceManager.GetObject("edtReportPrinterNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtReportPrinterNum, "edtReportPrinterNum");
      this.edtReportPrinterNum.Name = "edtReportPrinterNum";
      this.helpProvider1.SetShowHelp((Control) this.edtReportPrinterNum, (bool) componentResourceManager.GetObject("edtReportPrinterNum.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label18, "label18");
      this.label18.Name = "label18";
      this.focusExtender1.SetFocusBackColor((Control) this.edtValidatorNum, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtValidatorNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtValidatorNum, componentResourceManager.GetString("edtValidatorNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtValidatorNum, (HelpNavigator) componentResourceManager.GetObject("edtValidatorNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtValidatorNum, "edtValidatorNum");
      this.edtValidatorNum.Name = "edtValidatorNum";
      this.helpProvider1.SetShowHelp((Control) this.edtValidatorNum, (bool) componentResourceManager.GetObject("edtValidatorNum.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label19, "label19");
      this.label19.Name = "label19";
      this.focusExtender1.SetFocusBackColor((Control) this.edtWaybillPrinterNum, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtWaybillPrinterNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtWaybillPrinterNum, componentResourceManager.GetString("edtWaybillPrinterNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtWaybillPrinterNum, (HelpNavigator) componentResourceManager.GetObject("edtWaybillPrinterNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtWaybillPrinterNum, "edtWaybillPrinterNum");
      this.edtWaybillPrinterNum.Name = "edtWaybillPrinterNum";
      this.helpProvider1.SetShowHelp((Control) this.edtWaybillPrinterNum, (bool) componentResourceManager.GetObject("edtWaybillPrinterNum.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label20, "label20");
      this.label20.Name = "label20";
      this.focusExtender1.SetFocusBackColor((Control) this.edtPalPrinterNum, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtPalPrinterNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtPalPrinterNum, componentResourceManager.GetString("edtPalPrinterNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtPalPrinterNum, (HelpNavigator) componentResourceManager.GetObject("edtPalPrinterNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtPalPrinterNum, "edtPalPrinterNum");
      this.edtPalPrinterNum.Name = "edtPalPrinterNum";
      this.helpProvider1.SetShowHelp((Control) this.edtPalPrinterNum, (bool) componentResourceManager.GetObject("edtPalPrinterNum.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label14, "label14");
      this.label14.Name = "label14";
      this.focusExtender1.SetFocusBackColor((Control) this.edtScaleNum, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtScaleNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtScaleNum, componentResourceManager.GetString("edtScaleNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtScaleNum, (HelpNavigator) componentResourceManager.GetObject("edtScaleNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtScaleNum, "edtScaleNum");
      this.edtScaleNum.Name = "edtScaleNum";
      this.helpProvider1.SetShowHelp((Control) this.edtScaleNum, (bool) componentResourceManager.GetObject("edtScaleNum.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label15, "label15");
      this.label15.Name = "label15";
      this.focusExtender1.SetFocusBackColor((Control) this.edtKeyboardNum, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtKeyboardNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtKeyboardNum, componentResourceManager.GetString("edtKeyboardNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtKeyboardNum, (HelpNavigator) componentResourceManager.GetObject("edtKeyboardNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtKeyboardNum, "edtKeyboardNum");
      this.edtKeyboardNum.Name = "edtKeyboardNum";
      this.helpProvider1.SetShowHelp((Control) this.edtKeyboardNum, (bool) componentResourceManager.GetObject("edtKeyboardNum.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label13, "label13");
      this.label13.Name = "label13";
      this.focusExtender1.SetFocusBackColor((Control) this.edtMonitorNum, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtMonitorNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtMonitorNum, componentResourceManager.GetString("edtMonitorNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtMonitorNum, (HelpNavigator) componentResourceManager.GetObject("edtMonitorNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtMonitorNum, "edtMonitorNum");
      this.edtMonitorNum.Name = "edtMonitorNum";
      this.helpProvider1.SetShowHelp((Control) this.edtMonitorNum, (bool) componentResourceManager.GetObject("edtMonitorNum.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label12, "label12");
      this.label12.Name = "label12";
      this.focusExtender1.SetFocusBackColor((Control) this.edtBpuNum, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtBpuNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtBpuNum, componentResourceManager.GetString("edtBpuNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtBpuNum, (HelpNavigator) componentResourceManager.GetObject("edtBpuNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtBpuNum, "edtBpuNum");
      this.edtBpuNum.Name = "edtBpuNum";
      this.helpProvider1.SetShowHelp((Control) this.edtBpuNum, (bool) componentResourceManager.GetObject("edtBpuNum.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label11, "label11");
      this.label11.Name = "label11";
      this.focusExtender1.SetFocusBackColor((Control) this.edtEmployeeNum, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtEmployeeNum, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtEmployeeNum, componentResourceManager.GetString("edtEmployeeNum.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtEmployeeNum, (HelpNavigator) componentResourceManager.GetObject("edtEmployeeNum.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtEmployeeNum, "edtEmployeeNum");
      this.edtEmployeeNum.Name = "edtEmployeeNum";
      this.helpProvider1.SetShowHelp((Control) this.edtEmployeeNum, (bool) componentResourceManager.GetObject("edtEmployeeNum.ShowHelp"));
      this.colorGroupBox22.BorderThickness = 1f;
      this.tableLayoutPanel4.SetColumnSpan((Control) this.colorGroupBox22, 2);
      this.colorGroupBox22.Controls.Add((Control) this.chkMaskCustomsValue);
      componentResourceManager.ApplyResources((object) this.colorGroupBox22, "colorGroupBox22");
      this.colorGroupBox22.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox22.Name = "colorGroupBox22";
      this.colorGroupBox22.RoundCorners = 5;
      this.colorGroupBox22.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkMaskCustomsValue, componentResourceManager.GetString("chkMaskCustomsValue.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkMaskCustomsValue, (HelpNavigator) componentResourceManager.GetObject("chkMaskCustomsValue.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkMaskCustomsValue, "chkMaskCustomsValue");
      this.chkMaskCustomsValue.Name = "chkMaskCustomsValue";
      this.helpProvider1.SetShowHelp((Control) this.chkMaskCustomsValue, (bool) componentResourceManager.GetObject("chkMaskCustomsValue.ShowHelp"));
      this.chkMaskCustomsValue.UseVisualStyleBackColor = true;
      this.colorGroupBox21.BorderThickness = 1f;
      this.tableLayoutPanel4.SetColumnSpan((Control) this.colorGroupBox21, 2);
      this.colorGroupBox21.Controls.Add((Control) this.chkActivateUnlimitedFutureDay);
      componentResourceManager.ApplyResources((object) this.colorGroupBox21, "colorGroupBox21");
      this.colorGroupBox21.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox21.Name = "colorGroupBox21";
      this.colorGroupBox21.RoundCorners = 5;
      this.colorGroupBox21.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkActivateUnlimitedFutureDay, componentResourceManager.GetString("chkActivateUnlimitedFutureDay.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkActivateUnlimitedFutureDay, (HelpNavigator) componentResourceManager.GetObject("chkActivateUnlimitedFutureDay.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkActivateUnlimitedFutureDay, "chkActivateUnlimitedFutureDay");
      this.chkActivateUnlimitedFutureDay.Name = "chkActivateUnlimitedFutureDay";
      this.helpProvider1.SetShowHelp((Control) this.chkActivateUnlimitedFutureDay, (bool) componentResourceManager.GetObject("chkActivateUnlimitedFutureDay.ShowHelp"));
      this.chkActivateUnlimitedFutureDay.UseVisualStyleBackColor = true;
      this.colorGroupBox20.BorderThickness = 1f;
      this.tableLayoutPanel4.SetColumnSpan((Control) this.colorGroupBox20, 2);
      this.colorGroupBox20.Controls.Add((Control) this.chkFedExProvidedLinehaul);
      this.colorGroupBox20.Controls.Add((Control) this.chkEnableTd);
      componentResourceManager.ApplyResources((object) this.colorGroupBox20, "colorGroupBox20");
      this.colorGroupBox20.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox20.Name = "colorGroupBox20";
      this.colorGroupBox20.RoundCorners = 5;
      this.colorGroupBox20.TabStop = false;
      componentResourceManager.ApplyResources((object) this.chkFedExProvidedLinehaul, "chkFedExProvidedLinehaul");
      this.helpProvider1.SetHelpKeyword((Control) this.chkFedExProvidedLinehaul, componentResourceManager.GetString("chkFedExProvidedLinehaul.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkFedExProvidedLinehaul, (HelpNavigator) componentResourceManager.GetObject("chkFedExProvidedLinehaul.HelpNavigator"));
      this.chkFedExProvidedLinehaul.Name = "chkFedExProvidedLinehaul";
      this.helpProvider1.SetShowHelp((Control) this.chkFedExProvidedLinehaul, (bool) componentResourceManager.GetObject("chkFedExProvidedLinehaul.ShowHelp"));
      this.chkFedExProvidedLinehaul.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkEnableTd, componentResourceManager.GetString("chkEnableTd.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkEnableTd, (HelpNavigator) componentResourceManager.GetObject("chkEnableTd.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkEnableTd, "chkEnableTd");
      this.chkEnableTd.Name = "chkEnableTd";
      this.helpProvider1.SetShowHelp((Control) this.chkEnableTd, (bool) componentResourceManager.GetObject("chkEnableTd.ShowHelp"));
      this.chkEnableTd.UseVisualStyleBackColor = true;
      this.colorGroupBox19.BorderThickness = 1f;
      this.colorGroupBox19.Controls.Add((Control) this.chkNoUploadNoShip);
      componentResourceManager.ApplyResources((object) this.colorGroupBox19, "colorGroupBox19");
      this.colorGroupBox19.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox19.Name = "colorGroupBox19";
      this.colorGroupBox19.RoundCorners = 5;
      this.colorGroupBox19.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkNoUploadNoShip, componentResourceManager.GetString("chkNoUploadNoShip.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkNoUploadNoShip, (HelpNavigator) componentResourceManager.GetObject("chkNoUploadNoShip.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkNoUploadNoShip, "chkNoUploadNoShip");
      this.chkNoUploadNoShip.Name = "chkNoUploadNoShip";
      this.helpProvider1.SetShowHelp((Control) this.chkNoUploadNoShip, (bool) componentResourceManager.GetObject("chkNoUploadNoShip.ShowHelp"));
      this.chkNoUploadNoShip.UseVisualStyleBackColor = true;
      this.colorGroupBox18.BorderThickness = 1f;
      this.colorGroupBox18.Controls.Add((Control) this.panelCommStuff);
      this.colorGroupBox18.Controls.Add((Control) this.btnSetCommunications);
      componentResourceManager.ApplyResources((object) this.colorGroupBox18, "colorGroupBox18");
      this.colorGroupBox18.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox18.Name = "colorGroupBox18";
      this.colorGroupBox18.RoundCorners = 5;
      this.tableLayoutPanel4.SetRowSpan((Control) this.colorGroupBox18, 2);
      this.colorGroupBox18.TabStop = false;
      this.panelCommStuff.Controls.Add((Control) this.label21);
      this.panelCommStuff.Controls.Add((Control) this.label16);
      this.panelCommStuff.Controls.Add((Control) this.chkOverrideDialtoneCheck);
      this.panelCommStuff.Controls.Add((Control) this.spnReconcileWindow);
      this.panelCommStuff.Controls.Add((Control) this.spnCloseHourlyUploadWindow);
      this.panelCommStuff.Controls.Add((Control) this.label10);
      this.panelCommStuff.Controls.Add((Control) this.label8);
      this.panelCommStuff.IsVisible = true;
      componentResourceManager.ApplyResources((object) this.panelCommStuff, "panelCommStuff");
      this.panelCommStuff.Name = "panelCommStuff";
      componentResourceManager.ApplyResources((object) this.label21, "label21");
      this.label21.Name = "label21";
      componentResourceManager.ApplyResources((object) this.label16, "label16");
      this.label16.Name = "label16";
      this.helpProvider1.SetHelpKeyword((Control) this.chkOverrideDialtoneCheck, componentResourceManager.GetString("chkOverrideDialtoneCheck.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkOverrideDialtoneCheck, (HelpNavigator) componentResourceManager.GetObject("chkOverrideDialtoneCheck.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkOverrideDialtoneCheck, "chkOverrideDialtoneCheck");
      this.chkOverrideDialtoneCheck.Name = "chkOverrideDialtoneCheck";
      this.helpProvider1.SetShowHelp((Control) this.chkOverrideDialtoneCheck, (bool) componentResourceManager.GetObject("chkOverrideDialtoneCheck.ShowHelp"));
      this.chkOverrideDialtoneCheck.UseVisualStyleBackColor = true;
      this.focusExtender1.SetFocusBackColor((Control) this.spnReconcileWindow, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.spnReconcileWindow, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.spnReconcileWindow, componentResourceManager.GetString("spnReconcileWindow.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnReconcileWindow, (HelpNavigator) componentResourceManager.GetObject("spnReconcileWindow.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.spnReconcileWindow, "spnReconcileWindow");
      this.spnReconcileWindow.Maximum = new Decimal(new int[4]
      {
        72,
        0,
        0,
        0
      });
      this.spnReconcileWindow.Minimum = new Decimal(new int[4]
      {
        24,
        0,
        0,
        0
      });
      this.spnReconcileWindow.Name = "spnReconcileWindow";
      this.helpProvider1.SetShowHelp((Control) this.spnReconcileWindow, (bool) componentResourceManager.GetObject("spnReconcileWindow.ShowHelp"));
      this.spnReconcileWindow.Value = new Decimal(new int[4]
      {
        24,
        0,
        0,
        0
      });
      this.focusExtender1.SetFocusBackColor((Control) this.spnCloseHourlyUploadWindow, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.spnCloseHourlyUploadWindow, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.spnCloseHourlyUploadWindow, componentResourceManager.GetString("spnCloseHourlyUploadWindow.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnCloseHourlyUploadWindow, (HelpNavigator) componentResourceManager.GetObject("spnCloseHourlyUploadWindow.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.spnCloseHourlyUploadWindow, "spnCloseHourlyUploadWindow");
      this.spnCloseHourlyUploadWindow.Maximum = new Decimal(new int[4]
      {
        72,
        0,
        0,
        0
      });
      this.spnCloseHourlyUploadWindow.Minimum = new Decimal(new int[4]
      {
        24,
        0,
        0,
        0
      });
      this.spnCloseHourlyUploadWindow.Name = "spnCloseHourlyUploadWindow";
      this.helpProvider1.SetShowHelp((Control) this.spnCloseHourlyUploadWindow, (bool) componentResourceManager.GetObject("spnCloseHourlyUploadWindow.ShowHelp"));
      this.spnCloseHourlyUploadWindow.Value = new Decimal(new int[4]
      {
        24,
        0,
        0,
        0
      });
      componentResourceManager.ApplyResources((object) this.label10, "label10");
      this.label10.Name = "label10";
      componentResourceManager.ApplyResources((object) this.label8, "label8");
      this.label8.Name = "label8";
      this.helpProvider1.SetHelpKeyword((Control) this.btnSetCommunications, componentResourceManager.GetString("btnSetCommunications.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnSetCommunications, (HelpNavigator) componentResourceManager.GetObject("btnSetCommunications.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.btnSetCommunications, "btnSetCommunications");
      this.btnSetCommunications.Name = "btnSetCommunications";
      this.helpProvider1.SetShowHelp((Control) this.btnSetCommunications, (bool) componentResourceManager.GetObject("btnSetCommunications.ShowHelp"));
      this.btnSetCommunications.UseVisualStyleBackColor = true;
      this.btnSetCommunications.Click += new EventHandler(this.btnSetCommunications_Click);
      this.colorGroupBox17.BorderThickness = 1f;
      this.colorGroupBox17.Controls.Add((Control) this.chkLabelTransfer);
      this.colorGroupBox17.Controls.Add((Control) this.chkAutoPassportListening);
      this.colorGroupBox17.Controls.Add((Control) this.chkBatch);
      componentResourceManager.ApplyResources((object) this.colorGroupBox17, "colorGroupBox17");
      this.colorGroupBox17.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox17.Name = "colorGroupBox17";
      this.colorGroupBox17.RoundCorners = 5;
      this.colorGroupBox17.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkLabelTransfer, componentResourceManager.GetString("chkLabelTransfer.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkLabelTransfer, (HelpNavigator) componentResourceManager.GetObject("chkLabelTransfer.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkLabelTransfer, "chkLabelTransfer");
      this.chkLabelTransfer.Name = "chkLabelTransfer";
      this.helpProvider1.SetShowHelp((Control) this.chkLabelTransfer, (bool) componentResourceManager.GetObject("chkLabelTransfer.ShowHelp"));
      this.chkLabelTransfer.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkAutoPassportListening, componentResourceManager.GetString("chkAutoPassportListening.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAutoPassportListening, (HelpNavigator) componentResourceManager.GetObject("chkAutoPassportListening.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAutoPassportListening, "chkAutoPassportListening");
      this.chkAutoPassportListening.Name = "chkAutoPassportListening";
      this.helpProvider1.SetShowHelp((Control) this.chkAutoPassportListening, (bool) componentResourceManager.GetObject("chkAutoPassportListening.ShowHelp"));
      this.chkAutoPassportListening.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkBatch, componentResourceManager.GetString("chkBatch.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkBatch, (HelpNavigator) componentResourceManager.GetObject("chkBatch.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkBatch, "chkBatch");
      this.chkBatch.Name = "chkBatch";
      this.helpProvider1.SetShowHelp((Control) this.chkBatch, (bool) componentResourceManager.GetObject("chkBatch.ShowHelp"));
      this.chkBatch.UseVisualStyleBackColor = true;
      this.colorGroupBox16.BorderThickness = 1f;
      this.tableLayoutPanel4.SetColumnSpan((Control) this.colorGroupBox16, 3);
      this.colorGroupBox16.Controls.Add((Control) this.txtOriginId);
      this.colorGroupBox16.Controls.Add((Control) this.label9);
      this.colorGroupBox16.Controls.Add((Control) this.txtStateProvince);
      this.colorGroupBox16.Controls.Add((Control) this.label7);
      this.colorGroupBox16.Controls.Add((Control) this.label6);
      this.colorGroupBox16.Controls.Add((Control) this.label4);
      this.colorGroupBox16.Controls.Add((Control) this.label3);
      this.colorGroupBox16.Controls.Add((Control) this.label2);
      this.colorGroupBox16.Controls.Add((Control) this.label1);
      this.colorGroupBox16.Controls.Add((Control) this.edtReceiveDownloadTime);
      this.colorGroupBox16.Controls.Add((Control) this.edtZipPostal);
      this.colorGroupBox16.Controls.Add((Control) this.edtCity);
      this.colorGroupBox16.Controls.Add((Control) this.edtAccountNumber);
      this.colorGroupBox16.Controls.Add((Control) this.cboCountry);
      componentResourceManager.ApplyResources((object) this.colorGroupBox16, "colorGroupBox16");
      this.colorGroupBox16.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox16.Name = "colorGroupBox16";
      this.colorGroupBox16.RoundCorners = 5;
      this.tableLayoutPanel4.SetRowSpan((Control) this.colorGroupBox16, 2);
      this.colorGroupBox16.TabStop = false;
      componentResourceManager.ApplyResources((object) this.txtOriginId, "txtOriginId");
      this.txtOriginId.Name = "txtOriginId";
      componentResourceManager.ApplyResources((object) this.label9, "label9");
      this.label9.Name = "label9";
      componentResourceManager.ApplyResources((object) this.txtStateProvince, "txtStateProvince");
      this.txtStateProvince.Name = "txtStateProvince";
      componentResourceManager.ApplyResources((object) this.label7, "label7");
      this.label7.Name = "label7";
      componentResourceManager.ApplyResources((object) this.label6, "label6");
      this.label6.Name = "label6";
      componentResourceManager.ApplyResources((object) this.label4, "label4");
      this.label4.Name = "label4";
      componentResourceManager.ApplyResources((object) this.label3, "label3");
      this.label3.Name = "label3";
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.edtReceiveDownloadTime.Allow = "";
      this.edtReceiveDownloadTime.Disallow = "";
      this.edtReceiveDownloadTime.eMask = eMasks.maskCustom;
      this.edtReceiveDownloadTime.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtReceiveDownloadTime, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtReceiveDownloadTime, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtReceiveDownloadTime, componentResourceManager.GetString("edtReceiveDownloadTime.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtReceiveDownloadTime, (HelpNavigator) componentResourceManager.GetObject("edtReceiveDownloadTime.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtReceiveDownloadTime, "edtReceiveDownloadTime");
      this.edtReceiveDownloadTime.Mask = "#0:00 >BB";
      this.edtReceiveDownloadTime.Name = "edtReceiveDownloadTime";
      this.helpProvider1.SetShowHelp((Control) this.edtReceiveDownloadTime, (bool) componentResourceManager.GetObject("edtReceiveDownloadTime.ShowHelp"));
      this.edtZipPostal.Allow = "";
      this.edtZipPostal.Disallow = "";
      this.edtZipPostal.eMask = eMasks.maskZipCodeNine;
      this.edtZipPostal.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtZipPostal, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtZipPostal, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtZipPostal, componentResourceManager.GetString("edtZipPostal.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtZipPostal, (HelpNavigator) componentResourceManager.GetObject("edtZipPostal.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtZipPostal, "edtZipPostal");
      this.edtZipPostal.Mask = "99999-9999";
      this.edtZipPostal.Name = "edtZipPostal";
      this.helpProvider1.SetShowHelp((Control) this.edtZipPostal, (bool) componentResourceManager.GetObject("edtZipPostal.ShowHelp"));
      this.edtZipPostal.Leave += new EventHandler(this.edtZipPostal_Leave);
      this.focusExtender1.SetFocusBackColor((Control) this.edtCity, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtCity, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtCity, componentResourceManager.GetString("edtCity.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtCity, (HelpNavigator) componentResourceManager.GetObject("edtCity.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtCity, "edtCity");
      this.edtCity.Name = "edtCity";
      this.helpProvider1.SetShowHelp((Control) this.edtCity, (bool) componentResourceManager.GetObject("edtCity.ShowHelp"));
      this.edtAccountNumber.AllowPromptAsInput = false;
      this.focusExtender1.SetFocusBackColor((Control) this.edtAccountNumber, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtAccountNumber, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtAccountNumber, componentResourceManager.GetString("edtAccountNumber.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtAccountNumber, (HelpNavigator) componentResourceManager.GetObject("edtAccountNumber.HelpNavigator"));
      this.edtAccountNumber.HidePromptOnLeave = true;
      componentResourceManager.ApplyResources((object) this.edtAccountNumber, "edtAccountNumber");
      this.edtAccountNumber.Name = "edtAccountNumber";
      this.helpProvider1.SetShowHelp((Control) this.edtAccountNumber, (bool) componentResourceManager.GetObject("edtAccountNumber.ShowHelp"));
      this.cboCountry.DisplayMember = "CountryCode";
      this.cboCountry.DisplayMemberQ = "CountryCode";
      this.cboCountry.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboCountry.DropDownWidth = 220;
      this.cboCountry.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboCountry, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboCountry, SystemColors.WindowText);
      this.cboCountry.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboCountry, componentResourceManager.GetString("cboCountry.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboCountry, (HelpNavigator) componentResourceManager.GetObject("cboCountry.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.cboCountry, "cboCountry");
      this.cboCountry.Name = "cboCountry";
      this.cboCountry.SelectedIndexQ = -1;
      this.cboCountry.SelectedItemQ = (object) null;
      this.cboCountry.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboCountry, (bool) componentResourceManager.GetObject("cboCountry.ShowHelp"));
      this.cboCountry.ValueMember = "CountryCode";
      this.cboCountry.ValueMemberQ = "CountryCode";
      this.cboCountry.SelectedValueChanged += new EventHandler(this.cboCountry_SelectedValueChanged);
      this.cboCountry.KeyDown += new KeyEventHandler(this.cboCountry_KeyDown);
      this.cboCountry.KeyPress += new KeyPressEventHandler(this.cboCountry_KeyPress);
      this.cboCountry.Leave += new EventHandler(this.cboCountry_Leave);
      this.panel1.Controls.Add((Control) this.colorGroupBox1);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      this.colorGroupBox1.BorderThickness = 1f;
      this.colorGroupBox1.Controls.Add((Control) this.chkEnableOneRate);
      this.colorGroupBox1.Controls.Add((Control) this.chkDisableAutoTrack);
      this.colorGroupBox1.Controls.Add((Control) this.chkDisableShortcuts);
      this.colorGroupBox1.Controls.Add((Control) this.btnRestoreMeterAccount);
      componentResourceManager.ApplyResources((object) this.colorGroupBox1, "colorGroupBox1");
      this.colorGroupBox1.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox1.Name = "colorGroupBox1";
      this.colorGroupBox1.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox1, (bool) componentResourceManager.GetObject("colorGroupBox1.ShowHelp"));
      this.colorGroupBox1.TabStop = false;
      componentResourceManager.ApplyResources((object) this.chkEnableOneRate, "chkEnableOneRate");
      this.chkEnableOneRate.Name = "chkEnableOneRate";
      this.chkEnableOneRate.UseVisualStyleBackColor = true;
      this.chkEnableOneRate.CheckedChanged += new EventHandler(this.chkEnableOneRate_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.chkDisableAutoTrack, "chkDisableAutoTrack");
      this.chkDisableAutoTrack.Name = "chkDisableAutoTrack";
      this.chkDisableAutoTrack.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.chkDisableShortcuts, "chkDisableShortcuts");
      this.helpProvider1.SetHelpKeyword((Control) this.chkDisableShortcuts, componentResourceManager.GetString("chkDisableShortcuts.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkDisableShortcuts, (HelpNavigator) componentResourceManager.GetObject("chkDisableShortcuts.HelpNavigator"));
      this.chkDisableShortcuts.Name = "chkDisableShortcuts";
      this.helpProvider1.SetShowHelp((Control) this.chkDisableShortcuts, (bool) componentResourceManager.GetObject("chkDisableShortcuts.ShowHelp"));
      this.chkDisableShortcuts.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.btnRestoreMeterAccount, componentResourceManager.GetString("btnRestoreMeterAccount.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnRestoreMeterAccount, (HelpNavigator) componentResourceManager.GetObject("btnRestoreMeterAccount.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.btnRestoreMeterAccount, "btnRestoreMeterAccount");
      this.btnRestoreMeterAccount.Name = "btnRestoreMeterAccount";
      this.helpProvider1.SetShowHelp((Control) this.btnRestoreMeterAccount, (bool) componentResourceManager.GetObject("btnRestoreMeterAccount.ShowHelp"));
      this.btnRestoreMeterAccount.UseVisualStyleBackColor = true;
      this.btnRestoreMeterAccount.Click += new EventHandler(this.btnRestoreMeterAccount_Click);
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanel4);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (SystemSettingsExpressAdmin);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.SystemSettingsExpressAdmin_Load);
      this.tableLayoutPanel4.ResumeLayout(false);
      this.gbxSerialNumbers.ResumeLayout(false);
      this.gbxSerialNumbers.PerformLayout();
      this.colorGroupBox22.ResumeLayout(false);
      this.colorGroupBox21.ResumeLayout(false);
      this.colorGroupBox20.ResumeLayout(false);
      this.colorGroupBox19.ResumeLayout(false);
      this.colorGroupBox18.ResumeLayout(false);
      this.panelCommStuff.ResumeLayout(false);
      this.spnReconcileWindow.EndInit();
      this.spnCloseHourlyUploadWindow.EndInit();
      this.colorGroupBox17.ResumeLayout(false);
      this.colorGroupBox16.ResumeLayout(false);
      this.colorGroupBox16.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.colorGroupBox1.ResumeLayout(false);
      this.colorGroupBox1.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
