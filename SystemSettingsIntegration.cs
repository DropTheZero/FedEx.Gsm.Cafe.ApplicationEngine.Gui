// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SystemSettingsIntegration
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.ConfigManager;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class SystemSettingsIntegration : UserControlHelpEx
  {
    private bool _fxiaVersionChanged;
    private SystemSettings _myParent;
    private const int CustomLabelLoadAllImagesIndex = 0;
    private const int CustomLabelDeleteAllImagesIndex = 1;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private ColorGroupBox colorGroupBox4;
    private Panel panel1;
    private ColorGroupBox colorGroupBox1;
    private ComboBox cboLabelType;
    private Label label1;
    private RadioButton chkPrintLabelToFile;
    private CheckBox chkTurnOffRecipientCombo;
    private CheckBox chkFXI;
    private CheckBox chkEnableDataListeningOnPort2001;
    private Label label2;
    private FocusExtender focusExtender1;
    private ComboBox cboLabelFormat;
    private Label lblLabelFormat;
    private RadioButton rdoLabelFormat;
    private Button btnImageLocation;
    private TextBox txtImageLocation;
    private Panel pnlLabelFormat;
    private Label lblImageLocation;
    private ColorGroupBox grpLabelOptions;
    private RadioButton rdbNone;
    private Panel pnlCustomLabelImages;
    private Button btnUpdateCustomLabelImage;
    private ComboBoxEx cboCustomLabelImages;
    private Label lblCustomLabelImages;
    private GroupBox groupBox1;

    public SystemSettingsIntegration()
    {
      this.InitializeComponent();
      this.cboLabelType.SelectedIndex = 0;
    }

    public bool IsIntegrationChecked => this.chkFXI.Checked;

    public bool HasFxiaVersionChanged => this._fxiaVersionChanged;

    public SystemSettings MyParent
    {
      get => this._myParent;
      set => this._myParent = value;
    }

    public void ScreenToObjects(SystemSettingsObject settings)
    {
      settings.RegistrySettings["UseRecipientEdit"].Value = this.chkTurnOffRecipientCombo.Checked ? (object) "Y" : (object) "N";
      settings.RegistrySettings["DataListenEnabled"].Value = this.chkEnableDataListeningOnPort2001.Checked ? (object) "Y" : (object) "N";
      settings.RegistrySettings["VBAIntegration"].Value = this.chkFXI.Checked ? (object) "Y" : (object) "N";
      if (this.chkPrintLabelToFile.Checked)
      {
        int num = this.cboLabelType.SelectedIndex + 1;
        settings.RegistrySettings["PrintLabelToFile"].Value = (object) num.ToString();
      }
      else
        settings.RegistrySettings["PrintLabelToFile"].Value = (object) "0";
      if (this.rdoLabelFormat.Checked && this.cboLabelFormat.SelectedIndex > 0)
      {
        settings.RegistrySettings["LABELFORMAT"].Value = (object) this.cboLabelFormat.SelectedIndex.ToString();
        settings.RegistrySettings["LABELLOCATION"].Value = !string.IsNullOrEmpty(this.txtImageLocation.Text) ? (object) this.txtImageLocation.Text : (object) DataFileLocations.Instance.TempDirectory;
      }
      else
      {
        settings.RegistrySettings["LABELFORMAT"].Value = (object) "0";
        settings.RegistrySettings["LABELLOCATION"].Value = (object) string.Empty;
      }
    }

    public int ObjectsToScreen(SystemSettingsObject settings, Utility.FormOperation op)
    {
      if (settings.RegistrySettings != null)
      {
        this.chkTurnOffRecipientCombo.Checked = settings.RegistrySettings["UseRecipientEdit"].Value.ToString().ToUpper() == "Y";
        this.chkEnableDataListeningOnPort2001.Checked = settings.RegistrySettings["DataListenEnabled"].Value.ToString().ToUpper() == "Y";
        this.chkFXI.Checked = settings.RegistrySettings["VBAIntegration"].Value.ToString().ToUpper() == "Y" || bool.TrueString.Equals(settings.RegistrySettings["VBAIntegration"].Value as string, StringComparison.InvariantCultureIgnoreCase);
        int result;
        if (int.TryParse(settings.RegistrySettings["PrintLabelToFile"].Value as string, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result) && result > 0)
        {
          this.chkPrintLabelToFile.Checked = true;
          this.cboLabelType.SelectedIndex = result - 1;
        }
        else if (int.TryParse(settings.RegistrySettings["LABELFORMAT"].Value as string, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result) && result > 0)
        {
          this.rdoLabelFormat.Checked = true;
          this.cboLabelFormat.SelectedIndex = result;
          this.txtImageLocation.Text = settings.RegistrySettings["LABELLOCATION"].Value as string;
        }
        else
          this.rdbNone.Checked = true;
      }
      return 0;
    }

    public bool HandleError(Error error)
    {
      Utility.DisplayError(error);
      return error.Code != 1 && this.HandleError(error.Code);
    }

    public bool HandleError(int error)
    {
      Control control = (Control) null;
      int num = 0;
      if (num == 0)
        return num != 0;
      if (control == null)
        return num != 0;
      if (!(this.Parent.Parent is SystemSettings parent))
        return num != 0;
      if (parent.SystemSettingsTabControl.SelectedTab == this.Parent)
        return num != 0;
      parent.SystemSettingsTabControl.SelectedTab = this.Parent as TabPage;
      control.Focus();
      if (control.Enabled)
        return num != 0;
      control.Enabled = true;
      return num != 0;
    }

    public void SetupEvents()
    {
    }

    private void chkPrintLabelToFile_CheckedChanged(object sender, EventArgs e)
    {
      this.cboLabelType.Enabled = this.chkPrintLabelToFile.Checked;
    }

    private void chkFXI_Click(object sender, EventArgs e)
    {
      this.chkFXI.Checked = !this.chkFXI.Checked;
      if (!this.chkFXI.Checked || !this._myParent.UserPage.IsPreReadChecked)
        return;
      int num = (int) MessageBox.Show(GuiData.Languafier.TranslateMessage(43336), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private void rdoLabelFormat_CheckedChanged(object sender, EventArgs e)
    {
      this.pnlLabelFormat.Enabled = this.rdoLabelFormat.Checked;
    }

    private void btnImageLocation_Click(object sender, EventArgs e)
    {
      using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
      {
        bool flag;
        do
        {
          flag = false;
          if (folderBrowserDialog.ShowDialog((IWin32Window) this) == DialogResult.OK)
          {
            string text = Utility.TestFilePath(folderBrowserDialog.SelectedPath, false);
            if (!string.IsNullOrEmpty(text) && !"DO NOT DISPLAY MSG".Equals(text))
            {
              int num = (int) MessageBox.Show((IWin32Window) this, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Hand);
              flag = true;
            }
            else
              this.txtImageLocation.Text = folderBrowserDialog.SelectedPath;
          }
        }
        while (flag);
      }
    }

    private void cboCustomLabelImages_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.btnUpdateCustomLabelImage.Enabled = true;
    }

    private void btnUpdateCustomLabelImage_Click(object sender, EventArgs e)
    {
      if (this.MyParent == null || this.MyParent.Operation == Utility.FormOperation.Add || this.MyParent.Operation == Utility.FormOperation.AddFirst || this.MyParent.Operation == Utility.FormOperation.AddByDup)
        return;
      switch (this.cboCustomLabelImages.SelectedIndex)
      {
        case 0:
          this.LoadImagesToPrinter();
          break;
        case 1:
          this.ClearImagesFromPrinter();
          break;
      }
    }

    private void LoadImagesToPrinter()
    {
      GuiData.AppController.ShipEngine.PublishMessage(new BroadcastMessage(OperationType.UpdateAllCustomLabelImages, string.Empty, EventArgs.Empty, GuiData.ConfigManager.NetworkClientID), false);
    }

    private void ClearImagesFromPrinter()
    {
      GuiData.AppController.ShipEngine.PublishMessage(new BroadcastMessage(OperationType.EraseAllCustomLabelImages, string.Empty, EventArgs.Empty, GuiData.ConfigManager.NetworkClientID), false);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SystemSettingsIntegration));
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.colorGroupBox4 = new ColorGroupBox();
      this.chkTurnOffRecipientCombo = new CheckBox();
      this.chkFXI = new CheckBox();
      this.grpLabelOptions = new ColorGroupBox();
      this.pnlCustomLabelImages = new Panel();
      this.btnUpdateCustomLabelImage = new Button();
      this.cboCustomLabelImages = new ComboBoxEx();
      this.lblCustomLabelImages = new Label();
      this.rdbNone = new RadioButton();
      this.rdoLabelFormat = new RadioButton();
      this.cboLabelType = new ComboBox();
      this.pnlLabelFormat = new Panel();
      this.lblImageLocation = new Label();
      this.txtImageLocation = new TextBox();
      this.btnImageLocation = new Button();
      this.cboLabelFormat = new ComboBox();
      this.lblLabelFormat = new Label();
      this.label1 = new Label();
      this.chkPrintLabelToFile = new RadioButton();
      this.panel1 = new Panel();
      this.label2 = new Label();
      this.colorGroupBox1 = new ColorGroupBox();
      this.chkEnableDataListeningOnPort2001 = new CheckBox();
      this.groupBox1 = new GroupBox();
      this.focusExtender1 = new FocusExtender();
      this.tableLayoutPanel1.SuspendLayout();
      this.colorGroupBox4.SuspendLayout();
      this.grpLabelOptions.SuspendLayout();
      this.pnlCustomLabelImages.SuspendLayout();
      this.pnlLabelFormat.SuspendLayout();
      this.panel1.SuspendLayout();
      this.colorGroupBox1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.colorGroupBox4, 0, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.panel1, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.colorGroupBox1, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.groupBox1, 1, 1);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.helpProvider1.SetShowHelp((Control) this.tableLayoutPanel1, (bool) componentResourceManager.GetObject("tableLayoutPanel1.ShowHelp"));
      this.colorGroupBox4.BorderThickness = 1f;
      this.tableLayoutPanel1.SetColumnSpan((Control) this.colorGroupBox4, 2);
      this.colorGroupBox4.Controls.Add((Control) this.chkTurnOffRecipientCombo);
      this.colorGroupBox4.Controls.Add((Control) this.chkFXI);
      this.colorGroupBox4.Controls.Add((Control) this.grpLabelOptions);
      componentResourceManager.ApplyResources((object) this.colorGroupBox4, "colorGroupBox4");
      this.colorGroupBox4.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.helpProvider1.SetHelpKeyword((Control) this.colorGroupBox4, componentResourceManager.GetString("colorGroupBox4.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.colorGroupBox4, (HelpNavigator) componentResourceManager.GetObject("colorGroupBox4.HelpNavigator"));
      this.colorGroupBox4.Name = "colorGroupBox4";
      this.colorGroupBox4.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox4, (bool) componentResourceManager.GetObject("colorGroupBox4.ShowHelp"));
      this.colorGroupBox4.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkTurnOffRecipientCombo, componentResourceManager.GetString("chkTurnOffRecipientCombo.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkTurnOffRecipientCombo, (HelpNavigator) componentResourceManager.GetObject("chkTurnOffRecipientCombo.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkTurnOffRecipientCombo, "chkTurnOffRecipientCombo");
      this.chkTurnOffRecipientCombo.Name = "chkTurnOffRecipientCombo";
      this.helpProvider1.SetShowHelp((Control) this.chkTurnOffRecipientCombo, (bool) componentResourceManager.GetObject("chkTurnOffRecipientCombo.ShowHelp"));
      this.chkTurnOffRecipientCombo.UseVisualStyleBackColor = true;
      this.chkFXI.AutoCheck = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkFXI, componentResourceManager.GetString("chkFXI.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkFXI, (HelpNavigator) componentResourceManager.GetObject("chkFXI.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkFXI, "chkFXI");
      this.chkFXI.Name = "chkFXI";
      this.helpProvider1.SetShowHelp((Control) this.chkFXI, (bool) componentResourceManager.GetObject("chkFXI.ShowHelp"));
      this.chkFXI.UseVisualStyleBackColor = true;
      this.chkFXI.Click += new EventHandler(this.chkFXI_Click);
      this.grpLabelOptions.BorderThickness = 1f;
      this.grpLabelOptions.Controls.Add((Control) this.pnlCustomLabelImages);
      this.grpLabelOptions.Controls.Add((Control) this.rdbNone);
      this.grpLabelOptions.Controls.Add((Control) this.rdoLabelFormat);
      this.grpLabelOptions.Controls.Add((Control) this.cboLabelType);
      this.grpLabelOptions.Controls.Add((Control) this.pnlLabelFormat);
      this.grpLabelOptions.Controls.Add((Control) this.label1);
      this.grpLabelOptions.Controls.Add((Control) this.chkPrintLabelToFile);
      this.grpLabelOptions.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.grpLabelOptions, "grpLabelOptions");
      this.grpLabelOptions.Name = "grpLabelOptions";
      this.grpLabelOptions.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.grpLabelOptions, (bool) componentResourceManager.GetObject("grpLabelOptions.ShowHelp"));
      this.grpLabelOptions.TabStop = false;
      this.pnlCustomLabelImages.Controls.Add((Control) this.btnUpdateCustomLabelImage);
      this.pnlCustomLabelImages.Controls.Add((Control) this.cboCustomLabelImages);
      this.pnlCustomLabelImages.Controls.Add((Control) this.lblCustomLabelImages);
      componentResourceManager.ApplyResources((object) this.pnlCustomLabelImages, "pnlCustomLabelImages");
      this.pnlCustomLabelImages.Name = "pnlCustomLabelImages";
      componentResourceManager.ApplyResources((object) this.btnUpdateCustomLabelImage, "btnUpdateCustomLabelImage");
      this.btnUpdateCustomLabelImage.Name = "btnUpdateCustomLabelImage";
      this.btnUpdateCustomLabelImage.UseVisualStyleBackColor = true;
      this.btnUpdateCustomLabelImage.Click += new EventHandler(this.btnUpdateCustomLabelImage_Click);
      this.cboCustomLabelImages.DisplayMemberQ = "";
      this.cboCustomLabelImages.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboCustomLabelImages.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboCustomLabelImages, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboCustomLabelImages, SystemColors.WindowText);
      this.cboCustomLabelImages.FormattingEnabled = true;
      this.cboCustomLabelImages.Items.AddRange(new object[2]
      {
        (object) componentResourceManager.GetString("cboCustomLabelImages.Items"),
        (object) componentResourceManager.GetString("cboCustomLabelImages.Items1")
      });
      componentResourceManager.ApplyResources((object) this.cboCustomLabelImages, "cboCustomLabelImages");
      this.cboCustomLabelImages.Name = "cboCustomLabelImages";
      this.cboCustomLabelImages.SelectedIndexQ = -1;
      this.cboCustomLabelImages.SelectedItemQ = (object) null;
      this.cboCustomLabelImages.SelectedValueQ = (object) null;
      this.cboCustomLabelImages.ValueMemberQ = "";
      this.cboCustomLabelImages.SelectedIndexChanged += new EventHandler(this.cboCustomLabelImages_SelectedIndexChanged);
      componentResourceManager.ApplyResources((object) this.lblCustomLabelImages, "lblCustomLabelImages");
      this.lblCustomLabelImages.Name = "lblCustomLabelImages";
      componentResourceManager.ApplyResources((object) this.rdbNone, "rdbNone");
      this.rdbNone.Name = "rdbNone";
      this.helpProvider1.SetShowHelp((Control) this.rdbNone, (bool) componentResourceManager.GetObject("rdbNone.ShowHelp"));
      this.rdbNone.TabStop = true;
      this.rdbNone.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.rdoLabelFormat, "rdoLabelFormat");
      this.rdoLabelFormat.Name = "rdoLabelFormat";
      this.helpProvider1.SetShowHelp((Control) this.rdoLabelFormat, (bool) componentResourceManager.GetObject("rdoLabelFormat.ShowHelp"));
      this.rdoLabelFormat.TabStop = true;
      this.rdoLabelFormat.UseVisualStyleBackColor = true;
      this.rdoLabelFormat.CheckedChanged += new EventHandler(this.rdoLabelFormat_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.cboLabelType, "cboLabelType");
      this.focusExtender1.SetFocusBackColor((Control) this.cboLabelType, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboLabelType, SystemColors.WindowText);
      this.cboLabelType.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboLabelType, componentResourceManager.GetString("cboLabelType.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboLabelType, (HelpNavigator) componentResourceManager.GetObject("cboLabelType.HelpNavigator"));
      this.cboLabelType.Items.AddRange(new object[2]
      {
        (object) componentResourceManager.GetString("cboLabelType.Items"),
        (object) componentResourceManager.GetString("cboLabelType.Items1")
      });
      this.cboLabelType.Name = "cboLabelType";
      this.helpProvider1.SetShowHelp((Control) this.cboLabelType, (bool) componentResourceManager.GetObject("cboLabelType.ShowHelp"));
      this.pnlLabelFormat.Controls.Add((Control) this.lblImageLocation);
      this.pnlLabelFormat.Controls.Add((Control) this.txtImageLocation);
      this.pnlLabelFormat.Controls.Add((Control) this.btnImageLocation);
      this.pnlLabelFormat.Controls.Add((Control) this.cboLabelFormat);
      this.pnlLabelFormat.Controls.Add((Control) this.lblLabelFormat);
      componentResourceManager.ApplyResources((object) this.pnlLabelFormat, "pnlLabelFormat");
      this.pnlLabelFormat.Name = "pnlLabelFormat";
      this.helpProvider1.SetShowHelp((Control) this.pnlLabelFormat, (bool) componentResourceManager.GetObject("pnlLabelFormat.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblImageLocation, "lblImageLocation");
      this.lblImageLocation.Name = "lblImageLocation";
      this.helpProvider1.SetShowHelp((Control) this.lblImageLocation, (bool) componentResourceManager.GetObject("lblImageLocation.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.txtImageLocation, "txtImageLocation");
      this.focusExtender1.SetFocusBackColor((Control) this.txtImageLocation, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.txtImageLocation, SystemColors.WindowText);
      this.txtImageLocation.Name = "txtImageLocation";
      this.txtImageLocation.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.txtImageLocation, (bool) componentResourceManager.GetObject("txtImageLocation.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnImageLocation, "btnImageLocation");
      this.btnImageLocation.Name = "btnImageLocation";
      this.helpProvider1.SetShowHelp((Control) this.btnImageLocation, (bool) componentResourceManager.GetObject("btnImageLocation.ShowHelp"));
      this.btnImageLocation.UseVisualStyleBackColor = true;
      this.btnImageLocation.Click += new EventHandler(this.btnImageLocation_Click);
      componentResourceManager.ApplyResources((object) this.cboLabelFormat, "cboLabelFormat");
      this.cboLabelFormat.DropDownStyle = ComboBoxStyle.DropDownList;
      this.focusExtender1.SetFocusBackColor((Control) this.cboLabelFormat, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboLabelFormat, SystemColors.WindowText);
      this.cboLabelFormat.FormattingEnabled = true;
      this.cboLabelFormat.Items.AddRange(new object[3]
      {
        (object) componentResourceManager.GetString("cboLabelFormat.Items"),
        (object) componentResourceManager.GetString("cboLabelFormat.Items1"),
        (object) componentResourceManager.GetString("cboLabelFormat.Items2")
      });
      this.cboLabelFormat.Name = "cboLabelFormat";
      this.helpProvider1.SetShowHelp((Control) this.cboLabelFormat, (bool) componentResourceManager.GetObject("cboLabelFormat.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lblLabelFormat, "lblLabelFormat");
      this.lblLabelFormat.Name = "lblLabelFormat";
      this.helpProvider1.SetShowHelp((Control) this.lblLabelFormat, (bool) componentResourceManager.GetObject("lblLabelFormat.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.helpProvider1.SetShowHelp((Control) this.label1, (bool) componentResourceManager.GetObject("label1.ShowHelp"));
      this.helpProvider1.SetHelpKeyword((Control) this.chkPrintLabelToFile, componentResourceManager.GetString("chkPrintLabelToFile.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkPrintLabelToFile, (HelpNavigator) componentResourceManager.GetObject("chkPrintLabelToFile.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkPrintLabelToFile, "chkPrintLabelToFile");
      this.chkPrintLabelToFile.Name = "chkPrintLabelToFile";
      this.helpProvider1.SetShowHelp((Control) this.chkPrintLabelToFile, (bool) componentResourceManager.GetObject("chkPrintLabelToFile.ShowHelp"));
      this.chkPrintLabelToFile.UseVisualStyleBackColor = true;
      this.chkPrintLabelToFile.CheckedChanged += new EventHandler(this.chkPrintLabelToFile_CheckedChanged);
      this.tableLayoutPanel1.SetColumnSpan((Control) this.panel1, 2);
      this.panel1.Controls.Add((Control) this.label2);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      this.helpProvider1.SetShowHelp((Control) this.panel1, (bool) componentResourceManager.GetObject("panel1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      this.helpProvider1.SetShowHelp((Control) this.label2, (bool) componentResourceManager.GetObject("label2.ShowHelp"));
      this.colorGroupBox1.BorderThickness = 1f;
      this.colorGroupBox1.Controls.Add((Control) this.chkEnableDataListeningOnPort2001);
      componentResourceManager.ApplyResources((object) this.colorGroupBox1, "colorGroupBox1");
      this.colorGroupBox1.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.colorGroupBox1.Name = "colorGroupBox1";
      this.colorGroupBox1.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.colorGroupBox1, (bool) componentResourceManager.GetObject("colorGroupBox1.ShowHelp"));
      this.colorGroupBox1.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.chkEnableDataListeningOnPort2001, componentResourceManager.GetString("chkEnableDataListeningOnPort2001.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.chkEnableDataListeningOnPort2001, (HelpNavigator) componentResourceManager.GetObject("chkEnableDataListeningOnPort2001.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.chkEnableDataListeningOnPort2001, "chkEnableDataListeningOnPort2001");
      this.chkEnableDataListeningOnPort2001.Name = "chkEnableDataListeningOnPort2001";
      this.helpProvider1.SetShowHelp((Control) this.chkEnableDataListeningOnPort2001, (bool) componentResourceManager.GetObject("chkEnableDataListeningOnPort2001.ShowHelp"));
      this.chkEnableDataListeningOnPort2001.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.TabStop = false;
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.Name = nameof (SystemSettingsIntegration);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.tableLayoutPanel1.ResumeLayout(false);
      this.colorGroupBox4.ResumeLayout(false);
      this.grpLabelOptions.ResumeLayout(false);
      this.pnlCustomLabelImages.ResumeLayout(false);
      this.pnlLabelFormat.ResumeLayout(false);
      this.pnlLabelFormat.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.colorGroupBox1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
