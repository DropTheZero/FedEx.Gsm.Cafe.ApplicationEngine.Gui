// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Preferences.TDShipAlertPreferences
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui.Preferences
{
  public class TDShipAlertPreferences : ShipAlertPreferences
  {
    private bool _bRefreshingList;
    private IContainer components;

    public TDShipAlertPreferences() => this.InitializeComponent();

    protected override bool SaveFieldPref(int index)
    {
      if (this._bRefreshingList || index == -1)
        return true;
      bool flag = true;
      FieldPref emailFieldPref = this.PreferenceObject.GetEmailFieldPref(index);
      if (emailFieldPref != null)
      {
        emailFieldPref.Behavior = this.GetBehavior(emailFieldPref.Index);
        if (emailFieldPref.Behavior == ShipDefl.Behavior.Constant)
        {
          Control control = this.GetControl(emailFieldPref);
          if (control != null)
          {
            flag = this.SaveConstantValue(control, emailFieldPref);
            if (!flag)
            {
              int num = (int) Utility.DisplayError(GuiData.Languafier.Translate("InvalidConstant"), Error.ErrorType.Failure);
              control.Focus();
            }
          }
        }
      }
      return flag;
    }

    private bool SaveConstantValue(Control c, FieldPref fp)
    {
      if (c == null || fp == null)
        return false;
      bool flag = true;
      switch (fp.Control)
      {
        case FieldPref.ControlType.NoControl:
          fp.IntFieldDeflVal = ((RadioButton) c).Checked ? 1 : 0;
          break;
        case FieldPref.ControlType.DropDownCombo:
          fp.IntFieldDeflVal = Convert.ToInt32(this.cboConstant.SelectedValueQ as string);
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
          fp.StringFieldDeflVal = this.edtConstant.Text;
          if (string.IsNullOrEmpty(fp.StringFieldDeflVal))
          {
            flag = false;
            break;
          }
          break;
        default:
          fp.DefaultValueType = FieldPref.PreferenceValueType.None;
          break;
      }
      return flag;
    }

    protected override void RefreshList()
    {
      if (this.PreferenceObject == null || this.cboPreferenceType.SelectedValue == null)
        return;
      this._bRefreshingList = true;
      FieldPref.FieldPreferenceType fieldPreferenceType = (FieldPref.FieldPreferenceType) Enum.Parse(typeof (FieldPref.FieldPreferenceType), this.cboPreferenceType.SelectedValue.ToString());
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Index", typeof (int));
      dataTable.Columns.Add("Description");
      foreach (FieldPref emailNotifPref in this.PreferenceObject.EmailNotifPrefs)
      {
        if (emailNotifPref.FieldPrefType == fieldPreferenceType && emailNotifPref.FieldDescription != "DUMMY")
        {
          switch (fieldPreferenceType)
          {
            case FieldPref.FieldPreferenceType.Returns:
            case FieldPref.FieldPreferenceType.MPS:
            case FieldPref.FieldPreferenceType.ReturnsMPS:
              continue;
            default:
              if (emailNotifPref.Index != 8 && emailNotifPref.Index != 25 && emailNotifPref.Index != 39 && emailNotifPref.Index != 44 && emailNotifPref.Index != 45 && (emailNotifPref.Index != 36 && emailNotifPref.Index != 35 && emailNotifPref.Index != 37 && emailNotifPref.Index != 38 && emailNotifPref.Index != 42 && emailNotifPref.Index != 43 && emailNotifPref.Index != 41 && emailNotifPref.Index != 40 || GuiData.CurrentAccount.SHIPDATE_Noti_Estimated_Initiative_Enabled))
              {
                DataRow row = dataTable.NewRow();
                row["Index"] = (object) emailNotifPref.Index;
                row["Description"] = (object) GuiData.Languafier.Translate("EMAILFIELDPREF_T_" + emailNotifPref.Index.ToString());
                dataTable.Rows.Add(row);
                continue;
              }
              continue;
          }
        }
      }
      dataTable.DefaultView.Sort = "Description ASC";
      this.lbxFields.DataSource = (object) dataTable;
      this.lbxFields.DisplayMember = "Description";
      this.lbxFields.ValueMember = "Index";
      this._bRefreshingList = false;
    }

    protected override void PopulateControl(FieldPref fp)
    {
      if (fp == null)
        return;
      switch ((TDShipDefl.EmailFieldPreference) fp.Index)
      {
        case TDShipDefl.EmailFieldPreference.EmailFormatType:
        case TDShipDefl.EmailFieldPreference.EmailFormatTypeBroker:
        case TDShipDefl.EmailFieldPreference.EmailFormatTypeOther1:
        case TDShipDefl.EmailFieldPreference.EmailFormatTypeOther2:
        case TDShipDefl.EmailFieldPreference.EmailFormatTypeRecipent:
        case TDShipDefl.EmailFieldPreference.EmailFormatTypeSender:
          DataTable dataTable = Utility.GetDataTable(Utility.ListTypes.EmailFormatType);
          if (dataTable != null)
          {
            dataTable.Rows.Remove(dataTable.Select("Code = 2")[0]);
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          this.cboConstant.DataSourceQ = (object) dataTable;
          break;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TDShipAlertPreferences));
      this.SuspendLayout();
      this.helpProvider1.SetHelpKeyword((Control) this.cboConstant, componentResourceManager.GetString("cboConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboConstant, (HelpNavigator) componentResourceManager.GetObject("cboConstant.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboConstant, (bool) componentResourceManager.GetObject("cboConstant.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbUnchecked, componentResourceManager.GetString("rdbUnchecked.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbUnchecked, (HelpNavigator) componentResourceManager.GetObject("rdbUnchecked.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdbUnchecked, (bool) componentResourceManager.GetObject("rdbUnchecked.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbAutoSelect, componentResourceManager.GetString("rdbAutoSelect.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbAutoSelect, (HelpNavigator) componentResourceManager.GetObject("rdbAutoSelect.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdbAutoSelect, (bool) componentResourceManager.GetObject("rdbAutoSelect.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbSkip, componentResourceManager.GetString("rdbSkip.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbSkip, (HelpNavigator) componentResourceManager.GetObject("rdbSkip.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdbSkip, (bool) componentResourceManager.GetObject("rdbSkip.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbAlwaysChecked, componentResourceManager.GetString("rdbAlwaysChecked.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbAlwaysChecked, (HelpNavigator) componentResourceManager.GetObject("rdbAlwaysChecked.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdbAlwaysChecked, (bool) componentResourceManager.GetObject("rdbAlwaysChecked.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.cboPreferenceType, componentResourceManager.GetString("cboPreferenceType.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboPreferenceType, (HelpNavigator) componentResourceManager.GetObject("cboPreferenceType.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboPreferenceType, (bool) componentResourceManager.GetObject("cboPreferenceType.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.lbxFields, componentResourceManager.GetString("lbxFields.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.lbxFields, (HelpNavigator) componentResourceManager.GetObject("lbxFields.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.lbxFields, (bool) componentResourceManager.GetObject("lbxFields.ShowHelp"));
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (TDShipAlertPreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ResumeLayout(false);
    }
  }
}
