// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.ChooseSystemAccount
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.DataAccess;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class ChooseSystemAccount : HelpFormBase
  {
    private IContainer components;
    private Button btnAdd;
    private Button btnAddByDup;
    private Button btnViewEdit;
    private Button btnClose;
    private DataGridView dgvSystemAccount;
    private FlowLayoutPanel flowLayoutPanel1;
    private Button btnSetAsCurrent;
    private Button btnDelete;
    private DataGridViewTextBoxColumn colSystem;
    private DataGridViewTextBoxColumn colAccount;
    private DataGridViewTextBoxColumn colDescription;
    private DataGridViewCheckBoxColumn colMasterMeterInd;

    public event TopicDelegate NewSystemAccount;

    public event TopicDelegate CurrentAccountChanged;

    public event TopicDelegate SwitchMeter;

    public ChooseSystemAccount()
    {
      this.InitializeComponent();
      this.RefreshSystemAccountList();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      if (disposing)
        this.RemoveEvents();
      base.Dispose(disposing);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.BringToFront();
      this.Focus();
    }

    private void SetupEvents()
    {
      GuiData.EventBroker.AddPublisher(EventBroker.Events.NewSystemAccount, (object) this, "NewSystemAccount");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.NewSystemAccount, (object) this, "OnNewSystemAccount");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.SwitchMeter, (object) this, "SwitchMeter");
      GuiData.EventBroker.AddPublisher(EventBroker.Events.CurrentAccountChanged, (object) this, "CurrentAccountChanged");
    }

    private void RemoveEvents()
    {
      try
      {
        GuiData.EventBroker.RemovePublisher(EventBroker.Events.SwitchMeter, (object) this, "SwitchMeter");
        GuiData.EventBroker.RemovePublisher(EventBroker.Events.NewSystemAccount, (object) this, "NewSystemAccount");
        GuiData.EventBroker.RemoveSubscriber(EventBroker.Events.NewSystemAccount, (object) this, "OnNewSystemAccount");
        GuiData.EventBroker.RemovePublisher(EventBroker.Events.CurrentAccountChanged, (object) this, "CurrentAccountChanged");
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Verbose, FxLogger.AppCode_GUI, "ChooseSystemAccount.RemoveEvents()", ex.ToString());
      }
    }

    public void OnNewSystemAccount(object sender, EventArgs e) => this.RefreshSystemAccountList();

    private void RefreshSystemAccountList()
    {
      DataTable output = (DataTable) null;
      Error error = new Error();
      if (GuiData.AppController.ShipEngine.GetDataList((object) null, GsmDataAccess.ListSpecification.AccountList, out output, error) != 1)
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "ChooseSystemAccount.RefreshSystemAccountList()", "Error retrieving accounts " + error?.ToString());
      this.dgvSystemAccount.AutoGenerateColumns = false;
      this.dgvSystemAccount.DataSource = (object) output;
    }

    private void btnModify_Click(object sender, EventArgs e)
    {
      Account accountForCurrentRow = this.GetAccountForCurrentRow();
      using (SystemSettings systemSettings = new SystemSettings(accountForCurrentRow, Utility.FormOperation.ViewEdit))
      {
        if (systemSettings.ShowDialog((IWin32Window) this) == DialogResult.OK)
        {
          if (GuiData.CurrentAccount != null)
          {
            if (accountForCurrentRow.MeterNumber == GuiData.CurrentAccount.MeterNumber)
            {
              if (accountForCurrentRow.AccountNumber == GuiData.CurrentAccount.AccountNumber)
              {
                GuiData.AutoTab = systemSettings.Settings.RegistrySettings["AutoTab"].Value.ToString().ToUpper() == "Y";
                AccountEventArgs args = new AccountEventArgs();
                args.OldAccount = GuiData.CurrentAccount;
                args.NewAccount = systemSettings.Settings.AccountObject;
                GuiData.CurrentAccount = systemSettings.Settings.AccountObject;
                if (this.CurrentAccountChanged != null)
                  this.CurrentAccountChanged((object) this, (EventArgs) args);
              }
            }
          }
        }
      }
      this.DialogResult = DialogResult.OK;
    }

    private void dgvSystemAccount_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex <= -1)
        return;
      this.btnModify_Click(sender, (EventArgs) e);
    }

    private void dgvSystemAccount_SelectionChanged(object sender, EventArgs e)
    {
      this.EnableButtons();
    }

    private void EnableButtons()
    {
      int count = this.dgvSystemAccount.SelectedRows.Count;
      this.btnAddByDup.Enabled = count == 1;
      this.btnViewEdit.Enabled = count == 1;
      this.btnSetAsCurrent.Enabled = count == 1;
      this.btnDelete.Enabled = count == 1 && !GuiData.ConfigManager.IsNetworkClient && this.dgvSystemAccount.CurrentRow != null && !"1".Equals(this.dgvSystemAccount.CurrentRow.Cells["colMasterMeterInd"].Value.ToString(), StringComparison.InvariantCultureIgnoreCase);
      this.btnClose.Visible = this.dgvSystemAccount.Rows.Count > 0;
    }

    private void ChooseSystemAccount_Load(object sender, EventArgs e)
    {
      this.SetupEvents();
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      string str1;
      configManager.GetProfileString("SETTINGS", "METER", out str1);
      string str2;
      configManager.GetProfileString("SETTINGS", "ACCOUNT", out str2);
      bool flag = false;
      foreach (DataGridViewRow row in (IEnumerable) this.dgvSystemAccount.Rows)
      {
        if (row.Cells["colSystem"].Value.ToString() == str1 && row.Cells["colAccount"].Value.ToString() == str2)
        {
          flag = true;
          Utility.SetDataGridPosition(this.dgvSystemAccount, row.Index);
          break;
        }
      }
      this.ShowInTaskbar = string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2) && this.dgvSystemAccount.Rows.Count == 0;
      if (this.dgvSystemAccount.Rows.Count > 0 && !flag)
        this.btnSetAsCurrent.Visible = true;
      this.EnableButtons();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      using (SystemSettings systemSettings = new SystemSettings((Account) null, this.dgvSystemAccount.Rows.Count > 0 ? Utility.FormOperation.Add : Utility.FormOperation.AddFirst))
      {
        if (systemSettings.ShowDialog((IWin32Window) this) == DialogResult.OK && (systemSettings.Operation.ToString().StartsWith("Add") || systemSettings.Operation.ToString().StartsWith("Restore")))
        {
          FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
          configManager.SetProfileValue("SETTINGS", "METER", (object) systemSettings.Settings.AccountObject.MeterNumber);
          configManager.SetProfileValue("SETTINGS", "ACCOUNT", (object) systemSettings.Settings.AccountObject.AccountNumber);
          if (this.NewSystemAccount != null)
            this.NewSystemAccount((object) this, (EventArgs) new AccountEventArgs()
            {
              OldAccount = (Account) null,
              NewAccount = new Account(systemSettings.Settings.AccountObject)
            });
          this.DialogResult = DialogResult.OK;
          if (!systemSettings.Settings.AccountObject.IsMaster && systemSettings.Operation == Utility.FormOperation.RestoreClient && MessageBox.Show((IWin32Window) this, GuiData.Languafier.TranslateMessage(7102), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes && this.SwitchMeter != null)
          {
            configManager.SetProfileValue("SETTINGS/N" + systemSettings.Settings.AccountObject.MeterNumber + "-" + systemSettings.Settings.AccountObject.AccountNumber, "DOWNLOADSPENDING", (object) true);
            this.SwitchMeter((object) this, (EventArgs) new AccountEventArgs()
            {
              OldAccount = GuiData.CurrentAccount,
              NewAccount = new Account(systemSettings.Settings.AccountObject)
            });
          }
        }
        this.RefreshSystemAccountList();
      }
    }

    private void btnAddByDup_Click(object sender, EventArgs e)
    {
      using (SystemSettings systemSettings = new SystemSettings(this.GetAccountForCurrentRow(), Utility.FormOperation.AddByDup))
      {
        if (DialogResult.OK != systemSettings.ShowDialog((IWin32Window) this))
          return;
        this.RefreshSystemAccountList();
      }
    }

    private void btnClose_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

    private Account GetAccountForCurrentRow()
    {
      DataRow dataRow = Utility.CurrentRow(this.dgvSystemAccount);
      return new Account()
      {
        MeterNumber = dataRow["Meter"].ToString(),
        AccountNumber = dataRow["Account"].ToString(),
        Description = dataRow["Description"].ToString()
      };
    }

    private void btnSetAsCurrent_Click(object sender, EventArgs e)
    {
      DataRow dataRow = Utility.CurrentRow(this.dgvSystemAccount);
      string str1 = dataRow["Meter"].ToString();
      string str2 = dataRow["Account"].ToString();
      FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
      configManager.SetProfileValue("SETTINGS", "METER", (object) str1);
      configManager.SetProfileValue("SETTINGS", "ACCOUNT", (object) str2);
      this.DialogResult = DialogResult.OK;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      Account output;
      if (GuiData.AppController.ShipEngine.Retrieve<Account>(this.GetAccountForCurrentRow(), out output, out Error _) == 1)
      {
        using (AdminLogin adminLogin = new AdminLogin(AdminLogin.PasswordType.MeterRemoval))
        {
          if (adminLogin.ShowDialog((IWin32Window) this) == DialogResult.OK)
          {
            if (MessageBox.Show((IWin32Window) this, GuiData.Languafier.Translate("RemoveMeterConfirmPrompt"), string.Empty, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
              ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.DeleteAccountMeter(output);
              if (serviceResponse.HasError)
                Utility.DisplayError(serviceResponse.Error);
            }
          }
        }
      }
      this.DialogResult = DialogResult.OK;
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChooseSystemAccount));
      this.btnAdd = new Button();
      this.btnAddByDup = new Button();
      this.btnViewEdit = new Button();
      this.btnClose = new Button();
      this.dgvSystemAccount = new DataGridView();
      this.colSystem = new DataGridViewTextBoxColumn();
      this.colAccount = new DataGridViewTextBoxColumn();
      this.colDescription = new DataGridViewTextBoxColumn();
      this.colMasterMeterInd = new DataGridViewCheckBoxColumn();
      this.flowLayoutPanel1 = new FlowLayoutPanel();
      this.btnDelete = new Button();
      this.btnSetAsCurrent = new Button();
      ((ISupportInitialize) this.dgvSystemAccount).BeginInit();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
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
      this.btnViewEdit.Click += new EventHandler(this.btnModify_Click);
      componentResourceManager.ApplyResources((object) this.btnClose, "btnClose");
      this.btnClose.DialogResult = DialogResult.OK;
      this.btnClose.Name = "btnClose";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.dgvSystemAccount.AllowUserToAddRows = false;
      this.dgvSystemAccount.AllowUserToDeleteRows = false;
      this.dgvSystemAccount.AllowUserToResizeRows = false;
      componentResourceManager.ApplyResources((object) this.dgvSystemAccount, "dgvSystemAccount");
      this.dgvSystemAccount.BackgroundColor = SystemColors.Window;
      this.dgvSystemAccount.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvSystemAccount.Columns.AddRange((DataGridViewColumn) this.colSystem, (DataGridViewColumn) this.colAccount, (DataGridViewColumn) this.colDescription, (DataGridViewColumn) this.colMasterMeterInd);
      this.helpProvider1.SetHelpKeyword((Control) this.dgvSystemAccount, componentResourceManager.GetString("dgvSystemAccount.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.dgvSystemAccount, (HelpNavigator) componentResourceManager.GetObject("dgvSystemAccount.HelpNavigator"));
      this.dgvSystemAccount.MultiSelect = false;
      this.dgvSystemAccount.Name = "dgvSystemAccount";
      this.dgvSystemAccount.ReadOnly = true;
      this.dgvSystemAccount.RowHeadersVisible = false;
      this.dgvSystemAccount.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvSystemAccount.ShowCellErrors = false;
      this.dgvSystemAccount.ShowCellToolTips = false;
      this.dgvSystemAccount.ShowEditingIcon = false;
      this.helpProvider1.SetShowHelp((Control) this.dgvSystemAccount, (bool) componentResourceManager.GetObject("dgvSystemAccount.ShowHelp"));
      this.dgvSystemAccount.ShowRowErrors = false;
      this.dgvSystemAccount.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvSystemAccount_CellDoubleClick);
      this.dgvSystemAccount.CurrentCellChanged += new EventHandler(this.dgvSystemAccount_SelectionChanged);
      this.dgvSystemAccount.SelectionChanged += new EventHandler(this.dgvSystemAccount_SelectionChanged);
      this.colSystem.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.colSystem.DataPropertyName = "Meter";
      componentResourceManager.ApplyResources((object) this.colSystem, "colSystem");
      this.colSystem.Name = "colSystem";
      this.colSystem.ReadOnly = true;
      this.colAccount.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.colAccount.DataPropertyName = "Account";
      componentResourceManager.ApplyResources((object) this.colAccount, "colAccount");
      this.colAccount.Name = "colAccount";
      this.colAccount.ReadOnly = true;
      this.colDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.colDescription.DataPropertyName = "Description";
      componentResourceManager.ApplyResources((object) this.colDescription, "colDescription");
      this.colDescription.Name = "colDescription";
      this.colDescription.ReadOnly = true;
      this.colMasterMeterInd.DataPropertyName = "MasterMeterInd";
      this.colMasterMeterInd.FalseValue = (object) "0";
      componentResourceManager.ApplyResources((object) this.colMasterMeterInd, "colMasterMeterInd");
      this.colMasterMeterInd.Name = "colMasterMeterInd";
      this.colMasterMeterInd.ReadOnly = true;
      this.colMasterMeterInd.TrueValue = (object) "1";
      componentResourceManager.ApplyResources((object) this.flowLayoutPanel1, "flowLayoutPanel1");
      this.flowLayoutPanel1.Controls.Add((Control) this.btnAdd);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnAddByDup);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnDelete);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnViewEdit);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnSetAsCurrent);
      this.flowLayoutPanel1.Controls.Add((Control) this.btnClose);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.helpProvider1.SetShowHelp((Control) this.flowLayoutPanel1, (bool) componentResourceManager.GetObject("flowLayoutPanel1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
      componentResourceManager.ApplyResources((object) this.btnSetAsCurrent, "btnSetAsCurrent");
      this.btnSetAsCurrent.DialogResult = DialogResult.OK;
      this.btnSetAsCurrent.Name = "btnSetAsCurrent";
      this.helpProvider1.SetShowHelp((Control) this.btnSetAsCurrent, (bool) componentResourceManager.GetObject("btnSetAsCurrent.ShowHelp"));
      this.btnSetAsCurrent.UseVisualStyleBackColor = true;
      this.btnSetAsCurrent.Click += new EventHandler(this.btnSetAsCurrent_Click);
      this.AcceptButton = (IButtonControl) this.btnViewEdit;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnClose;
      this.Controls.Add((Control) this.flowLayoutPanel1);
      this.Controls.Add((Control) this.dgvSystemAccount);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ChooseSystemAccount);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Load += new EventHandler(this.ChooseSystemAccount_Load);
      ((ISupportInitialize) this.dgvSystemAccount).EndInit();
      this.flowLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
