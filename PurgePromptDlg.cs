// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.PurgePromptDlg
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Properties;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class PurgePromptDlg : Form
  {
    private DatabaseType _dbType;
    private const int _timeoutValue = 30;
    private bool _moreToPurge;
    private IContainer components;
    private Label lblPurgeCountdown;
    private ProgressBar progressBar;
    private Button button1;
    private Button button2;
    private Timer timer1;
    private Label lblPurgeDescription;
    private Label lblMoreToPurge;

    public bool MoreToPurge
    {
      get => this._moreToPurge;
      set
      {
        this._moreToPurge = value;
        this.lblMoreToPurge.Visible = value;
      }
    }

    public PurgePromptDlg() => this.InitializeComponent();

    public PurgePromptDlg(DatabaseType db)
    {
      this._dbType = db;
      this.InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

    private void button2_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.progressBar.PerformStep();
      this.lblPurgeCountdown.Text = string.Format(Resources.PurgeCountdownString, (object) this.progressBar.Value);
      if (this.progressBar.Value > 0)
        return;
      this.timer1.Stop();
      this.button1_Click((object) this, EventArgs.Empty);
    }

    private void PurgePromptDlg_Load(object sender, EventArgs e)
    {
      this.ResetWaitTime();
      this.lblPurgeDescription.Text = string.Format(this.lblPurgeDescription.Text, (object) 30);
    }

    public void ResetWaitTime()
    {
      this.progressBar.Minimum = 0;
      this.progressBar.Maximum = 30;
      this.progressBar.Value = 30;
      this.progressBar.Step = -1;
      this.lblPurgeCountdown.Text = string.Format(Resources.PurgeCountdownString, (object) 30);
      this.timer1.Interval = 1000;
      this.timer1.Start();
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PurgePromptDlg));
      this.lblPurgeCountdown = new Label();
      this.progressBar = new ProgressBar();
      this.button1 = new Button();
      this.button2 = new Button();
      this.timer1 = new Timer(this.components);
      this.lblPurgeDescription = new Label();
      this.lblMoreToPurge = new Label();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.lblPurgeCountdown, "lblPurgeCountdown");
      this.lblPurgeCountdown.Name = "lblPurgeCountdown";
      componentResourceManager.ApplyResources((object) this.progressBar, "progressBar");
      this.progressBar.Name = "progressBar";
      this.progressBar.Value = 100;
      componentResourceManager.ApplyResources((object) this.button1, "button1");
      this.button1.Name = "button1";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      componentResourceManager.ApplyResources((object) this.button2, "button2");
      this.button2.Name = "button2";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      componentResourceManager.ApplyResources((object) this.lblPurgeDescription, "lblPurgeDescription");
      this.lblPurgeDescription.Name = "lblPurgeDescription";
      componentResourceManager.ApplyResources((object) this.lblMoreToPurge, "lblMoreToPurge");
      this.lblMoreToPurge.Name = "lblMoreToPurge";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.lblMoreToPurge);
      this.Controls.Add((Control) this.lblPurgeDescription);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.progressBar);
      this.Controls.Add((Control) this.lblPurgeCountdown);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (PurgePromptDlg);
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Load += new EventHandler(this.PurgePromptDlg_Load);
      this.ResumeLayout(false);
    }
  }
}
