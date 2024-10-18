// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.CustomLabelProfiles
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Properties;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.ValidatorCustomLabel;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.DataAccess;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class CustomLabelProfiles : HelpFormBase
  {
    private IContainer components;
    private FlowLayoutPanel flowLayoutPanel1;
    protected Button btnOk;
    protected Button btnAdd;
    protected Button btnAddByDup;
    protected Button btnViewEdit;
    protected Button btnDelete;
    private DataGridView dataGridView;
    private ContextMenuStrip menuAddPopup;
    private ToolStripMenuItem customLabelToolStripMenuItem;
    private ToolStripMenuItem validatorToolStripMenuItem;
    private DataGridViewImageColumn colImage;
    private DataGridViewTextBoxColumn colCode;
    private DataGridViewTextBoxColumn colDesc;

    public event TopicDelegate ProfilesListChanged;

    public event TopicDelegate ValidatorProfilesListChanged;

    public CustomLabelProfiles() => this.InitializeComponent();

    private void RefreshList(string sCode, string sType)
    {
      this.RefreshList();
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridView.Rows)
      {
        if (row.Cells[1].Value.ToString() == sCode && row.Cells[0].Value.ToString() == sType)
        {
          row.Selected = true;
          this.dataGridView.CurrentCell = row.Cells[0];
          this.EnableButtons();
          break;
        }
      }
    }

    private void RefreshList()
    {
      List<GsmFilter> filterList = new List<GsmFilter>();
      List<GsmSort> sortList = new List<GsmSort>();
      DataTable output;
      GuiData.AppController.ShipEngine.GetDataList((object) null, GsmDataAccess.ListSpecification.CustomLabel_Menu, out output, filterList, sortList, (List<string>) null, new Error());
      if (output == null || output.Rows.Count == 0)
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "No default Profiles were created on initial new install");
      output.DefaultView.Sort = "ProfileCode ASC";
      this.dataGridView.AutoGenerateColumns = false;
      this.dataGridView.DataSource = (object) output;
      this.EnableButtons();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      bool bVal;
      new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI).GetProfileBool("Settings", "CUSTOMLABELENABLED", out bVal, true);
      if (!bVal)
        this.menuAddPopup.Items[0].Visible = false;
      this.btnAdd.ContextMenuStrip.Show((Control) this.btnAdd, 0, this.btnAdd.Height);
    }

    private void btnAddByDup_Click(object sender, EventArgs e)
    {
      this.ShowPrefDialog(Utility.FormOperation.AddByDup, string.Empty);
    }

    private void btnViewEdit_Click(object sender, EventArgs e)
    {
      this.ShowPrefDialog(Utility.FormOperation.ViewEdit, string.Empty);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GuiData.Languafier.TranslateMessage(6010), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      DataRow dataRow = Utility.CurrentRow(this.dataGridView);
      if (dataRow == null)
        return;
      if (dataRow["Type"].ToString() == "V")
      {
        ValidatorConfig input = new ValidatorConfig();
        input.Code = dataRow["ProfileCode"].ToString();
        try
        {
          if (GuiData.AppController.ShipEngine.Delete<ValidatorConfig>(input).Error.Code == 1)
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Successfully deleted Profile");
            EventArgs args = new EventArgs();
            if (this.ValidatorProfilesListChanged != null)
              this.ValidatorProfilesListChanged((object) this, args);
            GuiData.AppController.RemoveValidatorProfile(input.Code);
          }
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Failed to delete Profile");
        }
        catch
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Caught exception deleting Profile");
        }
      }
      else
      {
        if (!(dataRow["Type"].ToString() == "C"))
          return;
        CustomLabelConfig input = new CustomLabelConfig();
        input.Code = dataRow["ProfileCode"].ToString();
        try
        {
          if (GuiData.AppController.ShipEngine.Delete<CustomLabelConfig>(input).Error.Code == 1)
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Successfully deleted Profile");
            EventArgs args = new EventArgs();
            if (this.ProfilesListChanged != null)
              this.ProfilesListChanged((object) this, args);
            GuiData.AppController.RemoveCustomLabelProfile(input.Code);
          }
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Failed to delete Profile");
        }
        catch
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Caught exception deleting Profile");
        }
      }
      this.RefreshList();
    }

    private void customLabelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ShowPrefDialog(Utility.FormOperation.Add, "C");
    }

    private void validatorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ShowPrefDialog(Utility.FormOperation.Add, "V");
    }

    private void ShowPrefDialog(Utility.FormOperation eOperation)
    {
      this.ShowPrefDialog(eOperation, string.Empty);
    }

    private void ShowPrefDialog(Utility.FormOperation eOperation, string ProfileType)
    {
      Error error = new Error();
      string empty = string.Empty;
      if (eOperation == Utility.FormOperation.AddByDup || eOperation == Utility.FormOperation.ViewEdit)
      {
        DataRow dataRow = Utility.CurrentRow(this.dataGridView);
        if (dataRow != null)
        {
          ProfileType = dataRow["Type"].ToString();
          empty = dataRow["ProfileCode"].ToString();
        }
      }
      using (frmCustLabelConfig frmCustLabelConfig = new frmCustLabelConfig(ProfileType.ToUpper() == "C" ? CustomLabelControl.LabelTypes.Custom : CustomLabelControl.LabelTypes.Validator))
      {
        if (!string.IsNullOrEmpty(empty))
          frmCustLabelConfig.CustomLabelProfileCode = empty;
        frmCustLabelConfig.FormOperation = eOperation;
        if (frmCustLabelConfig.ShowDialog((IWin32Window) this) != DialogResult.OK)
          return;
        string labelProfileCode = frmCustLabelConfig.CustomLabelProfileCode;
        EventArgs args = new EventArgs();
        if (frmCustLabelConfig.LabelType == CustomLabelControl.LabelTypes.Validator)
        {
          if (this.ValidatorProfilesListChanged != null)
            this.ValidatorProfilesListChanged((object) this, args);
          GuiData.AppController.RemoveValidatorProfile(labelProfileCode);
        }
        else
        {
          if (this.ProfilesListChanged != null)
            this.ProfilesListChanged((object) this, args);
          GuiData.AppController.RemoveCustomLabelProfile(labelProfileCode);
        }
        this.RefreshList();
        this.SetGridFocus(ProfileType, labelProfileCode);
      }
    }

    private void SetGridFocus(string ProfileType, string profileName)
    {
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridView.Rows)
      {
        if (row.Cells[1].Value.ToString() == profileName && row.Cells[0].Value.ToString() == ProfileType)
        {
          row.Selected = true;
          this.dataGridView.CurrentCell = row.Cells[0];
          this.EnableButtons();
          break;
        }
      }
    }

    private void dataGridView_SelectionChanged(object sender, EventArgs e) => this.EnableButtons();

    private void EnableButtons()
    {
      int count = this.dataGridView.SelectedRows.Count;
      this.btnAdd.Enabled = true;
      this.btnAddByDup.Enabled = count == 1;
      this.btnViewEdit.Enabled = count == 1;
      this.btnDelete.Enabled = count == 1;
    }

    private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.ColumnIndex != 0)
        return;
      switch (e.Value.ToString())
      {
        case "V":
          e.Value = (object) Resources.ValidatorImage;
          break;
        default:
          e.Value = (object) Resources.CustomImage;
          break;
      }
    }

    private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex <= -1)
        return;
      this.btnViewEdit_Click(sender, (EventArgs) e);
    }

    private void CustomLabelProfiles_Load(object sender, EventArgs e)
    {
      this.SetupEvents();
      Utility.UpdateToolstripFonts((ToolStrip) this.menuAddPopup);
      this.RefreshList();
    }

    private void SetupEvents()
    {
      GuiData.EventBroker.GetTopic(EventBroker.Events.CustomLabelProfilesListChanged)?.AddPublisher((object) this, "ProfilesListChanged");
      GuiData.EventBroker.GetTopic(EventBroker.Events.ValidatorLabelProfilesListChanged)?.AddPublisher((object) this, "ValidatorProfilesListChanged");
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CustomLabelProfiles));
      this.flowLayoutPanel1 = new FlowLayoutPanel();
      this.btnOk = new Button();
      this.btnAdd = new Button();
      this.menuAddPopup = new ContextMenuStrip(this.components);
      this.customLabelToolStripMenuItem = new ToolStripMenuItem();
      this.validatorToolStripMenuItem = new ToolStripMenuItem();
      this.btnAddByDup = new Button();
      this.btnViewEdit = new Button();
      this.btnDelete = new Button();
      this.dataGridView = new DataGridView();
      this.colImage = new DataGridViewImageColumn();
      this.colCode = new DataGridViewTextBoxColumn();
      this.colDesc = new DataGridViewTextBoxColumn();
      this.flowLayoutPanel1.SuspendLayout();
      this.menuAddPopup.SuspendLayout();
      ((ISupportInitialize) this.dataGridView).BeginInit();
      this.SuspendLayout();
      this.flowLayoutPanel1.Controls.Add((Control) this.btnOk);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnAdd);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnAddByDup);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnViewEdit);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnDelete);
      componentResourceManager.ApplyResources((object) this.flowLayoutPanel1, "flowLayoutPanel1");
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.btnOk.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
      this.btnAdd.ContextMenuStrip = this.menuAddPopup;
      this.helpProvider1.SetHelpKeyword((Control) this.btnAdd, componentResourceManager.GetString("btnAdd.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnAdd, (HelpNavigator) componentResourceManager.GetObject("btnAdd.HelpNavigator"));
      this.btnAdd.Name = "btnAdd";
      this.helpProvider1.SetShowHelp((Control) this.btnAdd, (bool) componentResourceManager.GetObject("btnAdd.ShowHelp"));
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
      this.menuAddPopup.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.customLabelToolStripMenuItem,
        (ToolStripItem) this.validatorToolStripMenuItem
      });
      this.menuAddPopup.Name = "menuAddPopup";
      componentResourceManager.ApplyResources((object) this.menuAddPopup, "menuAddPopup");
      this.customLabelToolStripMenuItem.Image = (Image) Resources.CustomImage;
      this.customLabelToolStripMenuItem.Name = "customLabelToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.customLabelToolStripMenuItem, "customLabelToolStripMenuItem");
      this.customLabelToolStripMenuItem.Click += new EventHandler(this.customLabelToolStripMenuItem_Click);
      this.validatorToolStripMenuItem.Image = (Image) Resources.ValidatorImage;
      this.validatorToolStripMenuItem.Name = "validatorToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.validatorToolStripMenuItem, "validatorToolStripMenuItem");
      this.validatorToolStripMenuItem.Click += new EventHandler(this.validatorToolStripMenuItem_Click);
      componentResourceManager.ApplyResources((object) this.btnAddByDup, "btnAddByDup");
      this.helpProvider1.SetHelpKeyword((Control) this.btnAddByDup, componentResourceManager.GetString("btnAddByDup.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnAddByDup, (HelpNavigator) componentResourceManager.GetObject("btnAddByDup.HelpNavigator"));
      this.btnAddByDup.Name = "btnAddByDup";
      this.helpProvider1.SetShowHelp((Control) this.btnAddByDup, (bool) componentResourceManager.GetObject("btnAddByDup.ShowHelp"));
      this.btnAddByDup.UseVisualStyleBackColor = true;
      this.btnAddByDup.Click += new EventHandler(this.btnAddByDup_Click);
      componentResourceManager.ApplyResources((object) this.btnViewEdit, "btnViewEdit");
      this.helpProvider1.SetHelpKeyword((Control) this.btnViewEdit, componentResourceManager.GetString("btnViewEdit.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnViewEdit, (HelpNavigator) componentResourceManager.GetObject("btnViewEdit.HelpNavigator"));
      this.btnViewEdit.Name = "btnViewEdit";
      this.helpProvider1.SetShowHelp((Control) this.btnViewEdit, (bool) componentResourceManager.GetObject("btnViewEdit.ShowHelp"));
      this.btnViewEdit.UseVisualStyleBackColor = true;
      this.btnViewEdit.Click += new EventHandler(this.btnViewEdit_Click);
      componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
      this.dataGridView.AllowUserToAddRows = false;
      this.dataGridView.AllowUserToDeleteRows = false;
      this.dataGridView.AllowUserToResizeRows = false;
      componentResourceManager.ApplyResources((object) this.dataGridView, "dataGridView");
      this.dataGridView.BackgroundColor = SystemColors.Window;
      this.dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView.Columns.AddRange((DataGridViewColumn) this.colImage, (DataGridViewColumn) this.colCode, (DataGridViewColumn) this.colDesc);
      this.helpProvider1.SetHelpKeyword((Control) this.dataGridView, componentResourceManager.GetString("dataGridView.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.dataGridView, (HelpNavigator) componentResourceManager.GetObject("dataGridView.HelpNavigator"));
      this.dataGridView.MultiSelect = false;
      this.dataGridView.Name = "dataGridView";
      this.dataGridView.ReadOnly = true;
      this.dataGridView.RowHeadersVisible = false;
      this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView.ShowCellErrors = false;
      this.dataGridView.ShowEditingIcon = false;
      this.helpProvider1.SetShowHelp((Control) this.dataGridView, (bool) componentResourceManager.GetObject("dataGridView.ShowHelp"));
      this.dataGridView.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
      this.dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
      this.dataGridView.SelectionChanged += new EventHandler(this.dataGridView_SelectionChanged);
      this.colImage.DataPropertyName = "Type";
      componentResourceManager.ApplyResources((object) this.colImage, "colImage");
      this.colImage.Name = "colImage";
      this.colImage.ReadOnly = true;
      this.colImage.Resizable = DataGridViewTriState.False;
      this.colCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.colCode.DataPropertyName = "ProfileCode";
      componentResourceManager.ApplyResources((object) this.colCode, "colCode");
      this.colCode.Name = "colCode";
      this.colCode.ReadOnly = true;
      this.colCode.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.colDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.colDesc.DataPropertyName = "Description";
      componentResourceManager.ApplyResources((object) this.colDesc, "colDesc");
      this.colDesc.Name = "colDesc";
      this.colDesc.ReadOnly = true;
      this.colDesc.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnOk;
      this.Controls.Add((Control) this.dataGridView);
      this.Controls.Add((Control) this.flowLayoutPanel1);
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (CustomLabelProfiles);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Load += new EventHandler(this.CustomLabelProfiles_Load);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.menuAddPopup.ResumeLayout(false);
      ((ISupportInitialize) this.dataGridView).EndInit();
      this.ResumeLayout(false);
    }
  }
}
