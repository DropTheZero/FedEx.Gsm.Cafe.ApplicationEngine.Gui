// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.NetworkClientAdmin
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.Entities.RemotingSupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class NetworkClientAdmin : HelpFormBase
  {
    private IContainer components;
    private ColorGroupBox gbxNetworkClientConnections;
    private ListView listConnections;
    private Button btnRefresh;
    private Button btnShutdown;
    private Button btnSendMessage;
    private Button btnPing;
    private Button btnOK;

    private void UpdateConnectionList()
    {
      this.Cursor = Cursors.WaitCursor;
      List<NetworkClientEntry> clientList = (List<NetworkClientEntry>) null;
      List<NetworkClientEntry> unresponsiveList = (List<NetworkClientEntry>) null;
      GuiData.AppController.ShipEngine.GetConnectedClientList(out clientList, out unresponsiveList);
      this.listConnections.Clear();
      this.listConnections.Columns.Add("Client ID", 60);
      this.listConnections.Columns.Add("DNS Name", 130);
      this.listConnections.Columns.Add("IP Address", 100);
      this.listConnections.Columns.Add("Meter", 90);
      this.listConnections.Columns.Add("Account", 90);
      this.listConnections.Columns.Add("Responsive", 100);
      if (clientList != null)
      {
        foreach (NetworkClientEntry s in clientList)
          this.AddClientToListConnections(s, true);
      }
      if (unresponsiveList != null)
      {
        foreach (NetworkClientEntry s in unresponsiveList)
          this.AddClientToListConnections(s, false);
      }
      this.Cursor = Cursors.Default;
      if (this.listConnections.Items.Count <= 0)
        return;
      this.listConnections.Items[0].Selected = true;
    }

    private void AddClientToListConnections(NetworkClientEntry s, bool responsive)
    {
      IPHostEntry ipHostEntry = (IPHostEntry) null;
      try
      {
        ipHostEntry = Dns.GetHostEntry(IPAddress.Parse(s.IPAddress));
      }
      catch (SocketException ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_GUI, this.ToString(), "Socket Exception in RefreshClientList for '" + s.IPAddress + "' error=" + ex.ErrorCode.ToString() + " Message = " + ex.Message);
      }
      ListViewItem owner = new ListViewItem(s.NetworkClientID.ToString());
      string text = ipHostEntry != null ? ipHostEntry.HostName : string.Empty;
      ListViewItem.ListViewSubItem listViewSubItem1 = new ListViewItem.ListViewSubItem(owner, text);
      owner.SubItems.Add(listViewSubItem1);
      ListViewItem.ListViewSubItem listViewSubItem2 = new ListViewItem.ListViewSubItem(owner, s.IPAddress);
      owner.SubItems.Add(listViewSubItem2);
      ListViewItem.ListViewSubItem listViewSubItem3 = new ListViewItem.ListViewSubItem(owner, s.MeterNumber);
      owner.SubItems.Add(listViewSubItem3);
      ListViewItem.ListViewSubItem listViewSubItem4 = new ListViewItem.ListViewSubItem(owner, s.AccountNumber);
      owner.SubItems.Add(listViewSubItem4);
      ListViewItem.ListViewSubItem listViewSubItem5 = new ListViewItem.ListViewSubItem(owner, responsive ? "Yes" : "No");
      owner.SubItems.Add(listViewSubItem5);
      this.listConnections.Items.Add(owner);
    }

    public NetworkClientAdmin() => this.InitializeComponent();

    private void SystemSettingsNetworkClient_Load(object sender, EventArgs e)
    {
      this.UpdateConnectionList();
    }

    private void btnRefresh_Click(object sender, EventArgs e) => this.UpdateConnectionList();

    private void btnPing_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      if (this.listConnections.SelectedItems.Count > 0)
      {
        string text1 = this.listConnections.SelectedItems[0].SubItems[0].Text;
        string text2 = this.listConnections.SelectedItems[0].SubItems[2].Text;
        if (!string.IsNullOrEmpty(text1))
        {
          int result = 0;
          int.TryParse(text1, out result);
          if (!GuiData.AppController.ShipEngine.PingNetworkClient(result).HasError)
          {
            this.listConnections.SelectedItems[0].SubItems[5].Text = "Yes";
            int num = (int) MessageBox.Show("Network Client ID [" + text1 + "] at IP Address [" + text2 + "] is Responsive.");
          }
          else
          {
            this.listConnections.SelectedItems[0].SubItems[5].Text = "No";
            int num = (int) MessageBox.Show("Network Client ID [" + text1 + "] at IP Address [" + text2 + "] is Unresponsive.");
          }
        }
      }
      this.Cursor = Cursors.Default;
    }

    private void btnSendMessage_Click(object sender, EventArgs e)
    {
      if (this.listConnections.SelectedItems.Count <= 0)
        return;
      string text = this.listConnections.SelectedItems[0].SubItems[2].Text;
      if (string.IsNullOrEmpty(text))
        return;
      int num = (int) new SendandRecieveMessageToClientDlg(text).ShowDialog();
    }

    private void btnShutdown_Click(object sender, EventArgs e)
    {
      int result = 0;
      if (this.listConnections.SelectedItems.Count <= 0)
        return;
      int.TryParse(this.listConnections.SelectedItems[0].SubItems[0].Text, out result);
      if (result == 0)
        return;
      GuiData.AppController.ShipEngine.ShutdownNetworkClient(result);
      this.UpdateConnectionList();
    }

    private void btnOK_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

    private void EnableButtons(bool enabled)
    {
      this.btnPing.Enabled = enabled;
      this.btnRefresh.Enabled = enabled;
      this.btnSendMessage.Enabled = enabled;
      this.btnShutdown.Enabled = enabled;
    }

    private void listConnections_ItemSelectionChanged(
      object sender,
      ListViewItemSelectionChangedEventArgs e)
    {
      if (this.listConnections.SelectedIndices.Count > 0)
        this.EnableButtons(true);
      else
        this.EnableButtons(false);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NetworkClientAdmin));
      this.btnOK = new Button();
      this.gbxNetworkClientConnections = new ColorGroupBox();
      this.btnShutdown = new Button();
      this.btnSendMessage = new Button();
      this.btnPing = new Button();
      this.listConnections = new ListView();
      this.btnRefresh = new Button();
      this.gbxNetworkClientConnections.SuspendLayout();
      this.SuspendLayout();
      this.btnOK.DialogResult = DialogResult.Cancel;
      componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
      this.btnOK.Name = "btnOK";
      this.helpProvider1.SetShowHelp((Control) this.btnOK, (bool) componentResourceManager.GetObject("btnOK.ShowHelp"));
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.gbxNetworkClientConnections.BorderThickness = 1f;
      this.gbxNetworkClientConnections.Controls.Add((Control) this.btnShutdown);
      this.gbxNetworkClientConnections.Controls.Add((Control) this.btnSendMessage);
      this.gbxNetworkClientConnections.Controls.Add((Control) this.btnPing);
      this.gbxNetworkClientConnections.Controls.Add((Control) this.listConnections);
      this.gbxNetworkClientConnections.Controls.Add((Control) this.btnRefresh);
      componentResourceManager.ApplyResources((object) this.gbxNetworkClientConnections, "gbxNetworkClientConnections");
      this.gbxNetworkClientConnections.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxNetworkClientConnections.Name = "gbxNetworkClientConnections";
      this.gbxNetworkClientConnections.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxNetworkClientConnections, (bool) componentResourceManager.GetObject("gbxNetworkClientConnections.ShowHelp"));
      this.gbxNetworkClientConnections.TabStop = false;
      componentResourceManager.ApplyResources((object) this.btnShutdown, "btnShutdown");
      this.btnShutdown.Name = "btnShutdown";
      this.helpProvider1.SetShowHelp((Control) this.btnShutdown, (bool) componentResourceManager.GetObject("btnShutdown.ShowHelp"));
      this.btnShutdown.UseVisualStyleBackColor = true;
      this.btnShutdown.Click += new EventHandler(this.btnShutdown_Click);
      componentResourceManager.ApplyResources((object) this.btnSendMessage, "btnSendMessage");
      this.btnSendMessage.Name = "btnSendMessage";
      this.helpProvider1.SetShowHelp((Control) this.btnSendMessage, (bool) componentResourceManager.GetObject("btnSendMessage.ShowHelp"));
      this.btnSendMessage.UseVisualStyleBackColor = true;
      this.btnSendMessage.Click += new EventHandler(this.btnSendMessage_Click);
      componentResourceManager.ApplyResources((object) this.btnPing, "btnPing");
      this.btnPing.Name = "btnPing";
      this.helpProvider1.SetShowHelp((Control) this.btnPing, (bool) componentResourceManager.GetObject("btnPing.ShowHelp"));
      this.btnPing.UseVisualStyleBackColor = true;
      this.btnPing.Click += new EventHandler(this.btnPing_Click);
      this.listConnections.CausesValidation = false;
      this.listConnections.FullRowSelect = true;
      this.listConnections.GridLines = true;
      this.listConnections.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      this.listConnections.HideSelection = false;
      componentResourceManager.ApplyResources((object) this.listConnections, "listConnections");
      this.listConnections.MultiSelect = false;
      this.listConnections.Name = "listConnections";
      this.helpProvider1.SetShowHelp((Control) this.listConnections, (bool) componentResourceManager.GetObject("listConnections.ShowHelp"));
      this.listConnections.UseCompatibleStateImageBehavior = false;
      this.listConnections.View = View.Details;
      this.listConnections.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.listConnections_ItemSelectionChanged);
      componentResourceManager.ApplyResources((object) this.btnRefresh, "btnRefresh");
      this.btnRefresh.Name = "btnRefresh";
      this.helpProvider1.SetShowHelp((Control) this.btnRefresh, (bool) componentResourceManager.GetObject("btnRefresh.ShowHelp"));
      this.btnRefresh.UseVisualStyleBackColor = true;
      this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
      this.AcceptButton = (IButtonControl) this.btnOK;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnOK;
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.gbxNetworkClientConnections);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (NetworkClientAdmin);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Load += new EventHandler(this.SystemSettingsNetworkClient_Load);
      this.gbxNetworkClientConnections.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private enum columns
    {
      NetworkClientId,
      DNSName,
      IPAdress,
      Meter,
      Account,
      Responsive,
    }
  }
}
