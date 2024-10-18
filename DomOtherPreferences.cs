// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.DomOtherPreferences
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class DomOtherPreferences : OtherPreferences
  {
    private DShipDefl _domShipDefl;
    private IContainer components;

    public DomOtherPreferences() => this.InitializeComponent();

    private void DomOtherPreferences_Load(object sender, EventArgs e)
    {
      if (this._carrier == Shipment.CarrierType.Express)
      {
        if (!this.DesignMode && GuiData.CurrentAccount.Address.CountryCode == "MX")
        {
          this.gbxReferenceLabel.Visible = false;
          this.txtCurrency.Text = "NMP";
        }
        this.chkOverrideGroundToHome.Visible = false;
        this.chkGroundHomeToggle.Visible = false;
      }
      else if (this._carrier == Shipment.CarrierType.Ground)
      {
        this.chkTrackingNbrOverwrite.Visible = false;
        this.gbxReferenceLabel.Visible = false;
        if (!this.DesignMode)
        {
          this.lblLabelFormat.Text = GuiData.Languafier.Translate("FedExGroundLabelFormat");
          this.chkOverrideGroundToHome.Visible = GuiData.CurrentAccount.Address.CountryCode == "US";
        }
      }
      else if (this._carrier == Shipment.CarrierType.SmartPost)
      {
        this.chkOverrideGroundToHome.Visible = false;
        this.chkGroundHomeToggle.Visible = false;
        this.chkTrackingNbrOverwrite.Visible = false;
        this.gbxReferenceLabel.Visible = false;
        if (!this.DesignMode)
        {
          this.lblLabelFormat.Text = GuiData.Languafier.Translate("FedExSmartPostLabelFormat");
          object selectedValue = this.cboLabelFormat.SelectedValue;
          if (this.cboLabelFormat.DataSource is DataTable dataSource)
            dataSource.DefaultView.RowFilter = "Code <> 12 AND Code <> 13";
          if (selectedValue != null)
            this.cboLabelFormat.SelectedValue = selectedValue;
        }
      }
      if (this.DesignMode || !("CA" == GuiData.CurrentAccount.Address.CountryCode))
        return;
      this.txtCurrency.Text = "CAD";
    }

    public void InitOtherPrefs(DShipDefl domShipDefl, Shipment.CarrierType carrier)
    {
      this._domShipDefl = domShipDefl;
      if (this.DesignMode)
        return;
      this.InitOtherPrefs((ShipDefl) this._domShipDefl, carrier);
    }

    public override void ObjectToScreen()
    {
      if (this._carrier == Shipment.CarrierType.Express)
        this.chkTrackingNbrOverwrite.Checked = this._domShipDefl.TrkngNbrOverwrite;
      base.ObjectToScreen();
    }

    public override void ScreenToObject()
    {
      if (this._carrier == Shipment.CarrierType.Express)
        this._domShipDefl.TrkngNbrOverwrite = this.chkTrackingNbrOverwrite.Checked;
      base.ScreenToObject();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DomOtherPreferences));
      this.panelHandlingCharge.SuspendLayout();
      this.SuspendLayout();
      this.helpProvider1.SetHelpKeyword((Control) this.chkRequireReferences, componentResourceManager.GetString("chkRequireReferences.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkRequireReferences, (HelpNavigator) componentResourceManager.GetObject("chkRequireReferences.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chkRequireReferences, (bool) componentResourceManager.GetObject("chkRequireReferences.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.cboInsertInReferences, componentResourceManager.GetString("cboInsertInReferences.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboInsertInReferences, (HelpNavigator) componentResourceManager.GetObject("cboInsertInReferences.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboInsertInReferences, (bool) componentResourceManager.GetObject("cboInsertInReferences.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chk3DayFreight, componentResourceManager.GetString("chk3DayFreight.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chk3DayFreight, (HelpNavigator) componentResourceManager.GetObject("chk3DayFreight.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chk3DayFreight, (bool) componentResourceManager.GetObject("chk3DayFreight.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chk2DayFreight, componentResourceManager.GetString("chk2DayFreight.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chk2DayFreight, (HelpNavigator) componentResourceManager.GetObject("chk2DayFreight.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chk2DayFreight, (bool) componentResourceManager.GetObject("chk2DayFreight.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chk1DayFreight, componentResourceManager.GetString("chk1DayFreight.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chk1DayFreight, (HelpNavigator) componentResourceManager.GetObject("chk1DayFreight.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chk1DayFreight, (bool) componentResourceManager.GetObject("chk1DayFreight.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkIncludeAdditionalHandlingCharge, componentResourceManager.GetString("chkIncludeAdditionalHandlingCharge.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkIncludeAdditionalHandlingCharge, (HelpNavigator) componentResourceManager.GetObject("chkIncludeAdditionalHandlingCharge.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chkIncludeAdditionalHandlingCharge, (bool) componentResourceManager.GetObject("chkIncludeAdditionalHandlingCharge.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.cboVariableChargeInd, componentResourceManager.GetString("cboVariableChargeInd.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboVariableChargeInd, (HelpNavigator) componentResourceManager.GetObject("cboVariableChargeInd.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboVariableChargeInd, (bool) componentResourceManager.GetObject("cboVariableChargeInd.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.edtVariable, componentResourceManager.GetString("edtVariable.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtVariable, (HelpNavigator) componentResourceManager.GetObject("edtVariable.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.edtVariable, (bool) componentResourceManager.GetObject("edtVariable.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.edtFixed, componentResourceManager.GetString("edtFixed.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtFixed, (HelpNavigator) componentResourceManager.GetObject("edtFixed.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.edtFixed, (bool) componentResourceManager.GetObject("edtFixed.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkVariable, componentResourceManager.GetString("chkVariable.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkVariable, (HelpNavigator) componentResourceManager.GetObject("chkVariable.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chkVariable, (bool) componentResourceManager.GetObject("chkVariable.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkFixed, componentResourceManager.GetString("chkFixed.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkFixed, (HelpNavigator) componentResourceManager.GetObject("chkFixed.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chkFixed, (bool) componentResourceManager.GetObject("chkFixed.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.cboLabelFormat, componentResourceManager.GetString("cboLabelFormat.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboLabelFormat, (HelpNavigator) componentResourceManager.GetObject("cboLabelFormat.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.cboLabelFormat, (bool) componentResourceManager.GetObject("cboLabelFormat.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdoCustomizeDocTab, componentResourceManager.GetString("rdoCustomizeDocTab.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoCustomizeDocTab, (HelpNavigator) componentResourceManager.GetObject("rdoCustomizeDocTab.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdoCustomizeDocTab, (bool) componentResourceManager.GetObject("rdoCustomizeDocTab.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.rdoDefaultDocTab, componentResourceManager.GetString("rdoDefaultDocTab.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdoDefaultDocTab, (HelpNavigator) componentResourceManager.GetObject("rdoDefaultDocTab.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.rdoDefaultDocTab, (bool) componentResourceManager.GetObject("rdoDefaultDocTab.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.btnDocTabConfig, componentResourceManager.GetString("btnDocTabConfig.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnDocTabConfig, (HelpNavigator) componentResourceManager.GetObject("btnDocTabConfig.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.btnDocTabConfig, (bool) componentResourceManager.GetObject("btnDocTabConfig.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkTrackingNbrOverwrite, componentResourceManager.GetString("chkTrackingNbrOverwrite.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkTrackingNbrOverwrite, (HelpNavigator) componentResourceManager.GetObject("chkTrackingNbrOverwrite.HelpNavigator"));
      this.helpProvider1.SetShowHelp((Control) this.chkTrackingNbrOverwrite, (bool) componentResourceManager.GetObject("chkTrackingNbrOverwrite.ShowHelp"));
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = nameof (DomOtherPreferences);
      this.Load += new EventHandler(this.DomOtherPreferences_Load);
      this.panelHandlingCharge.ResumeLayout(false);
      this.panelHandlingCharge.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
