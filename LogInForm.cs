// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.LogInForm
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Plugins;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.DataAccess;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class LogInForm : UserControlHelpEx, IFedExGsmGuiTabPlugin
  {
    private ToolStripMenuItem _tabItem = new ToolStripMenuItem();
    private const string SUPER_USER_ID = "_SUPERUSER_";
    private const string SUPER_USER_NAME = "FedEx";
    private const string SUPER_USER_PASSWORD = "Cafe";
    private IContainer components;
    private ColorGroupBox gbxLogin;
    private Button btnLogin;
    private TextBox edtPassword;
    private ComboBox cboSystem;
    private ComboBox cboUserId;
    private Label lblSystem;
    private Label lblPassword;
    private Label lblUserId;
    private Button btnCancel;

    public event TopicDelegate UserLoggedIn;

    public ToolStripItem TabItem => (ToolStripItem) this._tabItem;

    public LogInForm()
    {
      this.InitializeComponent();
      this._tabItem.Text = GuiData.Languafier.Translate("Login");
      this._tabItem.Padding = new Padding(5, 0, 5, 0);
      this._tabItem.Name = "toolStripButtonLogIn";
      this._tabItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.SetupEvents();
      this.PopulateCombos();
    }

    public void OnUserListChanged(object sender, EventArgs e) => this.RefreshUserList();

    public void SetSystemAccount(string system, string account)
    {
      int num = this.cboSystem.FindString(system);
      if (num == -1)
        return;
      this.cboSystem.SelectedIndex = num;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (msg.WParam.ToInt32() == 13)
        this.btnLogin.PerformClick();
      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void SetupEvents()
    {
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.UserListChanged, (object) this, "OnUserListChanged");
      GuiData.EventBroker.AddSubscriber(EventBroker.Events.NewSystemAccount, (object) this, "OnNewSystemAccount");
      GuiData.EventBroker.AddPublisher("LoginCancelButtonPressed", (object) this.btnCancel, "Click");
    }

    private void RefreshUserList()
    {
      DataTable output = (DataTable) null;
      if (GuiData.AppController.ShipEngine.GetDataList(GsmDataAccess.ListSpecification.User_List, out output, new Error()) == 1)
      {
        DataRow row = output.NewRow();
        row["Code"] = (object) "_SUPERUSER_";
        row["Description"] = (object) "FedEx";
        output.Rows.Add(row);
      }
      this.cboUserId.DataSource = (object) output;
    }

    private void RefreshAccountList()
    {
      this.cboSystem.Enabled = false;
      Error error = new Error();
      DataTable output;
      if (GuiData.AppController.ShipEngine.GetDataList((object) null, GsmDataAccess.ListSpecification.AccountList, out output, error) != 1 || output == null)
      {
        if (output == null)
          output = new DataTable();
        output.Columns.Add(new DataColumn("Meter"));
        output.Columns.Add(new DataColumn("Account"));
        output.Columns.Add(new DataColumn("Description"));
      }
      output.Columns.Add(new DataColumn("MeterDescription", typeof (string), "Meter + ' - ' + Description"));
      Utility.SetDisplayAndValue(this.cboSystem, output, "MeterDescription", "Meter", false);
      if (this.cboSystem.Items.Count > 0)
        this.cboSystem.SelectedIndex = 0;
      if (this.cboSystem.Items.Count <= 1)
        return;
      this.cboSystem.Enabled = true;
    }

    private void PopulateCombos()
    {
      this.RefreshUserList();
      this.RefreshAccountList();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
      User currUser;
      if (this.ValidPassword(this.cboUserId.SelectedValue as string, out currUser))
      {
        GuiData.CurrentUser = currUser;
        if (this.UserLoggedIn != null && this.cboSystem.SelectedItem is DataRowView selectedItem)
        {
          Account output;
          if (GuiData.AppController.ShipEngine.Retrieve<Account>(new Account()
          {
            MeterNumber = selectedItem["Meter"].ToString(),
            AccountNumber = selectedItem["Account"].ToString()
          }, out output, out Error _) == 1)
            this.UserLoggedIn((object) this, (EventArgs) new AccountEventArgs()
            {
              NewAccount = output
            });
        }
        this.edtPassword.Text = string.Empty;
      }
      else
      {
        int num = (int) MessageBox.Show(GuiData.Languafier.Translate("InvalidPassword"));
        this.edtPassword.Focus();
        this.edtPassword.SelectAll();
      }
    }

    private bool ValidPassword(string user, out User currUser)
    {
      currUser = (User) null;
      if (user == "_SUPERUSER_")
      {
        if (!"Cafe".Equals(this.edtPassword.Text, StringComparison.InvariantCultureIgnoreCase))
          return false;
        currUser = new User();
        currUser.UserId = "_SUPERUSER_";
        currUser.Name = "FedEx";
        return true;
      }
      Error error;
      if (GuiData.AppController.ShipEngine.Retrieve<User>(new User()
      {
        UserId = this.cboUserId.SelectedValue as string
      }, out currUser, out error) == 1)
      {
        if (string.Equals(this.edtPassword.Text, currUser.Password, StringComparison.CurrentCultureIgnoreCase))
          return true;
        currUser = (User) null;
        return false;
      }
      currUser = (User) null;
      Utility.DisplayError(error);
      return false;
    }

    private void LogInForm_Load(object sender, EventArgs e)
    {
      GuiData.EventBroker.GetTopic(EventBroker.Events.UserLoggedIn).AddPublisher((object) this, "UserLoggedIn");
    }

    public void OnNewSystemAccount(object sender, AccountEventArgs args)
    {
      this.RefreshAccountList();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LogInForm));
      this.gbxLogin = new ColorGroupBox();
      this.btnCancel = new Button();
      this.btnLogin = new Button();
      this.edtPassword = new TextBox();
      this.cboSystem = new ComboBox();
      this.cboUserId = new ComboBox();
      this.lblSystem = new Label();
      this.lblPassword = new Label();
      this.lblUserId = new Label();
      this.gbxLogin.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) this.gbxLogin, "gbxLogin");
      this.gbxLogin.BorderThickness = 1f;
      this.gbxLogin.Controls.Add((Control) this.btnCancel);
      this.gbxLogin.Controls.Add((Control) this.btnLogin);
      this.gbxLogin.Controls.Add((Control) this.edtPassword);
      this.gbxLogin.Controls.Add((Control) this.cboSystem);
      this.gbxLogin.Controls.Add((Control) this.cboUserId);
      this.gbxLogin.Controls.Add((Control) this.lblSystem);
      this.gbxLogin.Controls.Add((Control) this.lblPassword);
      this.gbxLogin.Controls.Add((Control) this.lblUserId);
      this.gbxLogin.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxLogin.Name = "gbxLogin";
      this.gbxLogin.RoundCorners = 5;
      this.gbxLogin.TabStop = false;
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnLogin, "btnLogin");
      this.btnLogin.Name = "btnLogin";
      this.btnLogin.UseVisualStyleBackColor = true;
      this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
      this.helpProvider1.SetHelpKeyword((Control) this.edtPassword, componentResourceManager.GetString("edtPassword.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.edtPassword, (HelpNavigator) componentResourceManager.GetObject("edtPassword.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.edtPassword, "edtPassword");
      this.edtPassword.Name = "edtPassword";
      this.helpProvider1.SetShowHelp((Control) this.edtPassword, (bool) componentResourceManager.GetObject("edtPassword.ShowHelp"));
      this.edtPassword.UseSystemPasswordChar = true;
      this.cboSystem.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboSystem.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboSystem, componentResourceManager.GetString("cboSystem.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboSystem, (HelpNavigator) componentResourceManager.GetObject("cboSystem.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.cboSystem, "cboSystem");
      this.cboSystem.Name = "cboSystem";
      this.helpProvider1.SetShowHelp((Control) this.cboSystem, (bool) componentResourceManager.GetObject("cboSystem.ShowHelp"));
      this.cboUserId.DisplayMember = "Description";
      this.cboUserId.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboUserId.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboUserId, componentResourceManager.GetString("cboUserId.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboUserId, (HelpNavigator) componentResourceManager.GetObject("cboUserId.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.cboUserId, "cboUserId");
      this.cboUserId.Name = "cboUserId";
      this.helpProvider1.SetShowHelp((Control) this.cboUserId, (bool) componentResourceManager.GetObject("cboUserId.ShowHelp"));
      this.cboUserId.ValueMember = "Code";
      componentResourceManager.ApplyResources((object) this.lblSystem, "lblSystem");
      this.lblSystem.Name = "lblSystem";
      componentResourceManager.ApplyResources((object) this.lblPassword, "lblPassword");
      this.lblPassword.Name = "lblPassword";
      componentResourceManager.ApplyResources((object) this.lblUserId, "lblUserId");
      this.lblUserId.Name = "lblUserId";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.gbxLogin);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (LogInForm);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.LogInForm_Load);
      this.gbxLogin.ResumeLayout(false);
      this.gbxLogin.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
