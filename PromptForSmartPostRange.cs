// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.PromptForSmartPostRange
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class PromptForSmartPostRange : HelpFormBase
  {
    private static string _startTrkNumber = "";
    private static string _nextTrkNumber = "";
    private static string _endTrkNumber = "";
    private IContainer components;
    private Label label1;
    private Label label2;
    private Label label3;
    private GroupBox groupBox1;
    private Button btnOK;
    private Button btnCancel;
    private FdxMaskedEdit edtEndTrkNumber;
    private FdxMaskedEdit edtStartTrkNumber;
    private FdxMaskedEdit edtNextTrkNumber;
    private RichTextBox richTextBox1;

    internal string StartTrkNumber
    {
      get => PromptForSmartPostRange._startTrkNumber;
      set => PromptForSmartPostRange._startTrkNumber = value;
    }

    internal string NextTrkNumber
    {
      get => PromptForSmartPostRange._nextTrkNumber;
      set => PromptForSmartPostRange._nextTrkNumber = value;
    }

    internal string EndTrkNumber
    {
      get => PromptForSmartPostRange._endTrkNumber;
      set => PromptForSmartPostRange._endTrkNumber = value;
    }

    public PromptForSmartPostRange() => this.InitializeComponent();

    private void btnCancel_Click(object sender, EventArgs e) => this.Dispose();

    private void btnOK_Click(object sender, EventArgs e)
    {
      PromptForSmartPostRange._nextTrkNumber = this.edtNextTrkNumber.Text;
      PromptForSmartPostRange._startTrkNumber = this.edtStartTrkNumber.Text;
      PromptForSmartPostRange._endTrkNumber = this.edtEndTrkNumber.Text;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PromptForSmartPostRange));
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.groupBox1 = new GroupBox();
      this.richTextBox1 = new RichTextBox();
      this.edtEndTrkNumber = new FdxMaskedEdit();
      this.edtStartTrkNumber = new FdxMaskedEdit();
      this.edtNextTrkNumber = new FdxMaskedEdit();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      componentResourceManager.ApplyResources((object) this.label3, "label3");
      this.label3.Name = "label3";
      this.groupBox1.Controls.Add((Control) this.richTextBox1);
      this.groupBox1.Controls.Add((Control) this.edtEndTrkNumber);
      this.groupBox1.Controls.Add((Control) this.edtStartTrkNumber);
      this.groupBox1.Controls.Add((Control) this.edtNextTrkNumber);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.label2);
      componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.TabStop = false;
      this.richTextBox1.BorderStyle = BorderStyle.None;
      componentResourceManager.ApplyResources((object) this.richTextBox1, "richTextBox1");
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.TabStop = false;
      this.edtEndTrkNumber.Allow = "";
      this.edtEndTrkNumber.AllowPromptAsInput = false;
      this.edtEndTrkNumber.AsciiOnly = true;
      this.edtEndTrkNumber.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
      this.edtEndTrkNumber.Disallow = "";
      this.edtEndTrkNumber.eMask = eMasks.maskCustom;
      this.edtEndTrkNumber.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.edtEndTrkNumber, componentResourceManager.GetString("edtEndTrkNumber.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtEndTrkNumber, (HelpNavigator) componentResourceManager.GetObject("edtEndTrkNumber.HelpNavigator"));
      this.edtEndTrkNumber.HidePromptOnLeave = false;
      this.edtEndTrkNumber.InsertKeyMode = InsertKeyMode.Default;
      componentResourceManager.ApplyResources((object) this.edtEndTrkNumber, "edtEndTrkNumber");
      this.edtEndTrkNumber.Mask = "99999999";
      this.edtEndTrkNumber.Name = "edtEndTrkNumber";
      this.edtEndTrkNumber.Prefix = "";
      this.edtEndTrkNumber.PromptChar = ' ';
      this.edtEndTrkNumber.Raw = "";
      this.edtEndTrkNumber.Shift = false;
      this.helpProvider1.SetShowHelp((Control) this.edtEndTrkNumber, (bool) componentResourceManager.GetObject("edtEndTrkNumber.ShowHelp"));
      this.edtEndTrkNumber.Suffix = "";
      this.edtEndTrkNumber.Testing = (string) null;
      this.edtEndTrkNumber.TextMaskFormat = MaskFormat.IncludeLiterals;
      this.edtEndTrkNumber.ValidatingType = typeof (DateTime);
      this.edtStartTrkNumber.Allow = "";
      this.edtStartTrkNumber.AllowPromptAsInput = false;
      this.edtStartTrkNumber.AsciiOnly = true;
      this.edtStartTrkNumber.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
      this.edtStartTrkNumber.Disallow = "";
      this.edtStartTrkNumber.eMask = eMasks.maskCustom;
      this.edtStartTrkNumber.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.edtStartTrkNumber, componentResourceManager.GetString("edtStartTrkNumber.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtStartTrkNumber, (HelpNavigator) componentResourceManager.GetObject("edtStartTrkNumber.HelpNavigator"));
      this.edtStartTrkNumber.HidePromptOnLeave = false;
      this.edtStartTrkNumber.InsertKeyMode = InsertKeyMode.Default;
      componentResourceManager.ApplyResources((object) this.edtStartTrkNumber, "edtStartTrkNumber");
      this.edtStartTrkNumber.Mask = "99999999";
      this.edtStartTrkNumber.Name = "edtStartTrkNumber";
      this.edtStartTrkNumber.Prefix = "";
      this.edtStartTrkNumber.PromptChar = ' ';
      this.edtStartTrkNumber.Raw = "";
      this.edtStartTrkNumber.Shift = false;
      this.helpProvider1.SetShowHelp((Control) this.edtStartTrkNumber, (bool) componentResourceManager.GetObject("edtStartTrkNumber.ShowHelp"));
      this.edtStartTrkNumber.Suffix = "";
      this.edtStartTrkNumber.Testing = (string) null;
      this.edtStartTrkNumber.TextMaskFormat = MaskFormat.IncludeLiterals;
      this.edtStartTrkNumber.ValidatingType = typeof (DateTime);
      this.edtNextTrkNumber.Allow = "";
      this.edtNextTrkNumber.AllowPromptAsInput = false;
      this.edtNextTrkNumber.AsciiOnly = true;
      this.edtNextTrkNumber.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
      this.edtNextTrkNumber.Disallow = "";
      this.edtNextTrkNumber.eMask = eMasks.maskCustom;
      this.edtNextTrkNumber.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.edtNextTrkNumber, componentResourceManager.GetString("edtNextTrkNumber.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtNextTrkNumber, (HelpNavigator) componentResourceManager.GetObject("edtNextTrkNumber.HelpNavigator"));
      this.edtNextTrkNumber.HidePromptOnLeave = false;
      this.edtNextTrkNumber.InsertKeyMode = InsertKeyMode.Default;
      componentResourceManager.ApplyResources((object) this.edtNextTrkNumber, "edtNextTrkNumber");
      this.edtNextTrkNumber.Mask = "99999999";
      this.edtNextTrkNumber.Name = "edtNextTrkNumber";
      this.edtNextTrkNumber.Prefix = "";
      this.edtNextTrkNumber.PromptChar = ' ';
      this.edtNextTrkNumber.Raw = "";
      this.edtNextTrkNumber.Shift = false;
      this.helpProvider1.SetShowHelp((Control) this.edtNextTrkNumber, (bool) componentResourceManager.GetObject("edtNextTrkNumber.ShowHelp"));
      this.edtNextTrkNumber.Suffix = "";
      this.edtNextTrkNumber.Testing = (string) null;
      this.edtNextTrkNumber.TextMaskFormat = MaskFormat.IncludeLiterals;
      this.edtNextTrkNumber.ValidatingType = typeof (DateTime);
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
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.groupBox1);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (PromptForSmartPostRange);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
