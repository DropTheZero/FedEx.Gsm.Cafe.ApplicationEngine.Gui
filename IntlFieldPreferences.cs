// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.IntlFieldPreferences
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
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class IntlFieldPreferences : FieldPreferences
  {
    private bool _bRefreshingList;
    private bool _bResettingPrefType;
    private Account _account;
    private IContainer components;
    private ComboBoxEx cboPreferenceType;
    private Label grpPrefType;

    public IntlFieldPreferences() => this.InitializeComponent();

    internal Account CurrentAccount
    {
      get => this._account ?? GuiData.CurrentAccount;
      set => this._account = value;
    }

    private bool GPR3
    {
      get
      {
        return this.CurrentAccount != null && this.CurrentAccount.is_GPR3_SVCOPTION_OFFERING_Initiative_Enabled;
      }
    }

    protected override void RefreshList()
    {
      if (this.PreferenceObject == null)
        return;
      this._bRefreshingList = true;
      this.edtConstant.Text = "";
      this.cboConstant.DataSourceQ = (object) null;
      FieldPref.FieldPreferenceType fieldPreferenceType = (FieldPref.FieldPreferenceType) Enum.Parse(typeof (FieldPref.FieldPreferenceType), this.cboPreferenceType.SelectedValue.ToString());
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Index", typeof (int));
      dataTable.Columns.Add("Description");
      foreach (FieldPref fieldPref in this.PreferenceObject.FieldPrefs)
      {
        if (fieldPref.FieldPrefType == fieldPreferenceType)
        {
          switch (fieldPreferenceType)
          {
            case FieldPref.FieldPreferenceType.Outbound:
              if (fieldPref.Index == 51 || fieldPref.Index == 52 || fieldPref.Index == 36 || fieldPref.Index == 41 || fieldPref.Index == 35 || fieldPref.Index == 51 || fieldPref.Index == 43 || fieldPref.Index == 44 || fieldPref.Index == 45 || fieldPref.Index == 46 || fieldPref.Index == 78 || fieldPref.Index == 23 || fieldPref.Index == 47 || this.CurrentAccount.Address.CountryCode != "CA" && fieldPref.Index == 65 || this.CurrentAccount.Address.CountryCode != "CA" && this.CurrentAccount.Address.CountryCode != "US" && this.CurrentAccount.Address.CountryCode != "MX" && !Utility.IsLacCountry(this.CurrentAccount.Address.CountryCode) && fieldPref.Index == 2 || this.CurrentAccount.Address.CountryCode == "CA" && (fieldPref.Index == 47 || fieldPref.Index == 79) || this.CurrentAccount.Address.CountryCode != "CA" && this.CurrentAccount.Address.CountryCode != "US" && fieldPref.Index == 75 || !this.CurrentAccount.IsIpdEnabled && (fieldPref.Index == 84 || fieldPref.Index == 85) || !this.CurrentAccount.ETD_ENHANCE_POSTSHIP_UPLOAD_Initiative_Enabled && fieldPref.Index == 116 || fieldPref.Index == 97 || fieldPref.Index == 98 || this.CurrentAccount.is_ZENITH_CHANGES_INIT && (fieldPref.Index == 117 || fieldPref.Index == 131 || fieldPref.Index == 50 || fieldPref.Index == 118 || fieldPref.Index == 30 || fieldPref.Index == 31 || fieldPref.Index == 115 || fieldPref.Index == 32 || fieldPref.Index == 80))
                continue;
              break;
            case FieldPref.FieldPreferenceType.Returns:
              if (fieldPref.Index == 97 || fieldPref.Index == 98 || this.CurrentAccount.is_ZENITH_CHANGES_INIT && (fieldPref.Index == 119 || fieldPref.Index == 120 || fieldPref.Index == 89 || fieldPref.Index == 110))
                continue;
              break;
            case FieldPref.FieldPreferenceType.MPS:
              if (!this.CurrentAccount.IsIpdEnabled && (fieldPref.Index == 81 || fieldPref.Index == 82 || fieldPref.Index == 83) || this.CurrentAccount.is_ZENITH_CHANGES_INIT && (fieldPref.Index == 64 || fieldPref.Index == 66 || fieldPref.Index == 67))
                continue;
              break;
          }
          DataRow row = dataTable.NewRow();
          row["Index"] = (object) fieldPref.Index;
          row["Description"] = (object) GuiData.Languafier.Translate("FIELDPREF_I_" + fieldPref.Index.ToString());
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
        ((ComboBox) c).DropDownStyle = ComboBoxStyle.DropDown;
      else if (fp.Control == FieldPref.ControlType.TextBox || fp.Control == FieldPref.ControlType.TextBoxWithBrowse || fp.Control == FieldPref.ControlType.DropDownComboAndTextBox && c is TextBox)
      {
        string str = (string) null;
        eMasks mask = eMasks.maskNone;
        if (c is FdxMaskedEdit)
          ((FdxMaskedEdit) c).SetMask(mask);
        switch ((IShipDefl.FieldPreference) fp.Index)
        {
          case IShipDefl.FieldPreference.TotalPackages:
            str = "####";
            break;
          case IShipDefl.FieldPreference.Weight:
          case IShipDefl.FieldPreference.MPSWeight:
          case IShipDefl.FieldPreference.ReturnEstimatedWeight:
            str = "#####.##";
            break;
          case IShipDefl.FieldPreference.BillTransCharges3rdPartyAcctNbr:
          case IShipDefl.FieldPreference.BillDutyTaxFees3rdPartyAcctNbr:
          case IShipDefl.FieldPreference.Ground3rdPartyAcctNbr:
          case IShipDefl.FieldPreference.GroundBillRecipientAcctNbr:
          case IShipDefl.FieldPreference.ReturnBillTransCharges3rdPartyAcctNbr:
          case IShipDefl.FieldPreference.ReturnBillDutyTaxFees3rdPartyAcctNbr:
            str = "999999999";
            break;
          case IShipDefl.FieldPreference.References:
          case IShipDefl.FieldPreference.MPSReferences:
            ((FdxMaskedEdit) c).SetMask('I', 39);
            break;
          case IShipDefl.FieldPreference.DepartmentNotes:
          case IShipDefl.FieldPreference.MPSDepartmentNotes:
            str = "IIIIIIIIIIIIIIIIIIIIIIIII";
            break;
          case IShipDefl.FieldPreference.NumberOfCommercialInvoiceCopies:
            this.edtConstant.Visible = false;
            this.spnConstant.Show();
            break;
          case IShipDefl.FieldPreference.AdditionalReference1:
          case IShipDefl.FieldPreference.AdditionalReference2:
          case IShipDefl.FieldPreference.AdditionalReference3:
          case IShipDefl.FieldPreference.MPSAdditionalReference1:
          case IShipDefl.FieldPreference.MPSAdditionalReference2:
          case IShipDefl.FieldPreference.MPSAdditionalReference3:
          case IShipDefl.FieldPreference.ReturnCustomerReference:
            ((FdxMaskedEdit) c).SetMask('I', 30);
            break;
          case IShipDefl.FieldPreference.EmergencyPhoneNbr:
            ((FdxMaskedEdit) c).SetMask('9', 15);
            this.edtConstantExt.SetMask('9', 6);
            break;
          case IShipDefl.FieldPreference.ShipDate:
            Utility.SetDateMasks(c as FdxMaskedEdit);
            break;
          case IShipDefl.FieldPreference.TotalCustomsValueShipmentDetails:
            mask = eMasks.maskMoneySep;
            str = "###########0.00~$,<";
            break;
          case IShipDefl.FieldPreference.ReturnsPrintReturnMessage:
            ((FdxMaskedEdit) c).SetMask('I', 200);
            break;
          case IShipDefl.FieldPreference.TermsOfSaleValue:
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
      else
      {
        if (fp.Control != FieldPref.ControlType.RadioButtons)
          return;
        if (fp.Index == 78)
        {
          this.rdbConstantYes.Text = GuiData.Languafier.Translate("FieldPrefRadioButtonTextYesPrintCI");
          this.rdbConstantNo.Text = GuiData.Languafier.Translate("FieldPrefRadioButtonTextNoPrintCI");
        }
        else
        {
          this.rdbConstantYes.Text = GuiData.Languafier.Translate("FieldPrefRadioButtonTextYesGeneric");
          this.rdbConstantNo.Text = GuiData.Languafier.Translate("FieldPrefRadioButtonTextNoGeneric");
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
      switch ((IShipDefl.FieldPreference) fp.Index)
      {
        case IShipDefl.FieldPreference.WeightType:
        case IShipDefl.FieldPreference.ReturnWeightType:
          this.cboConstant.ValueMemberQ = "";
          this.cboConstant.DisplayMemberQ = "";
          this.cboConstant.DataSourceQ = (object) null;
          this.cboConstant.BeginUpdate();
          this.cboConstant.Items.Clear();
          foreach (string name in Enum.GetNames(typeof (WeightUnits)))
            this.cboConstant.Items.Add((object) name.ToLower());
          this.cboConstant.EndUpdate();
          break;
        case IShipDefl.FieldPreference.Service:
        case IShipDefl.FieldPreference.ReturnService:
          SvcListResponse serviceList1 = GuiData.AppController.ShipEngine.GetServiceList(this.CurrentAccount, FieldPref.PreferenceTypes.InternationalService);
          if (serviceList1 == null || serviceList1.SvcList == null)
            break;
          DataTable serviceListDataTable = Utility.GetServiceListDataTable(this.CurrentAccount, serviceList1.SvcList);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) serviceListDataTable;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.PackageType:
        case IShipDefl.FieldPreference.GroundPackageType:
        case IShipDefl.FieldPreference.ReturnExpressPackageType:
        case IShipDefl.FieldPreference.ReturnGroundPackageType:
          SvcListResponse serviceList2 = GuiData.AppController.ShipEngine.GetServiceList(this.CurrentAccount, FieldPref.PreferenceTypes.InternationalService);
          if (serviceList2 == null || serviceList2.SvcList == null)
            break;
          bool bAddYourPackagingIfNoData = fp.Index == 89 || fp.Index == 30;
          for (int index = serviceList2.SvcList.SvcArray.Count - 1; index >= 0; --index)
          {
            if (!bAddYourPackagingIfNoData && Svc.IsGroundService(serviceList2.SvcList.SvcArray[index].ServiceCode) || bAddYourPackagingIfNoData && Svc.IsExpressService(serviceList2.SvcList.SvcArray[index].ServiceCode))
              serviceList2.SvcList.SvcArray.RemoveAt(index);
          }
          string valueColumnName1 = "Code";
          string displayColumnName1 = "Description";
          DataTable consolidatedPackageList = Utility.GetConsolidatedPackageList(this.CurrentAccount, serviceList2.SvcList, valueColumnName1, displayColumnName1, "SortOrder", bAddYourPackagingIfNoData);
          this.cboConstant.ValueMemberQ = valueColumnName1;
          this.cboConstant.DisplayMemberQ = displayColumnName1;
          this.cboConstant.DataSourceQ = (object) consolidatedPackageList;
          if (bAddYourPackagingIfNoData && consolidatedPackageList.Rows.Count == 1)
          {
            this.cboConstant.SelectedIndexQ = 0;
            break;
          }
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.PackageSize:
        case IShipDefl.FieldPreference.MPSPackageSize:
          if (GuiData.AppController.ShipEngine.GetDataList(GsmDataAccess.ListSpecification.Dimension_DropDown, out output1, new Error()) == 1)
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
        case IShipDefl.FieldPreference.BillTransCharges:
        case IShipDefl.FieldPreference.BillDutyTaxFees:
          DataTable dataTable1 = Utility.GetDataTable(Utility.ListTypes.PaymentTypes);
          dataTable1.DefaultView.RowFilter = "Code <> 4";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) dataTable1;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.DepartmentNotes:
        case IShipDefl.FieldPreference.MPSDepartmentNotes:
          DataTable output2 = (DataTable) null;
          GuiData.AppController.ShipEngine.GetDataList(GsmDataAccess.ListSpecification.Department_DropDown, out output2, new Error());
          if (output2 != null)
            output2.DefaultView.Sort = "Code ASC";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) output2;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          this.edtConstant.Text = "";
          break;
        case IShipDefl.FieldPreference.SpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(this.CurrentAccount, FieldPref.PreferenceTypes.InternationalSpecialService).SpecialServices, this.GPR3));
          break;
        case IShipDefl.FieldPreference.TermsOfSale:
          DataTable dataTable2 = Utility.GetDataTable(Utility.ListTypes.TermsOfSale);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.DataSourceQ = (object) dataTable2;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.Purpose:
          DataTable dataTable3 = Utility.GetDataTable(Utility.ListTypes.Purposes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable3;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.MiscChargesCategory:
          DataTable dataTable4 = Utility.GetDataTable(Utility.ListTypes.MiscellaneousChargeTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.DataSourceQ = (object) dataTable4;
          break;
        case IShipDefl.FieldPreference.Currency:
        case IShipDefl.FieldPreference.GroundCurrencyDefault:
          Utility.PopulateCurrencyCombo(new ComboBox[1]
          {
            (ComboBox) this.cboConstant
          }, this.CurrentAccount.Address.CountryCode != "MX");
          break;
        case IShipDefl.FieldPreference.CountryOfManufacture:
        case IShipDefl.FieldPreference.MPSCountryOfManufacture:
          Utility.PopulateCountryCombo(new ComboBox[1]
          {
            (ComboBox) this.cboConstant
          }, Utility.CountryComboType.CountryOfManufacture);
          break;
        case IShipDefl.FieldPreference.PIBCode:
          DataTable dataTable5 = Utility.GetDataTable(Utility.ListTypes.DocumentDescriptions);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable5;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.GroundPaymentType:
          DataTable dataTable6 = Utility.GetDataTable(Utility.ListTypes.PaymentTypes);
          if (!this.CurrentAccount.CollectBillingOption)
            dataTable6.DefaultView.RowFilter = "Code <> 4";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) dataTable6;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.GroundSpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(this.CurrentAccount, FieldPref.PreferenceTypes.InternationalGroundSpecialService).SpecialServices, this.GPR3));
          break;
        case IShipDefl.FieldPreference.DangerousGoodsRegulations:
          DataTable dataTable7 = Utility.GetDataTable(Utility.ListTypes.DgRegulationTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable7;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.FedExFreightChargeType:
        case IShipDefl.FieldPreference.GroundChargeType:
          DataTable dataTable8 = Utility.GetDataTable(Utility.ListTypes.FreightChargeTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.DataSourceQ = (object) dataTable8;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.BrokerSelection:
          DataTable dataTable9 = Utility.GetDataTable(Utility.ListTypes.BrokerSelection);
          this.cboConstant.ValueMemberQ = "Description";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable9;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.B13AFilingOption:
          DataTable optionsDataTable = Utility.GetCanadaExportDeclarationFilingOptionsDataTable(this.CurrentAccount.Culture);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.DataSourceQ = (object) optionsDataTable;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.GroundMPSType:
          DataTable dataTable10 = Utility.GetDataTable(Utility.ListTypes.MpsTypesGround);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable10;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.ExpressMPSType:
          DataTable dataTable11 = Utility.GetDataTable(Utility.ListTypes.MpsTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable11;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.MPSGroundRateSelection:
        case IShipDefl.FieldPreference.ExpressRateSelection:
        case IShipDefl.FieldPreference.GroundRateSelection:
          DataTable dataTable12 = Utility.GetDataTable(Utility.ListTypes.MpsRateSelectionGround);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable12;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.MPSExpressRateSelection:
          DataTable dataTable13 = Utility.GetDataTable(Utility.ListTypes.MpsRateSelection);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable13;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.CommodityID:
        case IShipDefl.FieldPreference.MPSCommodityID:
          if (GuiData.AppController.ShipEngine.GetDataList(GsmDataAccess.ListSpecification.Commodity_DropDownFull, out output1, new Error()) == 1)
            output1.DefaultView.Sort = "Code ASC";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) output1;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.InternationalPackageType:
          List<Pkging> allPackageTypeList = GuiData.AppController.ShipEngine.GetAllPackageTypeList(GuiData.CurrentAccount);
          string valueColumnName2 = "Code";
          string displayColumnName2 = "Description";
          if (allPackageTypeList != null)
          {
            output1 = Utility.GetPackageList(allPackageTypeList, valueColumnName2, displayColumnName2);
            output1.DefaultView.RowFilter = valueColumnName2 + " <> '08' AND " + valueColumnName2 + " <> '01'";
          }
          this.cboConstant.BeginUpdate();
          this.cboConstant.DataSourceQ = (object) output1;
          this.cboConstant.ValueMemberQ = valueColumnName2;
          this.cboConstant.DisplayMemberQ = displayColumnName2;
          this.cboConstant.EndUpdate();
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.SignatureOptions:
        case IShipDefl.FieldPreference.ReturnSignatureOptions:
          DataTable dataTable14 = Utility.GetDataTable(Utility.ListTypes.SignatureOptions);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable14;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.DCSType:
          DataTable dataTable15 = Utility.GetDataTable(Utility.ListTypes.DcsTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable15;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.IPD_IDF_IOR:
          DataTable output3 = (DataTable) null;
          try
          {
            GuiData.AppController.ShipEngine.GetDataList((object) null, GsmDataAccess.ListSpecification.IPDIORList, out output3, (List<GsmFilter>) null, (List<GsmSort>) null, (List<string>) null, new Error());
            if (output3 == null)
              break;
            output3.Columns.Add(new DataColumn("CodeAndContact", typeof (string), "Code + ' - ' + Contact"));
            Utility.SetDisplayAndValue((ComboBox) this.cboConstant, output3, "CodeAndContact", "Code", true);
            break;
          }
          catch
          {
            break;
          }
        case IShipDefl.FieldPreference.ReturnDTFPaymentType:
        case IShipDefl.FieldPreference.ReturnPaymentType:
          DataTable dataTable16 = Utility.GetDataTable(Utility.ListTypes.PaymentTypes);
          dataTable16.DefaultView.RowFilter = "Code <> 2 AND Code <> 4";
          for (int index = dataTable16.Rows.Count - 1; index >= 0; --index)
          {
            string str = dataTable16.Rows[index].ItemArray[0] as string;
            if ("1" == str)
              dataTable16.Rows[index]["Description"] = (object) GuiData.Languafier.Translate("d28789");
            else if ("3" == str)
              dataTable16.Rows[index]["Description"] = (object) GuiData.Languafier.Translate("d28790");
            else
              dataTable16.Rows.RemoveAt(index);
          }
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) dataTable16;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.ReturnLabelType:
          DataTable dataTable17 = Utility.GetDataTable(Utility.ListTypes.ReturnLabelTypes);
          if (dataTable17 != null)
          {
            dataTable17.DefaultView.RowFilter = "Code = 2 OR Code = 1";
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          this.cboConstant.DataSourceQ = (object) dataTable17;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.NAFTARole:
          DataTable dataTable18 = Utility.GetDataTable(Utility.ListTypes.NaftaRoleOptions);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable18;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.ReturnReason:
          DataTable dataTable19 = Utility.GetDataTable(Utility.ListTypes.ReturnReasonTypeOutbound);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.DataSourceQ = (object) dataTable19;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.ReturnReasonForReturn:
          DataTable dataTable20 = Utility.GetDataTable(Utility.ListTypes.ReturnReasonType);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.DataSourceQ = (object) dataTable20;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.ReturnEmailExpirationDate:
          DataTable dataTable21 = Utility.GetDataTable(Utility.ListTypes.EmailExpirationDateOptions);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable21;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.ReturnExpressSpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(this.CurrentAccount, FieldPref.PreferenceTypes.ReturnInternationalSpecialService).SpecialServices, this.GPR3));
          break;
        case IShipDefl.FieldPreference.ReturnGroundSpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(this.CurrentAccount, FieldPref.PreferenceTypes.ReturnInternationalGroundSpecialService).SpecialServices, this.GPR3));
          break;
        case IShipDefl.FieldPreference.DistanceUnitOfMeasure:
        case IShipDefl.FieldPreference.ReturnDistanceUnitOfMeasure:
          DataTable dataTable22 = Utility.GetDataTable(Utility.ListTypes.DistanceUnitOfMeasure);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable22;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.GroundBuyer:
        case IShipDefl.FieldPreference.ReturnGroundBuyer:
          DataTable dataTable23 = Utility.GetDataTable(Utility.ListTypes.GroundBuyerSource);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable23;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case IShipDefl.FieldPreference.GroundImporterIfBrokerSelect:
        case IShipDefl.FieldPreference.ReturnGroundImporterIfBrokerSelect:
          DataTable dataTable24 = Utility.GetDataTable(Utility.ListTypes.GroundImporterSource);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable24;
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
        case 94:
          this.cboConstant.SelectedItem = (object) fp.StringFieldDeflVal;
          break;
        case 3:
        case 95:
          if (string.IsNullOrEmpty(fp.StringFieldDeflVal))
            break;
          Utility.SetServicePrefValue((ComboBox) this.cboConstant, fp.StringFieldDeflVal);
          break;
        case 4:
        case 5:
        case 15:
        case 29:
        case 30:
        case 49:
        case 50:
        case 58:
        case 88:
        case 89:
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
        case 22:
        case 27:
        case 28:
        case 79:
        case 83:
        case 99:
        case 100:
        case 101:
        case 105:
        case 112:
        case 113:
        case 117:
        case 118:
        case 119:
        case 120:
        case 131:
          this.cboConstant.SelectedValue = (object) fp.StringFieldDeflVal;
          break;
        case 12:
        case 59:
          this.edtConstant.Text = fp.StringFieldDeflVal;
          break;
        case 13:
        case 63:
          if (fp.StringFieldDeflVal.Length <= 1 || !fp.StringFieldDeflVal.Contains("\t"))
            break;
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal.Substring(0, fp.StringFieldDeflVal.IndexOf("\t"));
          this.edtConstant.Text = fp.StringFieldDeflVal.Substring(fp.StringFieldDeflVal.IndexOf("\t") + 1);
          break;
        case 14:
        case 32:
        case 109:
        case 110:
          if (fp.StringListFieldDeflVal == null)
            break;
          this.SetSpecialServiceListBoxItems(fp.StringListFieldDeflVal);
          break;
        case 18:
        case 54:
        case 55:
        case 72:
        case 75:
        case 82:
        case 85:
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal;
          break;
        case 23:
          Decimal result;
          Decimal.TryParse(fp.StringFieldDeflVal, out result);
          if (!(result > 0M))
            break;
          this.spnConstant.Value = result;
          break;
        case 31:
        case 90:
        case 91:
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal;
          break;
        case 40:
          if (fp.StringFieldDeflVal.Length > 1 && fp.StringFieldDeflVal.Contains("\t"))
          {
            this.edtConstant.Text = fp.StringFieldDeflVal.Substring(0, fp.StringFieldDeflVal.IndexOf("\t"));
            this.edtConstantExt.Text = fp.StringFieldDeflVal.Substring(fp.StringFieldDeflVal.IndexOf("\t") + 1);
            break;
          }
          this.edtConstant.Text = fp.StringFieldDeflVal;
          break;
        case 47:
        case 64:
        case 65:
        case 67:
        case 68:
        case 76:
        case 111:
        case 114:
        case 115:
          this.cboConstant.SelectedValue = (object) fp.IntFieldDeflVal.ToString();
          break;
        case 48:
        case 56:
        case 66:
        case 70:
        case 73:
        case 74:
        case 78:
        case 84:
        case 96:
        case 97:
        case 98:
        case 106:
        case 107:
        case 108:
        case 116:
        case 121:
        case 122:
        case 123:
        case 124:
        case 125:
        case 126:
        case (int) sbyte.MaxValue:
        case 128:
        case 129:
        case 130:
        case 132:
        case 133:
        case 134:
          if ((fp.Index == 84 || fp.Index == 97 || fp.Index == 98) && fp.Behavior == ShipDefl.Behavior.None)
          {
            this.rdbConstantYes.Checked = true;
            break;
          }
          this.rdbConstantYes.Checked = fp.IntFieldDeflVal == 1;
          this.rdbConstantNo.Checked = fp.IntFieldDeflVal == 0;
          break;
        case 80:
          this.edtConstant.Raw = fp.StringFieldDeflVal;
          break;
        case 92:
          this.cboConstant.SelectedValueQ = (object) fp.IntFieldDeflVal;
          break;
        default:
          if ((fp.DefaultValueType == FieldPref.PreferenceValueType.Float || fp.DefaultValueType == FieldPref.PreferenceValueType.String) && this.edtConstant.Visible)
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
          switch ((IShipDefl.FieldPreference) fp.Index)
          {
            case IShipDefl.FieldPreference.Service:
            case IShipDefl.FieldPreference.ReturnService:
              fp.StringFieldDeflVal = Utility.GetServicePrefValue((ComboBox) c);
              break;
            case IShipDefl.FieldPreference.PackageType:
            case IShipDefl.FieldPreference.PackageSize:
            case IShipDefl.FieldPreference.BillTransCharges:
            case IShipDefl.FieldPreference.BillDutyTaxFees:
            case IShipDefl.FieldPreference.TermsOfSale:
            case IShipDefl.FieldPreference.Purpose:
            case IShipDefl.FieldPreference.MiscChargesCategory:
            case IShipDefl.FieldPreference.Currency:
            case IShipDefl.FieldPreference.CountryOfManufacture:
            case IShipDefl.FieldPreference.PIBCode:
            case IShipDefl.FieldPreference.GroundPackageType:
            case IShipDefl.FieldPreference.GroundPaymentType:
            case IShipDefl.FieldPreference.FedExFreightChargeType:
            case IShipDefl.FieldPreference.GroundChargeType:
            case IShipDefl.FieldPreference.B13AFilingOption:
            case IShipDefl.FieldPreference.MPSPackageSize:
            case IShipDefl.FieldPreference.CommodityID:
            case IShipDefl.FieldPreference.InternationalPackageType:
            case IShipDefl.FieldPreference.DCSType:
            case IShipDefl.FieldPreference.MPSCommodityID:
            case IShipDefl.FieldPreference.MPSCountryOfManufacture:
            case IShipDefl.FieldPreference.IPD_IDF_IOR:
            case IShipDefl.FieldPreference.ReturnExpressPackageType:
            case IShipDefl.FieldPreference.ReturnGroundPackageType:
            case IShipDefl.FieldPreference.ReturnDTFPaymentType:
            case IShipDefl.FieldPreference.ReturnPaymentType:
            case IShipDefl.FieldPreference.NAFTARole:
            case IShipDefl.FieldPreference.ReturnReason:
            case IShipDefl.FieldPreference.ReturnReasonForReturn:
            case IShipDefl.FieldPreference.ReturnEmailExpirationDate:
            case IShipDefl.FieldPreference.DistanceUnitOfMeasure:
            case IShipDefl.FieldPreference.ReturnDistanceUnitOfMeasure:
            case IShipDefl.FieldPreference.GroundBuyer:
            case IShipDefl.FieldPreference.GroundImporterIfBrokerSelect:
            case IShipDefl.FieldPreference.ReturnGroundBuyer:
            case IShipDefl.FieldPreference.ReturnGroundImporterIfBrokerSelect:
            case IShipDefl.FieldPreference.GroundCurrencyDefault:
              fp.StringFieldDeflVal = ((ComboBoxEx) c).SelectedValue as string;
              break;
            case IShipDefl.FieldPreference.DangerousGoodsRegulations:
            case IShipDefl.FieldPreference.GroundMPSType:
            case IShipDefl.FieldPreference.ExpressMPSType:
            case IShipDefl.FieldPreference.MPSGroundRateSelection:
            case IShipDefl.FieldPreference.MPSExpressRateSelection:
            case IShipDefl.FieldPreference.SignatureOptions:
            case IShipDefl.FieldPreference.ReturnLabelType:
            case IShipDefl.FieldPreference.ReturnSignatureOptions:
            case IShipDefl.FieldPreference.ExpressRateSelection:
            case IShipDefl.FieldPreference.GroundRateSelection:
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
          switch ((IShipDefl.FieldPreference) fp.Index)
          {
            case IShipDefl.FieldPreference.BillTransCharges3rdPartyAcctNbr:
            case IShipDefl.FieldPreference.BillDutyTaxFees3rdPartyAcctNbr:
            case IShipDefl.FieldPreference.Ground3rdPartyAcctNbr:
            case IShipDefl.FieldPreference.GroundBillRecipientAcctNbr:
            case IShipDefl.FieldPreference.ReturnBillTransCharges3rdPartyAcctNbr:
            case IShipDefl.FieldPreference.ReturnBillDutyTaxFees3rdPartyAcctNbr:
              if (c.Text.Length > 0)
              {
                ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.ValidateAccountNumber(c.Text);
                if (serviceResponse.Error.Code == 1)
                {
                  fp.StringFieldDeflVal = c.Text;
                  break;
                }
                Utility.DisplayError(serviceResponse.Error);
                flag1 = false;
                break;
              }
              break;
            case IShipDefl.FieldPreference.NumberOfCommercialInvoiceCopies:
              fp.DefaultValueType = FieldPref.PreferenceValueType.String;
              fp.StringFieldDeflVal = this.spnConstant.Value.ToString();
              break;
            case IShipDefl.FieldPreference.EmergencyPhoneNbr:
              fp.StringFieldDeflVal = ((FdxMaskedEdit) c).Raw + "\t" + this.edtConstantExt.Raw;
              break;
            case IShipDefl.FieldPreference.TotalCustomsValueShipmentDetails:
              fp.DefaultValueType = FieldPref.PreferenceValueType.Float;
              fp.StringFieldDeflVal = this.edtConstant.Raw;
              break;
            case IShipDefl.FieldPreference.TermsOfSaleValue:
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
              fp.DefaultValueType = FieldPref.PreferenceValueType.String;
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
          if (fp.Index == 130 && ((RadioButton) c).Checked)
          {
            int num1 = (int) Utility.DisplayError(GuiData.Languafier.Translate("AutoTrackPrefVolumeWarning"), Error.ErrorType.Warning);
          }
          fp.IntFieldDeflVal = ((RadioButton) c).Checked ? 1 : 0;
          break;
        case FieldPref.ControlType.MultiSelectListBox:
          switch ((IShipDefl.FieldPreference) fp.Index)
          {
            case IShipDefl.FieldPreference.SpecialServices:
            case IShipDefl.FieldPreference.GroundSpecialServices:
            case IShipDefl.FieldPreference.ReturnExpressSpecialServices:
            case IShipDefl.FieldPreference.ReturnGroundSpecialServices:
              if (this.lbxConstant.SelectedItems.Count == 0)
              {
                int num2 = (int) Utility.DisplayError(GuiData.Languafier.Translate("InvalidConstant"), Error.ErrorType.Failure);
                flag1 = false;
                break;
              }
              if (fp.Index == 14 || fp.Index == 109)
                flag1 = !this.GPR3 ? this.ValidateExpressSpecialServices(this.lbxConstant.SelectedItems.Cast<DataRowView>().Select<DataRowView, SplSvc.SpecialServiceType>((System.Func<DataRowView, SplSvc.SpecialServiceType>) (drv => (SplSvc.SpecialServiceType) Enum.Parse(typeof (SplSvc.SpecialServiceType), (string) drv[this.lbxConstant.ValueMember])))) : this.ValidateExpressSpecialServicesGPR(this.lbxConstant.SelectedItems.Cast<DataRowView>().Select<DataRowView, SplSvc>((System.Func<DataRowView, SplSvc>) (drv => drv["SplSvc"] as SplSvc)));
              else if (fp.Index == 32 || fp.Index == 110)
              {
                bool flag2 = false;
                bool flag3 = false;
                foreach (DataRowView selectedItem in this.lbxConstant.SelectedItems)
                {
                  switch ((SplSvc.SpecialServiceType) Enum.Parse(typeof (SplSvc.SpecialServiceType), (string) selectedItem[this.lbxConstant.ValueMember]))
                  {
                    case SplSvc.SpecialServiceType.GroundSelectDayDelivery:
                      flag2 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundAppointmentDelivery:
                      flag3 = true;
                      continue;
                    default:
                      continue;
                  }
                }
                if (flag2 & flag3)
                {
                  int num3 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(38300), Error.ErrorType.Failure);
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
          if (fp.Index == 13 || fp.Index == 63)
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
              switch ((IShipDefl.FieldPreference) fieldPref.Index)
              {
                case IShipDefl.FieldPreference.NumberOfCommercialInvoiceCopies:
                  if (this.spnConstant.Value < 1M)
                  {
                    flag = false;
                    break;
                  }
                  break;
                case IShipDefl.FieldPreference.TermsOfSaleValue:
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
            if (!flag && fieldPref.Index != 14 && fieldPref.Index != 32 && fieldPref.Index != 36 && fieldPref.Index != 41 && fieldPref.Index != 10 && fieldPref.Index != 8 && fieldPref.Index != 104 && fieldPref.Index != 103 && fieldPref.Index != 102 && fieldPref.Index != 109 && fieldPref.Index != 110)
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
            stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(9519), (object) GuiData.Languafier.Translate("FIELDPREF_I_" + fieldPref.Index.ToString()));
            int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
            control.Focus();
          }
        }
      }
      return flag;
    }

    private void IntlFieldPreferences_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.PopulatePreferenceTypesCombo();
      this.cboReturnRecip.Visible = false;
      this.lblReturnRecip.Visible = false;
      this.cboReturnRecip.Visible = false;
      this.chkAlwaysUseReturnToCode.Visible = false;
    }

    private void PopulatePreferenceTypesCombo()
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Code", typeof (int));
      dataTable.Columns.Add("Description");
      foreach (FieldPref.FieldPreferenceType fieldPreferenceType in Enum.GetValues(typeof (FieldPref.FieldPreferenceType)))
      {
        switch (fieldPreferenceType)
        {
          case FieldPref.FieldPreferenceType.Outbound:
          case FieldPref.FieldPreferenceType.Returns:
          case FieldPref.FieldPreferenceType.MPS:
            DataRow row = dataTable.NewRow();
            row["Code"] = (object) fieldPreferenceType;
            row["Description"] = (object) GuiData.Languafier.Translate("PREFTYPE_" + fieldPreferenceType.ToString());
            dataTable.Rows.Add(row);
            continue;
          default:
            continue;
        }
      }
      this.cboPreferenceType.DisplayMemberQ = "Description";
      this.cboPreferenceType.ValueMemberQ = "Code";
      this.cboPreferenceType.DataSource = (object) dataTable;
    }

    private void cboPreferenceType_SelectedValueChanged(object sender, EventArgs e)
    {
      this.CurrentPrefTypeIndex = this.cboPreferenceType.SelectedIndex;
      if (this.PrevPrefTypeIndex == -1)
        this.PrevPrefTypeIndex = this.cboPreferenceType.SelectedIndex;
      if (this.lbxFields.SelectedIndex > -1 && !this._bResettingPrefType && this.SaveFieldPref((int) ((DataRowView) this.lbxFields.Items[this.lbxFields.SelectedIndex])[0]))
      {
        this.PrevPrefTypeIndex = this.cboPreferenceType.SelectedIndex;
        this.RefreshList();
      }
      else
      {
        if (this.PrevPrefTypeIndex <= -1 || this.PrevPrefTypeIndex == this.CurrentPrefTypeIndex)
          return;
        this._bResettingPrefType = true;
        this.CurrentPrefTypeIndex = this.PrevPrefTypeIndex;
        this.cboPreferenceType.SelectedIndexQ = this.PrevPrefTypeIndex;
        this._bResettingPrefType = false;
      }
    }

    private bool ValidateExpressSpecialServices(IEnumerable<SplSvc.SpecialServiceType> sss)
    {
      bool flag1 = true;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      bool flag6 = false;
      bool flag7 = false;
      bool flag8 = false;
      bool flag9 = false;
      bool flag10 = false;
      bool flag11 = false;
      bool flag12 = false;
      bool flag13 = false;
      bool flag14 = false;
      bool flag15 = false;
      foreach (DataRowView selectedItem in this.lbxConstant.SelectedItems)
      {
        switch ((SplSvc.SpecialServiceType) Enum.Parse(typeof (SplSvc.SpecialServiceType), (string) selectedItem[this.lbxConstant.ValueMember]))
        {
          case SplSvc.SpecialServiceType.SaturdayDelivery:
            flag12 = true;
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
          case SplSvc.SpecialServiceType.BSO:
            flag7 = true;
            continue;
          case SplSvc.SpecialServiceType.COD:
            flag3 = true;
            continue;
          case SplSvc.SpecialServiceType.IdfPieceCountVerification:
            flag13 = true;
            continue;
          case SplSvc.SpecialServiceType.IdfAppointmentDelivery:
            flag10 = true;
            continue;
          case SplSvc.SpecialServiceType.ThirdPartyConsignee:
            flag6 = true;
            continue;
          case SplSvc.SpecialServiceType.CutFlowers:
            flag11 = true;
            continue;
          case SplSvc.SpecialServiceType.FICE:
            flag8 = true;
            continue;
          case SplSvc.SpecialServiceType.ITAR:
            flag9 = true;
            continue;
          case SplSvc.SpecialServiceType.PriorityAlertPlus:
            flag14 = true;
            continue;
          case SplSvc.SpecialServiceType.ReturnClearance:
            flag15 = true;
            continue;
          default:
            continue;
        }
      }
      if (flag4 & flag5)
      {
        int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9528), Error.ErrorType.Failure);
        flag1 = false;
      }
      else if (flag6 & flag7)
      {
        int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(13928), Error.ErrorType.Failure);
        flag1 = false;
      }
      else if (flag2 && flag9 | flag6 | flag3)
      {
        string str = string.Empty;
        if (flag9)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ITAR.ToString());
        else if (flag6)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ThirdPartyConsignee.ToString());
        else if (flag14)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.PriorityAlertPlus.ToString());
        else if (flag3)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.COD.ToString());
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.PriorityAlert.ToString()), (object) str);
        int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
        flag1 = false;
      }
      else if (flag14 && flag9 | flag6 | flag2 | flag3)
      {
        string str = string.Empty;
        if (flag9)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ITAR.ToString());
        else if (flag6)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ThirdPartyConsignee.ToString());
        else if (flag2)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.PriorityAlert.ToString());
        else if (flag3)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.COD.ToString());
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.PriorityAlertPlus.ToString()), (object) str);
        int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
        flag1 = false;
      }
      else if (flag9 && flag8 | flag6 | flag13 | flag12 | flag11 | flag10 | flag3)
      {
        string str = string.Empty;
        if (flag8)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.FICE.ToString());
        else if (flag6)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ThirdPartyConsignee.ToString());
        else if (flag13)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.IdfPieceCountVerification.ToString());
        else if (flag12)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.SaturdayDelivery.ToString());
        else if (flag11)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.CutFlowers.ToString());
        else if (flag10)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.IdfAppointmentDelivery.ToString());
        else if (flag3)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.COD.ToString());
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ITAR.ToString()), (object) str);
        int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
        flag1 = false;
      }
      else if (flag15 && flag8 | flag7 | flag9 | flag11)
      {
        string str = string.Empty;
        if (flag8)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.FICE.ToString());
        else if (flag7)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.BSO.ToString());
        else if (flag9)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ITAR.ToString());
        else if (flag11)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.CutFlowers.ToString());
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ReturnClearance.ToString()), (object) str);
        int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
        flag1 = false;
      }
      return flag1;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IntlFieldPreferences));
      this.cboPreferenceType = new ComboBoxEx();
      this.grpPrefType = new Label();
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
      componentResourceManager.ApplyResources((object) this.rdbConstantNo, "rdbConstantNo");
      this.helpProvider1.SetShowHelp((Control) this.rdbConstantNo, (bool) componentResourceManager.GetObject("rdbConstantNo.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbConstantYes, componentResourceManager.GetString("rdbConstantYes.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbConstantYes, (HelpNavigator) componentResourceManager.GetObject("rdbConstantYes.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbConstantYes, "rdbConstantYes");
      this.helpProvider1.SetShowHelp((Control) this.rdbConstantYes, (bool) componentResourceManager.GetObject("rdbConstantYes.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lbxConstant, "lbxConstant");
      this.helpProvider1.SetHelpKeyword((Control) this.edtConstant, componentResourceManager.GetString("edtConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtConstant, (HelpNavigator) componentResourceManager.GetObject("edtConstant.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtConstant, "edtConstant");
      this.helpProvider1.SetShowHelp((Control) this.edtConstant, (bool) componentResourceManager.GetObject("edtConstant.ShowHelp"));
      this.cboConstant.DropDownWidth = 350;
      this.helpProvider1.SetHelpKeyword((Control) this.cboConstant, componentResourceManager.GetString("cboConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboConstant, (HelpNavigator) componentResourceManager.GetObject("cboConstant.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.cboConstant, "cboConstant");
      this.helpProvider1.SetShowHelp((Control) this.cboConstant, (bool) componentResourceManager.GetObject("cboConstant.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.btnBrowse, componentResourceManager.GetString("btnBrowse.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnBrowse, (HelpNavigator) componentResourceManager.GetObject("btnBrowse.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.btnBrowse, "btnBrowse");
      this.helpProvider1.SetShowHelp((Control) this.btnBrowse, (bool) componentResourceManager.GetObject("btnBrowse.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkValidateAndRequireDeptNotes, componentResourceManager.GetString("chkValidateAndRequireDeptNotes.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkValidateAndRequireDeptNotes, (HelpNavigator) componentResourceManager.GetObject("chkValidateAndRequireDeptNotes.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chkValidateAndRequireDeptNotes, (bool) componentResourceManager.GetObject("chkValidateAndRequireDeptNotes.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.spnConstant, componentResourceManager.GetString("spnConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.spnConstant, (HelpNavigator) componentResourceManager.GetObject("spnConstant.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.spnConstant, "spnConstant");
      this.helpProvider1.SetShowHelp((Control) this.spnConstant, (bool) componentResourceManager.GetObject("spnConstant.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.cboRemitCode, componentResourceManager.GetString("cboRemitCode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboRemitCode, (HelpNavigator) componentResourceManager.GetObject("cboRemitCode.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboRemitCode, (bool) componentResourceManager.GetObject("cboRemitCode.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.gbxOtherPrefs, "gbxOtherPrefs");
      this.helpProvider1.SetShowHelp((Control) this.lblReturnRecip, (bool) componentResourceManager.GetObject("lblReturnRecip.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.cboReturnRecip, componentResourceManager.GetString("cboReturnRecip.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboReturnRecip, (HelpNavigator) componentResourceManager.GetObject("cboReturnRecip.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboReturnRecip, (bool) componentResourceManager.GetObject("cboReturnRecip.ShowHelp"));
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
      this.cboPreferenceType.DisplayMemberQ = "";
      this.cboPreferenceType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboPreferenceType.DroppedDownQ = false;
      componentResourceManager.ApplyResources((object) this.cboPreferenceType, "cboPreferenceType");
      this.cboPreferenceType.Name = "cboPreferenceType";
      this.cboPreferenceType.SelectedIndexQ = -1;
      this.cboPreferenceType.SelectedItemQ = (object) null;
      this.cboPreferenceType.SelectedValueQ = (object) null;
      this.cboPreferenceType.ValueMemberQ = "";
      this.cboPreferenceType.SelectedValueChanged += new EventHandler(this.cboPreferenceType_SelectedValueChanged);
      componentResourceManager.ApplyResources((object) this.grpPrefType, "grpPrefType");
      this.grpPrefType.Name = "grpPrefType";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.cboPreferenceType);
      this.Controls.Add((Control) this.grpPrefType);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (IntlFieldPreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.IntlFieldPreferences_Load);
      this.Controls.SetChildIndex((Control) this.gbxOtherPrefs, 0);
      this.Controls.SetChildIndex((Control) this.gbxStartPosition, 0);
      this.Controls.SetChildIndex((Control) this.lbxFields, 0);
      this.Controls.SetChildIndex((Control) this.grpPrefType, 0);
      this.Controls.SetChildIndex((Control) this.cboPreferenceType, 0);
      this.spnConstant.EndInit();
      this.gbxOtherPrefs.ResumeLayout(false);
      this.gbxStartPosition.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
