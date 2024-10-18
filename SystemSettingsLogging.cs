// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SystemSettingsLogging
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Properties;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class SystemSettingsLogging : UserControlHelpEx
  {
    private bool _initialRateLogging;
    private IContainer components;
    private ColorGroupBox gbxLevelDescription;
    private DataGridView dgvLogLevels;
    private ColorGroupBox colorGroupBox1;
    private TableLayoutPanel tableLayoutPanel1;
    private Panel panel1;
    private Button btnViewLog;
    private ComboBox cboSetAllLogLevels;
    private Button btnSetAllLogLevels;
    private Label label1;
    private WebBrowser webBrowserLogLevelDesc;
    private DataGridViewTextBoxColumn colComponent;
    private DataGridViewComboBoxColumn colLevel;
    private DataGridViewTextBoxColumn colCode;
    private FocusExtender focusExtender1;
    private ColorGroupBox grpExternalLogging;
    private CheckBox chkRateLogging;

    public bool RateLoggingChanged => this.chkRateLogging.Checked != this._initialRateLogging;

    public SystemSettingsLogging() => this.InitializeComponent();

    public void ScreenToObjects()
    {
      this.SaveLevels();
      if (!this.RateLoggingChanged)
        return;
      GuiData.AppController.ShipEngine.UpdateCRSVLoggingSettings(this.chkRateLogging.Checked);
    }

    public int ObjectsToScreen(SystemSettingsObject settings, Utility.FormOperation op)
    {
      this.LoadLevels();
      this._initialRateLogging = GuiData.AppController.ShipEngine.GetCRSVLoggingStatus();
      this.chkRateLogging.Checked = this._initialRateLogging;
      return 0;
    }

    public bool HandleError(Error error)
    {
      Utility.DisplayError(error);
      return error.Code != 1 && this.HandleError(error.Code);
    }

    public bool HandleError(int error) => false;

    private void SystemSettingsLogging_Load(object sender, EventArgs e)
    {
      this.webBrowserLogLevelDesc.DocumentText = Resources.LogLevelDesc.Replace("_BACKGROUND_COLOR_", ColorTranslator.ToHtml(Color.FromArgb(this.Parent.Parent.BackColor.ToArgb())));
    }

    private DataTable GetLevelData()
    {
      DataTable levelData = new DataTable("Levels");
      levelData.Columns.Add("Text");
      levelData.Columns.Add("Level");
      foreach (int num in Enum.GetValues(typeof (FedEx.Gsm.Common.Logging.LogLevel)))
      {
        DataRow row = levelData.NewRow();
        row["Text"] = (object) ((FedEx.Gsm.Common.Logging.LogLevel) Enum.ToObject(typeof (FedEx.Gsm.Common.Logging.LogLevel), num)).ToString();
        row["Level"] = (object) num;
        levelData.Rows.Add(row);
      }
      return levelData;
    }

    private void LoadLevels()
    {
      this.colLevel.DisplayMember = "Text";
      this.colLevel.ValueMember = "Level";
      this.colLevel.DataSource = (object) this.GetLevelData();
      this.cboSetAllLogLevels.DisplayMember = "Text";
      this.cboSetAllLogLevels.ValueMember = "Level";
      this.cboSetAllLogLevels.SelectedIndex = -1;
      this.cboSetAllLogLevels.DataSource = (object) this.GetLevelData();
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Code");
      dataTable.Columns.Add("Component");
      dataTable.Columns.Add("Level");
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager();
      foreach (string enumSubKey in configManager.EnumSubKeys("SHIPNET2000/LOGGING"))
      {
        if (enumSubKey.Length == 2)
        {
          string str;
          configManager.GetProfileString("SHIPNET2000/LOGGING/" + enumSubKey, "DEFAULT", out str);
          long lval;
          configManager.GetProfileLong("SHIPNET2000/LOGGING/" + enumSubKey, "LOGLVL", out lval);
          if (Enum.IsDefined(typeof (FedEx.Gsm.Common.Logging.LogLevel), (object) (int) lval))
          {
            DataRow row = dataTable.NewRow();
            row["Code"] = (object) enumSubKey;
            row["Component"] = (object) str;
            row["Level"] = (object) lval.ToString();
            dataTable.Rows.Add(row);
          }
        }
      }
      this.dgvLogLevels.DataSource = (object) dataTable;
    }

    private void SaveLevels()
    {
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager1 = new FedEx.Gsm.Common.ConfigManager.ConfigManager();
      foreach (DataGridViewRow row in (IEnumerable) this.dgvLogLevels.Rows)
      {
        DataRowView dataBoundItem = row.DataBoundItem as DataRowView;
        configManager1.SetProfileValue("SHIPNET2000/LOGGING/" + dataBoundItem.Row["Code"].ToString(), "LOGLVL", (object) dataBoundItem.Row["Level"].ToString());
        FedEx.Gsm.Common.ConfigManager.ConfigManager configManager2 = configManager1;
        string path = "SHIPNET2000/LOGGING/" + dataBoundItem.Row["Code"].ToString();
        DateTime dateTime = DateTime.Now;
        dateTime = dateTime.AddDays(5.0);
        string shortDateString = dateTime.ToShortDateString();
        configManager2.SetProfileValue(path, "EXPIRATION", (object) shortDateString);
      }
    }

    private void btnSetAllLogLevels_Click(object sender, EventArgs e)
    {
      if (this.cboSetAllLogLevels.SelectedIndex <= -1)
        return;
      string str = this.cboSetAllLogLevels.SelectedValue.ToString();
      foreach (DataGridViewRow row in (IEnumerable) this.dgvLogLevels.Rows)
        (row.DataBoundItem as DataRowView).Row["Level"] = (object) str;
    }

    private void btnViewLog_Click(object sender, EventArgs e)
    {
      try
      {
        Process.Start(new FedEx.Gsm.Common.ConfigManager.ConfigManager().InstallLocs.BinDirectory + "\\LogViewer.exe");
      }
      catch (Win32Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "Error opening LogViewer");
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SystemSettingsLogging));
      this.gbxLevelDescription = new ColorGroupBox();
      this.webBrowserLogLevelDesc = new WebBrowser();
      this.dgvLogLevels = new DataGridView();
      this.colComponent = new DataGridViewTextBoxColumn();
      this.colLevel = new DataGridViewComboBoxColumn();
      this.colCode = new DataGridViewTextBoxColumn();
      this.colorGroupBox1 = new ColorGroupBox();
      this.cboSetAllLogLevels = new ComboBox();
      this.btnSetAllLogLevels = new Button();
      this.label1 = new Label();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.panel1 = new Panel();
      this.btnViewLog = new Button();
      this.focusExtender1 = new FocusExtender();
      this.grpExternalLogging = new ColorGroupBox();
      this.chkRateLogging = new CheckBox();
      this.gbxLevelDescription.SuspendLayout();
      ((ISupportInitialize) this.dgvLogLevels).BeginInit();
      this.colorGroupBox1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.grpExternalLogging.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      this.gbxLevelDescription.BorderThickness = 1f;
      this.gbxLevelDescription.Controls.Add((Control) this.webBrowserLogLevelDesc);
      componentResourceManager.ApplyResources((object) this.gbxLevelDescription, "gbxLevelDescription");
      this.gbxLevelDescription.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxLevelDescription.Name = "gbxLevelDescription";
      this.gbxLevelDescription.RoundCorners = 5;
      this.gbxLevelDescription.TabStop = false;
      this.webBrowserLogLevelDesc.AllowNavigation = false;
      this.webBrowserLogLevelDesc.AllowWebBrowserDrop = false;
      componentResourceManager.ApplyResources((object) this.webBrowserLogLevelDesc, "webBrowserLogLevelDesc");
      this.helpProvider1.SetHelpKeyword((Control) this.webBrowserLogLevelDesc, componentResourceManager.GetString("webBrowserLogLevelDesc.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.webBrowserLogLevelDesc, (HelpNavigator) componentResourceManager.GetObject("webBrowserLogLevelDesc.HelpNavigator"));
      this.webBrowserLogLevelDesc.IsWebBrowserContextMenuEnabled = false;
      this.webBrowserLogLevelDesc.Name = "webBrowserLogLevelDesc";
      this.webBrowserLogLevelDesc.ScriptErrorsSuppressed = true;
      this.webBrowserLogLevelDesc.ScrollBarsEnabled = false;
      this.helpProvider1.SetShowHelp((Control) this.webBrowserLogLevelDesc, (bool) componentResourceManager.GetObject("webBrowserLogLevelDesc.ShowHelp"));
      this.webBrowserLogLevelDesc.WebBrowserShortcutsEnabled = false;
      this.dgvLogLevels.AllowUserToAddRows = false;
      this.dgvLogLevels.AllowUserToDeleteRows = false;
      this.dgvLogLevels.AllowUserToResizeRows = false;
      componentResourceManager.ApplyResources((object) this.dgvLogLevels, "dgvLogLevels");
      this.dgvLogLevels.BackgroundColor = SystemColors.Window;
      this.dgvLogLevels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvLogLevels.Columns.AddRange((DataGridViewColumn) this.colComponent, (DataGridViewColumn) this.colLevel, (DataGridViewColumn) this.colCode);
      this.helpProvider1.SetHelpKeyword((Control) this.dgvLogLevels, componentResourceManager.GetString("dgvLogLevels.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.dgvLogLevels, (HelpNavigator) componentResourceManager.GetObject("dgvLogLevels.HelpNavigator"));
      this.dgvLogLevels.MultiSelect = false;
      this.dgvLogLevels.Name = "dgvLogLevels";
      this.dgvLogLevels.RowHeadersVisible = false;
      this.dgvLogLevels.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvLogLevels.ShowEditingIcon = false;
      this.helpProvider1.SetShowHelp((Control) this.dgvLogLevels, (bool) componentResourceManager.GetObject("dgvLogLevels.ShowHelp"));
      this.colComponent.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.colComponent.DataPropertyName = "Component";
      componentResourceManager.ApplyResources((object) this.colComponent, "colComponent");
      this.colComponent.Name = "colComponent";
      this.colLevel.DataPropertyName = "Level";
      this.colLevel.DisplayStyleForCurrentCellOnly = true;
      componentResourceManager.ApplyResources((object) this.colLevel, "colLevel");
      this.colLevel.Name = "colLevel";
      this.colCode.DataPropertyName = "Code";
      componentResourceManager.ApplyResources((object) this.colCode, "colCode");
      this.colCode.Name = "colCode";
      this.colorGroupBox1.BorderThickness = 1f;
      this.colorGroupBox1.Controls.Add((Control) this.cboSetAllLogLevels);
      this.colorGroupBox1.Controls.Add((Control) this.btnSetAllLogLevels);
      this.colorGroupBox1.Controls.Add((Control) this.label1);
      this.colorGroupBox1.Controls.Add((Control) this.dgvLogLevels);
      componentResourceManager.ApplyResources((object) this.colorGroupBox1, "colorGroupBox1");
      this.colorGroupBox1.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox1.Name = "colorGroupBox1";
      this.colorGroupBox1.RoundCorners = 5;
      this.tableLayoutPanel1.SetRowSpan((Control) this.colorGroupBox1, 2);
      this.colorGroupBox1.TabStop = false;
      componentResourceManager.ApplyResources((object) this.cboSetAllLogLevels, "cboSetAllLogLevels");
      this.cboSetAllLogLevels.DropDownStyle = ComboBoxStyle.DropDownList;
      this.focusExtender1.SetFocusBackColor((Control) this.cboSetAllLogLevels, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboSetAllLogLevels, SystemColors.WindowText);
      this.cboSetAllLogLevels.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboSetAllLogLevels, componentResourceManager.GetString("cboSetAllLogLevels.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboSetAllLogLevels, (HelpNavigator) componentResourceManager.GetObject("cboSetAllLogLevels.HelpNavigator"));
      this.cboSetAllLogLevels.Name = "cboSetAllLogLevels";
      this.helpProvider1.SetShowHelp((Control) this.cboSetAllLogLevels, (bool) componentResourceManager.GetObject("cboSetAllLogLevels.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnSetAllLogLevels, "btnSetAllLogLevels");
      this.helpProvider1.SetHelpKeyword((Control) this.btnSetAllLogLevels, componentResourceManager.GetString("btnSetAllLogLevels.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnSetAllLogLevels, (HelpNavigator) componentResourceManager.GetObject("btnSetAllLogLevels.HelpNavigator"));
      this.btnSetAllLogLevels.Name = "btnSetAllLogLevels";
      this.helpProvider1.SetShowHelp((Control) this.btnSetAllLogLevels, (bool) componentResourceManager.GetObject("btnSetAllLogLevels.ShowHelp"));
      this.btnSetAllLogLevels.UseVisualStyleBackColor = true;
      this.btnSetAllLogLevels.Click += new EventHandler(this.btnSetAllLogLevels_Click);
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.panel1, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.colorGroupBox1, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.gbxLevelDescription, 1, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.grpExternalLogging, 0, 2);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.panel1.Controls.Add((Control) this.btnViewLog);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      componentResourceManager.ApplyResources((object) this.btnViewLog, "btnViewLog");
      this.helpProvider1.SetHelpKeyword((Control) this.btnViewLog, componentResourceManager.GetString("btnViewLog.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnViewLog, (HelpNavigator) componentResourceManager.GetObject("btnViewLog.HelpNavigator"));
      this.btnViewLog.Name = "btnViewLog";
      this.helpProvider1.SetShowHelp((Control) this.btnViewLog, (bool) componentResourceManager.GetObject("btnViewLog.ShowHelp"));
      this.btnViewLog.UseVisualStyleBackColor = true;
      this.btnViewLog.Click += new EventHandler(this.btnViewLog_Click);
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      this.grpExternalLogging.BorderThickness = 1f;
      this.tableLayoutPanel1.SetColumnSpan((Control) this.grpExternalLogging, 2);
      this.grpExternalLogging.Controls.Add((Control) this.chkRateLogging);
      componentResourceManager.ApplyResources((object) this.grpExternalLogging, "grpExternalLogging");
      this.grpExternalLogging.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.grpExternalLogging.Name = "grpExternalLogging";
      this.grpExternalLogging.RoundCorners = 5;
      this.grpExternalLogging.TabStop = false;
      componentResourceManager.ApplyResources((object) this.chkRateLogging, "chkRateLogging");
      this.chkRateLogging.Name = "chkRateLogging";
      this.chkRateLogging.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (SystemSettingsLogging);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.SystemSettingsLogging_Load);
      this.gbxLevelDescription.ResumeLayout(false);
      ((ISupportInitialize) this.dgvLogLevels).EndInit();
      this.colorGroupBox1.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.grpExternalLogging.ResumeLayout(false);
      this.grpExternalLogging.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
