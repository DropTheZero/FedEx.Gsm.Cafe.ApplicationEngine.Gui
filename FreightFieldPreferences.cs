// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.FreightFieldPreferences
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.DataAccess;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class FreightFieldPreferences : FieldPreferences
  {
    private bool _bRefreshingList;
    private IContainer components;

    public FreightFieldPreferences() => this.InitializeComponent();

    protected override void RefreshList()
    {
      if (this.PreferenceObject == null)
        return;
      this._bRefreshingList = true;
      this.edtConstant.Text = "";
      this.cboConstant.DataSourceQ = (object) null;
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Index", typeof (int));
      dataTable.Columns.Add("Description");
      foreach (FieldPref fieldPref in this.PreferenceObject.FieldPrefs)
      {
        if ((!GuiData.CurrentAccount.is_FREIGHT2020_HANDLING_UNITS_CTL_Initiative_Enabled || fieldPref.Index != 12) && (!GuiData.CurrentAccount.is_FREIGHT2020_HANDLING_UNITS_CTL_Initiative_Enabled || fieldPref.Index != 8) && (GuiData.CurrentAccount.is_FREIGHT2020_HANDLING_UNITS_CTL_Initiative_Enabled || fieldPref.Index != 25))
        {
          string str = GuiData.Languafier.Translate("FIELDPREF_F_" + fieldPref.Index.ToString());
          if (GuiData.CurrentAccount.is_FREIGHT2020_HANDLING_UNITS_CTL_Initiative_Enabled && fieldPref.Index == 15)
            str = GuiData.Languafier.Translate("FIELDPREF_F_" + fieldPref.Index.ToString() + "_2020");
          DataRow row = dataTable.NewRow();
          row["Index"] = (object) fieldPref.Index;
          row["Description"] = (object) str;
          dataTable.Rows.Add(row);
        }
      }
      dataTable.DefaultView.Sort = "Description ASC";
      this.lbxFields.DataSource = (object) dataTable;
      this.lbxFields.DisplayMember = "Description";
      this.lbxFields.ValueMember = "Index";
      this._bRefreshingList = false;
    }

    protected override void ConfigureControl(Control c, FieldPref fp)
    {
      if (c == null || fp == null)
        return;
      if (fp.Control == FieldPref.ControlType.DropDownCombo)
        ((ComboBox) c).DropDownStyle = ComboBoxStyle.DropDownList;
      else if (fp.Control == FieldPref.ControlType.DropDownComboAndTextBox && c is ComboBox)
      {
        ((ComboBox) c).DropDownStyle = ComboBoxStyle.DropDown;
      }
      else
      {
        if (fp.Control != FieldPref.ControlType.TextBox && fp.Control != FieldPref.ControlType.TextBoxWithBrowse && (fp.Control != FieldPref.ControlType.DropDownComboAndTextBox || !(c is TextBox)))
          return;
        string str = (string) null;
        eMasks mask = eMasks.maskNone;
        if (c is FdxMaskedEdit)
          ((FdxMaskedEdit) c).SetMask(mask);
        switch ((FShipDefl.FieldPreference) fp.Index)
        {
          case FShipDefl.FieldPreference.PickupContactNumber:
          case FShipDefl.FieldPreference.HazMatEmergencyPhoneNbr:
            mask = eMasks.maskPhoneTen;
            break;
          case FShipDefl.FieldPreference.DeclaredValue:
            str = "######";
            break;
          case FShipDefl.FieldPreference.FourQuadrantLabelStart:
            this.edtConstant.Visible = false;
            this.spnConstant.Maximum = 4M;
            this.spnConstant.Show();
            break;
          case FShipDefl.FieldPreference.HazMat:
            ((FdxMaskedEdit) c).SetMask('A', 35);
            break;
          case FShipDefl.FieldPreference.NumberOfLabels:
            this.edtConstant.Visible = false;
            this.spnConstant.Maximum = 500M;
            this.spnConstant.Show();
            break;
          case FShipDefl.FieldPreference.PickupCloseTime:
            str = "99:99 >BB~/^[0-9]{0,4} ?([AP]M?)?$/~";
            break;
          case FShipDefl.FieldPreference.PickupContactName:
            ((FdxMaskedEdit) c).SetMask('I', 22);
            break;
        }
        if (str != null)
        {
          ((FdxMaskedEdit) c).SetMask(eMasks.maskCustom);
          ((FdxMaskedEdit) c).Mask = str;
        }
        else
        {
          if (mask == eMasks.maskNone)
            return;
          ((FdxMaskedEdit) c).SetMask(mask);
        }
      }
    }

    protected override void PopulateControl(FieldPref fp)
    {
      if (fp == null)
        return;
      DataTable output1 = new DataTable();
      this.cboConstant.DataSourceQ = (object) null;
      switch (fp.Index)
      {
        case 0:
          Error error = new Error();
          if (GuiData.AppController.ShipEngine.GetDataList(GsmDataAccess.ListSpecification.FreightAccount_Table, out output1, error) != 1)
            break;
          if (output1 != null)
          {
            this.cboConstant.ValueMemberQ = "FreightAcctNbr";
            this.cboConstant.DisplayMemberQ = "FreightAcctNbr";
          }
          this.cboConstant.DataSourceQ = (object) output1;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case 1:
          DataTable dataTable1 = Utility.GetDataTable(Utility.ListTypes.BOLType);
          if (dataTable1 != null)
          {
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          this.cboConstant.DataSourceQ = (object) dataTable1;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case 2:
        case 3:
          DataTable output2 = (DataTable) null;
          try
          {
            GuiData.AppController.ShipEngine.GetDataList((object) null, GsmDataAccess.ListSpecification.BrokerList, out output2, (List<GsmFilter>) null, (List<GsmSort>) null, (List<string>) null, new Error());
            if (output2 != null)
            {
              this.cboConstant.ValueMemberQ = "Code";
              this.cboConstant.DisplayMemberQ = "Code";
            }
            this.cboConstant.DataSourceQ = (object) output2;
            this.cboConstant.SelectedIndexQ = -1;
            this.cboConstant.SelectedIndexQ = -1;
            break;
          }
          catch
          {
            break;
          }
        case 4:
          DataTable dataTable2 = Utility.GetDataTable(Utility.ListTypes.FreightNmfcClasses);
          if (dataTable2 != null)
          {
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          this.cboConstant.DataSourceQ = (object) dataTable2;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case 5:
          this.lbxConstant.DataSource = (object) Utility.GetDataTable(Utility.ListTypes.FreightCommodityOptionsPref);
          this.lbxConstant.DisplayMember = "Description";
          this.lbxConstant.ValueMember = "Code";
          this.lbxConstant.SelectedItems.Clear();
          break;
        case 6:
          DataTable dataTable3 = Utility.GetDataTable(Utility.ListTypes.DcsTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable3;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case 11:
          this.lbxConstant.DataSource = (object) Utility.GetDataTable(Utility.ListTypes.FreightDeliveryOptionsPref);
          this.lbxConstant.DisplayMember = "Description";
          this.lbxConstant.ValueMember = "Code";
          this.lbxConstant.SelectedItems.Clear();
          break;
        case 16:
          DataTable dataTable4 = Utility.GetDataTable(Utility.ListTypes.FreightPaymentTerms);
          if (dataTable4 != null)
          {
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          this.cboConstant.DataSourceQ = (object) dataTable4;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case 17:
          DataTable dataTable5 = Utility.GetDataTable(Utility.ListTypes.FreightPaymentType);
          if (dataTable5 != null)
          {
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          this.cboConstant.DataSourceQ = (object) dataTable5;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case 18:
          this.lbxConstant.DataSource = (object) Utility.GetDataTable(Utility.ListTypes.FreightPickupOptionsPref);
          this.lbxConstant.DisplayMember = "Description";
          this.lbxConstant.ValueMember = "Code";
          this.lbxConstant.SelectedItems.Clear();
          break;
        case 21:
          DataTable dataTable6 = Utility.GetDataTable(Utility.ListTypes.FreightServiceTypes);
          if (dataTable6 != null)
          {
            dataTable6.DefaultView.RowFilter = "Code <> 3 AND Code <> 4";
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          this.cboConstant.DataSourceQ = (object) dataTable6;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case 22:
          DataTable dataTable7 = Utility.GetDataTable(Utility.ListTypes.FreightPackageTypesPreference);
          if (dataTable7 != null)
          {
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          dataTable7.DefaultView.Sort = "Code ASC";
          this.cboConstant.DataSourceQ = (object) dataTable7;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case 23:
          this.cboConstant.ValueMemberQ = "";
          this.cboConstant.DisplayMemberQ = "";
          this.cboConstant.DataSourceQ = (object) null;
          this.cboConstant.BeginUpdate();
          this.cboConstant.Items.Clear();
          foreach (string name in Enum.GetNames(typeof (WeightUnits)))
            this.cboConstant.Items.Add((object) name.ToLower());
          this.cboConstant.EndUpdate();
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case 24:
          DataTable dataTable8 = Utility.GetDataTable(Utility.ListTypes.FreightVolume);
          if (dataTable8 != null)
          {
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "CodeDescription";
          }
          dataTable8.DefaultView.Sort = "Code ASC";
          this.cboConstant.DataSourceQ = (object) dataTable8;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
      }
    }

    protected override void SetConstantValue(FieldPref fp)
    {
      if (fp == null)
        return;
      switch (fp.Index)
      {
        case 0:
        case 2:
        case 3:
        case 4:
        case 6:
        case 22:
        case 24:
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal;
          break;
        case 5:
        case 11:
        case 18:
          fp.DefaultValueType = FieldPref.PreferenceValueType.StringList;
          if (fp.StringListFieldDeflVal == null)
            break;
          this.SetSpecialServiceListBoxItems(fp.StringListFieldDeflVal);
          break;
        case 8:
        case 9:
        case 25:
        case 26:
          this.rdbConstantYes.Checked = fp.IntFieldDeflVal == 1;
          this.rdbConstantNo.Checked = fp.IntFieldDeflVal == 0;
          break;
        case 10:
          this.edtConstant.Raw = fp.StringFieldDeflVal;
          break;
        case 12:
        case 15:
          Decimal result;
          Decimal.TryParse(fp.StringFieldDeflVal, out result);
          if (!(result > 0M))
            break;
          this.spnConstant.Value = result;
          break;
        case 23:
          this.cboConstant.SelectedItem = (object) fp.StringFieldDeflVal;
          break;
        default:
          if (fp.DefaultValueType == FieldPref.PreferenceValueType.String && this.edtConstant.Visible)
          {
            this.edtConstant.Text = fp.StringFieldDeflVal;
            break;
          }
          if (fp.DefaultValueType == FieldPref.PreferenceValueType.Integer && this.cboConstant.Visible)
          {
            this.cboConstant.SelectedValue = (object) fp.IntFieldDeflVal.ToString();
            break;
          }
          if (fp.DefaultValueType != FieldPref.PreferenceValueType.String || !this.cboConstant.Visible)
            break;
          this.cboConstant.SelectedValue = (object) fp.StringFieldDeflVal.ToString();
          break;
      }
    }

    private bool SaveConstantValue(Control c, FieldPref fp)
    {
      if (c == null || fp == null)
        return false;
      bool flag = true;
      switch (fp.Control)
      {
        case FieldPref.ControlType.NoControl:
          fp.DefaultValueType = FieldPref.PreferenceValueType.None;
          break;
        case FieldPref.ControlType.DropDownCombo:
          switch ((FShipDefl.FieldPreference) fp.Index)
          {
            case FShipDefl.FieldPreference.AccountNumber:
            case FShipDefl.FieldPreference.BOLType:
            case FShipDefl.FieldPreference.ImportBroker:
            case FShipDefl.FieldPreference.ExportBroker:
            case FShipDefl.FieldPreference.Class:
            case FShipDefl.FieldPreference.DCSType:
            case FShipDefl.FieldPreference.PaymentTerms:
            case FShipDefl.FieldPreference.PaymentType:
            case FShipDefl.FieldPreference.ServiceType:
            case FShipDefl.FieldPreference.PackageType:
            case FShipDefl.FieldPreference.VolumeType:
              fp.StringFieldDeflVal = ((ComboBoxEx) c).SelectedValue as string;
              break;
            default:
              fp.StringFieldDeflVal = c.Text;
              break;
          }
          if (fp.DefaultValueType == FieldPref.PreferenceValueType.String && string.IsNullOrEmpty(fp.StringFieldDeflVal))
          {
            flag = false;
            break;
          }
          if (fp.DefaultValueType == FieldPref.PreferenceValueType.Integer && fp.IntFieldDeflVal < 0)
          {
            flag = false;
            break;
          }
          break;
        case FieldPref.ControlType.TextBox:
        case FieldPref.ControlType.TextBoxWithBrowse:
          switch ((FShipDefl.FieldPreference) fp.Index)
          {
            case FShipDefl.FieldPreference.PickupContactNumber:
            case FShipDefl.FieldPreference.HazMatEmergencyPhoneNbr:
              fp.StringFieldDeflVal = ((FdxMaskedEdit) c).Raw;
              break;
            case FShipDefl.FieldPreference.FourQuadrantLabelStart:
            case FShipDefl.FieldPreference.NumberOfLabels:
              fp.DefaultValueType = FieldPref.PreferenceValueType.String;
              fp.StringFieldDeflVal = this.spnConstant.Value.ToString();
              break;
            default:
              fp.DefaultValueType = FieldPref.PreferenceValueType.String;
              fp.StringFieldDeflVal = c.Text;
              break;
          }
          if (string.IsNullOrEmpty(fp.StringFieldDeflVal))
          {
            flag = false;
            break;
          }
          break;
        case FieldPref.ControlType.RadioButtons:
          if (fp.Index == 26 && ((RadioButton) c).Checked)
          {
            int num1 = (int) Utility.DisplayError(GuiData.Languafier.Translate("AutoTrackPrefVolumeWarning"), Error.ErrorType.Warning);
          }
          fp.IntFieldDeflVal = ((RadioButton) c).Checked ? 1 : 0;
          break;
        case FieldPref.ControlType.MultiSelectListBox:
          if (this.lbxConstant.SelectedItems.Count == 0)
          {
            int num2 = (int) Utility.DisplayError(GuiData.Languafier.Translate("InvalidConstant"), Error.ErrorType.Failure);
            flag = false;
          }
          if (flag)
          {
            fp.StringListFieldDeflVal = new List<string>();
            fp.DefaultValueType = FieldPref.PreferenceValueType.StringList;
            IEnumerator enumerator = this.lbxConstant.SelectedItems.GetEnumerator();
            try
            {
              while (enumerator.MoveNext())
              {
                string str = (string) ((DataRowView) enumerator.Current)[this.lbxConstant.ValueMember];
                if (!string.IsNullOrEmpty(str))
                  fp.StringListFieldDeflVal.Add(str);
              }
              break;
            }
            finally
            {
              if (enumerator is IDisposable disposable)
                disposable.Dispose();
            }
          }
          else
          {
            this.lbxConstant.SelectedItems.Clear();
            break;
          }
      }
      return flag;
    }

    protected override bool SaveFieldPref(int index)
    {
      if (this._bRefreshingList || index == -1)
        return true;
      bool flag = true;
      FieldPref fieldPref = this.PreferenceObject.GetFieldPref(index);
      if (fieldPref != null)
      {
        fieldPref.Behavior = this.GetBehavior();
        if (fieldPref.Behavior == ShipDefl.Behavior.Constant)
        {
          Control control = this.GetControl(fieldPref);
          switch (fieldPref.Control)
          {
            case FieldPref.ControlType.DropDownCombo:
            case FieldPref.ControlType.DropDownComboAndTextBox:
              if (this.cboConstant.Text == "")
                flag = false;
              if (fieldPref.Control == FieldPref.ControlType.DropDownComboAndTextBox && this.edtConstant.Raw == "")
              {
                flag = false;
                break;
              }
              break;
            case FieldPref.ControlType.TextBox:
            case FieldPref.ControlType.TextBoxWithBrowse:
              switch ((FShipDefl.FieldPreference) fieldPref.Index)
              {
                case FShipDefl.FieldPreference.FourQuadrantLabelStart:
                case FShipDefl.FieldPreference.NumberOfLabels:
                  if (this.spnConstant.Value < 1M)
                  {
                    flag = false;
                    break;
                  }
                  break;
                default:
                  if (this.edtConstant.Text == "" || this.edtConstant.Mask.Length > 0 && this.edtConstant.Raw == "")
                  {
                    flag = false;
                    break;
                  }
                  break;
              }
              break;
            case FieldPref.ControlType.MultiSelectListBox:
              if (this.lbxConstant.SelectedItems.Count == 0)
              {
                flag = false;
                break;
              }
              break;
          }
          if (flag)
          {
            flag = this.SaveConstantValue(control, fieldPref);
            if (!flag && fieldPref.Index != 5 && fieldPref.Index != 11 && fieldPref.Index != 18)
            {
              int num = (int) Utility.DisplayError(GuiData.Languafier.Translate("InvalidConstant"), Error.ErrorType.Failure);
              control.Focus();
            }
            else if (!flag)
              control.Focus();
          }
          else
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(9519), (object) GuiData.Languafier.Translate("FIELDPREF_F_" + fieldPref.Index.ToString()));
            int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
            control.Focus();
          }
        }
      }
      return flag;
    }

    private void FreightFieldPreferences_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.RefreshList();
    }

    private void cboPreferenceType_SelectedValueChanged(object sender, EventArgs e)
    {
      if (this.lbxFields.SelectedIndex <= -1 || !this.SaveFieldPref((int) ((DataRowView) this.lbxFields.Items[this.lbxFields.SelectedIndex])[0]))
        return;
      this.RefreshList();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FreightFieldPreferences));
      this.spnConstant.BeginInit();
      this.gbxOtherPrefs.SuspendLayout();
      this.gbxStartPosition.SuspendLayout();
      this.SuspendLayout();
      this.helpProvider1.SetHelpKeyword((Control) this.lbxFields, componentResourceManager.GetString("lbxFields.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.lbxFields, (HelpNavigator) componentResourceManager.GetObject("lbxFields.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.lbxFields, "lbxFields");
      this.helpProvider1.SetShowHelp((Control) this.lbxFields, (bool) componentResourceManager.GetObject("lbxFields.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbConstantNo, componentResourceManager.GetString("rdbConstantNo.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbConstantNo, (HelpNavigator) componentResourceManager.GetObject("rdbConstantNo.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdbConstantNo, (bool) componentResourceManager.GetObject("rdbConstantNo.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbConstantYes, componentResourceManager.GetString("rdbConstantYes.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbConstantYes, (HelpNavigator) componentResourceManager.GetObject("rdbConstantYes.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdbConstantYes, (bool) componentResourceManager.GetObject("rdbConstantYes.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lbxConstant, "lbxConstant");
      this.helpProvider1.SetHelpKeyword((Control) this.edtConstant, componentResourceManager.GetString("edtConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtConstant, (HelpNavigator) componentResourceManager.GetObject("edtConstant.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.edtConstant, (bool) componentResourceManager.GetObject("edtConstant.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.cboConstant, componentResourceManager.GetString("cboConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboConstant, (HelpNavigator) componentResourceManager.GetObject("cboConstant.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.cboConstant, "cboConstant");
      this.helpProvider1.SetShowHelp((Control) this.cboConstant, (bool) componentResourceManager.GetObject("cboConstant.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.btnBrowse, componentResourceManager.GetString("btnBrowse.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnBrowse, (HelpNavigator) componentResourceManager.GetObject("btnBrowse.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.btnBrowse, (bool) componentResourceManager.GetObject("btnBrowse.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkValidateAndRequireDeptNotes, componentResourceManager.GetString("chkValidateAndRequireDeptNotes.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkValidateAndRequireDeptNotes, (HelpNavigator) componentResourceManager.GetObject("chkValidateAndRequireDeptNotes.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chkValidateAndRequireDeptNotes, (bool) componentResourceManager.GetObject("chkValidateAndRequireDeptNotes.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.spnConstant, componentResourceManager.GetString("spnConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnConstant, (HelpNavigator) componentResourceManager.GetObject("spnConstant.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.spnConstant, (bool) componentResourceManager.GetObject("spnConstant.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.cboRemitCode, componentResourceManager.GetString("cboRemitCode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboRemitCode, (HelpNavigator) componentResourceManager.GetObject("cboRemitCode.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboRemitCode, (bool) componentResourceManager.GetObject("cboRemitCode.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.gbxOtherPrefs, "gbxOtherPrefs");
      componentResourceManager.ApplyResources((object) this.gbxStartPosition, "gbxStartPosition");
      this.helpProvider1.SetShowHelp((Control) this.lblReturnRecip, (bool) componentResourceManager.GetObject("lblReturnRecip.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.cboReturnRecip, componentResourceManager.GetString("cboReturnRecip.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboReturnRecip, (HelpNavigator) componentResourceManager.GetObject("cboReturnRecip.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboReturnRecip, (bool) componentResourceManager.GetObject("cboReturnRecip.ShowHelp"));
      this.helpProvider1.SetShowHelp((Control) this.label2, (bool) componentResourceManager.GetObject("label2.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.cboDIACode, componentResourceManager.GetString("cboDIACode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboDIACode, (HelpNavigator) componentResourceManager.GetObject("cboDIACode.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboDIACode, (bool) componentResourceManager.GetObject("cboDIACode.ShowHelp"));
      this.helpProvider1.SetShowHelp((Control) this.label1, (bool) componentResourceManager.GetObject("label1.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkAlwaysUseReturnToCode, componentResourceManager.GetString("chkAlwaysUseReturnToCode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAlwaysUseReturnToCode, (HelpNavigator) componentResourceManager.GetObject("chkAlwaysUseReturnToCode.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chkAlwaysUseReturnToCode, (bool) componentResourceManager.GetObject("chkAlwaysUseReturnToCode.ShowHelp"));
      componentResourceManager.ApplyResources((object) this, "$this");
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (FreightFieldPreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.FreightFieldPreferences_Load);
      this.spnConstant.EndInit();
      this.gbxOtherPrefs.ResumeLayout(false);
      this.gbxStartPosition.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
