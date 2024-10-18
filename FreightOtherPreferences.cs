// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.FreightOtherPreferences
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
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class FreightOtherPreferences : UserControlHelpEx
  {
    private FShipDefl _freightShipDefl;
    private IContainer components;
    protected ColorGroupBox gbxLabelFormat;
    protected ComboBoxEx cboLabelFormat;
    protected Label lblLabelFormat;
    protected CheckBox chkAutoPrintBOLWithShipment;
    protected CheckBox chkAutoPrintLabelWithShipment;
    protected CheckBox chkAutoPrintTradeDocsWithShipment;
    protected ColorGroupBox gbxLetterhead;
    private Button btnUploadImages;
    protected Label label2;
    protected ColorGroupBox gbxETDPref;
    protected Label lbldocPref;
    protected RadioButton rdoFedExCreatedIntlDoc;
    protected RadioButton rdoCreateIntlOnMyOwn;
    public NumericUpDown spnNumBOL;
    protected Button btnDocTabConfig;
    protected RadioButton rdoCustomizeDocTab;
    protected RadioButton rdoDefaultDocTab;
    protected ComboBoxEx cboBarcodeDocTab;
    protected RadioButton rdoBarcodeDocTab;
    protected Label label1;
    protected ColorGroupBox gbxRequireReferences;
    private CheckBox chkReqBillOfLadingPoNum;
    private CheckBox chkReqConsigneeId;
    private CheckBox chkReqShipId2;
    private CheckBox chkReqShipId1;
    private TableLayoutPanel tlpMain;

    public FreightOtherPreferences() => this.InitializeComponent();

    private void FreightOtherPreferences_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.PopulateCombo();
      this.EnableDisableRadioButtons();
      if (GuiData.CurrentAccount == null || !GuiData.CurrentAccount.is_FREIGHT2020_HANDLING_UNITS_CTL_Initiative_Enabled)
        return;
      this.chkReqBillOfLadingPoNum.Text = GuiData.Languafier.Translate("ReqBolHandlingUnitsPo");
    }

    public void InitOtherPrefs(FShipDefl freightShipDefl)
    {
      this._freightShipDefl = freightShipDefl;
    }

    public FShipDefl PreferenceObject
    {
      get => this._freightShipDefl;
      set => this._freightShipDefl = value;
    }

    public void PopulateCombo()
    {
      if (this.cboLabelFormat.Items.Count > 0)
        return;
      DataTable dataTable1 = Utility.GetDataTable(Utility.ListTypes.FreightLabelFormat);
      dataTable1.DefaultView.Sort = "Description ASC";
      dataTable1.DefaultView.RowFilter = !GuiData.CurrentAccount.is_FREIGHT2020_HANDLING_UNITS_CTL_Initiative_Enabled ? "Code <> 9 AND Code <> 11" : "Code <> 5";
      this.cboLabelFormat.DataSourceQ = (object) dataTable1;
      this.cboLabelFormat.ValueMemberQ = "Code";
      this.cboLabelFormat.DisplayMemberQ = "Description";
      DataTable dataTable2 = Utility.GetDataTable(Utility.ListTypes.FreightDocTab);
      dataTable2.DefaultView.RowFilter = "Code <> '29'";
      dataTable2.DefaultView.Sort = "Description ASC";
      this.cboBarcodeDocTab.DataSourceQ = (object) dataTable2;
      this.cboBarcodeDocTab.ValueMemberQ = "Code";
      this.cboBarcodeDocTab.DisplayMemberQ = "Description";
    }

    public void ObjectToScreen()
    {
      this.PopulateCombo();
      if (this._freightShipDefl == null)
        return;
      this.cboLabelFormat.SelectedValue = (object) Enum.Format(typeof (FreightShipment.LabelFormat), (object) this._freightShipDefl.LabelFormatId, "d");
      this.chkAutoPrintBOLWithShipment.Checked = this._freightShipDefl.PrintBolAtShipTime;
      this.chkAutoPrintLabelWithShipment.Checked = this._freightShipDefl.PrintLabelsAtShipTime;
      this.chkAutoPrintTradeDocsWithShipment.Checked = this._freightShipDefl.PrintTradeDocumentsWithInternationalShipment;
      this.rdoFedExCreatedIntlDoc.Checked = this._freightShipDefl.FSMCreateInternationalDocumentation;
      this.rdoCreateIntlOnMyOwn.Checked = !this._freightShipDefl.FSMCreateInternationalDocumentation;
      this.spnNumBOL.Value = (Decimal) this._freightShipDefl.NumberOfBolCopies;
      this.chkReqShipId1.Checked = this._freightShipDefl.RequireShipId1Ref;
      this.chkReqShipId2.Checked = this._freightShipDefl.RequireShipId2Ref;
      this.chkReqConsigneeId.Checked = this._freightShipDefl.RequireConsigneeIdRef;
      this.chkReqBillOfLadingPoNum.Checked = this._freightShipDefl.RequireBolPONumRef;
      switch (this._freightShipDefl.FreightDocTabOption)
      {
        case ShipDefl.DocTabOption.CustomDocTab:
          this.rdoCustomizeDocTab.Checked = true;
          this.cboBarcodeDocTab.SelectedIndex = -1;
          break;
        case ShipDefl.DocTabOption.BarcodeDocTab:
          this.rdoBarcodeDocTab.Checked = true;
          this.cboBarcodeDocTab.SelectedValue = (object) this._freightShipDefl.FreightBarcodeDocTabVal;
          break;
        default:
          this.rdoDefaultDocTab.Checked = true;
          this.cboBarcodeDocTab.SelectedIndex = -1;
          break;
      }
    }

    public void ScreenToObject()
    {
      this._freightShipDefl.LabelFormatId = this.cboLabelFormat.SelectedValueQ == null ? FreightShipment.LabelFormat.PlainPaper : (FreightShipment.LabelFormat) Enum.Parse(typeof (FreightShipment.LabelFormat), this.cboLabelFormat.SelectedValueQ as string);
      this._freightShipDefl.PrintBolAtShipTime = this.chkAutoPrintBOLWithShipment.Checked;
      this._freightShipDefl.PrintLabelsAtShipTime = this.chkAutoPrintLabelWithShipment.Checked;
      this._freightShipDefl.PrintTradeDocumentsWithInternationalShipment = this.chkAutoPrintTradeDocsWithShipment.Checked;
      this._freightShipDefl.FSMCreateInternationalDocumentation = this.rdoFedExCreatedIntlDoc.Checked;
      this._freightShipDefl.RequireShipId1Ref = this.chkReqShipId1.Checked;
      this._freightShipDefl.RequireShipId2Ref = this.chkReqShipId2.Checked;
      this._freightShipDefl.RequireConsigneeIdRef = this.chkReqConsigneeId.Checked;
      this._freightShipDefl.RequireBolPONumRef = this.chkReqBillOfLadingPoNum.Checked;
      if (this.chkAutoPrintBOLWithShipment.Checked)
        this._freightShipDefl.NumberOfBolCopies = (short) this.spnNumBOL.Value;
      if (this.rdoCustomizeDocTab.Checked)
        this._freightShipDefl.FreightDocTabOption = ShipDefl.DocTabOption.CustomDocTab;
      else if (this.rdoBarcodeDocTab.Checked)
        this._freightShipDefl.FreightDocTabOption = ShipDefl.DocTabOption.BarcodeDocTab;
      else
        this._freightShipDefl.FreightDocTabOption = ShipDefl.DocTabOption.DefaultDocTab;
      this._freightShipDefl.FreightBarcodeDocTabVal = this.cboBarcodeDocTab.SelectedValue as string;
    }

    private void btnUploadImages_Click(object sender, EventArgs e)
    {
      UploadImagesDlg uploadImagesDlg = new UploadImagesDlg();
      int printType = (int) this._freightShipDefl.PrintType;
      uploadImagesDlg.ApplyLetterheadToCountry = this._freightShipDefl.PrintType != ImagePrintType.NoneSelected ? (object) (int) this._freightShipDefl.PrintType : (object) 1;
      if (this._freightShipDefl.CompanyLogo != null)
        uploadImagesDlg.LetterheadImage = this._freightShipDefl.CompanyLogo;
      if (this._freightShipDefl.SignatureImage != null)
        uploadImagesDlg.SignatureImage = this._freightShipDefl.SignatureImage;
      uploadImagesDlg.AlwaysApplySignature = this._freightShipDefl.AlwaysApplySignature;
      if (uploadImagesDlg.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        this._freightShipDefl.AlwaysApplySignature = uploadImagesDlg.AlwaysApplySignature;
        this._freightShipDefl.PrintType = (ImagePrintType) Enum.Parse(typeof (ImagePrintType), uploadImagesDlg.ApplyLetterheadToCountry.ToString());
        this._freightShipDefl.CompanyLogo = uploadImagesDlg.LetterheadImage ?? new BinaryDocument(string.Empty, string.Empty, new byte[0]);
        this._freightShipDefl.SignatureImage = uploadImagesDlg.SignatureImage ?? new BinaryDocument(string.Empty, string.Empty, new byte[0]);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "CustomsDocuments", ex.Message);
      }
    }

    private void chkAutoPrintBOLWithShipment_CheckedChanged(object sender, EventArgs e)
    {
      this.spnNumBOL.Enabled = this.chkAutoPrintBOLWithShipment.Checked;
    }

    private void btnDocTabConfig_Click(object sender, EventArgs e)
    {
      int num = (int) new DocTabConfigDlg((ShipDefl) this._freightShipDefl, Shipment.CarrierType.Freight).ShowDialog();
    }

    private void rdoDefaultDocTab_CheckedChanged(object sender, EventArgs e)
    {
      this.EnableDisableDocTabOptions();
    }

    private void cboLabelFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.EnableDisableRadioButtons();
      this.EnableDisableDocTabOptions();
    }

    private void EnableDisableRadioButtons()
    {
      if (this.cboLabelFormat.SelectedValue != null && ((FreightShipment.LabelFormat) Enum.Parse(typeof (FreightShipment.LabelFormat), this.cboLabelFormat.SelectedValueQ as string) == FreightShipment.LabelFormat.Form354 || (FreightShipment.LabelFormat) Enum.Parse(typeof (FreightShipment.LabelFormat), this.cboLabelFormat.SelectedValueQ as string) == FreightShipment.LabelFormat.WithDocTabForm434))
      {
        this.rdoCustomizeDocTab.Enabled = true;
        this.rdoDefaultDocTab.Enabled = true;
        this.rdoBarcodeDocTab.Enabled = true;
      }
      else
      {
        this.rdoCustomizeDocTab.Enabled = false;
        this.rdoDefaultDocTab.Enabled = false;
        this.rdoBarcodeDocTab.Enabled = false;
        this.btnDocTabConfig.Enabled = false;
        this.cboBarcodeDocTab.Enabled = false;
      }
    }

    private void EnableDisableDocTabOptions()
    {
      this.btnDocTabConfig.Enabled = this.rdoCustomizeDocTab.Enabled && this.rdoCustomizeDocTab.Checked;
      this.cboBarcodeDocTab.Enabled = this.rdoBarcodeDocTab.Enabled && this.rdoBarcodeDocTab.Checked;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FreightOtherPreferences));
      this.gbxETDPref = new ColorGroupBox();
      this.rdoFedExCreatedIntlDoc = new RadioButton();
      this.rdoCreateIntlOnMyOwn = new RadioButton();
      this.lbldocPref = new Label();
      this.gbxLetterhead = new ColorGroupBox();
      this.btnUploadImages = new Button();
      this.label2 = new Label();
      this.gbxLabelFormat = new ColorGroupBox();
      this.label1 = new Label();
      this.cboBarcodeDocTab = new ComboBoxEx();
      this.rdoBarcodeDocTab = new RadioButton();
      this.btnDocTabConfig = new Button();
      this.rdoCustomizeDocTab = new RadioButton();
      this.rdoDefaultDocTab = new RadioButton();
      this.spnNumBOL = new NumericUpDown();
      this.chkAutoPrintTradeDocsWithShipment = new CheckBox();
      this.chkAutoPrintBOLWithShipment = new CheckBox();
      this.chkAutoPrintLabelWithShipment = new CheckBox();
      this.cboLabelFormat = new ComboBoxEx();
      this.lblLabelFormat = new Label();
      this.gbxRequireReferences = new ColorGroupBox();
      this.chkReqBillOfLadingPoNum = new CheckBox();
      this.chkReqConsigneeId = new CheckBox();
      this.chkReqShipId2 = new CheckBox();
      this.chkReqShipId1 = new CheckBox();
      this.tlpMain = new TableLayoutPanel();
      this.gbxETDPref.SuspendLayout();
      this.gbxLetterhead.SuspendLayout();
      this.gbxLabelFormat.SuspendLayout();
      this.spnNumBOL.BeginInit();
      this.gbxRequireReferences.SuspendLayout();
      this.tlpMain.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.gbxETDPref, "gbxETDPref");
      this.gbxETDPref.BorderThickness = 1f;
      this.gbxETDPref.Controls.Add((Control) this.rdoFedExCreatedIntlDoc);
      this.gbxETDPref.Controls.Add((Control) this.rdoCreateIntlOnMyOwn);
      this.gbxETDPref.Controls.Add((Control) this.lbldocPref);
      this.gbxETDPref.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxETDPref.Name = "gbxETDPref";
      this.gbxETDPref.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxETDPref, (bool) componentResourceManager.GetObject("gbxETDPref.ShowHelp"));
      this.gbxETDPref.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoFedExCreatedIntlDoc, componentResourceManager.GetString("rdoFedExCreatedIntlDoc.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoFedExCreatedIntlDoc, (HelpNavigator) componentResourceManager.GetObject("rdoFedExCreatedIntlDoc.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoFedExCreatedIntlDoc, "rdoFedExCreatedIntlDoc");
      this.rdoFedExCreatedIntlDoc.Name = "rdoFedExCreatedIntlDoc";
      this.helpProvider1.SetShowHelp((Control) this.rdoFedExCreatedIntlDoc, (bool) componentResourceManager.GetObject("rdoFedExCreatedIntlDoc.ShowHelp"));
      this.rdoFedExCreatedIntlDoc.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.rdoCreateIntlOnMyOwn, componentResourceManager.GetString("rdoCreateIntlOnMyOwn.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoCreateIntlOnMyOwn, (HelpNavigator) componentResourceManager.GetObject("rdoCreateIntlOnMyOwn.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoCreateIntlOnMyOwn, "rdoCreateIntlOnMyOwn");
      this.rdoCreateIntlOnMyOwn.Name = "rdoCreateIntlOnMyOwn";
      this.helpProvider1.SetShowHelp((Control) this.rdoCreateIntlOnMyOwn, (bool) componentResourceManager.GetObject("rdoCreateIntlOnMyOwn.ShowHelp"));
      this.rdoCreateIntlOnMyOwn.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.lbldocPref, "lbldocPref");
      this.lbldocPref.Name = "lbldocPref";
      this.helpProvider1.SetShowHelp((Control) this.lbldocPref, (bool) componentResourceManager.GetObject("lbldocPref.ShowHelp"));
      this.gbxLetterhead.BorderThickness = 1f;
      this.tlpMain.SetColumnSpan((Control) this.gbxLetterhead, 2);
      this.gbxLetterhead.Controls.Add((Control) this.btnUploadImages);
      this.gbxLetterhead.Controls.Add((Control) this.label2);
      componentResourceManager.ApplyResources((object) this.gbxLetterhead, "gbxLetterhead");
      this.gbxLetterhead.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxLetterhead.Name = "gbxLetterhead";
      this.gbxLetterhead.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxLetterhead, (bool) componentResourceManager.GetObject("gbxLetterhead.ShowHelp"));
      this.gbxLetterhead.TabStop = false;
      componentResourceManager.ApplyResources((object) this.btnUploadImages, "btnUploadImages");
      this.helpProvider1.SetHelpKeyword((Control) this.btnUploadImages, componentResourceManager.GetString("btnUploadImages.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnUploadImages, (HelpNavigator) componentResourceManager.GetObject("btnUploadImages.HelpNavigator"));
      this.btnUploadImages.Name = "btnUploadImages";
      this.helpProvider1.SetShowHelp((Control) this.btnUploadImages, (bool) componentResourceManager.GetObject("btnUploadImages.ShowHelp"));
      this.btnUploadImages.UseVisualStyleBackColor = true;
      this.btnUploadImages.Click += new EventHandler(this.btnUploadImages_Click);
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      this.helpProvider1.SetShowHelp((Control) this.label2, (bool) componentResourceManager.GetObject("label2.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.gbxLabelFormat, "gbxLabelFormat");
      this.gbxLabelFormat.BorderThickness = 1f;
      this.tlpMain.SetColumnSpan((Control) this.gbxLabelFormat, 2);
      this.gbxLabelFormat.Controls.Add((Control) this.label1);
      this.gbxLabelFormat.Controls.Add((Control) this.cboBarcodeDocTab);
      this.gbxLabelFormat.Controls.Add((Control) this.rdoBarcodeDocTab);
      this.gbxLabelFormat.Controls.Add((Control) this.btnDocTabConfig);
      this.gbxLabelFormat.Controls.Add((Control) this.rdoCustomizeDocTab);
      this.gbxLabelFormat.Controls.Add((Control) this.rdoDefaultDocTab);
      this.gbxLabelFormat.Controls.Add((Control) this.spnNumBOL);
      this.gbxLabelFormat.Controls.Add((Control) this.chkAutoPrintTradeDocsWithShipment);
      this.gbxLabelFormat.Controls.Add((Control) this.chkAutoPrintBOLWithShipment);
      this.gbxLabelFormat.Controls.Add((Control) this.chkAutoPrintLabelWithShipment);
      this.gbxLabelFormat.Controls.Add((Control) this.cboLabelFormat);
      this.gbxLabelFormat.Controls.Add((Control) this.lblLabelFormat);
      this.gbxLabelFormat.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxLabelFormat.Name = "gbxLabelFormat";
      this.gbxLabelFormat.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxLabelFormat, (bool) componentResourceManager.GetObject("gbxLabelFormat.ShowHelp"));
      this.gbxLabelFormat.TabStop = false;
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.helpProvider1.SetShowHelp((Control) this.label1, (bool) componentResourceManager.GetObject("label1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.cboBarcodeDocTab, "cboBarcodeDocTab");
      this.cboBarcodeDocTab.DisplayMemberQ = "";
      this.cboBarcodeDocTab.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboBarcodeDocTab.DropDownWidth = 310;
      this.cboBarcodeDocTab.DroppedDownQ = false;
      this.cboBarcodeDocTab.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboBarcodeDocTab, componentResourceManager.GetString("cboBarcodeDocTab.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboBarcodeDocTab, (HelpNavigator) componentResourceManager.GetObject("cboBarcodeDocTab.HelpNavigator"));
      this.cboBarcodeDocTab.Name = "cboBarcodeDocTab";
      this.cboBarcodeDocTab.SelectedIndexQ = -1;
      this.cboBarcodeDocTab.SelectedItemQ = (object) null;
      this.cboBarcodeDocTab.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboBarcodeDocTab, (bool) componentResourceManager.GetObject("cboBarcodeDocTab.ShowHelp"));
      this.cboBarcodeDocTab.ValueMemberQ = "";
      this.helpProvider1.SetHelpKeyword((Control) this.rdoBarcodeDocTab, componentResourceManager.GetString("rdoBarcodeDocTab.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoBarcodeDocTab, (HelpNavigator) componentResourceManager.GetObject("rdoBarcodeDocTab.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoBarcodeDocTab, "rdoBarcodeDocTab");
      this.rdoBarcodeDocTab.Name = "rdoBarcodeDocTab";
      this.helpProvider1.SetShowHelp((Control) this.rdoBarcodeDocTab, (bool) componentResourceManager.GetObject("rdoBarcodeDocTab.ShowHelp"));
      this.rdoBarcodeDocTab.UseVisualStyleBackColor = true;
      this.rdoBarcodeDocTab.CheckedChanged += new EventHandler(this.rdoDefaultDocTab_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.btnDocTabConfig, "btnDocTabConfig");
      this.helpProvider1.SetHelpKeyword((Control) this.btnDocTabConfig, componentResourceManager.GetString("btnDocTabConfig.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnDocTabConfig, (HelpNavigator) componentResourceManager.GetObject("btnDocTabConfig.HelpNavigator"));
      this.btnDocTabConfig.Name = "btnDocTabConfig";
      this.helpProvider1.SetShowHelp((Control) this.btnDocTabConfig, (bool) componentResourceManager.GetObject("btnDocTabConfig.ShowHelp"));
      this.btnDocTabConfig.UseVisualStyleBackColor = true;
      this.btnDocTabConfig.Click += new EventHandler(this.btnDocTabConfig_Click);
      this.helpProvider1.SetHelpKeyword((Control) this.rdoCustomizeDocTab, componentResourceManager.GetString("rdoCustomizeDocTab.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoCustomizeDocTab, (HelpNavigator) componentResourceManager.GetObject("rdoCustomizeDocTab.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoCustomizeDocTab, "rdoCustomizeDocTab");
      this.rdoCustomizeDocTab.Name = "rdoCustomizeDocTab";
      this.helpProvider1.SetShowHelp((Control) this.rdoCustomizeDocTab, (bool) componentResourceManager.GetObject("rdoCustomizeDocTab.ShowHelp"));
      this.rdoCustomizeDocTab.UseVisualStyleBackColor = true;
      this.rdoCustomizeDocTab.CheckedChanged += new EventHandler(this.rdoDefaultDocTab_CheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdoDefaultDocTab, componentResourceManager.GetString("rdoDefaultDocTab.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoDefaultDocTab, (HelpNavigator) componentResourceManager.GetObject("rdoDefaultDocTab.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoDefaultDocTab, "rdoDefaultDocTab");
      this.rdoDefaultDocTab.Name = "rdoDefaultDocTab";
      this.helpProvider1.SetShowHelp((Control) this.rdoDefaultDocTab, (bool) componentResourceManager.GetObject("rdoDefaultDocTab.ShowHelp"));
      this.rdoDefaultDocTab.UseVisualStyleBackColor = true;
      this.rdoDefaultDocTab.CheckedChanged += new EventHandler(this.rdoDefaultDocTab_CheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.spnNumBOL, componentResourceManager.GetString("spnNumBOL.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnNumBOL, (HelpNavigator) componentResourceManager.GetObject("spnNumBOL.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.spnNumBOL, "spnNumBOL");
      this.spnNumBOL.Maximum = new Decimal(new int[4]
      {
        9,
        0,
        0,
        0
      });
      this.spnNumBOL.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.spnNumBOL.Name = "spnNumBOL";
      this.spnNumBOL.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.spnNumBOL, (bool) componentResourceManager.GetObject("spnNumBOL.ShowHelp"));
      this.spnNumBOL.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.helpProvider1.SetHelpKeyword((Control) this.chkAutoPrintTradeDocsWithShipment, componentResourceManager.GetString("chkAutoPrintTradeDocsWithShipment.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAutoPrintTradeDocsWithShipment, (HelpNavigator) componentResourceManager.GetObject("chkAutoPrintTradeDocsWithShipment.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAutoPrintTradeDocsWithShipment, "chkAutoPrintTradeDocsWithShipment");
      this.chkAutoPrintTradeDocsWithShipment.Name = "chkAutoPrintTradeDocsWithShipment";
      this.helpProvider1.SetShowHelp((Control) this.chkAutoPrintTradeDocsWithShipment, (bool) componentResourceManager.GetObject("chkAutoPrintTradeDocsWithShipment.ShowHelp"));
      this.chkAutoPrintTradeDocsWithShipment.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkAutoPrintBOLWithShipment, componentResourceManager.GetString("chkAutoPrintBOLWithShipment.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAutoPrintBOLWithShipment, (HelpNavigator) componentResourceManager.GetObject("chkAutoPrintBOLWithShipment.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAutoPrintBOLWithShipment, "chkAutoPrintBOLWithShipment");
      this.chkAutoPrintBOLWithShipment.Name = "chkAutoPrintBOLWithShipment";
      this.helpProvider1.SetShowHelp((Control) this.chkAutoPrintBOLWithShipment, (bool) componentResourceManager.GetObject("chkAutoPrintBOLWithShipment.ShowHelp"));
      this.chkAutoPrintBOLWithShipment.UseVisualStyleBackColor = true;
      this.chkAutoPrintBOLWithShipment.CheckedChanged += new EventHandler(this.chkAutoPrintBOLWithShipment_CheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.chkAutoPrintLabelWithShipment, componentResourceManager.GetString("chkAutoPrintLabelWithShipment.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAutoPrintLabelWithShipment, (HelpNavigator) componentResourceManager.GetObject("chkAutoPrintLabelWithShipment.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAutoPrintLabelWithShipment, "chkAutoPrintLabelWithShipment");
      this.chkAutoPrintLabelWithShipment.Name = "chkAutoPrintLabelWithShipment";
      this.helpProvider1.SetShowHelp((Control) this.chkAutoPrintLabelWithShipment, (bool) componentResourceManager.GetObject("chkAutoPrintLabelWithShipment.ShowHelp"));
      this.chkAutoPrintLabelWithShipment.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.cboLabelFormat, "cboLabelFormat");
      this.cboLabelFormat.DisplayMemberQ = "";
      this.cboLabelFormat.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboLabelFormat.DroppedDownQ = false;
      this.cboLabelFormat.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboLabelFormat, componentResourceManager.GetString("cboLabelFormat.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboLabelFormat, (HelpNavigator) componentResourceManager.GetObject("cboLabelFormat.HelpNavigator"));
      this.cboLabelFormat.Name = "cboLabelFormat";
      this.cboLabelFormat.SelectedIndexQ = -1;
      this.cboLabelFormat.SelectedItemQ = (object) null;
      this.cboLabelFormat.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboLabelFormat, (bool) componentResourceManager.GetObject("cboLabelFormat.ShowHelp"));
      this.cboLabelFormat.ValueMemberQ = "";
      this.cboLabelFormat.SelectedIndexChanged += new EventHandler(this.cboLabelFormat_SelectedIndexChanged);
      componentResourceManager.ApplyResources((object) this.lblLabelFormat, "lblLabelFormat");
      this.lblLabelFormat.Name = "lblLabelFormat";
      this.helpProvider1.SetShowHelp((Control) this.lblLabelFormat, (bool) componentResourceManager.GetObject("lblLabelFormat.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.gbxRequireReferences, "gbxRequireReferences");
      this.gbxRequireReferences.BorderThickness = 1f;
      this.gbxRequireReferences.Controls.Add((Control) this.chkReqBillOfLadingPoNum);
      this.gbxRequireReferences.Controls.Add((Control) this.chkReqConsigneeId);
      this.gbxRequireReferences.Controls.Add((Control) this.chkReqShipId2);
      this.gbxRequireReferences.Controls.Add((Control) this.chkReqShipId1);
      this.gbxRequireReferences.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxRequireReferences.Name = "gbxRequireReferences";
      this.gbxRequireReferences.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxRequireReferences, (bool) componentResourceManager.GetObject("gbxRequireReferences.ShowHelp"));
      this.gbxRequireReferences.TabStop = false;
      componentResourceManager.ApplyResources((object) this.chkReqBillOfLadingPoNum, "chkReqBillOfLadingPoNum");
      this.chkReqBillOfLadingPoNum.Name = "chkReqBillOfLadingPoNum";
      this.chkReqBillOfLadingPoNum.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.chkReqConsigneeId, "chkReqConsigneeId");
      this.chkReqConsigneeId.Name = "chkReqConsigneeId";
      this.chkReqConsigneeId.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.chkReqShipId2, "chkReqShipId2");
      this.chkReqShipId2.Name = "chkReqShipId2";
      this.chkReqShipId2.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.chkReqShipId1, "chkReqShipId1");
      this.chkReqShipId1.Name = "chkReqShipId1";
      this.chkReqShipId1.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.tlpMain, "tlpMain");
      this.tlpMain.Controls.Add((Control) this.gbxETDPref, 0, 1);
      this.tlpMain.Controls.Add((Control) this.gbxRequireReferences, 1, 1);
      this.tlpMain.Controls.Add((Control) this.gbxLetterhead, 0, 2);
      this.tlpMain.Controls.Add((Control) this.gbxLabelFormat, 0, 0);
      this.tlpMain.Name = "tlpMain";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tlpMain);
      this.Name = nameof (FreightOtherPreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.FreightOtherPreferences_Load);
      this.gbxETDPref.ResumeLayout(false);
      this.gbxLetterhead.ResumeLayout(false);
      this.gbxLabelFormat.ResumeLayout(false);
      this.spnNumBOL.EndInit();
      this.gbxRequireReferences.ResumeLayout(false);
      this.gbxRequireReferences.PerformLayout();
      this.tlpMain.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
