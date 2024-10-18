// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.ShipAlertPreferences
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class ShipAlertPreferences : UserControlHelpEx
  {
    private ShipDefl _shipDefl;
    private int _prevSelection = -1;
    private int _currentSelection = -1;
    private bool _bIgnore;
    private bool _combosLoaded;
    private bool _bLoading;
    private int _prevPrefTypeIndex = -1;
    private int _currentPrefTypeIndex = -1;
    private bool _bResettingPrefType;
    private IContainer components;
    protected ColorGroupBox gbxFieldValue;
    protected FdxMaskedEdit edtConstant;
    protected ComboBoxEx cboConstant;
    protected ColorGroupBox gbxBehavior;
    protected RadioButton rdbUnchecked;
    protected TableLayoutPanel tableLayoutPanel1;
    protected RadioButton rdbAutoSelect;
    protected RadioButton rdbSkip;
    protected RadioButton rdbAlwaysChecked;
    protected Panel panel1;
    protected ComboBoxEx cboPreferenceType;
    protected Label label1;
    protected ListBox lbxFields;
    private FocusExtender focusExtender1;

    public ShipDefl PreferenceObject
    {
      get => this._shipDefl;
      set
      {
        this._shipDefl = value;
        this.RefreshList();
      }
    }

    public bool IsLoading
    {
      get => this._bLoading;
      set => this._bLoading = value;
    }

    public int PrevPrefTypeIndex
    {
      get => this._prevPrefTypeIndex;
      set => this._prevPrefTypeIndex = value;
    }

    public int CurrentPrefTypeIndex
    {
      get => this._currentPrefTypeIndex;
      set => this._currentPrefTypeIndex = value;
    }

    protected virtual bool SaveFieldPref(int index) => false;

    protected virtual void PopulateControl(FieldPref fp)
    {
    }

    public ShipAlertPreferences() => this.InitializeComponent();

    protected virtual void SetBehavior()
    {
      this._bLoading = true;
      FieldPref selectedEmailPref = this.GetSelectedEmailPref();
      this.rdbUnchecked.Checked = true;
      if (selectedEmailPref != null)
      {
        if (this.IsShipAlertPref(selectedEmailPref.Index))
        {
          this.rdbUnchecked.Checked = selectedEmailPref.Behavior == ShipDefl.Behavior.None;
          this.rdbAutoSelect.Checked = selectedEmailPref.Behavior == ShipDefl.Behavior.AutoSelect;
          this.rdbAlwaysChecked.Checked = selectedEmailPref.Behavior == ShipDefl.Behavior.AlwaysChecked;
          this.rdbSkip.Checked = selectedEmailPref.Behavior == ShipDefl.Behavior.Skip;
          this.rdbUnchecked.Text = GuiData.Languafier.Translate("FIELDPREF_BEHAVIOR_UNCHECKED");
          this.rdbAutoSelect.Text = GuiData.Languafier.Translate("FIELDPREF_BEHAVIOR_AUTOMATICALLYSELECT");
          this.rdbAlwaysChecked.Text = GuiData.Languafier.Translate("FIELDPREF_BEHAVIOR_ALWAYSCHECKED");
          this.rdbSkip.Text = GuiData.Languafier.Translate("FIELDPREF_BEHAVIOR_SKIP");
        }
        else
        {
          this.rdbUnchecked.Checked = selectedEmailPref.Behavior == ShipDefl.Behavior.None;
          this.rdbAutoSelect.Checked = selectedEmailPref.Behavior == ShipDefl.Behavior.Constant;
          this.rdbAlwaysChecked.Checked = selectedEmailPref.Behavior == ShipDefl.Behavior.Carry;
          this.rdbSkip.Checked = selectedEmailPref.Behavior == ShipDefl.Behavior.Skip;
          this.rdbUnchecked.Text = GuiData.Languafier.Translate("FIELDPREF_BEHAVIOR_NONE");
          this.rdbAutoSelect.Text = GuiData.Languafier.Translate("FIELDPREF_BEHAVIOR_CONST");
          this.rdbAlwaysChecked.Text = GuiData.Languafier.Translate("FIELDPREF_BEHAVIOR_CARRY");
          this.rdbSkip.Text = GuiData.Languafier.Translate("FIELDPREF_BEHAVIOR_SKIP");
        }
      }
      this._bLoading = false;
    }

    protected virtual ShipDefl.Behavior GetBehavior(int iIndex)
    {
      if (this.IsShipAlertPref(iIndex))
      {
        if (this.rdbAutoSelect.Checked)
          return ShipDefl.Behavior.AutoSelect;
        if (this.rdbSkip.Checked)
          return ShipDefl.Behavior.Skip;
        return this.rdbAlwaysChecked.Checked ? ShipDefl.Behavior.AlwaysChecked : ShipDefl.Behavior.None;
      }
      if (this.rdbAutoSelect.Checked)
        return ShipDefl.Behavior.Constant;
      if (this.rdbSkip.Checked)
        return ShipDefl.Behavior.Skip;
      return this.rdbAlwaysChecked.Checked ? ShipDefl.Behavior.Carry : ShipDefl.Behavior.None;
    }

    protected virtual void RefreshList()
    {
      if (this._shipDefl == null)
        return;
      this.lbxFields.DataSource = (object) this._shipDefl.FieldPrefs;
      this.lbxFields.DisplayMember = "FieldDescription";
      this.lbxFields.ValueMember = "Index";
    }

    protected virtual void BehaviorCheckedChanged(object sender, EventArgs e)
    {
      RadioButton radioButton = sender as RadioButton;
      if (!radioButton.Checked)
        return;
      FieldPref selectedEmailPref = this.GetSelectedEmailPref();
      if (selectedEmailPref == null)
        return;
      this.ShowFieldValue(selectedEmailPref, radioButton.Equals((object) this.rdbAutoSelect));
    }

    public virtual bool OkToClose()
    {
      return this.lbxFields.SelectedIndex <= -1 || this.SaveFieldPref((int) ((DataRowView) this.lbxFields.Items[this.lbxFields.SelectedIndex])[0]);
    }

    protected virtual bool HasAuxData(FieldPref fp)
    {
      bool flag = false;
      if (fp.Control == FieldPref.ControlType.DropDownCombo || fp.Control == FieldPref.ControlType.TextBox)
        flag = true;
      return flag;
    }

    protected virtual Control GetControl(FieldPref fp)
    {
      if (fp == null)
        return (Control) null;
      switch (fp.Control)
      {
        case FieldPref.ControlType.NoControl:
          return (Control) null;
        case FieldPref.ControlType.DropDownCombo:
        case FieldPref.ControlType.DropDownComboAndTextBox:
          return (Control) this.cboConstant;
        case FieldPref.ControlType.TextBox:
        case FieldPref.ControlType.TextBoxWithBrowse:
          return (Control) this.edtConstant;
        default:
          return (Control) null;
      }
    }

    protected virtual void PopulatePreferenceTypesCombo()
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Code", typeof (int));
      dataTable.Columns.Add("Description");
      foreach (FieldPref.FieldPreferenceType fieldPreferenceType in Enum.GetValues(typeof (FieldPref.FieldPreferenceType)))
      {
        switch (fieldPreferenceType)
        {
          case FieldPref.FieldPreferenceType.Outbound:
            DataRow row1 = dataTable.NewRow();
            row1["Code"] = (object) fieldPreferenceType;
            row1["Description"] = (object) GuiData.Languafier.Translate("PREFTYPE_" + fieldPreferenceType.ToString());
            dataTable.Rows.Add(row1);
            continue;
          case FieldPref.FieldPreferenceType.Returns:
            if (!this._shipDefl.GetType().ToString().Contains("TDShipDefl") && !this._shipDefl.GetType().ToString().Contains("FShipDefl"))
            {
              DataRow row2 = dataTable.NewRow();
              row2["Code"] = (object) fieldPreferenceType;
              row2["Description"] = (object) GuiData.Languafier.Translate("PREFTYPE_" + fieldPreferenceType.ToString());
              dataTable.Rows.Add(row2);
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      this.cboPreferenceType.DisplayMemberQ = "Description";
      this.cboPreferenceType.ValueMemberQ = "Code";
      this.cboPreferenceType.DataSource = (object) dataTable;
    }

    private bool IsShipAlertPref(int iIndex)
    {
      bool flag1 = this._shipDefl.GetType().ToString().Contains("IShipDefl");
      bool flag2 = this._shipDefl.GetType().ToString().Contains("DShipDefl");
      bool flag3 = this._shipDefl.GetType().ToString().Contains("TDShipDefl");
      bool flag4 = this._shipDefl.GetType().ToString().Contains("FShipDefl");
      return (!flag1 || iIndex != (int) sbyte.MaxValue && iIndex != 126 && iIndex != 125 && iIndex != 124 && iIndex != 123 && iIndex != 132 && iIndex != 131 && iIndex != 130 && iIndex != 129 && iIndex != 128 && iIndex != 31 && iIndex != 32 && iIndex != 83 && iIndex != 84) && (!flag2 || iIndex != 130 && iIndex != 129 && iIndex != 128 && iIndex != (int) sbyte.MaxValue && iIndex != 126 && iIndex != 135 && iIndex != 134 && iIndex != 133 && iIndex != 132 && iIndex != 131 && iIndex != 52 && iIndex != 53 && iIndex != 54 && iIndex != 55) && (!flag3 || iIndex != 49 && iIndex != 48 && iIndex != 47 && iIndex != 46 && iIndex != 45 && iIndex != 25 && iIndex != 26) && (!flag4 || iIndex != 41 && iIndex != 40 && iIndex != 39 && iIndex != 38 && iIndex != 37 && iIndex != 30 && iIndex != 31) && (!flag1 || iIndex != 91 && iIndex != 92 && iIndex != 93 && iIndex != 94 && iIndex != 95 && iIndex != 96 && iIndex != 97 && iIndex != 98 && iIndex != 99 && iIndex != 100 && iIndex != 101 && iIndex != 102) && (!flag2 || iIndex != 94 && iIndex != 95 && iIndex != 96 && iIndex != 97 && iIndex != 98 && iIndex != 99 && iIndex != 100 && iIndex != 101 && iIndex != 102 && iIndex != 103 && iIndex != 104 && iIndex != 105);
    }

    protected virtual void ConfigureControl(Control c, FieldPref fp)
    {
      if (c == null || fp == null || fp.Control != FieldPref.ControlType.DropDownCombo)
        return;
      ((ComboBox) c).DropDownStyle = ComboBoxStyle.DropDownList;
    }

    protected virtual void ShowValidBehaviors()
    {
      FieldPref selectedEmailPref = this.GetSelectedEmailPref();
      if (selectedEmailPref == null)
        return;
      if (this.IsShipAlertPref(selectedEmailPref.Index))
      {
        this.rdbUnchecked.Visible = selectedEmailPref.AllowedBehaviors.Contains(ShipDefl.Behavior.None);
        this.rdbAutoSelect.Visible = selectedEmailPref.AllowedBehaviors.Contains(ShipDefl.Behavior.AutoSelect);
        this.rdbAlwaysChecked.Visible = selectedEmailPref.AllowedBehaviors.Contains(ShipDefl.Behavior.AlwaysChecked);
        this.rdbSkip.Visible = selectedEmailPref.AllowedBehaviors.Contains(ShipDefl.Behavior.Skip);
        this.gbxFieldValue.Hide();
      }
      else
      {
        this.rdbUnchecked.Visible = selectedEmailPref.AllowedBehaviors.Contains(ShipDefl.Behavior.None);
        this.rdbAutoSelect.Visible = selectedEmailPref.AllowedBehaviors.Contains(ShipDefl.Behavior.Constant);
        this.rdbAlwaysChecked.Visible = selectedEmailPref.AllowedBehaviors.Contains(ShipDefl.Behavior.Carry);
        this.rdbSkip.Visible = selectedEmailPref.AllowedBehaviors.Contains(ShipDefl.Behavior.Skip);
        this.gbxFieldValue.Show();
      }
    }

    protected FieldPref GetSelectedEmailPref()
    {
      FieldPref selectedEmailPref = (FieldPref) null;
      if (this._shipDefl != null)
      {
        DataRowView dataRowView = this.lbxFields.Items[this.lbxFields.SelectedIndex] as DataRowView;
        int result = 0;
        int.TryParse(dataRowView["Index"].ToString(), out result);
        selectedEmailPref = this._shipDefl.GetEmailFieldPref(result);
      }
      return selectedEmailPref;
    }

    protected virtual void ShowFieldValue(FieldPref fp, bool bShow)
    {
      if (fp == null)
        return;
      if (bShow)
      {
        foreach (Control control in (ArrangedElementCollection) this.gbxFieldValue.Controls)
          control.Hide();
        switch (fp.Control)
        {
          case FieldPref.ControlType.DropDownCombo:
            this.ConfigureControl((Control) this.cboConstant, fp);
            this.cboConstant.Show();
            this.PopulateControl(fp);
            this.cboConstant.SelectedValueQ = (object) fp.IntFieldDeflVal;
            if (this.cboConstant.SelectedValueQ != null || fp.DefaultValueType != FieldPref.PreferenceValueType.String)
              break;
            this.cboConstant.SelectedValueQ = (object) fp.StringFieldDeflVal;
            break;
          case FieldPref.ControlType.TextBox:
            this.ConfigureControl((Control) this.edtConstant, fp);
            this.edtConstant.Show();
            this.edtConstant.SetMask('I', 120);
            this.edtConstant.Text = fp.StringFieldDeflVal;
            break;
        }
      }
      else
      {
        foreach (Control control in (ArrangedElementCollection) this.gbxFieldValue.Controls)
          control.Hide();
      }
    }

    private void lbxFields_SelectedIndexChanged(object sender, EventArgs e)
    {
      this._prevSelection = this._currentSelection;
      this._currentSelection = this.lbxFields.SelectedIndex;
      if (this._bIgnore)
        this._bIgnore = false;
      else if (this._prevSelection > -1 && this._prevSelection < this.lbxFields.Items.Count)
      {
        if (this.SaveFieldPref((int) ((DataRowView) this.lbxFields.Items[this._prevSelection])[0]))
        {
          this.ShowValidBehaviors();
          this.SetBehavior();
        }
        else
        {
          this._bIgnore = true;
          this.lbxFields.SelectedIndex = this._prevSelection;
        }
      }
      else
      {
        this.ShowValidBehaviors();
        this.SetBehavior();
      }
    }

    private void ShipAlertPreferences_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      if (this._shipDefl != null && !this._shipDefl.GetType().ToString().Contains("FShipDefl"))
        this.PopulatePreferenceTypesCombo();
      this.RefreshList();
    }

    private void cboPreferenceType_SelectedValueChanged(object sender, EventArgs e)
    {
      this.CurrentPrefTypeIndex = this.cboPreferenceType.SelectedIndex;
      if (this.PrevPrefTypeIndex == -1)
        this.PrevPrefTypeIndex = this.cboPreferenceType.SelectedIndex;
      if (this.lbxFields.SelectedIndex > -1 && !this._bResettingPrefType && this.SaveFieldPref((int) ((DataRowView) this.lbxFields.Items[this.lbxFields.SelectedIndex])[0]))
      {
        this.PrevPrefTypeIndex = this.cboPreferenceType.SelectedIndex;
        this.RefreshList();
      }
      else
      {
        if (this.PrevPrefTypeIndex <= -1 || this.PrevPrefTypeIndex == this.CurrentPrefTypeIndex)
          return;
        this._bResettingPrefType = true;
        this.CurrentPrefTypeIndex = this.PrevPrefTypeIndex;
        this.cboPreferenceType.SelectedIndexQ = this.PrevPrefTypeIndex;
        this._bResettingPrefType = false;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ShipAlertPreferences));
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.gbxFieldValue = new ColorGroupBox();
      this.cboConstant = new ComboBoxEx();
      this.edtConstant = new FdxMaskedEdit();
      this.gbxBehavior = new ColorGroupBox();
      this.rdbSkip = new RadioButton();
      this.rdbAlwaysChecked = new RadioButton();
      this.rdbAutoSelect = new RadioButton();
      this.rdbUnchecked = new RadioButton();
      this.panel1 = new Panel();
      this.lbxFields = new ListBox();
      this.label1 = new Label();
      this.cboPreferenceType = new ComboBoxEx();
      this.focusExtender1 = new FocusExtender();
      this.tableLayoutPanel1.SuspendLayout();
      this.gbxFieldValue.SuspendLayout();
      this.gbxBehavior.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.helpProvider1, "helpProvider1");
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.Controls.Add((Control) this.gbxFieldValue, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.gbxBehavior, 1, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.panel1, 0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.helpProvider1.SetShowHelp((Control) this.tableLayoutPanel1, (bool) componentResourceManager.GetObject("tableLayoutPanel1.ShowHelp"));
      this.gbxFieldValue.BorderThickness = 1f;
      this.gbxFieldValue.Controls.Add((Control) this.cboConstant);
      this.gbxFieldValue.Controls.Add((Control) this.edtConstant);
      componentResourceManager.ApplyResources((object) this.gbxFieldValue, "gbxFieldValue");
      this.gbxFieldValue.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxFieldValue.Name = "gbxFieldValue";
      this.gbxFieldValue.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxFieldValue, (bool) componentResourceManager.GetObject("gbxFieldValue.ShowHelp"));
      this.gbxFieldValue.TabStop = false;
      this.cboConstant.DisplayMemberQ = "";
      this.cboConstant.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboConstant, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboConstant, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.cboConstant, componentResourceManager.GetString("cboConstant.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboConstant, (HelpNavigator) componentResourceManager.GetObject("cboConstant.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.cboConstant, "cboConstant");
      this.cboConstant.Name = "cboConstant";
      this.cboConstant.SelectedIndexQ = -1;
      this.cboConstant.SelectedItemQ = (object) null;
      this.cboConstant.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboConstant, (bool) componentResourceManager.GetObject("cboConstant.ShowHelp"));
      this.cboConstant.ValueMemberQ = "";
      this.edtConstant.Allow = "";
      this.edtConstant.Disallow = "";
      this.edtConstant.eMask = eMasks.maskCustom;
      this.edtConstant.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtConstant, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtConstant, SystemColors.WindowText);
      componentResourceManager.ApplyResources((object) this.edtConstant, "edtConstant");
      this.edtConstant.Mask = "";
      this.edtConstant.Name = "edtConstant";
      this.helpProvider1.SetShowHelp((Control) this.edtConstant, (bool) componentResourceManager.GetObject("edtConstant.ShowHelp"));
      this.gbxBehavior.BorderThickness = 1f;
      this.gbxBehavior.Controls.Add((Control) this.rdbSkip);
      this.gbxBehavior.Controls.Add((Control) this.rdbAlwaysChecked);
      this.gbxBehavior.Controls.Add((Control) this.rdbAutoSelect);
      this.gbxBehavior.Controls.Add((Control) this.rdbUnchecked);
      componentResourceManager.ApplyResources((object) this.gbxBehavior, "gbxBehavior");
      this.gbxBehavior.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.gbxBehavior.Name = "gbxBehavior";
      this.gbxBehavior.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxBehavior, (bool) componentResourceManager.GetObject("gbxBehavior.ShowHelp"));
      this.gbxBehavior.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.rdbSkip, componentResourceManager.GetString("rdbSkip.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbSkip, (HelpNavigator) componentResourceManager.GetObject("rdbSkip.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbSkip, "rdbSkip");
      this.rdbSkip.Name = "rdbSkip";
      this.helpProvider1.SetShowHelp((Control) this.rdbSkip, (bool) componentResourceManager.GetObject("rdbSkip.ShowHelp"));
      this.rdbSkip.CheckedChanged += new EventHandler(this.BehaviorCheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbAlwaysChecked, componentResourceManager.GetString("rdbAlwaysChecked.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbAlwaysChecked, (HelpNavigator) componentResourceManager.GetObject("rdbAlwaysChecked.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbAlwaysChecked, "rdbAlwaysChecked");
      this.rdbAlwaysChecked.Name = "rdbAlwaysChecked";
      this.helpProvider1.SetShowHelp((Control) this.rdbAlwaysChecked, (bool) componentResourceManager.GetObject("rdbAlwaysChecked.ShowHelp"));
      this.rdbAlwaysChecked.CheckedChanged += new EventHandler(this.BehaviorCheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbAutoSelect, componentResourceManager.GetString("rdbAutoSelect.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbAutoSelect, (HelpNavigator) componentResourceManager.GetObject("rdbAutoSelect.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbAutoSelect, "rdbAutoSelect");
      this.rdbAutoSelect.Name = "rdbAutoSelect";
      this.helpProvider1.SetShowHelp((Control) this.rdbAutoSelect, (bool) componentResourceManager.GetObject("rdbAutoSelect.ShowHelp"));
      this.rdbAutoSelect.CheckedChanged += new EventHandler(this.BehaviorCheckedChanged);
      this.helpProvider1.SetHelpKeyword((Control) this.rdbUnchecked, componentResourceManager.GetString("rdbUnchecked.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.rdbUnchecked, (HelpNavigator) componentResourceManager.GetObject("rdbUnchecked.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.rdbUnchecked, "rdbUnchecked");
      this.rdbUnchecked.Name = "rdbUnchecked";
      this.helpProvider1.SetShowHelp((Control) this.rdbUnchecked, (bool) componentResourceManager.GetObject("rdbUnchecked.ShowHelp"));
      this.rdbUnchecked.CheckedChanged += new EventHandler(this.BehaviorCheckedChanged);
      this.panel1.Controls.Add((Control) this.lbxFields);
      this.panel1.Controls.Add((Control) this.label1);
      this.panel1.Controls.Add((Control) this.cboPreferenceType);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      this.tableLayoutPanel1.SetRowSpan((Control) this.panel1, 2);
      this.helpProvider1.SetShowHelp((Control) this.panel1, (bool) componentResourceManager.GetObject("panel1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.lbxFields, "lbxFields");
      this.lbxFields.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.lbxFields, componentResourceManager.GetString("lbxFields.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.lbxFields, (HelpNavigator) componentResourceManager.GetObject("lbxFields.HelpNavigator"));
      this.lbxFields.Name = "lbxFields";
      this.helpProvider1.SetShowHelp((Control) this.lbxFields, (bool) componentResourceManager.GetObject("lbxFields.ShowHelp"));
      this.lbxFields.SelectedIndexChanged += new EventHandler(this.lbxFields_SelectedIndexChanged);
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      this.helpProvider1.SetShowHelp((Control) this.label1, (bool) componentResourceManager.GetObject("label1.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.cboPreferenceType, "cboPreferenceType");
      this.cboPreferenceType.DisplayMemberQ = "";
      this.cboPreferenceType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboPreferenceType.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboPreferenceType, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboPreferenceType, SystemColors.WindowText);
      this.cboPreferenceType.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboPreferenceType, componentResourceManager.GetString("cboPreferenceType.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.cboPreferenceType, (HelpNavigator) componentResourceManager.GetObject("cboPreferenceType.HelpNavigator"));
      this.cboPreferenceType.Name = "cboPreferenceType";
      this.cboPreferenceType.SelectedIndexQ = -1;
      this.cboPreferenceType.SelectedItemQ = (object) null;
      this.cboPreferenceType.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboPreferenceType, (bool) componentResourceManager.GetObject("cboPreferenceType.ShowHelp"));
      this.cboPreferenceType.ValueMemberQ = "";
      this.cboPreferenceType.SelectedValueChanged += new EventHandler(this.cboPreferenceType_SelectedValueChanged);
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.Name = nameof (ShipAlertPreferences);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.Load += new EventHandler(this.ShipAlertPreferences_Load);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.gbxFieldValue.ResumeLayout(false);
      this.gbxFieldValue.PerformLayout();
      this.gbxBehavior.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
