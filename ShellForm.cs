// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.ShellForm
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Controller;
using FedEx.Gsm.Cafe.ApplicationEngine.Controller.Utilities;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.BackupRestore;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.CommandManagement;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.HTML;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Plugins;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Properties;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.ShipModule;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UploadDownload;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Utilities;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions.Plugins;
using FedEx.Gsm.Cafe.ApplicationEngine.Integration;
using FedEx.Gsm.Common.ConfigManager;
using FedEx.Gsm.Common.Integration.EventData;
using FedEx.Gsm.Common.Integration.SharedEnumsStructs;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.Common.Parser.CTS;
using FedEx.Gsm.ShipEngine.CommApi;
using FedEx.Gsm.ShipEngine.DataAccess;
using FedEx.Gsm.ShipEngine.Entities;
using FedEx.Gsm.ShipEngine.Reports.ReportLogic;
using FedEx.Gsm.ShipEngine.Reports.Utilities;
using FedEx.Gsm.ShipEngine.ShipmentTransaction;
using FedEx.PluginManagement;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using ValidatorMigrationLogic;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class ShellForm : Form
  {
    private CafeColorTable _colorTable = CafeColorTable.FlatColorTable;
    private bool _bIsFaxSpodInProgress;
    private bool _bSddgPrinterSet;
    private bool _bOP900LGPrinterSet;
    private bool _bOP900LLPrinterSet;
    private bool _bIntlAwbPrinterSet;
    private bool _forceQuietShutdown;
    private bool _fxiStarted;
    private bool _passportAllReadyLoaded;
    private AutoResetEvent _stopCommThread = new AutoResetEvent(false);
    private AutoResetEvent _commMessageAvailable = new AutoResetEvent(false);
    private ShellForm.CommStatusCallback CommCallback;
    private CommStatus _commStatus;
    private static string _currentCommMessage = string.Empty;
    private System.Timers.Timer _midnightTimer = new System.Timers.Timer();
    private System.Timers.Timer _iddCloseTimer = new System.Timers.Timer();
    private static FedEx.Gsm.Cafe.ApplicationEngine.Gui.BackupRestore.BackupRestore _backupRestoreModule = new FedEx.Gsm.Cafe.ApplicationEngine.Gui.BackupRestore.BackupRestore();
    private CommandManager _commandManager = new CommandManager();
    private Process _softwareProcess = new Process();
    private bool _bSoftwareOnlySuccess;
    private List<string> _holdFileIdList;
    private int _holdFileIndex;
    private DgWarning _dgWarnDialog;
    private TwoFiftyTransactionTrafficCop _twoFifty;
    private HoldFileLogic _holdLogic;
    private IntegrationLauncher _launcher;
    private HTMLModule _htmlModule;
    private int _fontIndex;
    private Process reportProcessing;
    private List<AbstractToolStripItemPluginHandler> _listPluginHandlers = new List<AbstractToolStripItemPluginHandler>();
    protected const int VK_RETURN = 13;
    protected const int WM_KEYDOWN = 256;
    private IContainer components;
    private MenuStrip menuStripMain;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStrip toolStripMain;
    private Label label1;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem databasesToolStripMenuItem;
    private ToolStripMenuItem utilitiesToolStripMenuItem;
    private ToolStripMenuItem integrationToolStripMenuItem;
    private ToolStripMenuItem inboundToolStripMenuItem;
    private ToolStripMenuItem fedexcomToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem fileMaintenanceToolStripMenuItem;
    private ToolStripMenuItem backupToolStripMenuItem;
    private ToolStripMenuItem restoreToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem customizeToolStripMenuItem;
    private ToolStripMenuItem activeSystemAccountToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripMenuItem shippingProfilesToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripMenuItem customizeFieldsToolStripMenuItem;
    private ToolStripMenuItem customizeUserPromptsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripMenuItem systemSettingsToolStripMenuItem;
    private ToolStripMenuItem formsToolStripMenuItem;
    private ToolStripMenuItem customLabelToolStripMenuItem;
    private ToolStripMenuItem downloadToolStripMenuItem;
    private ToolStripMenuItem installSoftwareUpdateToolStripMenuItem;
    private ToolStripMenuItem uploadToolStripMenuItem;
    private StatusStrip statusStripMain;
    private ToolStripStatusLabel toolStripStatusAreaHelp;
    private ToolStripStatusLabel toolStripStatusAreaDateTime;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem languageToolStripMenuItem;
    private ToolStripMenuItem englishToolStripMenuItem;
    private ToolStripMenuItem spanishToolStripMenuItem;
    private ToolStripMenuItem frenchToolStripMenuItem;
    private ToolStripMenuItem recipientListToolStripMenuItem;
    private ToolStripMenuItem fedExAddressCheckerToolStripMenuItem;
    private ToolStripMenuItem addrChkrPrefsMenuItem;
    private ToolStripMenuItem addrChkrBatchMenuItem;
    private ToolStripProgressBar toolStripStatusBarProgress;
    private ToolStripStatusLabel toolStripStatusUrsa;
    private ToolStripStatusLabel toolStripSpecialMessage;
    private ToolStripMenuItem testCommunicationsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem helpMeFedExToolStripMenuItem;
    private ToolStripStatusLabel toolStripStatusLabelDevMode;
    private ToolStripStatusLabel toolStripStatusLabelDebugMode;
    private ToolStripMenuItem loadAShipmentFromXMLToolStripMenuItem;
    private ToolStripMenuItem goToPassportScreenToolStripMenuItem;
    private ToolStripMenuItem helpTopicsToolStripMenuItem;
    private ToolStripMenuItem functionKeyHelpToolStripMenuItem;
    private ToolStripMenuItem addSystemAccountNumberToolStripMenuItem;
    private ToolStripMenuItem forceReloadOfGuiDataListsToolStripMenuItem;
    private ToolStripMenuItem printerSetupToolStripMenuItem;
    private ToolStripLabel toolStripFedExLogo;
    private ToolStripMenuItem formAlignToolStripMenuItem;
    private ToolStripMenuItem fedExIntlAirWaybillToolStripMenuItem;
    private ToolStripMenuItem shippersDecForDGSDDGToolStripMenuItem;
    private System.Windows.Forms.Timer _autoCloseTimer;
    private ToolStripMenuItem sendClientMessageToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private BackgroundWorker _bwSoftwareUpdates;
    private System.Windows.Forms.Timer _smartPostAutoCloseTimer;
    private ToolStripMenuItem contactInformationToolStripMenuItem;
    private ToolStripMenuItem networkClientAdministrationToolStripMenuItem;
    private ToolStripMenuItem configureScaleToolStripMenuItem;
    private System.Timers.Timer _clientRegistrationTimer;
    private Panel panelMain;
    private ToolStripMenuItem oP900LGToolStripMenuItem;
    private BackgroundWorker _bwPurge;
    private ToolStripMenuItem oP900LLToolStripMenuItem;
    private ToolStripMenuItem passportToolStripMenuItem;
    private ToolStripMenuItem printDirectoryLabelToolStripMenuItem;
    private ToolStripMenuItem fontSizeToolStripMenuItem;
    private ToolStripMenuItem standardToolStripMenuItem;
    private ToolStripMenuItem largerToolStripMenuItem;
    private ToolStripMenuItem largestToolStripMenuItem;
    private ToolStripMenuItem ltlFreightTemplateToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem selfHelpMenuItem;
    private ToolStripMenuItem integrationHelpMenuItem;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripMenuItem EULAAcceptanceDetailMenuItem;
    private ToolStripMenuItem restartFedExShipManagerToolStripMenuItem;
    private ToolStripMenuItem manualReconcileToolStripMenuItem;
    private ToolStripMenuItem howCanWeImproveToolStripMenuItem;
    private ToolStripProgressBar toolStripDownloadProgress;
    private ToolStripMenuItem softwareManagementPortalToolStripMenuItem;
    private ToolStripMenuItem authenticateInstallationToolStripMenuItem;

    public event TopicDelegate GoToUrl;

    public event TopicDelegate CurrentAccountChanged;

    public event ShellForm.CurrentSystemPrefsChangedDelegate CurrentSystemPrefsChanged;

    public event TopicDelegate CurrentSenderCodeChanged;

    public event TopicDelegate UpdateStatusBar;

    public event TopicDelegate RePopulateReferenceCombos;

    public event EventHandler<ShutdownRequestEventArgs> RequestShutdown;

    public event EventHandler<HoldFileSingleEditEventArgs> HoldFileSingleEditEvent;

    public event EventHandler<HoldFileSingleEditEventArgs> FreightHoldFileSingleEditEvent;

    public event EventHandler PrinterSettingsChanged;

    public event EventHandler<AccountEventArgs> NewSystemAccount;

    public event EventHandler SystemTimeChanged;

    public AutoResetEvent StopCommThreadEvent => this._stopCommThread;

    public AutoResetEvent CommMessageAvailable => this._commMessageAvailable;

    private void ConfigureNetworkClientPrinterCheck()
    {
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager();
      if (configManager.KeyExist("SHIPNET2000/SETTINGS/NETWORKCLIENT/PRINTERLABEL"))
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "Setting Network client Form Settings from FSMConfig Keys.");
        string labelPrinter;
        if (!configManager.GetProfileString("SHIPNET2000/SETTINGS/NETWORKCLIENT", "PRINTERLABEL", out labelPrinter) || string.IsNullOrWhiteSpace(labelPrinter))
          labelPrinter = (string) null;
        string reportPrinter;
        if (!configManager.GetProfileString("SHIPNET2000/SETTINGS/NETWORKCLIENT", "PRINTERREPORT", out reportPrinter) || string.IsNullOrWhiteSpace(reportPrinter))
          reportPrinter = (string) null;
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "Network client Label Printer Key = ." + labelPrinter);
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "Network client Report Printer Key = ." + reportPrinter);
        Utility.SetupPrinters(GuiData.CurrentAccount.MeterNumber, GuiData.CurrentAccount.AccountNumber, (short) configManager.NetworkClientID, labelPrinter, reportPrinter);
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "Removing NetworkClient Printer keys after formsetting creation.");
        configManager.RemoveKey("SHIPNET2000/SETTINGS/NETWORKCLIENT/PRINTERLABEL");
        configManager.RemoveKey("SHIPNET2000/SETTINGS/NETWORKCLIENT/PRINTERREPORT");
      }
      else
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), "The network client printer config entries are missing.");
    }

    private void StartAdminService()
    {
      bool bVal = false;
      new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.SETTINGS).GetProfileBool("NetworkClient", "Remote", out bVal);
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "Calling StartTheEngine()");
      if (bVal)
        return;
      try
      {
        ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.StartTheEngine();
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "StartTheEngine() return serviceresponse.IsOperationOK = " + serviceResponse.IsOperationOk.ToString());
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, this.ToString(), "Exception starting Admin Service. Calling StartTheEngine(): " + ex.Message);
      }
    }

    private void VerifyShipServiceIsRunning()
    {
      bool isRunning = false;
      bool isLocal = false;
      GuiData.AppController.ShipEngine.IsShipServiceRunning(out isRunning, out isLocal);
      if (!isRunning & isLocal)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "Ship Service is NOT running.");
        ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.StartTheEngine();
        if (!serviceResponse.IsOperationOk)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, this.ToString(), string.Format("ShipEngine.StartTheEngine() returned Error Code: {0}. Error Message: {1}.", (object) serviceResponse.ErrorCode, (object) serviceResponse.ErrorMessage));
          int num = (int) MessageBox.Show("Could Not Start the FedEx Shipping Services.");
          Environment.Exit(0);
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "ShipEngine.StartTheEngine() started services successfully.");
      }
      else
      {
        if (isRunning)
          return;
        int num = (int) MessageBox.Show("The FedEx ShipEngine Services are not running on the remote machine. Please contact your system administrator.");
        Environment.Exit(0);
      }
    }

    private void InitializeLabelService() => GuiData.AppController.Initialize();

    public ShellForm()
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Verbose, FxLogger.AppCode_GUI, "ShellForm()", "Entering method");
      this.Cursor = Cursors.WaitCursor;
      this.CustomizeFontSize();
      this.InitializeComponent();
      this.CustomizeMenuFonts();
      this.CustomizeToolstrip();
      this.Cursor = Cursors.Default;
      SplashScreen.Show();
      this.StartAdminService();
      this.VerifyShipServiceIsRunning();
      this.ValidateNetworkClientVersionIfNetworkClient();
      this.RegisterNetworkClient();
      this.InitializeLabelService();
      SplashScreen.Status = GuiData.Languafier.Translate("LoadingCustomerDatabases");
      GuiData.RefreshRecipientComboDataTable();
      DeckController vc = new DeckController((Form) this, (ScrollableControl) this.panelMain);
      vc.ViewShown += new DeckController.DeckEventDelegate(this.main_ViewShown);
      vc.ViewShowing += new EventHandler<ViewShowingEventArgs>(this.main_ViewShowing);
      Utility.DeckBroker.AddDeck(vc);
      this.SetupCommandManager();
      this.SetupEvents();
      this.InitializeMidnightTimer();
      this.AddViews();
      this.StartReportProcessing();
      RemotePrintHelper.StartRemoting();
      this.InitializeShipmentTransactionHandler();
      this._holdLogic = new HoldFileLogic();
      this._twoFifty = new TwoFiftyTransactionTrafficCop();
      this._twoFifty.ReceivingInteractiveTransaction += new EventHandler<CancelEventArgs>(this.OnCanAcceptInput);
      IntegrationRemotingHelper.StartListening(new ProcessATransaction(this._twoFifty.ProcessTransaction), new ProcessATransaction(this._holdLogic.ProcessTransaction));
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Verbose, FxLogger.AppCode_GUI, "ShellForm()", "Leaving method");
    }

    private void InitializeShipmentTransactionHandler()
    {
      ServicePackageMapping.Initialize((Func<ServicePackageResponse>) (() => GuiData.AppController.ShipEngine.GetServicePackaging(new ServicePackageRequest())));
      ServicePackageMapping.InitializeGPR3((System.Func<PolicyGridEpicEnterpriseRequest, string>) (request =>
      {
        string offeringId;
        GuiData.AppController.ShipEngine.GetOfferingIDForNonDedicatedHandlingCode(request, out offeringId);
        return offeringId;
      }));
      TransactionCountryHelper.Initialize((Func<IEnumerable<CountryProfile>>) (() =>
      {
        if (GuiData.CurrentAccount != null && GuiData.CurrentAccount.is_GPR4_5_NEW_MARKET_EXPANSION_Initiative_Enabled)
        {
          CountryListResponse countryProfileList = GuiData.AppController.ShipEngine.GetCountryProfileList();
          if (countryProfileList != null && !countryProfileList.HasError && countryProfileList.CountryProfileList != null)
            return (IEnumerable<CountryProfile>) countryProfileList.CountryProfileList;
        }
        return Enumerable.Empty<CountryProfile>();
      }));
      EngineWaldoProvider provider = new EngineWaldoProvider(GuiData.AppController.ShipEngine);
      WaldoHelper.SetProvider((IWaldoProvider) provider);
      SpecialServiceHelper.SetProvider((IWaldoProvider) provider);
    }

    private void CustomizeToolstrip()
    {
      this.toolStripMain.Renderer = (ToolStripRenderer) new CafeToolstripRenderer(this._colorTable);
    }

    private void CustomizeMenuFonts()
    {
      Utility.UpdateToolstripFonts((ToolStrip) this.menuStripMain);
      Utility.UpdateToolstripFonts(this.toolStripMain);
      Utility.UpdateToolstripFonts((ToolStrip) this.statusStripMain);
    }

    private void CustomizeFontSize()
    {
      string s;
      if (GuiData.ConfigManager.GetProfileString("SHIPNET2000/GUI/SETTINGS", "FONTSIZE", out s) && int.TryParse(s, out this._fontIndex))
      {
        float emSize;
        switch (this._fontIndex)
        {
          case 1:
            emSize = 10f;
            break;
          case 2:
            emSize = 12f;
            break;
          default:
            emSize = 8f;
            break;
        }
        int num = this._fontIndex * 2;
        this.Font = new Font(this.Font.FontFamily, emSize);
        FontHelper.DefaultFont = this.Font;
        HeaderText.FontSize += num;
        using (Font menuFont = SystemFonts.MenuFont)
          FontHelper.DefaultMenuFont = new Font(menuFont.FontFamily, menuFont.Size + (float) num);
      }
      else
      {
        FontHelper.DefaultFont = this.Font;
        FontHelper.DefaultMenuFont = SystemFonts.MenuFont;
      }
    }

    private void StartReportProcessing()
    {
      try
      {
        this.reportProcessing = Process.Start(new ProcessStartInfo(Path.Combine(InstallLocations.Instance.BinDirectory, "ReportProcessing.exe")));
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_AppLogic, "ShellForm.StartReportProcessing", "Could not start the reporting process. " + ex.ToString());
      }
    }

    private void StopReportProcessing()
    {
      try
      {
        if (this.reportProcessing == null)
          return;
        if (this.reportProcessing.CloseMainWindow())
          this.reportProcessing.WaitForExit(1500);
        if (this.reportProcessing.HasExited)
          return;
        this.reportProcessing.Kill();
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_AppLogic, "ShellForm.StopReportProcessing", "Could not stop the reporting process. " + ex.ToString());
      }
    }

    private void StartIntegration()
    {
      this.GenerateIntegrationDataFiles();
      this._launcher = new IntegrationLauncher();
      bool vbaIntegration = this.GetVbaIntegration();
      if (!vbaIntegration && !this._launcher.FXI.IsInstalled())
      {
        if (this._launcher.FXIA.Launch())
          this.LoadIntegrationMenu(true);
        else
          this.LoadIntegrationMenu(false);
      }
      else if ((vbaIntegration || GuiData.ConfigManager.IsNetworkClient) && this._launcher.FXI.Launch())
      {
        this._fxiStarted = true;
        if (GuiData.ConfigManager.IsNetworkClient && !vbaIntegration)
          GuiData.ConfigManager.SetProfileValue("SHIPNET2000/GUI/SETTINGS/INTEGRATION", "VBAINTEGRATION", (object) "Y");
        this.LoadIntegrationMenu(false);
      }
      GuiData.EnableIntegration(true);
      this.IntegrationHook(120);
    }

    private void GenerateIntegrationDataFiles()
    {
      Account masterAccount = this.GetMasterAccount();
      if (masterAccount == null)
        return;
      string culture;
      if (!GuiData.ConfigManager.GetProfileString("SHIPNET2000/GUI/SETTINGS", "LANGUAGE", out culture))
        culture = "en-US";
      new IntegrationDataGenerator(masterAccount, GuiData.AppController.ShipEngine).GenerateFiles(culture);
    }

    private void RegisterNetworkClient()
    {
      ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.RegisterNetworkClient();
      if (serviceResponse.ErrorCode != 1)
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, this.ToString(), "RegisterNetworkClient Failed Error=" + serviceResponse.ErrorCode.ToString());
      if (GuiData.ConfigManager.IsNetworkClient && GuiData.ConfigManager.NetworkClientID == -1)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, GuiData.Languafier.TranslateError(70000), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        this.Close();
      }
      string port;
      if (!GuiData.ConfigManager.IsNetworkClient || !GuiData.AppController.ShipEngine.GetFastPojoPort(out port).IsOperationOk || string.Equals(port, GuiData.ConfigManager.FastPojoPort, StringComparison.Ordinal))
        return;
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.RegisterNetworkClient", "Updating fast port from " + GuiData.ConfigManager.FastPojoPort + " to " + port);
      GuiData.ConfigManager.SetProfileValue("SHIPNET2000/SETTINGS/SYSTEM", "FASTPOJOPORT", (object) port);
    }

    private void main_ViewShowing(object sender, ViewShowingEventArgs e)
    {
      e.Cancel = GuiData.CurrentAccount != null && GuiData.CurrentAccount.IsCashOnly && !"CashBlock".Equals(e.View, StringComparison.InvariantCultureIgnoreCase);
    }

    private void DeselectTabs()
    {
      foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) this.toolStripMain.Items)
      {
        if (toolStripItem is ToolStripButton)
          ((ToolStripButton) toolStripItem).Checked = false;
      }
    }

    private void main_ViewShown(object sender, DeckEventArgs args)
    {
      this.toolStripMain.Visible = args.View != "LoginView" && args.View != "CashBlock";
      this.menuStripMain.Visible = args.View != "LoginView";
      try
      {
        ToolStripItem selected = (ToolStripItem) null;
        foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) this.toolStripMain.Items)
        {
          if (args.View == toolStripItem.Tag as string)
          {
            selected = toolStripItem;
            break;
          }
        }
        if (selected == null)
          this.DeselectTabs();
        else
          this.UpdatePeerChecks(selected);
      }
      catch (Exception ex)
      {
      }
      IntegrationScreens integrationScreens = !(args.View == "ShipView") ? (!(args.View == "TrackView") ? (!(args.View == "ReportsModule") ? (!(args.View == "CloseView") ? (!(args.View == "PassportView") ? (!(args.View == "InboundView") ? (!(args.View == "SvcBullBrdView") ? (!(args.View == "SmartPostModule") ? (!(args.View == "BillOfLadingView") ? (!(args.View == "FreightView") ? IntegrationScreens.UnknownScreen : IntegrationScreens.FreightView) : IntegrationScreens.BillOfLadingView) : IntegrationScreens.SmartPostView) : IntegrationScreens.ServiceBulletinBoardView) : IntegrationScreens.InboundReceiveView) : IntegrationScreens.PassportView) : IntegrationScreens.CloseView) : IntegrationScreens.ReportsView) : IntegrationScreens.TrackingView) : IntegrationScreens.ShipView;
      if (integrationScreens == IntegrationScreens.UnknownScreen)
        return;
      this.IntegrationHook(118, ((int) integrationScreens).ToString());
    }

    private void ShellForm_Load(object sender, EventArgs e)
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Verbose, FxLogger.AppCode_GUI, nameof (ShellForm_Load), "Entering method.");
      Application.Idle += new EventHandler(this.Application_Idle);
      this.CommCallback = new ShellForm.CommStatusCallback(this.CommMsg);
      this._commStatus = new CommStatus((Delegate) this.CommCallback);
      this._commStatus.InitCommStatusMessages();
      ThreadPool.QueueUserWorkItem(new WaitCallback(ShellForm.CommMessageThread), (object) this);
      if (!Utility.IsRegistered)
      {
        FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
        string str;
        configManager.GetProfileString("SETTINGS", "METER", out str);
        if (string.IsNullOrEmpty(str) && Utility.SoftwareOnly)
        {
          SplashScreen.Hide();
          Account masterAccount = this.GetMasterAccount();
          if (masterAccount != null)
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, nameof (ShellForm_Load), "SWO Registered was false even though we have a master meter!");
            configManager.SetProfileValue("SETTINGS", "Registered", (object) "Y");
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, nameof (ShellForm_Load), "Reset Registered flag to 'Y', continuing startup");
            configManager.SetProfileValue("SETTINGS", "METER", (object) masterAccount.MeterNumber);
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, nameof (ShellForm_Load), "Reset current meter to master '" + masterAccount.MeterNumber + "'");
          }
          else
          {
            this._softwareProcess.StartInfo.FileName = configManager.InstallLocs.BinDirectory + "\\FSMRegistration.exe";
            this._softwareProcess.Exited += new EventHandler(this.SoftwareProcess_Exited);
            this._softwareProcess.Start();
            do
            {
              Thread.Sleep(3000);
            }
            while (!this._softwareProcess.HasExited);
            if (!this._bSoftwareOnlySuccess || this._softwareProcess.ExitCode <= 0)
            {
              if (!this._bSoftwareOnlySuccess)
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, this.ToString(), "Software registration failed, no meter found in config.");
              else
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, this.ToString(), "Software registration failed, process returned error code " + this._softwareProcess.ExitCode.ToString());
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, this.ToString(), "Software registration failed. Exiting...");
              Application.Exit();
              return;
            }
          }
        }
        else
        {
          SplashScreen.Hide();
          int num = (int) Utility.DisplayError(GuiData.Languafier.Translate("d28813"));
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, this.ToString(), "Unregistered software only system already has a meter assigned. Exiting...");
          Application.Exit();
          return;
        }
      }
      Error error = new Error();
      DataTable output;
      if (GuiData.AppController.ShipEngine.GetDataList((object) null, GsmDataAccess.ListSpecification.AccountList, out output, new List<GsmFilter>()
      {
        new GsmFilter("MasterMeterInd", "=", (object) "1")
      }, (List<GsmSort>) null, (List<string>) null, error) != 1)
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm.Load", "Error retrieving account list for master sanity check " + error?.ToString());
      if (output != null && output.Rows.Count > 1)
      {
        int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateMessage("MultipleMasterMeters"));
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, this.ToString(), "Multiple master meters detected, exiting...");
        Application.Exit();
      }
      else
      {
        this.RefreshReferences();
        this.StartIntegration();
        this.InitializeSystemAccount();
        ReportVariables.DateFormat = GuiData.CurrentAccount.DateFormat.Equals("mm/dd/yyyy") ? "MM/dd/yyyy" : "dd/MMMM/yyyy";
        this.SetSelectedLanguage();
        this.SetSelectedFontSize();
        this.LoadSystemAccountMenu(true);
        this.InitializeAddressChecker();
        try
        {
          GuiData.ShipEngineEventSink = new ShipEngineEventSink();
          GuiData.ShipEngineEventSink.MainForm = (Form) this;
          GuiData.AppController.ShipEngine.RegisterForUnsolicitedServerMessages(GuiData.CurrentAccount);
          GuiData.AppController.Owner = (IWin32Window) this;
        }
        catch (Exception ex)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), "Exception while registering client callback with server: " + ex.Message);
        }
        if (Utility.SoftwareOnly)
        {
          long lval = 0;
          FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
          configManager.GetProfileLong("Settings", "SOStartupCountSinceBackup", out lval);
          configManager.SetProfileValue("Settings", "SOStartupCountSinceBackup", (object) (lval + 1L));
        }
        if (Utility.IsSoftwareOnly || Utility.DevMode)
        {
          this.installSoftwareUpdateToolStripMenuItem.Enabled = true;
          this.configureScaleToolStripMenuItem.Visible = true;
        }
        if (GuiData.CurrentAccount != null)
          this.DoStartupStuff();
        if (GuiData.ConfigManager.IsNetworkClient)
        {
          this._clientRegistrationTimer.Start();
          this.addSystemAccountNumberToolStripMenuItem.Visible = false;
          this.downloadToolStripMenuItem.Visible = false;
          this.systemSettingsToolStripMenuItem.Visible = false;
          this.shippingProfilesToolStripMenuItem.Visible = false;
          this.customizeFieldsToolStripMenuItem.Visible = false;
          this.uploadToolStripMenuItem.Visible = false;
          this.installSoftwareUpdateToolStripMenuItem.Visible = false;
          this.downloadToolStripMenuItem.Visible = false;
          this.testCommunicationsToolStripMenuItem.Visible = false;
        }
        if (GuiData.ConfigManager.IsNetworkServer)
          this.networkClientAdministrationToolStripMenuItem.Visible = true;
        else
          this.sendClientMessageToolStripMenuItem.Visible = false;
        if (Utility.IsSoftwareOnly)
          this._bwSoftwareUpdates.RunWorkerAsync();
        this.Show();
        SplashScreen.Close();
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Verbose, FxLogger.AppCode_GUI, nameof (ShellForm_Load), "Splash Screen Closed.");
        this.AutoActivateLastActiveProfile("OnCafeRestart");
        this._dgWarnDialog = (DgWarning) null;
        if (!GuiData.CurrentAccount.RequireUserLogin)
        {
          this._dgWarnDialog = new DgWarning();
          this._dgWarnDialog.Show((IWin32Window) this);
          this._dgWarnDialog.BringToFront();
          this.DisplayTaxDisclaimer();
        }
        string str;
        GuiData.ConfigManager.GetProfileString("SHIPNET2000/GUI/SETTINGS/USERPROMPTS", "DONTSHOW800600MSG", out str);
        if (SystemInformation.PrimaryMonitorSize.Width == 800 && SystemInformation.PrimaryMonitorSize.Height == 600 && str != "Y")
        {
          using (CustomMessageBox customMessageBox = new CustomMessageBox(GuiData.Languafier.Translate("ScreenResMsg"), GuiData.Languafier.Translate("ScreenResTitle")))
          {
            if (customMessageBox.ShowDialog() == DialogResult.OK)
            {
              if (customMessageBox.DontShowChecked)
                GuiData.ConfigManager.SetProfileValue("SHIPNET2000/GUI/SETTINGS/USERPROMPTS", "DONTSHOW800600MSG", (object) "Y");
            }
          }
        }
        BackupRestoreDataAccess.RegisterEngine((IBackupRestoreEngine) new ControllerBackupRestoreEngine(GuiData.AppController));
        if (!GuiData.ConfigManager.IsNetworkClient)
        {
          ShellForm._backupRestoreModule.CheckForRequireBackup();
          this.CheckUrsa();
          this.CheckUploadStatus();
          this.CheckZeroRates();
        }
        else
        {
          this.MigrateValidatorProfiles();
          this.ConfigureNetworkClientPrinterCheck();
        }
        if (this._dgWarnDialog != null)
        {
          this.PromptForSvcBulletinBoard(GuiData.CurrentAccount);
          this.DisplayQueuedMessages(GuiData.CurrentAccount);
          this._dgWarnDialog.Focus();
        }
        if (!Utility.IsNetworkClient)
        {
          bool bVal;
          GuiData.ConfigManager.GetProfileBool("SHIPNET2000/GUI/SETTINGS", "PRINT_CLOSE_REPORTS_AT_STARTUP", out bVal, true);
          if (bVal)
            this.OnPrintEodOrInvoiceReports((object) this, EventArgs.Empty);
        }
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Verbose, FxLogger.AppCode_GUI, nameof (ShellForm_Load), "Leaving method.");
      }
    }

    private Account GetMasterAccount()
    {
      Account output;
      return GuiData.AppController.ShipEngine.Retrieve<Account>(new Account()
      {
        IsMaster = true
      }, out output, out Error _) == 1 ? output : (Account) null;
    }

    private void SetSelectedLanguage()
    {
      this.SetSelectedMenuItem(this.languageToolStripMenuItem.DropDownItems, Application.CurrentCulture.Name);
    }

    private void SetSelectedFontSize()
    {
      this.SetSelectedMenuItem(this.fontSizeToolStripMenuItem.DropDownItems, this._fontIndex.ToString());
    }

    private void SetSelectedMenuItem(ToolStripItemCollection items, string tag)
    {
      foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) items)
      {
        if (toolStripItem is ToolStripMenuItem)
          ((ToolStripMenuItem) toolStripItem).Checked = tag.Equals(toolStripItem.Tag);
      }
    }

    private void PromptForSvcBulletinBoard(Account acct)
    {
      if (Utility.IsNetworkClient)
        return;
      try
      {
        DataTable output;
        GuiData.AppController.ShipEngine.GetDataList((object) new SvcBullBrd()
        {
          ReadInd = false,
          DeletedInd = false,
          AccountNumber = acct.AccountNumber,
          MeterNumber = acct.MeterNumber
        }, GsmDataAccess.ListSpecification.SvcBullBrd_List_Not_Deleted, out output, new Error());
        int num1 = 0;
        foreach (DataRow row in (InternalDataCollectionBase) output.Rows)
        {
          if (row["ReadInd"] != null && bool.FalseString.Equals(row["ReadInd"].ToString(), StringComparison.InvariantCultureIgnoreCase))
            ++num1;
        }
        if (num1 <= 0)
          return;
        int num2 = (int) MessageBox.Show((IWin32Window) this, string.Format(GuiData.Languafier.TranslateMessage(38550), (object) num1), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        Utility.DeckBroker[this.Name].ShowView("SvcBullBrdView");
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm::PromptForSvcBulletinBoard", "Caught exception " + ex.ToString());
      }
    }

    private void DisplayQueuedMessages(Account acct)
    {
      try
      {
        MessageQueueEntry output;
        while (GuiData.AppController.ShipEngine.Retrieve<MessageQueueEntry>(new MessageQueueEntry()
        {
          MeterNumber = acct.MeterNumber,
          AccountNumber = acct.AccountNumber
        }, out output, out Error _) == 1)
        {
          int num = (int) MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate(output.LanguafierString), string.Empty, MessageBoxButtons.OK);
          GuiData.AppController.ShipEngine.Delete<MessageQueueEntry>(output);
        }
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm::ShowQueuedMessages", "Caught exception " + ex.ToString());
      }
    }

    private void UpdatePeerChecks(ToolStripItem selected)
    {
      if (selected == null || selected.Owner == null)
        return;
      foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) selected.Owner.Items)
      {
        if (toolStripItem is ToolStripMenuItem)
          ((ToolStripMenuItem) toolStripItem).Checked = toolStripItem == selected;
        else if (toolStripItem is ToolStripButton)
          ((ToolStripButton) toolStripItem).Checked = toolStripItem == selected;
      }
    }

    private void SoftwareProcess_Exited(object sender, EventArgs e)
    {
      string str;
      new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI).GetProfileString("SETTINGS", "METER", out str);
      this._bSoftwareOnlySuccess = !string.IsNullOrEmpty(str);
    }

    private void CheckUrsa() => this.CheckUrsa(true);

    private void CheckUrsa(bool bDownload)
    {
      try
      {
        ShipView plugin = (ShipView) Utility.DeckBroker.GetDeck(this.Name).GetPlugin("ShipView");
        try
        {
          int num = GuiData.AppController.ShipEngine.IsURSAValid();
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.CheckUrsa", "IsURSAValid returned " + num.ToString());
          switch (num)
          {
            case -1:
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.CheckUrsa", "IsURSAValid says the URSA file is corrupt.");
              break;
            case 0:
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, "ShellForm.CheckUrsa", "URSA valid, hiding message");
              this.toolStripStatusUrsa.Visible = false;
              return;
          }
          SplashScreen.Close();
          if (bDownload && MessageBox.Show(GuiData.Languafier.Translate("InvalidUrsaDownloadNow"), GuiData.Languafier.Translate("InvalidUrsa"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
          {
            plugin.DownloadUrsa();
          }
          else
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, "ShellForm.CheckUrsa", "URSA invalid, display message");
            this.toolStripStatusUrsa.Visible = true;
          }
        }
        catch (Exception ex)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_GUI, "ShellForm.CheckUrsa", "IsURSAValid threw an exception. " + ex.ToString());
          this.toolStripStatusUrsa.Visible = true;
        }
        finally
        {
          ((ShipDetails) plugin.ShipDetails).UrsaExpiredVisible = this.toolStripStatusUrsa.Visible;
        }
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_GUI, "ShellForm.CheckUrsa", "Utility.DeckBroker.GetDeck threw an exception in CheckUrsa. " + ex.ToString());
      }
    }

    private void InitializeSystemAccount()
    {
      SplashScreen.Status = GuiData.Languafier.Translate("LoadMeterAccount");
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      string str1;
      configManager.GetProfileString("SETTINGS", "METER", out str1);
      string str2;
      configManager.GetProfileString("SETTINGS", "ACCOUNT", out str2);
      Account output;
      if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2) && GuiData.AppController.ShipEngine.Retrieve<Account>(new Account()
      {
        MeterNumber = str1,
        AccountNumber = str2
      }, out output, out Error _) == 1)
      {
        this.SwitchSystemAccount(output, true);
      }
      else
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), "Could not load meter/account.");
        SplashScreen.Hide();
        ChooseSystemAccount chooseSystemAccount = new ChooseSystemAccount();
        chooseSystemAccount.ShowIcon = true;
        chooseSystemAccount.StartPosition = FormStartPosition.CenterScreen;
        do
          ;
        while (chooseSystemAccount.ShowDialog((IWin32Window) this) != DialogResult.OK);
        this.InitializeSystemAccount();
      }
    }

    private void AddViews()
    {
      SplashScreen.Status = GuiData.Languafier.Translate("LoadMainViews");
      this.LoadPlugins();
      this.LoadShipModule();
      SplashScreen.Status = GuiData.Languafier.Translate("LoadPlugins");
      this.LoadCashBlockModule();
      this.LoadLoginModule();
      if (Utility.IsNetworkClient)
        return;
      this.LoadServiceBulletinBoardModule();
    }

    private void LoadDefaultRecipientColumnsPrefs(SystemPrefs prefs)
    {
      prefs.LOptionalRecipientFields = new List<long>()
      {
        4L,
        5L,
        2L
      };
    }

    private void LoadDefaultReferencePreferenceLabels(SystemPrefs sysPrefs)
    {
      if (sysPrefs == null)
        return;
      sysPrefs.AddRef1ScrDsply = GuiData.Languafier.Translate("AddRef1Label") ?? string.Empty;
      sysPrefs.AddRef2ScrDsply = GuiData.Languafier.Translate("AddRef2Label") ?? string.Empty;
      sysPrefs.AddRef3ScrDsply = GuiData.Languafier.Translate("AddRef3Label") ?? string.Empty;
      sysPrefs.CustRefScrDsply = GuiData.Languafier.Translate("CustomerRefLabel") ?? string.Empty;
      sysPrefs.DeptScrDsply = GuiData.Languafier.Translate("DepartmentLabel") ?? string.Empty;
      sysPrefs.RecipIDScrDsply = GuiData.Languafier.Translate("RecipientIdLabel") ?? string.Empty;
    }

    public void SetSystemAccount(Account newAccount)
    {
      if (newAccount == null)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, nameof (SetSystemAccount), "Account object is null.");
      }
      else
      {
        foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection) this.activeSystemAccountToolStripMenuItem.DropDownItems)
          dropDownItem.Checked = dropDownItem.Text.StartsWith(newAccount.MeterNumber);
        FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
        string str1;
        if (configManager.GetProfileString("SETTINGS", "LANGUAGE", out str1) && newAccount.Culture != str1)
        {
          newAccount.Culture = str1;
          GuiData.AppController.ShipEngine.Modify<Account>(newAccount);
        }
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        if (GuiData.CurrentAccount == null || GuiData.CurrentAccount.MeterNumber != newAccount.MeterNumber)
        {
          configManager.SetProfileValue("Settings", "METER", (object) newAccount.MeterNumber);
          configManager.SetProfileValue("Settings", "ACCOUNT", (object) newAccount.AccountNumber);
          flag1 = true;
          this.toolStripStatusLabelDevMode.Visible = Utility.DevMode;
        }
        if (GuiData.CurrentAccount == null || GuiData.CurrentAccount.MeterNumber != newAccount.MeterNumber || GuiData.CurrentAccount.AccountNumber != newAccount.AccountNumber)
          flag2 = true;
        if (GuiData.CurrentAccount == null || GuiData.CurrentAccount.DefaultSenderCode != newAccount.DefaultSenderCode)
          flag3 = true;
        if (this.CurrentAccountChanged != null)
        {
          AccountEventArgs args = new AccountEventArgs();
          args.OldAccount = GuiData.CurrentAccount;
          args.NewAccount = newAccount;
          GuiData.CurrentAccount = newAccount;
          this.CurrentAccountChanged((object) this, (EventArgs) args);
        }
        else
          GuiData.CurrentAccount = newAccount;
        if (flag1)
        {
          string str2 = configManager.SoftwareVersion;
          if (!string.IsNullOrEmpty(str2))
            str2 = str2.Substring(str2.Length - 4);
          string str3 = !Utility.IsNetworkClient ? GuiData.Languafier.Translate("SoftwareName") : GuiData.Languafier.Translate("NetworkClientName");
          StringBuilder stringBuilder = new StringBuilder();
          bool vbaIntegration = this.GetVbaIntegration();
          if (!vbaIntegration && this.FXIAUserProfileCount != 0)
          {
            stringBuilder.AppendFormat("{0} | v.{1}-a | {2}", (object) str3, (object) str2, (object) (newAccount.MeterNumber + " - " + newAccount.Description));
            this.Text = stringBuilder.ToString();
            if (GuiData.IsAnyFXIAProfileInUse)
              this.AutoActivateLastActiveProfile("OnMeterSwitch");
          }
          else if (vbaIntegration && this._fxiStarted)
          {
            stringBuilder.AppendFormat("{0} | v.{1}-i | {2}", (object) str3, (object) str2, (object) (newAccount.MeterNumber + " - " + newAccount.Description));
            this.Text = stringBuilder.ToString();
          }
          else
          {
            stringBuilder.AppendFormat("{0} | v.{1} | {2}", (object) str3, (object) str2, (object) (newAccount.MeterNumber + " - " + newAccount.Description));
            this.Text = stringBuilder.ToString();
          }
        }
        if (flag2)
        {
          SystemPrefs output;
          if (GuiData.AppController.ShipEngine.Retrieve<SystemPrefs>(new SystemPrefs()
          {
            FedExAcctNbr = newAccount.AccountNumber,
            MeterNumber = newAccount.MeterNumber
          }, out output, out Error _) == 0 && output == null)
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "Could not load SystemPrefs for current system & account. Will commit new object.");
            output = new SystemPrefs();
            this.LoadDefaultRecipientColumnsPrefs(output);
            this.LoadDefaultReferencePreferenceLabels(output);
            output.MeterNumber = newAccount.MeterNumber;
            output.FedExAcctNbr = newAccount.AccountNumber;
            GuiData.AppController.ShipEngine.Insert<SystemPrefs>(output);
          }
          else if (output != null && string.IsNullOrEmpty(output.AddRef1ScrDsply))
            this.LoadDefaultReferencePreferenceLabels(output);
          CurrentSystemPrefsEventArgs args = new CurrentSystemPrefsEventArgs();
          args.OldSystemPrefs = GuiData.CurrentSystemPrefs;
          args.NewSystemPrefs = output;
          GuiData.CurrentSystemPrefs = output;
          if (this.CurrentSystemPrefsChanged != null)
          {
            long lval = -1;
            configManager.GetProfileLong("Settings", "CountrySortBy", out lval);
            args.OldCountrySortBy = (int) lval;
            args.NewCountrySortBy = (int) lval;
            this.CurrentSystemPrefsChanged((object) this, args);
          }
        }
        if (flag3 && this.CurrentSenderCodeChanged != null)
          this.CurrentSenderCodeChanged((object) this, new EventArgs());
        Utility.DeckBroker[this.Name].ShowView("ShipView");
        this.utilitiesToolStripMenuItem.DropDownItems["fedExAddressCheckerToolStripMenuItem"].Visible = Utility.AddressCheckerEnabled;
        if (!Utility.AddressCheckerBatchEnabled)
          ((ToolStripDropDownItem) this.utilitiesToolStripMenuItem.DropDownItems["fedExAddressCheckerToolStripMenuItem"]).DropDownItems.Remove((ToolStripItem) this.addrChkrBatchMenuItem);
        this.InitializeIDDCloseTimer(newAccount);
      }
    }

    private void LoadPlugins()
    {
      this.LoadInboundModule();
      this.LoadDatabaseMenu();
      this.LoadMainToolStripPlugins();
      this.LoadFreightTemplateMenu();
      SplashScreen.Status = string.Empty;
    }

    private void LoadInboundModule()
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.LoadInboundModule", "Entering Method");
      this.AddPluginsToToolStrip((ToolStripDropDownItem) this.inboundToolStripMenuItem, PluginManager.GetPlugins(GuiPluginPoint.InboundMenu));
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.LoadInboundModule", "Leaving Method");
    }

    private void LoadMainToolStripPlugins()
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.LoadMainToolStripPlugins", "Entering Method");
      this.AddPluginsToToolStrip(this.toolStripMain, PluginManager.GetPlugins(GuiPluginPoint.MainButtonPanel));
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.LoadMainToolStripPlugins", "Leaving Method");
      this.LoadHTMLModule();
    }

    private void LoadFreightTemplateMenu()
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.LoadFreightTemplateMenu", "Entering Method");
      this.AddPlugin(PluginManager.GetNamedGuiPlugin("LtlFreightTemplate"), (ToolStripItem) this.ltlFreightTemplateToolStripMenuItem);
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.LoadFreightTemplateMenu", "Leaving Method");
    }

    private void AddPluginsToToolStrip(ToolStripDropDownItem toolStrip, List<GuiPluginItem> plugins)
    {
      try
      {
        if (plugins == null)
          return;
        foreach (GuiPluginItem plugin in plugins)
        {
          if ((plugin.AllowedClient == AllowedClientType.Any || Utility.IsNetworkClient && plugin.AllowedClient == AllowedClientType.Network ? 1 : (Utility.IsNetworkClient ? 0 : (plugin.AllowedClient == AllowedClientType.Local ? 1 : 0))) != 0)
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, "ShellForm.AddPluginToToolStrip(GuiPluginItem,ToolStripMenuItem)", "Loading Plugin " + plugin.ToString());
            switch (plugin)
            {
              case GuiPlaceholderPlugin _:
                toolStrip.DropDownItems.Add((ToolStripItem) new ToolStripSeparator());
                break;
              case GuiPluginSet _:
                GuiPluginSet guiPluginSet = (GuiPluginSet) plugin;
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.AddPluginToToolStrip(GuiPluginItem,ToolStripMenuItem)", "Loading PluginPoint " + guiPluginSet.PluginPoint.ToString());
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                this.AddPlugin(plugin, (ToolStripItem) toolStripMenuItem);
                toolStrip.DropDownItems.Add((ToolStripItem) toolStripMenuItem);
                this.AddPluginsToToolStrip((ToolStripDropDownItem) toolStripMenuItem, guiPluginSet.GetPlugins());
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.AddPluginToToolStrip(GuiPluginItem,ToolStripMenuItem)", "Completed PluginPoint " + guiPluginSet.PluginPoint.ToString());
                break;
              default:
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                this.AddPlugin(plugin, (ToolStripItem) menuItem);
                toolStrip.DropDownItems.Add((ToolStripItem) menuItem);
                break;
            }
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, "ShellForm.AddPluginToToolStrip(GuiPluginItem,ToolStripMenuItem)", "Completed Plugin " + plugin.ToString());
          }
        }
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm.AddPluginToToolStrip(GuiPluginItem,ToolStripMenuItem)", ex.ToString());
      }
    }

    private void AddPluginsToToolStrip(ToolStrip toolStrip, List<GuiPluginItem> plugins)
    {
      try
      {
        if (plugins == null)
          return;
        foreach (GuiPluginItem plugin1 in plugins)
        {
          if ((plugin1.AllowedClient == AllowedClientType.Any || Utility.IsNetworkClient && plugin1.AllowedClient == AllowedClientType.Network ? 1 : (Utility.IsNetworkClient ? 0 : (plugin1.AllowedClient == AllowedClientType.Local ? 1 : 0))) != 0)
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, "ShellForm.AddPluginToToolStrip(GuiPluginItem,ToolStrip)", "Loading Plugin " + plugin1.ToString());
            switch (plugin1)
            {
              case GuiPlaceholderPlugin _:
                toolStrip.Items.Add((ToolStripItem) new ToolStripSeparator());
                break;
              case GuiPluginSet _:
                GuiPluginSet guiPluginSet = (GuiPluginSet) plugin1;
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.AddPluginToToolStrip(GuiPluginItem,ToolStrip)", "Loading PluginPoint " + guiPluginSet.PluginPoint.ToString());
                if (guiPluginSet.PluginPoint == GuiPluginPoint.IDDMenu)
                {
                  List<GuiPluginItem> plugins1 = guiPluginSet.GetPlugins();
                  GuiPluginItem plugin2 = plugins1.First<GuiPluginItem>();
                  List<GuiPluginItem> list = plugins1.Skip<GuiPluginItem>(1).ToList<GuiPluginItem>();
                  ToolStripSplitButton stripSplitButton = new ToolStripSplitButton();
                  this.AddPlugin(plugin2, (ToolStripItem) stripSplitButton);
                  toolStrip.Items.Add((ToolStripItem) stripSplitButton);
                  this.AddPluginsToToolStrip((ToolStripDropDownItem) stripSplitButton, list);
                }
                else
                {
                  ToolStripDropDownButton stripDropDownButton = new ToolStripDropDownButton();
                  stripDropDownButton.MouseDown += new MouseEventHandler(this.toolStripDropDownButton_MouseDown);
                  this.AddPlugin(plugin1, (ToolStripItem) stripDropDownButton);
                  toolStrip.Items.Add((ToolStripItem) stripDropDownButton);
                  this.AddPluginsToToolStrip((ToolStripDropDownItem) stripDropDownButton, guiPluginSet.GetPlugins());
                }
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.AddPluginToToolStrip(GuiPluginItem,ToolStrip)", "Completed PluginPoint " + guiPluginSet.PluginPoint.ToString());
                break;
              default:
                ToolStripButton menuItem = new ToolStripButton();
                this.AddPlugin(plugin1, (ToolStripItem) menuItem);
                toolStrip.Items.Add((ToolStripItem) menuItem);
                break;
            }
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, "ShellForm.AddPluginToToolStrip(GuiPluginItem,ToolStrip)", "Completed Plugin " + plugin1.ToString());
          }
        }
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm.AddPluginToToolStrip(GuiPluginItem,ToolStrip)", ex.ToString());
      }
    }

    private void AddPlugin(GuiPluginItem plugin, ToolStripItem menuItem)
    {
      if (!string.IsNullOrEmpty(plugin.TextCode))
      {
        menuItem.Text = GuiData.Languafier.Translate(plugin.TextCode);
        if (string.IsNullOrEmpty(menuItem.Text))
          menuItem.Text = plugin.TextCode;
      }
      if (!string.IsNullOrEmpty(plugin.ToolTipCode))
        menuItem.ToolTipText = GuiData.Languafier.Translate(plugin.ToolTipCode);
      if (!string.IsNullOrEmpty(plugin.ControlName))
        menuItem.Name = plugin.ControlName;
      if (!string.IsNullOrEmpty(plugin.GetMenuItemImageMethod))
      {
        try
        {
          System.Type componentType = plugin.GetComponentType();
          if (componentType != (System.Type) null)
          {
            MethodInfo method = componentType.GetMethod(plugin.GetMenuItemImageMethod, BindingFlags.Static | BindingFlags.Public);
            if (method != (MethodInfo) null)
            {
              if (method.Invoke((object) null, (object[]) null) is Image image)
              {
                menuItem.Image = image;
                menuItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
              }
            }
          }
        }
        catch (Exception ex)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm.AddPlugin invoking GetMenuImage", ex.ToString());
        }
      }
      switch (plugin)
      {
        case GuiMainPanelPlugin _:
          menuItem.Tag = (object) ((GuiMainPanelPlugin) plugin).DeckName;
          this._listPluginHandlers.Add((AbstractToolStripItemPluginHandler) new MainPanelPluginHandler(plugin as GuiMainPanelPlugin, Utility.DeckBroker.GetDeck(this.Name), menuItem, (Control) this));
          break;
        case GuiDialogPlugin _:
          this._listPluginHandlers.Add((AbstractToolStripItemPluginHandler) new DialogPluginHandler(plugin as GuiDialogPlugin, menuItem, (Control) this));
          break;
      }
    }

    private void LoadPassportModule()
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.LoadPassportModule", "Entering Method");
      this.AddPluginsToToolStrip((ToolStripDropDownItem) this.passportToolStripMenuItem, PluginManager.GetPlugins(GuiPluginPoint.PassportMenu));
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.LoadPassportModule", "Leaving Method");
      this.passportToolStripMenuItem.Visible = true;
    }

    private void HandlePassportModule()
    {
      if (GuiData.CurrentAccount == null)
        return;
      if (GuiData.CurrentAccount.PassPortEnabled != 0)
      {
        if (!this._passportAllReadyLoaded)
        {
          this.LoadPassportModule();
          this._passportAllReadyLoaded = true;
        }
        else
          this.passportToolStripMenuItem.Visible = true;
      }
      else
      {
        this.passportToolStripMenuItem.Visible = false;
        if (!Utility.DeckBroker[this.Name].IsViewActive("PassportView"))
          return;
        Utility.DeckBroker[this.Name].ShowView("ShipView");
      }
    }

    private void LoadShipModule()
    {
      if (Utility.DeckBroker.GetDeck(this.Name).GetPlugin("ShipView") is ShipView plugin)
      {
        int index1 = this.customizeToolStripMenuItem.DropDownItems.IndexOf((ToolStripItem) this.recipientListToolStripMenuItem);
        if (index1 != -1)
        {
          this.customizeToolStripMenuItem.DropDownItems.RemoveAt(index1);
          this.customizeToolStripMenuItem.DropDownItems.Insert(index1, (ToolStripItem) plugin.CustomizeMultiColumnRecipientCombo);
        }
        int index2 = this.customizeToolStripMenuItem.DropDownItems.IndexOf((ToolStripItem) this.customizeFieldsToolStripMenuItem);
        if (index2 == -1)
          return;
        this.customizeToolStripMenuItem.DropDownItems.RemoveAt(index2);
        this.customizeToolStripMenuItem.DropDownItems.Insert(index2, (ToolStripItem) plugin.CustomizeFields);
      }
      else
      {
        this.customizeToolStripMenuItem.DropDownItems.Remove((ToolStripItem) this.customizeFieldsToolStripMenuItem);
        this.customizeToolStripMenuItem.DropDownItems.Remove((ToolStripItem) this.recipientListToolStripMenuItem);
      }
    }

    private void LoadHTMLModule()
    {
      try
      {
        this._htmlModule = PluginManager.GetNamedPlugin("HTMLModule").CreateObject() as HTMLModule;
        this._htmlModule.LoadMenuItems(this.fedexcomToolStripMenuItem);
        this.fedexcomToolStripMenuItem.Enabled = true;
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, "ShellForm.LoadHTMLModule", ex.ToString());
      }
    }

    private void LoadLoginModule()
    {
      SplashScreen.Status = GuiData.Languafier.Translate(nameof (LoadLoginModule));
      LogInForm tabPlugin = new LogInForm();
      this._commandManager.Commands.Add(new Command("OnLogoff", new Command.ExecuteHandler(this.OnLogoff), new Command.UpdateHandler(this.OnUpdateLogoffCommand)));
      this._commandManager.Commands["OnLogoff"].CommandInstances.Add((object) tabPlugin.TabItem);
      ((ToolStripDropDownItem) this.menuStripMain.Items[0]).DropDownItems.Insert(0, tabPlugin.TabItem);
      Utility.DeckBroker.GetDeck(this.Name).AddView("LoginView", (IFedExGsmGuiTabPlugin) tabPlugin);
    }

    private void LoadCashBlockModule()
    {
      CashBlockForm tabPlugin = new CashBlockForm();
      Utility.DeckBroker.GetDeck(this.Name).AddView("CashBlock", (IFedExGsmGuiTabPlugin) tabPlugin);
    }

    private void LoadDatabaseMenu()
    {
      SplashScreen.Status = GuiData.Languafier.Translate("LoadDatabases");
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.LoadDatabaseMenu", "Loading plugins for DatabaseMenu");
      this.AddPluginsToToolStrip((ToolStripDropDownItem) this.databasesToolStripMenuItem, PluginManager.GetPlugins(GuiPluginPoint.DatabaseMenu));
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.LoadDatabaseMenu", "Loading plugins for DatabaseMenu complete");
    }

    private void LoadServiceBulletinBoardModule()
    {
      SplashScreen.Status = GuiData.Languafier.Translate("LoadSBBModule");
      ucServiceBulletinBoard tabPlugin = new ucServiceBulletinBoard();
      tabPlugin.TabItem.Click += new EventHandler(this.serviceBulletinBoardStripMenuItem_Click);
      int num = this.utilitiesToolStripMenuItem.DropDownItems.IndexOfKey(this.uploadToolStripMenuItem.Name);
      if (num != -1)
      {
        this.utilitiesToolStripMenuItem.DropDownItems.Insert(num + 1, tabPlugin.TabItem);
      }
      else
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.Name, "Cannot find Upload menu item to use for inserting Service Bulletin Board sub menu item.  Service Bulletin Board sub menu item will be appended as the 1st sub menu within Utilities menu item.");
        this.utilitiesToolStripMenuItem.DropDownItems.Insert(0, tabPlugin.TabItem);
      }
      Utility.DeckBroker.GetDeck(this.Name).AddView("SvcBullBrdView", (IFedExGsmGuiTabPlugin) tabPlugin);
    }

    private void LoadSystemAccountMenu(bool refresh)
    {
      if (!refresh)
      {
        SplashScreen.Status = GuiData.Languafier.Translate("LoadSystemAccountsMenu");
      }
      else
      {
        this.activeSystemAccountToolStripMenuItem.DropDownItems.Clear();
        this.HandlePassportModule();
      }
      Account account = new Account();
      string str = (string) null;
      if (GuiData.CurrentAccount != null)
        str = GuiData.CurrentAccount.MeterNumber + "-" + GuiData.CurrentAccount.Description;
      Error error = new Error();
      DataTable output;
      if (GuiData.AppController.ShipEngine.GetDataList((object) null, GsmDataAccess.ListSpecification.AccountList, out output, error) == 1 && output != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) output.Rows)
        {
          ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
          toolStripMenuItem.Text = row["Meter"]?.ToString() + "-" + row["Description"]?.ToString();
          toolStripMenuItem.Click += new EventHandler(this.AccountMeter_Click);
          toolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
          toolStripMenuItem.Tag = row["Account"];
          if (!string.IsNullOrEmpty(str))
            toolStripMenuItem.Checked = str == toolStripMenuItem.Text;
          this.activeSystemAccountToolStripMenuItem.DropDownItems.Add((ToolStripItem) toolStripMenuItem);
        }
      }
      else
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm.LoadSystemAccountMenu()", "Error retrieving accounts " + error?.ToString());
    }

    private void InitializeMidnightTimer()
    {
      TimeSpan timeSpan = DateTime.Today.AddDays(1.0) - DateTime.Now;
      this._midnightTimer.AutoReset = false;
      this._midnightTimer.Interval = timeSpan.TotalMilliseconds;
      this._midnightTimer.Start();
    }

    private void SetupCommandManager()
    {
      this._commandManager.Commands.Add(new Command("FormAlignWaybill", new Command.ExecuteHandler(this.OnFormAlignWaybill), new Command.UpdateHandler(this.OnUpdateFormAlignWaybill)));
      this._commandManager.Commands["FormAlignWaybill"].CommandInstances.Add((object) this.fedExIntlAirWaybillToolStripMenuItem);
      this._commandManager.Commands.Add(new Command("FormAlignSDDG", new Command.ExecuteHandler(this.OnFormAlignSDDG), new Command.UpdateHandler(this.OnUpdateFormAlignSDDG)));
      this._commandManager.Commands["FormAlignSDDG"].CommandInstances.Add((object) this.shippersDecForDGSDDGToolStripMenuItem);
      this._commandManager.Commands.Add(new Command("FormAlignOP900LG", new Command.ExecuteHandler(this.OnFormAlignOP900LG), new Command.UpdateHandler(this.OnUpdateFormAlignOP900LG)));
      this._commandManager.Commands["FormAlignOP900LG"].CommandInstances.Add((object) this.oP900LGToolStripMenuItem);
      this._commandManager.Commands.Add(new Command("FormAlignOP900LL", new Command.ExecuteHandler(this.OnFormAlignOP900LL), new Command.UpdateHandler(this.OnUpdateFormAlignOP900LL)));
      this._commandManager.Commands["FormAlignOP900LL"].CommandInstances.Add((object) this.oP900LLToolStripMenuItem);
    }

    private void SetupEvents()
    {
      GuiData.EventBroker.AddPublisher(EventBroker.Events.NewSystemAccount, (object) this, "NewSystemAccount");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.NewSystemAccount, (object) this, "OnNewSystemAccount");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.CurrentShipDateChanged, (object) this, "OnCurrentShipDateChanged");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.UpdateStatusBar, (object) this, "OnUpdateStatusBar");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.UpdateStatusBar, (object) this, "UpdateStatusBar");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.UpdateExtraServiceMessage, (object) this, "OnUpdateExtraServiceMessage");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.LoginCancelButtonPressed, (object) this, "OnLoginFormExitButtonPressed");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.IntegrationEventReceived, (object) this, "OnIntegrationEventReceived");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.ServerMessageRecieved, (object) this, "OnServerMessageReceived");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.ShutdownNetworkClient, (object) this, "OnNetworkClientShutdownMessageReceived");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.BatchEditEventReceived, (object) this, "OnBatchEditEventReceived");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.BatchEditCancelled, (object) this, "OnBatchEditCancelled");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.SwitchMeter, (object) this, "OnSwitchMeter");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.DownloadFinished, (object) this, "OnDownloadFinished");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.CurrentAccountChanged, (object) this, "OnCurrentAccountChanged");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.CurrentAccountChanged, (object) this, "CurrentAccountChanged");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.UserLoggedIn, (object) this, "OnUserLoggedIn");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.CurrentSenderCodeChanged, (object) this, "CurrentSenderCodeChanged");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.ReloadSystemPrefs, (object) this, "OnReloadSystemPrefs");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.CurrentSystemPrefsChanged, (object) this, "CurrentSystemPrefsChanged");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.MidnightTimerElapsed, (object) this._midnightTimer, "Elapsed");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.MidnightTimerElapsed, (object) this, "OnMidnightTimerElapsed");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.ReferenceListChanged, (object) this, "OnReferenceListChanged");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.RePopulateReferenceCombos, (object) this, "RePopulateReferenceCombos");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.NavigateToUrl, (object) this, "OnDisplayURL");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.NavigateToUrl, (object) this, "GoToUrl");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.MarshalForIntegration, (object) this, "OnMarshalForIntegration");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.ShutdownRequest, (object) this, "RequestShutdown");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.ShutdownRequest, (object) this, "OnShutdownRequested");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.BatchEditProcessNextEntry, (object) this, "OnProcessNextHoldFileBatchEdit");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.HoldFileSinglePackageEvent, (object) this, "HoldFileSingleEditEvent");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.FreightHoldFileSingleEditEvent, (object) this, "FreightHoldFileSingleEditEvent");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.SoftwareDownloadComplete, (object) this, "OnSoftwareDownloadsReady");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.EtdUploadFailed, (object) this, "OnEtdUploadFailed");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.PrintEodOrInvoiceReports, (object) this, "OnPrintEodOrInvoiceReports");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.FreightListChanged, (object) this, "OnFreightAccountListChanged");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.ReconcileCompleted, (object) this, "OnReconcileCompleted");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.UpdatePrinterImages, (object) this, "OnUpdatePrinterImages");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.PrinterSettingsChanged, (object) this, "PrinterSettingsChanged");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.GroundAccountNumberFormatChanged, (object) this, "OnGroundAccountNumberFormatChanged");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.AutoCloseUploadOverdue, (object) this, "OnUploadOverdue");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.TrackRequested, (object) this, "OnTrackingRequested");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.AutoTrackDetailRequest, (object) this, "OnAutoTrackDetailRequest");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.GPRDataChanged, (object) this, "OnGPRDataChanged");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.SystemTimeChanged, (object) this, "SystemTimeChanged");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.DownloadStarted, (object) this, "OnDownloadStarted");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.IDDPreferencesChanged, (object) this, "OnIDDPreferencesChanged");
      this._iddCloseTimer.Elapsed += new ElapsedEventHandler(this._iddCloseTimer_Elapsed);
      this._midnightTimer.SynchronizingObject = (ISynchronizeInvoke) this;
      SystemEvents.TimeChanged += new EventHandler(this.SystemEvents_TimeChanged);
    }

    private void SystemEvents_TimeChanged(object sender, EventArgs e)
    {
      this.InitializeMidnightTimer();
      this.InitializeIDDCloseTimer(GuiData.CurrentAccount);
      EventHandler systemTimeChanged = this.SystemTimeChanged;
      if (systemTimeChanged == null)
        return;
      systemTimeChanged((object) this, e);
    }

    public void OnNewSystemAccount(object sender, AccountEventArgs args)
    {
      if (!args.NewAccount.IsMaster)
        return;
      SplashScreen.Hide();
      if ((Utility.IsLacCountry(args.NewAccount.Address.CountryCode) || Utility.IsPuertoRico(args.NewAccount.Address)) && Registry.LocalMachine.GetValue("Software\\FedEx\\FdxWorld") != null && Utility.DisplayError(GuiData.Languafier.Translate("RunWorldProImport"), Error.ErrorType.Question) == DialogResult.Yes)
        Process.Start(new FedEx.Gsm.Common.ConfigManager.ConfigManager().InstallLocs.InstallDirectory + "ConversionUtility\\FedEx.Integration.WorldProConversion.exe");
      this.SwitchSystemAccount(args.NewAccount);
    }

    private void SwitchSystemAccount(Account newAccount)
    {
      this.SwitchSystemAccount(newAccount, false);
    }

    private void SwitchSystemAccount(Account newAccount, bool initial)
    {
      this.SetSystemAccount(newAccount);
      GuiData.AppController.ShipEngine.UpdateAccountMeterInClientTable(newAccount);
      if (newAccount.IsCashOnly)
        Utility.DeckBroker.GetDeck(this.Name).ShowView("CashBlock");
      else if (newAccount.RequireUserLogin)
      {
        ((LogInForm) Utility.DeckBroker.GetDeck(this.Name).GetPlugin("LoginView")).SetSystemAccount(newAccount.MeterNumber, newAccount.AccountNumber);
        Utility.DeckBroker.GetDeck(this.Name).ShowView("LoginView");
      }
      else
      {
        if (this.NeedsDownload(newAccount))
        {
          using (DemandDownload demandDownload = new DemandDownload())
          {
            GuiData.ConfigManager.SetProfileValue("SHIPNET2000/GUI/SETTINGS/N" + newAccount.MeterNumber + "-" + newAccount.AccountNumber, "DOWNLOADSPENDING", (object) false);
            demandDownload.SelectAll();
            int num = (int) demandDownload.ShowDialog((IWin32Window) this);
          }
        }
        if (initial)
          return;
        this.DisplayTaxDisclaimer();
      }
    }

    private bool NeedsDownload(Account acct)
    {
      bool bVal;
      return GuiData.ConfigManager.GetProfileBool("SHIPNET2000/GUI/SETTINGS/N" + acct.MeterNumber + "-" + acct.AccountNumber, "DOWNLOADSPENDING", out bVal) & bVal;
    }

    private void UpdateRateFlags()
    {
      ServiceResponse serviceResponse1 = GuiData.AppController.ShipEngine.ValidateRateTables(GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber, "D");
      bool flag1 = serviceResponse1 != null && serviceResponse1.IsOperationOk;
      ServiceResponse serviceResponse2 = GuiData.AppController.ShipEngine.ValidateRateTables(GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber, "I");
      bool flag2 = serviceResponse2 != null && serviceResponse2.IsOperationOk;
      ServiceResponse serviceResponse3 = GuiData.AppController.ShipEngine.ValidateRateTables(GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber, "C");
      bool flag3 = serviceResponse3 != null && serviceResponse3.IsOperationOk;
      ServiceResponse serviceResponse4 = GuiData.AppController.ShipEngine.ValidateRateTables(GuiData.CurrentAccount.AccountNumber, "list", "D");
      bool flag4 = serviceResponse4 != null && serviceResponse4.IsOperationOk;
      GuiData.DownloadedRates[GuiData.RatesType.Domestic] = flag1;
      GuiData.DownloadedRates[GuiData.RatesType.International] = flag2;
      GuiData.DownloadedRates[GuiData.RatesType.IntraCA] = flag3;
      GuiData.DownloadedRates[GuiData.RatesType.List] = flag4;
    }

    public void OnDownloadFinished(object sender, DownloadFinishedEventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Delegate) new DownloadFinishedDelegate(this.OnDownloadFinished), sender, (object) e);
      }
      else
      {
        this.toolStripDownloadProgress.Visible = false;
        if (e.DomesticListRatings || e.DomesticRatings || e.GroundListRates || e.InternationalRatings || e.SmartPostRates)
        {
          this.UpdateRateFlags();
          Account accountToUpdate = new Account(GuiData.CurrentAccount);
          if (this.UpdateCurrentAccount(ref accountToUpdate))
          {
            GuiData.CurrentAccount = accountToUpdate;
            this.RefreshAllPluginHandlers(accountToUpdate);
            Utility.UpdateSeparators(this.toolStripMain);
          }
        }
        if (e.URSA)
          this.CheckUrsa(false);
        if (e.Software)
          this.installSoftwareUpdateToolStripMenuItem.Enabled = true;
        if (!e.CountryList)
          return;
        GuiData.ReloadCountryComboDataTables();
      }
    }

    private void RefreshAllPluginHandlers(Account acct)
    {
      if (this._listPluginHandlers == null)
        return;
      foreach (AbstractToolStripItemPluginHandler listPluginHandler in this._listPluginHandlers)
        listPluginHandler.RefreshVisibleAndEnabled(acct);
    }

    private void UpdatePluginNames(Account acct)
    {
      ToolStripItem menuItem = this.databasesToolStripMenuItem.DropDownItems["freightBolViewMenuItem"];
      AbstractToolStripItemPluginHandler itemPluginHandler = this._listPluginHandlers.FirstOrDefault<AbstractToolStripItemPluginHandler>((System.Func<AbstractToolStripItemPluginHandler, bool>) (p => p.TabItem == menuItem));
      if (itemPluginHandler == null || menuItem == null || !(itemPluginHandler.Plugin is GuiDialogPlugin plugin))
        return;
      string id = GuiData.CurrentAccount.is_FREIGHT2020_HANDLING_UNITS_CTL_Initiative_Enabled ? plugin.TextCode + "2020" : plugin.TextCode;
      menuItem.Text = GuiData.Languafier.Translate(id);
    }

    private bool UpdateCurrentAccount(ref Account accountToUpdate)
    {
      bool flag = false;
      if (accountToUpdate == null)
        return flag;
      Account output;
      if (GuiData.AppController.ShipEngine.Retrieve<Account>(new Account()
      {
        AccountNumber = accountToUpdate.AccountNumber,
        MeterNumber = accountToUpdate.MeterNumber
      }, out output, out Error _) == 1)
      {
        accountToUpdate = output;
        flag = true;
      }
      return flag;
    }

    public void OnUserLoggedIn(object sender, AccountEventArgs e)
    {
      this.SetSystemAccount(e.NewAccount);
      if (this._dgWarnDialog == null)
      {
        this._dgWarnDialog = new DgWarning();
        this._dgWarnDialog.Show((IWin32Window) this);
        this._dgWarnDialog.BringToFront();
        this._dgWarnDialog.Focus();
      }
      this.IntegrationHook(129, (GuiData.CurrentUser != null ? GuiData.CurrentUser.UserId : string.Empty) + " || " + (GuiData.CurrentUser != null ? GuiData.CurrentUser.Name : string.Empty));
      if (this._dgWarnDialog != null)
      {
        this.PromptForSvcBulletinBoard(e.NewAccount);
        this.DisplayQueuedMessages(e.NewAccount);
      }
      this.DisplayTaxDisclaimer();
      if (!this.NeedsDownload(e.NewAccount))
        return;
      using (DemandDownload demandDownload = new DemandDownload())
      {
        GuiData.ConfigManager.SetProfileValue("SHIPNET2000/GUI/SETTINGS/N" + e.NewAccount.MeterNumber + "-" + e.NewAccount.AccountNumber, "DOWNLOADSPENDING", (object) false);
        demandDownload.SelectAll();
        int num = (int) demandDownload.ShowDialog((IWin32Window) this);
      }
    }

    public void OnMidnightTimerElapsed(object sender, EventArgs args)
    {
      this.InitializeMidnightTimer();
    }

    private void OnDoWork_SoftwareUpdates(object sender, DoWorkEventArgs args)
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Verbose, FxLogger.AppCode_GUI, this.Name, "Starting to check for software updates");
      List<AdminInstallInfo> availableInstalls = new List<AdminInstallInfo>();
      if (!GuiData.AppController.ShipEngine.ListAvailableInstalls(ref availableInstalls).IsOperationOk || availableInstalls.Count <= 0)
        return;
      args.Result = (object) availableInstalls;
    }

    public void OnSoftwareDownloadsReady(object sender, SoftwareDownloadCompletArgs args)
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.OnSoftwareDownloadsReady", "Received download signal from Admin");
      if (args.Successful && args.Result != null)
        this.Invoke((Delegate) new ShellForm.ReceivedSoftwareUpdatesDelegate(this.ReceivedSoftwareUpdates), (object) args.Result);
      else
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_GUI, "ShellForm.OnSoftwareDownloadsReady", "Download Signal ignored due to error or null result.");
    }

    public void OnPrintEodOrInvoiceReports(object sender, EventArgs args)
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.OnPrintEodOrInvoiceReports", "Entering method.");
      if (!Utility.IsNetworkClient)
      {
        try
        {
          RemoteReportHelper.ReportProcessing.ProcessEndOfDay();
        }
        catch (Exception ex)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm.OnPrintEodOrInvoiceReports", "Could not process reports. " + ex.ToString());
        }
      }
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.OnPrintEodOrInvoiceReports", "Leaving method.");
    }

    public void OnReconcileCompleted(object sender, EventArgs args)
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, "ShellForm.OnReconcileCompleted", "Recieved ReconcileCompleted message, refreshing ursa status");
      this.CheckUrsa(false);
      this.CheckZeroRatesReconcile();
    }

    private bool HasPendingActivations()
    {
      bool flag = false;
      try
      {
        DeltaHistoryListResponse deltaHistoryList = GuiData.AppController.ShipEngine.GetDeltaHistoryList(new DeltaHistory());
        if (deltaHistoryList.DeltaHistories != null)
        {
          foreach (DeltaHistory deltaHistory in deltaHistoryList.DeltaHistories)
          {
            if (deltaHistory.ReconcileStatus == 0)
              flag = true;
          }
        }
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm.HasPendingActivations", "Error enumerating Delta History " + ex.ToString());
      }
      return flag;
    }

    public void ReceivedSoftwareUpdates(List<AdminInstallInfo> installInfo)
    {
      bool flag = this.HasPendingActivations();
      if (installInfo != null && installInfo.Count > 0 && !flag)
      {
        int num1 = 3;
        FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
        string s = "";
        ref string local = ref s;
        if (configManager.GetProfileString("SETTINGS", "SOFTWARE_TIMEOUT", out local) && !string.IsNullOrEmpty(s))
        {
          int num2 = int.Parse(s);
          if (num2 > 0)
            num1 = num2;
        }
        this.installSoftwareUpdateToolStripMenuItem.Enabled = true;
        if (installInfo[0].DownloadStatus == AdminInstallInfo.DownloadState.Downloaded)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString(), "Maximum install grace period is " + num1.ToString() + " days.");
          TimeSpan timeSpan = DateTime.Now.Subtract(installInfo[0].DownloadDate);
          int iDays = Math.Max(num1 - timeSpan.Days, 0);
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString(), string.Format("Remaining days from download date ({0}): {1}", (object) installInfo[0].DownloadDate, (object) iDays));
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.ReceivedSortwareUpdates", "Showing update install dialog");
          using (NewSoftwareAvailable softwareAvailable = new NewSoftwareAvailable(iDays))
          {
            SplashScreen.Close();
            if (softwareAvailable.ShowDialog((IWin32Window) this) == DialogResult.OK)
            {
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.ReceivedSortwareUpdates", "User selected install, calling ExecuteInstalls");
              ServiceResponse serviceResponse = GuiData.AppController.ExecuteInstalls(installInfo);
              if (serviceResponse != null)
              {
                if (serviceResponse.IsOperationOk)
                  this.installSoftwareUpdateToolStripMenuItem.Enabled = false;
              }
            }
            else if (iDays <= 0)
            {
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.ReceivedSortwareUpdates", "User selected exit system, no days remaining, exiting");
              Application.Exit();
            }
            else
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.ReceivedSortwareUpdates", "User selected install later.");
          }
        }
      }
      else
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_GUI, "ShellForm.ReceivedSortwareUpdates", "Update not shown due to pending activations or empty install list.");
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Verbose, FxLogger.AppCode_GUI, this.Name, "Done checking for software updates");
    }

    public void OnWorkCompleted_SoftwareUpdates(object sender, RunWorkerCompletedEventArgs args)
    {
      this.ReceivedSoftwareUpdates(args.Result as List<AdminInstallInfo>);
    }

    public void OnMarshalForIntegration(object sender, MarshalForIntegrationEventArgs e)
    {
      try
      {
        int num = ManagedToUnmanagedWrapper.WinExecEntry("FSM.Cafe,CafeInterfaceEx", e.TagId, e.FieldType, e.EventType, e.PageForIntegration, e.Data, e.MeterNumber);
        string inMessage = string.Format("WinExecEntry returned status {0}", (object) num);
        FxLogger.LogMessage(num == 0 ? FedEx.Gsm.Common.Logging.LogLevel.Info : FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "MarshalForIntegration", inMessage);
      }
      catch
      {
      }
    }

    public void OnLogoff(Command cmd)
    {
      if (this._dgWarnDialog != null)
      {
        if (!this._dgWarnDialog.IsDisposed)
          this._dgWarnDialog.Dispose();
        this._dgWarnDialog = (DgWarning) null;
      }
      Utility.DeckBroker[this.Name].ShowView("LoginView");
      this.IntegrationHook(130, (GuiData.CurrentUser != null ? GuiData.CurrentUser.UserId : string.Empty) + " || " + (GuiData.CurrentUser != null ? GuiData.CurrentUser.Name : string.Empty));
    }

    public void OnUpdateLogoffCommand(Command cmd)
    {
    }

    private void processTransactionsStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        Utility.DeckBroker[this.Name].ShowView("PassportView");
        this.DeselectTabs();
      }
      catch (KeyNotFoundException ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, nameof (processTransactionsStripMenuItem_Click), "Passport not loaded");
      }
    }

    private void serviceBulletinBoardStripMenuItem_Click(object sender, EventArgs e)
    {
      Utility.DeckBroker[this.Name].ShowView("SvcBullBrdView");
    }

    private void billOfLading_Click(object sender, EventArgs e)
    {
      Utility.DeckBroker[this.Name].ShowView("BillOfLadingView");
    }

    private void ReportsModule_TabItem_Click(object sender, EventArgs e)
    {
      Utility.DeckBroker[this.Name].ShowView("ReportsModule");
    }

    private void toolStripDropDownButton_MouseDown(object sender, MouseEventArgs e)
    {
      if (Control.ModifierKeys != Keys.Alt || e.Button != MouseButtons.Left)
        return;
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_GUI, "ShellForm.toolStripDropDownButton_MouseDown", "ALT Key Pressed when trying to open dropdown, ");
      SendKeys.Send("%");
    }

    private void Language_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate("LanguageChangedRestart"), string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
        return;
      ToolStripMenuItem selected = sender as ToolStripMenuItem;
      FedEx.Gsm.Common.Languafier.Languafier.SetCulture(selected.Tag.ToString());
      new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI).SetProfileValue("Settings", "Language", (object) Thread.CurrentThread.CurrentUICulture.Name);
      GuiData.CurrentAccount.Culture = Thread.CurrentThread.CurrentUICulture.Name;
      GuiData.AppController.ShipEngine.Modify<Account>(GuiData.CurrentAccount);
      this.IntegrationHook(134, Thread.CurrentThread.CurrentUICulture.Name);
      this.UpdatePeerChecks((ToolStripItem) selected);
      SystemShutdownRequest ssr = new SystemShutdownRequest();
      ssr.Restart = SystemShutdownRequest.RestartType.Soft;
      ssr.StopType = SystemShutdownRequest.StopStart.StopAllButAdmin;
      if (this.RequestShutdown == null)
        return;
      this.RequestShutdown((object) this, new ShutdownRequestEventArgs(ssr, true));
    }

    private void AccountMeter_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
      string[] strArray = toolStripMenuItem.Text.Split('-');
      Account output;
      if (GuiData.AppController.ShipEngine.Retrieve<Account>(new Account()
      {
        AccountNumber = toolStripMenuItem.Tag.ToString(),
        MeterNumber = strArray[0]
      }, out output, out Error _) != 1)
        return;
      this.SwitchSystemAccount(output);
      this.IntegrationHook(133, output.MeterNumber);
    }

    public void OnSwitchMeter(object sender, AccountEventArgs args)
    {
      Account output;
      if (GuiData.AppController.ShipEngine.Retrieve<Account>(new Account()
      {
        MeterNumber = args.NewAccount.MeterNumber,
        AccountNumber = args.NewAccount.AccountNumber
      }, out output, out Error _) != 1)
        return;
      this.SwitchSystemAccount(output);
    }

    public void OnReloadSystemPrefs(object sender, EventArgs args)
    {
      SystemPrefs output;
      if (GuiData.AppController.ShipEngine.Retrieve<SystemPrefs>(new SystemPrefs()
      {
        FedExAcctNbr = GuiData.CurrentAccount.AccountNumber,
        MeterNumber = GuiData.CurrentAccount.MeterNumber
      }, out output, out Error _) == 0 && output == null)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "Could not load SystemPrefs for current system & account. Will commit new object.");
        output = new SystemPrefs();
        this.LoadDefaultRecipientColumnsPrefs(output);
        this.LoadDefaultReferencePreferenceLabels(output);
        output.MeterNumber = GuiData.CurrentAccount.MeterNumber;
        output.FedExAcctNbr = GuiData.CurrentAccount.AccountNumber;
        GuiData.AppController.ShipEngine.Insert<SystemPrefs>(output);
      }
      else if (output != null && string.IsNullOrEmpty(output.AddRef1ScrDsply))
        this.LoadDefaultReferencePreferenceLabels(output);
      CurrentSystemPrefsEventArgs args1 = new CurrentSystemPrefsEventArgs();
      args1.OldSystemPrefs = GuiData.CurrentSystemPrefs;
      args1.NewSystemPrefs = output;
      GuiData.CurrentSystemPrefs = output;
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      long num = -1;
      ref long local = ref num;
      configManager.GetProfileLong("Settings", "CountrySortBy", out local);
      args1.OldCountrySortBy = (int) num;
      args1.NewCountrySortBy = (int) num;
      if (this.CurrentSystemPrefsChanged == null)
        return;
      this.CurrentSystemPrefsChanged((object) this, args1);
    }

    public void OnCurrentAccountChanged(object sender, AccountEventArgs e)
    {
      this.authenticateInstallationToolStripMenuItem.Visible = Utility.DevMode && GuiData.AppController.ShipEngine.IsApiRegistrationNeeded();
      this.loadAShipmentFromXMLToolStripMenuItem.Visible = Utility.DevMode;
      this.forceReloadOfGuiDataListsToolStripMenuItem.Visible = Utility.DevMode;
      this.printDirectoryLabelToolStripMenuItem.Visible = this.GetCustomLabelDiagnosticModeEnabled();
      this.integrationHelpMenuItem.Visible = e.NewAccount.Address != null && (e.NewAccount.Address.CountryCode == "US" || Utility.IsLacCountry(e.NewAccount.Address.CountryCode));
      ShellForm._backupRestoreModule.CurrentAccountNumber = e.NewAccount.AccountNumber;
      ShellForm._backupRestoreModule.CurrentMeterNumber = e.NewAccount.MeterNumber;
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("SETTINGS/N{0}-{1}", (object) e.NewAccount.MeterNumber, (object) e.NewAccount.AccountNumber);
      string str;
      configManager.GetProfileString(stringBuilder.ToString(), "AutoTab", out str);
      IFedExGsmGuiTabPlugin plugin = Utility.DeckBroker.GetDeck(this.Name).GetPlugin("LoginView");
      if (plugin != null)
        plugin.TabItem.Enabled = e.NewAccount.RequireUserLogin;
      GuiData.AutoTab = !string.IsNullOrEmpty(str) && str.ToUpper() == "Y";
      FocusExtender.FocusInfo.FocusBackColor = Utility.GetFieldColor(e.NewAccount.MeterNumber, e.NewAccount.AccountNumber);
      this.UpdatePrinterFlags();
      this.UpdateRateFlags();
      if (string.IsNullOrEmpty(e.NewAccount.DateFormat))
      {
        string empty = string.Empty;
        e.NewAccount.DateFormat = !configManager.GetProfileString("SETTINGS", "DateFormat", out empty) ? "MM/dd/yyyy" : empty;
      }
      ReportVariables.DateFormat = e.NewAccount.DateFormat.Equals("mm/dd/yyyy") ? "MM/dd/yyyy" : "dd/MMM/yyyy";
      Utility.LoadTdMaster((string) null, e.NewAccount);
      this.HandlePassportModule();
      this.RefreshAllPluginHandlers(e.NewAccount);
      Utility.UpdateSeparators(this.toolStripMain);
      this.UpdatePluginNames(e.NewAccount);
      this.StartAutoCloseTimer();
      if (!GuiData.CurrentAccount.IsSmartPostEnabled || !GuiData.CurrentAccount.SPAutoCloseEnabled)
        return;
      this.StartSmartPostAutoCloseTimer(GuiData.CurrentAccount.SPAutoCloseTime);
    }

    private void LoadCurrenTdmaster(Account acct)
    {
      if (acct == null)
      {
        GuiData.CurrentTdMasterShipment = (MasterShipment) null;
      }
      else
      {
        FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
        string str = (string) null;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("SETTINGS/N{0}-{1}", (object) acct.MeterNumber, (object) acct.AccountNumber);
        configManager.GetProfileString(stringBuilder.ToString(), "CurrentMasterShipmentID", out str);
        string s = (string) null;
        stringBuilder.AppendFormat("SETTINGS/N{0}-{1}", (object) acct.MeterNumber, (object) acct.AccountNumber);
        configManager.GetProfileString(stringBuilder.ToString(), "CurrentMasterShipmentPickupDate", out s);
        DateTime result;
        DateTime.TryParse(s, out result);
        if (string.IsNullOrEmpty(str))
          return;
        int num = result != DateTime.MinValue ? 1 : 0;
      }
    }

    private void systemSettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SplashScreen.Hide();
      using (ChooseSystemAccount chooseSystemAccount = new ChooseSystemAccount())
      {
        if (DialogResult.OK != chooseSystemAccount.ShowDialog((IWin32Window) this))
          return;
        this.LoadSystemAccountMenu(true);
        Utility.UpdateSeparators(this.toolStripMain);
      }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (AboutBox aboutBox = new AboutBox())
      {
        int num = (int) aboutBox.ShowDialog((IWin32Window) this);
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GuiData.AppController.ShipEngine.UnRegisterForUnsolicitedServerMessages();
      this.Close();
    }

    public void OnShutdownRequested(object sender, ShutdownRequestEventArgs e)
    {
      this._forceQuietShutdown = e.ForceQuiet;
      GuiData.AppController.ShipEngine.UnRegisterForUnsolicitedServerMessages();
      this.Close();
      GuiData.AppController.ShipEngine.SystemShutdown(e.ShutdownRequest);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      if (!Utility.DevMode && !this._forceQuietShutdown && e.CloseReason == CloseReason.UserClosing)
      {
        if (Utility.SoftwareOnly || Utility.IsNetworkClient)
        {
          if (this.CanCafeShutdown())
          {
            if (this._bIsFaxSpodInProgress && MessageBox.Show(GuiData.Languafier.TranslateMessage(38534), (string) null, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
              e.Cancel = true;
              return;
            }
          }
          else
          {
            e.Cancel = true;
            return;
          }
        }
        else
        {
          if (this._bIsFaxSpodInProgress && MessageBox.Show(GuiData.Languafier.TranslateMessage(38534), (string) null, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
          {
            e.Cancel = true;
            return;
          }
          ExitTheSoftware exitTheSoftware = new ExitTheSoftware();
          if (exitTheSoftware.ShowDialog((IWin32Window) this) == DialogResult.No)
          {
            e.Cancel = true;
            return;
          }
          switch (exitTheSoftware.UserExitAction)
          {
            case ExitTheSoftware.ExitAction.Logoff:
              Utility.DeckBroker.GetDeck(this.Name).ShowView("LoginView");
              e.Cancel = true;
              break;
            case ExitTheSoftware.ExitAction.Reboot:
              if (this.CanCafeShutdown())
              {
                if (MessageBox.Show(GuiData.Languafier.TranslateMessage(38368), " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                  new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.ADMINSVC).SetProfileValue("Settings", "LastReboot", (object) DateTime.Now.ToString("G"));
                  GuiData.AppController.ShipEngine.SystemShutdown(new SystemShutdownRequest()
                  {
                    StopType = SystemShutdownRequest.StopStart.StopAll,
                    Restart = SystemShutdownRequest.RestartType.Reboot
                  });
                  break;
                }
                e.Cancel = true;
                return;
              }
              e.Cancel = true;
              return;
            case ExitTheSoftware.ExitAction.Exit:
              this._midnightTimer.Stop();
              this._iddCloseTimer.Stop();
              if (new AdminLogin(AdminLogin.PasswordType.Any).ShowDialog((IWin32Window) this) != DialogResult.OK)
              {
                e.Cancel = true;
                return;
              }
              this.Refresh();
              break;
            default:
              e.Cancel = true;
              break;
          }
        }
      }
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.OnFormClosing", "Closing due to " + e.CloseReason.ToString());
      base.OnFormClosing(e);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
      try
      {
        this.IntegrationHook(119);
        this._commStatus.EndCommStatusMessages();
        this._stopCommThread.Set();
        this._midnightTimer.Stop();
        this._iddCloseTimer.Stop();
        this.StopReportProcessing();
        RemotePrintHelper.StopRemoting();
        GuiData.ShipEngineEventSink.Stop();
        GuiData.AppController.CleanUpOnShutdown();
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), string.Format("Exception: {0}", (object) ex));
      }
      base.OnFormClosed(e);
    }

    private void CloseMeter(Shipment.CarrierType carrier)
    {
      RevenueServiceRequest revenueServiceRequest = new RevenueServiceRequest()
      {
        IncludeExpress = carrier == Shipment.CarrierType.Express,
        IncludeGround = carrier == Shipment.CarrierType.Ground,
        AccountList = new List<Account>()
      };
      revenueServiceRequest.AccountList.Add(new Account(GuiData.CurrentAccount));
      if (this.UpdateStatusBar != null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(GuiData.Languafier.Translate("ClosingFedEx"), (object) carrier.ToString());
        this.UpdateStatusBar((object) this, (EventArgs) new StatusBarEventArgs(stringBuilder.ToString()));
      }
      GuiData.AppController.ShipEngine.CloseMeter(revenueServiceRequest);
      while (GuiData.AppController.ShipEngine.CheckCloseStatus(revenueServiceRequest).RevStatusList[0].Status != RevenueServiceResponse.RevStatus.NotRunning)
        Thread.Sleep(5000);
    }

    private bool GetShipmentsToUpload(Shipment.CarrierType carrier)
    {
      Error error = new Error();
      bool shipmentsToUpload = GuiData.AppController.ShipEngine.GetShipmentsToUpload(GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber, carrier, error);
      return error.Code == 1 && shipmentsToUpload;
    }

    private bool CanCafeShutdown()
    {
      bool flag = true;
      if (Utility.SoftwareOnly && Utility.IsNetworkClient)
        return true;
      bool shipmentsToUpload1 = this.GetShipmentsToUpload(Shipment.CarrierType.Express);
      bool shipmentsToUpload2 = this.GetShipmentsToUpload(Shipment.CarrierType.Ground);
      if (shipmentsToUpload1 | shipmentsToUpload2)
      {
        switch (new ShutdownDlg().ShowDialog((IWin32Window) this))
        {
          case DialogResult.OK:
            if (shipmentsToUpload1)
              this.CloseMeter(Shipment.CarrierType.Express);
            if (shipmentsToUpload2)
              this.CloseMeter(Shipment.CarrierType.Ground);
            flag = true;
            break;
          case DialogResult.Cancel:
            if (DateTime.Now < GuiData.CurrentAccount.UploadFailTime)
            {
              GuiData.CurrentAccount.UploadFailTime = DateTime.Now;
              break;
            }
            flag = true;
            break;
          case DialogResult.Retry:
            this.UploadStuff();
            flag = true;
            break;
          case DialogResult.Yes:
            flag = true;
            break;
          case DialogResult.No:
            flag = false;
            break;
        }
      }
      return flag;
    }

    private int UploadStuff()
    {
      RevenueServiceRequest revenueServiceRequest = new RevenueServiceRequest();
      revenueServiceRequest.NunsOrigin = RevenueServiceRequest.NUNSOrigin.MutexOffOriginShutdown;
      if (this.UpdateStatusBar != null)
        this.UpdateStatusBar((object) this, (EventArgs) new StatusBarEventArgs("UploadingPleaseWait"));
      RevenueServiceResponse revenueServiceResponse = GuiData.AppController.ShipEngine.DemandUpload(revenueServiceRequest);
      if (!revenueServiceResponse.IsOperationOk)
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, nameof (UploadStuff), revenueServiceResponse.Error.Message);
      return revenueServiceResponse.ErrorCode;
    }

    private void InitializeAddressChecker()
    {
      AddressChecker.RegisterClient((ICheckAddress) new ShareClient(GuiData.AppController.ShipEngine));
    }

    private void addrChkrPrefsMenuItem_Click(object sender, EventArgs e)
    {
      AddressChecker.GetClient(GuiData.CurrentAccount).ShowVerifiCustomizationDlg();
    }

    private void addrChkrBatchMenuItem_Click(object sender, EventArgs e)
    {
      throw new NotSupportedException("Batch verifi is not supported.");
    }

    private void installSoftwareUpdateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      List<AdminInstallInfo> availableInstalls = new List<AdminInstallInfo>();
      ServiceResponse serviceResponse1 = GuiData.AppController.ShipEngine.ListAvailableInstalls(ref availableInstalls);
      if (serviceResponse1 == null || !serviceResponse1.IsOperationOk)
        return;
      ServiceResponse serviceResponse2 = GuiData.AppController.ExecuteInstalls(availableInstalls);
      this.installSoftwareUpdateToolStripMenuItem.Enabled = serviceResponse2 != null && !serviceResponse2.IsOperationOk;
    }

    private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (DemandDownload demandDownload = new DemandDownload())
      {
        int num = (int) demandDownload.ShowDialog((IWin32Window) this);
      }
    }

    private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (Upload upload = new Upload())
      {
        int num = (int) upload.ShowDialog((IWin32Window) this);
      }
    }

    private void formsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (PrintSettings printSettings = new PrintSettings())
      {
        int num = (int) printSettings.ShowDialog((IWin32Window) this);
        this.UpdatePrinterFlags();
        if (!printSettings.SettingChanged || this.PrinterSettingsChanged == null)
          return;
        this.PrinterSettingsChanged((object) this, EventArgs.Empty);
      }
    }

    private void customizeUserPromptsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (CustomizeUserPrompts customizeUserPrompts = new CustomizeUserPrompts())
      {
        int num = (int) customizeUserPrompts.ShowDialog((IWin32Window) this);
      }
    }

    public void OnProcessNextHoldFileBatchEdit(object sender, EventArgs e)
    {
      this.ProcessNextHoldFileBatchEdit();
    }

    private void ProcessNextHoldFileBatchEdit()
    {
      StatusBarEventArgs args = new StatusBarEventArgs(string.Empty);
      if (this._holdFileIndex >= this._holdFileIdList.Count)
      {
        this._holdFileIdList = (List<string>) null;
        this._holdFileIndex = 0;
        GuiData.BatchEditInProgress = false;
        if (this.UpdateStatusBar == null)
          return;
        this.UpdateStatusBar((object) this, (EventArgs) args);
      }
      else
      {
        HoldFileInfo output;
        if (GuiData.AppController.ShipEngine.Retrieve<HoldFileInfo>(new HoldFileInfo()
        {
          HoldFileInfoID = this._holdFileIdList[this._holdFileIndex++]
        }, out output, out Error _) != 1)
          return;
        if (this.UpdateStatusBar != null)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendFormat(GuiData.Languafier.Translate("ProcessingHoldFileEntryXofY"), (object) this._holdFileIndex, (object) this._holdFileIdList.Count);
          args.Step = 1;
          args.Message = stringBuilder.ToString();
          this.UpdateStatusBar((object) this, (EventArgs) args);
        }
        TransactionParser parser = new TransactionParser();
        parser.SetTransaction(output.HoldFileData);
        if (!FedEx.Gsm.Common.Integration.SharedMethods.SharedMethods.IsFreightTransaction(parser))
        {
          HoldFileSingleEditEventArgs e = new HoldFileSingleEditEventArgs(output.HoldFileInfoID);
          if (this.HoldFileSingleEditEvent == null)
            return;
          this.HoldFileSingleEditEvent((object) this, e);
        }
        else
        {
          HoldFileSingleEditEventArgs e = new HoldFileSingleEditEventArgs(output.HoldFileInfoID);
          Utility.DeckBroker[nameof (ShellForm)].ContainsView("FreightView");
          if (this.FreightHoldFileSingleEditEvent == null)
            return;
          this.FreightHoldFileSingleEditEvent((object) this, e);
        }
      }
    }

    private void ProcessHoldFileBatchEdit(object sender, HoldFileBatchEditEventArgs eventData)
    {
      GuiData.BatchEditInProgress = true;
      StatusBarEventArgs args = new StatusBarEventArgs(string.Empty);
      if (this.UpdateStatusBar != null)
      {
        args.Maximum = eventData.HoldFileIdList.Count;
        this.UpdateStatusBar((object) this, (EventArgs) args);
      }
      this._holdFileIdList = new List<string>((IEnumerable<string>) eventData.HoldFileIdList);
      this._holdFileIndex = 0;
      this.ProcessNextHoldFileBatchEdit();
    }

    public void OnBatchEditCancelled(object sender, EventArgs e)
    {
      this._holdFileIdList = (List<string>) null;
      this._holdFileIndex = 0;
      GuiData.BatchEditInProgress = false;
      StatusBarEventArgs args = new StatusBarEventArgs(string.Empty);
      if (this.UpdateStatusBar == null)
        return;
      this.UpdateStatusBar((object) this, (EventArgs) args);
    }

    public void DoWorkForIntegration(object sender, TwoFiftyTransactionEventArgs args)
    {
      TransactionParser trxnParserIn = new TransactionParser();
      trxnParserIn.SetTransaction(args.Transaction);
      switch (trxnParserIn.GetField(FieldID.IntegrationShipScreenAction).ToUpper())
      {
        case "CLEARFXIAMENU":
          this.ClearFXIAMenu(trxnParserIn);
          break;
        case "ADDFXIAMENUITEM":
          this.AddFXIAMenuItem(trxnParserIn);
          break;
        case "REMOVEFXIAMENUITEM":
          this.RemoveFXIAMenuItem(trxnParserIn);
          break;
        case "SETFXIAMENUITEM":
          this.SetFXIAMenuItem(trxnParserIn);
          break;
        case "SETCOLOR":
          this.SetColor(trxnParserIn);
          break;
      }
      Error error = new Error();
      args.OutputTransaction = ((TwoFiftyTransactionTrafficCop) sender).EntryPoint(args.Transaction, ref error);
      args.Error = (object) error;
    }

    public void OnServerMessageReceived(object sender, ServerMessageReceivedArgs args)
    {
      this.ShowMessageDialog(args.Message);
    }

    public void OnNetworkClientShutdownMessageReceived(object sender, EventArgs args)
    {
      GuiData.AppController.ShipEngine.UnRegisterForUnsolicitedServerMessages();
      this.Close();
    }

    public void ShowMessageDialog(string message)
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Delegate) new ShellForm.MessageDialogDelegate(this.ShowMessageDialog), (object) message);
      }
      else
      {
        int num = (int) MessageBox.Show((IWin32Window) this, message, "Message From FedEx Ship Manager Administrator");
      }
    }

    public void OnIntegrationEventReceived(object sender, TwoFiftyTransactionEventArgs args)
    {
      if (this.InvokeRequired)
        this.Invoke((Delegate) new ShellForm.IntegrationEventReceived(this.DoWorkForIntegration), sender, (object) args);
      else
        this.DoWorkForIntegration(sender, args);
    }

    public void OnBatchEditEventReceived(object sender, HoldFileBatchEditEventArgs args)
    {
      if (this.InvokeRequired)
        this.Invoke((Delegate) new ShellForm.BatchEditEventReceived(this.ProcessHoldFileBatchEdit), sender, (object) args);
      else
        this.ProcessHoldFileBatchEdit(sender, args);
    }

    private bool IntegrationHook(
      string tag,
      int eventType,
      string controlType,
      int pageForIntegration,
      string data)
    {
      bool flag = false;
      if (!GuiData.IntegrationInProgress || eventType == 122)
      {
        ManagedToUnmanagedWrapper.WinExecEntry("FSM.Cafe,CafeInterfaceEx", tag, controlType, eventType, pageForIntegration, data, GuiData.CurrentAccount != null ? GuiData.CurrentAccount.MeterNumber : (string) null);
        flag = true;
      }
      return flag;
    }

    private bool IntegrationHook(int eventType)
    {
      return this.IntegrationHook(string.Empty, eventType, string.Empty, 1, string.Empty);
    }

    private bool IntegrationHook(int eventType, string data)
    {
      return this.IntegrationHook(string.Empty, eventType, string.Empty, 1, data);
    }

    private void ShellForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (!e.Control)
        return;
      FkeysEvents eventType;
      switch (e.KeyCode)
      {
        case Keys.F1:
          eventType = FkeysEvents.F1;
          break;
        case Keys.F2:
          eventType = FkeysEvents.F2;
          break;
        case Keys.F3:
          eventType = FkeysEvents.F3;
          break;
        case Keys.F4:
          eventType = FkeysEvents.F4;
          break;
        case Keys.F5:
          eventType = FkeysEvents.F5;
          break;
        case Keys.F6:
          eventType = FkeysEvents.F6;
          break;
        case Keys.F7:
          eventType = FkeysEvents.F7;
          break;
        case Keys.F8:
          eventType = FkeysEvents.F8;
          break;
        case Keys.F9:
          eventType = FkeysEvents.F9;
          break;
        case Keys.F10:
          eventType = FkeysEvents.F10;
          break;
        case Keys.F11:
          eventType = FkeysEvents.F11;
          break;
        case Keys.F12:
          eventType = FkeysEvents.F12;
          break;
        default:
          eventType = FkeysEvents.UnknownKey;
          break;
      }
      if (eventType != FkeysEvents.UnknownKey)
      {
        this.IntegrationHook((int) eventType);
        e.Handled = true;
      }
      else
        e.Handled = false;
    }

    private void LoadIntegrationMenu(bool enabledFlag)
    {
      this.integrationToolStripMenuItem.Enabled = enabledFlag;
      if (!enabledFlag)
        return;
      this.AddIntegrationStandardSubMenuItems();
    }

    private bool GetVbaIntegration()
    {
      bool bVal = false;
      return new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI).GetProfileBool("SETTINGS/INTEGRATION", "VBAINTEGRATION", out bVal, false) & bVal;
    }

    private void SetLastActiveFXIAProfileName(string profileName)
    {
      try
      {
        new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI).SetProfileValue("SETTINGS/INTEGRATION", "LastActiveFXIAProfileName", (object) profileName);
      }
      catch (Exception ex)
      {
      }
    }

    private string GetLastActiveFXIAProfileName()
    {
      string activeFxiaProfileName;
      new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI).GetProfileString("SETTINGS/INTEGRATION", "LastActiveFXIAProfileName", out activeFxiaProfileName);
      return activeFxiaProfileName;
    }

    private void AutoActivateLastActiveProfile(string who)
    {
      bool flag = false;
      try
      {
        string activeFxiaProfileName = this.GetLastActiveFXIAProfileName();
        if (string.IsNullOrEmpty(activeFxiaProfileName) || FedEx.Gsm.Common.Integration.SharedMethods.SharedMethods.IsEqual(activeFxiaProfileName, "None"))
          return;
        if (GuiData.EnableIntegration(false))
          flag = true;
        TransactionParser trxnParserIn = new TransactionParser();
        trxnParserIn.SetField(FieldID.TransactionType, "250");
        trxnParserIn.SetField(FieldID.TransactionID, who);
        if (FedEx.Gsm.Common.Integration.SharedMethods.SharedMethods.IsEqual(who, "OnMeterSwitch"))
          trxnParserIn.SetField(FieldID.IntegrationFieldIdList, "None");
        else
          trxnParserIn.SetField(FieldID.IntegrationFieldIdList, activeFxiaProfileName);
        trxnParserIn.SetField(FieldID.IntegrationShipScreenAction, "SETFXIAMENUITEM");
        TransactionParser trxnParserOut;
        new TwoFiftyTransactionTrafficCop().ProcessTransaction(trxnParserIn, out trxnParserOut);
        if (!FedEx.Gsm.Common.Integration.SharedMethods.SharedMethods.IsEqual(who, "OnMeterSwitch"))
          return;
        trxnParserIn.SetField(FieldID.IntegrationFieldIdList, activeFxiaProfileName);
        new TwoFiftyTransactionTrafficCop().ProcessTransaction(trxnParserIn, out trxnParserOut);
      }
      catch (Exception ex)
      {
      }
      finally
      {
        if (flag)
          GuiData.EnableIntegration(true);
      }
    }

    private void AddIntegrationStandardSubMenuItems()
    {
      ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("FedEx Integration Assistant");
      toolStripMenuItem1.Name = toolStripMenuItem1.Text;
      this.integrationToolStripMenuItem.DropDownItems.Add((ToolStripItem) toolStripMenuItem1);
      toolStripMenuItem1.Click += new EventHandler(this.OnSelectProfile);
      this.integrationToolStripMenuItem.DropDownItems.Add((ToolStripItem) new ToolStripSeparator());
      ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(GuiData.Languafier.Translate("None"));
      toolStripMenuItem2.Name = "None";
      this.integrationToolStripMenuItem.DropDownItems.Add((ToolStripItem) toolStripMenuItem2);
      toolStripMenuItem2.Click += new EventHandler(this.OnSelectProfile);
    }

    private void OnSelectProfile(object sender, EventArgs e)
    {
      ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem) sender;
      if (toolStripMenuItem.Name == "FedEx Integration Assistant")
        this._launcher.FXIAWizard.Launch();
      else if (toolStripMenuItem.Name == "None")
        this.SetIntegrationMenuDefaultState();
      else
        this.ActivateProfile(toolStripMenuItem.Name);
    }

    private void ResetBatchEdit()
    {
      this._holdFileIdList = (List<string>) null;
      this._holdFileIndex = 0;
      GuiData.BatchEditInProgress = false;
      StatusBarEventArgs args = new StatusBarEventArgs(string.Empty);
      if (this.UpdateStatusBar == null)
        return;
      this.UpdateStatusBar((object) this, (EventArgs) args);
    }

    private void ActivateProfile(string profileName)
    {
      if (((ToolStripMenuItem) this.integrationToolStripMenuItem.DropDownItems[profileName]).Checked)
        return;
      this.ResetBatchEdit();
      int count = this.integrationToolStripMenuItem.DropDownItems.Count;
      for (int index = 2; index < count; ++index)
        ((ToolStripMenuItem) this.integrationToolStripMenuItem.DropDownItems[index]).Checked = false;
      ((ToolStripMenuItem) this.integrationToolStripMenuItem.DropDownItems[profileName]).Checked = true;
      this.ActiveControl = (Control) null;
      if (FedEx.Gsm.Common.Integration.SharedMethods.SharedMethods.IsEqual(profileName, "None"))
      {
        GuiData.IsAnyFXIAProfileInUse = false;
        FocusExtender.FocusInfo.FocusBackColor = SharedIntegrationStuff.GetColorInConfigFile();
      }
      else
      {
        this.SetLastActiveFXIAProfileName(profileName);
        GuiData.IsAnyFXIAProfileInUse = true;
        FocusExtender.FocusInfo.FocusBackColor = this.integrationToolStripMenuItem.DropDownItems[profileName].ForeColor;
      }
      this.RefreshTitle(profileName);
      this.IntegrationHook(122, profileName);
    }

    private void SetIntegrationMenuDefaultState()
    {
      this.ResetBatchEdit();
      int count = this.integrationToolStripMenuItem.DropDownItems.Count;
      for (int index = 2; index < count; ++index)
        ((ToolStripMenuItem) this.integrationToolStripMenuItem.DropDownItems[index]).Checked = false;
      ((ToolStripMenuItem) this.integrationToolStripMenuItem.DropDownItems["None"]).Checked = true;
      this.SetLastActiveFXIAProfileName("None");
      GuiData.IsAnyFXIAProfileInUse = false;
      this.ActiveControl = (Control) null;
      FocusExtender.FocusInfo.FocusBackColor = SharedIntegrationStuff.GetColorInConfigFile();
      this.RefreshTitle((string) null);
      this.IntegrationHook(122, "None");
    }

    private void ClearFXIAMenu(TransactionParser trxnParserIn)
    {
      this.integrationToolStripMenuItem.DropDownItems.Clear();
      this.AddIntegrationStandardSubMenuItems();
      this.RefreshTitle((string) null);
    }

    private void AddFXIAMenuItem(TransactionParser trxnParserIn)
    {
      string text = string.Empty;
      int result = 2;
      GuiData.ConfigManager.SetProfileValue("SHIPNET2000/GUI/SETTINGS/INTEGRATION", "AnyFXIAProfilesInUse", (object) "Y");
      string[] strArray = trxnParserIn.GetField(FieldID.IntegrationFieldIdList).Split(',');
      for (int index = 0; index < strArray.Length; ++index)
      {
        string s = strArray[index];
        switch (index)
        {
          case 0:
            text = s;
            break;
          case 1:
            int.TryParse(s, out result);
            break;
        }
      }
      if (string.IsNullOrEmpty(text))
        return;
      ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(text);
      toolStripMenuItem.Name = toolStripMenuItem.Text;
      toolStripMenuItem.ForeColor = this.GetColor(result);
      this.integrationToolStripMenuItem.DropDownItems.Add((ToolStripItem) toolStripMenuItem);
      toolStripMenuItem.Click += new EventHandler(this.OnSelectProfile);
    }

    private void RemoveFXIAMenuItem(TransactionParser trxnParserIn)
    {
      string field = trxnParserIn.GetField(FieldID.IntegrationFieldIdList);
      if (!this.integrationToolStripMenuItem.DropDownItems.ContainsKey(field))
        return;
      int num = ((ToolStripMenuItem) this.integrationToolStripMenuItem.DropDownItems[field]).Checked ? 1 : 0;
      this.integrationToolStripMenuItem.DropDownItems.RemoveByKey(field);
      if (num == 0)
        return;
      this.RefreshTitle((string) null);
      this.IntegrationHook(122, "None");
    }

    private void SetFXIAMenuItem(TransactionParser trxnParserIn)
    {
      string field = trxnParserIn.GetField(FieldID.IntegrationFieldIdList);
      if (!this.integrationToolStripMenuItem.DropDownItems.ContainsKey(field))
        return;
      this.ActivateProfile(field);
    }

    private void RefreshTitle(string profileName)
    {
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = !Utility.IsNetworkClient ? GuiData.Languafier.Translate("SoftwareName") : GuiData.Languafier.Translate("NetworkClientName");
      string str2 = configManager.SoftwareVersion;
      if (!string.IsNullOrEmpty(str2))
        str2 = str2.Substring(str2.Length - 4);
      if (GuiData.CurrentAccount != null)
      {
        if (!string.IsNullOrEmpty(profileName) && !FedEx.Gsm.Common.Integration.SharedMethods.SharedMethods.IsEqual(profileName, "None"))
          stringBuilder.AppendFormat("{0} | v.{1}-a | {2} | {3}", (object) str1, (object) str2, (object) (GuiData.CurrentAccount.MeterNumber + " - " + GuiData.CurrentAccount.Description), (object) profileName);
        else if (this.FXIAUserProfileCount != 0)
          stringBuilder.AppendFormat("{0} | v.{1}-a | {2}", (object) str1, (object) str2, (object) (GuiData.CurrentAccount.MeterNumber + " - " + GuiData.CurrentAccount.Description));
        else
          stringBuilder.AppendFormat("{0} | v.{1} | {2}", (object) str1, (object) str2, (object) (GuiData.CurrentAccount.MeterNumber + " - " + GuiData.CurrentAccount.Description));
      }
      else
        stringBuilder.AppendFormat("{0} | v.{1}", (object) str1, (object) str2);
      this.Text = stringBuilder.ToString();
    }

    private int FXIAUserProfileCount
    {
      get
      {
        int userProfileCount = 0;
        if (this.integrationToolStripMenuItem.Enabled)
          userProfileCount = this.integrationToolStripMenuItem.DropDownItems.Count - this.integrationToolStripMenuItem.DropDownItems.IndexOfKey("None") - 1;
        return userProfileCount;
      }
    }

    public void SetColor(TransactionParser trxnParserIn)
    {
      if (GuiData.IsAnyFXIAProfileInUse)
        return;
      try
      {
        string upper = trxnParserIn.GetField(FieldID.IntegrationFieldIdList).ToUpper();
        System.Drawing.Color color;
        if (upper != null)
        {
          switch (upper.Length)
          {
            case 3:
              if (upper == "RED")
              {
                color = System.Drawing.Color.Red;
                goto label_40;
              }
              else
                break;
            case 4:
              switch (upper[0])
              {
                case 'A':
                  if (upper == "AQUA")
                  {
                    color = System.Drawing.Color.Aqua;
                    goto label_40;
                  }
                  else
                    break;
                case 'B':
                  if (upper == "BLUE")
                  {
                    color = System.Drawing.Color.Blue;
                    goto label_40;
                  }
                  else
                    break;
                case 'G':
                  if (upper == "GREY")
                  {
                    color = System.Drawing.Color.Gray;
                    goto label_40;
                  }
                  else
                    break;
                case 'L':
                  if (upper == "LIME")
                  {
                    color = System.Drawing.Color.Lime;
                    goto label_40;
                  }
                  else
                    break;
                case 'N':
                  if (upper == "NAVY")
                  {
                    color = System.Drawing.Color.Navy;
                    goto label_40;
                  }
                  else
                    break;
                case 'T':
                  if (upper == "TEAL")
                  {
                    color = System.Drawing.Color.Teal;
                    goto label_40;
                  }
                  else
                    break;
              }
              break;
            case 5:
              switch (upper[0])
              {
                case 'B':
                  if (upper == "BLACK")
                  {
                    color = System.Drawing.Color.Black;
                    goto label_40;
                  }
                  else
                    break;
                case 'G':
                  if (upper == "GREEN")
                  {
                    color = System.Drawing.Color.Green;
                    goto label_40;
                  }
                  else
                    break;
                case 'O':
                  if (upper == "OLIVE")
                  {
                    color = System.Drawing.Color.Olive;
                    goto label_40;
                  }
                  else
                    break;
                case 'W':
                  if (upper == "WHITE")
                  {
                    color = System.Drawing.Color.White;
                    goto label_40;
                  }
                  else
                    break;
              }
              break;
            case 6:
              switch (upper[0])
              {
                case 'M':
                  if (upper == "MAROON")
                  {
                    color = System.Drawing.Color.Maroon;
                    goto label_40;
                  }
                  else
                    break;
                case 'P':
                  if (upper == "PURPLE")
                  {
                    color = System.Drawing.Color.Purple;
                    goto label_40;
                  }
                  else
                    break;
                case 'S':
                  if (upper == "SILVER")
                  {
                    color = System.Drawing.Color.Silver;
                    goto label_40;
                  }
                  else
                    break;
                case 'Y':
                  if (upper == "YELLOW")
                  {
                    color = System.Drawing.Color.Yellow;
                    goto label_40;
                  }
                  else
                    break;
              }
              break;
            case 7:
              if (upper == "FUSHCIA")
              {
                color = System.Drawing.Color.Fuchsia;
                goto label_40;
              }
              else
                break;
          }
        }
        color = System.Drawing.Color.Blue;
label_40:
        FocusExtender.FocusInfo.FocusBackColor = color;
        if (GuiData.CurrentAccount == null)
          return;
        FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("SETTINGS/N{0}-{1}", (object) GuiData.CurrentAccount.MeterNumber, (object) GuiData.CurrentAccount.AccountNumber);
        string path = stringBuilder.ToString();
        string str = color.ToArgb().ToString();
        configManager.SetProfileValue(path, "FieldColor", (object) str);
      }
      catch (Exception ex)
      {
      }
    }

    private System.Drawing.Color GetColor(int colorId)
    {
      System.Drawing.Color blue = System.Drawing.Color.Blue;
      System.Drawing.Color color;
      switch (colorId)
      {
        case 0:
          color = System.Drawing.Color.Black;
          break;
        case 1:
          color = System.Drawing.Color.Red;
          break;
        case 2:
          color = System.Drawing.Color.Blue;
          break;
        case 3:
          color = System.Drawing.Color.Green;
          break;
        case 4:
          color = System.Drawing.Color.Purple;
          break;
        case 5:
          color = System.Drawing.Color.Maroon;
          break;
        case 6:
          color = System.Drawing.Color.Olive;
          break;
        case 7:
          color = System.Drawing.Color.Navy;
          break;
        case 8:
          color = System.Drawing.Color.Teal;
          break;
        case 9:
          color = System.Drawing.Color.Gray;
          break;
        case 10:
          color = System.Drawing.Color.Silver;
          break;
        case 11:
          color = System.Drawing.Color.Lime;
          break;
        case 12:
          color = System.Drawing.Color.Yellow;
          break;
        case 13:
          color = System.Drawing.Color.Fuchsia;
          break;
        case 14:
          color = System.Drawing.Color.Aqua;
          break;
        case 15:
          color = System.Drawing.Color.White;
          break;
        default:
          color = System.Drawing.Color.Blue;
          break;
      }
      return color;
    }

    public void OnUpdateStatusBar(object sender, StatusBarEventArgs args)
    {
      if (this.statusStripMain.InvokeRequired)
      {
        this.statusStripMain.Invoke((Delegate) new EventHandler<StatusBarEventArgs>(this.OnUpdateStatusBar), sender, (object) args);
      }
      else
      {
        this.toolStripStatusAreaHelp.Text = args.Message;
        if (args.Maximum > 0)
        {
          this.toolStripStatusBarProgress.Maximum = args.Maximum;
          if (!this.toolStripStatusBarProgress.Visible)
            this.toolStripStatusBarProgress.Visible = true;
        }
        else if (this.toolStripStatusBarProgress.Visible)
          this.toolStripStatusBarProgress.Visible = false;
        if (args.Step > 0)
        {
          this.toolStripStatusBarProgress.Step = args.Step;
          this.toolStripStatusBarProgress.PerformStep();
        }
        this.statusStripMain.Update();
      }
    }

    public void OnUpdateExtraServiceMessage(object sender, ExtraServiceMessageEventArgs args)
    {
      this.UpdateSpecialMessage(args.MessageType);
    }

    public void OnCurrentShipDateChanged(object sender, ShipDateChangedEventArgs args)
    {
      this.CheckUrsa(false);
    }

    private void UpdateSpecialMessage(int iMsgType)
    {
      string str = (string) null;
      switch (iMsgType)
      {
        case 2400:
          str = GuiData.Languafier.Translate("PakRateApplies");
          break;
        case 3176:
          str = GuiData.Languafier.Translate("OneLbRateApplies");
          break;
      }
      this.toolStripSpecialMessage.Visible = str != null;
      if (str == null)
        return;
      this.toolStripSpecialMessage.Text = str;
    }

    public void OnLoginFormExitButtonPressed(object sender, EventArgs e) => this.Close();

    private void shippingProfilesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (ShippingProfiles shippingProfiles = new ShippingProfiles())
      {
        int num = (int) shippingProfiles.ShowDialog((IWin32Window) this);
      }
    }

    private void Application_Idle(object sender, EventArgs e) => this.UpdateCurrentDateTime();

    private void UpdateCurrentDateTime()
    {
      ToolStripStatusLabel statusAreaDateTime = this.toolStripStatusAreaDateTime;
      DateTime dateTime = DateTime.Today;
      string longDateString = dateTime.ToLongDateString();
      dateTime = DateTime.Now;
      string shortTimeString = dateTime.ToShortTimeString();
      string str = longDateString + " " + shortTimeString;
      statusAreaDateTime.Text = str;
    }

    public void OnReferenceListChanged(object sender, EventArgs e) => this.RefreshReferences();

    private void RefreshReferences()
    {
      GuiData.References = (DataTable) null;
      DataTable output;
      GuiData.AppController.ShipEngine.GetDataList(GsmDataAccess.ListSpecification.ReferenceInfo_List, out output, new Error());
      GuiData.References = output;
      if (this.RePopulateReferenceCombos == null)
        return;
      this.RePopulateReferenceCombos((object) this, new EventArgs());
    }

    private static void CommMessageThread(object parent)
    {
      try
      {
        ShellForm shellForm = (ShellForm) parent;
        while (true)
        {
          if (WaitHandle.WaitAny(new WaitHandle[2]
          {
            (WaitHandle) shellForm.StopCommThreadEvent,
            (WaitHandle) shellForm.CommMessageAvailable
          }) != 0 && shellForm != null && !shellForm.IsDisposed)
          {
            shellForm.BeginInvoke((Delegate) shellForm.UpdateStatusBar, (object) shellForm, (object) new StatusBarEventArgs(ShellForm._currentCommMessage));
            SplashScreen.Status = ShellForm._currentCommMessage;
          }
          else
            break;
        }
      }
      catch (ObjectDisposedException ex)
      {
      }
      catch (InvalidOperationException ex)
      {
      }
      catch (NullReferenceException ex)
      {
      }
    }

    private void CommMsg(string msg)
    {
      this._commMessageAvailable.Set();
      ShellForm._currentCommMessage = msg;
    }

    private void testCommunicationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (CommunicationTestDialog communicationTestDialog = new CommunicationTestDialog(GuiData.AppController.ShipEngine))
      {
        int num = (int) communicationTestDialog.ShowDialog((IWin32Window) this);
      }
    }

    private void helpMeFedExToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      if (GuiData.CurrentAccount.Address != null && Utility.IsLacCountry(GuiData.CurrentAccount.Address.CountryCode))
      {
        switch (configManager.Language)
        {
          case "es-MX":
            configManager.GetProfileString("Settings", "HELPMEFEDEXSP", out empty, "http://www.fedex.com/mx/helpme/");
            break;
          case "fr-CA":
            configManager.GetProfileString("Settings", "HELPMEFEDEXFR", out empty, "http://www.fedex.com/mq/helpme/");
            break;
          case "pt-BR":
            configManager.GetProfileString("Settings", "HELPMEFEDEXPT", out empty, "http://www.fedex.com/br/helpme/");
            break;
          default:
            configManager.GetProfileString("Settings", "HELPMEFEDEXEN", out empty, "http://www.fedex.com/mx_english/helpme/");
            break;
        }
      }
      else if (GuiData.CurrentAccount.Address != null && GuiData.CurrentAccount.Address.CountryCode == "CA")
      {
        switch (configManager.Language)
        {
          case "fr-CA":
            configManager.GetProfileString("Settings", "FedExHelpMeURL", out empty, "http://www.fedex.com/ca_french/helpme/");
            break;
          default:
            configManager.GetProfileString("Settings", "FedExHelpMeURL", out empty, "http://www.fedex.com/ca_english/helpme");
            break;
        }
      }
      else
      {
        int num = configManager.Language == "en-US" ? 1 : 0;
        configManager.GetProfileString("Settings", "FedExHelpMeURL", out empty, "http://www.fedex.com/us/automation/fsm/remotesupport");
      }
      if (string.IsNullOrEmpty(empty) || (EventTopic) GuiData.EventBroker.GetTopic(EventBroker.Events.NavigateToUrl) == null)
        return;
      UrlEventArgs args = new UrlEventArgs(empty);
      if (this.GoToUrl == null)
        return;
      this.GoToUrl((object) this, (EventArgs) args);
    }

    public void OnFreightAccountListChanged(object sender, EventArgs e)
    {
      this.RefreshAllPluginHandlers(GuiData.CurrentAccount);
    }

    private void loadAShipmentFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog openFileDialog = new OpenFileDialog())
      {
        openFileDialog.Filter = "Shipment Files (*.xml)|*.xml|All files (*.*)|*.*";
        openFileDialog.FilterIndex = 0;
        if (openFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
          return;
        Shipment outputObj = (Shipment) null;
        GSMUtility.DeSerializeFromFile<Shipment>(out outputObj, openFileDialog.FileName);
        this.LoadShipmentToScreen(outputObj);
      }
    }

    private int LoadShipmentToScreen(Shipment shipment) => 0;

    private void forceReloadOfGuiDataListsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Utility.GuiDataListsFile = new XmlDocument();
      Utility.GuiDataListsFile.Load(new FedEx.Gsm.Common.ConfigManager.ConfigManager().InstallLocs.BinDirectory + "\\GuiDataLists.xml");
    }

    private void goToPassportScreenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.processTransactionsStripMenuItem_Click(sender, e);
    }

    private void helpTopicsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Help.ShowHelp((Control) this, Utility.GetHelpFile(), HelpNavigator.Index);
    }

    private void functionKeyHelpToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Help.ShowHelp((Control) this, Utility.GetHelpFile(), HelpNavigator.TopicId, (object) "4345");
    }

    public void OnDisplayURL(object sender, UrlEventArgs e) => Process.Start(e.URL);

    public void OnTrackingRequested(object sender, TrackRequestedEventArgs e)
    {
      ((IFedExGsmGuiTrackTabPlugin) Utility.DeckBroker.GetDeck(this.Name).GetPlugin("TrackView")).TrackIt(e.TrackingNumber);
    }

    public void OnAutoTrackDetailRequest(object sender, AutoTrackDetailRequestEventArgs e)
    {
      ((IFedExGsmGuiTrackTabPlugin) Utility.DeckBroker.GetDeck(this.Name).GetPlugin("TrackView")).TrackIt(e.TrackInfo, e.TrackReply);
    }

    private void interactiveHelpGuideToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string url = string.Empty;
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      configManager.GetProfileString("Settings", "FedExHelpDemo", out url);
      if (url.Length == 0)
      {
        url = "http://www.fedex.com/us/software/helpguide";
        configManager.SetProfileValue("Settings", "FedExHelpDemo", (object) url);
      }
      if (string.IsNullOrEmpty(url) || (EventTopic) GuiData.EventBroker.GetTopic(EventBroker.Events.NavigateToUrl) == null)
        return;
      UrlEventArgs args = new UrlEventArgs(url);
      if (this.GoToUrl == null)
        return;
      this.GoToUrl((object) this, (EventArgs) args);
    }

    private void addSystemAccountNumberToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (AddSystemAccount addSystemAccount = new AddSystemAccount())
      {
        if (addSystemAccount.ShowDialog((IWin32Window) this) != DialogResult.OK)
          return;
        this.LoadSystemAccountMenu(true);
        Utility.UpdateSeparators(this.toolStripMain);
        if (this.NewSystemAccount == null)
          return;
        this.NewSystemAccount((object) this, new AccountEventArgs()
        {
          OldAccount = (Account) null,
          NewAccount = new Account(addSystemAccount.RegisteredAccount)
        });
      }
    }

    private void EULAAcceptanceDetailMenuItem_Click(object sender, EventArgs e)
    {
      using (EULAAcceptanceDetail acceptanceDetail = new EULAAcceptanceDetail())
      {
        int num = (int) acceptanceDetail.ShowDialog((IWin32Window) this);
      }
    }

    private void printerSetupToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (PrinterSetup printerSetup = new PrinterSetup())
      {
        if (printerSetup.ShowDialog((IWin32Window) this) != DialogResult.OK || this.PrinterSettingsChanged == null)
          return;
        this.PrinterSettingsChanged((object) this, EventArgs.Empty);
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (msg.Msg == 256)
      {
        IntPtr num = msg.WParam;
        if (num.ToInt32() == 13)
        {
          num = msg.LParam;
          if ((num.ToInt32() & 16777216) != 0)
          {
            SendKeys.Send("{TAB}");
            return true;
          }
        }
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void OnFormAlignWaybill(Command cmd)
    {
      try
      {
        new InitializeStaticReport().InitForm(string.Empty, GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber, string.Empty, string.Empty, 1018, "repIntlAirwayBill.rdlc", 0);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "PrintWayBill()", ex.Message);
      }
    }

    private void OnUpdateFormAlignWaybill(Command cmd) => cmd.Enabled = this._bIntlAwbPrinterSet;

    private void OnFormAlignSDDG(Command cmd)
    {
      try
      {
        new InitializeStaticReport().InitForm((Shipment) null, GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "Form1421CReport()", ex.Message);
      }
    }

    private void OnUpdateFormAlignSDDG(Command cmd) => cmd.Enabled = this._bSddgPrinterSet;

    private void OnFormAlignOP900LG(Command cmd)
    {
      try
      {
        new InitializeStaticReport().InitForm((Shipment) null, 0, false, GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber, "OP900LG");
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "FormAlignOP900LGReport()", ex.Message);
      }
    }

    private void OnFormAlignOP900LL(Command cmd)
    {
      try
      {
        int num = (int) new FormAlignmentOP900LL().ShowDialog();
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "FormAlignOP900LLReport()", ex.Message);
      }
    }

    private void OnUpdateFormAlignOP900LG(Command cmd) => cmd.Enabled = this._bOP900LGPrinterSet;

    private void OnUpdateFormAlignOP900LL(Command cmd) => cmd.Enabled = this._bOP900LLPrinterSet;

    private void UpdatePrinterFlags()
    {
      this._bIntlAwbPrinterSet = Utility.IsDotMatrixPrinterSet(FormSetting.FormSettingTypes.FORM_SETTINGS_INTL_AIR_WAYBILL);
      this._bSddgPrinterSet = Utility.IsDotMatrixPrinterSet(FormSetting.FormSettingTypes.FORM_SETTINGS_SDDG);
      this._bOP900LGPrinterSet = Utility.IsDotMatrixPrinterSet(FormSetting.FormSettingTypes.FORM_SETTINGS_HAZ_CERT);
      this._bOP900LLPrinterSet = !Utility.IsDotMatrixPrinterSet(FormSetting.FormSettingTypes.FORM_SETTINGS_HAZ_CERT);
    }

    private void StartAutoCloseTimer()
    {
      RevenueServiceResponse autoCloseTime = GuiData.AppController.ShipEngine.GetAutoCloseTime(GuiData.CurrentAccount);
      if (autoCloseTime != null)
      {
        if (autoCloseTime.IsOperationOk)
          this.StartAutoCloseTimer(autoCloseTime.AutoCloseTime);
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm::StartAutoCloseTimer()", "RevenueServiceResponse(" + autoCloseTime.ErrorCode.ToString() + ") " + autoCloseTime.ErrorMessage);
      }
      else
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm::StartAutoCloseTimer()", "RevenueServiceResponse was null");
    }

    private void StartSmartPostAutoCloseTimer(DateTime autoCloseTime)
    {
      this._smartPostAutoCloseTimer.Stop();
      DateTime now = DateTime.Now;
      DateTime dateTime = new DateTime(now.Year, now.Month, now.Day, autoCloseTime.Hour, autoCloseTime.Minute, autoCloseTime.Second);
      if (now > dateTime)
        dateTime = dateTime.AddDays(1.0);
      TimeSpan timeSpan = dateTime - now;
      int num = 900000;
      this._smartPostAutoCloseTimer.Interval = timeSpan.TotalMilliseconds >= (double) num ? (int) (timeSpan.TotalMilliseconds - (double) num) : 2;
      this._smartPostAutoCloseTimer.Start();
    }

    private void StartAutoCloseTimer(DateTime autoCloseTime)
    {
      this._autoCloseTimer.Stop();
      DateTime now = DateTime.Now;
      DateTime dateTime = new DateTime(now.Year, now.Month, now.Day, autoCloseTime.Hour, autoCloseTime.Minute, autoCloseTime.Second);
      if (now > dateTime)
        dateTime = dateTime.AddDays(1.0);
      TimeSpan timeSpan = dateTime - now;
      int num = 900000;
      this._autoCloseTimer.Interval = timeSpan.TotalMilliseconds >= (double) num ? (int) (timeSpan.TotalMilliseconds - (double) num) : 2;
      this._autoCloseTimer.Start();
    }

    private void _autoCloseTimer_Tick(object sender, EventArgs e)
    {
      this._autoCloseTimer.Stop();
      CloseWaitDialog closeWaitDialog = new CloseWaitDialog();
      closeWaitDialog.StartPosition = FormStartPosition.CenterParent;
      if (DialogResult.Yes == closeWaitDialog.ShowDialog((IWin32Window) this))
      {
        RevenueServiceResponse revenueServiceResponse = GuiData.AppController.ShipEngine.DelayCloseTIme(GuiData.CurrentAccount, 30);
        if (revenueServiceResponse != null && revenueServiceResponse.IsOperationOk)
          this.StartAutoCloseTimer(revenueServiceResponse.AutoCloseTime);
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm::_autoCloseTimer_Tick", "RevenueServiceResponse(" + revenueServiceResponse.ErrorCode.ToString() + ") " + revenueServiceResponse.ErrorMessage);
      }
      else
      {
        this._autoCloseTimer.Interval = 85500000;
        this._autoCloseTimer.Start();
      }
    }

    private void sendClientMessageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (SendandRecieveMessageToClientDlg messageToClientDlg = new SendandRecieveMessageToClientDlg())
      {
        int num = (int) messageToClientDlg.ShowDialog((IWin32Window) this);
      }
    }

    private void _smartPostAutoCloseTimer_Tick(object sender, EventArgs e)
    {
      this._smartPostAutoCloseTimer.Stop();
      CloseWaitDialog closeWaitDialog = new CloseWaitDialog();
      closeWaitDialog.StartPosition = FormStartPosition.CenterParent;
      if (DialogResult.Yes == closeWaitDialog.ShowDialog((IWin32Window) this))
      {
        RevenueServiceResponse revenueServiceResponse = GuiData.AppController.ShipEngine.DelayCloseTIme(GuiData.CurrentAccount, 30);
        if (revenueServiceResponse != null && revenueServiceResponse.IsOperationOk)
          this.StartSmartPostAutoCloseTimer(revenueServiceResponse.AutoCloseTime);
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm::_smartPostCloseTimer_Tick", "RevenueServiceResponse(" + revenueServiceResponse.ErrorCode.ToString() + ") " + revenueServiceResponse.ErrorMessage);
      }
      else
      {
        this._smartPostAutoCloseTimer.Interval = 85500000;
        this._smartPostAutoCloseTimer.Start();
      }
    }

    private void contactInformationToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (ContactInfromation contactInfromation = new ContactInfromation())
      {
        int num = (int) contactInfromation.ShowDialog((IWin32Window) this);
      }
    }

    private void networkClientAdministrationToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (NetworkClientAdmin networkClientAdmin = new NetworkClientAdmin())
      {
        int num = (int) networkClientAdmin.ShowDialog((IWin32Window) this);
      }
    }

    public void OnEtdUploadFailed(object sender, EventArgs e)
    {
      using (EtdUploadFailedDialog uploadFailedDialog = new EtdUploadFailedDialog())
      {
        int num = (int) uploadFailedDialog.ShowDialog((IWin32Window) this);
      }
    }

    private void configureScaleToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (ScaleConfigurationDialog configurationDialog = new ScaleConfigurationDialog())
      {
        int num = (int) configurationDialog.ShowDialog((IWin32Window) this);
      }
    }

    private void ValidateNetworkClientVersionIfNetworkClient()
    {
      try
      {
        if (!GuiData.ConfigManager.IsNetworkClient)
          return;
        int num1 = 0;
        string swVersion1 = (string) null;
        string swVersion2 = (string) null;
        GuiData.AppController.ShipEngine.GetSoftwareVersion(SoftwareVersionRequestType.client, out swVersion2);
        GuiData.AppController.ShipEngine.GetSoftwareVersion(SoftwareVersionRequestType.server, out swVersion1);
        if (!string.IsNullOrEmpty(swVersion2) && !string.IsNullOrEmpty(swVersion1))
        {
          string[] strArray1 = swVersion2.Split('.');
          string[] strArray2 = swVersion1.Split('.');
          int result1;
          int.TryParse(strArray1[0], out result1);
          int result2;
          int.TryParse(strArray1[1], out result2);
          int result3;
          int.TryParse(strArray1[2], out result3);
          int result4;
          int.TryParse(strArray2[0], out result4);
          int result5;
          int.TryParse(strArray2[1], out result5);
          int result6;
          int.TryParse(strArray2[2], out result6);
          if (result1 > result4)
          {
            int num2 = (int) MessageBox.Show(" The FedEx ShipManager Client version is newer than the server.\n Software versions must match. \n Client Version: " + swVersion2 + " Server: " + swVersion1, "Incompatible Software Version Detected", MessageBoxButtons.OK);
          }
          else
            num1 = result1 != result4 ? -1 : (result2 <= result5 ? (result2 != result5 ? -1 : (result3 <= result6 ? (result3 != result6 ? -1 : 0) : 1)) : 1);
          switch (num1)
          {
            case -1:
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), "Exiting Gui. Network client is OLDER than server. Client Version: " + swVersion2 + " Server: " + swVersion1);
              NativeWindow owner = new NativeWindow();
              owner.AssignHandle(SplashScreen.Handle);
              try
              {
                if (DialogResult.Yes == MessageBox.Show((IWin32Window) owner, " The FedEx ShipManager Client version is out of date.\n An upgrade is required to continue.\n Do you want to upgrade now?", "Software Update Required", MessageBoxButtons.YesNo))
                {
                  FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.ValidateNetworkClientVersion", "User clicked yes, downloading upgrade.");
                  if (new NetworkClientUpgradeDlg().ShowDialog((IWin32Window) owner) == DialogResult.OK)
                    break;
                  FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.ValidateNetworkClientVersion", "Upgrade cancelled, exiting.");
                  Environment.Exit(0);
                  break;
                }
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.ValidateNetworkClientVersion", "User clicked no, upgrade declined, exiting.");
                Environment.Exit(0);
                break;
              }
              finally
              {
                owner.ReleaseHandle();
              }
            case 1:
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), "Exiting Gui. Network client is NEWER than server. Client Version: " + swVersion2 + " Server: " + swVersion1);
              int num3 = (int) MessageBox.Show(" The FedEx ShipManager Client version is newer than the server.\n Software versions must match. \n Client Version: " + swVersion2 + " Server: " + swVersion1 + "\nPlease contact your administrator.", "Incompatible Software Version Detected", MessageBoxButtons.OK);
              Environment.Exit(0);
              break;
          }
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, "ShellForm.ValidateNetworkClientVersion", string.Format("Failed to compare client ('{0}') and server ('{1}') version because one was missing.", (object) swVersion2, (object) swVersion1));
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), "Failed to compare network client Version with server version Error= " + ex.ToString());
      }
    }

    private void _clientRegistrationTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      GuiData.AppController.ShipEngine.RegisterForUnsolicitedServerMessages(GuiData.CurrentAccount);
    }

    private void OnCanAcceptInput(object sender, CancelEventArgs args)
    {
    }

    public void OnUpdatePrinterImages(object sender, PrinterActionEventArgs args)
    {
      if (GuiData.CurrentAccount == null)
        return;
      string labelPrinterName = PrintHelper.GetLabelPrinterName(GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber, GuiData.AppController.ShipEngine);
      switch (args.Action)
      {
        case PrinterActionEventArgs.PrinterAction.UploadAll:
          DataTableResponse dataList = GuiData.AppController.ShipEngine.GetDataList(FedEx.Gsm.ShipEngine.Entities.ListSpecification.ImageDoc_DropDown);
          if (dataList.HasError || dataList.DataTable == null)
            break;
          IEnumerator enumerator = dataList.DataTable.Rows.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              DataRow current = (DataRow) enumerator.Current;
              if (current["ImageID"] != null)
              {
                ImageDocument output;
                if (GuiData.AppController.ShipEngine.Retrieve<ImageDocument>(new ImageDocument()
                {
                  ImageId = current["ImageID"].ToString()
                }, out output, out Error _) == 1)
                  GuiData.AppController.PushImageToPrinter(output, labelPrinterName);
              }
            }
            break;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        case PrinterActionEventArgs.PrinterAction.EraseAll:
          GuiData.AppController.ClearCustomLabelCache(labelPrinterName);
          ZplHelper.EraseImages(labelPrinterName);
          ZplHelper.InitializeFlashMemory(labelPrinterName);
          break;
      }
    }

    public void OnGroundAccountNumberFormatChanged(
      object sender,
      GroundAccountNumberFormatChangedEventArgs args)
    {
      if (GuiData.CurrentAccount == null || !(GuiData.CurrentAccount.MeterNumber == args.Meter) || !(GuiData.CurrentAccount.AccountNumber == args.Account))
        return;
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.OnGroundAccountNumberFormatChanged", string.Format("Updating account {0} meter {1}, changing ground account number from {2} to {3}", (object) args.Account, (object) args.Meter, (object) GuiData.CurrentAccount.GroundAccountNumber, (object) args.GroundAccount));
      GuiData.CurrentAccount.GroundAccountNumber = args.GroundAccount;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      if (disposing)
      {
        foreach (AbstractPluginHandler listPluginHandler in this._listPluginHandlers)
          listPluginHandler.Dispose();
      }
      base.Dispose(disposing);
    }

    private void customLabelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (CustomLabelProfiles customLabelProfiles = new CustomLabelProfiles())
      {
        int num = (int) customLabelProfiles.ShowDialog((IWin32Window) this);
      }
    }

    private bool GetCustomLabelDiagnosticModeEnabled()
    {
      bool bVal;
      return GuiData.ConfigManager.GetProfileBool("SHIPNET2000/GUI/SETTINGS", "CUSTOMLABELDIAGNOSTICMODE", out bVal) & bVal;
    }

    public void OnGPRDataChanged(object sender, EventArgs e)
    {
      this.GenerateIntegrationDataFiles();
      Utility.ReloadGPRCache();
    }

    private void printDirectoryLabelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (GuiData.CurrentAccount == null)
        return;
      ZplHelper.LabelDirectory(PrintHelper.GetLabelPrinterName(GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber, GuiData.AppController.ShipEngine));
    }

    private void fontSizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem selected = sender as ToolStripMenuItem;
      if (MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate("LanguageChangedRestart"), string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
        return;
      this.UpdatePeerChecks((ToolStripItem) selected);
      GuiData.ConfigManager.SetProfileValue("SHIPNET2000/GUI/SETTINGS", "FontSize", selected.Tag);
      SystemShutdownRequest ssr = new SystemShutdownRequest();
      ssr.Restart = SystemShutdownRequest.RestartType.Soft;
      ssr.StopType = SystemShutdownRequest.StopStart.StopAllButServices;
      if (this.RequestShutdown == null)
        return;
      this.RequestShutdown((object) this, new ShutdownRequestEventArgs(ssr, true));
    }

    private void selfHelpMenuItem_Click(object sender, EventArgs e)
    {
      string localizedUrl = this._htmlModule.GetLocalizedURL("SELFHELP");
      if (string.IsNullOrEmpty(localizedUrl))
        return;
      TopicDelegate goToUrl = this.GoToUrl;
      if (goToUrl == null)
        return;
      goToUrl((object) this, (EventArgs) new UrlEventArgs(localizedUrl));
    }

    private void integrationHelpMenuItem_Click(object sender, EventArgs e)
    {
      string localizedUrl = this._htmlModule.GetLocalizedURL("INTEGRATIONHELP");
      if (string.IsNullOrEmpty(localizedUrl))
        return;
      TopicDelegate goToUrl = this.GoToUrl;
      if (goToUrl == null)
        return;
      goToUrl((object) this, (EventArgs) new UrlEventArgs(localizedUrl));
    }

    private void CheckUploadStatus()
    {
      if (!GuiData.AppController.ShipEngine.IsUploadOverdue())
        return;
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, nameof (CheckUploadStatus), "IsUploadOverdue returned true, displaying upload overdue dialog.");
      this.OnUploadOverdue((object) this, EventArgs.Empty);
    }

    public void OnUploadOverdue(object sender, EventArgs e)
    {
      using (UploadOverdueDialog uploadOverdueDialog = new UploadOverdueDialog(Utility.DeckBroker.GetDeck(nameof (ShellForm)).GetPlugin("CloseView") as IFedExGsmGuiCloseTabPlugin))
      {
        if (uploadOverdueDialog.ShowDialog((IWin32Window) this) == DialogResult.OK)
          return;
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_GUI, "ShellForm.OnUploadOverdue", "Exit now selected from upload overdue dialog, exiting...");
        Application.Exit();
      }
    }

    private void CheckZeroRates()
    {
      if (GuiData.CurrentAccount == null)
        return;
      ZeroRateErrorResponse rateErrorResponse = GuiData.AppController.ShipEngine.VerifyAcctRatesAvailableStatus(GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber, GuiData.CurrentAccount.IsGroundEnabled);
      if (rateErrorResponse.ZeroResponseList == null || rateErrorResponse.ZeroResponseList.Count <= 0)
        return;
      foreach (zeroRateError zeroResponse in rateErrorResponse.ZeroResponseList)
        Utility.DisplayError(zeroResponse.zeroRateErrorCode);
    }

    private void CheckZeroRatesReconcile()
    {
      ZeroRateErrorResponse rateErrorResponse = GuiData.AppController.ShipEngine.VerifyRatesAvailableStatus();
      if (rateErrorResponse.HasError)
        Utility.DisplayError(rateErrorResponse.Error);
      if (rateErrorResponse.ZeroResponseList == null || rateErrorResponse.ZeroResponseList.Count <= 0)
        return;
      foreach (zeroRateError zeroResponse in rateErrorResponse.ZeroResponseList)
        Utility.DisplayError(zeroResponse.zeroRateErrorCode);
    }

    private void restartFedExShipManagerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GuiData.Languafier.Translate("RestartFSMPrompt"), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      GuiData.AppController.ShipEngine.SystemShutdown(new SystemShutdownRequest()
      {
        StopType = SystemShutdownRequest.StopStart.StopAll,
        Restart = SystemShutdownRequest.RestartType.Soft
      });
    }

    private void manualReconcileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ManualReconcileMenuItem_Click", "User selected manual reconcole menu option, calling ForceReconcile.");
      Task.Run<ServiceResponse>((Func<ServiceResponse>) (() => GuiData.AppController.ShipEngine.ForceReconcile()));
    }

    private void howCanWeImproveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        string fileName;
        if (!GuiData.ConfigManager.GetProfileString("SHIPNET2000/GUI/SETTINGS", "HELPIMPROVEURL", out fileName))
          return;
        Process.Start(fileName);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "howCanWeImproveMenuItem", "Error launching url " + ex?.ToString());
      }
    }

    public void OnDownloadStarted(object sender, DownloadStartedEventArgs args)
    {
      this.toolStripDownloadProgress.Visible = true;
    }

    private void InitializeIDDCloseTimer(Account a)
    {
      if (a != null && a.IsTDEnabled)
      {
        IDDPreferences output;
        Error error;
        if (GuiData.AppController.ShipEngine.Retrieve<IDDPreferences>(new IDDPreferences()
        {
          MeterNumber = a.MeterNumber,
          AccountNumber = a.AccountNumber
        }, out output, out error) == 1 && (error == null || error.GoodToGo()) && output != null)
        {
          if (output.IDDCloseTime != DateTime.MinValue)
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.InitializeIDDCloseTimer", string.Format("IDD Close time is {0}.", (object) output.IDDCloseTime));
            TimeSpan zero = TimeSpan.Zero;
            DateTime dateTime = DateTime.Now;
            TimeSpan timeOfDay1 = dateTime.TimeOfDay;
            dateTime = output.IDDCloseTime;
            TimeSpan timeOfDay2 = dateTime.TimeOfDay;
            TimeSpan timeSpan;
            if (timeOfDay1 > timeOfDay2)
            {
              dateTime = DateTime.Now;
              dateTime = dateTime.AddDays(1.0);
              DateTime date = dateTime.Date;
              dateTime = output.IDDCloseTime;
              TimeSpan timeOfDay3 = dateTime.TimeOfDay;
              timeSpan = date + timeOfDay3 - DateTime.Now;
            }
            else
            {
              dateTime = output.IDDCloseTime;
              TimeSpan timeOfDay4 = dateTime.TimeOfDay;
              dateTime = DateTime.Now;
              TimeSpan timeOfDay5 = dateTime.TimeOfDay;
              timeSpan = timeOfDay4 - timeOfDay5;
            }
            this._iddCloseTimer.AutoReset = false;
            this._iddCloseTimer.Interval = timeSpan.TotalMilliseconds;
            this._iddCloseTimer.Start();
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.InitializeIDDCloseTimer", string.Format("Setting IDD Close timer for {0} in {1} ({2}).", (object) a.MeterNumber, (object) timeSpan, (object) (DateTime.Now + timeSpan)));
          }
          else
            this._iddCloseTimer.Stop();
        }
        else
          this._iddCloseTimer.Stop();
      }
      else
        this._iddCloseTimer.Stop();
    }

    private void _iddCloseTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      List<GsmFilter> filterList = new List<GsmFilter>();
      filterList.Add(new GsmFilter("AccountNumber", "=", (object) GuiData.CurrentAccount.AccountNumber));
      filterList.Add(new GsmFilter("MeterNumber", "=", (object) GuiData.CurrentAccount.MeterNumber));
      filterList.Add(new GsmFilter("Status", "<=", (object) 1));
      filterList.Add(new GsmFilter("PickupDate", ">=", (object) DateTime.Now.Date));
      List<GsmFilter> gsmFilterList = filterList;
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.Date;
      GsmFilter gsmFilter = new GsmFilter("PickupDate", "<", (object) dateTime.AddDays(1.0));
      gsmFilterList.Add(gsmFilter);
      filterList.Add(new GsmFilter("DeletedInd", "=", (object) false));
      Error error = new Error();
      DataTable output;
      GuiData.AppController.ShipEngine.GetDataList((object) null, FedEx.Gsm.ShipEngine.Entities.ListSpecification.MasterShipmentList, out output, filterList, (List<GsmSort>) null, (List<string>) null, error);
      if (output != null && output.Rows.Count > 0)
      {
        bool bVal;
        if (!GuiData.ConfigManager.GetProfileBool("SHIPNET2000/GUI/SETTINGS/USERPROMPTS", "UP_IDDCLOSEWARNING", out bVal))
          bVal = true;
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.IddCloseTimer_Elapsed", "Prompt for idd close set to " + bVal.ToString());
        if (bVal)
        {
          using (CustomMessageBox customMessageBox = new CustomMessageBox(GuiData.Languafier.Translate("IDDCloseWarningMessage"), GuiData.Languafier.Translate("IDDCloseWarningTitle")))
          {
            customMessageBox.ShowCustomButton = true;
            customMessageBox.CustomButtonText = GuiData.Languafier.Translate("IDDCloseWarningButtonText");
            customMessageBox.CustomButtonResult = DialogResult.Yes;
            DialogResult dialogResult = customMessageBox.ShowDialog((IWin32Window) this);
            if ((dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes) && customMessageBox.DontShowChecked)
              GuiData.ConfigManager.SetProfileValue("SHIPNET2000/GUI/SETTINGS/USERPROMPTS", "UP_IDDCLOSEWARNING", (object) "N");
            if (dialogResult == DialogResult.Yes)
              ShipView.ShowTDDashboard();
          }
        }
      }
      else
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.IddCloseTimer_Elapsed", "No open or complete IDD shipments, resetting timer");
      this.InitializeIDDCloseTimer(GuiData.CurrentAccount);
    }

    public void OnIDDPreferencesChanged(object sender, IDDPreferencesChangedEventArgs args)
    {
      Account currentAccount = GuiData.CurrentAccount;
      if (currentAccount == null || !(currentAccount.MeterNumber == args.MeterNumber) || !(currentAccount.AccountNumber == args.AccountNumber))
        return;
      this.InitializeIDDCloseTimer(currentAccount);
    }

    private void CheckForApiRegistrationRequired()
    {
      if (!GuiData.AppController.ShipEngine.IsApiRegistrationNeeded())
        return;
      this.authenticateInstallationToolStripMenuItem.Visible = true;
      string empty = string.Empty;
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUISETTINGS);
      configManager.GetProfileString(string.Empty, "APIREGISTRATIONSHOWN", out empty);
      if (!(empty != configManager.SoftwareVersion))
        return;
      configManager.SetProfileValue(string.Empty, "APIREGISTRATIONSHOWN", (object) configManager.SoftwareVersion);
      using (ApiRegistrationDialog registrationDialog = new ApiRegistrationDialog())
      {
        DialogResult dialogResult;
        do
        {
          dialogResult = registrationDialog.ShowDialog((IWin32Window) this);
        }
        while (dialogResult != DialogResult.OK && dialogResult != DialogResult.Ignore);
        if (dialogResult != DialogResult.OK)
          return;
        this.authenticateInstallationToolStripMenuItem.Visible = GuiData.AppController.ShipEngine.IsApiRegistrationNeeded();
      }
    }

    private void softwareManagementPortalToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string url;
      if (!new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUISETTINGS).GetProfileString(string.Empty, "SMP_URL", out url) || (EventTopic) GuiData.EventBroker.GetTopic(EventBroker.Events.NavigateToUrl) == null)
        return;
      UrlEventArgs args = new UrlEventArgs(url);
      if (this.GoToUrl == null)
        return;
      this.GoToUrl((object) this, (EventArgs) args);
    }

    private void authenticateInstallationToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (ApiRegistrationDialog registrationDialog = new ApiRegistrationDialog())
      {
        if (registrationDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
          return;
        this.authenticateInstallationToolStripMenuItem.Visible = GuiData.AppController.ShipEngine.IsApiRegistrationNeeded();
      }
    }

    private void DisplayTaxDisclaimer()
    {
      bool bVal;
      if (GuiData.CurrentAccount == null || !(GuiData.CurrentAccount.Address?.CountryCode == "CA") || !(!GuiData.ConfigManager.GetProfileBool("SHIPNET2000/GUI/SETTINGS/USERPROMPTS", "UP_TAXDISCLAIMER", out bVal) | bVal))
        return;
      using (CustomMessageBox customMessageBox = new CustomMessageBox(GuiData.Languafier.Translate("TaxDisclaimerMessage"), GuiData.Languafier.Translate("TaxDisclaimerTitle")))
      {
        if (customMessageBox.ShowDialog((IWin32Window) this) != DialogResult.OK || !customMessageBox.DontShowChecked)
          return;
        GuiData.ConfigManager.SetProfileValue("SHIPNET2000/GUI/SETTINGS/USERPROMPTS", "UP_TAXDISCLAIMER", (object) "N");
      }
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ShellForm));
      this.menuStripMain = new MenuStrip();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.goToPassportScreenToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      this.databasesToolStripMenuItem = new ToolStripMenuItem();
      this.customizeToolStripMenuItem = new ToolStripMenuItem();
      this.activeSystemAccountToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator6 = new ToolStripSeparator();
      this.shippingProfilesToolStripMenuItem = new ToolStripMenuItem();
      this.customLabelToolStripMenuItem = new ToolStripMenuItem();
      this.ltlFreightTemplateToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator7 = new ToolStripSeparator();
      this.customizeFieldsToolStripMenuItem = new ToolStripMenuItem();
      this.customizeUserPromptsToolStripMenuItem = new ToolStripMenuItem();
      this.recipientListToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator8 = new ToolStripSeparator();
      this.systemSettingsToolStripMenuItem = new ToolStripMenuItem();
      this.formsToolStripMenuItem = new ToolStripMenuItem();
      this.languageToolStripMenuItem = new ToolStripMenuItem();
      this.englishToolStripMenuItem = new ToolStripMenuItem();
      this.spanishToolStripMenuItem = new ToolStripMenuItem();
      this.frenchToolStripMenuItem = new ToolStripMenuItem();
      this.fontSizeToolStripMenuItem = new ToolStripMenuItem();
      this.standardToolStripMenuItem = new ToolStripMenuItem();
      this.largerToolStripMenuItem = new ToolStripMenuItem();
      this.largestToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.sendClientMessageToolStripMenuItem = new ToolStripMenuItem();
      this.utilitiesToolStripMenuItem = new ToolStripMenuItem();
      this.downloadToolStripMenuItem = new ToolStripMenuItem();
      this.installSoftwareUpdateToolStripMenuItem = new ToolStripMenuItem();
      this.restartFedExShipManagerToolStripMenuItem = new ToolStripMenuItem();
      this.manualReconcileToolStripMenuItem = new ToolStripMenuItem();
      this.uploadToolStripMenuItem = new ToolStripMenuItem();
      this.fedExAddressCheckerToolStripMenuItem = new ToolStripMenuItem();
      this.addrChkrPrefsMenuItem = new ToolStripMenuItem();
      this.addrChkrBatchMenuItem = new ToolStripMenuItem();
      this.printerSetupToolStripMenuItem = new ToolStripMenuItem();
      this.configureScaleToolStripMenuItem = new ToolStripMenuItem();
      this.addSystemAccountNumberToolStripMenuItem = new ToolStripMenuItem();
      this.authenticateInstallationToolStripMenuItem = new ToolStripMenuItem();
      this.softwareManagementPortalToolStripMenuItem = new ToolStripMenuItem();
      this.EULAAcceptanceDetailMenuItem = new ToolStripMenuItem();
      this.testCommunicationsToolStripMenuItem = new ToolStripMenuItem();
      this.formAlignToolStripMenuItem = new ToolStripMenuItem();
      this.fedExIntlAirWaybillToolStripMenuItem = new ToolStripMenuItem();
      this.shippersDecForDGSDDGToolStripMenuItem = new ToolStripMenuItem();
      this.oP900LGToolStripMenuItem = new ToolStripMenuItem();
      this.oP900LLToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator5 = new ToolStripSeparator();
      this.helpMeFedExToolStripMenuItem = new ToolStripMenuItem();
      this.printDirectoryLabelToolStripMenuItem = new ToolStripMenuItem();
      this.loadAShipmentFromXMLToolStripMenuItem = new ToolStripMenuItem();
      this.forceReloadOfGuiDataListsToolStripMenuItem = new ToolStripMenuItem();
      this.networkClientAdministrationToolStripMenuItem = new ToolStripMenuItem();
      this.integrationToolStripMenuItem = new ToolStripMenuItem();
      this.inboundToolStripMenuItem = new ToolStripMenuItem();
      this.passportToolStripMenuItem = new ToolStripMenuItem();
      this.fedexcomToolStripMenuItem = new ToolStripMenuItem();
      this.helpToolStripMenuItem = new ToolStripMenuItem();
      this.helpTopicsToolStripMenuItem = new ToolStripMenuItem();
      this.functionKeyHelpToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator4 = new ToolStripSeparator();
      this.selfHelpMenuItem = new ToolStripMenuItem();
      this.integrationHelpMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator9 = new ToolStripSeparator();
      this.aboutToolStripMenuItem = new ToolStripMenuItem();
      this.contactInformationToolStripMenuItem = new ToolStripMenuItem();
      this.howCanWeImproveToolStripMenuItem = new ToolStripMenuItem();
      this.fileMaintenanceToolStripMenuItem = new ToolStripMenuItem();
      this.backupToolStripMenuItem = new ToolStripMenuItem();
      this.restoreToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.toolStripMain = new ToolStrip();
      this.toolStripFedExLogo = new ToolStripLabel();
      this.label1 = new Label();
      this.statusStripMain = new StatusStrip();
      this.toolStripStatusAreaHelp = new ToolStripStatusLabel();
      this.toolStripStatusBarProgress = new ToolStripProgressBar();
      this.toolStripDownloadProgress = new ToolStripProgressBar();
      this.toolStripStatusUrsa = new ToolStripStatusLabel();
      this.toolStripStatusLabelDevMode = new ToolStripStatusLabel();
      this.toolStripStatusLabelDebugMode = new ToolStripStatusLabel();
      this.toolStripSpecialMessage = new ToolStripStatusLabel();
      this.toolStripStatusAreaDateTime = new ToolStripStatusLabel();
      this._autoCloseTimer = new System.Windows.Forms.Timer(this.components);
      this._bwSoftwareUpdates = new BackgroundWorker();
      this._smartPostAutoCloseTimer = new System.Windows.Forms.Timer(this.components);
      this._clientRegistrationTimer = new System.Timers.Timer();
      this.panelMain = new Panel();
      this._bwPurge = new BackgroundWorker();
      this.menuStripMain.SuspendLayout();
      this.toolStripMain.SuspendLayout();
      this.statusStripMain.SuspendLayout();
      this._clientRegistrationTimer.BeginInit();
      this.SuspendLayout();
      this.menuStripMain.Items.AddRange(new ToolStripItem[9]
      {
        (ToolStripItem) this.fileToolStripMenuItem,
        (ToolStripItem) this.databasesToolStripMenuItem,
        (ToolStripItem) this.customizeToolStripMenuItem,
        (ToolStripItem) this.utilitiesToolStripMenuItem,
        (ToolStripItem) this.integrationToolStripMenuItem,
        (ToolStripItem) this.inboundToolStripMenuItem,
        (ToolStripItem) this.passportToolStripMenuItem,
        (ToolStripItem) this.fedexcomToolStripMenuItem,
        (ToolStripItem) this.helpToolStripMenuItem
      });
      componentResourceManager.ApplyResources((object) this.menuStripMain, "menuStripMain");
      this.menuStripMain.Name = "menuStripMain";
      this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.goToPassportScreenToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.exitToolStripMenuItem
      });
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.fileToolStripMenuItem, "fileToolStripMenuItem");
      this.goToPassportScreenToolStripMenuItem.Name = "goToPassportScreenToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.goToPassportScreenToolStripMenuItem, "goToPassportScreenToolStripMenuItem");
      this.goToPassportScreenToolStripMenuItem.Click += new EventHandler(this.goToPassportScreenToolStripMenuItem_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.exitToolStripMenuItem, "exitToolStripMenuItem");
      this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
      this.databasesToolStripMenuItem.Name = "databasesToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.databasesToolStripMenuItem, "databasesToolStripMenuItem");
      this.customizeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[16]
      {
        (ToolStripItem) this.activeSystemAccountToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator6,
        (ToolStripItem) this.shippingProfilesToolStripMenuItem,
        (ToolStripItem) this.customLabelToolStripMenuItem,
        (ToolStripItem) this.ltlFreightTemplateToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator7,
        (ToolStripItem) this.customizeFieldsToolStripMenuItem,
        (ToolStripItem) this.customizeUserPromptsToolStripMenuItem,
        (ToolStripItem) this.recipientListToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator8,
        (ToolStripItem) this.systemSettingsToolStripMenuItem,
        (ToolStripItem) this.formsToolStripMenuItem,
        (ToolStripItem) this.languageToolStripMenuItem,
        (ToolStripItem) this.fontSizeToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.sendClientMessageToolStripMenuItem
      });
      this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.customizeToolStripMenuItem, "customizeToolStripMenuItem");
      this.activeSystemAccountToolStripMenuItem.Name = "activeSystemAccountToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.activeSystemAccountToolStripMenuItem, "activeSystemAccountToolStripMenuItem");
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      componentResourceManager.ApplyResources((object) this.toolStripSeparator6, "toolStripSeparator6");
      this.shippingProfilesToolStripMenuItem.Name = "shippingProfilesToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.shippingProfilesToolStripMenuItem, "shippingProfilesToolStripMenuItem");
      this.shippingProfilesToolStripMenuItem.Click += new EventHandler(this.shippingProfilesToolStripMenuItem_Click);
      this.customLabelToolStripMenuItem.Name = "customLabelToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.customLabelToolStripMenuItem, "customLabelToolStripMenuItem");
      this.customLabelToolStripMenuItem.Click += new EventHandler(this.customLabelToolStripMenuItem_Click);
      this.ltlFreightTemplateToolStripMenuItem.Name = "ltlFreightTemplateToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.ltlFreightTemplateToolStripMenuItem, "ltlFreightTemplateToolStripMenuItem");
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      componentResourceManager.ApplyResources((object) this.toolStripSeparator7, "toolStripSeparator7");
      this.customizeFieldsToolStripMenuItem.Name = "customizeFieldsToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.customizeFieldsToolStripMenuItem, "customizeFieldsToolStripMenuItem");
      this.customizeUserPromptsToolStripMenuItem.Image = (Image) Resources.customizeUserPrompts;
      this.customizeUserPromptsToolStripMenuItem.Name = "customizeUserPromptsToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.customizeUserPromptsToolStripMenuItem, "customizeUserPromptsToolStripMenuItem");
      this.customizeUserPromptsToolStripMenuItem.Click += new EventHandler(this.customizeUserPromptsToolStripMenuItem_Click);
      this.recipientListToolStripMenuItem.Name = "recipientListToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.recipientListToolStripMenuItem, "recipientListToolStripMenuItem");
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      componentResourceManager.ApplyResources((object) this.toolStripSeparator8, "toolStripSeparator8");
      this.systemSettingsToolStripMenuItem.Name = "systemSettingsToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.systemSettingsToolStripMenuItem, "systemSettingsToolStripMenuItem");
      this.systemSettingsToolStripMenuItem.Click += new EventHandler(this.systemSettingsToolStripMenuItem_Click);
      this.formsToolStripMenuItem.Name = "formsToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.formsToolStripMenuItem, "formsToolStripMenuItem");
      this.formsToolStripMenuItem.Click += new EventHandler(this.formsToolStripMenuItem_Click);
      this.languageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.englishToolStripMenuItem,
        (ToolStripItem) this.spanishToolStripMenuItem,
        (ToolStripItem) this.frenchToolStripMenuItem
      });
      this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.languageToolStripMenuItem, "languageToolStripMenuItem");
      this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.englishToolStripMenuItem, "englishToolStripMenuItem");
      this.englishToolStripMenuItem.Tag = (object) "en-US";
      this.englishToolStripMenuItem.Click += new EventHandler(this.Language_Click);
      this.spanishToolStripMenuItem.Name = "spanishToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.spanishToolStripMenuItem, "spanishToolStripMenuItem");
      this.spanishToolStripMenuItem.Tag = (object) "es-MX";
      this.spanishToolStripMenuItem.Click += new EventHandler(this.Language_Click);
      this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.frenchToolStripMenuItem, "frenchToolStripMenuItem");
      this.frenchToolStripMenuItem.Tag = (object) "fr-CA";
      this.frenchToolStripMenuItem.Click += new EventHandler(this.Language_Click);
      this.fontSizeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.standardToolStripMenuItem,
        (ToolStripItem) this.largerToolStripMenuItem,
        (ToolStripItem) this.largestToolStripMenuItem
      });
      this.fontSizeToolStripMenuItem.Name = "fontSizeToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.fontSizeToolStripMenuItem, "fontSizeToolStripMenuItem");
      this.standardToolStripMenuItem.Name = "standardToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.standardToolStripMenuItem, "standardToolStripMenuItem");
      this.standardToolStripMenuItem.Tag = (object) "0";
      this.standardToolStripMenuItem.Click += new EventHandler(this.fontSizeToolStripMenuItem_Click);
      this.largerToolStripMenuItem.Name = "largerToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.largerToolStripMenuItem, "largerToolStripMenuItem");
      this.largerToolStripMenuItem.Tag = (object) "1";
      this.largerToolStripMenuItem.Click += new EventHandler(this.fontSizeToolStripMenuItem_Click);
      this.largestToolStripMenuItem.Name = "largestToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.largestToolStripMenuItem, "largestToolStripMenuItem");
      this.largestToolStripMenuItem.Tag = (object) "2";
      this.largestToolStripMenuItem.Click += new EventHandler(this.fontSizeToolStripMenuItem_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      componentResourceManager.ApplyResources((object) this.toolStripSeparator2, "toolStripSeparator2");
      this.sendClientMessageToolStripMenuItem.Name = "sendClientMessageToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.sendClientMessageToolStripMenuItem, "sendClientMessageToolStripMenuItem");
      this.sendClientMessageToolStripMenuItem.Click += new EventHandler(this.sendClientMessageToolStripMenuItem_Click);
      this.utilitiesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[20]
      {
        (ToolStripItem) this.downloadToolStripMenuItem,
        (ToolStripItem) this.installSoftwareUpdateToolStripMenuItem,
        (ToolStripItem) this.restartFedExShipManagerToolStripMenuItem,
        (ToolStripItem) this.manualReconcileToolStripMenuItem,
        (ToolStripItem) this.uploadToolStripMenuItem,
        (ToolStripItem) this.fedExAddressCheckerToolStripMenuItem,
        (ToolStripItem) this.printerSetupToolStripMenuItem,
        (ToolStripItem) this.configureScaleToolStripMenuItem,
        (ToolStripItem) this.addSystemAccountNumberToolStripMenuItem,
        (ToolStripItem) this.authenticateInstallationToolStripMenuItem,
        (ToolStripItem) this.softwareManagementPortalToolStripMenuItem,
        (ToolStripItem) this.EULAAcceptanceDetailMenuItem,
        (ToolStripItem) this.testCommunicationsToolStripMenuItem,
        (ToolStripItem) this.formAlignToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator5,
        (ToolStripItem) this.helpMeFedExToolStripMenuItem,
        (ToolStripItem) this.printDirectoryLabelToolStripMenuItem,
        (ToolStripItem) this.loadAShipmentFromXMLToolStripMenuItem,
        (ToolStripItem) this.forceReloadOfGuiDataListsToolStripMenuItem,
        (ToolStripItem) this.networkClientAdministrationToolStripMenuItem
      });
      this.utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.utilitiesToolStripMenuItem, "utilitiesToolStripMenuItem");
      this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.downloadToolStripMenuItem, "downloadToolStripMenuItem");
      this.downloadToolStripMenuItem.Click += new EventHandler(this.downloadToolStripMenuItem_Click);
      componentResourceManager.ApplyResources((object) this.installSoftwareUpdateToolStripMenuItem, "installSoftwareUpdateToolStripMenuItem");
      this.installSoftwareUpdateToolStripMenuItem.Name = "installSoftwareUpdateToolStripMenuItem";
      this.installSoftwareUpdateToolStripMenuItem.Click += new EventHandler(this.installSoftwareUpdateToolStripMenuItem_Click);
      this.restartFedExShipManagerToolStripMenuItem.Name = "restartFedExShipManagerToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.restartFedExShipManagerToolStripMenuItem, "restartFedExShipManagerToolStripMenuItem");
      this.restartFedExShipManagerToolStripMenuItem.Click += new EventHandler(this.restartFedExShipManagerToolStripMenuItem_Click);
      this.manualReconcileToolStripMenuItem.Name = "manualReconcileToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.manualReconcileToolStripMenuItem, "manualReconcileToolStripMenuItem");
      this.manualReconcileToolStripMenuItem.Click += new EventHandler(this.manualReconcileToolStripMenuItem_Click);
      this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.uploadToolStripMenuItem, "uploadToolStripMenuItem");
      this.uploadToolStripMenuItem.Click += new EventHandler(this.uploadToolStripMenuItem_Click);
      this.fedExAddressCheckerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.addrChkrPrefsMenuItem,
        (ToolStripItem) this.addrChkrBatchMenuItem
      });
      this.fedExAddressCheckerToolStripMenuItem.Name = "fedExAddressCheckerToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.fedExAddressCheckerToolStripMenuItem, "fedExAddressCheckerToolStripMenuItem");
      this.addrChkrPrefsMenuItem.Name = "addrChkrPrefsMenuItem";
      componentResourceManager.ApplyResources((object) this.addrChkrPrefsMenuItem, "addrChkrPrefsMenuItem");
      this.addrChkrPrefsMenuItem.Click += new EventHandler(this.addrChkrPrefsMenuItem_Click);
      this.addrChkrBatchMenuItem.Name = "addrChkrBatchMenuItem";
      componentResourceManager.ApplyResources((object) this.addrChkrBatchMenuItem, "addrChkrBatchMenuItem");
      this.addrChkrBatchMenuItem.Click += new EventHandler(this.addrChkrBatchMenuItem_Click);
      this.printerSetupToolStripMenuItem.Name = "printerSetupToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.printerSetupToolStripMenuItem, "printerSetupToolStripMenuItem");
      this.printerSetupToolStripMenuItem.Click += new EventHandler(this.printerSetupToolStripMenuItem_Click);
      this.configureScaleToolStripMenuItem.Name = "configureScaleToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.configureScaleToolStripMenuItem, "configureScaleToolStripMenuItem");
      this.configureScaleToolStripMenuItem.Click += new EventHandler(this.configureScaleToolStripMenuItem_Click);
      this.addSystemAccountNumberToolStripMenuItem.Name = "addSystemAccountNumberToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.addSystemAccountNumberToolStripMenuItem, "addSystemAccountNumberToolStripMenuItem");
      this.addSystemAccountNumberToolStripMenuItem.Click += new EventHandler(this.addSystemAccountNumberToolStripMenuItem_Click);
      this.authenticateInstallationToolStripMenuItem.Name = "authenticateInstallationToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.authenticateInstallationToolStripMenuItem, "authenticateInstallationToolStripMenuItem");
      this.authenticateInstallationToolStripMenuItem.Click += new EventHandler(this.authenticateInstallationToolStripMenuItem_Click);
      this.softwareManagementPortalToolStripMenuItem.Name = "softwareManagementPortalToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.softwareManagementPortalToolStripMenuItem, "softwareManagementPortalToolStripMenuItem");
      this.softwareManagementPortalToolStripMenuItem.Click += new EventHandler(this.softwareManagementPortalToolStripMenuItem_Click);
      this.EULAAcceptanceDetailMenuItem.Name = "EULAAcceptanceDetailMenuItem";
      componentResourceManager.ApplyResources((object) this.EULAAcceptanceDetailMenuItem, "EULAAcceptanceDetailMenuItem");
      this.EULAAcceptanceDetailMenuItem.Click += new EventHandler(this.EULAAcceptanceDetailMenuItem_Click);
      this.testCommunicationsToolStripMenuItem.Name = "testCommunicationsToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.testCommunicationsToolStripMenuItem, "testCommunicationsToolStripMenuItem");
      this.testCommunicationsToolStripMenuItem.Click += new EventHandler(this.testCommunicationsToolStripMenuItem_Click);
      this.formAlignToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.fedExIntlAirWaybillToolStripMenuItem,
        (ToolStripItem) this.shippersDecForDGSDDGToolStripMenuItem,
        (ToolStripItem) this.oP900LGToolStripMenuItem,
        (ToolStripItem) this.oP900LLToolStripMenuItem
      });
      this.formAlignToolStripMenuItem.Name = "formAlignToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.formAlignToolStripMenuItem, "formAlignToolStripMenuItem");
      this.fedExIntlAirWaybillToolStripMenuItem.Name = "fedExIntlAirWaybillToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.fedExIntlAirWaybillToolStripMenuItem, "fedExIntlAirWaybillToolStripMenuItem");
      this.shippersDecForDGSDDGToolStripMenuItem.Name = "shippersDecForDGSDDGToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.shippersDecForDGSDDGToolStripMenuItem, "shippersDecForDGSDDGToolStripMenuItem");
      this.oP900LGToolStripMenuItem.Name = "oP900LGToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.oP900LGToolStripMenuItem, "oP900LGToolStripMenuItem");
      this.oP900LLToolStripMenuItem.Name = "oP900LLToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.oP900LLToolStripMenuItem, "oP900LLToolStripMenuItem");
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      componentResourceManager.ApplyResources((object) this.toolStripSeparator5, "toolStripSeparator5");
      this.helpMeFedExToolStripMenuItem.Name = "helpMeFedExToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.helpMeFedExToolStripMenuItem, "helpMeFedExToolStripMenuItem");
      this.helpMeFedExToolStripMenuItem.Click += new EventHandler(this.helpMeFedExToolStripMenuItem_Click);
      this.printDirectoryLabelToolStripMenuItem.Name = "printDirectoryLabelToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.printDirectoryLabelToolStripMenuItem, "printDirectoryLabelToolStripMenuItem");
      this.printDirectoryLabelToolStripMenuItem.Click += new EventHandler(this.printDirectoryLabelToolStripMenuItem_Click);
      this.loadAShipmentFromXMLToolStripMenuItem.Name = "loadAShipmentFromXMLToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.loadAShipmentFromXMLToolStripMenuItem, "loadAShipmentFromXMLToolStripMenuItem");
      this.loadAShipmentFromXMLToolStripMenuItem.Click += new EventHandler(this.loadAShipmentFromXMLToolStripMenuItem_Click);
      this.forceReloadOfGuiDataListsToolStripMenuItem.Name = "forceReloadOfGuiDataListsToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.forceReloadOfGuiDataListsToolStripMenuItem, "forceReloadOfGuiDataListsToolStripMenuItem");
      this.forceReloadOfGuiDataListsToolStripMenuItem.Click += new EventHandler(this.forceReloadOfGuiDataListsToolStripMenuItem_Click);
      this.networkClientAdministrationToolStripMenuItem.Name = "networkClientAdministrationToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.networkClientAdministrationToolStripMenuItem, "networkClientAdministrationToolStripMenuItem");
      this.networkClientAdministrationToolStripMenuItem.Click += new EventHandler(this.networkClientAdministrationToolStripMenuItem_Click);
      componentResourceManager.ApplyResources((object) this.integrationToolStripMenuItem, "integrationToolStripMenuItem");
      this.integrationToolStripMenuItem.Name = "integrationToolStripMenuItem";
      this.inboundToolStripMenuItem.Name = "inboundToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.inboundToolStripMenuItem, "inboundToolStripMenuItem");
      this.passportToolStripMenuItem.Name = "passportToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.passportToolStripMenuItem, "passportToolStripMenuItem");
      componentResourceManager.ApplyResources((object) this.fedexcomToolStripMenuItem, "fedexcomToolStripMenuItem");
      this.fedexcomToolStripMenuItem.Name = "fedexcomToolStripMenuItem";
      this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[9]
      {
        (ToolStripItem) this.helpTopicsToolStripMenuItem,
        (ToolStripItem) this.functionKeyHelpToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.selfHelpMenuItem,
        (ToolStripItem) this.integrationHelpMenuItem,
        (ToolStripItem) this.toolStripSeparator9,
        (ToolStripItem) this.aboutToolStripMenuItem,
        (ToolStripItem) this.contactInformationToolStripMenuItem,
        (ToolStripItem) this.howCanWeImproveToolStripMenuItem
      });
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.helpToolStripMenuItem, "helpToolStripMenuItem");
      this.helpTopicsToolStripMenuItem.Name = "helpTopicsToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.helpTopicsToolStripMenuItem, "helpTopicsToolStripMenuItem");
      this.helpTopicsToolStripMenuItem.Click += new EventHandler(this.helpTopicsToolStripMenuItem_Click);
      this.functionKeyHelpToolStripMenuItem.Name = "functionKeyHelpToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.functionKeyHelpToolStripMenuItem, "functionKeyHelpToolStripMenuItem");
      this.functionKeyHelpToolStripMenuItem.Click += new EventHandler(this.functionKeyHelpToolStripMenuItem_Click);
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      componentResourceManager.ApplyResources((object) this.toolStripSeparator4, "toolStripSeparator4");
      this.selfHelpMenuItem.Name = "selfHelpMenuItem";
      componentResourceManager.ApplyResources((object) this.selfHelpMenuItem, "selfHelpMenuItem");
      this.selfHelpMenuItem.Click += new EventHandler(this.selfHelpMenuItem_Click);
      this.integrationHelpMenuItem.Name = "integrationHelpMenuItem";
      componentResourceManager.ApplyResources((object) this.integrationHelpMenuItem, "integrationHelpMenuItem");
      this.integrationHelpMenuItem.Click += new EventHandler(this.integrationHelpMenuItem_Click);
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      componentResourceManager.ApplyResources((object) this.toolStripSeparator9, "toolStripSeparator9");
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
      this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
      this.contactInformationToolStripMenuItem.Name = "contactInformationToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.contactInformationToolStripMenuItem, "contactInformationToolStripMenuItem");
      this.contactInformationToolStripMenuItem.Click += new EventHandler(this.contactInformationToolStripMenuItem_Click);
      this.howCanWeImproveToolStripMenuItem.Name = "howCanWeImproveToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.howCanWeImproveToolStripMenuItem, "howCanWeImproveToolStripMenuItem");
      this.howCanWeImproveToolStripMenuItem.Click += new EventHandler(this.howCanWeImproveToolStripMenuItem_Click);
      this.fileMaintenanceToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.backupToolStripMenuItem,
        (ToolStripItem) this.restoreToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator3
      });
      componentResourceManager.ApplyResources((object) this.fileMaintenanceToolStripMenuItem, "fileMaintenanceToolStripMenuItem");
      this.fileMaintenanceToolStripMenuItem.Name = "fileMaintenanceToolStripMenuItem";
      this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.backupToolStripMenuItem, "backupToolStripMenuItem");
      this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.restoreToolStripMenuItem, "restoreToolStripMenuItem");
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      componentResourceManager.ApplyResources((object) this.toolStripSeparator3, "toolStripSeparator3");
      this.toolStripMain.GripStyle = ToolStripGripStyle.Hidden;
      this.toolStripMain.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.toolStripFedExLogo
      });
      componentResourceManager.ApplyResources((object) this.toolStripMain, "toolStripMain");
      this.toolStripMain.Name = "toolStripMain";
      this.toolStripMain.Stretch = true;
      this.toolStripFedExLogo.Alignment = ToolStripItemAlignment.Right;
      this.toolStripFedExLogo.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripFedExLogo.Image = (Image) Resources.Logo;
      componentResourceManager.ApplyResources((object) this.toolStripFedExLogo, "toolStripFedExLogo");
      this.toolStripFedExLogo.Name = "toolStripFedExLogo";
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.label1.Tag = (object) "You should never see this!";
      this.statusStripMain.Items.AddRange(new ToolStripItem[8]
      {
        (ToolStripItem) this.toolStripStatusAreaHelp,
        (ToolStripItem) this.toolStripStatusBarProgress,
        (ToolStripItem) this.toolStripDownloadProgress,
        (ToolStripItem) this.toolStripStatusUrsa,
        (ToolStripItem) this.toolStripStatusLabelDevMode,
        (ToolStripItem) this.toolStripStatusLabelDebugMode,
        (ToolStripItem) this.toolStripSpecialMessage,
        (ToolStripItem) this.toolStripStatusAreaDateTime
      });
      componentResourceManager.ApplyResources((object) this.statusStripMain, "statusStripMain");
      this.statusStripMain.Name = "statusStripMain";
      this.toolStripStatusAreaHelp.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.toolStripStatusAreaHelp.Name = "toolStripStatusAreaHelp";
      componentResourceManager.ApplyResources((object) this.toolStripStatusAreaHelp, "toolStripStatusAreaHelp");
      this.toolStripStatusAreaHelp.Spring = true;
      this.toolStripStatusBarProgress.Name = "toolStripStatusBarProgress";
      componentResourceManager.ApplyResources((object) this.toolStripStatusBarProgress, "toolStripStatusBarProgress");
      this.toolStripDownloadProgress.Name = "toolStripDownloadProgress";
      componentResourceManager.ApplyResources((object) this.toolStripDownloadProgress, "toolStripDownloadProgress");
      this.toolStripDownloadProgress.Style = ProgressBarStyle.Marquee;
      this.toolStripStatusUrsa.BorderSides = ToolStripStatusLabelBorderSides.All;
      this.toolStripStatusUrsa.DisplayStyle = ToolStripItemDisplayStyle.Text;
      componentResourceManager.ApplyResources((object) this.toolStripStatusUrsa, "toolStripStatusUrsa");
      this.toolStripStatusUrsa.ForeColor = System.Drawing.Color.Red;
      this.toolStripStatusUrsa.Name = "toolStripStatusUrsa";
      this.toolStripStatusLabelDevMode.BackColor = SystemColors.Control;
      this.toolStripStatusLabelDevMode.BorderSides = ToolStripStatusLabelBorderSides.All;
      this.toolStripStatusLabelDevMode.DisplayStyle = ToolStripItemDisplayStyle.Text;
      componentResourceManager.ApplyResources((object) this.toolStripStatusLabelDevMode, "toolStripStatusLabelDevMode");
      this.toolStripStatusLabelDevMode.ForeColor = SystemColors.ControlText;
      this.toolStripStatusLabelDevMode.Name = "toolStripStatusLabelDevMode";
      this.toolStripStatusLabelDebugMode.BackColor = SystemColors.Control;
      this.toolStripStatusLabelDebugMode.BorderSides = ToolStripStatusLabelBorderSides.All;
      this.toolStripStatusLabelDebugMode.DisplayStyle = ToolStripItemDisplayStyle.Text;
      componentResourceManager.ApplyResources((object) this.toolStripStatusLabelDebugMode, "toolStripStatusLabelDebugMode");
      this.toolStripStatusLabelDebugMode.ForeColor = SystemColors.ControlText;
      this.toolStripStatusLabelDebugMode.Name = "toolStripStatusLabelDebugMode";
      componentResourceManager.ApplyResources((object) this.toolStripSpecialMessage, "toolStripSpecialMessage");
      this.toolStripSpecialMessage.Name = "toolStripSpecialMessage";
      this.toolStripStatusAreaDateTime.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.toolStripStatusAreaDateTime.Name = "toolStripStatusAreaDateTime";
      componentResourceManager.ApplyResources((object) this.toolStripStatusAreaDateTime, "toolStripStatusAreaDateTime");
      this._autoCloseTimer.Tick += new EventHandler(this._autoCloseTimer_Tick);
      this._bwSoftwareUpdates.DoWork += new DoWorkEventHandler(this.OnDoWork_SoftwareUpdates);
      this._bwSoftwareUpdates.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.OnWorkCompleted_SoftwareUpdates);
      this._smartPostAutoCloseTimer.Tick += new EventHandler(this._smartPostAutoCloseTimer_Tick);
      this._clientRegistrationTimer.Interval = 600000.0;
      this._clientRegistrationTimer.SynchronizingObject = (ISynchronizeInvoke) this;
      this._clientRegistrationTimer.Elapsed += new ElapsedEventHandler(this._clientRegistrationTimer_Elapsed);
      componentResourceManager.ApplyResources((object) this.panelMain, "panelMain");
      this.panelMain.Name = "panelMain";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.panelMain);
      this.Controls.Add((Control) this.statusStripMain);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.toolStripMain);
      this.Controls.Add((Control) this.menuStripMain);
      this.KeyPreview = true;
      this.MainMenuStrip = this.menuStripMain;
      this.Name = nameof (ShellForm);
      this.Load += new EventHandler(this.ShellForm_Load);
      this.KeyDown += new KeyEventHandler(this.ShellForm_KeyDown);
      this.menuStripMain.ResumeLayout(false);
      this.menuStripMain.PerformLayout();
      this.toolStripMain.ResumeLayout(false);
      this.toolStripMain.PerformLayout();
      this.statusStripMain.ResumeLayout(false);
      this.statusStripMain.PerformLayout();
      this._clientRegistrationTimer.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private int DoStartupStuff()
    {
      int num = 1;
      this.DownloadStuff();
      if (!Utility.DownloadRatesOnBootup)
      {
        using (ShellForm.PurgeLogic purgeLogic = (ShellForm.PurgeLogic) new ShellForm.ShipmentPurgeLogic(this._bwPurge))
          purgeLogic.ProcessPurge(GuiData.CurrentAccount);
        using (ShellForm.PurgeLogic purgeLogic = (ShellForm.PurgeLogic) new ShellForm.FreightPurgeLogic(this._bwPurge))
          purgeLogic.ProcessPurge(GuiData.CurrentAccount);
      }
      return num;
    }

    private int DownloadStuff()
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.DownloadStuff", "Enter process downloads for current account");
      int num1 = ShellForm.ProcessDownloadsForCurrentAccount();
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.DownloadStuff", "Leave process downloads for current account");
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.DownloadStuff", "Enter process downloads for all accounts");
      int num2 = ShellForm.ProcessDownloadsForAllAccounts();
      int num3 = num1 + num2;
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "ShellForm.DownloadStuff", "Leave process downloads for all accounts");
      if (num3 > 0)
        this.ReloadCurrentAccount();
      return 1;
    }

    private static int ProcessDownloadsForAllAccounts()
    {
      int num = 0;
      bool bVal = false;
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      if (((configManager.IsNetworkClient ? 0 : (configManager.GetProfileBool("Settings", "DownloadPoliciesOnBootup", out bVal) ? 1 : 0)) & (bVal ? 1 : 0)) != 0)
      {
        Error error = new Error();
        DataTable output;
        if (GuiData.AppController.ShipEngine.GetDataList((object) null, FedEx.Gsm.ShipEngine.Entities.ListSpecification.AccountList, out output, error) == 1 && output != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) output.Rows)
          {
            string account = row["Account"] as string;
            string meter = row["Meter"] as string;
            if (meter != GuiData.CurrentAccount.MeterNumber)
            {
              ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.ProcessDownloadRequests(new List<AdminDownloadRequest>()
              {
                new AdminDownloadRequest(meter, account, AdminDownloadRequest.RequestType.DomesticRates),
                new AdminDownloadRequest(meter, account, AdminDownloadRequest.RequestType.CheckDownloadIMPB),
                new AdminDownloadRequest(meter, account, AdminDownloadRequest.RequestType.AccountPolicyGrid)
              }, new Error());
              if (serviceResponse.HasError)
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm_Load.DownloadStuff", string.Format("Downloads failed with error {0} {1}", (object) serviceResponse.ErrorCode, (object) serviceResponse.ErrorMessage));
              else if (account == GuiData.CurrentAccount.AccountNumber && meter == GuiData.CurrentAccount.MeterNumber)
                ++num;
            }
          }
          configManager.SetProfileValue("Settings", "DownloadPoliciesOnBootup", (object) "N");
        }
      }
      return num;
    }

    private static int ProcessDownloadsForCurrentAccount()
    {
      bool bVal = false;
      bool flag = false;
      string meterNumber = GuiData.CurrentAccount.MeterNumber;
      string accountNumber = GuiData.CurrentAccount.AccountNumber;
      FedEx.Gsm.Common.ConfigManager.ConfigManager cm = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      List<AdminDownloadRequest> requestList = new List<AdminDownloadRequest>();
      List<AdminDownloadRequest> adminDownloadRequestList = new List<AdminDownloadRequest>();
      if (cm.GetProfileBool("Settings", "DownloadRatesOnBootup", out bVal) & bVal)
      {
        flag = true;
        cm.SetProfileValue("Settings", "DownloadRatesOnBootup", (object) "N");
      }
      if (cm.GetProfileBool("Settings", "DownloadGroundRatesOnBootup", out bVal) & bVal && GuiData.CurrentAccount.IsGroundEnabled)
      {
        flag = true;
        cm.SetProfileValue("Settings", "DownloadGroundRatesOnBootup", (object) "N");
      }
      if (cm.GetProfileBool("Settings", "DownloadTrkNumsOnBootup", out bVal) & bVal)
      {
        flag = true;
        cm.SetProfileValue("Settings", "DownloadTrkNumsOnBootup", (object) "N");
      }
      if (cm.GetProfileBool("Settings", "DownloadURSAOnBootup", out bVal) & bVal)
      {
        flag = true;
        cm.SetProfileValue("Settings", "DownloadURSAOnBootup", (object) "N");
      }
      if (cm.GetProfileBool("Settings", "DownloadImpbOnBootup", out bVal) & bVal)
      {
        flag = true;
        cm.SetProfileValue("Settings", "DownloadImpbOnBootup", (object) "N");
      }
      if (cm.GetProfileBool("Settings", "DownloadHalOnBootup", out bVal) & bVal)
      {
        flag = ((flag ? 1 : 0) | 1) != 0;
        cm.SetProfileValue("Settings", "DownloadHalOnBootup", (object) "N");
      }
      if (cm.GetProfileBool("Settings", "DownloadHazOnBootup", out bVal) & bVal)
      {
        flag = true;
        cm.SetProfileValue("Settings", "DownloadHazOnBootup", (object) "N");
      }
      if (flag)
      {
        requestList.Add(new AdminDownloadRequest(meterNumber, accountNumber, AdminDownloadRequest.RequestType.Everything));
        if (Utility.IsSoftwareOnly)
        {
          ShellForm.IgnoreSuccessfulDownloads(cm, meterNumber, accountNumber, "WizDownloadedTracking", AdminDownloadRequest.RequestType.ExpressTrackingNumberRange, adminDownloadRequestList);
          ShellForm.IgnoreSuccessfulDownloads(cm, meterNumber, accountNumber, "WizDownloadedDomRates", AdminDownloadRequest.RequestType.DomesticRates, adminDownloadRequestList);
          ShellForm.IgnoreSuccessfulDownloads(cm, meterNumber, accountNumber, "WizDownloadedIntlRates", AdminDownloadRequest.RequestType.IntlRates, adminDownloadRequestList);
          ShellForm.IgnoreSuccessfulDownloads(cm, meterNumber, accountNumber, "WizDownloadedURSA", AdminDownloadRequest.RequestType.Ursa_Generic, adminDownloadRequestList);
        }
      }
      if (requestList.Count > 0)
      {
        SplashScreen.Show();
        SplashScreen.Status = GuiData.Languafier.Translate("d28824");
        ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.ProcessDownloadRequests(requestList, adminDownloadRequestList);
        if (serviceResponse.HasError)
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm_Load.DownloadStuff", string.Format("Downloads failed with error {0} {1}", (object) serviceResponse.ErrorCode, (object) serviceResponse.ErrorMessage));
      }
      return requestList.Count;
    }

    private static void IgnoreSuccessfulDownloads(
      FedEx.Gsm.Common.ConfigManager.ConfigManager cm,
      string meter,
      string account,
      string configKey,
      AdminDownloadRequest.RequestType request,
      List<AdminDownloadRequest> ignore)
    {
      bool bVal;
      if (!(cm.GetProfileBool("Settings", configKey, out bVal) & bVal))
        return;
      ignore.Add(new AdminDownloadRequest(meter, account, request));
      cm.SetProfileValue("Settings", configKey, (object) false);
    }

    private void ReloadCurrentAccount()
    {
      Account output;
      Error error;
      if (1 == GuiData.AppController.ShipEngine.Retrieve<Account>(GuiData.CurrentAccount, out output, out error))
      {
        AccountEventArgs args = new AccountEventArgs();
        args.OldAccount = GuiData.CurrentAccount;
        args.NewAccount = output;
        GuiData.CurrentAccount = output;
        this.CurrentAccountChanged((object) this, (EventArgs) args);
      }
      else
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm_Load.DownloadStuff", string.Format("Failed to retrieve account so we could update it after downloading via admin. {0} {1}", (object) error.Code, (object) error.Message));
    }

    private void MigrateValidatorProfiles()
    {
      try
      {
        FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUISETTINGS);
        bool bVal;
        if (!configManager.IsNetworkClient || configManager.GetProfileBool(string.Empty, "ValidatorProfilesMigrated", out bVal) && bVal)
          return;
        string binDirectory = DataFileLocations.Instance.BinDirectory;
        string str = Path.Combine(DataFileLocations.Instance.CLSLabelDirectory, "data\\custom");
        if (!Directory.Exists(str))
          return;
        new UpgradeValidator().UpgradeValidatorFiles(new ValidatorMigrationDetails(binDirectory, str, true, configManager.IsNetworkClient, configManager.NetworkClientID.ToString()));
        configManager.SetProfileValue(string.Empty, "ValidatorProfilesMigrated", (object) "Y");
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm.MigrateValidatorProfiles", "Unable to migrate validator profiles due to error: " + ex.ToString());
      }
    }

    private delegate void BatchEditEventReceived(object sender, HoldFileBatchEditEventArgs args);

    private delegate void IntegrationEventReceived(object sender, TwoFiftyTransactionEventArgs args);

    public delegate void CurrentSystemPrefsChangedDelegate(
      object sender,
      CurrentSystemPrefsEventArgs args);

    public delegate void CommStatusCallback(string msg);

    private delegate void ReceivedSoftwareUpdatesDelegate(List<AdminInstallInfo> installInfo);

    private delegate void MessageDialogDelegate(string message);

    [Flags]
    internal enum DownloadTypes
    {
      None = 0,
      ExpressRates = 1,
      GroundRates = 2,
      TrackingNumbers = 4,
      URSA = 8,
    }

    private abstract class PurgeLogic : IDisposable
    {
      private BackgroundWorker worker;
      private DoWorkEventHandler DoWork;
      private RunWorkerCompletedEventHandler WorkCompleted;

      public PurgeLogic()
      {
      }

      public PurgeLogic(BackgroundWorker worker) => this.HookWorker(worker);

      private void HookWorker(BackgroundWorker worker)
      {
        this.worker = worker;
        this.DoWork = new DoWorkEventHandler(this.worker_DoWork);
        worker.DoWork += this.DoWork;
        this.WorkCompleted = new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
        worker.RunWorkerCompleted += this.WorkCompleted;
      }

      private void UnHookWorker()
      {
        if (this.worker != null)
        {
          this.worker.DoWork -= this.DoWork;
          this.worker.RunWorkerCompleted -= this.WorkCompleted;
        }
        this.worker = (BackgroundWorker) null;
      }

      private void worker_DoWork(object sender, DoWorkEventArgs e)
      {
        ShellForm.PurgeLogic.PurgeArgs purgeArgs = (ShellForm.PurgeLogic.PurgeArgs) e.Argument;
        purgeArgs.Result = this.DoPurge(purgeArgs.Account, purgeArgs.Meter);
        e.Result = (object) purgeArgs;
      }

      private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
        ((ShellForm.PurgeLogic.PurgeArgs) e.Result).Dialog.DialogResult = DialogResult.OK;
      }

      public void Dispose()
      {
        if (this.worker != null)
          this.UnHookWorker();
        GC.SuppressFinalize((object) this);
      }

      protected virtual string DatabaseName => this.Database.ToString();

      protected abstract DatabaseType Database { get; }

      protected abstract string PurgeFlag { get; }

      protected virtual bool IsPurgeSet
      {
        get
        {
          bool bVal;
          return GuiData.ConfigManager.GetProfileBool("SHIPNET2000/GUI/SETTINGS", this.PurgeFlag, out bVal) & bVal;
        }
      }

      protected virtual void SetPurgeFlag(bool value)
      {
        GuiData.ConfigManager.SetProfileValue("SHIPNET2000/GUI/SETTINGS", this.PurgeFlag, value ? (object) "Y" : (object) "N");
      }

      protected abstract int GetMinPurgeDays(Account account);

      protected abstract PurgeResponse DoPurge(string account, string meter);

      protected virtual PurgeResponse PurgeDatabase(Account account)
      {
        PurgeResponse purgeResponse = PurgeResponse.OK;
        using (PurgePromptDlg purgePromptDlg = new PurgePromptDlg(this.Database))
        {
          while (purgePromptDlg.ShowDialog() == DialogResult.OK)
          {
            using (PurgingNowDialog dialog = new PurgingNowDialog())
            {
              if (this.worker != null)
              {
                ShellForm.PurgeLogic.PurgeArgs purgeArgs = new ShellForm.PurgeLogic.PurgeArgs((Form) dialog, account.AccountNumber, account.MeterNumber);
                this.worker.RunWorkerAsync((object) purgeArgs);
                int num = (int) dialog.ShowDialog();
                purgeResponse = purgeArgs.Result;
              }
              else
              {
                dialog.Show();
                purgeResponse = this.DoPurge(account.AccountNumber, account.MeterNumber);
              }
              dialog.Hide();
              if (purgeResponse == PurgeResponse.MoreToPurge)
              {
                purgePromptDlg.MoreToPurge = true;
                purgePromptDlg.ResetWaitTime();
              }
              else
                this.SetPurgeFlag(false);
            }
            if (purgeResponse != PurgeResponse.MoreToPurge)
              goto label_17;
          }
          this.SetPurgeFlag(true);
        }
label_17:
        return purgeResponse;
      }

      protected abstract bool NeedToPurge(string account, string meter);

      protected virtual void SetNeedToPurge(Account account)
      {
        if (!this.NeedToPurge(account.AccountNumber, account.MeterNumber))
          return;
        this.SetPurgeFlag(true);
      }

      public virtual void ProcessPurge(Account account)
      {
        if (this.IsPurgeSet)
        {
          SplashScreen.Hide();
          if (this.PurgeDatabase(account) == PurgeResponse.OK)
            return;
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ShellForm_Load", "Error purging " + this.DatabaseName + " database");
        }
        else
          this.SetNeedToPurge(account);
      }

      private class PurgeArgs : EventArgs
      {
        public string Account;
        public string Meter;
        public Form Dialog;
        public PurgeResponse Result;

        public PurgeArgs(Form dialog, string account, string meter)
        {
          this.Account = account;
          this.Meter = meter;
          this.Dialog = dialog;
        }
      }
    }

    private class ShipmentPurgeLogic : ShellForm.PurgeLogic
    {
      public ShipmentPurgeLogic(BackgroundWorker worker)
        : base(worker)
      {
      }

      protected override DatabaseType Database => DatabaseType.Shipment;

      protected override string PurgeFlag => "PurgeOnBootup";

      protected override PurgeResponse DoPurge(string account, string meter)
      {
        switch (GuiData.AppController.ShipEngine.ArchiveOnlineShipments(account, meter))
        {
          case 1:
            return PurgeResponse.AllHellBrokeLoose;
          case 2:
            return PurgeResponse.MoreToPurge;
          default:
            return PurgeResponse.OK;
        }
      }

      protected override bool NeedToPurge(string account, string meter)
      {
        return GuiData.AppController.ShipEngine.IsOnlineArchiveTime(account, meter);
      }

      protected override int GetMinPurgeDays(Account account) => (int) account.MinHistoryPurgeDays;
    }

    private class FreightPurgeLogic : ShellForm.PurgeLogic
    {
      public FreightPurgeLogic(BackgroundWorker worker)
        : base(worker)
      {
      }

      protected override DatabaseType Database => DatabaseType.Freight;

      protected override string PurgeFlag => "PurgeFreightOnBootup";

      protected override PurgeResponse DoPurge(string account, string meter)
      {
        ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.PurgeFreightShipment(account, meter, true, (Error) null);
        return serviceResponse == null || serviceResponse.Error == null || serviceResponse.ErrorCode == 1 ? PurgeResponse.OK : PurgeResponse.AllHellBrokeLoose;
      }

      protected override bool NeedToPurge(string account, string meter)
      {
        return GuiData.AppController.ShipEngine.IsFreightPurgeTime(account, meter);
      }

      protected override int GetMinPurgeDays(Account account) => account.FreightLTLPurgeMin;
    }
  }
}
