// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.ContactInfromation
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class ContactInfromation : HelpFormBase
  {
    public FedEx.Gsm.Common.ConfigManager.ConfigManager cm = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
    public DataTable dt = new DataTable();
    private IContainer components;
    private FlowLayoutPanel flowLayoutPanel1;
    protected Button btnOk;
    protected Button btnAdd;
    private DataGridView dataGridView;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    protected Button btnRemoveRow;
    private DataGridViewTextBoxColumn NAME;
    private DataGridViewTextBoxColumn Office;
    private DataGridViewTextBoxColumn Mobile;
    private DataGridViewTextBoxColumn Fax;
    private DataGridViewTextBoxColumn Email;
    private DataGridViewTextBoxColumn VoiceMail;
    private DataGridViewTextBoxColumn Other;

    public ContactInfromation() => this.InitializeComponent();

    private void btnAdd_Click(object sender, EventArgs e)
    {
      this.dt.Rows.Add(this.dt.NewRow());
      this.dataGridView.CurrentCell = this.dataGridView.Rows[this.dataGridView.Rows.Count - 1].Cells[0];
    }

    private void dataGridView_SelectionChanged(object sender, EventArgs e)
    {
    }

    private void RefreshList()
    {
      for (int index = 0; this.cm.KeyExist("SETTINGS/CONTACTINFORMATION/ROW_" + index.ToString()); ++index)
      {
        DataRow row = this.dt.NewRow();
        string empty = string.Empty;
        this.cm.GetProfileString("SETTINGS/CONTACTINFORMATION/ROW_" + index.ToString(), "NAME", out empty, string.Empty);
        row["NAME"] = (object) empty;
        this.cm.GetProfileString("SETTINGS/CONTACTINFORMATION/ROW_" + index.ToString(), "OFFICE", out empty, string.Empty);
        row["OFFICE"] = (object) empty;
        this.cm.GetProfileString("SETTINGS/CONTACTINFORMATION/ROW_" + index.ToString(), "MOBILE", out empty, string.Empty);
        row["MOBILE"] = (object) empty;
        this.cm.GetProfileString("SETTINGS/CONTACTINFORMATION/ROW_" + index.ToString(), "FAX", out empty, string.Empty);
        row["FAX"] = (object) empty;
        this.cm.GetProfileString("SETTINGS/CONTACTINFORMATION/ROW_" + index.ToString(), "EMAIL", out empty, string.Empty);
        row["EMAIL"] = (object) empty;
        this.cm.GetProfileString("SETTINGS/CONTACTINFORMATION/ROW_" + index.ToString(), "VOICEMAIL", out empty, string.Empty);
        row["VOICEMAIL"] = (object) empty;
        this.cm.GetProfileString("SETTINGS/CONTACTINFORMATION/ROW_" + index.ToString(), "OTHER", out empty, string.Empty);
        row["OTHER"] = (object) empty;
        this.dt.Rows.Add(row);
      }
      this.dataGridView.AutoGenerateColumns = false;
      this.dataGridView.DataSource = (object) this.dt;
    }

    private void SaveData()
    {
      int num;
      for (num = 0; this.cm.KeyExist("SETTINGS/CONTACTINFORMATION/ROW_" + num.ToString()); ++num)
        this.cm.RemoveKey("SETTINGS/CONTACTINFORMATION/ROW_" + num.ToString());
      num = 0;
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridView.Rows)
      {
        if (!string.IsNullOrEmpty(row.Cells["NAME"].Value.ToString()))
        {
          this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_" + num.ToString(), "NAME", (object) row.Cells["NAME"].Value.ToString());
          this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_" + num.ToString(), "OFFICE", (object) row.Cells["OFFICE"].Value.ToString());
          this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_" + num.ToString(), "MOBILE", (object) row.Cells["MOBILE"].Value.ToString());
          this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_" + num.ToString(), "FAX", (object) row.Cells["FAX"].Value.ToString());
          this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_" + num.ToString(), "EMAIL", (object) row.Cells["EMAIL"].Value.ToString());
          this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_" + num.ToString(), "VOICEMAIL", (object) row.Cells["VOICEMAIL"].Value.ToString());
          this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_" + num.ToString(), "OTHER", (object) row.Cells["OTHER"].Value.ToString());
        }
        ++num;
      }
    }

    private void ContactInfromation_Load(object sender, EventArgs e)
    {
      string empty = string.Empty;
      if ((!this.cm.KeyExist("SETTINGS/CONTACTINFORMATION") || !this.cm.GetProfileString("SETTINGS/CONTACTINFORMATION/ROW_0", "NAME", out empty, string.Empty) || string.IsNullOrEmpty(empty)) && string.IsNullOrEmpty(empty))
      {
        this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_0", "NAME", (object) "Technical Support");
        this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_0", "OFFICE", (object) "1-877-339-2774");
        this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_0", "MOBILE", (object) "");
        this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_0", "FAX", (object) "");
        this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_0", "EMAIL", (object) "");
        this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_0", "VOICEMAIL", (object) "");
        this.cm.SetProfileValue("SETTINGS/CONTACTINFORMATION/ROW_0", "OTHER", (object) "");
      }
      this.dt.Columns.Add("NAME");
      this.dt.Columns.Add("OFFICE");
      this.dt.Columns.Add("MOBILE");
      this.dt.Columns.Add("FAX");
      this.dt.Columns.Add("EMAIL");
      this.dt.Columns.Add("VOICEMAIL");
      this.dt.Columns.Add("OTHER");
      this.RefreshList();
    }

    private void btnOk_Click(object sender, EventArgs e) => this.SaveData();

    private void btnRemoveRow_Click(object sender, EventArgs e)
    {
      if (this.dataGridView.SelectedRows.Count <= 0)
        return;
      this.dataGridView.Rows.Remove(this.dataGridView.SelectedRows[0]);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ContactInfromation));
      this.flowLayoutPanel1 = new FlowLayoutPanel();
      this.btnOk = new Button();
      this.btnAdd = new Button();
      this.btnRemoveRow = new Button();
      this.dataGridView = new DataGridView();
      this.NAME = new DataGridViewTextBoxColumn();
      this.Office = new DataGridViewTextBoxColumn();
      this.Mobile = new DataGridViewTextBoxColumn();
      this.Fax = new DataGridViewTextBoxColumn();
      this.Email = new DataGridViewTextBoxColumn();
      this.VoiceMail = new DataGridViewTextBoxColumn();
      this.Other = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
      this.flowLayoutPanel1.SuspendLayout();
      ((ISupportInitialize) this.dataGridView).BeginInit();
      this.SuspendLayout();
      this.flowLayoutPanel1.Controls.Add((Control) this.btnOk);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnAdd);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnRemoveRow);
      componentResourceManager.ApplyResources((object) this.flowLayoutPanel1, "flowLayoutPanel1");
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.helpProvider1.SetShowHelp((Control) this.flowLayoutPanel1, (bool) componentResourceManager.GetObject("flowLayoutPanel1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.helpProvider1.SetShowHelp((Control) this.btnOk, (bool) componentResourceManager.GetObject("btnOk.ShowHelp"));
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
      this.helpProvider1.SetHelpKeyword((Control) this.btnAdd, componentResourceManager.GetString("btnAdd.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnAdd, (HelpNavigator) componentResourceManager.GetObject("btnAdd.HelpNavigator"));
      this.btnAdd.Name = "btnAdd";
      this.helpProvider1.SetShowHelp((Control) this.btnAdd, (bool) componentResourceManager.GetObject("btnAdd.ShowHelp"));
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
      componentResourceManager.ApplyResources((object) this.btnRemoveRow, "btnRemoveRow");
      this.helpProvider1.SetHelpKeyword((Control) this.btnRemoveRow, componentResourceManager.GetString("btnRemoveRow.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnRemoveRow, (HelpNavigator) componentResourceManager.GetObject("btnRemoveRow.HelpNavigator"));
      this.btnRemoveRow.Name = "btnRemoveRow";
      this.helpProvider1.SetShowHelp((Control) this.btnRemoveRow, (bool) componentResourceManager.GetObject("btnRemoveRow.ShowHelp"));
      this.btnRemoveRow.UseVisualStyleBackColor = true;
      this.btnRemoveRow.Click += new EventHandler(this.btnRemoveRow_Click);
      this.dataGridView.AllowUserToAddRows = false;
      this.dataGridView.AllowUserToDeleteRows = false;
      this.dataGridView.AllowUserToResizeRows = false;
      componentResourceManager.ApplyResources((object) this.dataGridView, "dataGridView");
      this.dataGridView.BackgroundColor = SystemColors.Window;
      this.dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView.Columns.AddRange((DataGridViewColumn) this.NAME, (DataGridViewColumn) this.Office, (DataGridViewColumn) this.Mobile, (DataGridViewColumn) this.Fax, (DataGridViewColumn) this.Email, (DataGridViewColumn) this.VoiceMail, (DataGridViewColumn) this.Other);
      this.dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
      this.helpProvider1.SetHelpKeyword((Control) this.dataGridView, componentResourceManager.GetString("dataGridView.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.dataGridView, (HelpNavigator) componentResourceManager.GetObject("dataGridView.HelpNavigator"));
      this.dataGridView.MultiSelect = false;
      this.dataGridView.Name = "dataGridView";
      this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.helpProvider1.SetShowHelp((Control) this.dataGridView, (bool) componentResourceManager.GetObject("dataGridView.ShowHelp"));
      this.NAME.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.NAME.DataPropertyName = "NAME";
      componentResourceManager.ApplyResources((object) this.NAME, "NAME");
      this.NAME.Name = "NAME";
      this.NAME.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Office.DataPropertyName = "OFFICE";
      componentResourceManager.ApplyResources((object) this.Office, "Office");
      this.Office.Name = "Office";
      this.Office.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Mobile.DataPropertyName = "MOBILE";
      componentResourceManager.ApplyResources((object) this.Mobile, "Mobile");
      this.Mobile.Name = "Mobile";
      this.Mobile.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Fax.DataPropertyName = "FAX";
      componentResourceManager.ApplyResources((object) this.Fax, "Fax");
      this.Fax.Name = "Fax";
      this.Fax.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Email.DataPropertyName = "EMAIL";
      componentResourceManager.ApplyResources((object) this.Email, "Email");
      this.Email.Name = "Email";
      this.Email.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.VoiceMail.DataPropertyName = "VOICEMAIL";
      componentResourceManager.ApplyResources((object) this.VoiceMail, "VoiceMail");
      this.VoiceMail.Name = "VoiceMail";
      this.VoiceMail.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Other.DataPropertyName = "OTHER";
      componentResourceManager.ApplyResources((object) this.Other, "Other");
      this.Other.Name = "Other";
      this.Other.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn1.DataPropertyName = "NAME";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.dataGridViewTextBoxColumn2.DataPropertyName = "OFFICE";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn3.DataPropertyName = "MOBILE";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn4.DataPropertyName = "FAX";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn5.DataPropertyName = "EMAIL";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn6.DataPropertyName = "VOICEMAIL";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
      this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      this.dataGridViewTextBoxColumn7.DataPropertyName = "OTHER";
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
      this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
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
      this.Name = nameof (ContactInfromation);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Load += new EventHandler(this.ContactInfromation_Load);
      this.flowLayoutPanel1.ResumeLayout(false);
      ((ISupportInitialize) this.dataGridView).EndInit();
      this.ResumeLayout(false);
    }
  }
}
