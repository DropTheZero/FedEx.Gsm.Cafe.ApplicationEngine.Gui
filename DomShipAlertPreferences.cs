// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.DomShipAlertPreferences
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class DomShipAlertPreferences : ShipAlertPreferences
  {
    private bool _bRefreshingList;
    private IContainer components;

    public DomShipAlertPreferences() => this.InitializeComponent();

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
              if (emailFieldPref.Index == 94 || emailFieldPref.Index == 95 || emailFieldPref.Index == 96 || emailFieldPref.Index == 97 || emailFieldPref.Index == 98 || emailFieldPref.Index == 99 || emailFieldPref.Index == 100 || emailFieldPref.Index == 101 || emailFieldPref.Index == 102 || emailFieldPref.Index == 103 || emailFieldPref.Index == 104 || emailFieldPref.Index == 105)
              {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(9519), (object) GuiData.Languafier.Translate("EMAILFIELDPREF_D_" + emailFieldPref.Index.ToString()));
                int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
              }
              else
              {
                int num1 = (int) Utility.DisplayError(GuiData.Languafier.Translate("InvalidConstant"), Error.ErrorType.Failure);
              }
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
          if (fp.DefaultValueType == FieldPref.PreferenceValueType.String)
            fp.StringFieldDeflVal = this.cboConstant.SelectedValueQ as string;
          else
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
        if (emailNotifPref.FieldPrefType == fieldPreferenceType && emailNotifPref.FieldDescription != "DUMMY" && (emailNotifPref.VendorCode != (short) 6 || GuiData.AppController.ShipEngine.IsAnyMeterSmartPostEnabled()) && (this.PreferenceObject.VendorCode != (short) 2 && emailNotifPref.VendorCode != (short) 2 || GuiData.CurrentAccount.IsGroundEnabled && !(GuiData.CurrentAccount.Address.CountryCode == "MX")) && emailNotifPref.Index != 19 && emailNotifPref.Index != 10 && emailNotifPref.Index != 9 && emailNotifPref.Index != 8 && emailNotifPref.Index != 52 && emailNotifPref.Index != 54 && emailNotifPref.Index != 110 && emailNotifPref.Index != 115 && emailNotifPref.Index != 120 && emailNotifPref.Index != 125 && emailNotifPref.Index != 126 && emailNotifPref.Index != 131 && (emailNotifPref.Index != 107 && emailNotifPref.Index != 106 && emailNotifPref.Index != 108 && emailNotifPref.Index != 109 && emailNotifPref.Index != 113 && emailNotifPref.Index != 114 && emailNotifPref.Index != 112 && emailNotifPref.Index != 111 && emailNotifPref.Index != 118 && emailNotifPref.Index != 119 && emailNotifPref.Index != 117 && emailNotifPref.Index != 116 && emailNotifPref.Index != 123 && emailNotifPref.Index != 124 && emailNotifPref.Index != 122 && emailNotifPref.Index != 121 || GuiData.CurrentAccount.SHIPDATE_Noti_Estimated_Initiative_Enabled) && (emailNotifPref.Index != 146 && emailNotifPref.Index != 147 && emailNotifPref.Index != 145 && emailNotifPref.Index != 144 && emailNotifPref.Index != 142 && emailNotifPref.Index != 143 && emailNotifPref.Index != 141 && emailNotifPref.Index != 140 && emailNotifPref.Index != 138 && emailNotifPref.Index != 139 && emailNotifPref.Index != 137 && emailNotifPref.Index != 136 && emailNotifPref.Index != 158 && emailNotifPref.Index != 159 && emailNotifPref.Index != 157 && emailNotifPref.Index != 156 && emailNotifPref.Index != 154 && emailNotifPref.Index != 155 && emailNotifPref.Index != 153 && emailNotifPref.Index != 152 && emailNotifPref.Index != 150 && emailNotifPref.Index != 151 && emailNotifPref.Index != 149 && emailNotifPref.Index != 148 || GuiData.CurrentAccount.SHIPDATE_NOTI_DATE_Initiative_Enabled))
        {
          switch (fieldPreferenceType)
          {
            case FieldPref.FieldPreferenceType.MPS:
            case FieldPref.FieldPreferenceType.ReturnsMPS:
              continue;
            default:
              DataRow row = dataTable.NewRow();
              row["Index"] = (object) emailNotifPref.Index;
              row["Description"] = (object) GuiData.Languafier.Translate("EMAILFIELDPREF_D_" + emailNotifPref.Index.ToString());
              dataTable.Rows.Add(row);
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
      switch ((DShipDefl.EmailFieldPreference) fp.Index)
      {
        case DShipDefl.EmailFieldPreference.EmailFormatType:
        case DShipDefl.EmailFieldPreference.ReturnEmailFormatType:
        case DShipDefl.EmailFieldPreference.EmailFormatTypeBroker:
        case DShipDefl.EmailFieldPreference.EmailFormatTypeOther1:
        case DShipDefl.EmailFieldPreference.EmailFormatTypeOther2:
        case DShipDefl.EmailFieldPreference.EmailFormatTypeRecipent:
        case DShipDefl.EmailFieldPreference.EmailFormatTypeSender:
        case DShipDefl.EmailFieldPreference.ReturnEmailFormatTypeBroker:
        case DShipDefl.EmailFieldPreference.ReturnEmailFormatTypeOther1:
        case DShipDefl.EmailFieldPreference.ReturnEmailFormatTypeOther2:
        case DShipDefl.EmailFieldPreference.ReturnEmailFormatTypeRecipent:
        case DShipDefl.EmailFieldPreference.ReturnEmailFormatTypeSender:
          DataTable dataTable1 = Utility.GetDataTable(Utility.ListTypes.EmailFormatType);
          if (dataTable1 != null)
          {
            dataTable1.Rows.Remove(dataTable1.Select("Code = 2")[0]);
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          this.cboConstant.DataSourceQ = (object) dataTable1;
          break;
        case DShipDefl.EmailFieldPreference.ReturnMerchantNotifyEmailLang1:
        case DShipDefl.EmailFieldPreference.ReturnMerchantNotifyEmailLang2:
        case DShipDefl.EmailFieldPreference.ReturnMerchantNotifyEmailLang3:
        case DShipDefl.EmailFieldPreference.ReturnMerchantNotifyEmailLang4:
        case DShipDefl.EmailFieldPreference.ReturnMerchantNotifyEmailLang5:
        case DShipDefl.EmailFieldPreference.ReturnMerchantNotifyEmailLang6:
          DataTable dataTable2 = Utility.GetDataTable(Utility.ListTypes.ReturnAlertNotificationLanguage);
          if (dataTable2 != null)
          {
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          this.cboConstant.DataSourceQ = (object) dataTable2;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DomShipAlertPreferences));
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
      this.Name = nameof (DomShipAlertPreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ResumeLayout(false);
    }
  }
}
