// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.OtherPreferences
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.LabelDataComponents;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class OtherPreferences : UserControlHelpEx
  {
    protected ShipDefl _shipDefl;
    protected Shipment.CarrierType _carrier;
    private bool _combosLoaded;
    private static DataTable _dt;
    private Account _account;
    private IContainer components;
    protected ColorGroupBox gbxHandlingCharge;
    protected ColorGroupBox gbxLabelFormat;
    protected ColorGroupBox gbxMiscellaneous;
    protected ColorGroupBox gbxReferenceLabel;
    protected Panel pnlInsertInReferences;
    protected TableLayoutPanel tableLayoutPanelOtherPrefs;
    protected ComboBoxEx cboInsertInReferences;
    protected CheckBox chk3DayFreight;
    protected CheckBox chk2DayFreight;
    protected CheckBox chk1DayFreight;
    protected CheckBox chkIncludeAdditionalHandlingCharge;
    protected Panel panelHandlingCharge;
    protected ComboBoxEx cboVariableChargeInd;
    protected Label lblPercentOf;
    protected FdxMaskedEdit edtVariable;
    protected FdxMaskedEdit edtFixed;
    protected Label txtCurrency;
    protected Label txtCombine;
    protected CheckBox chkVariable;
    protected CheckBox chkFixed;
    protected Label lblLabelFormat;
    protected ComboBoxEx cboLabelFormat;
    protected RadioButton rdoCustomizeDocTab;
    protected RadioButton rdoDefaultDocTab;
    protected Button btnDocTabConfig;
    protected CheckBox chkTrackingNbrOverwrite;
    public CheckBox chkRequireReferences;
    private FocusExtender focusExtender1;
    protected CheckBox chkOverrideGroundToHome;
    protected CheckBox chkFOFreight;
    protected RadioButton rdoBarcodeDocTab;
    protected ComboBoxEx cboBarcodeDocTab;
    protected ColorGroupBox gbxRequireRef;
    protected CheckBox chkAddlRef3;
    protected CheckBox chkAddlRef2;
    protected CheckBox chkAddlRef1;
    protected ColorGroupBox gbxInsertInRef;
    protected CheckBox chkRequireDims;
    private FlowLayoutPanel flpMiscellaneous;
    protected CheckBox chkRequireRates;
    protected CheckBox chkGroundHomeToggle;

    internal Account CurrentAccount
    {
      get => this._account ?? GuiData.CurrentAccount;
      set => this._account = value;
    }

    public OtherPreferences() => this.InitializeComponent();

    protected void InitOtherPrefs(ShipDefl shipDefl, Shipment.CarrierType carrier)
    {
      this._shipDefl = shipDefl;
      this._carrier = carrier;
    }

    public ShipDefl PreferenceObject
    {
      get => this._shipDefl;
      set => this._shipDefl = value;
    }

    public Shipment.CarrierType Carrier
    {
      get => this._carrier;
      set => this._carrier = value;
    }

    private void btnDocTabConfig_Click(object sender, EventArgs e)
    {
      if (this.rdoDefaultDocTab.Checked)
      {
        int num1 = (int) new DocTabCommentsDlg(this._shipDefl, this._carrier).ShowDialog();
      }
      else
      {
        int num2 = (int) new DocTabConfigDlg(this._shipDefl, this._carrier).ShowDialog();
      }
    }

    public virtual void ObjectToScreen()
    {
      this.PopulateCombos();
      if (this._shipDefl == null)
        return;
      try
      {
        if (this._carrier == Shipment.CarrierType.Ground)
        {
          this.chkRequireReferences.Checked = this._shipDefl.RequireGroundReference;
          this.chkAddlRef1.Checked = this._shipDefl.GroundAdditionalRef1;
          this.chkAddlRef2.Checked = this._shipDefl.GroundAdditionalRef2;
          this.chkAddlRef3.Checked = this._shipDefl.GroundAdditionalRef3;
          this.chkRequireDims.Checked = this._shipDefl.GroundRequireDimensions;
          this.chkRequireRates.Checked = this._shipDefl.GroundRequireRate;
          this.cboInsertInReferences.SelectedValueQ = (object) Enum.Format(typeof (ShipDefl.InsertIntoReference), (object) this._shipDefl.GroundInsertInRefValue, "d");
          this.chkIncludeAdditionalHandlingCharge.Checked = this._shipDefl.GroundHandlingCharge.VariableChgInd || this._shipDefl.GroundHandlingCharge.FixedChgInd;
          this.chkFixed.Checked = this._shipDefl.GroundHandlingCharge.FixedChgInd;
          this.edtFixed.Text = this._shipDefl.GroundHandlingCharge.FixedCharge.ToString();
          this.chkVariable.Checked = this._shipDefl.GroundHandlingCharge.VariableChgInd;
          this.edtVariable.Text = this._shipDefl.GroundHandlingCharge.VariablePercentage.ToString();
          this.cboVariableChargeInd.SelectedValueQ = (object) Enum.Format(typeof (HandlingCharge.HANDLINGCOMPTYPE), (object) this._shipDefl.GroundHandlingCharge.ComputationInd, "d");
          switch (this._shipDefl.GroundDocTabOption)
          {
            case ShipDefl.DocTabOption.CustomDocTab:
              this.rdoCustomizeDocTab.Checked = true;
              this.cboBarcodeDocTab.SelectedIndex = -1;
              break;
            case ShipDefl.DocTabOption.BarcodeDocTab:
              this.rdoBarcodeDocTab.Checked = true;
              this.cboBarcodeDocTab.SelectedValue = (object) this._shipDefl.GroundBarcodeDocTabVal;
              break;
            default:
              this.rdoDefaultDocTab.Checked = true;
              this.cboBarcodeDocTab.SelectedIndex = -1;
              break;
          }
          this.cboLabelFormat.SelectedValue = (object) Enum.Format(typeof (Shipment.LabelFormat), (object) this._shipDefl.GroundLabelFormat, "d");
          this.chkOverrideGroundToHome.Checked = this._shipDefl.OverrideGroundToHome;
          this.chkGroundHomeToggle.Checked = this._shipDefl.GroundHomeToggle;
        }
        else if (this._carrier == Shipment.CarrierType.SmartPost)
        {
          this.chkRequireReferences.Checked = this._shipDefl.RequireSmartPostReferences;
          this.chkAddlRef1.Checked = this._shipDefl.SmartPostAdditionalRef1;
          this.chkAddlRef2.Checked = this._shipDefl.SmartPostAdditionalRef2;
          this.chkAddlRef3.Checked = this._shipDefl.SmartPostAdditionalRef3;
          this.chkRequireDims.Checked = this._shipDefl.SmartPostRequireDimensions;
          this.chkRequireRates.Checked = this._shipDefl.SmartPostRequireRate;
          this.cboInsertInReferences.SelectedValueQ = (object) Enum.Format(typeof (ShipDefl.InsertIntoReference), (object) this._shipDefl.InsertInSmartPostReferences, "d");
          this.chkIncludeAdditionalHandlingCharge.Checked = this._shipDefl.SmartPostHandlingCharge.VariableChgInd || this._shipDefl.SmartPostHandlingCharge.FixedChgInd;
          this.chkFixed.Checked = this._shipDefl.SmartPostHandlingCharge.FixedChgInd;
          this.edtFixed.Text = this._shipDefl.SmartPostHandlingCharge.FixedCharge.ToString();
          this.chkVariable.Checked = this._shipDefl.SmartPostHandlingCharge.VariableChgInd;
          this.edtVariable.Text = this._shipDefl.SmartPostHandlingCharge.VariablePercentage.ToString();
          this.cboVariableChargeInd.SelectedValueQ = (object) Enum.Format(typeof (HandlingCharge.HANDLINGCOMPTYPE), (object) this._shipDefl.SmartPostHandlingCharge.ComputationInd, "d");
          switch (this._shipDefl.SmartPostDocTabOption)
          {
            case ShipDefl.DocTabOption.CustomDocTab:
              this.rdoCustomizeDocTab.Checked = true;
              this.cboBarcodeDocTab.SelectedIndex = -1;
              break;
            case ShipDefl.DocTabOption.BarcodeDocTab:
              this.rdoBarcodeDocTab.Checked = true;
              this.cboBarcodeDocTab.SelectedValue = (object) this._shipDefl.SmartPostBarcodeDocTabVal;
              break;
            default:
              this.rdoDefaultDocTab.Checked = true;
              this.cboBarcodeDocTab.SelectedIndex = -1;
              break;
          }
          this.cboLabelFormat.SelectedValue = (object) Enum.Format(typeof (Shipment.LabelFormat), (object) this._shipDefl.SmartPostLabelFormat, "d");
        }
        else
        {
          this.chkRequireReferences.Checked = this._shipDefl.RequireReference;
          this.chkAddlRef1.Checked = this._shipDefl.ExpressAdditionalRef1;
          this.chkAddlRef2.Checked = this._shipDefl.ExpressAdditionalRef2;
          this.chkAddlRef3.Checked = this._shipDefl.ExpressAdditionalRef3;
          this.chkRequireDims.Checked = this._shipDefl.ExpressRequireDimensions;
          this.chkRequireRates.Checked = this._shipDefl.ExpressRequireRate;
          this.cboInsertInReferences.SelectedValueQ = (object) Enum.Format(typeof (ShipDefl.InsertIntoReference), (object) this._shipDefl.ExpressInsertInRefValue, "d");
          this.chkFOFreight.Checked = this._shipDefl.ReferenceLabelFOF;
          this.chk1DayFreight.Checked = this._shipDefl.ReferenceLabelOvernight;
          this.chk2DayFreight.Checked = this._shipDefl.ReferenceLabel2Day;
          this.chk3DayFreight.Checked = this._shipDefl.ReferenceLabelES;
          this.chkIncludeAdditionalHandlingCharge.Checked = this._shipDefl.ExpressHandlingCharge.VariableChgInd || this._shipDefl.ExpressHandlingCharge.FixedChgInd;
          this.chkFixed.Checked = this._shipDefl.ExpressHandlingCharge.FixedChgInd;
          this.edtFixed.Text = this._shipDefl.ExpressHandlingCharge.FixedCharge.ToString();
          this.chkVariable.Checked = this._shipDefl.ExpressHandlingCharge.VariableChgInd;
          this.edtVariable.Text = this._shipDefl.ExpressHandlingCharge.VariablePercentage.ToString();
          this.cboVariableChargeInd.SelectedValueQ = (object) Enum.Format(typeof (HandlingCharge.HANDLINGCOMPTYPE), (object) this._shipDefl.ExpressHandlingCharge.ComputationInd, "d");
          switch (this._shipDefl.ExpressDocTabOption)
          {
            case ShipDefl.DocTabOption.CustomDocTab:
              this.rdoCustomizeDocTab.Checked = true;
              this.cboBarcodeDocTab.SelectedIndex = -1;
              break;
            case ShipDefl.DocTabOption.BarcodeDocTab:
              this.rdoBarcodeDocTab.Checked = true;
              this.cboBarcodeDocTab.SelectedValue = (object) this._shipDefl.ExpressBarcodeDocTabVal;
              break;
            default:
              this.rdoDefaultDocTab.Checked = true;
              this.cboBarcodeDocTab.SelectedIndex = -1;
              break;
          }
          this.cboLabelFormat.SelectedValue = (object) Enum.Format(typeof (Shipment.LabelFormat), (object) this._shipDefl.ExpressLabelFormat, "d");
        }
        this.SetupHandlingChargeSection();
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Exception: " + ex.Message);
      }
    }

    private void SetupHandlingChargeSection()
    {
      if (this.chkIncludeAdditionalHandlingCharge.Checked)
      {
        this.panelHandlingCharge.Enabled = true;
        this.edtFixed.Enabled = this.chkFixed.Checked;
        this.edtVariable.Enabled = this.chkVariable.Checked;
        this.cboVariableChargeInd.Enabled = this.chkVariable.Checked;
        this.txtCombine.Visible = this.chkFixed.Checked && this.chkVariable.Checked;
      }
      else
      {
        this.panelHandlingCharge.Enabled = false;
        this.chkFixed.Checked = false;
        this.chkVariable.Checked = false;
        this.edtFixed.Text = "0.00";
        this.txtCombine.Visible = false;
      }
    }

    public virtual void ScreenToObject()
    {
      try
      {
        HandlingCharge.HANDLINGCOMPTYPE handlingcomptype = this.cboVariableChargeInd.SelectedValueQ == null ? HandlingCharge.HANDLINGCOMPTYPE.FLAT_AMOUNT : (HandlingCharge.HANDLINGCOMPTYPE) Enum.Parse(typeof (HandlingCharge.HANDLINGCOMPTYPE), this.cboVariableChargeInd.SelectedValueQ as string);
        Shipment.LabelFormat labelFormat = this.cboLabelFormat.SelectedValueQ == null ? Shipment.LabelFormat.PlainPaper : (Shipment.LabelFormat) Enum.Parse(typeof (Shipment.LabelFormat), this.cboLabelFormat.SelectedValueQ as string);
        if (this._carrier == Shipment.CarrierType.Ground)
        {
          this._shipDefl.RequireGroundReference = this.chkRequireReferences.Checked;
          this._shipDefl.GroundAdditionalRef1 = this.chkAddlRef1.Checked;
          this._shipDefl.GroundAdditionalRef2 = this.chkAddlRef2.Checked;
          this._shipDefl.GroundAdditionalRef3 = this.chkAddlRef3.Checked;
          this._shipDefl.GroundRequireDimensions = this.chkRequireDims.Checked;
          this._shipDefl.GroundRequireRate = this.chkRequireRates.Checked;
          this._shipDefl.GroundInsertInRefValue = (ShipDefl.InsertIntoReference) Enum.Parse(typeof (ShipDefl.InsertIntoReference), this.cboInsertInReferences.SelectedValueQ as string);
          this._shipDefl.GroundHandlingCharge.ComputationInd = handlingcomptype;
          this._shipDefl.GroundHandlingCharge.VariableChgInd = this.chkVariable.Checked;
          this._shipDefl.GroundHandlingCharge.FixedChgInd = this.chkFixed.Checked;
          this._shipDefl.GroundHandlingCharge.FixedCharge = Convert.ToDouble(this.edtFixed.Raw);
          this._shipDefl.GroundHandlingCharge.VariablePercentage = !(this.edtVariable.Raw != "") ? Convert.ToDouble(0) : Convert.ToDouble(this.edtVariable.Raw);
          this._shipDefl.GroundLabelFormat = labelFormat;
          this._shipDefl.GroundDocTabOption = !this.rdoCustomizeDocTab.Checked ? (!this.rdoBarcodeDocTab.Checked ? ShipDefl.DocTabOption.DefaultDocTab : ShipDefl.DocTabOption.BarcodeDocTab) : ShipDefl.DocTabOption.CustomDocTab;
          this._shipDefl.GroundBarcodeDocTabVal = this.cboBarcodeDocTab.SelectedValue as string;
          this._shipDefl.PrintGroundTotalDocTab = this._shipDefl.PrintGroundTotalDocTab;
          this._shipDefl.OverrideGroundToHome = this.chkOverrideGroundToHome.Checked;
          this._shipDefl.GroundHomeToggle = this.chkGroundHomeToggle.Checked;
        }
        else if (this._carrier == Shipment.CarrierType.SmartPost)
        {
          this._shipDefl.RequireSmartPostReferences = this.chkRequireReferences.Checked;
          this._shipDefl.SmartPostAdditionalRef1 = this.chkAddlRef1.Checked;
          this._shipDefl.SmartPostAdditionalRef2 = this.chkAddlRef2.Checked;
          this._shipDefl.SmartPostAdditionalRef3 = this.chkAddlRef3.Checked;
          this._shipDefl.SmartPostRequireDimensions = this.chkRequireDims.Checked;
          this._shipDefl.SmartPostRequireRate = this.chkRequireRates.Checked;
          this._shipDefl.InsertInSmartPostReferences = (ShipDefl.InsertIntoReference) Enum.Parse(typeof (ShipDefl.InsertIntoReference), this.cboInsertInReferences.SelectedValueQ as string);
          if (this._shipDefl.SmartPostHandlingCharge == null)
            this._shipDefl.SmartPostHandlingCharge = new HandlingCharge();
          this._shipDefl.SmartPostHandlingCharge.ComputationInd = handlingcomptype;
          this._shipDefl.SmartPostHandlingCharge.VariableChgInd = this.chkVariable.Checked;
          this._shipDefl.SmartPostHandlingCharge.FixedChgInd = this.chkFixed.Checked;
          this._shipDefl.SmartPostHandlingCharge.FixedCharge = Convert.ToDouble(this.edtFixed.Raw);
          this._shipDefl.SmartPostHandlingCharge.VariablePercentage = !(this.edtVariable.Raw != "") ? Convert.ToDouble(0) : Convert.ToDouble(this.edtVariable.Raw);
          this._shipDefl.SmartPostLabelFormat = labelFormat;
          this._shipDefl.HasSmartPostCustDocTab = this.rdoCustomizeDocTab.Checked;
          this._shipDefl.SmartPostDocTabOption = !this.rdoCustomizeDocTab.Checked ? (!this.rdoBarcodeDocTab.Checked ? ShipDefl.DocTabOption.DefaultDocTab : ShipDefl.DocTabOption.BarcodeDocTab) : ShipDefl.DocTabOption.CustomDocTab;
          this._shipDefl.SmartPostBarcodeDocTabVal = this.cboBarcodeDocTab.SelectedValue as string;
          this._shipDefl.PrintSmartPostTotalDocTab = this._shipDefl.PrintSmartPostTotalDocTab;
        }
        else
        {
          this._shipDefl.RequireReference = this.chkRequireReferences.Checked;
          this._shipDefl.ExpressAdditionalRef1 = this.chkAddlRef1.Checked;
          this._shipDefl.ExpressAdditionalRef2 = this.chkAddlRef2.Checked;
          this._shipDefl.ExpressAdditionalRef3 = this.chkAddlRef3.Checked;
          this._shipDefl.ExpressRequireDimensions = this.chkRequireDims.Checked;
          this._shipDefl.ExpressRequireRate = this.chkRequireRates.Checked;
          this._shipDefl.ReferenceLabelFOF = this.chkFOFreight.Checked;
          this._shipDefl.ReferenceLabelOvernight = this.chk1DayFreight.Checked;
          this._shipDefl.ReferenceLabel2Day = this.chk2DayFreight.Checked;
          this._shipDefl.ReferenceLabelES = this.chk3DayFreight.Checked;
          this._shipDefl.ExpressInsertInRefValue = (ShipDefl.InsertIntoReference) Enum.Parse(typeof (ShipDefl.InsertIntoReference), this.cboInsertInReferences.SelectedValueQ as string);
          this._shipDefl.ExpressHandlingCharge.ComputationInd = handlingcomptype;
          this._shipDefl.ExpressHandlingCharge.VariableChgInd = this.chkVariable.Checked;
          this._shipDefl.ExpressHandlingCharge.FixedChgInd = this.chkFixed.Checked;
          this._shipDefl.ExpressHandlingCharge.FixedCharge = Convert.ToDouble(this.edtFixed.Raw);
          this._shipDefl.ExpressHandlingCharge.VariablePercentage = !(this.edtVariable.Raw != "") ? Convert.ToDouble(0) : Convert.ToDouble(this.edtVariable.Raw);
          this._shipDefl.ExpressLabelFormat = labelFormat;
          this._shipDefl.HasCustExpressDocTab = this.rdoCustomizeDocTab.Checked;
          this._shipDefl.ExpressDocTabOption = !this.rdoCustomizeDocTab.Checked ? (!this.rdoBarcodeDocTab.Checked ? ShipDefl.DocTabOption.DefaultDocTab : ShipDefl.DocTabOption.BarcodeDocTab) : ShipDefl.DocTabOption.CustomDocTab;
          this._shipDefl.ExpressBarcodeDocTabVal = this.cboBarcodeDocTab.SelectedValue as string;
          this._shipDefl.PrintExpressTotalDocTab = this._shipDefl.PrintExpressTotalDocTab;
        }
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Exception: " + ex.Message);
      }
    }

    private void OtherPreferences_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.PopulateCombos();
      if (!string.IsNullOrEmpty(this.CurrentAccount.RatingCurrency))
        this.txtCurrency.Text = this.CurrentAccount.RatingCurrency;
      if (this.CurrentAccount != null && this.CurrentAccount.Address.CountryCode == "CA")
        this.cboVariableChargeInd.DropDownWidth = 320;
      this.cboBarcodeDocTab.Enabled = this.rdoBarcodeDocTab.Checked;
      this.EnableDisableRadioButtons();
    }

    public void PopulateInsertIntoRefsCombo(ComboBoxEx cb)
    {
      DataTable dataTable = Utility.GetDataTable(Utility.ListTypes.InsertIntoRefs);
      if (this._carrier == Shipment.CarrierType.Ground)
      {
        DataRow row1 = dataTable.Select("Code = 1")[0];
        DataRow row2 = dataTable.NewRow();
        row2[0] = (object) row1[0].ToString();
        row2[1] = (object) row1[1].ToString();
        row2[2] = (object) row1[2].ToString();
        dataTable.Rows.Remove(row1);
        dataTable.Rows.Add(row2);
      }
      else if (this._carrier == Shipment.CarrierType.SmartPost || this._carrier == Shipment.CarrierType.Express)
        dataTable.DefaultView.RowFilter = "Code <> 4";
      cb.DataSourceQ = (object) dataTable;
      cb.ValueMemberQ = "Code";
      cb.DisplayMember = "Description";
    }

    public void PopulateVariableHandlingCombo(ComboBoxEx cb)
    {
      if (this._shipDefl != null)
      {
        cb.DataSourceQ = (object) Utility.GetVariableChargesList(this._shipDefl.GetType().ToString().Contains("IShipDefl"), this.CurrentAccount);
        cb.ValueMemberQ = "Code";
        cb.DisplayMember = "Description";
      }
      else
      {
        cb.DataSourceQ = (object) Utility.GetVariableChargesList(false, this.CurrentAccount);
        cb.ValueMemberQ = "Code";
        cb.DisplayMember = "Description";
      }
    }

    public void PopulateLabelFormatCombo(ComboBoxEx cb)
    {
      DataTable labelFormatTable = this.GetLabelFormatTable();
      labelFormatTable.DefaultView.Sort = "Description ASC";
      cb.DataSourceQ = (object) labelFormatTable;
      cb.ValueMemberQ = "Code";
      cb.DisplayMemberQ = "Description";
    }

    protected virtual DataTable GetLabelFormatTable()
    {
      return Utility.GetDataTable(Utility.ListTypes.ExtendedLabelFormat);
    }

    private void chkVariable_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox checkBox = sender as CheckBox;
      bool flag = checkBox.Checked;
      this.edtVariable.Enabled = flag;
      this.cboVariableChargeInd.Enabled = flag;
      if (flag)
        this.edtVariable.Text = "0";
      else
        this.edtVariable.Text = string.Empty;
      if (!this.cboVariableChargeInd.Enabled)
        this.cboVariableChargeInd.SelectedIndex = -1;
      if (checkBox.Checked && this.chkFixed.Checked)
        this.txtCombine.Visible = true;
      else
        this.txtCombine.Visible = false;
    }

    private void rdoCustomizeDocTab_CheckedChanged(object sender, EventArgs e)
    {
      if (this._carrier == Shipment.CarrierType.Ground)
      {
        if (this.rdoCustomizeDocTab.Checked)
          this._shipDefl.GroundDocTabOption = ShipDefl.DocTabOption.CustomDocTab;
        else if (this.rdoBarcodeDocTab.Checked)
          this._shipDefl.GroundDocTabOption = ShipDefl.DocTabOption.BarcodeDocTab;
        else
          this._shipDefl.GroundDocTabOption = ShipDefl.DocTabOption.DefaultDocTab;
      }
      else if (this._carrier == Shipment.CarrierType.SmartPost)
      {
        if (this.rdoCustomizeDocTab.Checked)
          this._shipDefl.SmartPostDocTabOption = ShipDefl.DocTabOption.CustomDocTab;
        else if (this.rdoBarcodeDocTab.Checked)
          this._shipDefl.SmartPostDocTabOption = ShipDefl.DocTabOption.BarcodeDocTab;
        else
          this._shipDefl.SmartPostDocTabOption = ShipDefl.DocTabOption.DefaultDocTab;
      }
      else if (this.rdoCustomizeDocTab.Checked)
        this._shipDefl.ExpressDocTabOption = ShipDefl.DocTabOption.CustomDocTab;
      else if (this.rdoBarcodeDocTab.Checked)
        this._shipDefl.ExpressDocTabOption = ShipDefl.DocTabOption.BarcodeDocTab;
      else
        this._shipDefl.ExpressDocTabOption = ShipDefl.DocTabOption.DefaultDocTab;
    }

    private void PopulateCombos()
    {
      if (!this._combosLoaded)
      {
        this.PopulateInsertIntoRefsCombo(this.cboInsertInReferences);
        this.PopulateVariableHandlingCombo(this.cboVariableChargeInd);
        this.PopulateLabelFormatCombo(this.cboLabelFormat);
        this.FillDocTabCombo((Control) this.cboBarcodeDocTab);
      }
      this._combosLoaded = true;
    }

    private void FillDocTabCombo(Control c)
    {
      if (OtherPreferences._dt == null)
      {
        OtherPreferences._dt = new FieldChooserDataComponent().getConfigurableDocTabFields();
        foreach (DataRow row in OtherPreferences._dt.Select("CTS_Tag_Id = '2800' OR CTS_Tag_Id = '2801' OR CTS_Tag_Id = '2803' "))
          OtherPreferences._dt.Rows.Remove(row);
        OtherPreferences._dt.Rows.InsertAt(OtherPreferences._dt.NewRow(), 0);
      }
      ComboBox comboBox = c as ComboBox;
      comboBox.BindingContext = new BindingContext();
      comboBox.DataSource = (object) OtherPreferences._dt;
      comboBox.DisplayMember = "CTS_Tag_Desc";
      comboBox.ValueMember = "CTS_Tag_Id";
      comboBox.SelectedIndex = -1;
    }

    private void chkFixed_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox checkBox = sender as CheckBox;
      this.edtFixed.Enabled = checkBox.Checked;
      this.edtFixed.Text = "0.00";
      if (checkBox.Checked && this.chkVariable.Checked)
        this.txtCombine.Visible = true;
      else
        this.txtCombine.Visible = false;
    }

    private void chkIncludeAdditionalHandlingCharge_CheckedChanged(object sender, EventArgs e)
    {
      this.SetupHandlingChargeSection();
    }

    private void cboLabelFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.EnableDisableRadioButtons();
      this.EnableDisableDocTabOptions();
    }

    private void EnableDisableRadioButtons()
    {
      if (this.cboLabelFormat.SelectedValue != null && ((Shipment.LabelFormat) Enum.Parse(typeof (Shipment.LabelFormat), this.cboLabelFormat.SelectedValueQ as string) == Shipment.LabelFormat.DocTab || (Shipment.LabelFormat) Enum.Parse(typeof (Shipment.LabelFormat), this.cboLabelFormat.SelectedValueQ as string) == Shipment.LabelFormat.WithDocTab434))
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

    private void rdoCustomizeDocTab_CheckedChanged_1(object sender, EventArgs e)
    {
      this.EnableDisableDocTabOptions();
    }

    private void EnableDisableDocTabOptions()
    {
      if (this._carrier == Shipment.CarrierType.Ground || this._carrier == Shipment.CarrierType.SmartPost)
        this.btnDocTabConfig.Enabled = this.rdoCustomizeDocTab.Enabled && this.rdoCustomizeDocTab.Checked;
      else
        this.btnDocTabConfig.Enabled = this.rdoCustomizeDocTab.Enabled && !this.rdoBarcodeDocTab.Checked;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OtherPreferences));
      this.tableLayoutPanelOtherPrefs = new TableLayoutPanel();
      this.gbxReferenceLabel = new ColorGroupBox();
      this.chkFOFreight = new CheckBox();
      this.chk3DayFreight = new CheckBox();
      this.chk2DayFreight = new CheckBox();
      this.chk1DayFreight = new CheckBox();
      this.gbxHandlingCharge = new ColorGroupBox();
      this.panelHandlingCharge = new Panel();
      this.txtCombine = new Label();
      this.txtCurrency = new Label();
      this.cboVariableChargeInd = new ComboBoxEx();
      this.lblPercentOf = new Label();
      this.edtVariable = new FdxMaskedEdit();
      this.edtFixed = new FdxMaskedEdit();
      this.chkVariable = new CheckBox();
      this.chkFixed = new CheckBox();
      this.chkIncludeAdditionalHandlingCharge = new CheckBox();
      this.gbxLabelFormat = new ColorGroupBox();
      this.cboBarcodeDocTab = new ComboBoxEx();
      this.rdoBarcodeDocTab = new RadioButton();
      this.btnDocTabConfig = new Button();
      this.rdoCustomizeDocTab = new RadioButton();
      this.rdoDefaultDocTab = new RadioButton();
      this.cboLabelFormat = new ComboBoxEx();
      this.lblLabelFormat = new Label();
      this.gbxMiscellaneous = new ColorGroupBox();
      this.flpMiscellaneous = new FlowLayoutPanel();
      this.chkRequireDims = new CheckBox();
      this.chkRequireRates = new CheckBox();
      this.chkTrackingNbrOverwrite = new CheckBox();
      this.chkOverrideGroundToHome = new CheckBox();
      this.chkGroundHomeToggle = new CheckBox();
      this.pnlInsertInReferences = new Panel();
      this.gbxRequireRef = new ColorGroupBox();
      this.chkAddlRef3 = new CheckBox();
      this.chkAddlRef2 = new CheckBox();
      this.chkAddlRef1 = new CheckBox();
      this.chkRequireReferences = new CheckBox();
      this.gbxInsertInRef = new ColorGroupBox();
      this.cboInsertInReferences = new ComboBoxEx();
      this.focusExtender1 = new FocusExtender();
      this.tableLayoutPanelOtherPrefs.SuspendLayout();
      this.gbxReferenceLabel.SuspendLayout();
      this.gbxHandlingCharge.SuspendLayout();
      this.panelHandlingCharge.SuspendLayout();
      this.gbxLabelFormat.SuspendLayout();
      this.gbxMiscellaneous.SuspendLayout();
      this.flpMiscellaneous.SuspendLayout();
      this.pnlInsertInReferences.SuspendLayout();
      this.gbxRequireRef.SuspendLayout();
      this.gbxInsertInRef.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) this.tableLayoutPanelOtherPrefs, "tableLayoutPanelOtherPrefs");
      this.tableLayoutPanelOtherPrefs.Controls.Add((Control) this.gbxReferenceLabel, 2, 0);
      this.tableLayoutPanelOtherPrefs.Controls.Add((Control) this.gbxHandlingCharge, 0, 1);
      this.tableLayoutPanelOtherPrefs.Controls.Add((Control) this.gbxLabelFormat, 0, 2);
      this.tableLayoutPanelOtherPrefs.Controls.Add((Control) this.gbxMiscellaneous, 0, 0);
      this.tableLayoutPanelOtherPrefs.Controls.Add((Control) this.pnlInsertInReferences, 1, 0);
      this.tableLayoutPanelOtherPrefs.Name = "tableLayoutPanelOtherPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tableLayoutPanelOtherPrefs, (bool) componentResourceManager.GetObject("tableLayoutPanelOtherPrefs.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.gbxReferenceLabel, "gbxReferenceLabel");
      this.gbxReferenceLabel.BorderThickness = 1f;
      this.gbxReferenceLabel.Controls.Add((Control) this.chkFOFreight);
      this.gbxReferenceLabel.Controls.Add((Control) this.chk3DayFreight);
      this.gbxReferenceLabel.Controls.Add((Control) this.chk2DayFreight);
      this.gbxReferenceLabel.Controls.Add((Control) this.chk1DayFreight);
      this.gbxReferenceLabel.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxReferenceLabel.Name = "gbxReferenceLabel";
      this.gbxReferenceLabel.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxReferenceLabel, (bool) componentResourceManager.GetObject("gbxReferenceLabel.ShowHelp"));
      this.gbxReferenceLabel.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkFOFreight, componentResourceManager.GetString("chkFOFreight.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkFOFreight, (HelpNavigator) componentResourceManager.GetObject("chkFOFreight.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkFOFreight, "chkFOFreight");
      this.chkFOFreight.Name = "chkFOFreight";
      this.helpProvider1.SetShowHelp((Control) this.chkFOFreight, (bool) componentResourceManager.GetObject("chkFOFreight.ShowHelp"));
      this.chkFOFreight.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chk3DayFreight, componentResourceManager.GetString("chk3DayFreight.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chk3DayFreight, (HelpNavigator) componentResourceManager.GetObject("chk3DayFreight.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chk3DayFreight, "chk3DayFreight");
      this.chk3DayFreight.Name = "chk3DayFreight";
      this.helpProvider1.SetShowHelp((Control) this.chk3DayFreight, (bool) componentResourceManager.GetObject("chk3DayFreight.ShowHelp"));
      this.chk3DayFreight.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chk2DayFreight, componentResourceManager.GetString("chk2DayFreight.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chk2DayFreight, (HelpNavigator) componentResourceManager.GetObject("chk2DayFreight.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chk2DayFreight, "chk2DayFreight");
      this.chk2DayFreight.Name = "chk2DayFreight";
      this.helpProvider1.SetShowHelp((Control) this.chk2DayFreight, (bool) componentResourceManager.GetObject("chk2DayFreight.ShowHelp"));
      this.chk2DayFreight.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chk1DayFreight, componentResourceManager.GetString("chk1DayFreight.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chk1DayFreight, (HelpNavigator) componentResourceManager.GetObject("chk1DayFreight.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chk1DayFreight, "chk1DayFreight");
      this.chk1DayFreight.Name = "chk1DayFreight";
      this.helpProvider1.SetShowHelp((Control) this.chk1DayFreight, (bool) componentResourceManager.GetObject("chk1DayFreight.ShowHelp"));
      this.chk1DayFreight.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.gbxHandlingCharge, "gbxHandlingCharge");
      this.gbxHandlingCharge.BorderThickness = 1f;
      this.tableLayoutPanelOtherPrefs.SetColumnSpan((Control) this.gbxHandlingCharge, 3);
      this.gbxHandlingCharge.Controls.Add((Control) this.panelHandlingCharge);
      this.gbxHandlingCharge.Controls.Add((Control) this.chkIncludeAdditionalHandlingCharge);
      this.gbxHandlingCharge.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxHandlingCharge.Name = "gbxHandlingCharge";
      this.gbxHandlingCharge.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxHandlingCharge, (bool) componentResourceManager.GetObject("gbxHandlingCharge.ShowHelp"));
      this.gbxHandlingCharge.TabStop = false;
      componentResourceManager.ApplyResources((object) this.panelHandlingCharge, "panelHandlingCharge");
      this.panelHandlingCharge.Controls.Add((Control) this.txtCombine);
      this.panelHandlingCharge.Controls.Add((Control) this.txtCurrency);
      this.panelHandlingCharge.Controls.Add((Control) this.cboVariableChargeInd);
      this.panelHandlingCharge.Controls.Add((Control) this.lblPercentOf);
      this.panelHandlingCharge.Controls.Add((Control) this.edtVariable);
      this.panelHandlingCharge.Controls.Add((Control) this.edtFixed);
      this.panelHandlingCharge.Controls.Add((Control) this.chkVariable);
      this.panelHandlingCharge.Controls.Add((Control) this.chkFixed);
      this.panelHandlingCharge.Name = "panelHandlingCharge";
      this.helpProvider1.SetShowHelp((Control) this.panelHandlingCharge, (bool) componentResourceManager.GetObject("panelHandlingCharge.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.txtCombine, "txtCombine");
      this.txtCombine.Name = "txtCombine";
      this.helpProvider1.SetShowHelp((Control) this.txtCombine, (bool) componentResourceManager.GetObject("txtCombine.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.txtCurrency, "txtCurrency");
      this.txtCurrency.Name = "txtCurrency";
      this.helpProvider1.SetShowHelp((Control) this.txtCurrency, (bool) componentResourceManager.GetObject("txtCurrency.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.cboVariableChargeInd, "cboVariableChargeInd");
      this.cboVariableChargeInd.DisplayMemberQ = "";
      this.cboVariableChargeInd.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboVariableChargeInd.DropDownWidth = 280;
      this.cboVariableChargeInd.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboVariableChargeInd, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboVariableChargeInd, SystemColors.WindowText);
      this.cboVariableChargeInd.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboVariableChargeInd, componentResourceManager.GetString("cboVariableChargeInd.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboVariableChargeInd, (HelpNavigator) componentResourceManager.GetObject("cboVariableChargeInd.HelpNavigator"));
      this.cboVariableChargeInd.Name = "cboVariableChargeInd";
      this.cboVariableChargeInd.SelectedIndexQ = -1;
      this.cboVariableChargeInd.SelectedItemQ = (object) null;
      this.cboVariableChargeInd.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboVariableChargeInd, (bool) componentResourceManager.GetObject("cboVariableChargeInd.ShowHelp"));
      this.cboVariableChargeInd.ValueMemberQ = "";
      componentResourceManager.ApplyResources((object) this.lblPercentOf, "lblPercentOf");
      this.lblPercentOf.Name = "lblPercentOf";
      this.helpProvider1.SetShowHelp((Control) this.lblPercentOf, (bool) componentResourceManager.GetObject("lblPercentOf.ShowHelp"));
      this.edtVariable.Allow = "";
      this.edtVariable.Disallow = "";
      this.edtVariable.eMask = eMasks.maskCustom;
      this.edtVariable.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtVariable, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtVariable, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtVariable, componentResourceManager.GetString("edtVariable.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtVariable, (HelpNavigator) componentResourceManager.GetObject("edtVariable.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtVariable, "edtVariable");
      this.edtVariable.Mask = "#######.##~-";
      this.edtVariable.Name = "edtVariable";
      this.helpProvider1.SetShowHelp((Control) this.edtVariable, (bool) componentResourceManager.GetObject("edtVariable.ShowHelp"));
      this.edtFixed.Allow = "";
      this.edtFixed.Disallow = "";
      this.edtFixed.eMask = eMasks.maskCustom;
      this.edtFixed.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtFixed, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtFixed, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtFixed, componentResourceManager.GetString("edtFixed.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtFixed, (HelpNavigator) componentResourceManager.GetObject("edtFixed.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtFixed, "edtFixed");
      this.edtFixed.Mask = "$######0.00~-";
      this.edtFixed.Name = "edtFixed";
      this.helpProvider1.SetShowHelp((Control) this.edtFixed, (bool) componentResourceManager.GetObject("edtFixed.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkVariable, componentResourceManager.GetString("chkVariable.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkVariable, (HelpNavigator) componentResourceManager.GetObject("chkVariable.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkVariable, "chkVariable");
      this.chkVariable.Name = "chkVariable";
      this.helpProvider1.SetShowHelp((Control) this.chkVariable, (bool) componentResourceManager.GetObject("chkVariable.ShowHelp"));
      this.chkVariable.UseVisualStyleBackColor = true;
      this.chkVariable.CheckedChanged += new EventHandler(this.chkVariable_CheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.chkFixed, componentResourceManager.GetString("chkFixed.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkFixed, (HelpNavigator) componentResourceManager.GetObject("chkFixed.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkFixed, "chkFixed");
      this.chkFixed.Name = "chkFixed";
      this.helpProvider1.SetShowHelp((Control) this.chkFixed, (bool) componentResourceManager.GetObject("chkFixed.ShowHelp"));
      this.chkFixed.UseVisualStyleBackColor = true;
      this.chkFixed.CheckedChanged += new EventHandler(this.chkFixed_CheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.chkIncludeAdditionalHandlingCharge, componentResourceManager.GetString("chkIncludeAdditionalHandlingCharge.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkIncludeAdditionalHandlingCharge, (HelpNavigator) componentResourceManager.GetObject("chkIncludeAdditionalHandlingCharge.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkIncludeAdditionalHandlingCharge, "chkIncludeAdditionalHandlingCharge");
      this.chkIncludeAdditionalHandlingCharge.Name = "chkIncludeAdditionalHandlingCharge";
      this.helpProvider1.SetShowHelp((Control) this.chkIncludeAdditionalHandlingCharge, (bool) componentResourceManager.GetObject("chkIncludeAdditionalHandlingCharge.ShowHelp"));
      this.chkIncludeAdditionalHandlingCharge.UseVisualStyleBackColor = true;
      this.chkIncludeAdditionalHandlingCharge.CheckedChanged += new EventHandler(this.chkIncludeAdditionalHandlingCharge_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.gbxLabelFormat, "gbxLabelFormat");
      this.gbxLabelFormat.BorderThickness = 1f;
      this.tableLayoutPanelOtherPrefs.SetColumnSpan((Control) this.gbxLabelFormat, 3);
      this.gbxLabelFormat.Controls.Add((Control) this.cboBarcodeDocTab);
      this.gbxLabelFormat.Controls.Add((Control) this.rdoBarcodeDocTab);
      this.gbxLabelFormat.Controls.Add((Control) this.btnDocTabConfig);
      this.gbxLabelFormat.Controls.Add((Control) this.rdoCustomizeDocTab);
      this.gbxLabelFormat.Controls.Add((Control) this.rdoDefaultDocTab);
      this.gbxLabelFormat.Controls.Add((Control) this.cboLabelFormat);
      this.gbxLabelFormat.Controls.Add((Control) this.lblLabelFormat);
      this.gbxLabelFormat.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxLabelFormat.Name = "gbxLabelFormat";
      this.gbxLabelFormat.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxLabelFormat, (bool) componentResourceManager.GetObject("gbxLabelFormat.ShowHelp"));
      this.gbxLabelFormat.TabStop = false;
      componentResourceManager.ApplyResources((object) this.cboBarcodeDocTab, "cboBarcodeDocTab");
      this.cboBarcodeDocTab.DisplayMemberQ = "";
      this.cboBarcodeDocTab.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboBarcodeDocTab.DropDownWidth = 310;
      this.cboBarcodeDocTab.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboBarcodeDocTab, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboBarcodeDocTab, SystemColors.WindowText);
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
      this.rdoCustomizeDocTab.CheckedChanged += new EventHandler(this.rdoCustomizeDocTab_CheckedChanged_1);
      this.helpProvider1.SetHelpKeyword((Control) this.rdoDefaultDocTab, componentResourceManager.GetString("rdoDefaultDocTab.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoDefaultDocTab, (HelpNavigator) componentResourceManager.GetObject("rdoDefaultDocTab.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdoDefaultDocTab, "rdoDefaultDocTab");
      this.rdoDefaultDocTab.Name = "rdoDefaultDocTab";
      this.helpProvider1.SetShowHelp((Control) this.rdoDefaultDocTab, (bool) componentResourceManager.GetObject("rdoDefaultDocTab.ShowHelp"));
      this.rdoDefaultDocTab.UseVisualStyleBackColor = true;
      this.rdoDefaultDocTab.CheckedChanged += new EventHandler(this.rdoCustomizeDocTab_CheckedChanged_1);
      componentResourceManager.ApplyResources((object) this.cboLabelFormat, "cboLabelFormat");
      this.cboLabelFormat.DisplayMemberQ = "";
      this.cboLabelFormat.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboLabelFormat.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboLabelFormat, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboLabelFormat, SystemColors.WindowText);
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
      componentResourceManager.ApplyResources((object) this.gbxMiscellaneous, "gbxMiscellaneous");
      this.gbxMiscellaneous.BorderThickness = 1f;
      this.gbxMiscellaneous.Controls.Add((Control) this.flpMiscellaneous);
      this.gbxMiscellaneous.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxMiscellaneous.Name = "gbxMiscellaneous";
      this.gbxMiscellaneous.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxMiscellaneous, (bool) componentResourceManager.GetObject("gbxMiscellaneous.ShowHelp"));
      this.gbxMiscellaneous.TabStop = false;
      this.flpMiscellaneous.Controls.Add((Control) this.chkRequireDims);
      this.flpMiscellaneous.Controls.Add((Control) this.chkRequireRates);
      this.flpMiscellaneous.Controls.Add((Control) this.chkTrackingNbrOverwrite);
      this.flpMiscellaneous.Controls.Add((Control) this.chkOverrideGroundToHome);
      this.flpMiscellaneous.Controls.Add((Control) this.chkGroundHomeToggle);
      componentResourceManager.ApplyResources((object) this.flpMiscellaneous, "flpMiscellaneous");
      this.flpMiscellaneous.Name = "flpMiscellaneous";
      this.helpProvider1.SetHelpKeyword((Control) this.chkRequireDims, componentResourceManager.GetString("chkRequireDims.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkRequireDims, (HelpNavigator) componentResourceManager.GetObject("chkRequireDims.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkRequireDims, "chkRequireDims");
      this.chkRequireDims.Name = "chkRequireDims";
      this.helpProvider1.SetShowHelp((Control) this.chkRequireDims, (bool) componentResourceManager.GetObject("chkRequireDims.ShowHelp"));
      this.chkRequireDims.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.chkRequireRates, "chkRequireRates");
      this.chkRequireRates.Name = "chkRequireRates";
      this.chkRequireRates.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkTrackingNbrOverwrite, componentResourceManager.GetString("chkTrackingNbrOverwrite.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkTrackingNbrOverwrite, (HelpNavigator) componentResourceManager.GetObject("chkTrackingNbrOverwrite.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkTrackingNbrOverwrite, "chkTrackingNbrOverwrite");
      this.chkTrackingNbrOverwrite.Name = "chkTrackingNbrOverwrite";
      this.helpProvider1.SetShowHelp((Control) this.chkTrackingNbrOverwrite, (bool) componentResourceManager.GetObject("chkTrackingNbrOverwrite.ShowHelp"));
      this.chkTrackingNbrOverwrite.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkOverrideGroundToHome, componentResourceManager.GetString("chkOverrideGroundToHome.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkOverrideGroundToHome, (HelpNavigator) componentResourceManager.GetObject("chkOverrideGroundToHome.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkOverrideGroundToHome, "chkOverrideGroundToHome");
      this.chkOverrideGroundToHome.Name = "chkOverrideGroundToHome";
      this.helpProvider1.SetShowHelp((Control) this.chkOverrideGroundToHome, (bool) componentResourceManager.GetObject("chkOverrideGroundToHome.ShowHelp"));
      this.chkOverrideGroundToHome.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.chkGroundHomeToggle, "chkGroundHomeToggle");
      this.chkGroundHomeToggle.Name = "chkGroundHomeToggle";
      this.chkGroundHomeToggle.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.pnlInsertInReferences, "pnlInsertInReferences");
      this.pnlInsertInReferences.Controls.Add((Control) this.gbxRequireRef);
      this.pnlInsertInReferences.Controls.Add((Control) this.gbxInsertInRef);
      this.pnlInsertInReferences.Name = "pnlInsertInReferences";
      this.helpProvider1.SetShowHelp((Control) this.pnlInsertInReferences, (bool) componentResourceManager.GetObject("pnlInsertInReferences.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.gbxRequireRef, "gbxRequireRef");
      this.gbxRequireRef.BorderThickness = 1f;
      this.gbxRequireRef.Controls.Add((Control) this.chkAddlRef3);
      this.gbxRequireRef.Controls.Add((Control) this.chkAddlRef2);
      this.gbxRequireRef.Controls.Add((Control) this.chkAddlRef1);
      this.gbxRequireRef.Controls.Add((Control) this.chkRequireReferences);
      this.gbxRequireRef.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxRequireRef.Name = "gbxRequireRef";
      this.gbxRequireRef.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxRequireRef, (bool) componentResourceManager.GetObject("gbxRequireRef.ShowHelp"));
      this.gbxRequireRef.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkAddlRef3, componentResourceManager.GetString("chkAddlRef3.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAddlRef3, (HelpNavigator) componentResourceManager.GetObject("chkAddlRef3.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAddlRef3, "chkAddlRef3");
      this.chkAddlRef3.Name = "chkAddlRef3";
      this.helpProvider1.SetShowHelp((Control) this.chkAddlRef3, (bool) componentResourceManager.GetObject("chkAddlRef3.ShowHelp"));
      this.chkAddlRef3.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkAddlRef2, componentResourceManager.GetString("chkAddlRef2.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAddlRef2, (HelpNavigator) componentResourceManager.GetObject("chkAddlRef2.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAddlRef2, "chkAddlRef2");
      this.chkAddlRef2.Name = "chkAddlRef2";
      this.helpProvider1.SetShowHelp((Control) this.chkAddlRef2, (bool) componentResourceManager.GetObject("chkAddlRef2.ShowHelp"));
      this.chkAddlRef2.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkAddlRef1, componentResourceManager.GetString("chkAddlRef1.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAddlRef1, (HelpNavigator) componentResourceManager.GetObject("chkAddlRef1.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAddlRef1, "chkAddlRef1");
      this.chkAddlRef1.Name = "chkAddlRef1";
      this.helpProvider1.SetShowHelp((Control) this.chkAddlRef1, (bool) componentResourceManager.GetObject("chkAddlRef1.ShowHelp"));
      this.chkAddlRef1.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkRequireReferences, componentResourceManager.GetString("chkRequireReferences.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkRequireReferences, (HelpNavigator) componentResourceManager.GetObject("chkRequireReferences.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkRequireReferences, "chkRequireReferences");
      this.chkRequireReferences.Name = "chkRequireReferences";
      this.helpProvider1.SetShowHelp((Control) this.chkRequireReferences, (bool) componentResourceManager.GetObject("chkRequireReferences.ShowHelp"));
      this.chkRequireReferences.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.gbxInsertInRef, "gbxInsertInRef");
      this.gbxInsertInRef.BorderThickness = 1f;
      this.gbxInsertInRef.Controls.Add((Control) this.cboInsertInReferences);
      this.gbxInsertInRef.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxInsertInRef.Name = "gbxInsertInRef";
      this.gbxInsertInRef.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxInsertInRef, (bool) componentResourceManager.GetObject("gbxInsertInRef.ShowHelp"));
      this.gbxInsertInRef.TabStop = false;
      componentResourceManager.ApplyResources((object) this.cboInsertInReferences, "cboInsertInReferences");
      this.cboInsertInReferences.DisplayMemberQ = "";
      this.cboInsertInReferences.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboInsertInReferences.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboInsertInReferences, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboInsertInReferences, SystemColors.WindowText);
      this.cboInsertInReferences.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboInsertInReferences, componentResourceManager.GetString("cboInsertInReferences.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboInsertInReferences, (HelpNavigator) componentResourceManager.GetObject("cboInsertInReferences.HelpNavigator"));
      this.cboInsertInReferences.Name = "cboInsertInReferences";
      this.cboInsertInReferences.SelectedIndexQ = -1;
      this.cboInsertInReferences.SelectedItemQ = (object) null;
      this.cboInsertInReferences.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboInsertInReferences, (bool) componentResourceManager.GetObject("cboInsertInReferences.ShowHelp"));
      this.cboInsertInReferences.ValueMemberQ = "";
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanelOtherPrefs);
      this.Name = nameof (OtherPreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.OtherPreferences_Load);
      this.tableLayoutPanelOtherPrefs.ResumeLayout(false);
      this.gbxReferenceLabel.ResumeLayout(false);
      this.gbxHandlingCharge.ResumeLayout(false);
      this.panelHandlingCharge.ResumeLayout(false);
      this.panelHandlingCharge.PerformLayout();
      this.gbxLabelFormat.ResumeLayout(false);
      this.gbxMiscellaneous.ResumeLayout(false);
      this.flpMiscellaneous.ResumeLayout(false);
      this.flpMiscellaneous.PerformLayout();
      this.pnlInsertInReferences.ResumeLayout(false);
      this.gbxRequireRef.ResumeLayout(false);
      this.gbxInsertInRef.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
