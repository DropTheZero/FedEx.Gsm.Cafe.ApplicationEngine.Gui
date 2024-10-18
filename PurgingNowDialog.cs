// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.PurgingNowDialog
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class PurgingNowDialog : Form
  {
    private IContainer components;
    private Label lblPurging;
    private ProgressBar progressBar1;

    public PurgingNowDialog() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PurgingNowDialog));
      this.lblPurging = new Label();
      this.progressBar1 = new ProgressBar();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.lblPurging, "lblPurging");
      this.lblPurging.Name = "lblPurging";
      componentResourceManager.ApplyResources((object) this.progressBar1, "progressBar1");
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Style = ProgressBarStyle.Marquee;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ControlBox = false;
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.lblPurging);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (PurgingNowDialog);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.ResumeLayout(false);
    }
  }
}
