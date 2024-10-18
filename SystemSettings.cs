// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SystemSettings
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.SmartPostConfiguration;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Cafe.ApplicationEngine.Integration;
using FedEx.Gsm.Common.ConfigManager;
using FedEx.Gsm.Common.Integration.SharedEnumsStructs;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.Entities;
using FedEx.Gsm.ShipEngine.Reports.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class SystemSettings : HelpFormBase
  {
    private SystemSettingsObject _settings;
    private Utility.FormOperation _operation;
    private bool _sendTransaction;
    private bool _accountCommitted;
    private bool _loggedInAlready;
    private EventBroker _systemSettingsEventBroker;
    private IContainer components;
    private TabControlEx tabControlSystemSettings;
    private TabPage tabPageUser;
    private TabPage tabPageCustomerAdmin;
    private Label label1;
    private FdxMaskedEdit edtSystem;
    private Label label2;
    private TextBox edtDescription;
    private TabPage tabPageLogging;
    private TabPage tabPageFedExExpressAdmin;
    private TabPage tabPageFedExGroundAdmin;
    private TabPage tabPageIntegration;
    private Button btnOk;
    private Button btnCancel;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem setAllToolStripMenuItem;
    private ToolStripMenuItem criticalToolStripMenuItem;
    private ToolStripMenuItem errorToolStripMenuItem;
    private ToolStripMenuItem warningToolStripMenuItem;
    private ToolStripMenuItem infoToolStripMenuItem;
    private ToolStripMenuItem verboseToolStripMenuItem;
    private ToolStripMenuItem debugToolStripMenuItem;
    private SystemSettingsUser _systemSettingsUser;
    private SystemSettingsCustomerAdmin _systemSettingsCustomerAdmin;
    private SystemSettingsExpressAdmin _systemSettingsExpressAdmin;
    private SystemSettingsLogging _systemSettingsLogging;
    private SystemSettingsGroundAdmin _systemSettingsGroundAdmin;
    private SystemSettingsIntegration _systemSettingsIntegration;
    private FocusExtender focusExtender1;

    public event TopicDelegate SenderListChanged;

    public event EventHandler PrinterSettingsChanged;

    public event EventHandler<ShutdownRequestEventArgs> RequestShutdown;

    public event EventHandler<SmartPostSettingsChangedEventArgs> SmartPostSettingsChanged;

    public EventBroker SystemSettingsEventBroker
    {
      get => this._systemSettingsEventBroker;
      set => this._systemSettingsEventBroker = value;
    }

    internal SystemSettingsObject Settings
    {
      get => this._settings;
      set => this._settings = value;
    }

    internal Utility.FormOperation Operation
    {
      get => this._operation;
      set => this._operation = value;
    }

    internal string MeterNumber
    {
      get => this.edtSystem.Raw;
      set => this.edtSystem.Text = value;
    }

    public TabControl SystemSettingsTabControl => (TabControl) this.tabControlSystemSettings;

    public SystemSettingsUser UserPage => this._systemSettingsUser;

    public SystemSettingsCustomerAdmin CustomerAdminPage => this._systemSettingsCustomerAdmin;

    public SystemSettingsLogging LoggingPage => this._systemSettingsLogging;

    public SystemSettingsExpressAdmin ExpressAdminPage => this._systemSettingsExpressAdmin;

    public SystemSettingsGroundAdmin GroundAdminPage => this._systemSettingsGroundAdmin;

    public SystemSettingsIntegration IntegrationPage => this._systemSettingsIntegration;

    public bool SendTransaction
    {
      get => this._sendTransaction;
      set => this._sendTransaction = value;
    }

    public string SystemNumber
    {
      get => this.edtSystem.Raw;
      set => this.edtSystem.Text = value;
    }

    public string Description
    {
      get => this.edtDescription.Text;
      set => this.edtDescription.Text = value;
    }

    public SystemSettings(Account account, Utility.FormOperation eOp)
    {
      this.InitializeComponent();
      this._operation = eOp;
      this.tabControlSystemSettings.MnemonicEnabled = true;
      this._systemSettingsUser.MyParent = this;
      this._systemSettingsExpressAdmin.MyParent = this;
      this._systemSettingsGroundAdmin.MyParent = this;
      this._systemSettingsCustomerAdmin.MyParent = this;
      this._systemSettingsIntegration.MyParent = this;
      this.InitializeObjects(account);
    }

    private void SystemSettings_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      Utility.HouseKeeping(this.Controls, Color.White);
      this.SetupEvents();
      this.ObjectsToScreen();
      this.tabControlSystemSettings.MnemonicEnabled = true;
    }

    private void SetupEvents()
    {
      GuiData.EventBroker.GetTopic(EventBroker.Events.SenderListChanged)?.AddPublisher((object) this, "SenderListChanged");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.ShutdownRequest, (object) this, "RequestShutdown");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.SmartPostSettingsChanged, (object) this, "SmartPostSettingsChanged");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.PrinterSettingsChanged, (object) this, "PrinterSettingsChanged");
      this._systemSettingsEventBroker = new EventBroker();
      this._systemSettingsUser.SetupEvents();
      this._systemSettingsExpressAdmin.SetupEvents();
      this._systemSettingsCustomerAdmin.SetupEvents();
      this._systemSettingsGroundAdmin.SetupEvents();
      this._systemSettingsIntegration.SetupEvents();
    }

    private void InitializeRegistrySettings(SystemSettingsObject settings)
    {
      string path = "SHIPNET2000/GUI/SETTINGS";
      settings.RegistrySettings["FieldColor"] = new RegistrySetting(path, (object) null, (object) Color.Blue.ToArgb().ToString(), true);
      settings.RegistrySettings["AutoTab"] = new RegistrySetting(path, (object) null, (object) "N", true);
      settings.RegistrySettings["FocusOnWeight"] = new RegistrySetting(path, (object) null, (object) "N", true);
      settings.RegistrySettings["NoGUIRating"] = new RegistrySetting(path, (object) null, (object) "N", true);
      settings.RegistrySettings["MWEODOptimization"] = new RegistrySetting(path, (object) null, (object) "N", true);
      settings.RegistrySettings["PreReadScale"] = new RegistrySetting(path, (object) null, (object) "N", true);
      settings.RegistrySettings["DisableSpecSvcOnShipDetails"] = new RegistrySetting(path, (object) null, (object) "N", true);
      settings.RegistrySettings["DisableAutoTrack"] = new RegistrySetting(path, (object) null, (object) "N", true);
      settings.RegistrySettings["NOTPRINTEXPRESSCOURIERREP"] = new RegistrySetting(path, (object) null, (object) "True", true);
      settings.RegistrySettings["Language"] = new RegistrySetting(path, (object) null, (object) "en-US", false);
      settings.RegistrySettings["IR_Attached"] = new RegistrySetting(path, (object) null, (object) "N", false);
      settings.RegistrySettings["IR_COM_Port"] = new RegistrySetting(path, (object) null, (object) "COM1", false);
      settings.RegistrySettings["SCALETIMEOUT"] = new RegistrySetting(path, (object) null, (object) "15", false);
      settings.RegistrySettings["FontSize"] = new RegistrySetting(path, (object) null, (object) "0", false);
      settings.RegistrySettings["HoldFileEnabled"] = new RegistrySetting(path, (object) null, (object) "Y", false);
      settings.RegistrySettings["PermanentDTChkOverride"] = new RegistrySetting(path, (object) null, (object) "1", false);
      settings.RegistrySettings["CloseForgivenessWindow"] = new RegistrySetting(path, (object) null, (object) "24", false);
      settings.RegistrySettings["ReconForgivenessWindow"] = new RegistrySetting(path, (object) null, (object) "24", false);
      settings.RegistrySettings["Auto_Passport_Listening"] = new RegistrySetting(path, (object) null, (object) "N", false);
      settings.RegistrySettings["DTChkReactivateDate"] = new RegistrySetting(path, (object) null, (object) null, false);
      settings.RegistrySettings["HFONEDITDONTSETHOLDCHECKBOX"] = new RegistrySetting(path, (object) null, (object) "Y", false);
      settings.RegistrySettings["UseRecipientEdit"] = new RegistrySetting(path + "/INTEGRATION", (object) null, (object) "N", false);
      settings.RegistrySettings["DataListenEnabled"] = new RegistrySetting(path + "/INTEGRATION", (object) null, (object) "N", false);
      settings.RegistrySettings["VBAIntegration"] = new RegistrySetting(path + "/INTEGRATION", (object) null, (object) "N", false);
      settings.RegistrySettings["PrintLabelToFile"] = new RegistrySetting(path + "/INTEGRATION", (object) null, (object) "0", false);
      settings.RegistrySettings["LABELFORMAT"] = new RegistrySetting(path + "/INTEGRATION", (object) null, (object) "0", false);
      settings.RegistrySettings["LABELLOCATION"] = new RegistrySetting(path + "/INTEGRATION", (object) null, (object) string.Empty, false);
      settings.RegistrySettings["DownloadGroundRatesAtRecon"] = new RegistrySetting("SHIPNET2000/ADMINSVC/SETTINGS", (object) null, (object) "N", false);
      settings.RegistrySettings["NeverDownloadGroundDiscounts"] = new RegistrySetting("SHIPNET2000/ADMINSVC/SETTINGS", (object) null, (object) "N", false);
    }

    private void SetupDefaultProfiles()
    {
      DefaultDShipDefl output1 = new DefaultDShipDefl();
      Error error1 = new Error();
      DefaultDShipDefl filter1 = new DefaultDShipDefl();
      filter1.DefaultInitializationType = ShipDefl.DefaultPreferenceType.Shipment;
      try
      {
        if (GuiData.AppController.ShipEngine.Retrieve<DefaultDShipDefl>(filter1, out output1, out error1) == 1)
        {
          output1.Description = "Default Domestic Shipping Profile";
          if (GuiData.AppController.ShipEngine.Insert<DefaultDShipDefl>(output1).Error.Code == 1)
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Saved Default Dom Profile");
          }
          else
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not save Default Dom Profile");
            if (error1 != null)
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, error1.Code.ToString() + " " + error1.Message);
          }
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not Retrieve Default Dom Profile" + error1.Code.ToString() + " " + error1.Message);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Threw exception Retrieving/Saving Default Dom Profile" + ex.Message);
      }
      filter1.ProfileCode = "PASSPORT";
      Error error2 = new Error();
      try
      {
        if (GuiData.AppController.ShipEngine.Retrieve<DefaultDShipDefl>(filter1, out output1, out error2) == 1)
        {
          output1.Description = "Default Passport Shipping Profile";
          if (GuiData.AppController.ShipEngine.Insert<DefaultDShipDefl>(output1).Error.Code == 1)
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Saved Default Passport Profile");
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not save Default Passport Profile");
          if (error2 != null)
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, error2.Code.ToString() + " " + error2.Message);
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not Retrieve Default Passport Profile" + error2.Code.ToString() + " " + error2.Message);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Threw exception Retrieving/Saving Default Passport Profile" + ex.Message);
      }
      DefaultIShipDefl output2 = new DefaultIShipDefl();
      DefaultIShipDefl filter2 = new DefaultIShipDefl();
      filter2.DefaultInitializationType = ShipDefl.DefaultPreferenceType.Shipment;
      Error error3 = new Error();
      try
      {
        if (GuiData.AppController.ShipEngine.Retrieve<DefaultIShipDefl>(filter2, out output2, out error3) == 1)
        {
          output2.Description = "Default International Shipping Profile";
          if (GuiData.AppController.ShipEngine.Insert<DefaultIShipDefl>(output2).Error.Code == 1)
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Saved Default Intl Profile");
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not save Default Intl Profile");
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not Retrieve Default Intl Profile");
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Threw exception Retrieving/Saving Default Intl Profile" + ex.Message);
      }
      DefaultTDShipDefl output3 = new DefaultTDShipDefl();
      DefaultTDShipDefl filter3 = new DefaultTDShipDefl();
      filter3.DefaultInitializationType = ShipDefl.DefaultPreferenceType.Shipment;
      Error error4 = new Error();
      try
      {
        if (GuiData.AppController.ShipEngine.Retrieve<DefaultTDShipDefl>(filter3, out output3, out error4) == 1)
        {
          output3.Description = "Default International Direct Distribution Shipping Profile";
          if (GuiData.AppController.ShipEngine.Insert<DefaultTDShipDefl>(output3).Error.Code == 1)
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Saved Default TD Profile");
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not save Default TD Profile");
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not Retrieve Default TD Profile");
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Threw exception Retrieving/Saving Default TD Profile" + ex.Message);
      }
      bool bVal = GuiData.ConfigManager.GetProfileBool("SHIPNET2000/GUI/SETTINGS", "EnableFreight", out bVal);
      if (!bVal)
        return;
      DefaultFShipDefl output4 = new DefaultFShipDefl();
      DefaultFShipDefl filter4 = new DefaultFShipDefl();
      filter4.DefaultInitializationType = ShipDefl.DefaultPreferenceType.Shipment;
      Error error5 = new Error();
      try
      {
        if (GuiData.AppController.ShipEngine.Retrieve<DefaultFShipDefl>(filter4, out output4, out error5) == 1)
        {
          output4.Description = "Default LTL Freight Shipping Profile";
          if (GuiData.AppController.ShipEngine.Insert<DefaultFShipDefl>(output4).Error.Code == 1)
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Saved Default Freight Profile");
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not save Default Freight Profile");
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not Retrieve Default Freight Profile");
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Threw exception Retrieving/Saving Default Freight Profile" + ex.Message);
      }
    }

    private void InitializeObjects(Account account)
    {
      if (this.DesignMode)
        return;
      try
      {
        this._settings = new SystemSettingsObject();
        this.InitializeRegistrySettings(this._settings);
        switch (this._operation)
        {
          case Utility.FormOperation.Add:
          case Utility.FormOperation.AddFirst:
            this._settings.AccountObject.DateFormat = "mm/dd/yyyy";
            this._settings.AccountObject.MinHistoryPurgeDays = (short) 120;
            this._settings.AccountObject.MaxHistoryPurgeDays = (short) 150;
            this._settings.AccountObject.RecipientDatabaseBackupInterval = (short) 30;
            this._settings.AccountObject.IsMaster = this._operation == Utility.FormOperation.AddFirst;
            Random random = new Random((int) DateTime.Now.Ticks);
            DateTime today = DateTime.Today;
            this._settings.AccountObject.GroundAutoCloseTime = new DateTime(today.Year, today.Month, today.Day, 17, random.Next(60), 0);
            string name = Application.CurrentCulture.Name;
            if (new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUISETTINGS).GetProfileString(string.Empty, "Language", out name))
              this._settings.AccountObject.Culture = name;
            this._settings.AccountObject.BoxInfo = new Box();
            this._settings.AccountObject.BoxInfo.SystemName = "1";
            this._settings.AccountObject.BoxInfo.SystemType = "1";
            this._settings.AccountObject.BoxInfo.SystemConfigType = "1";
            this._settings.AccountObject.FreightLTLPurgeMin = 7;
            this._settings.AccountObject.FreightLTLPurgeMax = 90;
            if (this._operation == Utility.FormOperation.AddFirst)
            {
              this.SetupDefaultProfiles();
              break;
            }
            break;
          case Utility.FormOperation.AddByDup:
          case Utility.FormOperation.ViewEdit:
            if (account == null)
              throw new ErrorException(0, "Account is null. Cannot proceed.");
            if (this._operation == Utility.FormOperation.ViewEdit)
            {
              this.edtSystem.ReadOnly = true;
              this.edtSystem.Text = account.MeterNumber;
            }
            Account output;
            int code = GuiData.AppController.ShipEngine.Retrieve<Account>(account, out output, out Error _);
            if (code != 1)
              throw new ErrorException(code, "Could not retrieve Account object");
            this._settings.AccountObject = output;
            if (this._operation == Utility.FormOperation.AddByDup && this._settings.AccountObject != null)
            {
              this._settings.AccountObject.AccountNumber = (string) null;
              this._settings.AccountObject.MeterNumber = (string) null;
              this._settings.AccountObject.IsMaster = false;
              this._settings.AccountObject.GroundAccountNumber = (string) null;
              break;
            }
            break;
        }
        FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager();
        foreach (KeyValuePair<string, RegistrySetting> registrySetting in this._settings.RegistrySettings)
        {
          string str = (string) null;
          if (registrySetting.Value.SystemAccountBased)
          {
            if (account != null)
            {
              StringBuilder stringBuilder = new StringBuilder();
              stringBuilder.AppendFormat("{0}/N{1}-{2}", (object) registrySetting.Value.Path, (object) account.MeterNumber, (object) account.AccountNumber);
              configManager.GetProfileString(stringBuilder.ToString(), registrySetting.Key, out str);
            }
          }
          else
            configManager.GetProfileString(registrySetting.Value.Path, registrySetting.Key, out str);
          this._settings.RegistrySettings[registrySetting.Key].Value = (object) str;
        }
      }
      catch (ErrorException ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), ex.Message);
        this._settings = (SystemSettingsObject) null;
      }
    }

    private void ObjectsToScreen()
    {
      if (this._settings == null || this._settings.AccountObject == null)
        return;
      this.edtSystem.Text = this._settings.AccountObject.MeterNumber;
      this.edtDescription.Text = this._settings.AccountObject.Description;
      this.UserPage.ObjectsToScreen(this._settings, this._operation);
      this.CustomerAdminPage.ObjectsToScreen(this._settings, this._operation);
      this.LoggingPage.ObjectsToScreen(this._settings, this._operation);
      this.ExpressAdminPage.ObjectsToScreen(this._settings, this._operation);
      this.GroundAdminPage.ObjectsToScreen(this._settings, this._operation);
      this.IntegrationPage.ObjectsToScreen(this._settings, this._operation);
    }

    private void ScreenToObjects()
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Enter method");
      this.Settings.AccountObject.MeterNumber = this.edtSystem.Raw;
      this.Settings.AccountObject.Description = this.edtDescription.Text;
      if (this._operation == Utility.FormOperation.AddFirst)
        this.Settings.AccountObject.IsMaster = true;
      this.ExpressAdminPage.ScreenToObjects(this._settings);
      this.CustomerAdminPage.ScreenToObjects(this._settings);
      this.UserPage.ScreenToObjects(this._settings);
      this.LoggingPage.ScreenToObjects();
      this.IntegrationPage.ScreenToObjects(this._settings);
      this.GroundAdminPage.ScreenToObjects(this._settings);
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "SystemSettings.btnOk_Click", "Okay Clicked");
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      try
      {
        this.DialogResult = DialogResult.None;
        this.ScreenToObjects();
        string appCodeGui1 = FxLogger.AppCode_GUI;
        TimeSpan elapsed = stopwatch.Elapsed;
        string inMessage1 = "screen to objects finished in " + elapsed.ToString();
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, appCodeGui1, "SystemSettings.btnOk_Click", inMessage1);
        if (this.ValidateObjects() == 1)
        {
          int num1 = this.SaveObjects();
          string appCodeGui2 = FxLogger.AppCode_GUI;
          elapsed = stopwatch.Elapsed;
          string inMessage2 = "save objects finished in " + elapsed.ToString();
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, appCodeGui2, "SystemSettings.btnOk_Click", inMessage2);
          ReportVariables.DateFormat = this._settings.AccountObject.DateFormat.Equals("mm/dd/yyyy") ? "MM/dd/yyyy" : "dd/MMM/yyyy";
          if (num1 == 1)
          {
            bool flag1 = false;
            ShutdownRequestEventArgs e1 = (ShutdownRequestEventArgs) null;
            if (this.Operation == Utility.FormOperation.AddFirst)
              Utility.RunWorldProImport(this._settings.AccountObject.Address.StateProvince == "PR" ? this._settings.AccountObject.Address.StateProvince : this._settings.AccountObject.Address.CountryCode);
            if (this.UserPage.ShowEtdImagePrompt)
            {
              using (EtdImagePromptDialog imagePromptDialog = new EtdImagePromptDialog(this._settings.AccountObject))
              {
                int num2 = (int) imagePromptDialog.ShowDialog((IWin32Window) this);
              }
            }
            if (this.UserPage.HasLanguageChanged || this.UserPage.HasFontSizeChanged)
            {
              if (this.UserPage.HasLanguageChanged)
                SystemSettings.IntegrationHook(OnEvents.OnLanguageChanged, this._settings.AccountObject.Culture);
              if (MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate("LanguageChangedRestart"), string.Empty, Utility.DevMode ? MessageBoxButtons.OKCancel : MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK || !Utility.DevMode)
              {
                e1 = new ShutdownRequestEventArgs(new SystemShutdownRequest()
                {
                  Restart = SystemShutdownRequest.RestartType.Soft,
                  StopType = SystemShutdownRequest.StopStart.StopAllButAdmin
                }, true);
                flag1 = true;
              }
            }
            bool expiredRatesChanged = this.CustomerAdminPage.ExpiredRatesChanged;
            if (!flag1 & expiredRatesChanged && (MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate("LanguageChangedRestart"), string.Empty, Utility.DevMode ? MessageBoxButtons.OKCancel : MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK || !Utility.DevMode))
            {
              e1 = new ShutdownRequestEventArgs(new SystemShutdownRequest()
              {
                Restart = SystemShutdownRequest.RestartType.Soft,
                StopType = SystemShutdownRequest.StopStart.StopAllButAdmin
              }, true);
              flag1 = true;
            }
            bool rateLoggingChanged = this.LoggingPage.RateLoggingChanged;
            if (!flag1 & rateLoggingChanged && (MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate("LanguageChangedRestart"), string.Empty, Utility.DevMode ? MessageBoxButtons.OKCancel : MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK || !Utility.DevMode))
            {
              e1 = new ShutdownRequestEventArgs(new SystemShutdownRequest()
              {
                Restart = SystemShutdownRequest.RestartType.Soft,
                StopType = SystemShutdownRequest.StopStart.StopAllButAdmin
              }, true);
              flag1 = true;
            }
            bool flag2 = this.Operation == Utility.FormOperation.AddFirst && this.IntegrationPage.IsIntegrationChecked;
            if (!flag1 && (flag2 || this.IntegrationPage.HasFxiaVersionChanged) && (MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate("LanguageChangedRestart"), string.Empty, Utility.DevMode ? MessageBoxButtons.OKCancel : MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK || !Utility.DevMode))
            {
              e1 = new ShutdownRequestEventArgs(new SystemShutdownRequest()
              {
                Restart = SystemShutdownRequest.RestartType.Soft,
                StopType = SystemShutdownRequest.StopStart.StopAllButServices
              }, true);
              flag1 = true;
            }
            if (!flag1)
              GuiData.AppController.ShipEngine.StartTheEngine();
            else if (this.RequestShutdown != null)
              this.RequestShutdown((object) this, e1);
            this.DialogResult = DialogResult.OK;
          }
        }
        stopwatch.Stop();
        string appCodeGui3 = FxLogger.AppCode_GUI;
        elapsed = stopwatch.Elapsed;
        string inMessage3 = "Finished sucessfully in " + elapsed.ToString();
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, appCodeGui3, "SystemSettings.btnOk_Click", inMessage3);
      }
      catch (TabPageException ex)
      {
        stopwatch.Stop();
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "SystemSettings.btnOk_Click", "Finished with error in " + stopwatch.Elapsed.ToString());
        this.tabControlSystemSettings.SelectedTab = ex.TabPage;
        this.HandleError(ex.Error.Code);
        Utility.DisplayError(new Error()
        {
          Message = ex.Error.Message,
          Code = ex.Error.Code
        });
      }
    }

    private void HandleError(int code)
    {
      if (code == 3167 || code == 13582)
      {
        this.edtSystem.Focus();
      }
      else
      {
        if (this.UserPage.HandleError(code) || this.CustomerAdminPage.HandleError(code) || this.ExpressAdminPage.HandleError(code) || this.GroundAdminPage.HandleError(code) || this.IntegrationPage.HandleError(code))
          return;
        this.LoggingPage.HandleError(code);
      }
    }

    private int ValidateObjects()
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Enter method");
      try
      {
        Account output;
        GuiData.AppController.ShipEngine.Retrieve<Account>(new Account()
        {
          IsMaster = true
        }, out output, out Error _);
        ServiceResponse serviceResponse1 = GuiData.AppController.ShipEngine.ValidateAccount(this._settings.AccountObject, output);
        if (serviceResponse1.HasError)
          throw new ErrorException(serviceResponse1.Error);
        ServiceResponse serviceResponse2 = GuiData.AppController.ShipEngine.ValidateAccountDetails(this._settings.AccountObject);
        if (serviceResponse2.HasError)
          throw new ErrorException(serviceResponse2.Error);
        if (this._settings.AccountObject.IsSmartPostEnabled)
        {
          string message;
          int errorCode = SmartPostConfigurationForm.ValidateSettings(this._settings.AccountObject, out message);
          if (errorCode != 1)
            throw new ErrorException(new Error(errorCode, message));
        }
      }
      catch (ErrorException ex)
      {
        Utility.DisplayError(ex.CafeError);
        this.HandleError(ex.CafeError.Code);
        return ex.CafeError.Code;
      }
      catch (Exception ex)
      {
        Error e = new Error();
        e.Message = ex.Message;
        e.Code = 0;
        Utility.DisplayError(e);
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), ex.StackTrace);
        return e.Code;
      }
      return 1;
    }

    private int SaveObjects()
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Enter method");
      ServiceResponse serviceResponse;
      try
      {
        if (this._operation == Utility.FormOperation.ViewEdit)
        {
          serviceResponse = GuiData.AppController.ShipEngine.Modify<Account>(this._settings.AccountObject);
        }
        else
        {
          serviceResponse = !this._accountCommitted ? GuiData.AppController.ShipEngine.Insert<Account>(this._settings.AccountObject) : GuiData.AppController.ShipEngine.Modify<Account>(this._settings.AccountObject);
          if (Utility.DisplayError(serviceResponse.Error) == 1)
          {
            if (!this._accountCommitted)
            {
              Account output;
              if (1 == GuiData.AppController.ShipEngine.Retrieve<Account>(this._settings.AccountObject, out output, out Error _))
                this._settings.AccountObject = output;
              else
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, nameof (SaveObjects), "Could not get the InitControl updated account");
            }
            this._accountCommitted = true;
            GuiData.AppController.ShipEngine.QueueAdminTransaction((IAdminTransaction) new Registration()
            {
              AccountNumber = this._settings.AccountObject.AccountNumber,
              Address1 = this._settings.AccountObject.Address.Addr1,
              Address2 = this._settings.AccountObject.Address.Addr2,
              City = this._settings.AccountObject.Address.City,
              CompanyName = this._settings.AccountObject.Address.Company,
              ContactName = this._settings.AccountObject.Address.Contact,
              Country = this._settings.AccountObject.Address.CountryCode,
              CustNumber = this._settings.AccountObject.AccountNumber,
              Fax = this._settings.AccountObject.Address.Phone,
              GroundAccountNumber = this._settings.AccountObject.GroundAccountNumber,
              MeterNumber = this._settings.AccountObject.MeterNumber,
              OriginSvcId = this._settings.AccountObject.OriginId,
              Phone = this._settings.AccountObject.Address.Phone,
              State = this._settings.AccountObject.Address.StateProvince,
              ZipCode = this._settings.AccountObject.Address.PostalCode
            });
          }
        }
        if (serviceResponse.Error.Code != 1)
          throw new ErrorException(serviceResponse.Error);
        if (this._systemSettingsUser.EtdChanged)
          this.LogEtdChange(this._settings.AccountObject);
        if (this._systemSettingsUser.ReturnsChanged)
          this.LogReturnsChange(this._settings.AccountObject);
        Sender s;
        if (this._systemSettingsCustomerAdmin.GetDefaultSender(out s) && s != null)
        {
          serviceResponse = GuiData.AppController.ShipEngine.Insert<SenderRequest>(new SenderRequest(this._settings.AccountObject, s));
          if (!serviceResponse.HasError)
          {
            if (this.SenderListChanged != null)
              this.SenderListChanged((object) this, new EventArgs());
            this._settings.AccountObject.DefaultSenderCode = s.Code;
            serviceResponse = GuiData.AppController.ShipEngine.Modify<Account>(this._settings.AccountObject);
            if (Utility.DisplayError(serviceResponse.Error) != 1)
            {
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Error modifying account - setting default sender.");
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, serviceResponse.Error.Message);
              return serviceResponse.Error.Code;
            }
          }
          else
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Error inserting default sender. " + serviceResponse.ErrorCode.ToString());
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, serviceResponse.Error.Message);
            Utility.DisplayError(serviceResponse.Error);
            serviceResponse.Error.Code = 1;
          }
        }
        this.SetPrinters();
        bool flag = false;
        SoftwareDataTrans adminTrans = new SoftwareDataTrans();
        adminTrans.Account = this._settings.AccountObject.AccountNumber;
        adminTrans.MeterNumber = this._settings.AccountObject.MeterNumber;
        if (this._systemSettingsCustomerAdmin.ToggleListRates)
        {
          adminTrans.ExpressListEnabled = this._systemSettingsCustomerAdmin.UseListRates ? SoftwareDataTrans.EnabledFlags.Enabled : SoftwareDataTrans.EnabledFlags.Disabled;
          flag = true;
        }
        if (flag)
          GuiData.AppController.ShipEngine.QueueAdminTransaction((IAdminTransaction) adminTrans);
        if (this._systemSettingsGroundAdmin.SendTransaction)
        {
          if (this.SendRegistrationTransaction())
          {
            GuiData.AppController.ShipEngine.Modify<Account>(this._settings.AccountObject);
          }
          else
          {
            this._settings.AccountObject.GroundAccountNumber = string.Empty;
            this._settings.AccountObject.IsGroundEnabled = false;
            this._settings.AccountObject.IsGroundEverEnabled |= this._settings.AccountObject.IsGroundEnabled;
            GuiData.AppController.ShipEngine.Modify<Account>(this._settings.AccountObject);
          }
        }
        FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager();
        foreach (KeyValuePair<string, RegistrySetting> registrySetting in this._settings.RegistrySettings)
        {
          if (registrySetting.Value.SystemAccountBased)
          {
            if (this._settings.AccountObject != null)
            {
              StringBuilder stringBuilder = new StringBuilder();
              stringBuilder.AppendFormat("{0}/N{1}-{2}", (object) registrySetting.Value.Path, (object) this._settings.AccountObject.MeterNumber, (object) this._settings.AccountObject.AccountNumber);
              configManager.SetProfileValue(stringBuilder.ToString(), registrySetting.Key, (object) registrySetting.Value.Value.ToString());
            }
          }
          else if (registrySetting.Value.Value != null)
            configManager.SetProfileValue(registrySetting.Value.Path, registrySetting.Key, (object) registrySetting.Value.Value.ToString());
        }
      }
      catch (ErrorException ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "SystemSettings.SaveObjects", "Error in SaveObjects: " + ex?.ToString());
        return ex.CafeError.Code;
      }
      return serviceResponse.Error.Code;
    }

    private bool SendRegistrationTransaction()
    {
      Registration requestInfo = new Registration();
      requestInfo.Address1 = this._settings.AccountObject.Address.Addr1;
      requestInfo.Address2 = this._settings.AccountObject.Address.Addr2;
      requestInfo.City = this._settings.AccountObject.Address.City;
      requestInfo.CompanyName = this._settings.AccountObject.Address.Company;
      requestInfo.ContactName = this._settings.AccountObject.Address.Contact;
      requestInfo.Country = this._settings.AccountObject.Address.CountryCode;
      requestInfo.State = this._settings.AccountObject.Address.StateProvince;
      requestInfo.ZipCode = this._settings.AccountObject.Address.PostalCode;
      requestInfo.AccountNumber = this.ExpressAdminPage.AccountNumber;
      requestInfo.MeterNumber = this.MeterNumber;
      Account groundData = (Account) null;
      Error error = new Error();
      try
      {
        this.Cursor = Cursors.WaitCursor;
        ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.DownloadGroundAccountData(requestInfo, out groundData);
        this.Cursor = Cursors.Arrow;
        if (!serviceResponse.IsOperationOk)
        {
          this.DisableGroundSettings();
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), string.Format("Error encountered registering Ground Account. Error details {0} {1}", (object) serviceResponse.ErrorCode, (object) serviceResponse.ErrorMessage));
          int num = (int) MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate("MeterNotGroundEnabled"), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          if (groundData.GroundAccountNumber != null)
          {
            if (!string.IsNullOrEmpty(groundData.GroundAccountNumber.Trim(' ', '0')))
            {
              this._settings.AccountObject.GroundAccountNumber = groundData.GroundAccountNumber;
              return true;
            }
          }
          int num = (int) MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate("MeterNotGroundEnabled"), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          this.DisableGroundSettings();
        }
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), ex.Message);
        int num = (int) Utility.DisplayError(ex.Message);
      }
      return false;
    }

    private void DisableGroundSettings()
    {
      this._settings.AccountObject.IsGroundEnabled = false;
      this._settings.AccountObject.IsGroundEverEnabled |= this._settings.AccountObject.IsGroundEnabled;
      this._settings.AccountObject.GroundAccountNumber = string.Empty;
      this._settings.AccountObject.SPReturnAddressEntryEnabled = false;
    }

    private void SetPrinters()
    {
      if (this.CustomerAdminPage.LabelPrinter == null && this.CustomerAdminPage.ReportPrinter == null)
        return;
      Utility.SetupPrinters(this._settings.AccountObject.MeterNumber, this._settings.AccountObject.AccountNumber, (short) new FedEx.Gsm.Common.ConfigManager.ConfigManager().NetworkClientID, this.CustomerAdminPage.LabelPrinter, this.CustomerAdminPage.ReportPrinter);
      if (this.Operation == Utility.FormOperation.Add || this.Operation == Utility.FormOperation.AddFirst || this.Operation == Utility.FormOperation.AddByDup || GuiData.CurrentAccount == null || !(GuiData.CurrentAccount.MeterNumber == this._settings.AccountObject.MeterNumber) || this.PrinterSettingsChanged == null)
        return;
      this.PrinterSettingsChanged((object) this, EventArgs.Empty);
    }

    private void LogEtdChange(Account account)
    {
      if (account == null)
        return;
      this.LogTermsConditionsChange("etd", DateTime.Now.ToString() + ": Etd " + (account.ETDEnabled ? "Activated" : "Deactivated") + " for Account " + account.AccountNumber + " Meter " + account.MeterNumber);
    }

    private void LogReturnsChange(Account account)
    {
      if (account == null)
        return;
      this.LogTermsConditionsChange("returns", DateTime.Now.ToString() + ": Return Shipping " + (account.ReturnShippingEnabled ? "Activated" : "Deactivated") + " for Account " + account.AccountNumber + " Meter " + account.MeterNumber);
    }

    private void LogTermsConditionsChange(string feature, string message)
    {
      if (string.IsNullOrEmpty(feature))
        throw new ArgumentNullException(nameof (feature));
      if (string.IsNullOrEmpty(message))
        throw new ArgumentNullException(nameof (message));
      try
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Verbose, FxLogger.AppCode_GUI, nameof (LogTermsConditionsChange), message);
        File.AppendAllText(Path.Combine(DataFileLocations.Instance.RootFilesDir, feature + ".log"), message);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, nameof (LogTermsConditionsChange), ex.ToString());
      }
    }

    public void ProcessAutoCloseTimes(Account account)
    {
      DateTime today = DateTime.Today;
      DateTime dateTime1 = new DateTime(today.Year, today.Month, today.Day, 22, 0, 0);
      Random random = new Random((int) DateTime.Now.Ticks);
      DateTime dateTime2 = new DateTime();
      DateTime dateTime3 = new DateTime();
      if (this.Operation == Utility.FormOperation.AddFirst)
      {
        dateTime2 = dateTime1.AddHours((double) random.Next(6)).AddMinutes((double) random.Next(60));
        dateTime3 = new DateTime(dateTime2.Ticks).AddHours(1.0);
      }
      else if (this.Operation == Utility.FormOperation.Add || this.Operation == Utility.FormOperation.AddByDup)
      {
        Account output;
        if (GuiData.AppController.ShipEngine.Retrieve<Account>(new Account()
        {
          IsMaster = true
        }, out output, out Error _) == 1)
        {
          DateTime autoCloseTime = output.AutoCloseTime;
          DateTime receiveDownloadTime = output.ReceiveDownloadTime;
          DateTime dateTime4 = autoCloseTime.AddMinutes(30.0);
          if (autoCloseTime.Hour < 4)
          {
            if (dateTime4.Hour >= 4)
            {
              dateTime2 = autoCloseTime;
              dateTime3 = autoCloseTime.AddMinutes(31.0);
            }
            else
            {
              TimeSpan timeSpan = new DateTime(dateTime4.Year, dateTime4.Month, dateTime4.Day, 4, 0, 0) - dateTime4;
              int num = random.Next(Convert.ToInt32(Math.Abs(timeSpan.TotalMinutes)));
              dateTime2 = dateTime4 + new TimeSpan(0, num / 60, num % 60, 0);
              if (dateTime2.Minute == 0)
                dateTime2 = dateTime2.AddMinutes(1.0);
              dateTime3 = dateTime2.AddHours(1.0);
            }
          }
          else
          {
            DateTime dateTime5 = dateTime4.Date;
            dateTime5 = dateTime5.AddDays(1.0);
            TimeSpan timeSpan = dateTime5.AddHours(4.0) - dateTime4;
            int num = random.Next(Convert.ToInt32(Math.Abs(timeSpan.TotalMinutes)));
            dateTime2 = receiveDownloadTime.Hour < 22 ? dateTime1 + new TimeSpan(0, num / 60, num % 60, 0) : dateTime4 + new TimeSpan(0, num / 60, num % 60, 0);
            if (dateTime2.Minute == 0)
              dateTime2 = dateTime2.AddMinutes(1.0);
            dateTime3 = dateTime2.AddHours(1.0);
          }
        }
      }
      else
      {
        dateTime2 = account.AutoCloseTime;
        dateTime3 = account.ReceiveDownloadTime;
      }
      this.UserPage.ExpressAutoCloseTime = dateTime2;
      this.ExpressAdminPage.ReconcileTime = dateTime3;
    }

    private void tabControlSystemSettings_Selecting(object sender, TabControlCancelEventArgs e)
    {
      if (e.Action != TabControlAction.Selecting)
        return;
      if (e.TabPage.Equals((object) this.tabPageUser) || e.TabPage.Equals((object) this.tabPageCustomerAdmin))
      {
        e.Cancel = false;
      }
      else
      {
        if (!this._loggedInAlready)
          this._loggedInAlready = new AdminLogin(AdminLogin.PasswordType.Any).ShowDialog((IWin32Window) this) == DialogResult.OK;
        e.Cancel = !this._loggedInAlready;
      }
    }

    private static void IntegrationHook(OnEvents eventType, string data)
    {
      if (GuiData.IntegrationInProgress)
        return;
      ManagedToUnmanagedWrapper.WinExecEntry("FSM.Cafe,CafeInterfaceEx", string.Empty, string.Empty, (int) eventType, 1, data, GuiData.CurrentAccount != null ? GuiData.CurrentAccount.MeterNumber : (string) null);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SystemSettings));
      this.label1 = new Label();
      this.edtSystem = new FdxMaskedEdit();
      this.label2 = new Label();
      this.edtDescription = new TextBox();
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.tabControlSystemSettings = new TabControlEx();
      this.tabPageUser = new TabPage();
      this._systemSettingsUser = new SystemSettingsUser();
      this.tabPageCustomerAdmin = new TabPage();
      this._systemSettingsCustomerAdmin = new SystemSettingsCustomerAdmin();
      this.tabPageLogging = new TabPage();
      this._systemSettingsLogging = new SystemSettingsLogging();
      this.tabPageFedExExpressAdmin = new TabPage();
      this._systemSettingsExpressAdmin = new SystemSettingsExpressAdmin();
      this.tabPageFedExGroundAdmin = new TabPage();
      this._systemSettingsGroundAdmin = new SystemSettingsGroundAdmin();
      this.tabPageIntegration = new TabPage();
      this._systemSettingsIntegration = new SystemSettingsIntegration();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.setAllToolStripMenuItem = new ToolStripMenuItem();
      this.criticalToolStripMenuItem = new ToolStripMenuItem();
      this.errorToolStripMenuItem = new ToolStripMenuItem();
      this.warningToolStripMenuItem = new ToolStripMenuItem();
      this.infoToolStripMenuItem = new ToolStripMenuItem();
      this.verboseToolStripMenuItem = new ToolStripMenuItem();
      this.debugToolStripMenuItem = new ToolStripMenuItem();
      this.focusExtender1 = new FocusExtender();
      this.tabControlSystemSettings.SuspendLayout();
      this.tabPageUser.SuspendLayout();
      this.tabPageCustomerAdmin.SuspendLayout();
      this.tabPageLogging.SuspendLayout();
      this.tabPageFedExExpressAdmin.SuspendLayout();
      this.tabPageFedExGroundAdmin.SuspendLayout();
      this.tabPageIntegration.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.helpProvider1.SetShowHelp((Control) this.label1, (bool) componentResourceManager.GetObject("label1.ShowHelp"));
      this.edtSystem.Allow = "";
      this.edtSystem.Disallow = "";
      this.edtSystem.eMask = eMasks.maskCustom;
      this.edtSystem.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtSystem, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtSystem, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtSystem, componentResourceManager.GetString("edtSystem.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtSystem, (HelpNavigator) componentResourceManager.GetObject("edtSystem.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtSystem, "edtSystem");
      this.edtSystem.Mask = "999999999~T! ";
      this.edtSystem.Name = "edtSystem";
      this.helpProvider1.SetShowHelp((Control) this.edtSystem, (bool) componentResourceManager.GetObject("edtSystem.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      this.helpProvider1.SetShowHelp((Control) this.label2, (bool) componentResourceManager.GetObject("label2.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.edtDescription, "edtDescription");
      this.focusExtender1.SetFocusBackColor((Control) this.edtDescription, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDescription, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDescription, componentResourceManager.GetString("edtDescription.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtDescription, (HelpNavigator) componentResourceManager.GetObject("edtDescription.HelpNavigator"));
      this.edtDescription.Name = "edtDescription";
      this.helpProvider1.SetShowHelp((Control) this.edtDescription, (bool) componentResourceManager.GetObject("edtDescription.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.Name = "btnOk";
      this.helpProvider1.SetShowHelp((Control) this.btnOk, (bool) componentResourceManager.GetObject("btnOk.ShowHelp"));
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Name = "btnCancel";
      this.helpProvider1.SetShowHelp((Control) this.btnCancel, (bool) componentResourceManager.GetObject("btnCancel.ShowHelp"));
      this.btnCancel.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.tabControlSystemSettings, "tabControlSystemSettings");
      this.tabControlSystemSettings.Controls.Add((Control) this.tabPageUser);
      this.tabControlSystemSettings.Controls.Add((Control) this.tabPageCustomerAdmin);
      this.tabControlSystemSettings.Controls.Add((Control) this.tabPageLogging);
      this.tabControlSystemSettings.Controls.Add((Control) this.tabPageFedExExpressAdmin);
      this.tabControlSystemSettings.Controls.Add((Control) this.tabPageFedExGroundAdmin);
      this.tabControlSystemSettings.Controls.Add((Control) this.tabPageIntegration);
      this.tabControlSystemSettings.MnemonicEnabled = false;
      this.tabControlSystemSettings.Name = "tabControlSystemSettings";
      this.tabControlSystemSettings.SelectedIndex = 0;
      this.helpProvider1.SetShowHelp((Control) this.tabControlSystemSettings, (bool) componentResourceManager.GetObject("tabControlSystemSettings.ShowHelp"));
      this.tabControlSystemSettings.UseIndexAsMnemonic = true;
      this.tabControlSystemSettings.Selecting += new TabControlCancelEventHandler(this.tabControlSystemSettings_Selecting);
      this.tabPageUser.Controls.Add((Control) this._systemSettingsUser);
      componentResourceManager.ApplyResources((object) this.tabPageUser, "tabPageUser");
      this.tabPageUser.Name = "tabPageUser";
      this.helpProvider1.SetShowHelp((Control) this.tabPageUser, (bool) componentResourceManager.GetObject("tabPageUser.ShowHelp"));
      this.tabPageUser.Tag = (object) "";
      this.tabPageUser.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this._systemSettingsUser, "_systemSettingsUser");
      this._systemSettingsUser.MyParent = (SystemSettings) null;
      this._systemSettingsUser.Name = "_systemSettingsUser";
      componentResourceManager.ApplyResources((object) this.tabPageCustomerAdmin, "tabPageCustomerAdmin");
      this.tabPageCustomerAdmin.Controls.Add((Control) this._systemSettingsCustomerAdmin);
      this.tabPageCustomerAdmin.Name = "tabPageCustomerAdmin";
      this.helpProvider1.SetShowHelp((Control) this.tabPageCustomerAdmin, (bool) componentResourceManager.GetObject("tabPageCustomerAdmin.ShowHelp"));
      this.tabPageCustomerAdmin.Tag = (object) "";
      this.tabPageCustomerAdmin.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this._systemSettingsCustomerAdmin, "_systemSettingsCustomerAdmin");
      this._systemSettingsCustomerAdmin.Name = "_systemSettingsCustomerAdmin";
      componentResourceManager.ApplyResources((object) this.tabPageLogging, "tabPageLogging");
      this.tabPageLogging.Controls.Add((Control) this._systemSettingsLogging);
      this.tabPageLogging.Name = "tabPageLogging";
      this.helpProvider1.SetShowHelp((Control) this.tabPageLogging, (bool) componentResourceManager.GetObject("tabPageLogging.ShowHelp"));
      this.tabPageLogging.Tag = (object) "";
      this.tabPageLogging.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this._systemSettingsLogging, "_systemSettingsLogging");
      this._systemSettingsLogging.Name = "_systemSettingsLogging";
      componentResourceManager.ApplyResources((object) this.tabPageFedExExpressAdmin, "tabPageFedExExpressAdmin");
      this.tabPageFedExExpressAdmin.Controls.Add((Control) this._systemSettingsExpressAdmin);
      this.tabPageFedExExpressAdmin.Name = "tabPageFedExExpressAdmin";
      this.helpProvider1.SetShowHelp((Control) this.tabPageFedExExpressAdmin, (bool) componentResourceManager.GetObject("tabPageFedExExpressAdmin.ShowHelp"));
      this.tabPageFedExExpressAdmin.Tag = (object) "";
      this.tabPageFedExExpressAdmin.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this._systemSettingsExpressAdmin, "_systemSettingsExpressAdmin");
      this._systemSettingsExpressAdmin.MyParent = (SystemSettings) null;
      this._systemSettingsExpressAdmin.Name = "_systemSettingsExpressAdmin";
      componentResourceManager.ApplyResources((object) this.tabPageFedExGroundAdmin, "tabPageFedExGroundAdmin");
      this.tabPageFedExGroundAdmin.Controls.Add((Control) this._systemSettingsGroundAdmin);
      this.tabPageFedExGroundAdmin.Name = "tabPageFedExGroundAdmin";
      this.helpProvider1.SetShowHelp((Control) this.tabPageFedExGroundAdmin, (bool) componentResourceManager.GetObject("tabPageFedExGroundAdmin.ShowHelp"));
      this.tabPageFedExGroundAdmin.Tag = (object) "";
      this.tabPageFedExGroundAdmin.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this._systemSettingsGroundAdmin, "_systemSettingsGroundAdmin");
      this._systemSettingsGroundAdmin.Name = "_systemSettingsGroundAdmin";
      this._systemSettingsGroundAdmin.SendTransaction = false;
      this.tabPageIntegration.BackColor = Color.White;
      this.tabPageIntegration.Controls.Add((Control) this._systemSettingsIntegration);
      componentResourceManager.ApplyResources((object) this.tabPageIntegration, "tabPageIntegration");
      this.tabPageIntegration.Name = "tabPageIntegration";
      this.helpProvider1.SetShowHelp((Control) this.tabPageIntegration, (bool) componentResourceManager.GetObject("tabPageIntegration.ShowHelp"));
      this.tabPageIntegration.Tag = (object) "";
      this.tabPageIntegration.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this._systemSettingsIntegration, "_systemSettingsIntegration");
      this._systemSettingsIntegration.MyParent = (SystemSettings) null;
      this._systemSettingsIntegration.Name = "_systemSettingsIntegration";
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.setAllToolStripMenuItem
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.helpProvider1.SetShowHelp((Control) this.contextMenuStrip1, (bool) componentResourceManager.GetObject("contextMenuStrip1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.contextMenuStrip1, "contextMenuStrip1");
      this.setAllToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.criticalToolStripMenuItem,
        (ToolStripItem) this.errorToolStripMenuItem,
        (ToolStripItem) this.warningToolStripMenuItem,
        (ToolStripItem) this.infoToolStripMenuItem,
        (ToolStripItem) this.verboseToolStripMenuItem,
        (ToolStripItem) this.debugToolStripMenuItem
      });
      this.setAllToolStripMenuItem.Name = "setAllToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.setAllToolStripMenuItem, "setAllToolStripMenuItem");
      this.criticalToolStripMenuItem.Name = "criticalToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.criticalToolStripMenuItem, "criticalToolStripMenuItem");
      this.errorToolStripMenuItem.Name = "errorToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.errorToolStripMenuItem, "errorToolStripMenuItem");
      this.warningToolStripMenuItem.Name = "warningToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.warningToolStripMenuItem, "warningToolStripMenuItem");
      this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.infoToolStripMenuItem, "infoToolStripMenuItem");
      this.verboseToolStripMenuItem.Name = "verboseToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.verboseToolStripMenuItem, "verboseToolStripMenuItem");
      this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.debugToolStripMenuItem, "debugToolStripMenuItem");
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.edtDescription);
      this.Controls.Add((Control) this.edtSystem);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.tabControlSystemSettings);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (SystemSettings);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Load += new EventHandler(this.SystemSettings_Load);
      this.tabControlSystemSettings.ResumeLayout(false);
      this.tabPageUser.ResumeLayout(false);
      this.tabPageCustomerAdmin.ResumeLayout(false);
      this.tabPageLogging.ResumeLayout(false);
      this.tabPageFedExExpressAdmin.ResumeLayout(false);
      this.tabPageFedExGroundAdmin.ResumeLayout(false);
      this.tabPageIntegration.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
