// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.FormAlignmentOP900LL
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using FedEx.Gsm.ShipEngine.Reports.ReportLogic;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class FormAlignmentOP900LL : HelpFormBase
  {
    private string saveLeft;
    private string saveRight;
    private string saveTop;
    private string saveBottom;
    private string saveShiftLeftMeasure;
    private FedEx.Gsm.Common.ConfigManager.ConfigManager cm = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.REPORTLOGIC);
    private InitializeStaticReport OP900LL = new InitializeStaticReport();
    private IContainer components;
    private Label label1;
    private Label lblShiftLeft;
    private Label lblShiftLeftMeasure;
    private Label lblShiftBottomMeasure;
    private Label lblShiftTopMeasure;
    private Label lblShiftRightMeasure;
    private Label lblShiftRight;
    private Label lblShiftTop;
    private Label lblShifBottom;
    private Button btnPrintForm;
    private Button btnCancel;
    private Button btnOK;
    private RadioButton rdoMillimeters;
    private RadioButton rdoInches;
    private ColorGroupBox gbxInternationalInfo;
    private FdxMaskedEdit txtLeft;
    private FdxMaskedEdit txtTop;
    private FdxMaskedEdit txtRight;
    private FdxMaskedEdit txtBottom;

    public FormAlignmentOP900LL()
    {
      this.InitializeComponent();
      Utility.HouseKeeping(this.Controls);
      string str = "";
      this.cm.GetProfileString("SETTINGS", "ShiftLeft", out str, "0");
      this.txtLeft.Text = str;
      this.saveLeft = str;
      this.cm.GetProfileString("SETTINGS", "ShiftRight", out str, "0");
      this.txtRight.Text = str;
      this.saveRight = str;
      this.cm.GetProfileString("SETTINGS", "ShiftTop", out str, "0");
      this.txtTop.Text = str;
      this.saveTop = str;
      this.cm.GetProfileString("SETTINGS", "ShiftBottom", out str, "0");
      this.txtBottom.Text = str;
      this.saveBottom = str;
      this.cm.GetProfileString("SETTINGS", "ShiftMeasure", out str, "mm");
      this.lblShiftLeftMeasure.Text = str;
      this.lblShiftRightMeasure.Text = str;
      this.lblShiftTopMeasure.Text = str;
      this.lblShiftBottomMeasure.Text = str;
      this.saveShiftLeftMeasure = str;
      if (str.Equals("mm"))
      {
        this.rdoMillimeters.Checked = true;
        this.rdoInches.Checked = false;
      }
      else
      {
        this.rdoMillimeters.Checked = false;
        this.rdoInches.Checked = true;
      }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      this.cm.SetProfileValue("SETTINGS", "ShiftLeft", (object) this.txtLeft.Text.ToString());
      this.cm.SetProfileValue("SETTINGS", "ShiftRight", (object) this.txtRight.Text.ToString());
      this.cm.SetProfileValue("SETTINGS", "ShiftTop", (object) this.txtTop.Text.ToString());
      this.cm.SetProfileValue("SETTINGS", "ShiftBottom", (object) this.txtBottom.Text.ToString());
      this.cm.SetProfileValue("SETTINGS", "ShiftMeasure", (object) this.lblShiftLeftMeasure.Text.ToString());
    }

    private void txtLeft_Leave(object sender, EventArgs e)
    {
      this.cm.SetProfileValue("SETTINGS", "ShiftLeft", (object) this.txtLeft.Text.ToString());
    }

    private void txtRight_Leave(object sender, EventArgs e)
    {
      this.cm.SetProfileValue("SETTINGS", "ShiftRight", (object) this.txtRight.Text.ToString());
    }

    private void txtTop_Leave(object sender, EventArgs e)
    {
      this.cm.SetProfileValue("SETTINGS", "ShiftTop", (object) this.txtTop.Text.ToString());
    }

    private void txtBottom_Leave(object sender, EventArgs e)
    {
      this.cm.SetProfileValue("SETTINGS", "ShiftBottom", (object) this.txtBottom.Text.ToString());
    }

    private void rdoMillimeters_Click(object sender, EventArgs e)
    {
      this.lblShiftLeftMeasure.Text = "mm";
      this.lblShiftRightMeasure.Text = "mm";
      this.lblShiftTopMeasure.Text = "mm";
      this.lblShiftBottomMeasure.Text = "mm";
      this.txtLeft.Text = (Convert.ToDouble(string.IsNullOrEmpty(this.txtLeft.Text.ToString()) ? "0" : this.txtLeft.Text.ToString()) / (5.0 / (double) sbyte.MaxValue)).ToString();
      this.txtRight.Text = (Convert.ToDouble(string.IsNullOrEmpty(this.txtRight.Text.ToString()) ? "0" : this.txtRight.Text.ToString()) / (5.0 / (double) sbyte.MaxValue)).ToString();
      this.txtTop.Text = (Convert.ToDouble(string.IsNullOrEmpty(this.txtTop.Text.ToString()) ? "0" : this.txtTop.Text.ToString()) / (5.0 / (double) sbyte.MaxValue)).ToString();
      this.txtBottom.Text = (Convert.ToDouble(string.IsNullOrEmpty(this.txtBottom.Text.ToString()) ? "0" : this.txtBottom.Text.ToString()) / (5.0 / (double) sbyte.MaxValue)).ToString();
    }

    private void rdoInches_Click(object sender, EventArgs e)
    {
      this.lblShiftLeftMeasure.Text = "in";
      this.lblShiftRightMeasure.Text = "in";
      this.lblShiftTopMeasure.Text = "in";
      this.lblShiftBottomMeasure.Text = "in";
      this.txtLeft.Text = (Convert.ToDouble(string.IsNullOrEmpty(this.txtLeft.Text.ToString()) ? "0" : this.txtLeft.Text.ToString()) * (5.0 / (double) sbyte.MaxValue)).ToString();
      this.txtRight.Text = (Convert.ToDouble(string.IsNullOrEmpty(this.txtRight.Text.ToString()) ? "0" : this.txtRight.Text.ToString()) * (5.0 / (double) sbyte.MaxValue)).ToString();
      this.txtTop.Text = (Convert.ToDouble(string.IsNullOrEmpty(this.txtTop.Text.ToString()) ? "0" : this.txtTop.Text.ToString()) * (5.0 / (double) sbyte.MaxValue)).ToString();
      this.txtBottom.Text = (Convert.ToDouble(string.IsNullOrEmpty(this.txtBottom.Text.ToString()) ? "0" : this.txtBottom.Text.ToString()) * (5.0 / (double) sbyte.MaxValue)).ToString();
    }

    private void btnPrintForm_Click(object sender, EventArgs e)
    {
      this.cm.SetProfileValue("SETTINGS", "ShiftLeft", (object) this.txtLeft.Text.ToString());
      this.cm.SetProfileValue("SETTINGS", "ShiftRight", (object) this.txtRight.Text.ToString());
      this.cm.SetProfileValue("SETTINGS", "ShiftTop", (object) this.txtTop.Text.ToString());
      this.cm.SetProfileValue("SETTINGS", "ShiftBottom", (object) this.txtBottom.Text.ToString());
      this.cm.SetProfileValue("SETTINGS", "ShiftMeasure", (object) this.lblShiftLeftMeasure.Text.ToString());
      this.OP900LL.InitForm((Shipment) null, 0, true, GuiData.CurrentAccount.AccountNumber, GuiData.CurrentAccount.MeterNumber, "OP900LL");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.cm.SetProfileValue("SETTINGS", "ShiftLeft", (object) this.saveLeft);
      this.cm.SetProfileValue("SETTINGS", "ShiftRight", (object) this.saveRight);
      this.cm.SetProfileValue("SETTINGS", "ShiftTop", (object) this.saveTop);
      this.cm.SetProfileValue("SETTINGS", "ShiftBottom", (object) this.saveBottom);
      this.cm.SetProfileValue("SETTINGS", "ShiftMeasure", (object) this.saveShiftLeftMeasure);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormAlignmentOP900LL));
      this.label1 = new Label();
      this.btnCancel = new Button();
      this.btnOK = new Button();
      this.gbxInternationalInfo = new ColorGroupBox();
      this.txtBottom = new FdxMaskedEdit();
      this.txtTop = new FdxMaskedEdit();
      this.txtRight = new FdxMaskedEdit();
      this.txtLeft = new FdxMaskedEdit();
      this.lblShiftLeft = new Label();
      this.rdoInches = new RadioButton();
      this.rdoMillimeters = new RadioButton();
      this.btnPrintForm = new Button();
      this.lblShiftLeftMeasure = new Label();
      this.lblShifBottom = new Label();
      this.lblShiftBottomMeasure = new Label();
      this.lblShiftTop = new Label();
      this.lblShiftTopMeasure = new Label();
      this.lblShiftRight = new Label();
      this.lblShiftRightMeasure = new Label();
      this.gbxInternationalInfo.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.btnCancel.DialogResult = DialogResult.Cancel;
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnOK.DialogResult = DialogResult.OK;
      componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
      this.btnOK.Name = "btnOK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.gbxInternationalInfo.BorderThickness = 1f;
      this.gbxInternationalInfo.Controls.Add((Control) this.txtBottom);
      this.gbxInternationalInfo.Controls.Add((Control) this.txtTop);
      this.gbxInternationalInfo.Controls.Add((Control) this.txtRight);
      this.gbxInternationalInfo.Controls.Add((Control) this.txtLeft);
      this.gbxInternationalInfo.Controls.Add((Control) this.lblShiftLeft);
      this.gbxInternationalInfo.Controls.Add((Control) this.rdoInches);
      this.gbxInternationalInfo.Controls.Add((Control) this.rdoMillimeters);
      this.gbxInternationalInfo.Controls.Add((Control) this.btnPrintForm);
      this.gbxInternationalInfo.Controls.Add((Control) this.lblShiftLeftMeasure);
      this.gbxInternationalInfo.Controls.Add((Control) this.lblShifBottom);
      this.gbxInternationalInfo.Controls.Add((Control) this.lblShiftBottomMeasure);
      this.gbxInternationalInfo.Controls.Add((Control) this.lblShiftTop);
      this.gbxInternationalInfo.Controls.Add((Control) this.lblShiftTopMeasure);
      this.gbxInternationalInfo.Controls.Add((Control) this.lblShiftRight);
      this.gbxInternationalInfo.Controls.Add((Control) this.lblShiftRightMeasure);
      this.gbxInternationalInfo.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxInternationalInfo, "gbxInternationalInfo");
      this.gbxInternationalInfo.Name = "gbxInternationalInfo";
      this.gbxInternationalInfo.RoundCorners = 5;
      this.gbxInternationalInfo.TabStop = false;
      this.txtBottom.Allow = "";
      this.txtBottom.Disallow = "";
      this.txtBottom.eMask = eMasks.maskCustom;
      this.txtBottom.FillFrom = LeftRightAlignment.Left;
      componentResourceManager.ApplyResources((object) this.txtBottom, "txtBottom");
      this.txtBottom.Mask = "99999.999999999999";
      this.txtBottom.Name = "txtBottom";
      this.txtBottom.Leave += new EventHandler(this.txtBottom_Leave);
      this.txtTop.Allow = "";
      this.txtTop.Disallow = "";
      this.txtTop.eMask = eMasks.maskCustom;
      this.txtTop.FillFrom = LeftRightAlignment.Left;
      componentResourceManager.ApplyResources((object) this.txtTop, "txtTop");
      this.txtTop.Mask = "99999.999999999999";
      this.txtTop.Name = "txtTop";
      this.txtTop.Leave += new EventHandler(this.txtTop_Leave);
      this.txtRight.Allow = "";
      this.txtRight.Disallow = "";
      this.txtRight.eMask = eMasks.maskCustom;
      this.txtRight.FillFrom = LeftRightAlignment.Left;
      componentResourceManager.ApplyResources((object) this.txtRight, "txtRight");
      this.txtRight.Mask = "99999.999999999999";
      this.txtRight.Name = "txtRight";
      this.txtRight.Leave += new EventHandler(this.txtRight_Leave);
      this.txtLeft.Allow = "";
      this.txtLeft.Disallow = "";
      this.txtLeft.eMask = eMasks.maskCustom;
      this.txtLeft.FillFrom = LeftRightAlignment.Left;
      componentResourceManager.ApplyResources((object) this.txtLeft, "txtLeft");
      this.txtLeft.Mask = "99999.999999999999";
      this.txtLeft.Name = "txtLeft";
      this.txtLeft.Leave += new EventHandler(this.txtLeft_Leave);
      componentResourceManager.ApplyResources((object) this.lblShiftLeft, "lblShiftLeft");
      this.lblShiftLeft.Name = "lblShiftLeft";
      componentResourceManager.ApplyResources((object) this.rdoInches, "rdoInches");
      this.rdoInches.Name = "rdoInches";
      this.rdoInches.UseVisualStyleBackColor = true;
      this.rdoInches.Click += new EventHandler(this.rdoInches_Click);
      componentResourceManager.ApplyResources((object) this.rdoMillimeters, "rdoMillimeters");
      this.rdoMillimeters.Checked = true;
      this.rdoMillimeters.Name = "rdoMillimeters";
      this.rdoMillimeters.TabStop = true;
      this.rdoMillimeters.UseVisualStyleBackColor = true;
      this.rdoMillimeters.Click += new EventHandler(this.rdoMillimeters_Click);
      componentResourceManager.ApplyResources((object) this.btnPrintForm, "btnPrintForm");
      this.btnPrintForm.Name = "btnPrintForm";
      this.btnPrintForm.UseVisualStyleBackColor = true;
      this.btnPrintForm.Click += new EventHandler(this.btnPrintForm_Click);
      componentResourceManager.ApplyResources((object) this.lblShiftLeftMeasure, "lblShiftLeftMeasure");
      this.lblShiftLeftMeasure.Name = "lblShiftLeftMeasure";
      componentResourceManager.ApplyResources((object) this.lblShifBottom, "lblShifBottom");
      this.lblShifBottom.Name = "lblShifBottom";
      componentResourceManager.ApplyResources((object) this.lblShiftBottomMeasure, "lblShiftBottomMeasure");
      this.lblShiftBottomMeasure.Name = "lblShiftBottomMeasure";
      componentResourceManager.ApplyResources((object) this.lblShiftTop, "lblShiftTop");
      this.lblShiftTop.Name = "lblShiftTop";
      componentResourceManager.ApplyResources((object) this.lblShiftTopMeasure, "lblShiftTopMeasure");
      this.lblShiftTopMeasure.Name = "lblShiftTopMeasure";
      componentResourceManager.ApplyResources((object) this.lblShiftRight, "lblShiftRight");
      this.lblShiftRight.Name = "lblShiftRight";
      componentResourceManager.ApplyResources((object) this.lblShiftRightMeasure, "lblShiftRightMeasure");
      this.lblShiftRightMeasure.Name = "lblShiftRightMeasure";
      this.AcceptButton = (IButtonControl) this.btnOK;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.ControlBox = false;
      this.Controls.Add((Control) this.gbxInternationalInfo);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FormAlignmentOP900LL);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.gbxInternationalInfo.ResumeLayout(false);
      this.gbxInternationalInfo.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
