// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SelectNewMeterAccount
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
  public class SelectNewMeterAccount : HelpFormBase
  {
    private static string _newMeterNumber = "";
    private static string _newAccountNumber = "";
    private static string _oldMeterNumber = "";
    private static string _oldAccountNumber = "";
    private IContainer components;
    private Button btnOK;
    private Button btnCancel;
    private GroupBox groupBox1;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label lblOldAccountNumber;
    private Label lblOldMeterNumber;
    private FdxMaskedEdit edtNewMeterNumber;
    private FdxMaskedEdit edtNewAccountNumber;

    internal string NewMeterNumber
    {
      get => SelectNewMeterAccount._newMeterNumber;
      set => SelectNewMeterAccount._newMeterNumber = value;
    }

    internal string NewAccountNumber
    {
      get => SelectNewMeterAccount._newAccountNumber;
      set => SelectNewMeterAccount._newAccountNumber = value;
    }

    internal string OldMeterNumber
    {
      get => SelectNewMeterAccount._oldMeterNumber;
      set => SelectNewMeterAccount._oldMeterNumber = value;
    }

    internal string OldAccountNumber
    {
      get => SelectNewMeterAccount._oldAccountNumber;
      set => SelectNewMeterAccount._oldAccountNumber = value;
    }

    public SelectNewMeterAccount() => this.InitializeComponent();

    public void UpdateLabels()
    {
      this.lblOldMeterNumber.Text = this.OldMeterNumber;
      this.lblOldAccountNumber.Text = this.OldAccountNumber;
    }

    private void btnCancel_Click(object sender, EventArgs e) => this.Dispose();

    private void btnOK_Click(object sender, EventArgs e)
    {
      FedEx.Gsm.Common.Languafier.Languafier languafier = new FedEx.Gsm.Common.Languafier.Languafier();
      if (this.edtNewMeterNumber.Text.Equals(""))
      {
        int num1 = (int) MessageBox.Show(languafier.TranslateMessage(39861));
      }
      else if (this.edtNewAccountNumber.Text.Equals(""))
      {
        int num2 = (int) MessageBox.Show(languafier.TranslateMessage(39862));
      }
      else
      {
        SelectNewMeterAccount._newMeterNumber = this.edtNewMeterNumber.Text;
        SelectNewMeterAccount._newAccountNumber = this.edtNewAccountNumber.Text;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectNewMeterAccount));
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.groupBox1 = new GroupBox();
      this.edtNewMeterNumber = new FdxMaskedEdit();
      this.edtNewAccountNumber = new FdxMaskedEdit();
      this.lblOldAccountNumber = new Label();
      this.lblOldMeterNumber = new Label();
      this.label2 = new Label();
      this.label1 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.groupBox1.SuspendLayout();
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
      this.groupBox1.Controls.Add((Control) this.edtNewMeterNumber);
      this.groupBox1.Controls.Add((Control) this.edtNewAccountNumber);
      this.groupBox1.Controls.Add((Control) this.lblOldAccountNumber);
      this.groupBox1.Controls.Add((Control) this.lblOldMeterNumber);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.label4);
      componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.TabStop = false;
      this.edtNewMeterNumber.Allow = "";
      this.edtNewMeterNumber.Disallow = "";
      this.edtNewMeterNumber.eMask = eMasks.maskCustom;
      this.edtNewMeterNumber.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.edtNewMeterNumber, componentResourceManager.GetString("edtNewMeterNumber.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtNewMeterNumber, (HelpNavigator) componentResourceManager.GetObject("edtNewMeterNumber.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtNewMeterNumber, "edtNewMeterNumber");
      this.edtNewMeterNumber.Mask = "000000000";
      this.edtNewMeterNumber.Name = "edtNewMeterNumber";
      this.helpProvider1.SetShowHelp((Control) this.edtNewMeterNumber, (bool) componentResourceManager.GetObject("edtNewMeterNumber.ShowHelp"));
      this.edtNewAccountNumber.Allow = "";
      this.edtNewAccountNumber.Disallow = "";
      this.edtNewAccountNumber.eMask = eMasks.maskCustom;
      this.edtNewAccountNumber.FillFrom = LeftRightAlignment.Left;
      this.helpProvider1.SetHelpKeyword((Control) this.edtNewAccountNumber, componentResourceManager.GetString("edtNewAccountNumber.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtNewAccountNumber, (HelpNavigator) componentResourceManager.GetObject("edtNewAccountNumber.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtNewAccountNumber, "edtNewAccountNumber");
      this.edtNewAccountNumber.Mask = "000000000";
      this.edtNewAccountNumber.Name = "edtNewAccountNumber";
      this.helpProvider1.SetShowHelp((Control) this.edtNewAccountNumber, (bool) componentResourceManager.GetObject("edtNewAccountNumber.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblOldAccountNumber, "lblOldAccountNumber");
      this.lblOldAccountNumber.Name = "lblOldAccountNumber";
      componentResourceManager.ApplyResources((object) this.lblOldMeterNumber, "lblOldMeterNumber");
      this.lblOldMeterNumber.Name = "lblOldMeterNumber";
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      componentResourceManager.ApplyResources((object) this.label3, "label3");
      this.label3.Name = "label3";
      componentResourceManager.ApplyResources((object) this.label4, "label4");
      this.label4.Name = "label4";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOK);
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (SelectNewMeterAccount);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
