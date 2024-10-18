// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.CustomizeUserPrompts
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class CustomizeUserPrompts : HelpFormBase
  {
    private List<CustomizeUserPrompts.UserPrompt> _userPrompts;
    private IContainer components;
    private DataGridView dgvUserPrompts;
    private Button btnOk;
    private Button btnCancel;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
    private DataGridViewTextBoxColumn colMessage;
    private DataGridViewComboBoxColumn colPrompt;

    public CustomizeUserPrompts()
    {
      this.InitializeComponent();
      this._userPrompts = new List<CustomizeUserPrompts.UserPrompt>();
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_RecipSave"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_CILocalLang"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_CILtr"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_CILocalLangLtr"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelSender"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelRecipient"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelDepartment"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelBroker"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelDimension"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelGroup"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelCommodity"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelHazMat"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelDangGood"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelIPDIOR"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelUser"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelTemplate"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelCustDbReport"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DelReference"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_CertOfOrigin"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_SuccessfulBackup"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_VagueCommodity"));
      if (!GuiData.CurrentAccount.is_ZENITH_CHANGES_INIT)
        this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_BrokerInclusiveMsg"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_SigSvcsOptChange"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_HazMatPaperStockVerification"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_NonEtdWarningMsg"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_APOFPODPOCustomsForm"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_SPReturnToChanged"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_ReturnIdenticalPkg"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_Return1421C"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_UAECommercialInvoice"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_ProrityAlertSpcSvc"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_ReturnsClearance"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_AlcoholShipmentLabel"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_SPThirdPartyBilling"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_DGHazMatWarning"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_MinBillWeightRate"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_NoRateWarning")
      {
        Prompt = "N"
      });
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_LtlFreightBrokerInclusiveWarning"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_GroundResi2Home"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_USMCADOWNLOAD"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_USMCADOWNLOADRET"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_LTLFreightUSMCADOWNLOAD"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_LtlFreightCapacityLoadWarning"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_IDDCLOSEWARNING"));
      this._userPrompts.Add(new CustomizeUserPrompts.UserPrompt("UP_TAXDISCLAIMER"));
    }

    private void CustomizeUserPrompts_Load(object sender, EventArgs e)
    {
      DataTable dataTable = new DataTable("Prompt");
      dataTable.Columns.Add("Description");
      dataTable.Columns.Add("Value");
      DataRow row1 = dataTable.NewRow();
      row1["Description"] = (object) GuiData.Languafier.Translate("UP_Prompt");
      row1["Value"] = (object) "Y";
      dataTable.Rows.Add(row1);
      DataRow row2 = dataTable.NewRow();
      row2["Description"] = (object) GuiData.Languafier.Translate("UP_DontPrompt");
      row2["Value"] = (object) "N";
      dataTable.Rows.Add(row2);
      ((DataGridViewComboBoxColumn) this.dgvUserPrompts.Columns["colPrompt"]).DataSource = (object) dataTable;
      ((DataGridViewComboBoxColumn) this.dgvUserPrompts.Columns["colPrompt"]).ValueMember = "Value";
      ((DataGridViewComboBoxColumn) this.dgvUserPrompts.Columns["colPrompt"]).DisplayMember = "Description";
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      foreach (CustomizeUserPrompts.UserPrompt userPrompt in this._userPrompts)
      {
        string str;
        if (configManager.GetProfileString("SETTINGS/USERPROMPTS", userPrompt.RegistryKey, out str))
        {
          if (str.ToLower() == "true")
            userPrompt.Prompt = "Y";
          else if (str.ToLower() == "false")
            userPrompt.Prompt = "N";
          else if (!string.IsNullOrEmpty(str))
            userPrompt.Prompt = str;
          else if (string.IsNullOrEmpty(userPrompt.Prompt))
            userPrompt.Prompt = "Y";
        }
        this.dgvUserPrompts.Rows[this.dgvUserPrompts.Rows.Add((object) GuiData.Languafier.Translate(userPrompt.RegistryKey), (object) userPrompt.Prompt)].Tag = (object) userPrompt.RegistryKey;
      }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      foreach (DataGridViewRow row in (IEnumerable) this.dgvUserPrompts.Rows)
        configManager.SetProfileValue("SETTINGS/USERPROMPTS", row.Tag.ToString(), (object) row.Cells["colPrompt"].Value.ToString());
      this.DialogResult = DialogResult.OK;
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing || this.components == null)
        return;
      this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CustomizeUserPrompts));
      this.dgvUserPrompts = new DataGridView();
      this.colMessage = new DataGridViewTextBoxColumn();
      this.colPrompt = new DataGridViewComboBoxColumn();
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewComboBoxColumn1 = new DataGridViewComboBoxColumn();
      ((ISupportInitialize) this.dgvUserPrompts).BeginInit();
      this.SuspendLayout();
      this.dgvUserPrompts.AllowUserToAddRows = false;
      this.dgvUserPrompts.AllowUserToDeleteRows = false;
      this.dgvUserPrompts.AllowUserToResizeRows = false;
      componentResourceManager.ApplyResources((object) this.dgvUserPrompts, "dgvUserPrompts");
      this.dgvUserPrompts.BackgroundColor = SystemColors.Window;
      this.dgvUserPrompts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvUserPrompts.Columns.AddRange((DataGridViewColumn) this.colMessage, (DataGridViewColumn) this.colPrompt);
      this.helpProvider1.SetHelpKeyword((Control) this.dgvUserPrompts, componentResourceManager.GetString("dgvUserPrompts.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.dgvUserPrompts, (HelpNavigator) componentResourceManager.GetObject("dgvUserPrompts.HelpNavigator"));
      this.dgvUserPrompts.MultiSelect = false;
      this.dgvUserPrompts.Name = "dgvUserPrompts";
      this.dgvUserPrompts.RowHeadersVisible = false;
      this.dgvUserPrompts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.helpProvider1.SetShowHelp((Control) this.dgvUserPrompts, (bool) componentResourceManager.GetObject("dgvUserPrompts.ShowHelp"));
      this.colMessage.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      componentResourceManager.ApplyResources((object) this.colMessage, "colMessage");
      this.colMessage.Name = "colMessage";
      this.colMessage.ReadOnly = true;
      this.colPrompt.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      componentResourceManager.ApplyResources((object) this.colPrompt, "colPrompt");
      this.colPrompt.MaxDropDownItems = 2;
      this.colPrompt.Name = "colPrompt";
      this.colPrompt.Resizable = DataGridViewTriState.False;
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.Name = "btnOk";
      this.helpProvider1.SetShowHelp((Control) this.btnOk, (bool) componentResourceManager.GetObject("btnOk.ShowHelp"));
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Name = "btnCancel";
      this.helpProvider1.SetShowHelp((Control) this.btnCancel, (bool) componentResourceManager.GetObject("btnCancel.ShowHelp"));
      this.btnCancel.UseVisualStyleBackColor = true;
      this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewComboBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
      componentResourceManager.ApplyResources((object) this.dataGridViewComboBoxColumn1, "dataGridViewComboBoxColumn1");
      this.dataGridViewComboBoxColumn1.MaxDropDownItems = 2;
      this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.dgvUserPrompts);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (CustomizeUserPrompts);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Load += new EventHandler(this.CustomizeUserPrompts_Load);
      ((ISupportInitialize) this.dgvUserPrompts).EndInit();
      this.ResumeLayout(false);
    }

    private class UserPrompt
    {
      private string _registryKey;
      private string _prompt = "Y";

      public UserPrompt(string key) => this._registryKey = key;

      public string RegistryKey
      {
        get => this._registryKey;
        set => this._registryKey = value;
      }

      public string Prompt
      {
        get => this._prompt;
        set => this._prompt = value;
      }
    }
  }
}
