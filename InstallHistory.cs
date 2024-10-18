// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.InstallHistory
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class InstallHistory : HelpFormBase
  {
    private IContainer components;
    private DataGridView dgvInstallHistory;
    private Button btnOk;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private DataGridViewTextBoxColumn InstallDate;
    private DataGridViewTextBoxColumn DeltaHistoryMVersion;
    private DataGridViewTextBoxColumn PackageName;
    private DataGridViewTextBoxColumn PatchVer;
    private DataGridViewTextBoxColumn Description;
    private DataGridViewTextBoxColumn Status;
    private DataGridViewTextBoxColumn Source;

    public InstallHistory() => this.InitializeComponent();

    private void btnOk_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.Cancel;

    private void InstallHistory_Load(object sender, EventArgs e)
    {
      DataTable dataTable = new DataTable();
      DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
      gridViewCellStyle.Format = Utility.DateFormatString + " h:mm tt";
      dataTable.Columns.Add("InstallDate", typeof (DateTime));
      this.InstallDate.DefaultCellStyle = gridViewCellStyle;
      dataTable.Columns.Add("DeltaHistoryMVersion");
      dataTable.Columns.Add("PackageName");
      dataTable.Columns.Add("PatchVer");
      dataTable.Columns.Add("Description");
      dataTable.Columns.Add("Status");
      dataTable.Columns.Add("Source");
      DeltaHistory filter = new DeltaHistory();
      try
      {
        DeltaHistoryListResponse deltaHistoryList = GuiData.AppController.ShipEngine.GetDeltaHistoryList(filter);
        if (deltaHistoryList.DeltaHistories != null)
        {
          foreach (DeltaHistory deltaHistory in deltaHistoryList.DeltaHistories)
          {
            DataRow row = dataTable.NewRow();
            row["InstallDate"] = (object) deltaHistory.RunTime;
            row["DeltaHistoryMVersion"] = (object) deltaHistory.CurrSWVer;
            row["PackageName"] = (object) deltaHistory.Name;
            row["PatchVer"] = (object) deltaHistory.PatchVer;
            row["Description"] = (object) deltaHistory.Description;
            row["Status"] = (object) deltaHistory.Status;
            row["Source"] = (object) deltaHistory.Source;
            dataTable.Rows.Add(row);
          }
        }
      }
      catch (Exception ex)
      {
      }
      dataTable.DefaultView.Sort = "InstallDate DESC";
      this.dgvInstallHistory.AutoGenerateColumns = false;
      this.dgvInstallHistory.DataSource = (object) dataTable;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (InstallHistory));
      this.dgvInstallHistory = new DataGridView();
      this.InstallDate = new DataGridViewTextBoxColumn();
      this.DeltaHistoryMVersion = new DataGridViewTextBoxColumn();
      this.PackageName = new DataGridViewTextBoxColumn();
      this.PatchVer = new DataGridViewTextBoxColumn();
      this.Description = new DataGridViewTextBoxColumn();
      this.Status = new DataGridViewTextBoxColumn();
      this.Source = new DataGridViewTextBoxColumn();
      this.btnOk = new Button();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
      ((ISupportInitialize) this.dgvInstallHistory).BeginInit();
      this.SuspendLayout();
      this.dgvInstallHistory.AllowUserToAddRows = false;
      this.dgvInstallHistory.AllowUserToDeleteRows = false;
      this.dgvInstallHistory.AllowUserToResizeRows = false;
      componentResourceManager.ApplyResources((object) this.dgvInstallHistory, "dgvInstallHistory");
      this.dgvInstallHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvInstallHistory.Columns.AddRange((DataGridViewColumn) this.InstallDate, (DataGridViewColumn) this.DeltaHistoryMVersion, (DataGridViewColumn) this.PackageName, (DataGridViewColumn) this.PatchVer, (DataGridViewColumn) this.Description, (DataGridViewColumn) this.Status, (DataGridViewColumn) this.Source);
      this.dgvInstallHistory.Name = "dgvInstallHistory";
      this.dgvInstallHistory.ReadOnly = true;
      this.dgvInstallHistory.RowHeadersVisible = false;
      this.InstallDate.DataPropertyName = "InstallDate";
      componentResourceManager.ApplyResources((object) this.InstallDate, "InstallDate");
      this.InstallDate.Name = "InstallDate";
      this.InstallDate.ReadOnly = true;
      this.DeltaHistoryMVersion.DataPropertyName = "DeltaHistoryMVersion";
      componentResourceManager.ApplyResources((object) this.DeltaHistoryMVersion, "DeltaHistoryMVersion");
      this.DeltaHistoryMVersion.Name = "DeltaHistoryMVersion";
      this.DeltaHistoryMVersion.ReadOnly = true;
      this.PackageName.DataPropertyName = "PackageName";
      componentResourceManager.ApplyResources((object) this.PackageName, "PackageName");
      this.PackageName.Name = "PackageName";
      this.PackageName.ReadOnly = true;
      this.PatchVer.DataPropertyName = "PatchVer";
      componentResourceManager.ApplyResources((object) this.PatchVer, "PatchVer");
      this.PatchVer.Name = "PatchVer";
      this.PatchVer.ReadOnly = true;
      this.Description.DataPropertyName = "Description";
      componentResourceManager.ApplyResources((object) this.Description, "Description");
      this.Description.Name = "Description";
      this.Description.ReadOnly = true;
      this.Status.DataPropertyName = "Status";
      componentResourceManager.ApplyResources((object) this.Status, "Status");
      this.Status.Name = "Status";
      this.Status.ReadOnly = true;
      this.Source.DataPropertyName = "Source";
      componentResourceManager.ApplyResources((object) this.Source, "Source");
      this.Source.Name = "Source";
      this.Source.ReadOnly = true;
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.dataGridViewTextBoxColumn1.DataPropertyName = "InstallDate";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn2.DataPropertyName = "DeltaHistoryMVersion";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn3.DataPropertyName = "PackageName";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn4.DataPropertyName = "PatchVer";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn5.DataPropertyName = "Description";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn6.DataPropertyName = "Status";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
      this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      this.dataGridViewTextBoxColumn7.DataPropertyName = "Source";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
      this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnOk;
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.dgvInstallHistory);
      this.KeyPreview = true;
      this.Name = nameof (InstallHistory);
      this.ShowIcon = false;
      this.Load += new EventHandler(this.InstallHistory_Load);
      ((ISupportInitialize) this.dgvInstallHistory).EndInit();
      this.ResumeLayout(false);
    }
  }
}
