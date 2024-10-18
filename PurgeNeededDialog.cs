// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.PurgeNeededDialog
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class PurgeNeededDialog : Form
  {
    private IContainer components;
    private Label lblPurge;
    private Button btnOk;
    private Timer timeout;

    public PurgeNeededDialog(int daysToPurge)
    {
      this.InitializeComponent();
      this.timeout.Interval = 82800000;
      this.lblPurge.Text = string.Format(this.lblPurge.Text, (object) Utility.FormattedDateToText(DateTime.Now.Subtract(TimeSpan.FromDays((double) daysToPurge))));
    }

    protected override void OnLoad(EventArgs e)
    {
      this.timeout.Start();
      base.OnLoad(e);
    }

    private void timeout_Tick(object sender, EventArgs e) => this.DialogResult = DialogResult.Abort;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PurgeNeededDialog));
      this.lblPurge = new Label();
      this.btnOk = new Button();
      this.timeout = new Timer(this.components);
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.lblPurge, "lblPurge");
      this.lblPurge.Name = "lblPurge";
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.btnOk.UseVisualStyleBackColor = true;
      this.timeout.Enabled = true;
      this.timeout.Tick += new EventHandler(this.timeout_Tick);
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.lblPurge);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (PurgeNeededDialog);
      this.ShowIcon = false;
      this.ResumeLayout(false);
    }
  }
}
