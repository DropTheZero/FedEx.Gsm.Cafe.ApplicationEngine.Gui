// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.AboutBox
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Properties;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class AboutBox : HelpFormBase
  {
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel;
    private PictureBox logoPictureBox;
    private Label labelProductName;
    private Label labelVersion;
    private Label labelCopyright;
    private Label labelCompanyName;
    private TextBox textBoxDescription;
    private Panel panel1;
    private Button btnInstallHistory;
    private Button btnOk;
    private Label labelFxiaVersion;
    private Label labelFsmHostVersion;

    public AboutBox()
    {
      this.InitializeComponent();
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.SETTINGS);
      this.Text = string.Format("About {0}", (object) this.AssemblyTitle);
      this.labelProductName.Text = this.AssemblyProduct;
      string str;
      ref string local = ref str;
      if (configManager.GetProfileString("SYSTEM", "PRODUCTVERSION", out local))
        this.labelVersion.Text = string.Format("Version {0}", (object) str);
      else
        this.labelVersion.Text = "Version";
      this.labelCopyright.Text = this.AssemblyCopyright;
      this.labelCompanyName.Text = this.AssemblyCompany;
      this.textBoxDescription.Text = this.AssemblyDescription;
      this.labelFxiaVersion.Text = string.Format("{0}: {1}", (object) "FedEx© Integration Assistant", (object) this.FXIAVersion);
      this.labelFsmHostVersion.Text = string.Format("FSM Host Object Version: {0}", (object) this.FSMHostObjectVersion);
    }

    public string AssemblyTitle => GuiData.Languafier.Translate("SoftwareName");

    public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

    public string AssemblyDescription
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
        return customAttributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute) customAttributes[0]).Description;
      }
    }

    public string AssemblyProduct => GuiData.Languafier.Translate("SoftwareName");

    public string AssemblyCopyright
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
        return customAttributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
      }
    }

    public string AssemblyCompany
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
        return customAttributes.Length == 0 ? "" : ((AssemblyCompanyAttribute) customAttributes[0]).Company;
      }
    }

    private void btnInstallHistory_Click(object sender, EventArgs e)
    {
      using (InstallHistory installHistory = new InstallHistory())
      {
        int num = (int) installHistory.ShowDialog((IWin32Window) this);
      }
      this.DialogResult = DialogResult.None;
    }

    public string FXIAVersion
    {
      get
      {
        try
        {
          return new IntegrationLauncher().FXIA.GetVersion();
        }
        catch (Exception ex)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "AboutBox.FXIAVersion", "Error reading FXIA version: " + ex.ToString());
        }
        return string.Empty;
      }
    }

    private string FSMHostObjectVersion
    {
      get
      {
        try
        {
          return this.GetFSMHostVersion();
        }
        catch (Exception ex)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "AboutBox.FSMHostObjectVersion", "Error reading FSM host object version: " + ex.ToString());
        }
        return string.Empty;
      }
    }

    private string GetFSMHostVersion()
    {
      return FileVersionInfo.GetVersionInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "FedEx\\Integration\\HostItems\\FSM.dll")).ProductVersion;
    }

    private void AboutBox_Load(object sender, EventArgs e)
    {
      if (!GuiData.ConfigManager.IsNetworkClient)
        return;
      this.btnInstallHistory.Visible = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AboutBox));
      this.tableLayoutPanel = new TableLayoutPanel();
      this.logoPictureBox = new PictureBox();
      this.labelProductName = new Label();
      this.labelVersion = new Label();
      this.labelCopyright = new Label();
      this.labelCompanyName = new Label();
      this.textBoxDescription = new TextBox();
      this.panel1 = new Panel();
      this.btnInstallHistory = new Button();
      this.btnOk = new Button();
      this.labelFxiaVersion = new Label();
      this.labelFsmHostVersion = new Label();
      this.tableLayoutPanel.SuspendLayout();
      ((ISupportInitialize) this.logoPictureBox).BeginInit();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel, "tableLayoutPanel");
      this.tableLayoutPanel.Controls.Add((Control) this.logoPictureBox, 0, 0);
      this.tableLayoutPanel.Controls.Add((Control) this.labelProductName, 1, 0);
      this.tableLayoutPanel.Controls.Add((Control) this.labelVersion, 1, 1);
      this.tableLayoutPanel.Controls.Add((Control) this.labelCopyright, 1, 2);
      this.tableLayoutPanel.Controls.Add((Control) this.labelCompanyName, 1, 3);
      this.tableLayoutPanel.Controls.Add((Control) this.textBoxDescription, 1, 6);
      this.tableLayoutPanel.Controls.Add((Control) this.panel1, 1, 7);
      this.tableLayoutPanel.Controls.Add((Control) this.labelFxiaVersion, 1, 4);
      this.tableLayoutPanel.Controls.Add((Control) this.labelFsmHostVersion, 1, 5);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.helpProvider1.SetShowHelp((Control) this.tableLayoutPanel, (bool) componentResourceManager.GetObject("tableLayoutPanel.ShowHelp"));
      this.logoPictureBox.BorderStyle = BorderStyle.FixedSingle;
      componentResourceManager.ApplyResources((object) this.logoPictureBox, "logoPictureBox");
      this.logoPictureBox.Image = (Image) Resources.about;
      this.logoPictureBox.Name = "logoPictureBox";
      this.tableLayoutPanel.SetRowSpan((Control) this.logoPictureBox, 8);
      this.helpProvider1.SetShowHelp((Control) this.logoPictureBox, (bool) componentResourceManager.GetObject("logoPictureBox.ShowHelp"));
      this.logoPictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.labelProductName, "labelProductName");
      this.labelProductName.Name = "labelProductName";
      this.helpProvider1.SetShowHelp((Control) this.labelProductName, (bool) componentResourceManager.GetObject("labelProductName.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.labelVersion, "labelVersion");
      this.labelVersion.Name = "labelVersion";
      this.helpProvider1.SetShowHelp((Control) this.labelVersion, (bool) componentResourceManager.GetObject("labelVersion.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.labelCopyright, "labelCopyright");
      this.labelCopyright.Name = "labelCopyright";
      this.helpProvider1.SetShowHelp((Control) this.labelCopyright, (bool) componentResourceManager.GetObject("labelCopyright.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.labelCompanyName, "labelCompanyName");
      this.labelCompanyName.Name = "labelCompanyName";
      this.helpProvider1.SetShowHelp((Control) this.labelCompanyName, (bool) componentResourceManager.GetObject("labelCompanyName.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.textBoxDescription, "textBoxDescription");
      this.textBoxDescription.Name = "textBoxDescription";
      this.textBoxDescription.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.textBoxDescription, (bool) componentResourceManager.GetObject("textBoxDescription.ShowHelp"));
      this.textBoxDescription.TabStop = false;
      this.panel1.Controls.Add((Control) this.btnInstallHistory);
      this.panel1.Controls.Add((Control) this.btnOk);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      this.helpProvider1.SetShowHelp((Control) this.panel1, (bool) componentResourceManager.GetObject("panel1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnInstallHistory, "btnInstallHistory");
      this.btnInstallHistory.DialogResult = DialogResult.Cancel;
      this.helpProvider1.SetHelpKeyword((Control) this.btnInstallHistory, componentResourceManager.GetString("btnInstallHistory.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnInstallHistory, (HelpNavigator) componentResourceManager.GetObject("btnInstallHistory.HelpNavigator"));
      this.btnInstallHistory.Name = "btnInstallHistory";
      this.helpProvider1.SetShowHelp((Control) this.btnInstallHistory, (bool) componentResourceManager.GetObject("btnInstallHistory.ShowHelp"));
      this.btnInstallHistory.UseVisualStyleBackColor = true;
      this.btnInstallHistory.Click += new EventHandler(this.btnInstallHistory_Click);
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.helpProvider1.SetShowHelp((Control) this.btnOk, (bool) componentResourceManager.GetObject("btnOk.ShowHelp"));
      this.btnOk.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.labelFxiaVersion, "labelFxiaVersion");
      this.labelFxiaVersion.Name = "labelFxiaVersion";
      this.helpProvider1.SetShowHelp((Control) this.labelFxiaVersion, (bool) componentResourceManager.GetObject("labelFxiaVersion.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.labelFsmHostVersion, "labelFsmHostVersion");
      this.labelFsmHostVersion.Name = "labelFsmHostVersion";
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnOk;
      this.Controls.Add((Control) this.tableLayoutPanel);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (AboutBox);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Load += new EventHandler(this.AboutBox_Load);
      this.tableLayoutPanel.ResumeLayout(false);
      this.tableLayoutPanel.PerformLayout();
      ((ISupportInitialize) this.logoPictureBox).EndInit();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
