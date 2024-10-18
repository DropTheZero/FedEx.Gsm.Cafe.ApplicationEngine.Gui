// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.CashBlockForm
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Plugins;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class CashBlockForm : UserControlHelpEx, IFedExGsmGuiTabPlugin
  {
    private ToolStripMenuItem _tabItem = new ToolStripMenuItem();
    private IContainer components;
    private Panel pnlCashBlock;
    private LinkLabelEx llCashBlock;

    public ToolStripItem TabItem => (ToolStripItem) this._tabItem;

    public CashBlockForm()
    {
      this.InitializeComponent();
      this._tabItem.Text = GuiData.Languafier.Translate("Cash Block");
      this._tabItem.Padding = new Padding(5, 0, 5, 0);
      this._tabItem.Name = "toolStripButtonCashBlock";
      this._tabItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
    }

    private void llCashBlock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Utility.DisplayLinkLabelHelp(sender, e);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CashBlockForm));
      this.pnlCashBlock = new Panel();
      this.llCashBlock = new LinkLabelEx();
      this.pnlCashBlock.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) this.pnlCashBlock, "pnlCashBlock");
      this.pnlCashBlock.BorderStyle = BorderStyle.Fixed3D;
      this.pnlCashBlock.Controls.Add((Control) this.llCashBlock);
      this.pnlCashBlock.Name = "pnlCashBlock";
      componentResourceManager.ApplyResources((object) this.llCashBlock, "llCashBlock");
      this.llCashBlock.Name = "llCashBlock";
      this.llCashBlock.TabStop = true;
      this.llCashBlock.Tag = (object) "1275";
      this.llCashBlock.UseCompatibleTextRendering = true;
      this.llCashBlock.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llCashBlock_LinkClicked);
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.pnlCashBlock);
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (CashBlockForm);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.pnlCashBlock.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
