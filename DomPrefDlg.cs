// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.DomPrefDlg
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class DomPrefDlg : HelpFormBase
  {
    private DShipDefl _preferenceObject;
    private Utility.FormOperation _eOperation;
    private IContainer components;
    private TabControlEx tabControlDomPrefs;
    private TabPage tabPageDomFieldPrefs;
    private TabPage tabPageExpressPrefs;
    private TabPage tabPageGroupShipPrefs;
    private TabPage tabPageShipAlertPrefs;
    private TabPage tabPageGroundPrefs;
    private Button btnOk;
    private Button btnCancel;
    private DomFieldPreferences DomAllFieldPreferences;
    private DomOtherPreferences DomOtherExpressPreferences;
    private GroupShipPrefs DomGroupShipPrefs;
    private DomShipAlertPreferences DomShipAlertPreferences;
    private DomOtherPreferences DomOtherGroundPreferences;
    private Profile_Header DomProfileHeader;
    private TabPage tabPageSmartPostPrefs;
    private DomOtherPreferences DomOtherSmartPostPreferences;
    private ColorGroupBox colorGroupBox2;
    private ComboBoxEx cboLabelPrintOrder;
    private ColorGroupBox colorGroupBox1;
    private ComboBoxEx cboPrintSecondaryBarCode;
    protected LinkLabelEx lnkPrintSecondaryBarCode;
    protected ColorGroupBox gbxGenTrkNumForSmartPost;
    protected CheckBox chkGenGrndTrkNumInternally;
    public LinkLabelEx lnkGenerateInternally;
    private TableLayoutPanel tableLayoutPanel1;

    public DomPrefDlg(DShipDefl preferenceObject, Utility.FormOperation eOperation)
    {
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this._preferenceObject = preferenceObject;
      this._eOperation = eOperation;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.DomProfileHeader.ProfileCode.Trim()))
      {
        int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(13547), Error.ErrorType.Failure);
        this.DialogResult = DialogResult.None;
      }
      else
      {
        this._preferenceObject.ProfileCode = this.DomProfileHeader.ProfileCode;
        this._preferenceObject.Description = this.DomProfileHeader.ProfileDescription;
        if (!this.DomAllFieldPreferences.OkToClose())
          this.DialogResult = DialogResult.None;
        else if (!this.DomShipAlertPreferences.OkToClose())
        {
          this.DialogResult = DialogResult.None;
        }
        else
        {
          this.DomOtherExpressPreferences.ScreenToObject();
          if (GuiData.CurrentAccount.IsGroundEnabled)
            this.DomOtherGroundPreferences.ScreenToObject();
          if (Utility.HasAnySmartPostMetersOrSenderOnDevice())
          {
            this.DomOtherSmartPostPreferences.ScreenToObject();
            this._preferenceObject.SPSecondaryBarcode = (SecondaryBarcodeOptions) Enum.Parse(typeof (SecondaryBarcodeOptions), this.cboPrintSecondaryBarCode.SelectedValue.ToString());
            this._preferenceObject.GenerateGroundTrackNbr = this.chkGenGrndTrkNumInternally.Checked;
          }
          if (this._preferenceObject.ExpressHandlingCharge.VariableChgInd && this._preferenceObject.ExpressHandlingCharge.ComputationInd == HandlingCharge.HANDLINGCOMPTYPE.FLAT_AMOUNT || this._preferenceObject.SmartPostHandlingCharge.VariableChgInd && this._preferenceObject.SmartPostHandlingCharge.ComputationInd == HandlingCharge.HANDLINGCOMPTYPE.FLAT_AMOUNT || this._preferenceObject.GroundHandlingCharge.VariableChgInd && this._preferenceObject.GroundHandlingCharge.ComputationInd == HandlingCharge.HANDLINGCOMPTYPE.FLAT_AMOUNT)
          {
            int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(13838), Error.ErrorType.Failure);
            this.DialogResult = DialogResult.None;
          }
          else
          {
            this.DomGroupShipPrefs.ScreenToObject(this._preferenceObject);
            if (this._preferenceObject.FieldPrefs[6].Behavior == ShipDefl.Behavior.Skip && this._preferenceObject.RequireReference)
            {
              int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9524), Error.ErrorType.Failure);
              this.DomOtherExpressPreferences.chkRequireReferences.Focus();
              this.DialogResult = DialogResult.None;
            }
            else
            {
              this._preferenceObject.LabelPrintOrder = this.cboLabelPrintOrder.SelectedValue as string;
              this.DialogResult = DialogResult.OK;
            }
          }
        }
      }
    }

    private void DomPrefDlg_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.DomProfileHeader.ProfileCode = this._preferenceObject.ProfileCode;
      this.DomProfileHeader.ProfileDescription = this._preferenceObject.Description;
      this.DomProfileHeader.EnableProfileCode = this._eOperation == Utility.FormOperation.Add || this._eOperation == Utility.FormOperation.AddByDup;
      this.DomProfileHeader.EnableProfileDesc = this._preferenceObject.ProfileCode != "DEFAULT";
      this.DomAllFieldPreferences.PreferenceObject = (ShipDefl) this._preferenceObject;
      this.DomShipAlertPreferences.PreferenceObject = (ShipDefl) this._preferenceObject;
      this.DomOtherExpressPreferences.InitOtherPrefs(this._preferenceObject, Shipment.CarrierType.Express);
      if (GuiData.CurrentAccount.IsGroundEnabled && (GuiData.CurrentAccount.Address == null || !(GuiData.CurrentAccount.Address.CountryCode == "MX")))
        this.DomOtherGroundPreferences.InitOtherPrefs(this._preferenceObject, Shipment.CarrierType.Ground);
      else
        this.tabControlDomPrefs.HideTabPage(this.tabPageGroundPrefs);
      if (Utility.HasAnySmartPostMetersOrSenderOnDevice())
      {
        this.DomOtherSmartPostPreferences.InitOtherPrefs(this._preferenceObject, Shipment.CarrierType.SmartPost);
        this.cboPrintSecondaryBarCode.DataSource = (object) Utility.GetDataTable(Utility.ListTypes.SecondaryBarCodeContent);
        this.cboPrintSecondaryBarCode.ValueMember = "Code";
        this.cboPrintSecondaryBarCode.DisplayMember = "Description";
        this.cboPrintSecondaryBarCode.SelectedValue = (object) Enum.Format(typeof (SecondaryBarcodeOptions), (object) this._preferenceObject.SPSecondaryBarcode, "d");
        this.chkGenGrndTrkNumInternally.Checked = this._preferenceObject.GenerateGroundTrackNbr;
        if (Utility.HasAnySmartPostReturnSenders(GuiData.CurrentAccount) || GuiData.CurrentAccount.SPPickupCarrier == Shipment.CarrierType.Ground)
          this.gbxGenTrkNumForSmartPost.Visible = true;
        else
          this.gbxGenTrkNumForSmartPost.Visible = false;
      }
      else
        this.tabControlDomPrefs.HideTabPage(this.tabPageSmartPostPrefs);
      if (GuiData.CurrentAccount.Address != null && (GuiData.CurrentAccount.Address.CountryCode == "CA" || GuiData.CurrentAccount.Address.CountryCode == "MX"))
        this.DomGroupShipPrefs.SetForOriginCountry(GuiData.CurrentAccount.Address.CountryCode);
      if (this.cboLabelPrintOrder.Enabled)
      {
        this.cboLabelPrintOrder.DataSource = (object) Utility.GetDataTable(Utility.ListTypes.LabelPrintOrder);
        this.cboLabelPrintOrder.ValueMember = "Code";
        this.cboLabelPrintOrder.DisplayMember = "Description";
      }
      this.cboLabelPrintOrder.SelectedValue = (object) this._preferenceObject.LabelPrintOrder;
      this.ObjectToScreen();
    }

    private void ObjectToScreen()
    {
      this.DomAllFieldPreferences.ObjectToScreen();
      this.DomOtherExpressPreferences.ObjectToScreen();
      this.DomGroupShipPrefs.ObjectToScreen(this._preferenceObject);
      if (GuiData.CurrentAccount.IsGroundEnabled)
        this.DomOtherGroundPreferences.ObjectToScreen();
      if (!Utility.HasAnySmartPostMetersOrSenderOnDevice())
        return;
      this.DomOtherSmartPostPreferences.ObjectToScreen();
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DomPrefDlg));
      this.tabControlDomPrefs = new TabControlEx();
      this.tabPageDomFieldPrefs = new TabPage();
      this.DomAllFieldPreferences = new DomFieldPreferences();
      this.tabPageExpressPrefs = new TabPage();
      this.colorGroupBox2 = new ColorGroupBox();
      this.cboLabelPrintOrder = new ComboBoxEx();
      this.DomOtherExpressPreferences = new DomOtherPreferences();
      this.tabPageGroupShipPrefs = new TabPage();
      this.DomGroupShipPrefs = new GroupShipPrefs();
      this.tabPageShipAlertPrefs = new TabPage();
      this.DomShipAlertPreferences = new DomShipAlertPreferences();
      this.tabPageGroundPrefs = new TabPage();
      this.DomOtherGroundPreferences = new DomOtherPreferences();
      this.tabPageSmartPostPrefs = new TabPage();
      this.gbxGenTrkNumForSmartPost = new ColorGroupBox();
      this.lnkGenerateInternally = new LinkLabelEx();
      this.chkGenGrndTrkNumInternally = new CheckBox();
      this.colorGroupBox1 = new ColorGroupBox();
      this.lnkPrintSecondaryBarCode = new LinkLabelEx();
      this.cboPrintSecondaryBarCode = new ComboBoxEx();
      this.DomOtherSmartPostPreferences = new DomOtherPreferences();
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.DomProfileHeader = new Profile_Header();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.tabControlDomPrefs.SuspendLayout();
      this.tabPageDomFieldPrefs.SuspendLayout();
      this.tabPageExpressPrefs.SuspendLayout();
      this.colorGroupBox2.SuspendLayout();
      this.tabPageGroupShipPrefs.SuspendLayout();
      this.tabPageShipAlertPrefs.SuspendLayout();
      this.tabPageGroundPrefs.SuspendLayout();
      this.tabPageSmartPostPrefs.SuspendLayout();
      this.gbxGenTrkNumForSmartPost.SuspendLayout();
      this.colorGroupBox1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.tabControlDomPrefs, "tabControlDomPrefs");
      this.tableLayoutPanel1.SetColumnSpan((Control) this.tabControlDomPrefs, 3);
      this.tabControlDomPrefs.Controls.Add((Control) this.tabPageDomFieldPrefs);
      this.tabControlDomPrefs.Controls.Add((Control) this.tabPageExpressPrefs);
      this.tabControlDomPrefs.Controls.Add((Control) this.tabPageGroupShipPrefs);
      this.tabControlDomPrefs.Controls.Add((Control) this.tabPageShipAlertPrefs);
      this.tabControlDomPrefs.Controls.Add((Control) this.tabPageGroundPrefs);
      this.tabControlDomPrefs.Controls.Add((Control) this.tabPageSmartPostPrefs);
      this.tabControlDomPrefs.DrawMode = TabDrawMode.OwnerDrawFixed;
      this.tabControlDomPrefs.MnemonicEnabled = true;
      this.tabControlDomPrefs.Multiline = true;
      this.tabControlDomPrefs.Name = "tabControlDomPrefs";
      this.tabControlDomPrefs.SelectedIndex = 0;
      this.helpProvider1.SetShowHelp((Control) this.tabControlDomPrefs, (bool) componentResourceManager.GetObject("tabControlDomPrefs.ShowHelp"));
      this.tabControlDomPrefs.UseIndexAsMnemonic = true;
      this.tabPageDomFieldPrefs.Controls.Add((Control) this.DomAllFieldPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageDomFieldPrefs, "tabPageDomFieldPrefs");
      this.tabPageDomFieldPrefs.Name = "tabPageDomFieldPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageDomFieldPrefs, (bool) componentResourceManager.GetObject("tabPageDomFieldPrefs.ShowHelp"));
      this.tabPageDomFieldPrefs.UseVisualStyleBackColor = true;
      this.DomAllFieldPreferences.BackColor = Color.White;
      this.DomAllFieldPreferences.CurrentPrefTypeIndex = -1;
      componentResourceManager.ApplyResources((object) this.DomAllFieldPreferences, "DomAllFieldPreferences");
      this.DomAllFieldPreferences.IsLoading = false;
      this.DomAllFieldPreferences.Name = "DomAllFieldPreferences";
      this.DomAllFieldPreferences.PreferenceObject = (ShipDefl) null;
      this.DomAllFieldPreferences.PrevPrefTypeIndex = -1;
      this.helpProvider1.SetShowHelp((Control) this.DomAllFieldPreferences, (bool) componentResourceManager.GetObject("DomAllFieldPreferences.ShowHelp"));
      this.tabPageExpressPrefs.BackColor = Color.White;
      this.tabPageExpressPrefs.Controls.Add((Control) this.colorGroupBox2);
      this.tabPageExpressPrefs.Controls.Add((Control) this.DomOtherExpressPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageExpressPrefs, "tabPageExpressPrefs");
      this.tabPageExpressPrefs.Name = "tabPageExpressPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageExpressPrefs, (bool) componentResourceManager.GetObject("tabPageExpressPrefs.ShowHelp"));
      this.tabPageExpressPrefs.UseVisualStyleBackColor = true;
      this.colorGroupBox2.BackColor = Color.White;
      this.colorGroupBox2.BorderThickness = 1f;
      this.colorGroupBox2.Controls.Add((Control) this.cboLabelPrintOrder);
      this.colorGroupBox2.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.colorGroupBox2, "colorGroupBox2");
      this.colorGroupBox2.Name = "colorGroupBox2";
      this.colorGroupBox2.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox2, (bool) componentResourceManager.GetObject("colorGroupBox2.ShowHelp"));
      this.colorGroupBox2.TabStop = false;
      this.cboLabelPrintOrder.DisplayMemberQ = "";
      this.cboLabelPrintOrder.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboLabelPrintOrder.DroppedDownQ = false;
      this.cboLabelPrintOrder.FormattingEnabled = true;
      componentResourceManager.ApplyResources((object) this.cboLabelPrintOrder, "cboLabelPrintOrder");
      this.cboLabelPrintOrder.Name = "cboLabelPrintOrder";
      this.cboLabelPrintOrder.SelectedIndexQ = -1;
      this.cboLabelPrintOrder.SelectedItemQ = (object) null;
      this.cboLabelPrintOrder.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboLabelPrintOrder, (bool) componentResourceManager.GetObject("cboLabelPrintOrder.ShowHelp"));
      this.cboLabelPrintOrder.ValueMemberQ = "";
      this.DomOtherExpressPreferences.Carrier = Shipment.CarrierType.NoCarrier;
      componentResourceManager.ApplyResources((object) this.DomOtherExpressPreferences, "DomOtherExpressPreferences");
      this.DomOtherExpressPreferences.Name = "DomOtherExpressPreferences";
      this.DomOtherExpressPreferences.PreferenceObject = (ShipDefl) null;
      this.helpProvider1.SetShowHelp((Control) this.DomOtherExpressPreferences, (bool) componentResourceManager.GetObject("DomOtherExpressPreferences.ShowHelp"));
      this.tabPageGroupShipPrefs.Controls.Add((Control) this.DomGroupShipPrefs);
      componentResourceManager.ApplyResources((object) this.tabPageGroupShipPrefs, "tabPageGroupShipPrefs");
      this.tabPageGroupShipPrefs.Name = "tabPageGroupShipPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageGroupShipPrefs, (bool) componentResourceManager.GetObject("tabPageGroupShipPrefs.ShowHelp"));
      this.tabPageGroupShipPrefs.UseVisualStyleBackColor = true;
      this.DomGroupShipPrefs.BackColor = Color.White;
      componentResourceManager.ApplyResources((object) this.DomGroupShipPrefs, "DomGroupShipPrefs");
      this.DomGroupShipPrefs.Name = "DomGroupShipPrefs";
      this.helpProvider1.SetShowHelp((Control) this.DomGroupShipPrefs, (bool) componentResourceManager.GetObject("DomGroupShipPrefs.ShowHelp"));
      this.tabPageShipAlertPrefs.Controls.Add((Control) this.DomShipAlertPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageShipAlertPrefs, "tabPageShipAlertPrefs");
      this.tabPageShipAlertPrefs.Name = "tabPageShipAlertPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageShipAlertPrefs, (bool) componentResourceManager.GetObject("tabPageShipAlertPrefs.ShowHelp"));
      this.tabPageShipAlertPrefs.UseVisualStyleBackColor = true;
      this.DomShipAlertPreferences.BackColor = Color.White;
      this.DomShipAlertPreferences.CurrentPrefTypeIndex = -1;
      componentResourceManager.ApplyResources((object) this.DomShipAlertPreferences, "DomShipAlertPreferences");
      this.DomShipAlertPreferences.IsLoading = false;
      this.DomShipAlertPreferences.Name = "DomShipAlertPreferences";
      this.DomShipAlertPreferences.PreferenceObject = (ShipDefl) null;
      this.DomShipAlertPreferences.PrevPrefTypeIndex = -1;
      this.helpProvider1.SetShowHelp((Control) this.DomShipAlertPreferences, (bool) componentResourceManager.GetObject("DomShipAlertPreferences.ShowHelp"));
      this.tabPageGroundPrefs.Controls.Add((Control) this.DomOtherGroundPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageGroundPrefs, "tabPageGroundPrefs");
      this.tabPageGroundPrefs.Name = "tabPageGroundPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageGroundPrefs, (bool) componentResourceManager.GetObject("tabPageGroundPrefs.ShowHelp"));
      this.tabPageGroundPrefs.UseVisualStyleBackColor = true;
      this.DomOtherGroundPreferences.BackColor = Color.White;
      this.DomOtherGroundPreferences.Carrier = Shipment.CarrierType.NoCarrier;
      componentResourceManager.ApplyResources((object) this.DomOtherGroundPreferences, "DomOtherGroundPreferences");
      this.DomOtherGroundPreferences.Name = "DomOtherGroundPreferences";
      this.DomOtherGroundPreferences.PreferenceObject = (ShipDefl) null;
      this.helpProvider1.SetShowHelp((Control) this.DomOtherGroundPreferences, (bool) componentResourceManager.GetObject("DomOtherGroundPreferences.ShowHelp"));
      this.tabPageSmartPostPrefs.Controls.Add((Control) this.gbxGenTrkNumForSmartPost);
      this.tabPageSmartPostPrefs.Controls.Add((Control) this.colorGroupBox1);
      this.tabPageSmartPostPrefs.Controls.Add((Control) this.DomOtherSmartPostPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageSmartPostPrefs, "tabPageSmartPostPrefs");
      this.tabPageSmartPostPrefs.Name = "tabPageSmartPostPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageSmartPostPrefs, (bool) componentResourceManager.GetObject("tabPageSmartPostPrefs.ShowHelp"));
      this.tabPageSmartPostPrefs.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.gbxGenTrkNumForSmartPost, "gbxGenTrkNumForSmartPost");
      this.gbxGenTrkNumForSmartPost.BackColor = Color.White;
      this.gbxGenTrkNumForSmartPost.BorderThickness = 1f;
      this.gbxGenTrkNumForSmartPost.Controls.Add((Control) this.lnkGenerateInternally);
      this.gbxGenTrkNumForSmartPost.Controls.Add((Control) this.chkGenGrndTrkNumInternally);
      this.gbxGenTrkNumForSmartPost.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxGenTrkNumForSmartPost.Name = "gbxGenTrkNumForSmartPost";
      this.gbxGenTrkNumForSmartPost.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxGenTrkNumForSmartPost, (bool) componentResourceManager.GetObject("gbxGenTrkNumForSmartPost.ShowHelp"));
      this.gbxGenTrkNumForSmartPost.TabStop = false;
      componentResourceManager.ApplyResources((object) this.lnkGenerateInternally, "lnkGenerateInternally");
      this.lnkGenerateInternally.Name = "lnkGenerateInternally";
      this.helpProvider1.SetShowHelp((Control) this.lnkGenerateInternally, (bool) componentResourceManager.GetObject("lnkGenerateInternally.ShowHelp"));
      this.lnkGenerateInternally.TabStop = true;
      this.lnkGenerateInternally.Tag = (object) "4995";
      this.lnkGenerateInternally.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
      this.helpProvider1.SetHelpKeyword((Control) this.chkGenGrndTrkNumInternally, componentResourceManager.GetString("chkGenGrndTrkNumInternally.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkGenGrndTrkNumInternally, (HelpNavigator) componentResourceManager.GetObject("chkGenGrndTrkNumInternally.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkGenGrndTrkNumInternally, "chkGenGrndTrkNumInternally");
      this.chkGenGrndTrkNumInternally.Name = "chkGenGrndTrkNumInternally";
      this.helpProvider1.SetShowHelp((Control) this.chkGenGrndTrkNumInternally, (bool) componentResourceManager.GetObject("chkGenGrndTrkNumInternally.ShowHelp"));
      this.chkGenGrndTrkNumInternally.UseVisualStyleBackColor = true;
      this.colorGroupBox1.BackColor = Color.White;
      this.colorGroupBox1.BorderThickness = 1f;
      this.colorGroupBox1.Controls.Add((Control) this.lnkPrintSecondaryBarCode);
      this.colorGroupBox1.Controls.Add((Control) this.cboPrintSecondaryBarCode);
      this.colorGroupBox1.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.colorGroupBox1, "colorGroupBox1");
      this.colorGroupBox1.Name = "colorGroupBox1";
      this.colorGroupBox1.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox1, (bool) componentResourceManager.GetObject("colorGroupBox1.ShowHelp"));
      this.colorGroupBox1.TabStop = false;
      componentResourceManager.ApplyResources((object) this.lnkPrintSecondaryBarCode, "lnkPrintSecondaryBarCode");
      this.lnkPrintSecondaryBarCode.Name = "lnkPrintSecondaryBarCode";
      this.helpProvider1.SetShowHelp((Control) this.lnkPrintSecondaryBarCode, (bool) componentResourceManager.GetObject("lnkPrintSecondaryBarCode.ShowHelp"));
      this.lnkPrintSecondaryBarCode.TabStop = true;
      this.lnkPrintSecondaryBarCode.Tag = (object) "4996";
      this.lnkPrintSecondaryBarCode.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnk_LinkClicked);
      this.cboPrintSecondaryBarCode.DisplayMemberQ = "";
      this.cboPrintSecondaryBarCode.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboPrintSecondaryBarCode.DroppedDownQ = false;
      this.cboPrintSecondaryBarCode.FormattingEnabled = true;
      componentResourceManager.ApplyResources((object) this.cboPrintSecondaryBarCode, "cboPrintSecondaryBarCode");
      this.cboPrintSecondaryBarCode.Name = "cboPrintSecondaryBarCode";
      this.cboPrintSecondaryBarCode.SelectedIndexQ = -1;
      this.cboPrintSecondaryBarCode.SelectedItemQ = (object) null;
      this.cboPrintSecondaryBarCode.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboPrintSecondaryBarCode, (bool) componentResourceManager.GetObject("cboPrintSecondaryBarCode.ShowHelp"));
      this.cboPrintSecondaryBarCode.ValueMemberQ = "";
      this.DomOtherSmartPostPreferences.BackColor = Color.White;
      this.DomOtherSmartPostPreferences.Carrier = Shipment.CarrierType.NoCarrier;
      componentResourceManager.ApplyResources((object) this.DomOtherSmartPostPreferences, "DomOtherSmartPostPreferences");
      this.DomOtherSmartPostPreferences.Name = "DomOtherSmartPostPreferences";
      this.DomOtherSmartPostPreferences.PreferenceObject = (ShipDefl) null;
      this.helpProvider1.SetShowHelp((Control) this.DomOtherSmartPostPreferences, (bool) componentResourceManager.GetObject("DomOtherSmartPostPreferences.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.helpProvider1.SetShowHelp((Control) this.btnOk, (bool) componentResourceManager.GetObject("btnOk.ShowHelp"));
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Name = "btnCancel";
      this.helpProvider1.SetShowHelp((Control) this.btnCancel, (bool) componentResourceManager.GetObject("btnCancel.ShowHelp"));
      this.btnCancel.UseVisualStyleBackColor = true;
      this.tableLayoutPanel1.SetColumnSpan((Control) this.DomProfileHeader, 3);
      componentResourceManager.ApplyResources((object) this.DomProfileHeader, "DomProfileHeader");
      this.DomProfileHeader.Name = "DomProfileHeader";
      this.DomProfileHeader.ProfileCode = "Profile Name";
      this.DomProfileHeader.ProfileDescription = "Profile Description";
      this.helpProvider1.SetShowHelp((Control) this.DomProfileHeader, (bool) componentResourceManager.GetObject("DomProfileHeader.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.DomProfileHeader, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.btnCancel, 3, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.btnOk, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.tabControlDomPrefs, 0, 1);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (DomPrefDlg);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Load += new EventHandler(this.DomPrefDlg_Load);
      this.tabControlDomPrefs.ResumeLayout(false);
      this.tabPageDomFieldPrefs.ResumeLayout(false);
      this.tabPageExpressPrefs.ResumeLayout(false);
      this.colorGroupBox2.ResumeLayout(false);
      this.tabPageGroupShipPrefs.ResumeLayout(false);
      this.tabPageShipAlertPrefs.ResumeLayout(false);
      this.tabPageGroundPrefs.ResumeLayout(false);
      this.tabPageSmartPostPrefs.ResumeLayout(false);
      this.gbxGenTrkNumForSmartPost.ResumeLayout(false);
      this.colorGroupBox1.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
