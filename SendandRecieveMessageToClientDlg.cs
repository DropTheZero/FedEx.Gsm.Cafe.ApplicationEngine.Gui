// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SendandRecieveMessageToClientDlg
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class SendandRecieveMessageToClientDlg : HelpFormBase
  {
    private string _IPAddress;
    private IContainer components;
    private Button btnOk;
    private Button btnCancel;
    private GroupBox groupBox1;
    private TextBox edtText;

    public SendandRecieveMessageToClientDlg() => this.InitializeComponent();

    public SendandRecieveMessageToClientDlg(string IPAddress)
    {
      this._IPAddress = IPAddress;
      this.InitializeComponent();
    }

    private void SendandRecieveMessageToClientDlg_Load(object sender, EventArgs e)
    {
      this.Text = "Send Client Message";
      this.groupBox1.Text = "Enter Message";
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this._IPAddress))
        GuiData.AppController.ShipEngine.PublishMessage(new BroadcastMessage(OperationType.DisplayMessage, this.edtText.Text, new EventArgs(), GuiData.ConfigManager.NetworkClientID), false);
      else
        GuiData.AppController.ShipEngine.SendNetworkClientMessage(this._IPAddress, this.edtText.Text);
      this.DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SendandRecieveMessageToClientDlg));
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.groupBox1 = new GroupBox();
      this.edtText = new TextBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.Name = "btnOk";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.btnCancel.DialogResult = DialogResult.Cancel;
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.groupBox1.Controls.Add((Control) this.edtText);
      componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.TabStop = false;
      componentResourceManager.ApplyResources((object) this.edtText, "edtText");
      this.edtText.Name = "edtText";
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOk);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (SendandRecieveMessageToClientDlg);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.TopMost = true;
      this.Load += new EventHandler(this.SendandRecieveMessageToClientDlg_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
