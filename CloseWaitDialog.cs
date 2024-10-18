// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.CloseWaitDialog
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class CloseWaitDialog : Form
  {
    private IContainer components;
    private Timer timer1;
    private Button btnNo;
    private Button btnYes;
    private Label label1;
    private Label label2;

    public CloseWaitDialog()
    {
      this.InitializeComponent();
      this.timer1.Start();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.No;
      this.Close();
    }

    private void CloseWaitDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.timer1.Stop();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CloseWaitDialog));
      this.timer1 = new Timer(this.components);
      this.btnNo = new Button();
      this.btnYes = new Button();
      this.label1 = new Label();
      this.label2 = new Label();
      this.SuspendLayout();
      this.timer1.Interval = 900000;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.btnNo.DialogResult = DialogResult.Cancel;
      componentResourceManager.ApplyResources((object) this.btnNo, "btnNo");
      this.btnNo.Name = "btnNo";
      this.btnNo.UseVisualStyleBackColor = true;
      this.btnYes.DialogResult = DialogResult.Yes;
      componentResourceManager.ApplyResources((object) this.btnYes, "btnYes");
      this.btnYes.Name = "btnYes";
      this.btnYes.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      this.AcceptButton = (IButtonControl) this.btnYes;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnNo;
      this.Controls.Add((Control) this.btnNo);
      this.Controls.Add((Control) this.btnYes);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.label2);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (CloseWaitDialog);
      this.TopMost = true;
      this.FormClosing += new FormClosingEventHandler(this.CloseWaitDialog_FormClosing);
      this.ResumeLayout(false);
    }
  }
}
