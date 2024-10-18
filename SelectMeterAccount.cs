// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.SelectMeterAccount
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class SelectMeterAccount : HelpFormBase
  {
    private static string _meterNumber = "";
    private static string _accountNumber = "";
    private static string _description = "";
    private static bool _bGroundEnabled = false;
    private static bool _bSmartPostEnabled = false;
    private IContainer components;
    private DataGridView dataMeterAcctGridView;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private Button btnCancel;
    private Button btnRestore;
    private DataGridViewTextBoxColumn colSystemNumber;
    private DataGridViewTextBoxColumn colAccountNumber;
    private DataGridViewTextBoxColumn colDescription;
    private DataGridViewTextBoxColumn colGroundEnabled;
    private DataGridViewTextBoxColumn colSmartPostEnabled;

    internal string MeterNumber
    {
      get => SelectMeterAccount._meterNumber;
      set => SelectMeterAccount._meterNumber = value;
    }

    internal string AccountNumber
    {
      get => SelectMeterAccount._accountNumber;
      set => SelectMeterAccount._accountNumber = value;
    }

    internal string Description
    {
      get => SelectMeterAccount._description;
      set => SelectMeterAccount._description = value;
    }

    internal bool GroundEnabled
    {
      get => SelectMeterAccount._bGroundEnabled;
      set => SelectMeterAccount._bGroundEnabled = value;
    }

    internal bool SmartPostEnabled
    {
      get => SelectMeterAccount._bSmartPostEnabled;
      set => SelectMeterAccount._bSmartPostEnabled = value;
    }

    internal SelectMeterAccount() => this.InitializeComponent();

    internal void PopulateDataGrid(DataTable dt)
    {
      this.dataMeterAcctGridView.AutoGenerateColumns = false;
      this.dataMeterAcctGridView.DataSource = (object) dt;
      if (this.dataMeterAcctGridView.RowCount == 0)
        this.btnRestore.Enabled = false;
      this.dataMeterAcctGridView.Refresh();
    }

    private void btnCancel_Click(object sender, EventArgs e) => this.Dispose();

    private void btnRestore_Click(object sender, EventArgs e)
    {
      SelectMeterAccount._meterNumber = this.dataMeterAcctGridView.SelectedRows[0].Cells["colSystemNumber"].Value.ToString();
      SelectMeterAccount._accountNumber = this.dataMeterAcctGridView.SelectedRows[0].Cells["colAccountNumber"].Value.ToString();
      SelectMeterAccount._description = this.dataMeterAcctGridView.SelectedRows[0].Cells["colDescription"].Value.ToString();
      SelectMeterAccount._bGroundEnabled = this.dataMeterAcctGridView.SelectedRows[0].Cells["colGroundEnabled"].Value.ToString().Equals("true");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectMeterAccount));
      this.dataMeterAcctGridView = new DataGridView();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.btnCancel = new Button();
      this.btnRestore = new Button();
      this.colSystemNumber = new DataGridViewTextBoxColumn();
      this.colAccountNumber = new DataGridViewTextBoxColumn();
      this.colDescription = new DataGridViewTextBoxColumn();
      this.colGroundEnabled = new DataGridViewTextBoxColumn();
      this.colSmartPostEnabled = new DataGridViewTextBoxColumn();
      ((ISupportInitialize) this.dataMeterAcctGridView).BeginInit();
      this.SuspendLayout();
      this.dataMeterAcctGridView.AllowUserToAddRows = false;
      this.dataMeterAcctGridView.AllowUserToDeleteRows = false;
      componentResourceManager.ApplyResources((object) this.dataMeterAcctGridView, "dataMeterAcctGridView");
      this.dataMeterAcctGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataMeterAcctGridView.Columns.AddRange((DataGridViewColumn) this.colSystemNumber, (DataGridViewColumn) this.colAccountNumber, (DataGridViewColumn) this.colDescription, (DataGridViewColumn) this.colGroundEnabled, (DataGridViewColumn) this.colSmartPostEnabled);
      this.dataMeterAcctGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
      this.helpProvider1.SetHelpKeyword((Control) this.dataMeterAcctGridView, componentResourceManager.GetString("dataMeterAcctGridView.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.dataMeterAcctGridView, (HelpNavigator) componentResourceManager.GetObject("dataMeterAcctGridView.HelpNavigator"));
      this.dataMeterAcctGridView.MultiSelect = false;
      this.dataMeterAcctGridView.Name = "dataMeterAcctGridView";
      this.dataMeterAcctGridView.ReadOnly = true;
      this.dataMeterAcctGridView.RowHeadersVisible = false;
      this.dataMeterAcctGridView.RowTemplate.ReadOnly = true;
      this.dataMeterAcctGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.helpProvider1.SetShowHelp((Control) this.dataMeterAcctGridView, (bool) componentResourceManager.GetObject("dataMeterAcctGridView.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      componentResourceManager.ApplyResources((object) this.btnRestore, "btnRestore");
      this.btnRestore.DialogResult = DialogResult.OK;
      this.helpProvider1.SetHelpKeyword((Control) this.btnRestore, componentResourceManager.GetString("btnRestore.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnRestore, (HelpNavigator) componentResourceManager.GetObject("btnRestore.HelpNavigator"));
      this.btnRestore.Name = "btnRestore";
      this.helpProvider1.SetShowHelp((Control) this.btnRestore, (bool) componentResourceManager.GetObject("btnRestore.ShowHelp"));
      this.btnRestore.UseVisualStyleBackColor = true;
      this.btnRestore.Click += new EventHandler(this.btnRestore_Click);
      this.colSystemNumber.DataPropertyName = "MeterNumber";
      componentResourceManager.ApplyResources((object) this.colSystemNumber, "colSystemNumber");
      this.colSystemNumber.Name = "colSystemNumber";
      this.colSystemNumber.ReadOnly = true;
      this.colAccountNumber.DataPropertyName = "AccountNumber";
      componentResourceManager.ApplyResources((object) this.colAccountNumber, "colAccountNumber");
      this.colAccountNumber.Name = "colAccountNumber";
      this.colAccountNumber.ReadOnly = true;
      this.colDescription.DataPropertyName = "Description";
      componentResourceManager.ApplyResources((object) this.colDescription, "colDescription");
      this.colDescription.Name = "colDescription";
      this.colDescription.ReadOnly = true;
      this.colGroundEnabled.DataPropertyName = "GroundEnabled";
      componentResourceManager.ApplyResources((object) this.colGroundEnabled, "colGroundEnabled");
      this.colGroundEnabled.Name = "colGroundEnabled";
      this.colGroundEnabled.ReadOnly = true;
      this.colGroundEnabled.Resizable = DataGridViewTriState.True;
      this.colSmartPostEnabled.DataPropertyName = "SmartPostEnabled";
      componentResourceManager.ApplyResources((object) this.colSmartPostEnabled, "colSmartPostEnabled");
      this.colSmartPostEnabled.Name = "colSmartPostEnabled";
      this.colSmartPostEnabled.ReadOnly = true;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.btnRestore);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.dataMeterAcctGridView);
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (SelectMeterAccount);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      ((ISupportInitialize) this.dataMeterAcctGridView).EndInit();
      this.ResumeLayout(false);
    }
  }
}
