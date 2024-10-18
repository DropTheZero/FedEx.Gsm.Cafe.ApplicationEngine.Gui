// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.NewSoftwareAvailable
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class NewSoftwareAvailable : Form
  {
    private IContainer components;
    private Button btnInstallNow;
    private Button btnInstallLater;
    private Label labelDaysRemaining;
    private Label txtRemaining;
    private Label labelNoneRemaining;
    private TableLayoutPanel tableLayoutPanel1;
    private Panel pnlMessage;
    private Panel pnlButtons;
    private Label lblCloseSoftware;

    public NewSoftwareAvailable(int iDays)
    {
      this.InitializeComponent();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat(this.txtRemaining.Text, (object) iDays);
      this.txtRemaining.Text = stringBuilder.ToString();
      bool flag = iDays > 0;
      this.labelDaysRemaining.Visible = flag;
      this.labelNoneRemaining.Visible = !flag;
      if (flag)
        return;
      this.btnInstallLater.Text = GuiData.Languafier.Translate("ExitSystem");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NewSoftwareAvailable));
      this.btnInstallNow = new Button();
      this.btnInstallLater = new Button();
      this.labelDaysRemaining = new Label();
      this.txtRemaining = new Label();
      this.labelNoneRemaining = new Label();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.pnlButtons = new Panel();
      this.pnlMessage = new Panel();
      this.lblCloseSoftware = new Label();
      this.tableLayoutPanel1.SuspendLayout();
      this.pnlButtons.SuspendLayout();
      this.pnlMessage.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.btnInstallNow, "btnInstallNow");
      this.btnInstallNow.DialogResult = DialogResult.OK;
      this.btnInstallNow.Name = "btnInstallNow";
      this.btnInstallNow.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnInstallLater, "btnInstallLater");
      this.btnInstallLater.DialogResult = DialogResult.Cancel;
      this.btnInstallLater.Name = "btnInstallLater";
      this.btnInstallLater.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.labelDaysRemaining, "labelDaysRemaining");
      this.labelDaysRemaining.Name = "labelDaysRemaining";
      componentResourceManager.ApplyResources((object) this.txtRemaining, "txtRemaining");
      this.txtRemaining.Name = "txtRemaining";
      componentResourceManager.ApplyResources((object) this.labelNoneRemaining, "labelNoneRemaining");
      this.labelNoneRemaining.Name = "labelNoneRemaining";
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.pnlMessage, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.pnlButtons, 0, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.txtRemaining, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.lblCloseSoftware, 0, 1);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
      this.pnlButtons.Controls.Add((Control) this.btnInstallLater);
      this.pnlButtons.Controls.Add((Control) this.btnInstallNow);
      this.pnlButtons.Name = "pnlButtons";
      this.pnlMessage.Controls.Add((Control) this.labelNoneRemaining);
      this.pnlMessage.Controls.Add((Control) this.labelDaysRemaining);
      componentResourceManager.ApplyResources((object) this.pnlMessage, "pnlMessage");
      this.pnlMessage.Name = "pnlMessage";
      componentResourceManager.ApplyResources((object) this.lblCloseSoftware, "lblCloseSoftware");
      this.lblCloseSoftware.Name = "lblCloseSoftware";
      this.AcceptButton = (IButtonControl) this.btnInstallNow;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnInstallLater;
      this.ControlBox = false;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (NewSoftwareAvailable);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.tableLayoutPanel1.ResumeLayout(false);
      this.pnlButtons.ResumeLayout(false);
      this.pnlMessage.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
