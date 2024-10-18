// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.ShippingProfiles
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Eventing;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Properties;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
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
  public class ShippingProfiles : HelpFormBase
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
    private ToolStripMenuItem domesticToolStripMenuItem;
    private ToolStripMenuItem internationalToolStripMenuItem;
    private ToolStripMenuItem transborderDistributionToolStripMenuItem;
    private DataGridViewImageColumn colImage;
    private DataGridViewTextBoxColumn colCode;
    private DataGridViewTextBoxColumn colDesc;

    public event TopicDelegate ProfilesListChanged;

    public event TopicDelegate ShippingPreferencesChanged;

    public ShippingProfiles() => this.InitializeComponent();

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
      if (!GuiData.CurrentAccount.IsTDEnabled)
        filterList.Add(new GsmFilter("ProfileType", "!=", (object) "T"));
      bool bVal = false;
      GuiData.ConfigManager.GetProfileBool("/SHIPNET2000/GUI/SETTINGS", "EnableFreight", out bVal);
      if (!bVal)
        filterList.Add(new GsmFilter("ProfileType", "!=", (object) "F"));
      DataTable output;
      GuiData.AppController.ShipEngine.GetDataList((object) null, GsmDataAccess.ListSpecification.ProfileList, out output, filterList, new List<GsmSort>()
      {
        new GsmSort("Code", GsmSort.SortOrderSpecification.Ascending)
      }, (List<string>) null, new Error());
      if (output.Rows.Count == 0)
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "No default Profiles were created on initial new install");
      this.dataGridView.AutoGenerateColumns = false;
      this.dataGridView.DataSource = (object) output;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      this.btnAdd.ContextMenuStrip.Show((Control) this.btnAdd, 0, this.btnAdd.Height);
    }

    private void btnAddByDup_Click(object sender, EventArgs e)
    {
      this.ShowPrefDialog(Utility.FormOperation.AddByDup);
    }

    private T GetProfile<T>(T inFilter, Utility.FormOperation eOperation, Error error) where T : ShipDefl, new()
    {
      T output = new T();
      inFilter.AccountNum = GuiData.CurrentAccount.AccountNumber;
      inFilter.MeterNum = GuiData.CurrentAccount.MeterNumber;
      switch (eOperation)
      {
        case Utility.FormOperation.Add:
          return output;
        case Utility.FormOperation.AddByDup:
        case Utility.FormOperation.ViewEdit:
          if (GuiData.AppController.ShipEngine.Retrieve<T>(inFilter, out output, out error) == 1)
          {
            if (eOperation == Utility.FormOperation.AddByDup)
            {
              output.ProfileCode = string.Empty;
              output.Description = string.Empty;
            }
            return output;
          }
          break;
      }
      return default (T);
    }

    private bool SaveProfile<T>(T profile, Utility.FormOperation eOperation) where T : ShipDefl, new()
    {
      try
      {
        if (eOperation == Utility.FormOperation.ViewEdit)
        {
          ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.Modify<T>(profile);
          if (serviceResponse.Error.Code != 1)
          {
            Utility.DisplayError(serviceResponse.Error);
            return false;
          }
          try
          {
            if (profile.ProfileCode == "PASSPORT")
              GuiData.PassportPrefs = this.LoadPrefs<PassportPreferences, DShipDefl, DefaultDShipDefl>("PASSPORT", GuiData.CurrentAccount);
            else if (profile.GetType() == typeof (DShipDefl))
            {
              if (GuiData.CurrentSender != null)
              {
                if (profile.ProfileCode == GuiData.CurrentSender.DomProfileCode)
                  GuiData.DomPrefs = this.LoadPrefs<DomPreferences, DShipDefl, DefaultDShipDefl>(profile.ProfileCode, GuiData.CurrentAccount);
              }
            }
            else if (profile.GetType() == typeof (IShipDefl))
            {
              if (GuiData.CurrentSender != null)
              {
                if (profile.ProfileCode == GuiData.CurrentSender.IntProfileCode)
                  GuiData.IntlPrefs = this.LoadPrefs<IntlPreferences, IShipDefl, DefaultIShipDefl>(profile.ProfileCode, GuiData.CurrentAccount);
              }
            }
            else if (profile.GetType() == typeof (TDShipDefl))
            {
              if (GuiData.CurrentSender != null)
              {
                if (profile.ProfileCode == GuiData.CurrentSender.TDProfileCode)
                  GuiData.TDPrefs = this.LoadPrefs<TdPreferences, TDShipDefl, DefaultTDShipDefl>(profile.ProfileCode, GuiData.CurrentAccount);
              }
            }
            else if (profile.GetType() == typeof (FShipDefl))
            {
              if (GuiData.CurrentSender != null)
                GuiData.FreightPrefs = this.LoadPrefs<FreightPreferences, FShipDefl, DefaultFShipDefl>(profile.ProfileCode, GuiData.CurrentAccount);
            }
          }
          catch (Exception ex)
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Exception Loading Profile:" + ex.ToString());
          }
          ShippingPreferencesChangedEventArgs args = new ShippingPreferencesChangedEventArgs((ShipDefl) profile);
          if (this.ShippingPreferencesChanged != null)
            this.ShippingPreferencesChanged((object) this, (EventArgs) args);
        }
        else
        {
          ServiceResponse serviceResponse = GuiData.AppController.ShipEngine.Insert<T>(profile);
          if (serviceResponse.Error.Code != 1)
          {
            Utility.DisplayError(serviceResponse.Error);
            return false;
          }
          EventArgs args = new EventArgs();
          if (this.ProfilesListChanged != null)
            this.ProfilesListChanged((object) this, args);
        }
        DataRow dataRow = Utility.CurrentRow(this.dataGridView);
        if (eOperation == Utility.FormOperation.ViewEdit && dataRow != null)
          this.RefreshList(dataRow["Code"].ToString(), dataRow["Type"].ToString());
        else if (eOperation == Utility.FormOperation.Add || eOperation == Utility.FormOperation.AddByDup)
          this.RefreshList(profile.ProfileCode, profile.GetType().ToString().Substring(profile.GetType().ToString().IndexOf("ShipDefl") - 1, 1).ToUpper());
        else
          this.RefreshList();
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Exception Saving Profile:" + ex.ToString());
        return false;
      }
      return true;
    }

    private TPrefs LoadPrefs<TPrefs, TDefl, TDefDefl>(string code, Account acct)
      where TPrefs : IPreferences, new()
      where TDefl : ShipDefl, new()
      where TDefDefl : ShipDefl, new()
    {
      TPrefs prefs = default (TPrefs);
      TDefl filter = new TDefl();
      filter.ProfileCode = code;
      filter.AccountNum = acct.AccountNumber;
      filter.MeterNum = acct.MeterNumber;
      TDefl output1;
      Error error;
      if (GuiData.AppController.ShipEngine.Retrieve<TDefl>(filter, out output1, out error) == 1)
      {
        prefs = new TPrefs();
        prefs.Initialize((ShipDefl) output1);
      }
      else
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), "Cannot load " + prefs.GetType().ToString());
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, this.ToString(), "Attempting to load default prefs: " + prefs.GetType().ToString());
        TDefDefl output2;
        if (GuiData.AppController.ShipEngine.Retrieve<TDefDefl>(new TDefDefl(), out output2, out error) == 1)
        {
          output2.ProfileCode = code;
          prefs = new TPrefs();
          prefs.Initialize((ShipDefl) output2);
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString(), "Cannot even load the default domestic prefs.");
      }
      return prefs;
    }

    private void ShowPrefDialog(Utility.FormOperation eOperation)
    {
      Error error = new Error();
      DataRow dataRow = Utility.CurrentRow(this.dataGridView);
      if (dataRow == null)
        return;
      switch (dataRow["Type"].ToString())
      {
        case "D":
          DShipDefl inFilter1 = new DShipDefl();
          inFilter1.ProfileCode = dataRow["Code"].ToString();
          inFilter1.MeterNum = GuiData.CurrentAccount.MeterNumber;
          inFilter1.AccountNum = GuiData.CurrentAccount.AccountNumber;
          DShipDefl profile1 = this.GetProfile<DShipDefl>(inFilter1, eOperation, error);
          if (profile1 != null)
          {
            if ((!profile1.ProfileCode.Equals("PASSPORT") ? (Form) new DomPrefDlg(profile1, eOperation) : (Form) new PassportPrefDlg(profile1)).ShowDialog() != DialogResult.OK)
              break;
            this.SaveProfile<DShipDefl>(profile1, eOperation);
            break;
          }
          if (error.Code == 1)
            break;
          Utility.DisplayError(error);
          break;
        case "I":
          IShipDefl inFilter2 = new IShipDefl();
          inFilter2.ProfileCode = dataRow["Code"].ToString();
          inFilter2.MeterNum = GuiData.CurrentAccount.MeterNumber;
          inFilter2.AccountNum = GuiData.CurrentAccount.AccountNumber;
          IShipDefl profile2 = this.GetProfile<IShipDefl>(inFilter2, eOperation, error);
          if (profile2 != null)
          {
            if (new IntlPrefDlg(profile2, eOperation).ShowDialog() != DialogResult.OK)
              break;
            this.SaveProfile<IShipDefl>(profile2, eOperation);
            break;
          }
          if (error.Code == 1)
            break;
          Utility.DisplayError(error);
          break;
        case "T":
          TDShipDefl inFilter3 = new TDShipDefl();
          inFilter3.ProfileCode = dataRow["Code"].ToString();
          inFilter3.MeterNum = GuiData.CurrentAccount.MeterNumber;
          inFilter3.AccountNum = GuiData.CurrentAccount.AccountNumber;
          TDShipDefl profile3 = this.GetProfile<TDShipDefl>(inFilter3, eOperation, error);
          if (profile3 != null)
          {
            if (new TDPrefDlg(profile3, eOperation).ShowDialog() != DialogResult.OK)
              break;
            this.SaveProfile<TDShipDefl>(profile3, eOperation);
            break;
          }
          Utility.DisplayError(error);
          break;
        case "F":
          FShipDefl inFilter4 = new FShipDefl();
          inFilter4.ProfileCode = dataRow["Code"].ToString();
          FShipDefl profile4 = this.GetProfile<FShipDefl>(inFilter4, eOperation, error);
          if (profile4 != null)
          {
            if (new FreightPrefDlg(profile4, eOperation).ShowDialog() != DialogResult.OK)
              break;
            this.SaveProfile<FShipDefl>(profile4, eOperation);
            break;
          }
          if (error.Code == 1)
            break;
          Utility.DisplayError(error);
          break;
      }
    }

    private void btnViewEdit_Click(object sender, EventArgs e)
    {
      this.ShowPrefDialog(Utility.FormOperation.ViewEdit);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GuiData.Languafier.TranslateMessage(38025), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      DataRow dataRow = Utility.CurrentRow(this.dataGridView);
      if (dataRow == null)
        return;
      ShipDefl shipProfile;
      if (dataRow["Type"].ToString() == "D")
        shipProfile = (ShipDefl) new DShipDefl();
      else if (dataRow["Type"].ToString() == "I")
      {
        shipProfile = (ShipDefl) new IShipDefl();
      }
      else
      {
        if (!(dataRow["Type"].ToString() == "T"))
          return;
        shipProfile = (ShipDefl) new TDShipDefl();
      }
      shipProfile.ProfileCode = dataRow["Code"].ToString();
      try
      {
        if (ShippingProfiles.DeleteProfile(shipProfile).Error.Code != 1)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Debug, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Successfully deleted Profile");
          EventArgs args = new EventArgs();
          if (this.ProfilesListChanged != null)
            this.ProfilesListChanged((object) this, args);
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Failed to delete Profile");
      }
      catch
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Caught exception deleting Profile");
      }
      this.RefreshList();
    }

    private static ServiceResponse DeleteProfile(ShipDefl shipProfile)
    {
      switch (shipProfile)
      {
        case DShipDefl _:
          return GuiData.AppController.ShipEngine.Delete<DShipDefl>((DShipDefl) shipProfile);
        case IShipDefl _:
          return GuiData.AppController.ShipEngine.Delete<IShipDefl>((IShipDefl) shipProfile);
        case TDShipDefl _:
          return GuiData.AppController.ShipEngine.Delete<TDShipDefl>((TDShipDefl) shipProfile);
        default:
          return GuiData.AppController.ShipEngine.Delete<ShipDefl>(shipProfile);
      }
    }

    private void domesticToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DShipDefl dshipDefl = new DShipDefl();
      dshipDefl.UpgradeDowngradeOptions = new UpgradeDowngradeOptions();
      dshipDefl.MeterNum = GuiData.CurrentAccount.MeterNumber;
      dshipDefl.AccountNum = GuiData.CurrentAccount.AccountNumber;
      DefaultDShipDefl output = new DefaultDShipDefl();
      Error error = new Error();
      DefaultDShipDefl filter = new DefaultDShipDefl();
      filter.DefaultInitializationType = ShipDefl.DefaultPreferenceType.Shipment;
      try
      {
        if (GuiData.AppController.ShipEngine.Retrieve<DefaultDShipDefl>(filter, out output, out error) == 1)
        {
          dshipDefl.FieldPrefs = output.FieldPrefs;
          dshipDefl.EmailNotifPrefs = output.EmailNotifPrefs;
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not Retrieve Default Dom Profile" + error.Code.ToString() + " " + error.Message);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Threw exception Retrieving/Saving Default Dom Profile" + ex.Message);
      }
      if (new DomPrefDlg(dshipDefl, Utility.FormOperation.Add).ShowDialog() != DialogResult.OK)
        return;
      this.SaveProfile<DShipDefl>(dshipDefl, Utility.FormOperation.Add);
    }

    private void internationalToolStripMenuItem_Click(object sender, EventArgs e)
    {
      IShipDefl ishipDefl = new IShipDefl();
      ishipDefl.MeterNum = GuiData.CurrentAccount.MeterNumber;
      ishipDefl.AccountNum = GuiData.CurrentAccount.AccountNumber;
      DefaultIShipDefl output = new DefaultIShipDefl();
      Error error = new Error();
      DefaultIShipDefl filter = new DefaultIShipDefl();
      filter.DefaultInitializationType = ShipDefl.DefaultPreferenceType.Shipment;
      try
      {
        if (GuiData.AppController.ShipEngine.Retrieve<DefaultIShipDefl>(filter, out output, out error) == 1)
        {
          ishipDefl.FieldPrefs = output.FieldPrefs;
          ishipDefl.EmailNotifPrefs = output.EmailNotifPrefs;
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not Retrieve Default Dom Profile" + error.Code.ToString() + " " + error.Message);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Threw exception Retrieving/Saving Default Dom Profile" + ex.Message);
      }
      if (new IntlPrefDlg(ishipDefl, Utility.FormOperation.Add).ShowDialog() != DialogResult.OK)
        return;
      this.SaveProfile<IShipDefl>(ishipDefl, Utility.FormOperation.Add);
    }

    private void transborderDistributionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TDShipDefl tdShipDefl = new TDShipDefl();
      tdShipDefl.MeterNum = GuiData.CurrentAccount.MeterNumber;
      tdShipDefl.AccountNum = GuiData.CurrentAccount.AccountNumber;
      DefaultTDShipDefl output = new DefaultTDShipDefl();
      Error error = new Error();
      DefaultTDShipDefl filter = new DefaultTDShipDefl();
      filter.DefaultInitializationType = ShipDefl.DefaultPreferenceType.Shipment;
      try
      {
        if (GuiData.AppController.ShipEngine.Retrieve<DefaultTDShipDefl>(filter, out output, out error) == 1)
        {
          tdShipDefl.FieldPrefs = output.FieldPrefs;
          tdShipDefl.EmailNotifPrefs = output.EmailNotifPrefs;
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Could not Retrieve Default Dom Profile" + error.Code.ToString() + " " + error.Message);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Threw exception Retrieving/Saving Default Dom Profile" + ex.Message);
      }
      if (new TDPrefDlg(tdShipDefl, Utility.FormOperation.Add).ShowDialog() != DialogResult.OK)
        return;
      this.SaveProfile<TDShipDefl>(tdShipDefl, Utility.FormOperation.Add);
    }

    private void dataGridView_SelectionChanged(object sender, EventArgs e) => this.EnableButtons();

    private void EnableButtons()
    {
      bool flag1 = false;
      bool flag2 = false;
      int count = this.dataGridView.SelectedRows.Count;
      DataRow dataRow = Utility.CurrentRow(this.dataGridView);
      if (dataRow != null)
      {
        flag1 = dataRow["Code"].ToString() == "DEFAULT";
        flag2 = dataRow["Code"].ToString() == "PASSPORT";
      }
      this.btnAdd.Enabled = dataRow["Type"].ToString() != "F";
      this.btnAddByDup.Enabled = count == 1 && !flag2 && dataRow["Type"].ToString() != "F";
      this.btnViewEdit.Enabled = count == 1;
      this.btnDelete.Enabled = count >= 1 && !flag2 && !flag1;
    }

    private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      switch (e.Value.ToString())
      {
        case "D":
        case "F":
          if (((DataGridView) sender).Rows[e.RowIndex].Cells[1].Value.ToString().StartsWith("P"))
          {
            e.Value = (object) Resources.Passport;
            break;
          }
          e.Value = (object) Resources.Domestic;
          break;
        case "I":
          e.Value = (object) Resources.International;
          break;
        case "T":
          e.Value = (object) Resources.TD;
          break;
      }
    }

    private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex <= -1)
        return;
      this.btnViewEdit_Click(sender, (EventArgs) e);
    }

    private void ShippingProfiles_Load(object sender, EventArgs e)
    {
      this.SetupEvents();
      Utility.UpdateToolstripFonts((ToolStrip) this.menuAddPopup);
      this.RefreshList();
      if (!GuiData.CurrentAccount.IsTDEnabled)
        this.transborderDistributionToolStripMenuItem.Visible = false;
      if (!GuiData.ConfigManager.GetProfileBool("SHIPNET2000/GUI/SETTINGS", "EnableFreight", out bool _) || ((DataTable) this.dataGridView.DataSource).Select("Type = 'F'").Length != 0)
        return;
      Utility.CreateDefaultFreightProfile();
      this.RefreshList();
    }

    private void SetupEvents()
    {
      GuiData.EventBroker.GetTopic(EventBroker.Events.ProfilesListChanged)?.AddPublisher((object) this, "ProfilesListChanged");
      GuiData.EventBroker.GetTopic(EventBroker.Events.ShippingPreferencesChanged)?.AddPublisher((object) this, "ShippingPreferencesChanged");
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ShippingProfiles));
      this.flowLayoutPanel1 = new FlowLayoutPanel();
      this.btnOk = new Button();
      this.btnAdd = new Button();
      this.menuAddPopup = new ContextMenuStrip(this.components);
      this.domesticToolStripMenuItem = new ToolStripMenuItem();
      this.internationalToolStripMenuItem = new ToolStripMenuItem();
      this.transborderDistributionToolStripMenuItem = new ToolStripMenuItem();
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
      this.menuAddPopup.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.domesticToolStripMenuItem,
        (ToolStripItem) this.internationalToolStripMenuItem,
        (ToolStripItem) this.transborderDistributionToolStripMenuItem
      });
      this.menuAddPopup.Name = "menuAddPopup";
      componentResourceManager.ApplyResources((object) this.menuAddPopup, "menuAddPopup");
      this.domesticToolStripMenuItem.Name = "domesticToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.domesticToolStripMenuItem, "domesticToolStripMenuItem");
      this.domesticToolStripMenuItem.Click += new EventHandler(this.domesticToolStripMenuItem_Click);
      this.internationalToolStripMenuItem.Name = "internationalToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.internationalToolStripMenuItem, "internationalToolStripMenuItem");
      this.internationalToolStripMenuItem.Click += new EventHandler(this.internationalToolStripMenuItem_Click);
      this.transborderDistributionToolStripMenuItem.Name = "transborderDistributionToolStripMenuItem";
      componentResourceManager.ApplyResources((object) this.transborderDistributionToolStripMenuItem, "transborderDistributionToolStripMenuItem");
      this.transborderDistributionToolStripMenuItem.Click += new EventHandler(this.transborderDistributionToolStripMenuItem_Click);
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
      this.colCode.DataPropertyName = "Code";
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
      this.Name = nameof (ShippingProfiles);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Load += new EventHandler(this.ShippingProfiles_Load);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.menuAddPopup.ResumeLayout(false);
      ((ISupportInitialize) this.dataGridView).EndInit();
      this.ResumeLayout(false);
    }
  }
}
