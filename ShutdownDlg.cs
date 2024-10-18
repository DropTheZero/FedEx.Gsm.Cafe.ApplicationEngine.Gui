// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.ShutdownDlg
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class ShutdownDlg : HelpFormBase
  {
    private IContainer components;
    private Button btnClose;
    private Button btnUploadOnly;
    private FlowLayoutPanel flowLayoutPanel1;
    private Button btnCancel;
    private Button btnYes;
    private Button btnNo;
    private Label txtSO5;
    private Label txtFailure;
    private Label txtSO4;
    private Label txtSO2;
    private Label txtSO3;
    private Label txtNotSO1;
    private Label txtSO1;
    private Label lblSmartPost;

    public ShutdownDlg()
    {
      this.InitializeComponent();
      bool softwareOnly = Utility.SoftwareOnly;
      this.txtNotSO1.Visible = !softwareOnly;
      this.txtSO1.Visible = softwareOnly;
      this.txtSO2.Visible = softwareOnly;
      this.txtSO3.Visible = softwareOnly;
      this.txtSO4.Visible = softwareOnly && GuiData.CurrentAccount.IsTDAllowed;
      this.txtSO5.Visible = softwareOnly;
      this.lblSmartPost.Visible = softwareOnly && GuiData.CurrentAccount.IsSmartPostEnabled;
      this.btnClose.Visible = !softwareOnly;
      this.btnUploadOnly.Visible = !softwareOnly;
      this.btnCancel.Visible = !softwareOnly;
      this.btnYes.Visible = softwareOnly;
      this.btnNo.Visible = softwareOnly;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ShutdownDlg));
      this.btnClose = new Button();
      this.btnUploadOnly = new Button();
      this.flowLayoutPanel1 = new FlowLayoutPanel();
      this.btnCancel = new Button();
      this.btnYes = new Button();
      this.btnNo = new Button();
      this.txtSO5 = new Label();
      this.txtFailure = new Label();
      this.txtSO4 = new Label();
      this.txtSO2 = new Label();
      this.txtSO3 = new Label();
      this.txtNotSO1 = new Label();
      this.txtSO1 = new Label();
      this.lblSmartPost = new Label();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.btnClose, "btnClose");
      this.btnClose.DialogResult = DialogResult.OK;
      this.btnClose.Name = "btnClose";
      this.btnClose.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnUploadOnly, "btnUploadOnly");
      this.btnUploadOnly.DialogResult = DialogResult.Retry;
      this.helpProvider1.SetHelpKeyword((Control) this.btnUploadOnly, componentResourceManager.GetString("btnUploadOnly.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnUploadOnly, (HelpNavigator) componentResourceManager.GetObject("btnUploadOnly.HelpNavigator"));
      this.btnUploadOnly.Name = "btnUploadOnly";
      this.helpProvider1.SetShowHelp((Control) this.btnUploadOnly, (bool) componentResourceManager.GetObject("btnUploadOnly.ShowHelp"));
      this.btnUploadOnly.UseVisualStyleBackColor = true;
      this.flowLayoutPanel1.Controls.Add((Control) this.btnClose);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnUploadOnly);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnCancel);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnYes);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnNo);
      componentResourceManager.ApplyResources((object) this.flowLayoutPanel1, "flowLayoutPanel1");
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnYes, "btnYes");
      this.btnYes.DialogResult = DialogResult.Yes;
      this.btnYes.Name = "btnYes";
      this.btnYes.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnNo, "btnNo");
      this.btnNo.DialogResult = DialogResult.No;
      this.btnNo.Name = "btnNo";
      this.btnNo.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.txtSO5, "txtSO5");
      this.txtSO5.Name = "txtSO5";
      componentResourceManager.ApplyResources((object) this.txtFailure, "txtFailure");
      this.txtFailure.Name = "txtFailure";
      componentResourceManager.ApplyResources((object) this.txtSO4, "txtSO4");
      this.txtSO4.Name = "txtSO4";
      componentResourceManager.ApplyResources((object) this.txtSO2, "txtSO2");
      this.txtSO2.Name = "txtSO2";
      componentResourceManager.ApplyResources((object) this.txtSO3, "txtSO3");
      this.txtSO3.Name = "txtSO3";
      componentResourceManager.ApplyResources((object) this.txtNotSO1, "txtNotSO1");
      this.txtNotSO1.Name = "txtNotSO1";
      componentResourceManager.ApplyResources((object) this.txtSO1, "txtSO1");
      this.txtSO1.Name = "txtSO1";
      componentResourceManager.ApplyResources((object) this.lblSmartPost, "lblSmartPost");
      this.lblSmartPost.Name = "lblSmartPost";
      this.helpProvider1.SetShowHelp((Control) this.lblSmartPost, (bool) componentResourceManager.GetObject("lblSmartPost.ShowHelp"));
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.lblSmartPost);
      this.Controls.Add((Control) this.txtSO1);
      this.Controls.Add((Control) this.txtNotSO1);
      this.Controls.Add((Control) this.txtSO3);
      this.Controls.Add((Control) this.txtSO2);
      this.Controls.Add((Control) this.txtSO4);
      this.Controls.Add((Control) this.txtFailure);
      this.Controls.Add((Control) this.txtSO5);
      this.Controls.Add((Control) this.flowLayoutPanel1);
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ShutdownDlg);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.flowLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
