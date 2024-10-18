// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.TDFieldPreferences
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
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
  public class TDFieldPreferences : FieldPreferences
  {
    private bool _bRefreshingList;
    private IContainer components;

    private bool GPR3
    {
      get
      {
        return GuiData.CurrentAccount != null && GuiData.CurrentAccount.is_GPR3_SVCOPTION_OFFERING_Initiative_Enabled;
      }
    }

    public TDFieldPreferences() => this.InitializeComponent();

    protected override void RefreshList()
    {
      if (this.PreferenceObject == null)
        return;
      this._bRefreshingList = true;
      this.edtConstant.Text = "";
      this.cboConstant.DataSourceQ = (object) null;
      FieldPref.FieldPreferenceType fieldPreferenceType = FieldPref.FieldPreferenceType.Outbound;
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Index", typeof (int));
      dataTable.Columns.Add("Description");
      foreach (FieldPref fieldPref in this.PreferenceObject.FieldPrefs)
      {
        if (fieldPref.FieldPrefType == fieldPreferenceType && (fieldPreferenceType != FieldPref.FieldPreferenceType.Outbound || (this.PreferenceObject.VendorCode != (short) 2 || GuiData.CurrentAccount.IsGroundEnabled) && fieldPref.Index != 45 && fieldPref.Index != 44 && fieldPref.Index != 43 && fieldPref.Index != 51 && fieldPref.Index != 52 && fieldPref.Index != 51 && fieldPref.Index != 46 && fieldPref.Index != 47 && (!(GuiData.CurrentAccount.Address.CountryCode != "CA") || !(GuiData.CurrentAccount.Address.CountryCode != "US") || !(GuiData.CurrentAccount.Address.CountryCode != "MX") || fieldPref.Index != 2) && (!(GuiData.CurrentAccount.Address.CountryCode == "CA") || fieldPref.Index != 47)))
        {
          DataRow row = dataTable.NewRow();
          row["Index"] = (object) fieldPref.Index;
          row["Description"] = (object) GuiData.Languafier.Translate("FIELDPREF_T_" + fieldPref.Index.ToString());
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
        switch ((TDShipDefl.FieldPreference) fp.Index)
        {
          case TDShipDefl.FieldPreference.TotalPackages:
            str = "####";
            break;
          case TDShipDefl.FieldPreference.Weight:
            str = "#####.##";
            break;
          case TDShipDefl.FieldPreference.CarriageValue:
            str = "######";
            break;
          case TDShipDefl.FieldPreference.BillTransCharges3rdPartyAcctNbr:
          case TDShipDefl.FieldPreference.BillDutyTaxFees3rdPartyAcctNbr:
          case TDShipDefl.FieldPreference.FedExLineHaul3rdPartyAcctNbr:
            str = "999999999";
            break;
          case TDShipDefl.FieldPreference.References:
            ((FdxMaskedEdit) c).SetMask('I', 39);
            break;
          case TDShipDefl.FieldPreference.DepartmentNotes:
            str = "IIIIIIIIIIIIIIIIIIIIIIIII";
            break;
          case TDShipDefl.FieldPreference.NumberOfCommercialInvoiceCopies:
            this.edtConstant.Visible = false;
            this.spnConstant.Show();
            this.spnConstant.Minimum = 0M;
            break;
          case TDShipDefl.FieldPreference.AdditionalReference1:
          case TDShipDefl.FieldPreference.AdditionalReference2:
          case TDShipDefl.FieldPreference.AdditionalReference3:
            ((FdxMaskedEdit) c).SetMask('I', 30);
            break;
          case TDShipDefl.FieldPreference.EmergencyPhoneNbr:
            mask = eMasks.maskIntlPhoneExt;
            break;
          case TDShipDefl.FieldPreference.ShipDate:
            Utility.SetDateMasks(c as FdxMaskedEdit);
            break;
          case TDShipDefl.FieldPreference.SCACCode:
            ((FdxMaskedEdit) c).SetMask('I', 4);
            break;
          case TDShipDefl.FieldPreference.ExportPermitNbr:
            ((FdxMaskedEdit) c).SetMask('I', 10);
            break;
          case TDShipDefl.FieldPreference.TermsOfSaleValue:
            ((FdxMaskedEdit) c).SetMask('a', 3);
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
      output1.Columns.Add("Code");
      output1.Columns.Add("CodeDescription");
      this.cboConstant.DataSourceQ = (object) null;
      switch ((TDShipDefl.FieldPreference) fp.Index)
      {
        case TDShipDefl.FieldPreference.WeightType:
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
        case TDShipDefl.FieldPreference.Service:
          SvcListResponse serviceList1 = GuiData.AppController.ShipEngine.GetServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.TDService);
          if (serviceList1 == null)
            break;
          DataTable serviceListDataTable = Utility.GetServiceListDataTable(serviceList1.SvcList);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) serviceListDataTable;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.PackageType:
        case TDShipDefl.FieldPreference.GroundPackageType:
          SvcListResponse serviceList2 = GuiData.AppController.ShipEngine.GetServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.TDService);
          if (serviceList2 == null || serviceList2.SvcList == null || serviceList2.SvcList.SvcArray == null)
            break;
          bool bAddYourPackagingIfNoData = fp.Index == 30;
          for (int index = serviceList2.SvcList.SvcArray.Count - 1; index >= 0; --index)
          {
            if (!bAddYourPackagingIfNoData && Svc.IsGroundService(serviceList2.SvcList.SvcArray[index].ServiceCode) || bAddYourPackagingIfNoData && Svc.IsExpressService(serviceList2.SvcList.SvcArray[index].ServiceCode))
              serviceList2.SvcList.SvcArray.RemoveAt(index);
          }
          string valueColumnName = "Code";
          string displayColumnName = "Description";
          DataTable consolidatedPackageList = Utility.GetConsolidatedPackageList(GuiData.CurrentAccount, serviceList2.SvcList, valueColumnName, displayColumnName, "SortOrder", bAddYourPackagingIfNoData);
          this.cboConstant.ValueMemberQ = valueColumnName;
          this.cboConstant.DisplayMemberQ = displayColumnName;
          this.cboConstant.DataSourceQ = (object) consolidatedPackageList;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.PackageSize:
          if (GuiData.AppController.ShipEngine.GetDataList(ListSpecification.Dimension_DropDown, out output1, new Error()) == 1)
          {
            output1.Columns.Add(new DataColumn("CodeDescription", typeof (string), "Code + ' - ' + Description"));
            output1.DefaultView.Sort = "Code ASC";
          }
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.DataSourceQ = (object) output1;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.BillTransCharges:
        case TDShipDefl.FieldPreference.GroundPaymentType:
          DataTable dataTable1 = Utility.GetDataTable(Utility.ListTypes.PaymentTypes);
          dataTable1.DefaultView.RowFilter = "Code <> 4";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) dataTable1;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.BillDutyTaxFees:
        case TDShipDefl.FieldPreference.FedExLineHaulPaymentType:
        case TDShipDefl.FieldPreference.BrokerTypeFTNBillFeesTo:
          DataTable dataTable2 = Utility.GetDataTable(Utility.ListTypes.PaymentTypes);
          dataTable2.DefaultView.RowFilter = "Code <> 2 AND Code <> 4";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) dataTable2;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.DepartmentNotes:
          DataTable output2 = (DataTable) null;
          GuiData.AppController.ShipEngine.GetDataList(ListSpecification.Department_DropDown, out output2, new Error());
          if (output2 != null)
            output2.DefaultView.Sort = "Code ASC";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) output2;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          this.edtConstant.Text = "";
          break;
        case TDShipDefl.FieldPreference.SpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.TDSpecialService).SpecialServices, this.GPR3));
          break;
        case TDShipDefl.FieldPreference.TermsOfSale:
          DataTable dataTable3 = Utility.GetDataTable(Utility.ListTypes.TermsOfSale);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.DataSourceQ = (object) dataTable3;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.Purpose:
          DataTable dataTable4 = Utility.GetDataTable(Utility.ListTypes.Purposes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable4;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.FreightCharges:
        case TDShipDefl.FieldPreference.FedExFreightChargeType:
        case TDShipDefl.FieldPreference.GroundChargeType:
          DataTable dataTable5 = Utility.GetDataTable(Utility.ListTypes.FreightChargeTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.DataSourceQ = (object) dataTable5;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.MiscChargesCategory:
          DataTable dataTable6 = Utility.GetDataTable(Utility.ListTypes.MiscellaneousChargeTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.DataSourceQ = (object) dataTable6;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.Currency:
          Utility.PopulateCurrencyCombo(new ComboBox[1]
          {
            (ComboBox) this.cboConstant
          }, true);
          break;
        case TDShipDefl.FieldPreference.CountryOfManufacture:
          Utility.PopulateCountryCombo(new ComboBox[1]
          {
            (ComboBox) this.cboConstant
          }, Utility.CountryComboType.CountryOfManufacture);
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.PIBCode:
          DataTable dataTable7 = Utility.GetDataTable(Utility.ListTypes.DocumentDescriptions);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable7;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.GroundSpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.TDGroundSpecialService).SpecialServices, this.GPR3));
          break;
        case TDShipDefl.FieldPreference.DangerousGoodsRegulations:
          DataTable dataTable8 = Utility.GetDataTable(Utility.ListTypes.DgRegulationTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable8;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.SignatureOptions:
          DataTable dataTable9 = Utility.GetDataTable(Utility.ListTypes.SignatureOptions);
          if (dataTable9.Rows.Count > 0)
          {
            GuiData.Languafier.Translate(dataTable9, "Description", "Description");
            dataTable9.DefaultView.Sort = "Code ASC";
          }
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable9;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.BrokerType:
          DataTable dataTable10 = Utility.GetDataTable(Utility.ListTypes.TdBrokerTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable10;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.BrokerTypeOtherBrokerID:
          Utility.PopulateBrokerComboBoxes(new ComboBox[1]
          {
            (ComboBox) this.cboConstant
          });
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.ReturnTo:
          GuiData.AppController.ShipEngine.GetDataList(ListSpecification.Sender_List, out output1, new Error());
          if (output1 != null)
          {
            output1.Columns.Add(new DataColumn("CodeDescription", typeof (string), "IIF(SenderCode <> '',SenderCode + ' - ' + SenderName, 'Current sender')"));
            output1.DefaultView.Sort = "SenderCode ASC";
            this.cboConstant.ValueMemberQ = "SenderCode";
            this.cboConstant.DisplayMemberQ = "CodeDescription";
            this.cboConstant.DataSourceQ = (object) output1;
            this.cboConstant.SelectedIndexQ = -1;
            this.cboConstant.SelectedIndexQ = -1;
            break;
          }
          this.cboConstant.DataSource = (object) null;
          break;
        case TDShipDefl.FieldPreference.ImporterID:
        case TDShipDefl.FieldPreference.SoldTo:
          GuiData.AppController.ShipEngine.GetDataList(ListSpecification.RecipList_DropDown, out output1, new Error());
          if (output1 != null)
          {
            output1.Columns.Add(new DataColumn("CodeDescription", typeof (string), "IIF(RecipientCode <> '',RecipientCode + ' - ' + RecipientName, 'Current sender')"));
            output1.DefaultView.Sort = "RecipientCode ASC";
            this.cboConstant.ValueMemberQ = "RecipientCode";
            this.cboConstant.DisplayMemberQ = "CodeDescription";
            this.cboConstant.DataSourceQ = (object) output1;
            this.cboConstant.SelectedIndexQ = -1;
            this.cboConstant.SelectedIndexQ = -1;
            break;
          }
          this.cboConstant.DataSource = (object) null;
          break;
        case TDShipDefl.FieldPreference.HomeDeliverySpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.TDHomeDeliverySpecialservice).SpecialServices, this.GPR3));
          break;
        case TDShipDefl.FieldPreference.ACEPortalProvider:
          DataTable dataTable11 = Utility.GetDataTable(Utility.ListTypes.IDDAcePortalProviders);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable11;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case TDShipDefl.FieldPreference.IDDCustomsSubmission:
          DataTable dataTable12 = Utility.GetDataTable(Utility.ListTypes.IDDCustomsSubmissionTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable12;
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
        case 2:
          this.cboConstant.SelectedItem = (object) fp.StringFieldDeflVal;
          break;
        case 3:
          if (string.IsNullOrEmpty(fp.StringFieldDeflVal))
            break;
          Utility.SetServicePrefValue((ComboBox) this.cboConstant, fp.StringFieldDeflVal);
          break;
        case 4:
        case 5:
        case 30:
          if (string.IsNullOrEmpty(fp.StringFieldDeflVal))
            break;
          if (fp.StringFieldDeflVal.Length > 1 && fp.StringFieldDeflVal.Contains(" "))
          {
            this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal.Substring(0, fp.StringFieldDeflVal.IndexOf(' '));
            break;
          }
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal;
          break;
        case 7:
        case 9:
        case 15:
        case 18:
        case 22:
        case 27:
        case 28:
        case 29:
        case 31:
        case 49:
        case 50:
        case 53:
        case 64:
        case 66:
        case 67:
        case 68:
        case 76:
        case 77:
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal;
          break;
        case 12:
          this.edtConstant.Text = fp.StringFieldDeflVal;
          break;
        case 13:
          if (fp.StringFieldDeflVal.Length <= 1 || !fp.StringFieldDeflVal.Contains("\t"))
            break;
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal.Substring(0, fp.StringFieldDeflVal.IndexOf("\t"));
          this.edtConstant.Text = fp.StringFieldDeflVal.Substring(fp.StringFieldDeflVal.IndexOf("\t") + 1);
          break;
        case 14:
        case 32:
        case 69:
          fp.DefaultValueType = FieldPref.PreferenceValueType.StringList;
          if (fp.StringListFieldDeflVal == null)
            break;
          this.SetSpecialServiceListBoxItems(fp.StringListFieldDeflVal);
          break;
        case 23:
          Decimal result;
          Decimal.TryParse(fp.StringFieldDeflVal, out result);
          if (!(result >= 0M))
            break;
          this.spnConstant.Value = result;
          break;
        case 47:
        case 58:
          this.cboConstant.SelectedValue = (object) fp.IntFieldDeflVal.ToString();
          break;
        case 48:
        case 56:
        case 60:
        case 61:
        case 71:
        case 72:
        case 73:
        case 74:
        case 75:
          this.rdbConstantYes.Checked = fp.IntFieldDeflVal == 1;
          this.rdbConstantNo.Checked = fp.IntFieldDeflVal == 0;
          break;
        case 63:
          this.cboConstant.SelectedValueQ = (object) fp.IntFieldDeflVal;
          break;
        case 65:
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal;
          break;
        default:
          if (fp.DefaultValueType == FieldPref.PreferenceValueType.String && this.edtConstant.Visible)
          {
            this.edtConstant.Text = fp.StringFieldDeflVal;
            break;
          }
          if (fp.DefaultValueType != FieldPref.PreferenceValueType.Integer || !this.cboConstant.Visible)
            break;
          this.cboConstant.SelectedValue = (object) fp.IntFieldDeflVal.ToString();
          break;
      }
    }

    private bool SaveConstantValue(Control c, FieldPref fp)
    {
      if (c == null || fp == null)
        return false;
      bool flag1 = true;
      switch (fp.Control)
      {
        case FieldPref.ControlType.NoControl:
          fp.DefaultValueType = FieldPref.PreferenceValueType.None;
          break;
        case FieldPref.ControlType.DropDownCombo:
          switch ((TDShipDefl.FieldPreference) fp.Index)
          {
            case TDShipDefl.FieldPreference.Service:
              fp.StringFieldDeflVal = Utility.GetServicePrefValue((ComboBox) c);
              break;
            case TDShipDefl.FieldPreference.PackageType:
            case TDShipDefl.FieldPreference.PackageSize:
            case TDShipDefl.FieldPreference.BillTransCharges:
            case TDShipDefl.FieldPreference.BillDutyTaxFees:
            case TDShipDefl.FieldPreference.TermsOfSale:
            case TDShipDefl.FieldPreference.Purpose:
            case TDShipDefl.FieldPreference.MiscChargesCategory:
            case TDShipDefl.FieldPreference.Currency:
            case TDShipDefl.FieldPreference.CountryOfManufacture:
            case TDShipDefl.FieldPreference.PIBCode:
            case TDShipDefl.FieldPreference.GroundPackageType:
            case TDShipDefl.FieldPreference.GroundPaymentType:
            case TDShipDefl.FieldPreference.FedExFreightChargeType:
            case TDShipDefl.FieldPreference.GroundChargeType:
            case TDShipDefl.FieldPreference.FedExLineHaulPaymentType:
            case TDShipDefl.FieldPreference.BrokerTypeFTNBillFeesTo:
            case TDShipDefl.FieldPreference.BrokerTypeOtherBrokerID:
            case TDShipDefl.FieldPreference.ReturnTo:
            case TDShipDefl.FieldPreference.ImporterID:
            case TDShipDefl.FieldPreference.SoldTo:
            case TDShipDefl.FieldPreference.ACEPortalProvider:
            case TDShipDefl.FieldPreference.IDDCustomsSubmission:
              fp.StringFieldDeflVal = ((ComboBoxEx) c).SelectedValue as string;
              break;
            case TDShipDefl.FieldPreference.DangerousGoodsRegulations:
            case TDShipDefl.FieldPreference.SignatureOptions:
            case TDShipDefl.FieldPreference.BrokerType:
              int result = -1;
              int.TryParse(((ComboBoxEx) c).SelectedValue.ToString(), out result);
              fp.IntFieldDeflVal = result;
              if (fp.IntFieldDeflVal == -1)
              {
                flag1 = false;
                break;
              }
              break;
            default:
              fp.DefaultValueType = FieldPref.PreferenceValueType.String;
              fp.StringFieldDeflVal = c.Text;
              break;
          }
          if (fp.DefaultValueType == FieldPref.PreferenceValueType.String && string.IsNullOrEmpty(fp.StringFieldDeflVal))
          {
            flag1 = false;
            break;
          }
          if (fp.DefaultValueType == FieldPref.PreferenceValueType.Integer && fp.IntFieldDeflVal < 0)
          {
            flag1 = false;
            break;
          }
          break;
        case FieldPref.ControlType.TextBox:
        case FieldPref.ControlType.TextBoxWithBrowse:
          fp.DefaultValueType = FieldPref.PreferenceValueType.String;
          switch ((TDShipDefl.FieldPreference) fp.Index)
          {
            case TDShipDefl.FieldPreference.NumberOfCommercialInvoiceCopies:
              fp.DefaultValueType = FieldPref.PreferenceValueType.String;
              fp.StringFieldDeflVal = this.spnConstant.Value.ToString();
              break;
            case TDShipDefl.FieldPreference.EmergencyPhoneNbr:
              fp.StringFieldDeflVal = ((FdxMaskedEdit) c).Raw;
              break;
            case TDShipDefl.FieldPreference.TermsOfSaleValue:
              if (c.Text.Length != 3)
              {
                Utility.DisplayError(new Error(28010));
                flag1 = false;
                break;
              }
              fp.DefaultValueType = FieldPref.PreferenceValueType.String;
              fp.StringFieldDeflVal = c.Text;
              break;
            default:
              fp.StringFieldDeflVal = c.Text;
              break;
          }
          if (string.IsNullOrEmpty(fp.StringFieldDeflVal))
          {
            flag1 = false;
            break;
          }
          break;
        case FieldPref.ControlType.RadioButtons:
          fp.IntFieldDeflVal = ((RadioButton) c).Checked ? 1 : 0;
          break;
        case FieldPref.ControlType.MultiSelectListBox:
          switch ((TDShipDefl.FieldPreference) fp.Index)
          {
            case TDShipDefl.FieldPreference.SpecialServices:
            case TDShipDefl.FieldPreference.GroundSpecialServices:
            case TDShipDefl.FieldPreference.HomeDeliverySpecialServices:
              if (this.lbxConstant.SelectedItems.Count == 0)
              {
                int num = (int) Utility.DisplayError(GuiData.Languafier.Translate("InvalidConstant"), Error.ErrorType.Failure);
                flag1 = false;
                break;
              }
              if (fp.Index == 14)
              {
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                bool flag5 = false;
                bool flag6 = false;
                bool flag7 = false;
                foreach (DataRowView selectedItem in this.lbxConstant.SelectedItems)
                {
                  switch ((SplSvc.SpecialServiceType) Enum.Parse(typeof (SplSvc.SpecialServiceType), (string) selectedItem[this.lbxConstant.ValueMember]))
                  {
                    case SplSvc.SpecialServiceType.HoldAtLocation:
                      flag6 = true;
                      continue;
                    case SplSvc.SpecialServiceType.DangerousGoods:
                      flag4 = true;
                      continue;
                    case SplSvc.SpecialServiceType.DryIceOnly:
                      flag5 = true;
                      continue;
                    case SplSvc.SpecialServiceType.PriorityAlert:
                      flag2 = true;
                      continue;
                    case SplSvc.SpecialServiceType.COD:
                      flag3 = true;
                      continue;
                    case SplSvc.SpecialServiceType.PharmacyDelivery:
                      flag7 = true;
                      continue;
                    default:
                      continue;
                  }
                }
                if (flag6 & flag7)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.PharmacyDelivery.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HoldAtLocation.ToString()));
                  int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag2 & flag3)
                {
                  int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(50562), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag4 & flag5)
                {
                  int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9528), Error.ErrorType.Failure);
                  flag1 = false;
                }
              }
              else if (fp.Index == 32)
              {
                bool flag8 = false;
                bool flag9 = false;
                foreach (DataRowView selectedItem in this.lbxConstant.SelectedItems)
                {
                  switch ((SplSvc.SpecialServiceType) Enum.Parse(typeof (SplSvc.SpecialServiceType), (string) selectedItem[this.lbxConstant.ValueMember]))
                  {
                    case SplSvc.SpecialServiceType.GroundSelectDayDelivery:
                      flag8 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundAppointmentDelivery:
                      flag9 = true;
                      continue;
                    default:
                      continue;
                  }
                }
                if (flag8 & flag9)
                {
                  int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(38300), Error.ErrorType.Failure);
                  flag1 = false;
                }
              }
              else if (fp.Index == 69)
              {
                bool flag10 = false;
                bool flag11 = false;
                bool flag12 = false;
                foreach (DataRowView selectedItem in this.lbxConstant.SelectedItems)
                {
                  switch ((SplSvc.SpecialServiceType) Enum.Parse(typeof (SplSvc.SpecialServiceType), (string) selectedItem[this.lbxConstant.ValueMember]))
                  {
                    case SplSvc.SpecialServiceType.GroundSelectDayDelivery:
                      flag10 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundEveningDelivery:
                      flag12 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundAppointmentDelivery:
                      flag11 = true;
                      continue;
                    default:
                      continue;
                  }
                }
                if (flag12 & flag11)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundAppointmentDelivery.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundEveningDelivery.ToString()));
                  int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag12 & flag10)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundSelectDayDelivery.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundEveningDelivery.ToString()));
                  int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag10 & flag11)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundSelectDayDelivery.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundAppointmentDelivery.ToString()));
                  int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
              }
              if (flag1)
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
          break;
        case FieldPref.ControlType.DropDownComboAndTextBox:
          if (fp.Index == 13)
          {
            fp.StringFieldDeflVal = ((ComboBoxEx) c).SelectedValue.ToString() + "\t" + this.edtConstant.Text;
            break;
          }
          break;
      }
      return flag1;
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
              switch ((TDShipDefl.FieldPreference) fieldPref.Index)
              {
                case TDShipDefl.FieldPreference.NumberOfCommercialInvoiceCopies:
                  if (this.spnConstant.Value < 0M)
                  {
                    flag = false;
                    break;
                  }
                  break;
                case TDShipDefl.FieldPreference.TermsOfSaleValue:
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
            if (!flag && fieldPref.Index != 14 && fieldPref.Index != 32 && fieldPref.Index != 69 && fieldPref.Index != 70)
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
            stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(9519), (object) GuiData.Languafier.Translate("FIELDPREF_T_" + fieldPref.Index.ToString()));
            int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
            control.Focus();
          }
        }
      }
      return flag;
    }

    private void DomFieldPreferences_Load(object sender, EventArgs e)
    {
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TDFieldPreferences));
      this.spnConstant.BeginInit();
      this.gbxOtherPrefs.SuspendLayout();
      this.gbxStartPosition.SuspendLayout();
      this.SuspendLayout();
      this.helpProvider1.SetHelpKeyword((Control) this.lbxFields, componentResourceManager.GetString("lbxFields.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.lbxFields, (HelpNavigator) componentResourceManager.GetObject("lbxFields.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.lbxFields, (bool) componentResourceManager.GetObject("lbxFields.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbConstantNo, componentResourceManager.GetString("rdbConstantNo.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbConstantNo, (HelpNavigator) componentResourceManager.GetObject("rdbConstantNo.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdbConstantNo, (bool) componentResourceManager.GetObject("rdbConstantNo.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbConstantYes, componentResourceManager.GetString("rdbConstantYes.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbConstantYes, (HelpNavigator) componentResourceManager.GetObject("rdbConstantYes.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdbConstantYes, (bool) componentResourceManager.GetObject("rdbConstantYes.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.edtConstant, componentResourceManager.GetString("edtConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtConstant, (HelpNavigator) componentResourceManager.GetObject("edtConstant.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.edtConstant, (bool) componentResourceManager.GetObject("edtConstant.ShowHelp"));
      this.cboConstant.DropDownWidth = 300;
      this.helpProvider1.SetHelpKeyword((Control) this.cboConstant, componentResourceManager.GetString("cboConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboConstant, (HelpNavigator) componentResourceManager.GetObject("cboConstant.HelpNavigator"));
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
      this.helpProvider1.SetShowHelp((Control) this.lblReturnRecip, (bool) componentResourceManager.GetObject("lblReturnRecip.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblReturnRecip, "lblReturnRecip");
      this.helpProvider1.SetHelpKeyword((Control) this.cboReturnRecip, componentResourceManager.GetString("cboReturnRecip.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboReturnRecip, (HelpNavigator) componentResourceManager.GetObject("cboReturnRecip.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboReturnRecip, (bool) componentResourceManager.GetObject("cboReturnRecip.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.cboReturnRecip, "cboReturnRecip");
      this.helpProvider1.SetShowHelp((Control) this.label2, (bool) componentResourceManager.GetObject("label2.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.helpProvider1.SetHelpKeyword((Control) this.cboDIACode, componentResourceManager.GetString("cboDIACode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboDIACode, (HelpNavigator) componentResourceManager.GetObject("cboDIACode.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboDIACode, (bool) componentResourceManager.GetObject("cboDIACode.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.cboDIACode, "cboDIACode");
      this.helpProvider1.SetShowHelp((Control) this.label1, (bool) componentResourceManager.GetObject("label1.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkAlwaysUseReturnToCode, componentResourceManager.GetString("chkAlwaysUseReturnToCode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkAlwaysUseReturnToCode, (HelpNavigator) componentResourceManager.GetObject("chkAlwaysUseReturnToCode.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chkAlwaysUseReturnToCode, (bool) componentResourceManager.GetObject("chkAlwaysUseReturnToCode.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.chkAlwaysUseReturnToCode, "chkAlwaysUseReturnToCode");
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (TDFieldPreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.spnConstant.EndInit();
      this.gbxOtherPrefs.ResumeLayout(false);
      this.gbxStartPosition.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
