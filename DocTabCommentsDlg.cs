// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.DocTabCommentsDlg
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

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
  public class DocTabCommentsDlg : HelpFormBase
  {
    private ShipDefl _shipPrefs;
    private Shipment.CarrierType _eCarrier;
    private IContainer components;
    public CheckBox chkIncludeDV;
    private GroupBox gbxDocTabComments;
    public FdxMaskedEdit edtDocTabComment3;
    public FdxMaskedEdit edtDocTabComment2;
    public FdxMaskedEdit edtDocTabComment1;
    private Label lblComment3;
    private Label lblComment2;
    private Label lblComment1;
    private Button btnOk;
    private Button btnCancel;

    public DocTabCommentsDlg(ShipDefl shipPrefs, Shipment.CarrierType eCarrier)
    {
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this._shipPrefs = shipPrefs;
      this._eCarrier = eCarrier;
    }

    private void DocTabCommentsDlg_Load(object sender, EventArgs e)
    {
      if (this._eCarrier == Shipment.CarrierType.Ground)
      {
        this.edtDocTabComment1.Text = this._shipPrefs.GroundDocTabComments[0];
        this.edtDocTabComment2.Text = this._shipPrefs.GroundDocTabComments[1];
        this.edtDocTabComment3.Text = this._shipPrefs.GroundDocTabComments[2];
        this.chkIncludeDV.Checked = this._shipPrefs.GroundIncludeDeclaredValue;
      }
      else if (this._eCarrier == Shipment.CarrierType.SmartPost)
      {
        this.edtDocTabComment1.Text = this._shipPrefs.SmartPostDocTabComments[0];
        this.edtDocTabComment2.Text = this._shipPrefs.SmartPostDocTabComments[1];
        this.edtDocTabComment3.Text = this._shipPrefs.SmartPostDocTabComments[2];
        this.chkIncludeDV.Checked = this._shipPrefs.SmartPostIncludeDeclaredValue;
      }
      else
      {
        this.edtDocTabComment1.Text = this._shipPrefs.DocTabComments[0];
        this.edtDocTabComment2.Text = this._shipPrefs.DocTabComments[1];
        this.edtDocTabComment3.Text = this._shipPrefs.DocTabComments[2];
        this.chkIncludeDV.Checked = this._shipPrefs.IncludeDeclaredValue;
      }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      this.ScreenToObject();
      this.DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void ScreenToObject()
    {
      if (this._eCarrier == Shipment.CarrierType.Ground)
      {
        this._shipPrefs.GroundDocTabComments = new List<string>(3);
        this._shipPrefs.GroundDocTabComments.Add(this.edtDocTabComment1.Text);
        this._shipPrefs.GroundDocTabComments.Add(this.edtDocTabComment2.Text);
        this._shipPrefs.GroundDocTabComments.Add(this.edtDocTabComment3.Text);
        this._shipPrefs.GroundIncludeDeclaredValue = this.chkIncludeDV.Checked;
      }
      else if (this._eCarrier == Shipment.CarrierType.SmartPost)
      {
        this._shipPrefs.SmartPostDocTabComments = new List<string>(3);
        this._shipPrefs.SmartPostDocTabComments.Add(this.edtDocTabComment1.Text);
        this._shipPrefs.SmartPostDocTabComments.Add(this.edtDocTabComment2.Text);
        this._shipPrefs.SmartPostDocTabComments.Add(this.edtDocTabComment3.Text);
        this._shipPrefs.SmartPostIncludeDeclaredValue = this.chkIncludeDV.Checked;
      }
      else
      {
        this._shipPrefs.DocTabComments = new List<string>(3);
        this._shipPrefs.DocTabComments.Add(this.edtDocTabComment1.Text);
        this._shipPrefs.DocTabComments.Add(this.edtDocTabComment2.Text);
        this._shipPrefs.DocTabComments.Add(this.edtDocTabComment3.Text);
        this._shipPrefs.IncludeDeclaredValue = this.chkIncludeDV.Checked;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocTabCommentsDlg));
      this.chkIncludeDV = new CheckBox();
      this.gbxDocTabComments = new GroupBox();
      this.edtDocTabComment3 = new FdxMaskedEdit();
      this.edtDocTabComment2 = new FdxMaskedEdit();
      this.edtDocTabComment1 = new FdxMaskedEdit();
      this.lblComment3 = new Label();
      this.lblComment2 = new Label();
      this.lblComment1 = new Label();
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.gbxDocTabComments.SuspendLayout();
      this.SuspendLayout();
      this.helpProvider1.SetHelpKeyword((Control) this.chkIncludeDV, componentResourceManager.GetString("chkIncludeDV.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkIncludeDV, (HelpNavigator) componentResourceManager.GetObject("chkIncludeDV.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkIncludeDV, "chkIncludeDV");
      this.chkIncludeDV.Name = "chkIncludeDV";
      this.helpProvider1.SetShowHelp((Control) this.chkIncludeDV, (bool) componentResourceManager.GetObject("chkIncludeDV.ShowHelp"));
      this.chkIncludeDV.UseVisualStyleBackColor = true;
      this.gbxDocTabComments.Controls.Add((Control) this.edtDocTabComment3);
      this.gbxDocTabComments.Controls.Add((Control) this.edtDocTabComment2);
      this.gbxDocTabComments.Controls.Add((Control) this.edtDocTabComment1);
      this.gbxDocTabComments.Controls.Add((Control) this.lblComment3);
      this.gbxDocTabComments.Controls.Add((Control) this.lblComment2);
      this.gbxDocTabComments.Controls.Add((Control) this.lblComment1);
      componentResourceManager.ApplyResources((object) this.gbxDocTabComments, "gbxDocTabComments");
      this.gbxDocTabComments.Name = "gbxDocTabComments";
      this.gbxDocTabComments.TabStop = false;
      this.edtDocTabComment3.Allow = "";
      this.edtDocTabComment3.Disallow = "";
      this.edtDocTabComment3.eMask = eMasks.maskCustom;
      this.edtDocTabComment3.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabComment3, componentResourceManager.GetString("edtDocTabComment3.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabComment3, (HelpNavigator) componentResourceManager.GetObject("edtDocTabComment3.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtDocTabComment3, "edtDocTabComment3");
      this.edtDocTabComment3.Mask = "IIIIIIIIIIIIIIIIIIIIIIIIIIIIII~!\"";
      this.edtDocTabComment3.Name = "edtDocTabComment3";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabComment3, (bool) componentResourceManager.GetObject("edtDocTabComment3.ShowHelp"));
      this.edtDocTabComment2.Allow = "";
      this.edtDocTabComment2.Disallow = "";
      this.edtDocTabComment2.eMask = eMasks.maskCustom;
      this.edtDocTabComment2.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabComment2, componentResourceManager.GetString("edtDocTabComment2.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabComment2, (HelpNavigator) componentResourceManager.GetObject("edtDocTabComment2.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtDocTabComment2, "edtDocTabComment2");
      this.edtDocTabComment2.Mask = "IIIIIIIIIIIIIIIIIIIIIIIIIIIIII~!\"";
      this.edtDocTabComment2.Name = "edtDocTabComment2";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabComment2, (bool) componentResourceManager.GetObject("edtDocTabComment2.ShowHelp"));
      this.edtDocTabComment1.Allow = "";
      this.edtDocTabComment1.Disallow = "";
      this.edtDocTabComment1.eMask = eMasks.maskCustom;
      this.edtDocTabComment1.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabComment1, componentResourceManager.GetString("edtDocTabComment1.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabComment1, (HelpNavigator) componentResourceManager.GetObject("edtDocTabComment1.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtDocTabComment1, "edtDocTabComment1");
      this.edtDocTabComment1.Mask = "IIIIIIIIIIIIIIIIIIIIIIIIIIIIII~!\"";
      this.edtDocTabComment1.Name = "edtDocTabComment1";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabComment1, (bool) componentResourceManager.GetObject("edtDocTabComment1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblComment3, "lblComment3");
      this.lblComment3.Name = "lblComment3";
      componentResourceManager.ApplyResources((object) this.lblComment2, "lblComment2");
      this.lblComment2.Name = "lblComment2";
      componentResourceManager.ApplyResources((object) this.lblComment1, "lblComment1");
      this.lblComment1.Name = "lblComment1";
      this.btnOk.DialogResult = DialogResult.OK;
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.Name = "btnOk";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.btnCancel.DialogResult = DialogResult.Cancel;
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.gbxDocTabComments);
      this.Controls.Add((Control) this.chkIncludeDV);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (DocTabCommentsDlg);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.DocTabCommentsDlg_Load);
      this.gbxDocTabComments.ResumeLayout(false);
      this.gbxDocTabComments.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
