// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.NetworkClientUpgradeDlg
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Common.ConfigManager;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class NetworkClientUpgradeDlg : Form
  {
    private Thread _downloadThread;
    private IContainer components;
    private ProgressBar progressBar1;
    private Label lblStatus;
    private Button btnOK;
    private Button btnCancel;
    private GroupBox groupBox1;

    public ProgressBar DownloadProgressBar => this.progressBar1;

    public NetworkClientUpgradeDlg() => this.InitializeComponent();

    private void DisplayMessage(string message)
    {
      int num = (int) MessageBox.Show(message, "FedEx Shipmanager Network Client Upgrade");
    }

    private void NetworkClientUpgradeDlg_Load(object sender, EventArgs e)
    {
      this._downloadThread = new Thread(new ParameterizedThreadStart(this.ThreadFunc));
      this.btnOK.Enabled = false;
      this._downloadThread.IsBackground = true;
      this._downloadThread.Start((object) this);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this._downloadThread.Abort();
      this.DialogResult = DialogResult.Cancel;
    }

    private void btnOK_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

    private void ThreadFunc(object state)
    {
      NetworkClientUpgradeDlg dlg = (NetworkClientUpgradeDlg) state;
      try
      {
        InstallStream fs = GuiData.AppController.ShipEngine.GetNetworkClientUpgradeInstallStream();
        if (fs != null)
        {
          string path = Path.Combine(DataFileLocations.Instance.AdminDirectory, "Execute\\NWCSetup.exe");
          Directory.CreateDirectory(Path.GetDirectoryName(path));
          using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
          {
            dlg.Invoke((Delegate) (() => dlg.DownloadProgressBar.Minimum = 0));
            dlg.Invoke((Delegate) (() => dlg.DownloadProgressBar.Maximum = (int) (fs.Length / 1024L)));
            byte[] buffer = new byte[64000];
            int readbytes = 0;
            do
            {
              readbytes = fs.Data.Read(buffer, 0, 64000);
              dlg.Invoke((Delegate) (() => dlg.progressBar1.Value += readbytes / 1024));
              dlg.Invoke((Delegate) (() => dlg.lblStatus.Text = "Downloading " + dlg.DownloadProgressBar.Value.ToString() + " of " + (fs.Length / 1024L).ToString() + " kilobytes."));
              fileStream.Write(buffer, 0, readbytes);
            }
            while (readbytes > 0);
          }
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "NetworkClientUpgradeDlg::ThreadFunc", "About to ask admin to launch the upgrade.");
          dlg.Invoke((Delegate) (() => dlg.lblStatus.Text = "Executing Install Package."));
          Thread thread = new Thread((ThreadStart) (() => GuiData.AppController.ShipEngine.ExecuteInstalls(new List<AdminInstallInfo>()
          {
            new AdminInstallInfo() { InstallID = path }
          })));
          thread.Start();
          thread.Join(30000);
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "NetworkClientUpgradeDlg::ThreadFunc", "Finished launching upgrade, exiting.");
          Environment.Exit(0);
        }
        else
        {
          dlg.Invoke((Delegate) (() => dlg.DisplayMessage("Could not Retrieve the Setup utility from the FSM Server")));
          dlg.DialogResult = DialogResult.Cancel;
        }
      }
      catch (Exception ex)
      {
        dlg.Invoke((Delegate) (() => dlg.DisplayMessage("An Exception occured Retrieving the Setup utility from the FSM Server")));
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, "NetworkClientUpgradeDlg::ThreadFunc", ex.ToString());
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NetworkClientUpgradeDlg));
      this.progressBar1 = new ProgressBar();
      this.lblStatus = new Label();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.groupBox1 = new GroupBox();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.progressBar1, "progressBar1");
      this.progressBar1.Name = "progressBar1";
      componentResourceManager.ApplyResources((object) this.lblStatus, "lblStatus");
      this.lblStatus.Name = "lblStatus";
      componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
      this.btnOK.Name = "btnOK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.TabStop = false;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.lblStatus);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.groupBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (NetworkClientUpgradeDlg);
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.TopMost = true;
      this.Load += new EventHandler(this.NetworkClientUpgradeDlg_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
