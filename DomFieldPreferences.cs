// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.DomFieldPreferences
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
  public class DomFieldPreferences : FieldPreferences
  {
    private bool _bRefreshingList;
    private bool _bResettingPrefType;
    private IContainer components;
    private ComboBoxEx cboPreferenceType;
    private Label grpPrefType;

    private bool GPR3
    {
      get
      {
        return GuiData.CurrentAccount != null && GuiData.CurrentAccount.is_GPR3_SVCOPTION_OFFERING_Initiative_Enabled;
      }
    }

    public DomFieldPreferences() => this.InitializeComponent();

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
        if (fieldPref.FieldPrefType == fieldPreferenceType && (fieldPref.VendorCode != (short) 6 || GuiData.AppController.ShipEngine.IsAnyMeterSmartPostEnabled()))
        {
          switch (fieldPreferenceType)
          {
            case FieldPref.FieldPreferenceType.Outbound:
              if (this.PreferenceObject.VendorCode == (short) 2 && !GuiData.CurrentAccount.IsGroundEnabled || fieldPref.Index == 20 || fieldPref.Index == 28 || fieldPref.Index == 17 || fieldPref.Index == 23 || fieldPref.Index == 30 || fieldPref.Index == 31 || fieldPref.Index == 32 || fieldPref.Index == 37 || fieldPref.Index == 38 || fieldPref.Index == 37 || fieldPref.Index == 33 || fieldPref.Index == 9 || GuiData.CurrentAccount.Address.CountryCode == "MX" && (fieldPref.Index == 34 || fieldPref.Index == 27 || fieldPref.Index == 23 || fieldPref.Index == 37 || fieldPref.Index == 28 || fieldPref.Index == 17 || fieldPref.Index == 36 || fieldPref.Index == 90 || fieldPref.Index == 12 || fieldPref.Index == 13 || fieldPref.Index == 38 || fieldPref.Index == 14 || fieldPref.Index == 88) || fieldPref.Index == 34 || GuiData.CurrentAccount.Address.CountryCode != "CA" && (fieldPref.Index == 21 || fieldPref.Index == 53) || GuiData.CurrentAccount.Address.CountryCode == "CA" && fieldPref.Index == 34 || GuiData.CurrentAccount.Address.CountryCode != "US" && fieldPref.Index == 103)
                continue;
              break;
            case FieldPref.FieldPreferenceType.MPS:
              if (GuiData.CurrentAccount.Address.CountryCode == "MX" && (fieldPref.Index == 52 || fieldPref.Index == 54 || fieldPref.Index == 56 || fieldPref.Index == 48 || fieldPref.Index == 49 || fieldPref.Index == 50 || fieldPref.Index == 51 || fieldPref.Index == 47 || fieldPref.Index == 55) || GuiData.CurrentAccount.Address.CountryCode != "CA" && GuiData.CurrentAccount.Address.CountryCode != "MX" && fieldPref.Index == 59 || GuiData.CurrentAccount.Address.CountryCode == "CA" && fieldPref.Index == 55)
                continue;
              break;
            case FieldPref.FieldPreferenceType.ReturnsMPS:
              if (fieldPref.Index == 75)
                continue;
              break;
          }
          DataRow row = dataTable.NewRow();
          row["Index"] = (object) fieldPref.Index;
          row["Description"] = (object) GuiData.Languafier.Translate("FIELDPREF_D_" + fieldPref.Index.ToString());
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
        ((FdxMaskedEdit) c).SetMask(mask);
        switch ((DShipDefl.FieldPreference) fp.Index)
        {
          case DShipDefl.FieldPreference.Weight:
          case DShipDefl.FieldPreference.MPSWeight:
          case DShipDefl.FieldPreference.ReturnMPSEstimatedWeight:
          case DShipDefl.FieldPreference.ReturnEstimatedWeight:
            str = "#####.##";
            break;
          case DShipDefl.FieldPreference.Express3rdPartyAccountNumber:
          case DShipDefl.FieldPreference.Ground3rdPartyAccountNumber:
          case DShipDefl.FieldPreference.GroundBillRecipientAccountNumber:
          case DShipDefl.FieldPreference.Return3rdPartyAccountNumber:
            str = "999999999";
            break;
          case DShipDefl.FieldPreference.References:
          case DShipDefl.FieldPreference.AdditionalReference1:
          case DShipDefl.FieldPreference.AdditionalReference2:
          case DShipDefl.FieldPreference.AdditionalReference3:
          case DShipDefl.FieldPreference.MPSReferences:
          case DShipDefl.FieldPreference.MPSAdditionalReference1:
          case DShipDefl.FieldPreference.MPSAdditionalReference2:
          case DShipDefl.FieldPreference.MPSAdditionalReference3:
          case DShipDefl.FieldPreference.ReturnCustomerReference:
          case DShipDefl.FieldPreference.ReturnAdditionalReference1:
          case DShipDefl.FieldPreference.ReturnAdditionalReference2:
          case DShipDefl.FieldPreference.ReturnAdditionalReference3:
          case DShipDefl.FieldPreference.ReturnMPSCustomerReference:
          case DShipDefl.FieldPreference.ReturnMPSAdditionalReference1:
          case DShipDefl.FieldPreference.ReturnMPSAdditionalReference2:
          case DShipDefl.FieldPreference.ReturnMPSAdditionalReference3:
            ((FdxMaskedEdit) c).SetMask('I', 30);
            break;
          case DShipDefl.FieldPreference.DepartmentNotes:
          case DShipDefl.FieldPreference.MPSDepartmentNotes:
          case DShipDefl.FieldPreference.ReturnDepartmentNotes:
            str = "IIIIIIIIIIIIIIIIIIIIIIIII";
            break;
          case DShipDefl.FieldPreference.DeclaredValue:
          case DShipDefl.FieldPreference.MPSDeclaredValue:
          case DShipDefl.FieldPreference.ReturnDeclaredValue:
          case DShipDefl.FieldPreference.ReturnMPSDeclaredValue:
            str = "######";
            break;
          case DShipDefl.FieldPreference.PackageContent1:
          case DShipDefl.FieldPreference.PackageContent2:
            ((FdxMaskedEdit) c).SetMask('I', 40);
            break;
          case DShipDefl.FieldPreference.TotalPackages:
            str = "####";
            break;
          case DShipDefl.FieldPreference.NameOfSignatory:
          case DShipDefl.FieldPreference.PlaceOfSignatory:
            ((FdxMaskedEdit) c).SetMask('I', 35);
            break;
          case DShipDefl.FieldPreference.EmergencyPhoneNumber:
            ((FdxMaskedEdit) c).SetMask('9', 15);
            this.edtConstantExt.SetMask('9', 6);
            break;
          case DShipDefl.FieldPreference.ShipmentDate:
            Utility.SetDateMasks(c as FdxMaskedEdit);
            break;
          case DShipDefl.FieldPreference.ReturnContactPhone:
            mask = !Utility.IsAnywhereToAnywhere() ? eMasks.maskPhoneTen : eMasks.maskIntlPhone;
            break;
          case DShipDefl.FieldPreference.ReturnItemDescription:
          case DShipDefl.FieldPreference.ReturnMPSItemDescription:
            ((FdxMaskedEdit) c).SetMask('I', 80);
            break;
          case DShipDefl.FieldPreference.ReturnOther1EmailAddress:
          case DShipDefl.FieldPreference.ReturnOther2EmailAddress:
            ((FdxMaskedEdit) c).SetMask('I', 120);
            break;
          case DShipDefl.FieldPreference.GroundHazMatEmergencyPhone:
            mask = eMasks.maskIntlPhoneExt;
            break;
          case DShipDefl.FieldPreference.GroundHazMatNameOfSignatory:
            ((FdxMaskedEdit) c).SetMask('A', 30);
            break;
          case DShipDefl.FieldPreference.GroundHazMatOfferorName:
            ((FdxMaskedEdit) c).SetMask('I', 22);
            break;
          case DShipDefl.FieldPreference.GroundHazMatPackageType:
            ((FdxMaskedEdit) c).SetMask('A', 25);
            break;
          case DShipDefl.FieldPreference.ReturnPrintReturnMessage:
            ((FdxMaskedEdit) c).SetMask('I', 200);
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
      switch ((DShipDefl.FieldPreference) fp.Index)
      {
        case DShipDefl.FieldPreference.Service:
        case DShipDefl.FieldPreference.ReturnService:
          SvcListResponse serviceList1 = GuiData.AppController.ShipEngine.GetServiceList(GuiData.CurrentAccount, fp.Index == 1 ? FieldPref.PreferenceTypes.DomesticService : FieldPref.PreferenceTypes.ReturnManagerService);
          if (serviceList1 == null || serviceList1.SvcList == null || serviceList1.SvcList.SvcArray == null)
            break;
          DataTable serviceListDataTable = Utility.GetServiceListDataTable(serviceList1.SvcList, GuiData.CurrentAccount.Address.CountryCode, GuiData.CurrentAccount.Address.CountryCode);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) serviceListDataTable;
          if (serviceListDataTable.Rows.Count <= 1)
            break;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.ExpressPackageType:
        case DShipDefl.FieldPreference.GroundPackageType:
        case DShipDefl.FieldPreference.ReturnPackageType:
        case DShipDefl.FieldPreference.ReturnExpressPackageType:
        case DShipDefl.FieldPreference.SmartPostPackageType:
        case DShipDefl.FieldPreference.ReturnSmartPostPackageType:
          bool bAddYourPackagingIfNoData = fp.Index == 12 || fp.Index == 41 || fp.Index == 95 || fp.Index == 101;
          SvcListResponse serviceList2 = GuiData.AppController.ShipEngine.GetServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.DomesticService);
          if (serviceList2 == null || serviceList2.SvcList == null || serviceList2.SvcList.SvcArray == null)
            break;
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
          if (consolidatedPackageList.Rows.Count <= 1)
            break;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.PackageSize:
        case DShipDefl.FieldPreference.MPSPackageSize:
        case DShipDefl.FieldPreference.ReturnPackageDimensions:
        case DShipDefl.FieldPreference.ReturnMPSPackageDimensions:
          if (GuiData.AppController.ShipEngine.GetDataList(FedEx.Gsm.ShipEngine.Entities.ListSpecification.Dimension_DropDown, out output1, new Error()) == 1)
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
        case DShipDefl.FieldPreference.ExpressPaymentType:
        case DShipDefl.FieldPreference.GroundPaymentType:
          DataTable dataTable1 = Utility.GetDataTable(Utility.ListTypes.PaymentTypes);
          if (!GuiData.CurrentAccount.CollectBillingOption || fp.Index == 4)
            dataTable1.DefaultView.RowFilter = "Code <> 4";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) dataTable1;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.DepartmentNotes:
        case DShipDefl.FieldPreference.MPSDepartmentNotes:
        case DShipDefl.FieldPreference.ReturnDepartmentNotes:
          DataTable output2 = (DataTable) null;
          GuiData.AppController.ShipEngine.GetDataList(GsmDataAccess.ListSpecification.Department_DropDown, out output2, new Error());
          if (output2 != null)
            output2.DefaultView.Sort = "Code ASC";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) output2;
          this.cboConstant.SelectedIndexQ = -1;
          this.edtConstant.Text = "";
          break;
        case DShipDefl.FieldPreference.SpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.DomesticSpecialService).SpecialServices, this.GPR3));
          break;
        case DShipDefl.FieldPreference.GroundSpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.DomesticGroundSpecialService).SpecialServices, this.GPR3));
          break;
        case DShipDefl.FieldPreference.WeightType:
        case DShipDefl.FieldPreference.ReturnWeightType:
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
        case DShipDefl.FieldPreference.DangerousGoodsRegulationType:
          DataTable dataTable2 = Utility.GetDataTable(Utility.ListTypes.DgRegulationTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable2;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.FedExFreightChargeType:
        case DShipDefl.FieldPreference.GroundFreightChargeType:
          DataTable dataTable3 = Utility.GetDataTable(Utility.ListTypes.FreightChargeTypes);
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) dataTable3;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.ReturnPaymentType:
          DataTable dataTable4 = Utility.GetDataTable(Utility.ListTypes.PaymentTypes);
          dataTable4.DefaultView.RowFilter = "Code <> 2 AND Code <> 4";
          for (int index = dataTable4.Rows.Count - 1; index >= 0; --index)
          {
            string str = dataTable4.Rows[index].ItemArray[0] as string;
            if ("1" == str)
              dataTable4.Rows[index]["Description"] = (object) GuiData.Languafier.Translate("d28789");
            else if ("3" == str)
              dataTable4.Rows[index]["Description"] = (object) GuiData.Languafier.Translate("d28790");
            else
              dataTable4.Rows.RemoveAt(index);
          }
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) dataTable4;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.MPSGroundMPSType:
          DataTable dataTable5 = Utility.GetDataTable(Utility.ListTypes.MpsTypesGround);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable5;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.MPSExpressMPSType:
          DataTable dataTable6 = Utility.GetDataTable(Utility.ListTypes.MpsTypes);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable6;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.MPSGroundRateSelection:
        case DShipDefl.FieldPreference.MPSExpressRateSelection:
        case DShipDefl.FieldPreference.ExpressRateSelection:
        case DShipDefl.FieldPreference.GroundRateSelection:
          DataTable dataTable7 = Utility.GetDataTable(Utility.ListTypes.MpsRateSelectionGround);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable7;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.ReturnLabelType:
          DataTable dataTable8 = Utility.GetDataTable(Utility.ListTypes.ReturnLabelTypes);
          if (dataTable8 != null)
          {
            this.cboConstant.ValueMemberQ = "Code";
            this.cboConstant.DisplayMemberQ = "Description";
          }
          this.cboConstant.DataSourceQ = (object) dataTable8;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.ReturnExpressSpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.ReturnExpressSpecialService).SpecialServices, this.GPR3));
          break;
        case DShipDefl.FieldPreference.ReturnGroundSpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.ReturnGroundSpecialService).SpecialServices, this.GPR3));
          break;
        case DShipDefl.FieldPreference.SignatureOptions:
        case DShipDefl.FieldPreference.ReturnSignatureOptions:
          DataTable dataTable9 = Utility.GetDataTable(Utility.ListTypes.SignatureOptions);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable9;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.SmartPostHubID:
          DataTable dataTable10 = Utility.GetDataTable(Utility.ListTypes.HubIds);
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) dataTable10;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.SmartPostPaymentType:
          DataTable dataTable11 = Utility.GetDataTable(Utility.ListTypes.PaymentTypes);
          dataTable11.DefaultView.RowFilter = "Code <> 2 AND Code <> 4";
          this.cboConstant.DisplayMemberQ = "CodeDescription";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DataSourceQ = (object) dataTable11;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.SmartPostSpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.SmartPostSpecialService).SpecialServices));
          break;
        case DShipDefl.FieldPreference.SmartPostStdMailInstruction:
          DataTable dataTable12 = Utility.GetDataTable(Utility.ListTypes.SmartPostInstructions);
          dataTable12.DefaultView.RowFilter = "Code = 1 OR Code = 4";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable12;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.SmartPostInstruction:
          DataTable dataTable13 = Utility.GetDataTable(Utility.ListTypes.SmartPostInstructions);
          dataTable13.DefaultView.Sort = "Description ASC";
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable13;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.HomeDeliverySpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.HomeDeliverySpecialservice).SpecialServices, this.GPR3));
          break;
        case DShipDefl.FieldPreference.ReturnHomeDeliverySpecialServices:
          this.BindSpecialServiceDataTableToListBox(this.BuildSpecialServiceDataTable(GuiData.AppController.ShipEngine.GetSpecialServiceList(GuiData.CurrentAccount, FieldPref.PreferenceTypes.ReturnHomeDeliverySpecialservice).SpecialServices, this.GPR3));
          break;
        case DShipDefl.FieldPreference.AlcoholRecipientType:
          DataTable dataTable14 = Utility.GetDataTable(Utility.ListTypes.AlcoholRecipType);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable14;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.AlcoholShippingLabel:
          DataTable dataTable15 = Utility.GetDataTable(Utility.ListTypes.AlcoholShipmentLabelType);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable15;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.ReturnEmailExpirationDate:
          DataTable dataTable16 = Utility.GetDataTable(Utility.ListTypes.EmailExpirationDateOptions);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable16;
          this.cboConstant.SelectedIndexQ = -1;
          this.cboConstant.SelectedIndexQ = -1;
          break;
        case DShipDefl.FieldPreference.DistanceUnitOfMeasure:
        case DShipDefl.FieldPreference.ReturnDistanceUnitOfMeasure:
          DataTable dataTable17 = Utility.GetDataTable(Utility.ListTypes.DistanceUnitOfMeasure);
          this.cboConstant.ValueMemberQ = "Code";
          this.cboConstant.DisplayMemberQ = "Description";
          this.cboConstant.DataSourceQ = (object) dataTable17;
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
        case 1:
        case 42:
          if (string.IsNullOrEmpty(fp.StringFieldDeflVal))
            break;
          Utility.SetServicePrefValue((ComboBox) this.cboConstant, fp.StringFieldDeflVal);
          break;
        case 2:
        case 3:
        case 12:
        case 41:
        case 46:
        case 73:
        case 78:
        case 87:
          if (string.IsNullOrEmpty(fp.StringFieldDeflVal))
            break;
          if (fp.StringFieldDeflVal.Length > 1 && fp.StringFieldDeflVal.Contains(" "))
          {
            this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal.Substring(0, fp.StringFieldDeflVal.IndexOf(' '));
            break;
          }
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal;
          break;
        case 4:
        case 13:
        case 40:
        case 94:
        case 95:
        case 96:
        case 98:
        case 99:
        case 101:
        case 106:
        case 107:
        case 109:
        case 113:
        case 114:
          this.cboConstant.SelectedValue = (object) fp.StringFieldDeflVal;
          break;
        case 6:
        case 47:
          this.edtConstant.Text = fp.StringFieldDeflVal;
          break;
        case 7:
        case 51:
        case 69:
          if (fp.StringFieldDeflVal.Length <= 1 || !fp.StringFieldDeflVal.Contains("\t"))
            break;
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal.Substring(0, fp.StringFieldDeflVal.IndexOf("\t"));
          this.edtConstant.Text = fp.StringFieldDeflVal.Substring(fp.StringFieldDeflVal.IndexOf("\t") + 1);
          break;
        case 10:
        case 14:
        case 71:
        case 72:
        case 97:
        case 103:
        case 104:
          fp.DefaultValueType = FieldPref.PreferenceValueType.StringList;
          if (fp.StringListFieldDeflVal == null)
            break;
          this.SetSpecialServiceListBoxItems(fp.StringListFieldDeflVal);
          break;
        case 21:
        case 115:
          this.cboConstant.SelectedItem = (object) fp.StringFieldDeflVal;
          break;
        case 27:
          if (fp.StringFieldDeflVal.Length > 1 && fp.StringFieldDeflVal.Contains("\t"))
          {
            this.edtConstant.Text = fp.StringFieldDeflVal.Substring(0, fp.StringFieldDeflVal.IndexOf("\t"));
            this.edtConstantExt.Text = fp.StringFieldDeflVal.Substring(fp.StringFieldDeflVal.IndexOf("\t") + 1);
            break;
          }
          this.edtConstant.Text = fp.StringFieldDeflVal;
          break;
        case 34:
        case 52:
        case 53:
        case 56:
        case 57:
        case 88:
        case 89:
        case 116:
        case 117:
          this.cboConstant.SelectedValue = (object) fp.IntFieldDeflVal.ToString();
          break;
        case 35:
        case 36:
          this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal;
          break;
        case 39:
        case 44:
        case 54:
        case 55:
        case 59:
        case 83:
        case 100:
        case 108:
        case 110:
        case 111:
        case 112:
        case 118:
        case 119:
        case 120:
        case 121:
        case 122:
        case 123:
        case 124:
        case 125:
        case 126:
        case (int) sbyte.MaxValue:
        case 128:
        case 129:
          this.rdbConstantYes.Checked = fp.IntFieldDeflVal == 1;
          this.rdbConstantNo.Checked = fp.IntFieldDeflVal == 0;
          break;
        case 64:
          this.cboConstant.SelectedValueQ = (object) fp.IntFieldDeflVal;
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
          switch ((DShipDefl.FieldPreference) fp.Index)
          {
            case DShipDefl.FieldPreference.Service:
            case DShipDefl.FieldPreference.ReturnService:
              fp.StringFieldDeflVal = Utility.GetServicePrefValue((ComboBox) c);
              break;
            case DShipDefl.FieldPreference.ExpressPackageType:
            case DShipDefl.FieldPreference.PackageSize:
            case DShipDefl.FieldPreference.ExpressPaymentType:
            case DShipDefl.FieldPreference.GroundPackageType:
            case DShipDefl.FieldPreference.GroundPaymentType:
            case DShipDefl.FieldPreference.Currency:
            case DShipDefl.FieldPreference.FedExFreightChargeType:
            case DShipDefl.FieldPreference.GroundFreightChargeType:
            case DShipDefl.FieldPreference.ReturnPaymentType:
            case DShipDefl.FieldPreference.ReturnPackageType:
            case DShipDefl.FieldPreference.MPSPackageSize:
            case DShipDefl.FieldPreference.ReturnPackageDimensions:
            case DShipDefl.FieldPreference.ReturnMPSPackageDimensions:
            case DShipDefl.FieldPreference.ReturnExpressPackageType:
            case DShipDefl.FieldPreference.SmartPostHubID:
            case DShipDefl.FieldPreference.SmartPostPackageType:
            case DShipDefl.FieldPreference.SmartPostPaymentType:
            case DShipDefl.FieldPreference.SmartPostStdMailInstruction:
            case DShipDefl.FieldPreference.SmartPostInstruction:
            case DShipDefl.FieldPreference.ReturnSmartPostPackageType:
            case DShipDefl.FieldPreference.AlcoholRecipientType:
            case DShipDefl.FieldPreference.AlcoholShippingLabel:
            case DShipDefl.FieldPreference.ReturnEmailExpirationDate:
            case DShipDefl.FieldPreference.DistanceUnitOfMeasure:
            case DShipDefl.FieldPreference.ReturnDistanceUnitOfMeasure:
              fp.StringFieldDeflVal = ((ComboBoxEx) c).SelectedValue as string;
              break;
            case DShipDefl.FieldPreference.DangerousGoodsRegulationType:
            case DShipDefl.FieldPreference.MPSGroundMPSType:
            case DShipDefl.FieldPreference.MPSExpressMPSType:
            case DShipDefl.FieldPreference.MPSGroundRateSelection:
            case DShipDefl.FieldPreference.MPSExpressRateSelection:
            case DShipDefl.FieldPreference.ReturnLabelType:
            case DShipDefl.FieldPreference.SignatureOptions:
            case DShipDefl.FieldPreference.ReturnSignatureOptions:
            case DShipDefl.FieldPreference.ExpressRateSelection:
            case DShipDefl.FieldPreference.GroundRateSelection:
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
          switch ((DShipDefl.FieldPreference) fp.Index)
          {
            case DShipDefl.FieldPreference.Express3rdPartyAccountNumber:
            case DShipDefl.FieldPreference.Return3rdPartyAccountNumber:
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
            case DShipDefl.FieldPreference.EmergencyPhoneNumber:
              fp.StringFieldDeflVal = ((FdxMaskedEdit) c).Raw + "\t" + this.edtConstantExt.Raw;
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
          if (fp.Index == (int) sbyte.MaxValue && ((RadioButton) c).Checked)
          {
            int num1 = (int) Utility.DisplayError(GuiData.Languafier.Translate("AutoTrackPrefVolumeWarning"), Error.ErrorType.Warning);
          }
          fp.IntFieldDeflVal = ((RadioButton) c).Checked ? 1 : 0;
          break;
        case FieldPref.ControlType.MultiSelectListBox:
          switch ((DShipDefl.FieldPreference) fp.Index)
          {
            case DShipDefl.FieldPreference.SpecialServices:
            case DShipDefl.FieldPreference.GroundSpecialServices:
            case DShipDefl.FieldPreference.ReturnExpressSpecialServices:
            case DShipDefl.FieldPreference.ReturnGroundSpecialServices:
            case DShipDefl.FieldPreference.SmartPostSpecialServices:
            case DShipDefl.FieldPreference.HomeDeliverySpecialServices:
            case DShipDefl.FieldPreference.ReturnHomeDeliverySpecialServices:
              if (this.lbxConstant.SelectedItems.Count == 0)
              {
                int num2 = (int) Utility.DisplayError(GuiData.Languafier.Translate("InvalidConstant"), Error.ErrorType.Failure);
                flag1 = false;
                break;
              }
              if (fp.Index == 10 || fp.Index == 71)
                flag1 = !this.GPR3 ? this.ValidateExpressSpecialServices(this.lbxConstant.SelectedItems.Cast<DataRowView>().Select<DataRowView, SplSvc.SpecialServiceType>((System.Func<DataRowView, SplSvc.SpecialServiceType>) (drv => (SplSvc.SpecialServiceType) Enum.Parse(typeof (SplSvc.SpecialServiceType), (string) drv[this.lbxConstant.ValueMember])))) : this.ValidateExpressSpecialServicesGPR(this.lbxConstant.SelectedItems.Cast<DataRowView>().Select<DataRowView, SplSvc>((System.Func<DataRowView, SplSvc>) (drv => drv["SplSvc"] as SplSvc)));
              else if (fp.Index == 14 || fp.Index == 72)
              {
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
                foreach (DataRowView selectedItem in this.lbxConstant.SelectedItems)
                {
                  switch ((SplSvc.SpecialServiceType) Enum.Parse(typeof (SplSvc.SpecialServiceType), (string) selectedItem[this.lbxConstant.ValueMember]))
                  {
                    case SplSvc.SpecialServiceType.Alcohol:
                      flag14 = true;
                      continue;
                    case SplSvc.SpecialServiceType.HoldAtLocation:
                      flag12 = true;
                      continue;
                    case SplSvc.SpecialServiceType.DryIceOnly:
                      flag4 = true;
                      continue;
                    case SplSvc.SpecialServiceType.BSO:
                      flag5 = true;
                      continue;
                    case SplSvc.SpecialServiceType.COD:
                    case SplSvc.SpecialServiceType.GroundCOD:
                      flag13 = true;
                      continue;
                    case SplSvc.SpecialServiceType.HazardousMaterials:
                      flag6 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundSelectDayDelivery:
                      flag2 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundEveningDelivery:
                      flag7 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundAppointmentDelivery:
                      flag3 = true;
                      continue;
                    case SplSvc.SpecialServiceType.ReturnManager:
                      flag9 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundORMD:
                      flag8 = true;
                      continue;
                    case SplSvc.SpecialServiceType.LithiumBattery:
                      flag11 = true;
                      continue;
                    case SplSvc.SpecialServiceType.SmallQuantityException:
                      flag10 = true;
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
                else if (flag5 & flag6)
                {
                  int num4 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9633), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag5 & flag8)
                {
                  int num5 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9634), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag5 & flag4)
                {
                  int num6 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9635), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag5 & flag10)
                {
                  int num7 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9636), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag5 & flag11)
                {
                  int num8 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9637), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag3 & flag6)
                {
                  int num9 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9638), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag2 & flag6)
                {
                  int num10 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9639), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag7 & flag6)
                {
                  int num11 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9640), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag8 & flag6)
                {
                  int num12 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9641), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag10 & flag6)
                {
                  int num13 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9642), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag10 & flag8)
                {
                  int num14 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9643), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag9 & flag6)
                {
                  int num15 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9644), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag12 & flag13)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HoldAtLocation.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundCOD.ToString()));
                  int num16 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag12 & flag4)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HoldAtLocation.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.DryIceOnly.ToString()));
                  int num17 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag12 & flag6)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HoldAtLocation.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HazardousMaterials.ToString()));
                  int num18 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag12 & flag8)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HoldAtLocation.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundORMD.ToString()));
                  int num19 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag12 & flag10)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HoldAtLocation.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.SmallQuantityException.ToString()));
                  int num20 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag12 & flag3)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HoldAtLocation.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundAppointmentDelivery.ToString()));
                  int num21 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag12 & flag7)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HoldAtLocation.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundEveningDelivery.ToString()));
                  int num22 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag12 & flag2)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HoldAtLocation.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundSelectDayDelivery.ToString()));
                  int num23 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag14 & flag6)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.Alcohol.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.HazardousMaterials.ToString()));
                  int num24 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag14 & flag8)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.Alcohol.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundORMD.ToString()));
                  int num25 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
              }
              else if (fp.Index == 103 || fp.Index == 104)
              {
                bool flag15 = false;
                bool flag16 = false;
                bool flag17 = false;
                bool flag18 = false;
                bool flag19 = false;
                foreach (DataRowView selectedItem in this.lbxConstant.SelectedItems)
                {
                  switch ((SplSvc.SpecialServiceType) Enum.Parse(typeof (SplSvc.SpecialServiceType), (string) selectedItem[this.lbxConstant.ValueMember]))
                  {
                    case SplSvc.SpecialServiceType.GroundSelectDayDelivery:
                      flag15 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundEveningDelivery:
                      flag17 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundAppointmentDelivery:
                      flag16 = true;
                      continue;
                    case SplSvc.SpecialServiceType.GroundORMD:
                      flag18 = true;
                      continue;
                    case SplSvc.SpecialServiceType.SmallQuantityException:
                      flag19 = true;
                      continue;
                    default:
                      continue;
                  }
                }
                if (flag17 & flag16)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundAppointmentDelivery.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundEveningDelivery.ToString()));
                  int num26 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag17 & flag15)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundSelectDayDelivery.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundEveningDelivery.ToString()));
                  int num27 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag15 & flag16)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundSelectDayDelivery.ToString()), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.GroundAppointmentDelivery.ToString()));
                  int num28 = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
                  flag1 = false;
                }
                else if (flag19 & flag18)
                {
                  int num29 = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9643), Error.ErrorType.Failure);
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
          if (fp.Index == 7 || fp.Index == 51 || fp.Index == 69)
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
              if (this.edtConstant.Text == "" || this.edtConstant.Mask.Length > 0 && this.edtConstant.Raw == "")
              {
                flag = false;
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
            if (!flag && fieldPref.Index != 5 && fieldPref.Index != 105 && fieldPref.Index != 10 && fieldPref.Index != 14 && fieldPref.Index != 72 && fieldPref.Index != 71 && fieldPref.Index != 103 && fieldPref.Index != 104)
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
            stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(9519), (object) GuiData.Languafier.Translate("FIELDPREF_D_" + fieldPref.Index.ToString()));
            int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
            control.Focus();
          }
        }
      }
      return flag;
    }

    private void DomFieldPreferences_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.PopulatePreferenceTypesCombo();
      if (!(GuiData.CurrentAccount.Address.CountryCode == "MX"))
        return;
      this.lblCodRemitCode.Visible = false;
      this.cboRemitCode.Visible = false;
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
          case FieldPref.FieldPreferenceType.ReturnsMPS:
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
      foreach (SplSvc.SpecialServiceType specialServiceType in sss)
      {
        switch (specialServiceType)
        {
          case SplSvc.SpecialServiceType.HoldAtLocation:
            flag9 = true;
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
          case SplSvc.SpecialServiceType.ThirdPartyConsignee:
            flag7 = true;
            continue;
          case SplSvc.SpecialServiceType.ITAR:
            flag6 = true;
            continue;
          case SplSvc.SpecialServiceType.PriorityAlertPlus:
            flag8 = true;
            continue;
          case SplSvc.SpecialServiceType.PharmacyDelivery:
            flag10 = true;
            continue;
          default:
            continue;
        }
      }
      if (flag9 & flag10)
      {
        StringBuilder stringBuilder = new StringBuilder();
        string format = GuiData.Languafier.TranslateError(3759);
        FedEx.Gsm.Common.Languafier.Languafier languafier1 = GuiData.Languafier;
        SplSvc.SpecialServiceType specialServiceType = SplSvc.SpecialServiceType.PharmacyDelivery;
        string id1 = "SS_" + specialServiceType.ToString();
        string str1 = languafier1.Translate(id1);
        FedEx.Gsm.Common.Languafier.Languafier languafier2 = GuiData.Languafier;
        specialServiceType = SplSvc.SpecialServiceType.HoldAtLocation;
        string id2 = "SS_" + specialServiceType.ToString();
        string str2 = languafier2.Translate(id2);
        stringBuilder.AppendFormat(format, (object) str1, (object) str2);
        int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
        flag1 = false;
      }
      else if (flag4 & flag5)
      {
        int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9528), Error.ErrorType.Failure);
        flag1 = false;
      }
      else if (flag2 && flag6 | flag7 | flag3)
      {
        string str = string.Empty;
        if (flag6)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ITAR.ToString());
        else if (flag7)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ThirdPartyConsignee.ToString());
        else if (flag8)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.PriorityAlertPlus.ToString());
        else if (flag3)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.COD.ToString());
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(GuiData.Languafier.TranslateError(3759), (object) GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.PriorityAlert.ToString()), (object) str);
        int num = (int) Utility.DisplayError(stringBuilder.ToString(), Error.ErrorType.Failure);
        flag1 = false;
      }
      else if (flag8 && flag6 | flag7 | flag2 | flag3)
      {
        string str = string.Empty;
        if (flag6)
          str = GuiData.Languafier.Translate("SS_" + SplSvc.SpecialServiceType.ITAR.ToString());
        else if (flag7)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DomFieldPreferences));
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
      this.helpProvider1.SetShowHelp((Control) this.rdbConstantNo, (bool) componentResourceManager.GetObject("rdbConstantNo.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdbConstantYes, componentResourceManager.GetString("rdbConstantYes.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbConstantYes, (HelpNavigator) componentResourceManager.GetObject("rdbConstantYes.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdbConstantYes, (bool) componentResourceManager.GetObject("rdbConstantYes.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.edtConstant, componentResourceManager.GetString("edtConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtConstant, (HelpNavigator) componentResourceManager.GetObject("edtConstant.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.edtConstant, (bool) componentResourceManager.GetObject("edtConstant.ShowHelp"));
      this.cboConstant.DropDownWidth = 242;
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
      this.cboPreferenceType.DisplayMemberQ = "";
      this.cboPreferenceType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboPreferenceType.DroppedDownQ = false;
      componentResourceManager.ApplyResources((object) this.cboPreferenceType, "cboPreferenceType");
      this.cboPreferenceType.Name = "cboPreferenceType";
      this.cboPreferenceType.SelectedIndexQ = -1;
      this.cboPreferenceType.SelectedItemQ = (object) null;
      this.cboPreferenceType.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboPreferenceType, (bool) componentResourceManager.GetObject("cboPreferenceType.ShowHelp"));
      this.cboPreferenceType.ValueMemberQ = "";
      this.cboPreferenceType.SelectedValueChanged += new EventHandler(this.cboPreferenceType_SelectedValueChanged);
      componentResourceManager.ApplyResources((object) this.grpPrefType, "grpPrefType");
      this.grpPrefType.Name = "grpPrefType";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Controls.Add((Control) this.cboPreferenceType);
      this.Controls.Add((Control) this.grpPrefType);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (DomFieldPreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.DomFieldPreferences_Load);
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
