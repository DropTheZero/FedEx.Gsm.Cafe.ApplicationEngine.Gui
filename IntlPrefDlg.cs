// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.IntlPrefDlg
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
  public class IntlPrefDlg : HelpFormBase
  {
    private IShipDefl _preferenceObject;
    private Utility.FormOperation _eOperation;
    private bool _setToCustomsTab;
    private Account _account;
    private IContainer components;
    private TabControlEx tabControlIntlPrefs;
    private TabPage tabPageIntlFieldPrefs;
    private TabPage tabPageIntlExpressOtherPrefs;
    private TabPage tabPageIntlShipAlertPrefs;
    private TabPage tabPageIntlGroundPrefs;
    private IntlFieldPreferences intlFieldPreferences;
    private IntlOtherPreferences intlOtherExpressPreferences;
    private IntlShipAlertPreferences intlShipAlertPreferences;
    private IntlOtherPreferences intlOtherGroundPreferences;
    private Button btnCancel;
    private Button btnOk;
    private Profile_Header IntlProfileHeader;
    private ColorGroupBox gbxIntlWaybillFormat;
    private ComboBox cboIntlWaybillFormat;
    private TabPage tabPageCustomsDocuments;
    private CustomsDocuments customsDocuments1;
    private ColorGroupBox colorGroupBox2;
    private ComboBoxEx cboLabelPrintOrder;
    private TableLayoutPanel tableLayoutPanel1;

    public IShipDefl PreferenceObject
    {
      get => this._preferenceObject;
      set => this._preferenceObject = value;
    }

    private Account CurrentAccount
    {
      get => this._account;
      set
      {
        if (value == null)
          return;
        this._account = value;
        this.intlFieldPreferences.CurrentAccount = value;
        this.intlShipAlertPreferences.CurrentAccount = value;
        this.customsDocuments1.CurrentAccount = value;
        this.intlOtherExpressPreferences.CurrentAccount = value;
        this.intlOtherGroundPreferences.CurrentAccount = value;
      }
    }

    public void SwitchToCustomsTab() => this._setToCustomsTab = true;

    public IntlPrefDlg(IShipDefl preferenceObject, Utility.FormOperation eOperation)
    {
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this._account = GuiData.CurrentAccount;
      this._preferenceObject = preferenceObject;
      this._eOperation = eOperation;
    }

    public IntlPrefDlg(
      Account account,
      IShipDefl preferenceObject,
      Utility.FormOperation eOperation)
    {
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this.CurrentAccount = account ?? GuiData.CurrentAccount;
      this._preferenceObject = preferenceObject;
      this._eOperation = eOperation;
    }

    private void IntlPrefDlg_Load(object sender, EventArgs e)
    {
      this.IntlProfileHeader.ProfileCode = this._preferenceObject.ProfileCode;
      this.IntlProfileHeader.ProfileDescription = this._preferenceObject.Description;
      this.IntlProfileHeader.EnableProfileCode = this._eOperation == Utility.FormOperation.Add || this._eOperation == Utility.FormOperation.AddByDup;
      this.IntlProfileHeader.EnableProfileDesc = this._preferenceObject.ProfileCode != "DEFAULT";
      this.intlFieldPreferences.PreferenceObject = (ShipDefl) this._preferenceObject;
      this.intlOtherExpressPreferences.InitOtherPrefs(this._preferenceObject, Shipment.CarrierType.Express);
      this.intlShipAlertPreferences.PreferenceObject = (ShipDefl) this._preferenceObject;
      this.customsDocuments1.InitCustomsDocPrefs((ShipDefl) this._preferenceObject);
      if (this.CurrentAccount.IsGroundEnabled && !this.CurrentAccount.is_ZENITH_CHANGES_INIT)
      {
        this.intlOtherGroundPreferences.InitOtherPrefs(this._preferenceObject, Shipment.CarrierType.Ground);
      }
      else
      {
        this.tabControlIntlPrefs.HideTabPage(this.tabPageIntlGroundPrefs);
        this.tabControlIntlPrefs.TabPages[this.tabControlIntlPrefs.TabPages.Count - 1].Text = this.tabControlIntlPrefs.TabPages[this.tabControlIntlPrefs.TabPages.Count - 1].Text.Replace("5", "4");
      }
      if (this.cboIntlWaybillFormat.Enabled)
      {
        this.cboIntlWaybillFormat.DataSource = (object) Utility.GetDataTable(Utility.ListTypes.IawbFormat);
        this.cboIntlWaybillFormat.ValueMember = "Code";
        this.cboIntlWaybillFormat.DisplayMember = "Description";
      }
      if (this.cboLabelPrintOrder.Enabled)
      {
        this.cboLabelPrintOrder.DataSource = (object) Utility.GetDataTable(Utility.ListTypes.LabelPrintOrder);
        this.cboLabelPrintOrder.ValueMember = "Code";
        this.cboLabelPrintOrder.DisplayMember = "Description";
      }
      this.cboLabelPrintOrder.SelectedValue = (object) this._preferenceObject.LabelPrintOrder;
      this.ObjectToScreen();
      if (!this._setToCustomsTab)
        return;
      this.tabControlIntlPrefs.SelectTab(this.tabPageCustomsDocuments.Name);
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.IntlProfileHeader.ProfileCode.Trim()))
      {
        int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(13547), Error.ErrorType.Failure);
        this.DialogResult = DialogResult.None;
      }
      else
      {
        this._preferenceObject.ProfileCode = this.IntlProfileHeader.ProfileCode;
        this._preferenceObject.Description = this.IntlProfileHeader.ProfileDescription;
        if (!this.intlFieldPreferences.OkToClose())
          this.DialogResult = DialogResult.None;
        else if (!this.intlShipAlertPreferences.OkToClose())
        {
          this.DialogResult = DialogResult.None;
        }
        else
        {
          this.intlOtherExpressPreferences.ScreenToObject();
          if (this.CurrentAccount.IsGroundEnabled)
            this.intlOtherGroundPreferences.ScreenToObject();
          if (this._preferenceObject.ExpressHandlingCharge.VariableChgInd && this._preferenceObject.ExpressHandlingCharge.ComputationInd == HandlingCharge.HANDLINGCOMPTYPE.FLAT_AMOUNT || this._preferenceObject.GroundHandlingCharge.VariableChgInd && this._preferenceObject.GroundHandlingCharge.ComputationInd == HandlingCharge.HANDLINGCOMPTYPE.FLAT_AMOUNT)
          {
            int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(13838), Error.ErrorType.Failure);
            this.DialogResult = DialogResult.None;
          }
          else if (this._preferenceObject.FieldPrefs[12].Behavior == ShipDefl.Behavior.Skip && this._preferenceObject.RequireReference)
          {
            int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9524), Error.ErrorType.Failure);
            this.intlOtherExpressPreferences.chkRequireReferences.Focus();
            this.DialogResult = DialogResult.None;
          }
          else
          {
            this.customsDocuments1.ScreenToObject();
            this._preferenceObject.LabelPrintOrder = this.cboLabelPrintOrder.SelectedValue as string;
            this.DialogResult = DialogResult.OK;
          }
        }
      }
    }

    private void ObjectToScreen()
    {
      this.intlFieldPreferences.ObjectToScreen();
      this.intlOtherExpressPreferences.ObjectToScreen();
      if (!this.CurrentAccount.IsGroundEnabled)
        return;
      this.intlOtherGroundPreferences.ObjectToScreen();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IntlPrefDlg));
      this.btnCancel = new Button();
      this.btnOk = new Button();
      this.tabControlIntlPrefs = new TabControlEx();
      this.tabPageIntlFieldPrefs = new TabPage();
      this.tabPageIntlExpressOtherPrefs = new TabPage();
      this.colorGroupBox2 = new ColorGroupBox();
      this.cboLabelPrintOrder = new ComboBoxEx();
      this.gbxIntlWaybillFormat = new ColorGroupBox();
      this.cboIntlWaybillFormat = new ComboBox();
      this.tabPageIntlShipAlertPrefs = new TabPage();
      this.tabPageIntlGroundPrefs = new TabPage();
      this.tabPageCustomsDocuments = new TabPage();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.IntlProfileHeader = new Profile_Header();
      this.intlFieldPreferences = new IntlFieldPreferences();
      this.intlOtherExpressPreferences = new IntlOtherPreferences();
      this.intlShipAlertPreferences = new IntlShipAlertPreferences();
      this.intlOtherGroundPreferences = new IntlOtherPreferences();
      this.customsDocuments1 = new CustomsDocuments();
      this.tabControlIntlPrefs.SuspendLayout();
      this.tabPageIntlFieldPrefs.SuspendLayout();
      this.tabPageIntlExpressOtherPrefs.SuspendLayout();
      this.colorGroupBox2.SuspendLayout();
      this.gbxIntlWaybillFormat.SuspendLayout();
      this.tabPageIntlShipAlertPrefs.SuspendLayout();
      this.tabPageIntlGroundPrefs.SuspendLayout();
      this.tabPageCustomsDocuments.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Name = "btnCancel";
      this.helpProvider1.SetShowHelp((Control) this.btnCancel, (bool) componentResourceManager.GetObject("btnCancel.ShowHelp"));
      this.btnCancel.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.helpProvider1.SetShowHelp((Control) this.btnOk, (bool) componentResourceManager.GetObject("btnOk.ShowHelp"));
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.tableLayoutPanel1.SetColumnSpan((Control) this.tabControlIntlPrefs, 3);
      this.tabControlIntlPrefs.Controls.Add((Control) this.tabPageIntlFieldPrefs);
      this.tabControlIntlPrefs.Controls.Add((Control) this.tabPageIntlExpressOtherPrefs);
      this.tabControlIntlPrefs.Controls.Add((Control) this.tabPageIntlShipAlertPrefs);
      this.tabControlIntlPrefs.Controls.Add((Control) this.tabPageIntlGroundPrefs);
      this.tabControlIntlPrefs.Controls.Add((Control) this.tabPageCustomsDocuments);
      componentResourceManager.ApplyResources((object) this.tabControlIntlPrefs, "tabControlIntlPrefs");
      this.tabControlIntlPrefs.DrawMode = TabDrawMode.OwnerDrawFixed;
      this.tabControlIntlPrefs.MnemonicEnabled = true;
      this.tabControlIntlPrefs.Multiline = true;
      this.tabControlIntlPrefs.Name = "tabControlIntlPrefs";
      this.tabControlIntlPrefs.SelectedIndex = 0;
      this.helpProvider1.SetShowHelp((Control) this.tabControlIntlPrefs, (bool) componentResourceManager.GetObject("tabControlIntlPrefs.ShowHelp"));
      this.tabControlIntlPrefs.UseIndexAsMnemonic = true;
      this.tabPageIntlFieldPrefs.Controls.Add((Control) this.intlFieldPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageIntlFieldPrefs, "tabPageIntlFieldPrefs");
      this.tabPageIntlFieldPrefs.Name = "tabPageIntlFieldPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageIntlFieldPrefs, (bool) componentResourceManager.GetObject("tabPageIntlFieldPrefs.ShowHelp"));
      this.tabPageIntlFieldPrefs.UseVisualStyleBackColor = true;
      this.tabPageIntlExpressOtherPrefs.Controls.Add((Control) this.colorGroupBox2);
      this.tabPageIntlExpressOtherPrefs.Controls.Add((Control) this.gbxIntlWaybillFormat);
      this.tabPageIntlExpressOtherPrefs.Controls.Add((Control) this.intlOtherExpressPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageIntlExpressOtherPrefs, "tabPageIntlExpressOtherPrefs");
      this.tabPageIntlExpressOtherPrefs.Name = "tabPageIntlExpressOtherPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageIntlExpressOtherPrefs, (bool) componentResourceManager.GetObject("tabPageIntlExpressOtherPrefs.ShowHelp"));
      this.tabPageIntlExpressOtherPrefs.UseVisualStyleBackColor = true;
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
      this.cboLabelPrintOrder.DropDownWidth = 258;
      this.cboLabelPrintOrder.DroppedDownQ = false;
      this.cboLabelPrintOrder.FormattingEnabled = true;
      componentResourceManager.ApplyResources((object) this.cboLabelPrintOrder, "cboLabelPrintOrder");
      this.cboLabelPrintOrder.Name = "cboLabelPrintOrder";
      this.cboLabelPrintOrder.SelectedIndexQ = -1;
      this.cboLabelPrintOrder.SelectedItemQ = (object) null;
      this.cboLabelPrintOrder.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboLabelPrintOrder, (bool) componentResourceManager.GetObject("cboLabelPrintOrder.ShowHelp"));
      this.cboLabelPrintOrder.ValueMemberQ = "";
      this.gbxIntlWaybillFormat.BackColor = Color.White;
      this.gbxIntlWaybillFormat.BorderThickness = 1f;
      this.gbxIntlWaybillFormat.Controls.Add((Control) this.cboIntlWaybillFormat);
      this.gbxIntlWaybillFormat.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxIntlWaybillFormat, "gbxIntlWaybillFormat");
      this.gbxIntlWaybillFormat.Name = "gbxIntlWaybillFormat";
      this.gbxIntlWaybillFormat.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxIntlWaybillFormat, (bool) componentResourceManager.GetObject("gbxIntlWaybillFormat.ShowHelp"));
      this.gbxIntlWaybillFormat.TabStop = false;
      this.cboIntlWaybillFormat.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboIntlWaybillFormat.FormattingEnabled = true;
      componentResourceManager.ApplyResources((object) this.cboIntlWaybillFormat, "cboIntlWaybillFormat");
      this.cboIntlWaybillFormat.Name = "cboIntlWaybillFormat";
      this.helpProvider1.SetShowHelp((Control) this.cboIntlWaybillFormat, (bool) componentResourceManager.GetObject("cboIntlWaybillFormat.ShowHelp"));
      this.tabPageIntlShipAlertPrefs.Controls.Add((Control) this.intlShipAlertPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageIntlShipAlertPrefs, "tabPageIntlShipAlertPrefs");
      this.tabPageIntlShipAlertPrefs.Name = "tabPageIntlShipAlertPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageIntlShipAlertPrefs, (bool) componentResourceManager.GetObject("tabPageIntlShipAlertPrefs.ShowHelp"));
      this.tabPageIntlShipAlertPrefs.UseVisualStyleBackColor = true;
      this.tabPageIntlGroundPrefs.Controls.Add((Control) this.intlOtherGroundPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageIntlGroundPrefs, "tabPageIntlGroundPrefs");
      this.tabPageIntlGroundPrefs.Name = "tabPageIntlGroundPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageIntlGroundPrefs, (bool) componentResourceManager.GetObject("tabPageIntlGroundPrefs.ShowHelp"));
      this.tabPageIntlGroundPrefs.UseVisualStyleBackColor = true;
      this.tabPageCustomsDocuments.Controls.Add((Control) this.customsDocuments1);
      componentResourceManager.ApplyResources((object) this.tabPageCustomsDocuments, "tabPageCustomsDocuments");
      this.tabPageCustomsDocuments.Name = "tabPageCustomsDocuments";
      this.helpProvider1.SetShowHelp((Control) this.tabPageCustomsDocuments, (bool) componentResourceManager.GetObject("tabPageCustomsDocuments.ShowHelp"));
      this.tabPageCustomsDocuments.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.btnCancel, 2, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.btnOk, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.IntlProfileHeader, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.tabControlIntlPrefs, 0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.helpProvider1.SetShowHelp((Control) this.tableLayoutPanel1, (bool) componentResourceManager.GetObject("tableLayoutPanel1.ShowHelp"));
      this.tableLayoutPanel1.SetColumnSpan((Control) this.IntlProfileHeader, 3);
      componentResourceManager.ApplyResources((object) this.IntlProfileHeader, "IntlProfileHeader");
      this.IntlProfileHeader.Name = "IntlProfileHeader";
      this.IntlProfileHeader.ProfileCode = "Profile Name";
      this.IntlProfileHeader.ProfileDescription = "Profile Description";
      this.helpProvider1.SetShowHelp((Control) this.IntlProfileHeader, (bool) componentResourceManager.GetObject("IntlProfileHeader.ShowHelp"));
      this.intlFieldPreferences.BackColor = Color.White;
      this.intlFieldPreferences.CurrentPrefTypeIndex = -1;
      componentResourceManager.ApplyResources((object) this.intlFieldPreferences, "intlFieldPreferences");
      this.intlFieldPreferences.IsLoading = false;
      this.intlFieldPreferences.Name = "intlFieldPreferences";
      this.intlFieldPreferences.PreferenceObject = (ShipDefl) null;
      this.intlFieldPreferences.PrevPrefTypeIndex = -1;
      this.helpProvider1.SetShowHelp((Control) this.intlFieldPreferences, (bool) componentResourceManager.GetObject("intlFieldPreferences.ShowHelp"));
      this.intlOtherExpressPreferences.BackColor = Color.White;
      this.intlOtherExpressPreferences.Carrier = Shipment.CarrierType.NoCarrier;
      componentResourceManager.ApplyResources((object) this.intlOtherExpressPreferences, "intlOtherExpressPreferences");
      this.intlOtherExpressPreferences.Name = "intlOtherExpressPreferences";
      this.intlOtherExpressPreferences.PreferenceObject = (ShipDefl) null;
      this.helpProvider1.SetShowHelp((Control) this.intlOtherExpressPreferences, (bool) componentResourceManager.GetObject("intlOtherExpressPreferences.ShowHelp"));
      this.intlShipAlertPreferences.BackColor = Color.White;
      this.intlShipAlertPreferences.CurrentPrefTypeIndex = -1;
      componentResourceManager.ApplyResources((object) this.intlShipAlertPreferences, "intlShipAlertPreferences");
      this.intlShipAlertPreferences.IsLoading = false;
      this.intlShipAlertPreferences.Name = "intlShipAlertPreferences";
      this.intlShipAlertPreferences.PreferenceObject = (ShipDefl) null;
      this.intlShipAlertPreferences.PrevPrefTypeIndex = -1;
      this.helpProvider1.SetShowHelp((Control) this.intlShipAlertPreferences, (bool) componentResourceManager.GetObject("intlShipAlertPreferences.ShowHelp"));
      this.intlOtherGroundPreferences.BackColor = Color.White;
      this.intlOtherGroundPreferences.Carrier = Shipment.CarrierType.NoCarrier;
      componentResourceManager.ApplyResources((object) this.intlOtherGroundPreferences, "intlOtherGroundPreferences");
      this.intlOtherGroundPreferences.Name = "intlOtherGroundPreferences";
      this.intlOtherGroundPreferences.PreferenceObject = (ShipDefl) null;
      this.helpProvider1.SetShowHelp((Control) this.intlOtherGroundPreferences, (bool) componentResourceManager.GetObject("intlOtherGroundPreferences.ShowHelp"));
      this.customsDocuments1.BackColor = Color.White;
      componentResourceManager.ApplyResources((object) this.customsDocuments1, "customsDocuments1");
      this.customsDocuments1.Name = "customsDocuments1";
      this.customsDocuments1.PreferenceObject = (ShipDefl) null;
      this.helpProvider1.SetShowHelp((Control) this.customsDocuments1, (bool) componentResourceManager.GetObject("customsDocuments1.ShowHelp"));
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
      this.Name = nameof (IntlPrefDlg);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Load += new EventHandler(this.IntlPrefDlg_Load);
      this.tabControlIntlPrefs.ResumeLayout(false);
      this.tabPageIntlFieldPrefs.ResumeLayout(false);
      this.tabPageIntlExpressOtherPrefs.ResumeLayout(false);
      this.colorGroupBox2.ResumeLayout(false);
      this.gbxIntlWaybillFormat.ResumeLayout(false);
      this.tabPageIntlShipAlertPrefs.ResumeLayout(false);
      this.tabPageIntlGroundPrefs.ResumeLayout(false);
      this.tabPageCustomsDocuments.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
