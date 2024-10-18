// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Profile_Header
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class Profile_Header : UserControlHelpEx
  {
    private IContainer components;
    private Label lblProfileCode;
    private TextBox edtProfileCode;
    private Label lblProfileDesc;
    private TextBox edtProfileDesc;
    private TableLayoutPanel tableLayoutPanel1;

    public Profile_Header() => this.InitializeComponent();

    public string ProfileCode
    {
      get => this.edtProfileCode.Text;
      set => this.edtProfileCode.Text = value;
    }

    public string ProfileDescription
    {
      get => this.edtProfileDesc.Text;
      set => this.edtProfileDesc.Text = value;
    }

    public bool EnableProfileCode
    {
      set => this.edtProfileCode.Enabled = value;
    }

    public bool EnableProfileDesc
    {
      set => this.edtProfileDesc.Enabled = value;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Profile_Header));
      this.lblProfileCode = new Label();
      this.edtProfileCode = new TextBox();
      this.lblProfileDesc = new Label();
      this.edtProfileDesc = new TextBox();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) this.lblProfileCode, "lblProfileCode");
      this.lblProfileCode.Name = "lblProfileCode";
      componentResourceManager.ApplyResources((object) this.edtProfileCode, "edtProfileCode");
      this.helpProvider1.SetHelpKeyword((Control) this.edtProfileCode, componentResourceManager.GetString("edtProfileCode.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtProfileCode, (HelpNavigator) componentResourceManager.GetObject("edtProfileCode.HelpNavigator"));
      this.edtProfileCode.Name = "edtProfileCode";
      this.helpProvider1.SetShowHelp((Control) this.edtProfileCode, (bool) componentResourceManager.GetObject("edtProfileCode.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblProfileDesc, "lblProfileDesc");
      this.lblProfileDesc.Name = "lblProfileDesc";
      componentResourceManager.ApplyResources((object) this.edtProfileDesc, "edtProfileDesc");
      this.helpProvider1.SetHelpKeyword((Control) this.edtProfileDesc, componentResourceManager.GetString("edtProfileDesc.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtProfileDesc, (HelpNavigator) componentResourceManager.GetObject("edtProfileDesc.HelpNavigator"));
      this.edtProfileDesc.Name = "edtProfileDesc";
      this.helpProvider1.SetShowHelp((Control) this.edtProfileDesc, (bool) componentResourceManager.GetObject("edtProfileDesc.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.lblProfileDesc, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.edtProfileDesc, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.lblProfileCode, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.edtProfileCode, 0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (Profile_Header);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
