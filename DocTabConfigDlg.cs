// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.DocTabConfigDlg
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class DocTabConfigDlg : HelpFormBase
  {
    private ShipDefl _shipDefl;
    private DocTab _pkgDocTab;
    private DocTab _totalDocTab;
    private bool _printTotalsDocTab;
    private Shipment.CarrierType _eCarrier;
    private IContainer components;
    private TabControlEx tabControl1;
    private TabPage tabPkgDocTab;
    private TabPage tabTotalsDocTab;
    private ucDocTab packageDocTab;
    private Button btnOK;
    private Button btnCancel;
    private ucDocTab totalsDocTab;

    public DocTabConfigDlg(ShipDefl shipPrefs, Shipment.CarrierType eCarrier)
    {
      this._eCarrier = eCarrier;
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this._shipDefl = shipPrefs;
      if (this._eCarrier == Shipment.CarrierType.Express)
      {
        this._pkgDocTab = shipPrefs.ExpressDocTab;
        this._totalDocTab = shipPrefs.ExpressTotalDocTab;
        this._printTotalsDocTab = shipPrefs.PrintExpressTotalDocTab;
      }
      else if (this._eCarrier == Shipment.CarrierType.SmartPost)
      {
        this._pkgDocTab = shipPrefs.SmartPostDocTab;
        this._totalDocTab = (DocTab) null;
        this._printTotalsDocTab = false;
      }
      else if (this._eCarrier == Shipment.CarrierType.Freight)
      {
        this._pkgDocTab = shipPrefs.FreightDocTab;
        this._totalDocTab = (DocTab) null;
        this._printTotalsDocTab = false;
      }
      else
      {
        this._pkgDocTab = shipPrefs.GroundDocTab;
        this._totalDocTab = shipPrefs.GroundTotalDocTab;
        this._printTotalsDocTab = shipPrefs.PrintGroundTotalDocTab;
      }
    }

    private void DocTabDlg_Load(object sender, EventArgs e)
    {
      if (this._eCarrier == Shipment.CarrierType.Express)
      {
        if (Utility.IsGPR3(GuiData.CurrentAccount))
        {
          ServicePackageResponse servicePackaging = GuiData.AppController.ShipEngine.GetServicePackaging(new ServicePackageRequest()
          {
            Culture = GuiData.CurrentAccount.Culture
          });
          List<SplSvc> specialServices = new List<SplSvc>();
          if (servicePackaging.SpecialServiceList != null)
            specialServices.AddRange((IEnumerable<SplSvc>) servicePackaging.SpecialServiceList);
          if (servicePackaging.NewSpecialServiceList != null)
            specialServices.AddRange((IEnumerable<SplSvc>) servicePackaging.NewSpecialServiceList);
          this.packageDocTab.AddSpecialServiceFields((IEnumerable<SplSvc>) specialServices);
        }
      }
      else if (this._eCarrier == Shipment.CarrierType.SmartPost)
      {
        this.packageDocTab.IsSmartPost = true;
        this.tabControl1.TabPages.RemoveAt(1);
        this.packageDocTab.ReLoadcomboBoxes();
      }
      else if (this._eCarrier == Shipment.CarrierType.Freight)
      {
        this.packageDocTab.IsFreight = true;
        this.tabControl1.TabPages.RemoveAt(1);
        this.packageDocTab.ReLoadcomboBoxes();
      }
      this.packageDocTab.chkPrintTotalsDocTab.Visible = false;
      this.ObjectToScreen();
    }

    private void ScreenToObject()
    {
      this._pkgDocTab.LabelList = new List<string>(14);
      this._pkgDocTab.DataList = new List<string>(14);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel1.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel2.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel3.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel4.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel5.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel6.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel7.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel8.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel9.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel10.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel11.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel12.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel13.Text);
      this._pkgDocTab.LabelList.Add(this.packageDocTab.edtDocTabLabel14.Text);
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData1.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData1.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData2.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData2.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData3.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData3.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData4.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData4.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData5.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData5.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData6.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData6.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData7.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData7.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData8.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData8.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData9.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData9.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData10.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData10.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData11.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData11.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData12.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData12.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData13.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData13.SelectedValue.ToString());
      this._pkgDocTab.DataList.Add(this.packageDocTab.cboDocTabData14.SelectedIndex == -1 ? "" : this.packageDocTab.cboDocTabData14.SelectedValue.ToString());
      if (this._eCarrier != Shipment.CarrierType.Freight && this._eCarrier != Shipment.CarrierType.SmartPost)
      {
        this._totalDocTab.LabelList = new List<string>(16);
        this._totalDocTab.DataList = new List<string>(16);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel1.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel2.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel3.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel4.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel5.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel6.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel7.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel8.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel9.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel10.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel11.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel12.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel13.Text);
        this._totalDocTab.LabelList.Add(this.totalsDocTab.edtDocTabLabel14.Text);
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData1.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData1.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData2.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData2.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData3.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData3.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData4.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData4.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData5.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData5.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData6.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData6.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData7.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData7.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData8.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData8.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData9.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData9.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData10.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData10.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData11.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData11.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData12.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData12.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData13.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData13.SelectedValue.ToString());
        this._totalDocTab.DataList.Add(this.totalsDocTab.cboDocTabData14.SelectedIndex == -1 ? "" : this.totalsDocTab.cboDocTabData14.SelectedValue.ToString());
        this._printTotalsDocTab = this.totalsDocTab.chkPrintTotalsDocTab.Checked;
      }
      if (this._eCarrier == Shipment.CarrierType.Ground)
      {
        this._shipDefl.GroundDocTab = this._pkgDocTab;
        this._shipDefl.GroundTotalDocTab = this._totalDocTab;
        this._shipDefl.PrintGroundTotalDocTab = this._printTotalsDocTab;
      }
      else if (this._eCarrier == Shipment.CarrierType.SmartPost)
        this._shipDefl.SmartPostDocTab = this._pkgDocTab;
      else if (this._eCarrier == Shipment.CarrierType.Freight)
      {
        this._shipDefl.FreightDocTab = this._pkgDocTab;
      }
      else
      {
        this._shipDefl.ExpressDocTab = this._pkgDocTab;
        this._shipDefl.ExpressTotalDocTab = this._totalDocTab;
        this._shipDefl.PrintExpressTotalDocTab = this._printTotalsDocTab;
      }
    }

    public void ObjectToScreen()
    {
      if (this._pkgDocTab != null)
      {
        if (this._pkgDocTab.LabelList.Count > 0)
          this.packageDocTab.edtDocTabLabel1.Text = this._pkgDocTab.LabelList[0];
        if (this._pkgDocTab.LabelList.Count > 1)
          this.packageDocTab.edtDocTabLabel2.Text = this._pkgDocTab.LabelList[1];
        if (this._pkgDocTab.LabelList.Count > 2)
          this.packageDocTab.edtDocTabLabel3.Text = this._pkgDocTab.LabelList[2];
        if (this._pkgDocTab.LabelList.Count > 3)
          this.packageDocTab.edtDocTabLabel4.Text = this._pkgDocTab.LabelList[3];
        if (this._pkgDocTab.LabelList.Count > 4)
          this.packageDocTab.edtDocTabLabel5.Text = this._pkgDocTab.LabelList[4];
        if (this._pkgDocTab.LabelList.Count > 5)
          this.packageDocTab.edtDocTabLabel6.Text = this._pkgDocTab.LabelList[5];
        if (this._pkgDocTab.LabelList.Count > 6)
          this.packageDocTab.edtDocTabLabel7.Text = this._pkgDocTab.LabelList[6];
        if (this._pkgDocTab.LabelList.Count > 7)
          this.packageDocTab.edtDocTabLabel8.Text = this._pkgDocTab.LabelList[7];
        if (this._pkgDocTab.LabelList.Count > 8)
          this.packageDocTab.edtDocTabLabel9.Text = this._pkgDocTab.LabelList[8];
        if (this._pkgDocTab.LabelList.Count > 9)
          this.packageDocTab.edtDocTabLabel10.Text = this._pkgDocTab.LabelList[9];
        if (this._pkgDocTab.LabelList.Count > 10)
          this.packageDocTab.edtDocTabLabel11.Text = this._pkgDocTab.LabelList[10];
        if (this._pkgDocTab.LabelList.Count > 11)
          this.packageDocTab.edtDocTabLabel12.Text = this._pkgDocTab.LabelList[11];
        if (this._pkgDocTab.LabelList.Count > 12)
          this.packageDocTab.edtDocTabLabel13.Text = this._pkgDocTab.LabelList[12];
        if (this._pkgDocTab.LabelList.Count > 13)
          this.packageDocTab.edtDocTabLabel14.Text = this._pkgDocTab.LabelList[13];
        if (this._pkgDocTab.DataList.Count > 0)
          this.packageDocTab.cboDocTabData1.SelectedValue = (object) this._pkgDocTab.DataList[0];
        if (this._pkgDocTab.DataList.Count > 1)
          this.packageDocTab.cboDocTabData2.SelectedValue = (object) this._pkgDocTab.DataList[1];
        if (this._pkgDocTab.DataList.Count > 2)
          this.packageDocTab.cboDocTabData3.SelectedValue = (object) this._pkgDocTab.DataList[2];
        if (this._pkgDocTab.DataList.Count > 3)
          this.packageDocTab.cboDocTabData4.SelectedValue = (object) this._pkgDocTab.DataList[3];
        if (this._pkgDocTab.DataList.Count > 4)
          this.packageDocTab.cboDocTabData5.SelectedValue = (object) this._pkgDocTab.DataList[4];
        if (this._pkgDocTab.DataList.Count > 5)
          this.packageDocTab.cboDocTabData6.SelectedValue = (object) this._pkgDocTab.DataList[5];
        if (this._pkgDocTab.DataList.Count > 6)
          this.packageDocTab.cboDocTabData7.SelectedValue = (object) this._pkgDocTab.DataList[6];
        if (this._pkgDocTab.DataList.Count > 7)
          this.packageDocTab.cboDocTabData8.SelectedValue = (object) this._pkgDocTab.DataList[7];
        if (this._pkgDocTab.DataList.Count > 8)
          this.packageDocTab.cboDocTabData9.SelectedValue = (object) this._pkgDocTab.DataList[8];
        if (this._pkgDocTab.DataList.Count > 9)
          this.packageDocTab.cboDocTabData10.SelectedValue = (object) this._pkgDocTab.DataList[9];
        if (this._pkgDocTab.DataList.Count > 10)
          this.packageDocTab.cboDocTabData11.SelectedValue = (object) this._pkgDocTab.DataList[10];
        if (this._pkgDocTab.DataList.Count > 11)
          this.packageDocTab.cboDocTabData12.SelectedValue = (object) this._pkgDocTab.DataList[11];
        if (this._pkgDocTab.DataList.Count > 12)
          this.packageDocTab.cboDocTabData13.SelectedValue = (object) this._pkgDocTab.DataList[12];
        if (this._pkgDocTab.DataList.Count > 13)
          this.packageDocTab.cboDocTabData14.SelectedValue = (object) this._pkgDocTab.DataList[13];
      }
      if (this._eCarrier == Shipment.CarrierType.Freight || this._eCarrier == Shipment.CarrierType.SmartPost)
        this._totalDocTab = (DocTab) null;
      if (this._totalDocTab == null)
        return;
      if (this._totalDocTab.LabelList.Count > 0)
        this.totalsDocTab.edtDocTabLabel1.Text = this._totalDocTab.LabelList[0];
      if (this._totalDocTab.LabelList.Count > 1)
        this.totalsDocTab.edtDocTabLabel2.Text = this._totalDocTab.LabelList[1];
      if (this._totalDocTab.LabelList.Count > 2)
        this.totalsDocTab.edtDocTabLabel3.Text = this._totalDocTab.LabelList[2];
      if (this._totalDocTab.LabelList.Count > 3)
        this.totalsDocTab.edtDocTabLabel4.Text = this._totalDocTab.LabelList[3];
      if (this._totalDocTab.LabelList.Count > 4)
        this.totalsDocTab.edtDocTabLabel5.Text = this._totalDocTab.LabelList[4];
      if (this._totalDocTab.LabelList.Count > 5)
        this.totalsDocTab.edtDocTabLabel6.Text = this._totalDocTab.LabelList[5];
      if (this._totalDocTab.LabelList.Count > 6)
        this.totalsDocTab.edtDocTabLabel7.Text = this._totalDocTab.LabelList[6];
      if (this._totalDocTab.LabelList.Count > 7)
        this.totalsDocTab.edtDocTabLabel8.Text = this._totalDocTab.LabelList[7];
      if (this._totalDocTab.LabelList.Count > 8)
        this.totalsDocTab.edtDocTabLabel9.Text = this._totalDocTab.LabelList[8];
      if (this._totalDocTab.LabelList.Count > 9)
        this.totalsDocTab.edtDocTabLabel10.Text = this._totalDocTab.LabelList[9];
      if (this._totalDocTab.LabelList.Count > 10)
        this.totalsDocTab.edtDocTabLabel11.Text = this._totalDocTab.LabelList[10];
      if (this._totalDocTab.LabelList.Count > 11)
        this.totalsDocTab.edtDocTabLabel12.Text = this._totalDocTab.LabelList[11];
      if (this._totalDocTab.LabelList.Count > 12)
        this.totalsDocTab.edtDocTabLabel13.Text = this._totalDocTab.LabelList[12];
      if (this._totalDocTab.LabelList.Count > 13)
        this.totalsDocTab.edtDocTabLabel14.Text = this._totalDocTab.LabelList[13];
      if (this._totalDocTab.DataList.Count > 0)
        this.totalsDocTab.cboDocTabData1.SelectedValue = (object) this._totalDocTab.DataList[0];
      if (this._totalDocTab.DataList.Count > 1)
        this.totalsDocTab.cboDocTabData2.SelectedValue = (object) this._totalDocTab.DataList[1];
      if (this._totalDocTab.DataList.Count > 2)
        this.totalsDocTab.cboDocTabData3.SelectedValue = (object) this._totalDocTab.DataList[2];
      if (this._totalDocTab.DataList.Count > 3)
        this.totalsDocTab.cboDocTabData4.SelectedValue = (object) this._totalDocTab.DataList[3];
      if (this._totalDocTab.DataList.Count > 4)
        this.totalsDocTab.cboDocTabData5.SelectedValue = (object) this._totalDocTab.DataList[4];
      if (this._totalDocTab.DataList.Count > 5)
        this.totalsDocTab.cboDocTabData6.SelectedValue = (object) this._totalDocTab.DataList[5];
      if (this._totalDocTab.DataList.Count > 6)
        this.totalsDocTab.cboDocTabData7.SelectedValue = (object) this._totalDocTab.DataList[6];
      if (this._totalDocTab.DataList.Count > 7)
        this.totalsDocTab.cboDocTabData8.SelectedValue = (object) this._totalDocTab.DataList[7];
      if (this._totalDocTab.DataList.Count > 8)
        this.totalsDocTab.cboDocTabData9.SelectedValue = (object) this._totalDocTab.DataList[8];
      if (this._totalDocTab.DataList.Count > 9)
        this.totalsDocTab.cboDocTabData10.SelectedValue = (object) this._totalDocTab.DataList[9];
      if (this._totalDocTab.DataList.Count > 10)
        this.totalsDocTab.cboDocTabData11.SelectedValue = (object) this._totalDocTab.DataList[10];
      if (this._totalDocTab.DataList.Count > 11)
        this.totalsDocTab.cboDocTabData12.SelectedValue = (object) this._totalDocTab.DataList[11];
      if (this._totalDocTab.DataList.Count > 12)
        this.totalsDocTab.cboDocTabData13.SelectedValue = (object) this._totalDocTab.DataList[12];
      if (this._totalDocTab.DataList.Count > 13)
        this.totalsDocTab.cboDocTabData14.SelectedValue = (object) this._totalDocTab.DataList[13];
      this.totalsDocTab.chkPrintTotalsDocTab.Checked = this._printTotalsDocTab;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      this.ScreenToObject();
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocTabConfigDlg));
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.tabControl1 = new TabControlEx();
      this.tabPkgDocTab = new TabPage();
      this.packageDocTab = new ucDocTab();
      this.tabTotalsDocTab = new TabPage();
      this.totalsDocTab = new ucDocTab();
      this.tabControl1.SuspendLayout();
      this.tabPkgDocTab.SuspendLayout();
      this.tabTotalsDocTab.SuspendLayout();
      this.SuspendLayout();
      this.btnOK.DialogResult = DialogResult.OK;
      componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
      this.btnOK.Name = "btnOK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.DialogResult = DialogResult.Cancel;
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.tabControl1.Controls.Add((Control) this.tabPkgDocTab);
      this.tabControl1.Controls.Add((Control) this.tabTotalsDocTab);
      this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
      componentResourceManager.ApplyResources((object) this.tabControl1, "tabControl1");
      this.tabControl1.MnemonicEnabled = true;
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.UseIndexAsMnemonic = true;
      this.tabPkgDocTab.Controls.Add((Control) this.packageDocTab);
      componentResourceManager.ApplyResources((object) this.tabPkgDocTab, "tabPkgDocTab");
      this.tabPkgDocTab.Name = "tabPkgDocTab";
      this.tabPkgDocTab.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.packageDocTab, "packageDocTab");
      this.packageDocTab.Name = "packageDocTab";
      this.tabTotalsDocTab.Controls.Add((Control) this.totalsDocTab);
      componentResourceManager.ApplyResources((object) this.tabTotalsDocTab, "tabTotalsDocTab");
      this.tabTotalsDocTab.Name = "tabTotalsDocTab";
      this.tabTotalsDocTab.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.totalsDocTab, "totalsDocTab");
      this.totalsDocTab.Name = "totalsDocTab";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.tabControl1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (DocTabConfigDlg);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Load += new EventHandler(this.DocTabDlg_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPkgDocTab.ResumeLayout(false);
      this.tabTotalsDocTab.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
