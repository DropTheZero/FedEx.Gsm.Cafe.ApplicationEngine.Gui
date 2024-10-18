// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.FieldPreferences
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Controller.Utilities;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.DatabaseForms;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.DataAccess;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class FieldPreferences : UserControlHelpEx
  {
    private ShipDefl _shipDefl;
    private int _prevSelection = -1;
    private int _currentSelection = -1;
    private bool _bIgnore;
    private bool _combosLoaded;
    private bool _bLoading;
    private Decimal _dOriginalSpinValue;
    private int _prevPrefTypeIndex = -1;
    private int _currentPrefTypeIndex = -1;
    private IContainer components;
    private ColorGroupBox gbxBehavior;
    private ColorGroupBox gbxFieldValue;
    private RadioButton rdbConfigurable;
    private RadioButton rdbSkip;
    private RadioButton rdbCarry;
    private RadioButton rdbConstant;
    private RadioButton rdbNone;
    private Button btnConfigurable;
    protected ListBox lbxFields;
    protected RadioButton rdbConstantNo;
    protected RadioButton rdbConstantYes;
    protected ListBox lbxConstant;
    protected FdxMaskedEdit edtConstant;
    protected ComboBoxEx cboConstant;
    protected Button btnBrowse;
    private CheckBox chkDefaultRecipToResi;
    private ComboBoxEx cboStartPos;
    protected CheckBox chkValidateAndRequireDeptNotes;
    public NumericUpDown spnConstant;
    private FocusExtender focusExtender1;
    protected ComboBoxEx cboRemitCode;
    protected Label lblCodRemitCode;
    protected ColorGroupBox gbxOtherPrefs;
    protected ColorGroupBox gbxStartPosition;
    protected Label lblReturnRecip;
    protected ComboBoxEx cboReturnRecip;
    protected Label label2;
    protected ComboBoxEx cboDIACode;
    protected Label label1;
    public CheckBox chkAlwaysUseReturnToCode;
    private Label lblConstantExt;
    protected FlowLayoutPanel flpConstantExt;
    protected FdxMaskedEdit edtConstantExt;

    public ShipDefl PreferenceObject
    {
      get => this._shipDefl;
      set
      {
        this._shipDefl = value;
        this.RefreshList();
      }
    }

    public bool IsLoading
    {
      get => this._bLoading;
      set => this._bLoading = value;
    }

    public int PrevPrefTypeIndex
    {
      get => this._prevPrefTypeIndex;
      set => this._prevPrefTypeIndex = value;
    }

    public int CurrentPrefTypeIndex
    {
      get => this._currentPrefTypeIndex;
      set => this._currentPrefTypeIndex = value;
    }

    protected virtual void PopulateControl(FieldPref fp)
    {
    }

    protected virtual void ConfigureControl(Control c, FieldPref fp)
    {
    }

    protected virtual void SetConstantValue(FieldPref fp)
    {
    }

    protected virtual bool SaveFieldPref(int index) => false;

    protected FieldPreferences() => this.InitializeComponent();

    protected FieldPref GetSelectedFieldPref()
    {
      FieldPref selectedFieldPref = (FieldPref) null;
      if (this._shipDefl != null)
      {
        DataRowView dataRowView = this.lbxFields.Items[this.lbxFields.SelectedIndex] as DataRowView;
        int result = 0;
        int.TryParse(dataRowView["Index"].ToString(), out result);
        selectedFieldPref = this._shipDefl.GetFieldPref(result);
      }
      return selectedFieldPref;
    }

    protected virtual void ShowValidBehaviors()
    {
      FieldPref selectedFieldPref = this.GetSelectedFieldPref();
      if (selectedFieldPref == null)
        return;
      this.rdbNone.Visible = selectedFieldPref.AllowedBehaviors.Contains(ShipDefl.Behavior.None);
      this.rdbConstant.Visible = selectedFieldPref.AllowedBehaviors.Contains(ShipDefl.Behavior.Constant);
      this.rdbCarry.Visible = selectedFieldPref.AllowedBehaviors.Contains(ShipDefl.Behavior.Carry);
      this.rdbSkip.Visible = selectedFieldPref.AllowedBehaviors.Contains(ShipDefl.Behavior.Skip);
      this.rdbConfigurable.Visible = selectedFieldPref.AllowedBehaviors.Contains(ShipDefl.Behavior.Configurable);
    }

    protected virtual void SetBehavior()
    {
      this._bLoading = true;
      this.rdbNone.Checked = true;
      FieldPref selectedFieldPref = this.GetSelectedFieldPref();
      if (selectedFieldPref != null)
      {
        if (selectedFieldPref.Behavior == ShipDefl.Behavior.ExternalSource)
          selectedFieldPref.Behavior = ShipDefl.Behavior.None;
        ((RadioButton) this.gbxBehavior.Controls["rdb" + selectedFieldPref.Behavior.ToString()]).Checked = true;
        if (this.rdbConstantYes.Visible)
        {
          this.rdbConstantYes.Checked = false;
          this.rdbConstantNo.Checked = false;
          this.SetConstantValue(selectedFieldPref);
        }
      }
      this._bLoading = false;
    }

    protected virtual ShipDefl.Behavior GetBehavior()
    {
      if (this.rdbNone.Checked)
        return ShipDefl.Behavior.None;
      if (this.rdbConstant.Checked)
        return ShipDefl.Behavior.Constant;
      if (this.rdbCarry.Checked)
        return ShipDefl.Behavior.Carry;
      if (this.rdbSkip.Checked)
        return ShipDefl.Behavior.Skip;
      return this.rdbConfigurable.Checked ? ShipDefl.Behavior.Configurable : ShipDefl.Behavior.None;
    }

    protected virtual int CompareFieldPrefs(FieldPref fp1, FieldPref fp2)
    {
      return fp1.FieldDescription.CompareTo(fp2.FieldDescription);
    }

    protected virtual void RefreshList()
    {
      if (this._shipDefl == null)
        return;
      this._shipDefl.FieldPrefs.Sort(new Comparison<FieldPref>(this.CompareFieldPrefs));
      this.lbxFields.DataSource = (object) this._shipDefl.FieldPrefs;
      this.lbxFields.DisplayMember = "FieldDescription";
      this.lbxFields.ValueMember = "Index";
    }

    protected virtual void BehaviorCheckedChanged(object sender, EventArgs e)
    {
      RadioButton radioButton = sender as RadioButton;
      if (!radioButton.Checked)
        return;
      FieldPref selectedFieldPref = this.GetSelectedFieldPref();
      if (selectedFieldPref == null)
        return;
      this.ShowFieldValue(selectedFieldPref, radioButton.Equals((object) this.rdbConstant));
      this.btnConfigurable.Visible = radioButton.Equals((object) this.rdbConfigurable);
      if (this._bLoading || !radioButton.Equals((object) this.rdbConfigurable) || !this.btnConfigurable.Visible)
        return;
      this.btnConfigurable.PerformClick();
    }

    protected virtual void ShowFieldValue(FieldPref fp, bool bShow)
    {
      if (fp == null)
        return;
      if (bShow)
      {
        foreach (Control control in (ArrangedElementCollection) this.gbxFieldValue.Controls)
          control.Hide();
        switch (fp.Control)
        {
          case FieldPref.ControlType.DropDownCombo:
            this.ConfigureControl((Control) this.cboConstant, fp);
            this.cboConstant.Show();
            this.PopulateControl(fp);
            break;
          case FieldPref.ControlType.TextBox:
            if (this._shipDefl.GetType() == typeof (IShipDefl) && fp.Index == 23 || this._shipDefl.GetType() == typeof (TDShipDefl) && fp.Index == 23 || this._shipDefl.GetType() == typeof (FShipDefl) && fp.Index == 12 || this._shipDefl.GetType() == typeof (FShipDefl) && fp.Index == 15)
            {
              this.ConfigureControl((Control) this.spnConstant, fp);
              this.spnConstant.Show();
              Decimal result;
              Decimal.TryParse(fp.StringFieldDeflVal, out result);
              if (result > 0M)
              {
                this.spnConstant.Value = result;
                break;
              }
              break;
            }
            if (this._shipDefl.GetType() == typeof (IShipDefl) && fp.Index == 40 || this._shipDefl.GetType() == typeof (DShipDefl) && fp.Index == 27)
            {
              this.ConfigureControl((Control) this.edtConstant, fp);
              this.edtConstant.Show();
              this.flpConstantExt.Show();
              break;
            }
            this.ConfigureControl((Control) this.edtConstant, fp);
            this.edtConstant.Show();
            if (this.edtConstant.GetType() == typeof (FdxMaskedEdit))
            {
              this.edtConstant.Text = fp.StringFieldDeflVal;
              break;
            }
            this.edtConstant.Text = fp.StringFieldDeflVal;
            break;
          case FieldPref.ControlType.Unused1:
            this.ConfigureControl((Control) this.spnConstant, fp);
            this.spnConstant.Show();
            this.spnConstant.Value = (Decimal) fp.IntFieldDeflVal;
            break;
          case FieldPref.ControlType.RadioButtons:
            if (this._shipDefl.GetType() == typeof (IShipDefl))
              this.ConfigureControl((Control) this.rdbConstantYes, fp);
            this.rdbConstantYes.Show();
            this.rdbConstantNo.Show();
            break;
          case FieldPref.ControlType.MultiSelectListBox:
            this.lbxConstant.Show();
            this.PopulateControl(fp);
            break;
          case FieldPref.ControlType.DropDownComboAndTextBox:
            this.ConfigureControl((Control) this.cboConstant, fp);
            this.ConfigureControl((Control) this.edtConstant, fp);
            this.cboConstant.Show();
            this.edtConstant.Show();
            this.PopulateControl(fp);
            break;
          case FieldPref.ControlType.TextBoxWithBrowse:
            this.ConfigureControl((Control) this.edtConstant, fp);
            this.edtConstant.Show();
            this.edtConstant.Text = fp.StringFieldDeflVal;
            this.btnBrowse.Show();
            break;
        }
        this.SetConstantValue(fp);
      }
      else
      {
        foreach (Control control in (ArrangedElementCollection) this.gbxFieldValue.Controls)
          control.Hide();
      }
    }

    protected virtual Control GetControl(FieldPref fp)
    {
      if (fp == null)
        return (Control) null;
      switch (fp.Control)
      {
        case FieldPref.ControlType.NoControl:
          return (Control) null;
        case FieldPref.ControlType.DropDownCombo:
        case FieldPref.ControlType.DropDownComboAndTextBox:
          return (Control) this.cboConstant;
        case FieldPref.ControlType.TextBox:
        case FieldPref.ControlType.TextBoxWithBrowse:
          return (Control) this.edtConstant;
        case FieldPref.ControlType.Unused1:
          return (Control) this.spnConstant;
        case FieldPref.ControlType.RadioButtons:
          return (Control) this.rdbConstantYes;
        case FieldPref.ControlType.MultiSelectListBox:
          return (Control) this.lbxConstant;
        default:
          return (Control) null;
      }
    }

    protected void SetSpecialServiceListBoxItems(List<string> ssEnumList)
    {
      this.lbxConstant.SelectedIndex = -1;
      DataTable dataTable = ((DataTable) this.lbxConstant.DataSource).Copy();
      foreach (string ssEnum in ssEnumList)
      {
        string s = (string) null;
        string filterExpression = "Code = '" + ssEnum + "'";
        foreach (DataRow dataRow in dataTable.Select(filterExpression))
          s = dataRow["Description"].ToString();
        if (s != string.Empty)
        {
          int stringExact = this.lbxConstant.FindStringExact(s);
          if (stringExact != -1)
            this.lbxConstant.SetSelected(stringExact, true);
        }
      }
    }

    protected DataTable BuildSpecialServiceDataTable(List<SplSvc> ssList)
    {
      return this.BuildSpecialServiceDataTable(ssList, false);
    }

    protected DataTable BuildSpecialServiceDataTable(List<SplSvc> ssList, bool isGPR3)
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Code", typeof (string));
      dataTable.Columns.Add("Description", typeof (string));
      if (isGPR3)
        dataTable.Columns.Add("SplSvc", typeof (object));
      foreach (SplSvc ss in ssList)
      {
        DataRow row = dataTable.NewRow();
        SplSvc.SpecialServiceType specialServiceCode;
        if (isGPR3)
        {
          row["Code"] = ss.SpecialServiceCode != SplSvc.SpecialServiceType.NUM_SPEC_SVCS ? (object) Convert.ToString(Enum.Format(typeof (SplSvc.SpecialServiceType), (object) ss.SpecialServiceCode, "d")) : (object) ss.OfferingID;
          DataRow dataRow = row;
          string str = ss.MediumName;
          if (str == null)
          {
            FedEx.Gsm.Common.Languafier.Languafier languafier = GuiData.Languafier;
            specialServiceCode = ss.SpecialServiceCode;
            string id = "SS_" + specialServiceCode.ToString();
            str = languafier.Translate(id);
          }
          dataRow["Description"] = (object) str;
          row["SplSvc"] = (object) ss;
        }
        else
        {
          row["Code"] = (object) Convert.ToString(Enum.Format(typeof (SplSvc.SpecialServiceType), (object) ss.SpecialServiceCode, "d"));
          DataRow dataRow = row;
          FedEx.Gsm.Common.Languafier.Languafier languafier = GuiData.Languafier;
          specialServiceCode = ss.SpecialServiceCode;
          string id = "SS_" + specialServiceCode.ToString();
          string str = languafier.Translate(id);
          dataRow["Description"] = (object) str;
        }
        dataTable.Rows.Add(row);
      }
      if (isGPR3)
        dataTable.DefaultView.Sort = "Description ASC";
      return dataTable;
    }

    protected bool ValidateExpressSpecialServicesGPR(IEnumerable<SplSvc> sss)
    {
      bool flag1 = true;
      bool flag2 = false;
      bool flag3 = false;
      IDictionary<string, SplSvc> conflictMap = SpecialServiceHelper.GetConflictMap(sss);
      foreach (SplSvc splSvc in sss)
      {
        if (splSvc.SpecialServiceCode == SplSvc.SpecialServiceType.DangerousGoods)
          flag2 = true;
        else if (splSvc.SpecialServiceCode == SplSvc.SpecialServiceType.DryIceOnly)
          flag3 = true;
        if (flag2 & flag3)
        {
          int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9528), Error.ErrorType.Failure);
          return false;
        }
        if (splSvc.ConflictsOfferingIDs != null)
        {
          foreach (OfferingConflict conflictsOfferingId in splSvc.ConflictsOfferingIDs)
          {
            if (conflictsOfferingId.ConflictOfferingType == Offering.MainOfferingType.SERVICEOPTION)
            {
              string conflictOfferingId = conflictsOfferingId.ConflictOfferingID;
              if (conflictMap.ContainsKey(conflictOfferingId))
              {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) splSvc.MediumName, (object) conflictMap[conflictOfferingId].MediumName);
                int num = (int) Utility.DisplayError(stringBuilder.ToString());
                return false;
              }
            }
          }
        }
      }
      return flag1;
    }

    protected void BindSpecialServiceDataTableToListBox(DataTable dt)
    {
      this.lbxConstant.DataSource = (object) dt;
      this.lbxConstant.DisplayMember = "Description";
      this.lbxConstant.ValueMember = "Code";
      this.lbxConstant.SelectedItems.Clear();
    }

    private void lbxFields_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._prevSelection > -1 && this._prevSelection == this._currentSelection)
        this._bIgnore = false;
      else
        this._prevSelection = this._currentSelection;
      this._currentSelection = this.lbxFields.SelectedIndex;
      if (this._bIgnore)
        this._bIgnore = false;
      else if (this._prevSelection > -1 && this._prevSelection < this.lbxFields.Items.Count)
      {
        if (this.SaveFieldPref((int) ((DataRowView) this.lbxFields.Items[this._prevSelection])[0]))
        {
          this.ShowValidBehaviors();
          this.SetBehavior();
        }
        else
        {
          this._bIgnore = true;
          this.lbxFields.SelectedIndex = this._prevSelection;
        }
      }
      else
      {
        this.ShowValidBehaviors();
        this.SetBehavior();
      }
    }

    private void PopulateCombos()
    {
      if (!this._combosLoaded)
      {
        this.PopulateComboWithSenderData(this.cboRemitCode);
        this.PopulateStartPositionCombo(this.cboStartPos);
        this.PopulateComboWithSenderData(this.cboReturnRecip);
        this.PopulateComboWithSenderData(this.cboDIACode);
      }
      this._combosLoaded = true;
    }

    private void PopulateStartPositionCombo(ComboBoxEx combo)
    {
      combo.DataSourceQ = (object) Utility.GetDataTable(Utility.ListTypes.StartPosition);
      combo.ValueMemberQ = "Code";
      combo.DisplayMemberQ = "Description";
    }

    private void PopulateComboWithSenderData(ComboBoxEx combo)
    {
      DataTable output;
      GuiData.AppController.ShipEngine.GetDataList(GsmDataAccess.ListSpecification.Sender_List, out output, new Error());
      if (output != null)
      {
        output.Columns.Add(new DataColumn("CodeAndName", typeof (string), "SenderCode + ' - ' + SenderName"));
        if (output.Rows.Count > 0)
        {
          DataRow row = output.NewRow();
          row["SenderCode"] = (object) null;
          row["SenderName"] = (object) null;
          output.Rows.InsertAt(row, 0);
        }
        output.DefaultView.Sort = "SenderCode ASC";
        combo.DisplayMemberQ = "CodeAndName";
        combo.ValueMemberQ = "SenderCode";
        combo.DataSourceQ = (object) output;
        combo.SelectedIndexQ = -1;
      }
      else
      {
        combo.DataSource = (object) null;
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Unable recip retrieve list of senders or no senders defined. Check log for DB error.");
      }
    }

    private void FieldPreferences_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.FixupListBox(this.lbxConstant);
      this.ObjectToScreen();
    }

    private void btnConfigurable_Click(object sender, EventArgs e)
    {
      int result = -1;
      if (this._shipDefl != null)
        int.TryParse((this.lbxFields.Items[this.lbxFields.SelectedIndex] as DataRowView)["Index"].ToString(), out result);
      if (result < 0)
        return;
      ShipDefl preferenceObject = this.PreferenceObject;
      int maxChar = 30;
      if (this.PreferenceObject.GetType() == typeof (DShipDefl) && result == 6 || this.PreferenceObject.GetType() == typeof (IShipDefl) && result == 12)
        maxChar = 39;
      ConfigurableRefView configurableRefView = new ConfigurableRefView(ref preferenceObject, result, maxChar);
      if (configurableRefView.ShowDialog() != DialogResult.OK && configurableRefView.iNumRows != 0)
        return;
      if (configurableRefView.iNumRows == 0)
        this.rdbNone.Checked = true;
      this.SaveFieldPref(result);
    }

    public virtual void ObjectToScreen()
    {
      this.PopulateCombos();
      if (this._shipDefl == null)
        return;
      this.cboStartPos.SelectedValue = (object) Convert.ToString((int) this._shipDefl.StartPosn);
      this.chkValidateAndRequireDeptNotes.Checked = this._shipDefl.ValidateDptNotes;
      this.chkDefaultRecipToResi.Checked = this._shipDefl.DeliveryType == ShipDefl.DeliveryTypeOptions.Residential;
      if (string.IsNullOrEmpty(this._shipDefl.CodRemittance))
        this.cboRemitCode.SelectedIndex = -1;
      else
        this.cboRemitCode.SelectedValue = (object) this._shipDefl.CodRemittance;
      if (string.IsNullOrEmpty(this._shipDefl.ReturnRecipient))
        this.cboReturnRecip.SelectedIndex = -1;
      else
        this.cboReturnRecip.SelectedValueQ = (object) this._shipDefl.ReturnRecipient;
      if (string.IsNullOrEmpty(this._shipDefl.DIARemittance))
        this.cboDIACode.SelectedIndex = -1;
      else
        this.cboDIACode.SelectedValueQ = (object) this._shipDefl.DIARemittance;
      if (!(this._shipDefl.GetType() == typeof (DShipDefl)))
        return;
      this.chkAlwaysUseReturnToCode.Checked = this._shipDefl.AlwaysUseReturnToCode;
    }

    public virtual bool OkToClose()
    {
      this._shipDefl.StartPosn = (ShipDefl.StartPosition) Enum.Parse(typeof (ShipDefl.StartPosition), this.cboStartPos.SelectedValueQ as string);
      this._shipDefl.ValidateDptNotes = this.chkValidateAndRequireDeptNotes.Checked;
      this._shipDefl.DeliveryType = this.chkDefaultRecipToResi.Checked ? ShipDefl.DeliveryTypeOptions.Residential : ShipDefl.DeliveryTypeOptions.Commercial;
      if (this.cboRemitCode.SelectedItem != null)
      {
        this._shipDefl.CodRemittance = this.cboRemitCode.SelectedValueQ as string;
        this.PreferenceObject.CodRemittance = this.cboRemitCode.SelectedValueQ as string;
      }
      if (this.cboReturnRecip.SelectedItem != null)
        this._shipDefl.ReturnRecipient = this.cboReturnRecip.SelectedValueQ as string;
      if (this.cboDIACode.SelectedItem != null)
        this._shipDefl.DIARemittance = this.cboDIACode.SelectedValueQ as string;
      if (this._shipDefl.GetType() == typeof (DShipDefl))
        this._shipDefl.AlwaysUseReturnToCode = this.chkAlwaysUseReturnToCode.Checked;
      bool close = this.SaveFieldPref((int) ((DataRowView) this.lbxFields.Items[this.lbxFields.SelectedIndex])[0]);
      if (this._shipDefl.GetType() == typeof (DShipDefl) && this._shipDefl.FieldPrefs[7].Behavior == ShipDefl.Behavior.Skip && this._shipDefl.ValidateDptNotes || this._shipDefl.GetType() == typeof (IShipDefl) && this._shipDefl.FieldPrefs[13].Behavior == ShipDefl.Behavior.Skip && this._shipDefl.ValidateDptNotes)
      {
        int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9523), Error.ErrorType.Failure);
        this.chkValidateAndRequireDeptNotes.Focus();
        close = false;
      }
      return close;
    }

    private void cboConstant_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!this.edtConstant.Visible)
        return;
      if (this.cboConstant.SelectedIndex <= -1)
        return;
      try
      {
        this.edtConstant.SetMask(eMasks.maskCustom);
        this.edtConstant.Mask = "IIIIIIIIIIIIIIIIIIIIIIIII";
        this.edtConstant.Text = ((DataRowView) this.cboConstant.SelectedItemQ)["Description"].ToString();
      }
      catch
      {
      }
    }

    private void spnConstant_KeyDown(object sender, KeyEventArgs e)
    {
      this._dOriginalSpinValue = ((NumericUpDown) sender).Value;
    }

    private void spnConstant_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
        return;
      e.Handled = true;
      NumericUpDown numericUpDown = sender as NumericUpDown;
      if (!(this._dOriginalSpinValue >= numericUpDown.Minimum) || !(this._dOriginalSpinValue <= numericUpDown.Maximum))
        return;
      numericUpDown.Value = this._dOriginalSpinValue;
    }

    private void cboConstant_Leave(object sender, EventArgs e)
    {
      try
      {
        if (!this.edtConstant.Visible || this.cboConstant.SelectedIndex != -1 || string.IsNullOrEmpty(this.cboConstant.Text))
          return;
        Department d = new Department();
        d.Code = this.cboConstant.Text;
        if (new DepartmentEdit(d, Utility.FormOperation.Flexible).ShowDialog() != DialogResult.OK)
          return;
        DataTable output = (DataTable) null;
        GuiData.AppController.ShipEngine.GetDataList(GsmDataAccess.ListSpecification.Department_DropDown, out output, new Error());
        if (output != null)
          output.DefaultView.Sort = "Code ASC";
        this.cboConstant.ValueMemberQ = "Code";
        this.cboConstant.DisplayMemberQ = "Code";
        this.cboConstant.DataSourceQ = (object) output;
        this.cboConstant.SelectedIndexQ = -1;
        this.cboConstant.SelectedIndexQ = -1;
        this.edtConstant.Text = "";
        this.cboConstant.SelectedValue = (object) d.Code;
      }
      catch
      {
      }
    }

    private void FixupListBox(ListBox box)
    {
      if (box == null)
        return;
      if (!box.IsHandleCreated)
        box.CreateControl();
      box.ItemHeight = box.GetItemHeight(0);
      box.DrawMode = DrawMode.OwnerDrawVariable;
      box.DrawItem += new DrawItemEventHandler(this.lbxConstant_DrawItem);
      box.MeasureItem += new MeasureItemEventHandler(this.box_MeasureItem);
    }

    private void box_MeasureItem(object sender, MeasureItemEventArgs e)
    {
      if (!(sender is ListBox listBox))
        return;
      Rectangle itemRectangle = listBox.GetItemRectangle(e.Index);
      e.ItemHeight = listBox.Font.Height;
      e.ItemWidth = itemRectangle.Width;
    }

    private void lbxConstant_DrawItem(object sender, DrawItemEventArgs e)
    {
      ListBox listBox = sender as ListBox;
      e.DrawBackground();
      if (listBox.Enabled && (e.State & DrawItemState.Selected) != DrawItemState.Selected)
        e.Graphics.FillRectangle(Brushes.White, e.Bounds);
      if (!listBox.Enabled && e.Index == -1)
        e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds);
      if (e.Index >= 0 && e.Index < listBox.Items.Count)
      {
        string itemText = listBox.GetItemText(listBox.Items[e.Index]);
        if (!listBox.Enabled)
          ControlPaint.DrawStringDisabled(e.Graphics, itemText, e.Font, SystemColors.Control, (RectangleF) e.Bounds, StringFormat.GenericDefault);
        else if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
          e.Graphics.DrawString(itemText, e.Font, Brushes.Black, (RectangleF) e.Bounds);
        else if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
        {
          using (SolidBrush solidBrush = new SolidBrush(listBox.ForeColor))
            e.Graphics.DrawString(itemText, e.Font, (Brush) solidBrush, (RectangleF) e.Bounds);
        }
        else
          e.Graphics.DrawString(itemText, e.Font, SystemBrushes.HighlightText, (RectangleF) e.Bounds);
        if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
          ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
      }
      e.DrawFocusRectangle();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FieldPreferences));
      this.lbxFields = new ListBox();
      this.gbxBehavior = new ColorGroupBox();
      this.rdbConfigurable = new RadioButton();
      this.rdbSkip = new RadioButton();
      this.rdbCarry = new RadioButton();
      this.rdbConstant = new RadioButton();
      this.rdbNone = new RadioButton();
      this.btnConfigurable = new Button();
      this.gbxFieldValue = new ColorGroupBox();
      this.flpConstantExt = new FlowLayoutPanel();
      this.lblConstantExt = new Label();
      this.edtConstantExt = new FdxMaskedEdit();
      this.spnConstant = new NumericUpDown();
      this.cboConstant = new ComboBoxEx();
      this.rdbConstantYes = new RadioButton();
      this.rdbConstantNo = new RadioButton();
      this.btnBrowse = new Button();
      this.edtConstant = new FdxMaskedEdit();
      this.lbxConstant = new ListBox();
      this.gbxOtherPrefs = new ColorGroupBox();
      this.cboDIACode = new ComboBoxEx();
      this.chkAlwaysUseReturnToCode = new CheckBox();
      this.cboReturnRecip = new ComboBoxEx();
      this.cboRemitCode = new ComboBoxEx();
      this.label2 = new Label();
      this.lblReturnRecip = new Label();
      this.lblCodRemitCode = new Label();
      this.chkValidateAndRequireDeptNotes = new CheckBox();
      this.chkDefaultRecipToResi = new CheckBox();
      this.gbxStartPosition = new ColorGroupBox();
      this.cboStartPos = new ComboBoxEx();
      this.label1 = new Label();
      this.focusExtender1 = new FocusExtender();
      this.gbxBehavior.SuspendLayout();
      this.gbxFieldValue.SuspendLayout();
      this.flpConstantExt.SuspendLayout();
      this.spnConstant.BeginInit();
      this.gbxOtherPrefs.SuspendLayout();
      this.gbxStartPosition.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      this.lbxFields.DisplayMember = "Description";
      this.helpProvider1.SetHelpKeyword((Control) this.lbxFields, componentResourceManager.GetString("lbxFields.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.lbxFields, (HelpNavigator) componentResourceManager.GetObject("lbxFields.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.lbxFields, "lbxFields");
      this.lbxFields.Name = "lbxFields";
      this.helpProvider1.SetShowHelp((Control) this.lbxFields, (bool) componentResourceManager.GetObject("lbxFields.ShowHelp"));
      this.lbxFields.SelectedIndexChanged += new EventHandler(this.lbxFields_SelectedIndexChanged);
      this.gbxBehavior.BorderThickness = 1f;
      this.gbxBehavior.Controls.Add((Control) this.rdbConfigurable);
      this.gbxBehavior.Controls.Add((Control) this.rdbSkip);
      this.gbxBehavior.Controls.Add((Control) this.rdbCarry);
      this.gbxBehavior.Controls.Add((Control) this.rdbConstant);
      this.gbxBehavior.Controls.Add((Control) this.rdbNone);
      this.gbxBehavior.Controls.Add((Control) this.btnConfigurable);
      this.gbxBehavior.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxBehavior, "gbxBehavior");
      this.gbxBehavior.Name = "gbxBehavior";
      this.gbxBehavior.RoundCorners = 5;
      this.gbxBehavior.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdbConfigurable, componentResourceManager.GetString("rdbConfigurable.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbConfigurable, (HelpNavigator) componentResourceManager.GetObject("rdbConfigurable.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbConfigurable, "rdbConfigurable");
      this.rdbConfigurable.Name = "rdbConfigurable";
      this.helpProvider1.SetShowHelp((Control) this.rdbConfigurable, (bool) componentResourceManager.GetObject("rdbConfigurable.ShowHelp"));
      this.rdbConfigurable.CheckedChanged += new EventHandler(this.BehaviorCheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbSkip, componentResourceManager.GetString("rdbSkip.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbSkip, (HelpNavigator) componentResourceManager.GetObject("rdbSkip.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbSkip, "rdbSkip");
      this.rdbSkip.Name = "rdbSkip";
      this.helpProvider1.SetShowHelp((Control) this.rdbSkip, (bool) componentResourceManager.GetObject("rdbSkip.ShowHelp"));
      this.rdbSkip.CheckedChanged += new EventHandler(this.BehaviorCheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbCarry, componentResourceManager.GetString("rdbCarry.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbCarry, (HelpNavigator) componentResourceManager.GetObject("rdbCarry.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbCarry, "rdbCarry");
      this.rdbCarry.Name = "rdbCarry";
      this.helpProvider1.SetShowHelp((Control) this.rdbCarry, (bool) componentResourceManager.GetObject("rdbCarry.ShowHelp"));
      this.rdbCarry.CheckedChanged += new EventHandler(this.BehaviorCheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbConstant, componentResourceManager.GetString("rdbConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbConstant, (HelpNavigator) componentResourceManager.GetObject("rdbConstant.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbConstant, "rdbConstant");
      this.rdbConstant.Name = "rdbConstant";
      this.helpProvider1.SetShowHelp((Control) this.rdbConstant, (bool) componentResourceManager.GetObject("rdbConstant.ShowHelp"));
      this.rdbConstant.CheckedChanged += new EventHandler(this.BehaviorCheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbNone, componentResourceManager.GetString("rdbNone.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbNone, (HelpNavigator) componentResourceManager.GetObject("rdbNone.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbNone, "rdbNone");
      this.rdbNone.Name = "rdbNone";
      this.helpProvider1.SetShowHelp((Control) this.rdbNone, (bool) componentResourceManager.GetObject("rdbNone.ShowHelp"));
      this.rdbNone.CheckedChanged += new EventHandler(this.BehaviorCheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.btnConfigurable, componentResourceManager.GetString("btnConfigurable.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnConfigurable, (HelpNavigator) componentResourceManager.GetObject("btnConfigurable.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.btnConfigurable, "btnConfigurable");
      this.btnConfigurable.Name = "btnConfigurable";
      this.helpProvider1.SetShowHelp((Control) this.btnConfigurable, (bool) componentResourceManager.GetObject("btnConfigurable.ShowHelp"));
      this.btnConfigurable.UseVisualStyleBackColor = true;
      this.btnConfigurable.Click += new EventHandler(this.btnConfigurable_Click);
      this.gbxFieldValue.BorderThickness = 1f;
      this.gbxFieldValue.Controls.Add((Control) this.flpConstantExt);
      this.gbxFieldValue.Controls.Add((Control) this.spnConstant);
      this.gbxFieldValue.Controls.Add((Control) this.cboConstant);
      this.gbxFieldValue.Controls.Add((Control) this.rdbConstantYes);
      this.gbxFieldValue.Controls.Add((Control) this.rdbConstantNo);
      this.gbxFieldValue.Controls.Add((Control) this.btnBrowse);
      this.gbxFieldValue.Controls.Add((Control) this.edtConstant);
      this.gbxFieldValue.Controls.Add((Control) this.lbxConstant);
      this.gbxFieldValue.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxFieldValue, "gbxFieldValue");
      this.gbxFieldValue.Name = "gbxFieldValue";
      this.gbxFieldValue.RoundCorners = 5;
      this.gbxFieldValue.TabStop = false;
      componentResourceManager.ApplyResources((object) this.flpConstantExt, "flpConstantExt");
      this.flpConstantExt.Controls.Add((Control) this.lblConstantExt);
      this.flpConstantExt.Controls.Add((Control) this.edtConstantExt);
      this.flpConstantExt.Name = "flpConstantExt";
      componentResourceManager.ApplyResources((object) this.lblConstantExt, "lblConstantExt");
      this.lblConstantExt.Name = "lblConstantExt";
      this.edtConstantExt.Allow = "";
      componentResourceManager.ApplyResources((object) this.edtConstantExt, "edtConstantExt");
      this.edtConstantExt.Disallow = "";
      this.edtConstantExt.eMask = eMasks.maskCustom;
      this.edtConstantExt.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtConstantExt, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtConstantExt, SystemColors.WindowText);
      this.edtConstantExt.Mask = "";
      this.edtConstantExt.Name = "edtConstantExt";
      this.focusExtender1.SetFocusBackColor((Control) this.spnConstant, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.spnConstant, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.spnConstant, componentResourceManager.GetString("spnConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnConstant, (HelpNavigator) componentResourceManager.GetObject("spnConstant.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.spnConstant, "spnConstant");
      this.spnConstant.Maximum = new Decimal(new int[4]
      {
        9,
        0,
        0,
        0
      });
      this.spnConstant.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.spnConstant.Name = "spnConstant";
      this.spnConstant.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.spnConstant, (bool) componentResourceManager.GetObject("spnConstant.ShowHelp"));
      this.spnConstant.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.spnConstant.KeyDown += new KeyEventHandler(this.spnConstant_KeyDown);
      this.spnConstant.KeyUp += new KeyEventHandler(this.spnConstant_KeyUp);
      componentResourceManager.ApplyResources((object) this.cboConstant, "cboConstant");
      this.cboConstant.DisplayMemberQ = "";
      this.cboConstant.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboConstant.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboConstant, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboConstant, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.cboConstant, componentResourceManager.GetString("cboConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboConstant, (HelpNavigator) componentResourceManager.GetObject("cboConstant.HelpNavigator"));
      this.cboConstant.Name = "cboConstant";
      this.cboConstant.SelectedIndexQ = -1;
      this.cboConstant.SelectedItemQ = (object) null;
      this.cboConstant.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboConstant, (bool) componentResourceManager.GetObject("cboConstant.ShowHelp"));
      this.cboConstant.ValueMemberQ = "";
      this.cboConstant.SelectedIndexChanged += new EventHandler(this.cboConstant_SelectedIndexChanged);
      this.cboConstant.Leave += new EventHandler(this.cboConstant_Leave);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbConstantYes, componentResourceManager.GetString("rdbConstantYes.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbConstantYes, (HelpNavigator) componentResourceManager.GetObject("rdbConstantYes.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbConstantYes, "rdbConstantYes");
      this.rdbConstantYes.Name = "rdbConstantYes";
      this.helpProvider1.SetShowHelp((Control) this.rdbConstantYes, (bool) componentResourceManager.GetObject("rdbConstantYes.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbConstantNo, componentResourceManager.GetString("rdbConstantNo.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbConstantNo, (HelpNavigator) componentResourceManager.GetObject("rdbConstantNo.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbConstantNo, "rdbConstantNo");
      this.rdbConstantNo.Name = "rdbConstantNo";
      this.helpProvider1.SetShowHelp((Control) this.rdbConstantNo, (bool) componentResourceManager.GetObject("rdbConstantNo.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnBrowse, "btnBrowse");
      this.helpProvider1.SetHelpKeyword((Control) this.btnBrowse, componentResourceManager.GetString("btnBrowse.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnBrowse, (HelpNavigator) componentResourceManager.GetObject("btnBrowse.HelpNavigator"));
      this.btnBrowse.Name = "btnBrowse";
      this.helpProvider1.SetShowHelp((Control) this.btnBrowse, (bool) componentResourceManager.GetObject("btnBrowse.ShowHelp"));
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.edtConstant.Allow = "";
      componentResourceManager.ApplyResources((object) this.edtConstant, "edtConstant");
      this.edtConstant.Disallow = "";
      this.edtConstant.eMask = eMasks.maskCustom;
      this.edtConstant.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtConstant, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtConstant, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtConstant, componentResourceManager.GetString("edtConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtConstant, (HelpNavigator) componentResourceManager.GetObject("edtConstant.HelpNavigator"));
      this.edtConstant.Mask = "";
      this.edtConstant.Name = "edtConstant";
      this.helpProvider1.SetShowHelp((Control) this.edtConstant, (bool) componentResourceManager.GetObject("edtConstant.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lbxConstant, "lbxConstant");
      this.lbxConstant.FormattingEnabled = true;
      this.lbxConstant.Name = "lbxConstant";
      this.lbxConstant.SelectionMode = SelectionMode.MultiSimple;
      this.gbxOtherPrefs.BorderThickness = 1f;
      this.gbxOtherPrefs.Controls.Add((Control) this.cboDIACode);
      this.gbxOtherPrefs.Controls.Add((Control) this.chkAlwaysUseReturnToCode);
      this.gbxOtherPrefs.Controls.Add((Control) this.cboReturnRecip);
      this.gbxOtherPrefs.Controls.Add((Control) this.cboRemitCode);
      this.gbxOtherPrefs.Controls.Add((Control) this.label2);
      this.gbxOtherPrefs.Controls.Add((Control) this.lblReturnRecip);
      this.gbxOtherPrefs.Controls.Add((Control) this.lblCodRemitCode);
      this.gbxOtherPrefs.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxOtherPrefs, "gbxOtherPrefs");
      this.gbxOtherPrefs.Name = "gbxOtherPrefs";
      this.gbxOtherPrefs.RoundCorners = 5;
      this.gbxOtherPrefs.TabStop = false;
      this.cboDIACode.DisplayMemberQ = "";
      this.cboDIACode.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDIACode.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDIACode, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDIACode, SystemColors.WindowText);
      this.cboDIACode.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDIACode, componentResourceManager.GetString("cboDIACode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboDIACode, (HelpNavigator) componentResourceManager.GetObject("cboDIACode.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.cboDIACode, "cboDIACode");
      this.cboDIACode.Name = "cboDIACode";
      this.cboDIACode.SelectedIndexQ = -1;
      this.cboDIACode.SelectedItemQ = (object) null;
      this.cboDIACode.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDIACode, (bool) componentResourceManager.GetObject("cboDIACode.ShowHelp"));
      this.cboDIACode.ValueMemberQ = "";
      this.helpProvider1.SetHelpKeyword((Control) this.chkAlwaysUseReturnToCode, componentResourceManager.GetString("chkAlwaysUseReturnToCode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAlwaysUseReturnToCode, (HelpNavigator) componentResourceManager.GetObject("chkAlwaysUseReturnToCode.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkAlwaysUseReturnToCode, "chkAlwaysUseReturnToCode");
      this.chkAlwaysUseReturnToCode.Name = "chkAlwaysUseReturnToCode";
      this.helpProvider1.SetShowHelp((Control) this.chkAlwaysUseReturnToCode, (bool) componentResourceManager.GetObject("chkAlwaysUseReturnToCode.ShowHelp"));
      this.chkAlwaysUseReturnToCode.UseVisualStyleBackColor = true;
      this.cboReturnRecip.DisplayMemberQ = "";
      this.cboReturnRecip.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboReturnRecip.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboReturnRecip, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboReturnRecip, SystemColors.WindowText);
      this.cboReturnRecip.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboReturnRecip, componentResourceManager.GetString("cboReturnRecip.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboReturnRecip, (HelpNavigator) componentResourceManager.GetObject("cboReturnRecip.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.cboReturnRecip, "cboReturnRecip");
      this.cboReturnRecip.Name = "cboReturnRecip";
      this.cboReturnRecip.SelectedIndexQ = -1;
      this.cboReturnRecip.SelectedItemQ = (object) null;
      this.cboReturnRecip.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboReturnRecip, (bool) componentResourceManager.GetObject("cboReturnRecip.ShowHelp"));
      this.cboReturnRecip.ValueMemberQ = "";
      this.cboRemitCode.DisplayMemberQ = "";
      this.cboRemitCode.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboRemitCode.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboRemitCode, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboRemitCode, SystemColors.WindowText);
      this.cboRemitCode.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboRemitCode, componentResourceManager.GetString("cboRemitCode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboRemitCode, (HelpNavigator) componentResourceManager.GetObject("cboRemitCode.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.cboRemitCode, "cboRemitCode");
      this.cboRemitCode.Name = "cboRemitCode";
      this.cboRemitCode.SelectedIndexQ = -1;
      this.cboRemitCode.SelectedItemQ = (object) null;
      this.cboRemitCode.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboRemitCode, (bool) componentResourceManager.GetObject("cboRemitCode.ShowHelp"));
      this.cboRemitCode.ValueMemberQ = "";
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      this.helpProvider1.SetShowHelp((Control) this.label2, (bool) componentResourceManager.GetObject("label2.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblReturnRecip, "lblReturnRecip");
      this.lblReturnRecip.Name = "lblReturnRecip";
      this.helpProvider1.SetShowHelp((Control) this.lblReturnRecip, (bool) componentResourceManager.GetObject("lblReturnRecip.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblCodRemitCode, "lblCodRemitCode");
      this.lblCodRemitCode.Name = "lblCodRemitCode";
      this.helpProvider1.SetHelpKeyword((Control) this.chkValidateAndRequireDeptNotes, componentResourceManager.GetString("chkValidateAndRequireDeptNotes.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkValidateAndRequireDeptNotes, (HelpNavigator) componentResourceManager.GetObject("chkValidateAndRequireDeptNotes.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkValidateAndRequireDeptNotes, "chkValidateAndRequireDeptNotes");
      this.chkValidateAndRequireDeptNotes.Name = "chkValidateAndRequireDeptNotes";
      this.helpProvider1.SetShowHelp((Control) this.chkValidateAndRequireDeptNotes, (bool) componentResourceManager.GetObject("chkValidateAndRequireDeptNotes.ShowHelp"));
      this.chkValidateAndRequireDeptNotes.UseVisualStyleBackColor = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkDefaultRecipToResi, componentResourceManager.GetString("chkDefaultRecipToResi.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkDefaultRecipToResi, (HelpNavigator) componentResourceManager.GetObject("chkDefaultRecipToResi.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkDefaultRecipToResi, "chkDefaultRecipToResi");
      this.chkDefaultRecipToResi.Name = "chkDefaultRecipToResi";
      this.helpProvider1.SetShowHelp((Control) this.chkDefaultRecipToResi, (bool) componentResourceManager.GetObject("chkDefaultRecipToResi.ShowHelp"));
      this.chkDefaultRecipToResi.UseVisualStyleBackColor = true;
      this.gbxStartPosition.BorderThickness = 1f;
      this.gbxStartPosition.Controls.Add((Control) this.cboStartPos);
      this.gbxStartPosition.Controls.Add((Control) this.label1);
      this.gbxStartPosition.Controls.Add((Control) this.chkValidateAndRequireDeptNotes);
      this.gbxStartPosition.Controls.Add((Control) this.chkDefaultRecipToResi);
      this.gbxStartPosition.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxStartPosition, "gbxStartPosition");
      this.gbxStartPosition.Name = "gbxStartPosition";
      this.gbxStartPosition.RoundCorners = 5;
      this.gbxStartPosition.TabStop = false;
      this.cboStartPos.DisplayMemberQ = "";
      this.cboStartPos.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboStartPos.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboStartPos, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboStartPos, SystemColors.WindowText);
      this.cboStartPos.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboStartPos, componentResourceManager.GetString("cboStartPos.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboStartPos, (HelpNavigator) componentResourceManager.GetObject("cboStartPos.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.cboStartPos, "cboStartPos");
      this.cboStartPos.Name = "cboStartPos";
      this.cboStartPos.SelectedIndexQ = -1;
      this.cboStartPos.SelectedItemQ = (object) null;
      this.cboStartPos.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboStartPos, (bool) componentResourceManager.GetObject("cboStartPos.ShowHelp"));
      this.cboStartPos.ValueMemberQ = "";
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.helpProvider1.SetShowHelp((Control) this.label1, (bool) componentResourceManager.GetObject("label1.ShowHelp"));
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.gbxStartPosition);
      this.Controls.Add((Control) this.gbxOtherPrefs);
      this.Controls.Add((Control) this.gbxFieldValue);
      this.Controls.Add((Control) this.gbxBehavior);
      this.Controls.Add((Control) this.lbxFields);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (FieldPreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.FieldPreferences_Load);
      this.gbxBehavior.ResumeLayout(false);
      this.gbxFieldValue.ResumeLayout(false);
      this.gbxFieldValue.PerformLayout();
      this.flpConstantExt.ResumeLayout(false);
      this.flpConstantExt.PerformLayout();
      this.spnConstant.EndInit();
      this.gbxOtherPrefs.ResumeLayout(false);
      this.gbxStartPosition.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
