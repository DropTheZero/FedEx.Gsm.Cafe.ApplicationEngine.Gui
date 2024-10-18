// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.CustomsDocuments
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class CustomsDocuments : UserControlHelpEx
  {
    protected ShipDefl _shipDefl;
    private bool _combosLoaded;
    private Account _account;
    private IContainer components;
    protected ColorGroupBox gbxYourOwnETD;
    protected ColorGroupBox gbxETDPref;
    protected TableLayoutPanel tableLayoutPanelOtherPrefs;
    protected Label lbldocPref;
    protected ComboBoxEx cboCI;
    protected ColorGroupBox gbxLetterhead;
    private FocusExtender focusExtender1;
    protected Label label1;
    private TextBox edtFileLocation;
    private Button btnBrowse;
    private LinkLabelEx txtDefaultFileLocation;
    public NumericUpDown spnCI;
    protected Label lblCIProforma;
    protected Label label2;
    private Button btnUploadImages;
    protected Label label3;
    protected ComboBoxEx cboCOO;
    protected Label label4;
    protected ComboBoxEx cboNaftaCOO;
    private TextBox edtOtherCustomsDoc;
    private LinkLabelEx lnkOtherTradDoc;
    public NumericUpDown spnCIOwnRecords;
    private LinkLabelEx lnkPrintCopiesForYourOwnRecords;
    public NumericUpDown spnNAFTACOOOwnRecords;
    public NumericUpDown spnNAFTACOO;
    public NumericUpDown spnCOOOwnRecords;
    public NumericUpDown spnCOO;
    private LinkLabelEx lnkPrintCopiesWhenUploadNA;
    private Panel pnlEtd;
    private Panel pnlETDEnabled;
    private TableLayoutPanel tlpDocPrefs;
    private Panel pnlCoo;
    private Panel pnlNaftaCoo;
    private Panel pnlOtherTrade;

    internal Account CurrentAccount
    {
      get => this._account ?? GuiData.CurrentAccount;
      set => this._account = value;
    }

    public CustomsDocuments() => this.InitializeComponent();

    public void InitCustomsDocPrefs(ShipDefl shipDefl) => this._shipDefl = shipDefl;

    public ShipDefl PreferenceObject
    {
      get => this._shipDefl;
      set => this._shipDefl = value;
    }

    public virtual void ObjectToScreen()
    {
      this.PopulateCombos();
      if (this._shipDefl == null)
        return;
      try
      {
        this.edtFileLocation.Text = ((IShipDefl) this._shipDefl).DefaultDocLocation;
        this.cboCI.SelectedValue = ((IShipDefl) this._shipDefl).CI_ProformaSelection != CI_ProformaType.NoneSelected ? (object) (int) ((IShipDefl) this._shipDefl).CI_ProformaSelection : (object) 1;
        this.cboCOO.SelectedValue = (object) (int) ((IShipDefl) this._shipDefl).CO_Generation;
        this.cboNaftaCOO.SelectedValue = (object) (int) ((IShipDefl) this._shipDefl).NAFTACO_Generation;
        if (((IShipDefl) this._shipDefl).OtherCustomsDocument != null && ((IShipDefl) this._shipDefl).OtherCustomsDocument.Length > 0)
          this.edtOtherCustomsDoc.Text = ((IShipDefl) this._shipDefl).OtherCustomsDocument;
        if (((IShipDefl) this._shipDefl).NumberOfCICopiesNonETD > 0)
          this.spnCI.Value = (Decimal) ((IShipDefl) this._shipDefl).NumberOfCICopiesNonETD;
        if (((IShipDefl) this._shipDefl).NumberOfCICopiesETD > 0)
          this.spnCIOwnRecords.Value = (Decimal) ((IShipDefl) this._shipDefl).NumberOfCICopiesETD;
        if (((IShipDefl) this._shipDefl).NumberOfCOOCopiesNonETD > 0)
          this.spnCOO.Value = (Decimal) ((IShipDefl) this._shipDefl).NumberOfCOOCopiesNonETD;
        if (((IShipDefl) this._shipDefl).NumberOfCOOCopiesETD > 0)
          this.spnCOOOwnRecords.Value = (Decimal) ((IShipDefl) this._shipDefl).NumberOfCOOCopiesETD;
        if (((IShipDefl) this._shipDefl).NumberOfNAFTACOOCopiesNonETD > 0)
          this.spnNAFTACOO.Value = (Decimal) ((IShipDefl) this._shipDefl).NumberOfNAFTACOOCopiesNonETD;
        if (((IShipDefl) this._shipDefl).NumberOfNAFTACOOCopiesETD <= 0)
          return;
        this.spnNAFTACOOOwnRecords.Value = (Decimal) ((IShipDefl) this._shipDefl).NumberOfNAFTACOOCopiesETD;
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Exception: " + ex.Message);
      }
    }

    public virtual void ScreenToObject()
    {
      try
      {
        ((IShipDefl) this._shipDefl).DefaultDocLocation = this.edtFileLocation.Text.Trim();
        if (this.cboCI.SelectedIndex > -1)
          ((IShipDefl) this._shipDefl).CI_ProformaSelection = (CI_ProformaType) Enum.Parse(typeof (CI_ProformaType), this.cboCI.SelectedValue.ToString());
        if (this.cboCOO.SelectedIndex > -1)
          ((IShipDefl) this._shipDefl).CO_Generation = (CO_GenerationType) Enum.Parse(typeof (CO_GenerationType), this.cboCOO.SelectedValue.ToString());
        if (this.cboNaftaCOO.SelectedIndex > -1)
          ((IShipDefl) this._shipDefl).NAFTACO_Generation = (NAFTACO_GenerationType) Enum.Parse(typeof (NAFTACO_GenerationType), this.cboNaftaCOO.SelectedValue.ToString());
        if (this.edtOtherCustomsDoc.Text.Trim() != GuiData.Languafier.Translate("OtherETDPrompt"))
          ((IShipDefl) this._shipDefl).OtherCustomsDocument = this.edtOtherCustomsDoc.Text.Trim();
        else
          ((IShipDefl) this._shipDefl).OtherCustomsDocument = string.Empty;
        if (this.spnCI.Visible)
          ((IShipDefl) this._shipDefl).NumberOfCICopiesNonETD = (int) this.spnCI.Value;
        if (this.spnCIOwnRecords.Visible)
          ((IShipDefl) this._shipDefl).NumberOfCICopiesETD = (int) this.spnCIOwnRecords.Value;
        if (this.spnCOO.Visible)
          ((IShipDefl) this._shipDefl).NumberOfCOOCopiesNonETD = (int) this.spnCOO.Value;
        if (this.spnCOOOwnRecords.Visible)
          ((IShipDefl) this._shipDefl).NumberOfCOOCopiesETD = (int) this.spnCOOOwnRecords.Value;
        if (this.spnNAFTACOO.Visible)
          ((IShipDefl) this._shipDefl).NumberOfNAFTACOOCopiesNonETD = (int) this.spnNAFTACOO.Value;
        if (!this.spnNAFTACOOOwnRecords.Visible)
          return;
        ((IShipDefl) this._shipDefl).NumberOfNAFTACOOCopiesETD = (int) this.spnNAFTACOOOwnRecords.Value;
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Exception: " + ex.Message);
      }
    }

    private void CustomsDocuments_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.PopulateCombos();
      if (this.CurrentAccount.ETDEnabled)
      {
        this.gbxYourOwnETD.Visible = true;
        this.pnlEtd.Visible = true;
        this.pnlETDEnabled.Visible = true;
      }
      else
      {
        this.gbxYourOwnETD.Visible = false;
        this.lnkPrintCopiesWhenUploadNA.Text = GuiData.Languafier.Translate("PrintCopies");
        this.pnlEtd.Visible = false;
        this.pnlETDEnabled.Visible = false;
      }
      this.ObjectToScreen();
      Utility.HouseKeeping(this.Controls);
    }

    private void PopulateCombos()
    {
      if (!this._combosLoaded)
      {
        Utility.SetDisplayAndValue((ComboBox) this.cboCI, Utility.GetDataTable(Utility.ListTypes.InvoiceSelectionTypes), "Description", "Code", true);
        Utility.SetDisplayAndValue((ComboBox) this.cboCOO, Utility.GetDataTable(Utility.ListTypes.CooSelectionTypes), "Description", "Code", true);
        Utility.SetDisplayAndValue((ComboBox) this.cboNaftaCOO, Utility.GetDataTable(Utility.ListTypes.NaftaCooSelectionTypes), "Description", "Code", true);
        this.HideAllSpinControls(true);
        this.HidePrintCloumHeadings();
      }
      this._combosLoaded = true;
    }

    private void HideAllSpinControls(bool bHide)
    {
      this.spnCI.Visible = !bHide;
      this.spnCIOwnRecords.Visible = !bHide;
      this.spnCOO.Visible = !bHide;
      this.spnCOOOwnRecords.Visible = !bHide;
      this.spnNAFTACOO.Visible = !bHide;
      this.spnNAFTACOOOwnRecords.Visible = !bHide;
    }

    private void HidePrintCloumHeadings()
    {
      if (this.spnCI.Visible || this.spnCOO.Visible || this.spnNAFTACOO.Visible)
      {
        this.lnkPrintCopiesForYourOwnRecords.Visible = true;
        this.lnkPrintCopiesWhenUploadNA.Visible = true;
      }
      else
      {
        this.lnkPrintCopiesForYourOwnRecords.Visible = false;
        this.lnkPrintCopiesWhenUploadNA.Visible = false;
      }
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
      folderBrowserDialog.SelectedPath = this.edtFileLocation.Text;
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      this.edtFileLocation.Text = folderBrowserDialog.SelectedPath;
    }

    private void cboCI_SelectedIndexChanged(object sender, EventArgs e)
    {
      bool flag = false;
      if (this.cboCI.SelectedValue != null)
        flag = this.cboCI.Text.ToString().ToUpper().Contains("FEDEX");
      this.spnCI.Visible = flag;
      this.spnCIOwnRecords.Visible = flag;
      this.HidePrintCloumHeadings();
    }

    private void cboCOO_SelectedIndexChanged(object sender, EventArgs e)
    {
      bool flag = false;
      if (this.cboCOO.SelectedValue != null)
        flag = this.cboCOO.Text.ToString().ToUpper().Contains("FEDEX");
      this.spnCOO.Visible = flag;
      this.spnCOOOwnRecords.Visible = flag;
      this.HidePrintCloumHeadings();
    }

    private void cboNaftaCOO_SelectedIndexChanged(object sender, EventArgs e)
    {
      bool flag = false;
      if (this.cboNaftaCOO.SelectedValue != null)
        flag = this.cboNaftaCOO.Text.ToString().ToUpper().Contains("FEDEX");
      this.spnNAFTACOO.Visible = flag;
      this.spnNAFTACOOOwnRecords.Visible = flag;
      this.HidePrintCloumHeadings();
    }

    private void btnUploadImages_Click(object sender, EventArgs e)
    {
      UploadImagesDlg uploadImagesDlg = new UploadImagesDlg();
      int printType = (int) ((IShipDefl) this._shipDefl).PrintType;
      uploadImagesDlg.ApplyLetterheadToCountry = ((IShipDefl) this._shipDefl).PrintType != ImagePrintType.NoneSelected ? (object) (int) ((IShipDefl) this._shipDefl).PrintType : (object) 1;
      if (((IShipDefl) this._shipDefl).CompanyLogo != null)
        uploadImagesDlg.LetterheadImage = ((IShipDefl) this._shipDefl).CompanyLogo;
      if (((IShipDefl) this._shipDefl).SignatureImage != null)
        uploadImagesDlg.SignatureImage = ((IShipDefl) this._shipDefl).SignatureImage;
      uploadImagesDlg.AlwaysApplySignature = ((IShipDefl) this._shipDefl).AlwaysApplySignature;
      if (uploadImagesDlg.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        ((IShipDefl) this._shipDefl).AlwaysApplySignature = uploadImagesDlg.AlwaysApplySignature;
        ((IShipDefl) this._shipDefl).PrintType = (ImagePrintType) Enum.Parse(typeof (ImagePrintType), uploadImagesDlg.ApplyLetterheadToCountry.ToString());
        ((IShipDefl) this._shipDefl).CompanyLogo = uploadImagesDlg.LetterheadImage ?? new BinaryDocument(string.Empty, string.Empty, new byte[0]);
        ((IShipDefl) this._shipDefl).SignatureImage = uploadImagesDlg.SignatureImage ?? new BinaryDocument(string.Empty, string.Empty, new byte[0]);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, nameof (CustomsDocuments), ex.Message);
      }
    }

    private void lnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Utility.DisplayLinkLabelHelp(sender, e);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CustomsDocuments));
      this.tableLayoutPanelOtherPrefs = new TableLayoutPanel();
      this.gbxLetterhead = new ColorGroupBox();
      this.btnUploadImages = new Button();
      this.label2 = new Label();
      this.gbxYourOwnETD = new ColorGroupBox();
      this.txtDefaultFileLocation = new LinkLabelEx();
      this.edtFileLocation = new TextBox();
      this.btnBrowse = new Button();
      this.label1 = new Label();
      this.gbxETDPref = new ColorGroupBox();
      this.pnlETDEnabled = new Panel();
      this.lnkPrintCopiesForYourOwnRecords = new LinkLabelEx();
      this.spnCIOwnRecords = new NumericUpDown();
      this.pnlEtd = new Panel();
      this.tlpDocPrefs = new TableLayoutPanel();
      this.pnlCoo = new Panel();
      this.spnCOOOwnRecords = new NumericUpDown();
      this.spnCOO = new NumericUpDown();
      this.label3 = new Label();
      this.cboCOO = new ComboBoxEx();
      this.pnlNaftaCoo = new Panel();
      this.spnNAFTACOOOwnRecords = new NumericUpDown();
      this.spnNAFTACOO = new NumericUpDown();
      this.label4 = new Label();
      this.cboNaftaCOO = new ComboBoxEx();
      this.pnlOtherTrade = new Panel();
      this.lnkOtherTradDoc = new LinkLabelEx();
      this.edtOtherCustomsDoc = new TextBox();
      this.lnkPrintCopiesWhenUploadNA = new LinkLabelEx();
      this.lblCIProforma = new Label();
      this.spnCI = new NumericUpDown();
      this.cboCI = new ComboBoxEx();
      this.lbldocPref = new Label();
      this.focusExtender1 = new FocusExtender();
      this.tableLayoutPanelOtherPrefs.SuspendLayout();
      this.gbxLetterhead.SuspendLayout();
      this.gbxYourOwnETD.SuspendLayout();
      this.gbxETDPref.SuspendLayout();
      this.pnlETDEnabled.SuspendLayout();
      this.spnCIOwnRecords.BeginInit();
      this.pnlEtd.SuspendLayout();
      this.tlpDocPrefs.SuspendLayout();
      this.pnlCoo.SuspendLayout();
      this.spnCOOOwnRecords.BeginInit();
      this.spnCOO.BeginInit();
      this.pnlNaftaCoo.SuspendLayout();
      this.spnNAFTACOOOwnRecords.BeginInit();
      this.spnNAFTACOO.BeginInit();
      this.pnlOtherTrade.SuspendLayout();
      this.spnCI.BeginInit();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) this.tableLayoutPanelOtherPrefs, "tableLayoutPanelOtherPrefs");
      this.tableLayoutPanelOtherPrefs.Controls.Add((Control) this.gbxLetterhead, 0, 2);
      this.tableLayoutPanelOtherPrefs.Controls.Add((Control) this.gbxYourOwnETD, 0, 0);
      this.tableLayoutPanelOtherPrefs.Controls.Add((Control) this.gbxETDPref, 0, 1);
      this.tableLayoutPanelOtherPrefs.Name = "tableLayoutPanelOtherPrefs";
      this.gbxLetterhead.BorderThickness = 1f;
      this.gbxLetterhead.Controls.Add((Control) this.btnUploadImages);
      this.gbxLetterhead.Controls.Add((Control) this.label2);
      componentResourceManager.ApplyResources((object) this.gbxLetterhead, "gbxLetterhead");
      this.gbxLetterhead.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxLetterhead.Name = "gbxLetterhead";
      this.gbxLetterhead.RoundCorners = 5;
      this.gbxLetterhead.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.btnUploadImages, componentResourceManager.GetString("btnUploadImages.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnUploadImages, (HelpNavigator) componentResourceManager.GetObject("btnUploadImages.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.btnUploadImages, "btnUploadImages");
      this.btnUploadImages.Name = "btnUploadImages";
      this.helpProvider1.SetShowHelp((Control) this.btnUploadImages, (bool) componentResourceManager.GetObject("btnUploadImages.ShowHelp"));
      this.btnUploadImages.UseVisualStyleBackColor = true;
      this.btnUploadImages.Click += new EventHandler(this.btnUploadImages_Click);
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      this.helpProvider1.SetShowHelp((Control) this.label2, (bool) componentResourceManager.GetObject("label2.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.gbxYourOwnETD, "gbxYourOwnETD");
      this.gbxYourOwnETD.BorderThickness = 1f;
      this.gbxYourOwnETD.Controls.Add((Control) this.txtDefaultFileLocation);
      this.gbxYourOwnETD.Controls.Add((Control) this.edtFileLocation);
      this.gbxYourOwnETD.Controls.Add((Control) this.btnBrowse);
      this.gbxYourOwnETD.Controls.Add((Control) this.label1);
      this.gbxYourOwnETD.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxYourOwnETD.Name = "gbxYourOwnETD";
      this.gbxYourOwnETD.RoundCorners = 5;
      this.gbxYourOwnETD.TabStop = false;
      this.txtDefaultFileLocation.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.txtDefaultFileLocation, "txtDefaultFileLocation");
      this.txtDefaultFileLocation.Name = "txtDefaultFileLocation";
      this.helpProvider1.SetShowHelp((Control) this.txtDefaultFileLocation, (bool) componentResourceManager.GetObject("txtDefaultFileLocation.ShowHelp"));
      this.txtDefaultFileLocation.TabStop = true;
      this.txtDefaultFileLocation.Tag = (object) "4828";
      this.txtDefaultFileLocation.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
      componentResourceManager.ApplyResources((object) this.edtFileLocation, "edtFileLocation");
      this.focusExtender1.SetFocusBackColor((Control) this.edtFileLocation, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtFileLocation, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtFileLocation, componentResourceManager.GetString("edtFileLocation.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtFileLocation, (HelpNavigator) componentResourceManager.GetObject("edtFileLocation.HelpNavigator"));
      this.edtFileLocation.Name = "edtFileLocation";
      this.helpProvider1.SetShowHelp((Control) this.edtFileLocation, (bool) componentResourceManager.GetObject("edtFileLocation.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnBrowse, "btnBrowse");
      this.helpProvider1.SetHelpKeyword((Control) this.btnBrowse, componentResourceManager.GetString("btnBrowse.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnBrowse, (HelpNavigator) componentResourceManager.GetObject("btnBrowse.HelpNavigator"));
      this.btnBrowse.Name = "btnBrowse";
      this.helpProvider1.SetShowHelp((Control) this.btnBrowse, (bool) componentResourceManager.GetObject("btnBrowse.ShowHelp"));
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.helpProvider1.SetShowHelp((Control) this.label1, (bool) componentResourceManager.GetObject("label1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.gbxETDPref, "gbxETDPref");
      this.gbxETDPref.BorderThickness = 1f;
      this.gbxETDPref.Controls.Add((Control) this.pnlETDEnabled);
      this.gbxETDPref.Controls.Add((Control) this.pnlEtd);
      this.gbxETDPref.Controls.Add((Control) this.lnkPrintCopiesWhenUploadNA);
      this.gbxETDPref.Controls.Add((Control) this.lblCIProforma);
      this.gbxETDPref.Controls.Add((Control) this.spnCI);
      this.gbxETDPref.Controls.Add((Control) this.cboCI);
      this.gbxETDPref.Controls.Add((Control) this.lbldocPref);
      this.gbxETDPref.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxETDPref.Name = "gbxETDPref";
      this.gbxETDPref.RoundCorners = 5;
      this.gbxETDPref.TabStop = false;
      componentResourceManager.ApplyResources((object) this.pnlETDEnabled, "pnlETDEnabled");
      this.pnlETDEnabled.BackColor = Color.FromArgb((int) byte.MaxValue, 192, (int) byte.MaxValue);
      this.pnlETDEnabled.Controls.Add((Control) this.lnkPrintCopiesForYourOwnRecords);
      this.pnlETDEnabled.Controls.Add((Control) this.spnCIOwnRecords);
      this.pnlETDEnabled.Name = "pnlETDEnabled";
      componentResourceManager.ApplyResources((object) this.lnkPrintCopiesForYourOwnRecords, "lnkPrintCopiesForYourOwnRecords");
      this.lnkPrintCopiesForYourOwnRecords.BackColor = Color.Transparent;
      this.lnkPrintCopiesForYourOwnRecords.Name = "lnkPrintCopiesForYourOwnRecords";
      this.helpProvider1.SetShowHelp((Control) this.lnkPrintCopiesForYourOwnRecords, (bool) componentResourceManager.GetObject("lnkPrintCopiesForYourOwnRecords.ShowHelp"));
      this.lnkPrintCopiesForYourOwnRecords.TabStop = true;
      this.lnkPrintCopiesForYourOwnRecords.Tag = (object) "4829";
      this.lnkPrintCopiesForYourOwnRecords.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
      componentResourceManager.ApplyResources((object) this.spnCIOwnRecords, "spnCIOwnRecords");
      this.focusExtender1.SetFocusBackColor((Control) this.spnCIOwnRecords, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.spnCIOwnRecords, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.spnCIOwnRecords, componentResourceManager.GetString("spnCIOwnRecords.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnCIOwnRecords, (HelpNavigator) componentResourceManager.GetObject("spnCIOwnRecords.HelpNavigator"));
      this.spnCIOwnRecords.Maximum = new Decimal(new int[4]
      {
        9,
        0,
        0,
        0
      });
      this.spnCIOwnRecords.Name = "spnCIOwnRecords";
      this.spnCIOwnRecords.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.spnCIOwnRecords, (bool) componentResourceManager.GetObject("spnCIOwnRecords.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.pnlEtd, "pnlEtd");
      this.pnlEtd.BackColor = Color.FromArgb((int) byte.MaxValue, 192, (int) byte.MaxValue);
      this.pnlEtd.Controls.Add((Control) this.tlpDocPrefs);
      this.pnlEtd.Name = "pnlEtd";
      componentResourceManager.ApplyResources((object) this.tlpDocPrefs, "tlpDocPrefs");
      this.tlpDocPrefs.Controls.Add((Control) this.pnlCoo, 0, 0);
      this.tlpDocPrefs.Controls.Add((Control) this.pnlNaftaCoo, 0, 1);
      this.tlpDocPrefs.Controls.Add((Control) this.pnlOtherTrade, 0, 2);
      this.tlpDocPrefs.Name = "tlpDocPrefs";
      this.pnlCoo.Controls.Add((Control) this.spnCOOOwnRecords);
      this.pnlCoo.Controls.Add((Control) this.spnCOO);
      this.pnlCoo.Controls.Add((Control) this.label3);
      this.pnlCoo.Controls.Add((Control) this.cboCOO);
      componentResourceManager.ApplyResources((object) this.pnlCoo, "pnlCoo");
      this.pnlCoo.Name = "pnlCoo";
      componentResourceManager.ApplyResources((object) this.spnCOOOwnRecords, "spnCOOOwnRecords");
      this.focusExtender1.SetFocusBackColor((Control) this.spnCOOOwnRecords, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.spnCOOOwnRecords, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.spnCOOOwnRecords, componentResourceManager.GetString("spnCOOOwnRecords.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnCOOOwnRecords, (HelpNavigator) componentResourceManager.GetObject("spnCOOOwnRecords.HelpNavigator"));
      this.spnCOOOwnRecords.Maximum = new Decimal(new int[4]
      {
        9,
        0,
        0,
        0
      });
      this.spnCOOOwnRecords.Name = "spnCOOOwnRecords";
      this.spnCOOOwnRecords.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.spnCOOOwnRecords, (bool) componentResourceManager.GetObject("spnCOOOwnRecords.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.spnCOO, "spnCOO");
      this.focusExtender1.SetFocusBackColor((Control) this.spnCOO, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.spnCOO, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.spnCOO, componentResourceManager.GetString("spnCOO.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnCOO, (HelpNavigator) componentResourceManager.GetObject("spnCOO.HelpNavigator"));
      this.spnCOO.Maximum = new Decimal(new int[4]
      {
        9,
        0,
        0,
        0
      });
      this.spnCOO.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.spnCOO.Name = "spnCOO";
      this.spnCOO.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.spnCOO, (bool) componentResourceManager.GetObject("spnCOO.ShowHelp"));
      this.spnCOO.Value = new Decimal(new int[4]
      {
        3,
        0,
        0,
        0
      });
      componentResourceManager.ApplyResources((object) this.label3, "label3");
      this.label3.Name = "label3";
      this.helpProvider1.SetShowHelp((Control) this.label3, (bool) componentResourceManager.GetObject("label3.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.cboCOO, "cboCOO");
      this.cboCOO.DisplayMemberQ = "";
      this.cboCOO.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboCOO.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboCOO, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboCOO, SystemColors.WindowText);
      this.cboCOO.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboCOO, componentResourceManager.GetString("cboCOO.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboCOO, (HelpNavigator) componentResourceManager.GetObject("cboCOO.HelpNavigator"));
      this.cboCOO.Name = "cboCOO";
      this.cboCOO.SelectedIndexQ = -1;
      this.cboCOO.SelectedItemQ = (object) null;
      this.cboCOO.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboCOO, (bool) componentResourceManager.GetObject("cboCOO.ShowHelp"));
      this.cboCOO.ValueMemberQ = "";
      this.cboCOO.SelectedIndexChanged += new EventHandler(this.cboCOO_SelectedIndexChanged);
      this.pnlNaftaCoo.Controls.Add((Control) this.spnNAFTACOOOwnRecords);
      this.pnlNaftaCoo.Controls.Add((Control) this.spnNAFTACOO);
      this.pnlNaftaCoo.Controls.Add((Control) this.label4);
      this.pnlNaftaCoo.Controls.Add((Control) this.cboNaftaCOO);
      componentResourceManager.ApplyResources((object) this.pnlNaftaCoo, "pnlNaftaCoo");
      this.pnlNaftaCoo.Name = "pnlNaftaCoo";
      componentResourceManager.ApplyResources((object) this.spnNAFTACOOOwnRecords, "spnNAFTACOOOwnRecords");
      this.focusExtender1.SetFocusBackColor((Control) this.spnNAFTACOOOwnRecords, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.spnNAFTACOOOwnRecords, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.spnNAFTACOOOwnRecords, componentResourceManager.GetString("spnNAFTACOOOwnRecords.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnNAFTACOOOwnRecords, (HelpNavigator) componentResourceManager.GetObject("spnNAFTACOOOwnRecords.HelpNavigator"));
      this.spnNAFTACOOOwnRecords.Maximum = new Decimal(new int[4]
      {
        9,
        0,
        0,
        0
      });
      this.spnNAFTACOOOwnRecords.Name = "spnNAFTACOOOwnRecords";
      this.spnNAFTACOOOwnRecords.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.spnNAFTACOOOwnRecords, (bool) componentResourceManager.GetObject("spnNAFTACOOOwnRecords.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.spnNAFTACOO, "spnNAFTACOO");
      this.focusExtender1.SetFocusBackColor((Control) this.spnNAFTACOO, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.spnNAFTACOO, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.spnNAFTACOO, componentResourceManager.GetString("spnNAFTACOO.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnNAFTACOO, (HelpNavigator) componentResourceManager.GetObject("spnNAFTACOO.HelpNavigator"));
      this.spnNAFTACOO.Maximum = new Decimal(new int[4]
      {
        9,
        0,
        0,
        0
      });
      this.spnNAFTACOO.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.spnNAFTACOO.Name = "spnNAFTACOO";
      this.spnNAFTACOO.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.spnNAFTACOO, (bool) componentResourceManager.GetObject("spnNAFTACOO.ShowHelp"));
      this.spnNAFTACOO.Value = new Decimal(new int[4]
      {
        3,
        0,
        0,
        0
      });
      componentResourceManager.ApplyResources((object) this.label4, "label4");
      this.label4.Name = "label4";
      this.helpProvider1.SetShowHelp((Control) this.label4, (bool) componentResourceManager.GetObject("label4.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.cboNaftaCOO, "cboNaftaCOO");
      this.cboNaftaCOO.DisplayMemberQ = "";
      this.cboNaftaCOO.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboNaftaCOO.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboNaftaCOO, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboNaftaCOO, SystemColors.WindowText);
      this.cboNaftaCOO.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboNaftaCOO, componentResourceManager.GetString("cboNaftaCOO.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboNaftaCOO, (HelpNavigator) componentResourceManager.GetObject("cboNaftaCOO.HelpNavigator"));
      this.cboNaftaCOO.Name = "cboNaftaCOO";
      this.cboNaftaCOO.SelectedIndexQ = -1;
      this.cboNaftaCOO.SelectedItemQ = (object) null;
      this.cboNaftaCOO.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboNaftaCOO, (bool) componentResourceManager.GetObject("cboNaftaCOO.ShowHelp"));
      this.cboNaftaCOO.ValueMemberQ = "";
      this.cboNaftaCOO.SelectedIndexChanged += new EventHandler(this.cboNaftaCOO_SelectedIndexChanged);
      this.pnlOtherTrade.Controls.Add((Control) this.lnkOtherTradDoc);
      this.pnlOtherTrade.Controls.Add((Control) this.edtOtherCustomsDoc);
      componentResourceManager.ApplyResources((object) this.pnlOtherTrade, "pnlOtherTrade");
      this.pnlOtherTrade.Name = "pnlOtherTrade";
      this.lnkOtherTradDoc.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.lnkOtherTradDoc, "lnkOtherTradDoc");
      this.lnkOtherTradDoc.Name = "lnkOtherTradDoc";
      this.helpProvider1.SetShowHelp((Control) this.lnkOtherTradDoc, (bool) componentResourceManager.GetObject("lnkOtherTradDoc.ShowHelp"));
      this.lnkOtherTradDoc.TabStop = true;
      this.lnkOtherTradDoc.Tag = (object) "4831";
      this.lnkOtherTradDoc.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
      componentResourceManager.ApplyResources((object) this.edtOtherCustomsDoc, "edtOtherCustomsDoc");
      this.focusExtender1.SetFocusBackColor((Control) this.edtOtherCustomsDoc, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtOtherCustomsDoc, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtOtherCustomsDoc, componentResourceManager.GetString("edtOtherCustomsDoc.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtOtherCustomsDoc, (HelpNavigator) componentResourceManager.GetObject("edtOtherCustomsDoc.HelpNavigator"));
      this.edtOtherCustomsDoc.Name = "edtOtherCustomsDoc";
      this.helpProvider1.SetShowHelp((Control) this.edtOtherCustomsDoc, (bool) componentResourceManager.GetObject("edtOtherCustomsDoc.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lnkPrintCopiesWhenUploadNA, "lnkPrintCopiesWhenUploadNA");
      this.lnkPrintCopiesWhenUploadNA.BackColor = Color.Transparent;
      this.lnkPrintCopiesWhenUploadNA.Name = "lnkPrintCopiesWhenUploadNA";
      this.helpProvider1.SetShowHelp((Control) this.lnkPrintCopiesWhenUploadNA, (bool) componentResourceManager.GetObject("lnkPrintCopiesWhenUploadNA.ShowHelp"));
      this.lnkPrintCopiesWhenUploadNA.TabStop = true;
      this.lnkPrintCopiesWhenUploadNA.Tag = (object) "4830";
      this.lnkPrintCopiesWhenUploadNA.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
      componentResourceManager.ApplyResources((object) this.lblCIProforma, "lblCIProforma");
      this.lblCIProforma.Name = "lblCIProforma";
      this.helpProvider1.SetShowHelp((Control) this.lblCIProforma, (bool) componentResourceManager.GetObject("lblCIProforma.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.spnCI, "spnCI");
      this.focusExtender1.SetFocusBackColor((Control) this.spnCI, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.spnCI, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.spnCI, componentResourceManager.GetString("spnCI.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnCI, (HelpNavigator) componentResourceManager.GetObject("spnCI.HelpNavigator"));
      this.spnCI.Maximum = new Decimal(new int[4]
      {
        9,
        0,
        0,
        0
      });
      this.spnCI.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.spnCI.Name = "spnCI";
      this.spnCI.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.spnCI, (bool) componentResourceManager.GetObject("spnCI.ShowHelp"));
      this.spnCI.Value = new Decimal(new int[4]
      {
        3,
        0,
        0,
        0
      });
      componentResourceManager.ApplyResources((object) this.cboCI, "cboCI");
      this.cboCI.DisplayMemberQ = "";
      this.cboCI.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboCI.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboCI, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboCI, SystemColors.WindowText);
      this.cboCI.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboCI, componentResourceManager.GetString("cboCI.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboCI, (HelpNavigator) componentResourceManager.GetObject("cboCI.HelpNavigator"));
      this.cboCI.Name = "cboCI";
      this.cboCI.SelectedIndexQ = -1;
      this.cboCI.SelectedItemQ = (object) null;
      this.cboCI.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboCI, (bool) componentResourceManager.GetObject("cboCI.ShowHelp"));
      this.cboCI.ValueMemberQ = "";
      this.cboCI.SelectedIndexChanged += new EventHandler(this.cboCI_SelectedIndexChanged);
      componentResourceManager.ApplyResources((object) this.lbldocPref, "lbldocPref");
      this.lbldocPref.Name = "lbldocPref";
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanelOtherPrefs);
      this.Name = nameof (CustomsDocuments);
      this.Load += new EventHandler(this.CustomsDocuments_Load);
      this.tableLayoutPanelOtherPrefs.ResumeLayout(false);
      this.gbxLetterhead.ResumeLayout(false);
      this.gbxYourOwnETD.ResumeLayout(false);
      this.gbxYourOwnETD.PerformLayout();
      this.gbxETDPref.ResumeLayout(false);
      this.pnlETDEnabled.ResumeLayout(false);
      this.spnCIOwnRecords.EndInit();
      this.pnlEtd.ResumeLayout(false);
      this.tlpDocPrefs.ResumeLayout(false);
      this.pnlCoo.ResumeLayout(false);
      this.spnCOOOwnRecords.EndInit();
      this.spnCOO.EndInit();
      this.pnlNaftaCoo.ResumeLayout(false);
      this.spnNAFTACOOOwnRecords.EndInit();
      this.spnNAFTACOO.EndInit();
      this.pnlOtherTrade.ResumeLayout(false);
      this.pnlOtherTrade.PerformLayout();
      this.spnCI.EndInit();
      this.ResumeLayout(false);
    }
  }
}
