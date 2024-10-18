// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.UploadOverdueDialog
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Plugins;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class UploadOverdueDialog : Form
  {
    private IFedExGsmGuiCloseTabPlugin _close;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private Label lblSuspended;
    private Label lblUpload;
    private Label lblExit;
    private FlowLayoutPanel flpButtons;
    private Button btnUploadNow;
    private Button btnExit;
    private Label lblTechnicalAssist;

    public UploadOverdueDialog(IFedExGsmGuiCloseTabPlugin close)
    {
      this._close = close;
      this.InitializeComponent();
    }

    private void btnUploadNow_Click(object sender, EventArgs e)
    {
      if (this._close.CloseIt() != 1)
        return;
      this.DialogResult = DialogResult.OK;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UploadOverdueDialog));
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.lblSuspended = new Label();
      this.lblUpload = new Label();
      this.lblExit = new Label();
      this.lblTechnicalAssist = new Label();
      this.flpButtons = new FlowLayoutPanel();
      this.btnExit = new Button();
      this.btnUploadNow = new Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.flpButtons.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.lblSuspended, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.lblUpload, 1, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.lblExit, 1, 7);
      this.tableLayoutPanel1.Controls.Add((Control) this.lblTechnicalAssist, 1, 5);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      componentResourceManager.ApplyResources((object) this.lblSuspended, "lblSuspended");
      this.lblSuspended.Name = "lblSuspended";
      componentResourceManager.ApplyResources((object) this.lblUpload, "lblUpload");
      this.lblUpload.Name = "lblUpload";
      componentResourceManager.ApplyResources((object) this.lblExit, "lblExit");
      this.lblExit.Name = "lblExit";
      componentResourceManager.ApplyResources((object) this.lblTechnicalAssist, "lblTechnicalAssist");
      this.lblTechnicalAssist.Name = "lblTechnicalAssist";
      componentResourceManager.ApplyResources((object) this.flpButtons, "flpButtons");
      this.flpButtons.Controls.Add((Control) this.btnExit);
      this.flpButtons.Controls.Add((Control) this.btnUploadNow);
      this.flpButtons.Name = "flpButtons";
      componentResourceManager.ApplyResources((object) this.btnExit, "btnExit");
      this.btnExit.DialogResult = DialogResult.Cancel;
      this.btnExit.Name = "btnExit";
      this.btnExit.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnUploadNow, "btnUploadNow");
      this.btnUploadNow.Name = "btnUploadNow";
      this.btnUploadNow.UseVisualStyleBackColor = true;
      this.btnUploadNow.Click += new EventHandler(this.btnUploadNow_Click);
      this.AcceptButton = (IButtonControl) this.btnUploadNow;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnExit;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.Controls.Add((Control) this.flpButtons);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (UploadOverdueDialog);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.flpButtons.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
