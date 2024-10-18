// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.ucDocTab
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.LabelDataComponents;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class ucDocTab : UserControlHelpEx
  {
    private DataTable _dt;
    private DataTable _dtSmartPost;
    private DataTable _dtFreight;
    private IContainer components;
    public CheckBox chkPrintTotalsDocTab;
    public FdxMaskedEdit edtDocTabLabel1;
    public FdxMaskedEdit edtDocTabLabel2;
    public FdxMaskedEdit edtDocTabLabel3;
    public FdxMaskedEdit edtDocTabLabel4;
    public FdxMaskedEdit edtDocTabLabel5;
    public FdxMaskedEdit edtDocTabLabel6;
    public FdxMaskedEdit edtDocTabLabel7;
    public FdxMaskedEdit edtDocTabLabel8;
    public ComboBoxEx cboDocTabData6;
    public ComboBoxEx cboDocTabData5;
    public ComboBoxEx cboDocTabData4;
    public ComboBoxEx cboDocTabData3;
    public ComboBoxEx cboDocTabData2;
    public ComboBoxEx cboDocTabData1;
    public FdxMaskedEdit edtDocTabLabel9;
    public ComboBoxEx cboDocTabData7;
    public ComboBoxEx cboDocTabData8;
    public ComboBoxEx cboDocTabData9;
    public ComboBoxEx cboDocTabData10;
    public FdxMaskedEdit edtDocTabLabel10;
    public ComboBoxEx cboDocTabData11;
    public FdxMaskedEdit edtDocTabLabel11;
    public ComboBoxEx cboDocTabData12;
    public FdxMaskedEdit edtDocTabLabel12;
    public ComboBoxEx cboDocTabData13;
    public FdxMaskedEdit edtDocTabLabel13;
    public ComboBoxEx cboDocTabData14;
    public FdxMaskedEdit edtDocTabLabel14;
    private FocusExtender focusExtender1;

    public bool IsSmartPost { get; set; }

    public bool IsFreight { get; set; }

    public ucDocTab()
    {
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this.PopulateFieldData();
    }

    private void PopulateFieldData()
    {
      if (this._dtSmartPost == null)
      {
        this._dtSmartPost = new FieldChooserDataComponent().getConfigurableDocTabFields();
        this._dtSmartPost.Rows.InsertAt(this._dtSmartPost.NewRow(), 0);
      }
      if (this._dt == null)
      {
        this._dt = new FieldChooserDataComponent().getConfigurableDocTabFields();
        foreach (DataRow row in this._dt.Select("CTS_Tag_Id = '2800' OR CTS_Tag_Id = '2801' OR CTS_Tag_Id = '2803' "))
          this._dt.Rows.Remove(row);
        this._dt.Rows.InsertAt(this._dt.NewRow(), 0);
      }
      if (this._dtFreight == null)
        this._dtFreight = Utility.GetDataTable(Utility.ListTypes.FreightDocTab);
      this.FillCombosWithValidatorFields();
    }

    public void AddSpecialServiceFields(IEnumerable<SplSvc> specialServices)
    {
      if (specialServices == null)
        return;
      foreach (SplSvc ss in specialServices.Where<SplSvc>((System.Func<SplSvc, bool>) (ss => ss.SpecialServiceCode == SplSvc.SpecialServiceType.NUM_SPEC_SVCS)))
        this._dt.Rows.Add((object) ss.OfferingID, (object) Utility.GetSpecialServiceIndicatorName(ss));
    }

    public void ReLoadcomboBoxes() => this.FillCombosWithValidatorFields();

    private void FillCombosWithValidatorFields()
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control is ComboBox)
        {
          this.FillCombo(control);
          if (this.IsFreight)
          {
            if (control.Name != "cboDocTabData14")
            {
              ComboBox comboBox = control as ComboBox;
              comboBox.BindingContext = new BindingContext();
              ((DataTable) comboBox.DataSource).DefaultView.RowFilter = "Code <> '29'";
            }
            else
            {
              ComboBox comboBox = control as ComboBox;
              comboBox.BindingContext = new BindingContext();
              comboBox.DataSource = (object) new DataView(this._dtFreight);
              comboBox.DisplayMember = "Description";
              comboBox.ValueMember = "Code";
            }
          }
        }
      }
    }

    private void FillCombo(Control c)
    {
      ComboBox cb = c as ComboBox;
      cb.BindingContext = new BindingContext();
      if (!this.IsFreight)
      {
        cb.DataSource = !this.IsSmartPost ? (object) this._dt : (object) this._dtSmartPost;
        cb.DisplayMember = "CTS_Tag_Desc";
        cb.ValueMember = "CTS_Tag_Id";
        cb.SelectedIndex = -1;
      }
      else
      {
        if (this._dtFreight == null)
          this._dtFreight = Utility.GetDataTable(Utility.ListTypes.FreightDocTab);
        Utility.SetDisplayAndValue(cb, this._dtFreight, "Description", "Code", false, false);
        cb.SelectedIndex = 0;
      }
    }

    private void ucDocTab_Load(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.edtDocTabLabel1 = new FdxMaskedEdit();
      this.edtDocTabLabel2 = new FdxMaskedEdit();
      this.edtDocTabLabel3 = new FdxMaskedEdit();
      this.edtDocTabLabel4 = new FdxMaskedEdit();
      this.edtDocTabLabel5 = new FdxMaskedEdit();
      this.edtDocTabLabel6 = new FdxMaskedEdit();
      this.edtDocTabLabel7 = new FdxMaskedEdit();
      this.edtDocTabLabel8 = new FdxMaskedEdit();
      this.edtDocTabLabel9 = new FdxMaskedEdit();
      this.chkPrintTotalsDocTab = new CheckBox();
      this.cboDocTabData1 = new ComboBoxEx();
      this.cboDocTabData2 = new ComboBoxEx();
      this.cboDocTabData3 = new ComboBoxEx();
      this.cboDocTabData4 = new ComboBoxEx();
      this.cboDocTabData5 = new ComboBoxEx();
      this.cboDocTabData6 = new ComboBoxEx();
      this.cboDocTabData7 = new ComboBoxEx();
      this.cboDocTabData8 = new ComboBoxEx();
      this.cboDocTabData9 = new ComboBoxEx();
      this.cboDocTabData10 = new ComboBoxEx();
      this.edtDocTabLabel10 = new FdxMaskedEdit();
      this.cboDocTabData11 = new ComboBoxEx();
      this.edtDocTabLabel11 = new FdxMaskedEdit();
      this.cboDocTabData12 = new ComboBoxEx();
      this.edtDocTabLabel12 = new FdxMaskedEdit();
      this.cboDocTabData13 = new ComboBoxEx();
      this.edtDocTabLabel13 = new FdxMaskedEdit();
      this.cboDocTabData14 = new ComboBoxEx();
      this.edtDocTabLabel14 = new FdxMaskedEdit();
      this.focusExtender1 = new FocusExtender();
      this.SuspendLayout();
      this.helpProvider1.HelpNamespace = "C:\\Program Files\\FedEx\\ShipManager\\Bin\\Fdx95_ENG.chm";
      this.edtDocTabLabel1.Allow = "";
      this.edtDocTabLabel1.Disallow = "";
      this.edtDocTabLabel1.eMask = eMasks.maskCustom;
      this.edtDocTabLabel1.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel1, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel1, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel1, "1046");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel1, HelpNavigator.TopicId);
      this.edtDocTabLabel1.Location = new Point(12, 35);
      this.edtDocTabLabel1.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel1.Name = "edtDocTabLabel1";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel1, true);
      this.edtDocTabLabel1.Size = new Size(99, 20);
      this.edtDocTabLabel1.TabIndex = 1;
      this.edtDocTabLabel2.Allow = "";
      this.edtDocTabLabel2.Disallow = "";
      this.edtDocTabLabel2.eMask = eMasks.maskCustom;
      this.edtDocTabLabel2.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel2, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel2, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel2, "1047");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel2, HelpNavigator.TopicId);
      this.edtDocTabLabel2.Location = new Point(12, 61);
      this.edtDocTabLabel2.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel2.Name = "edtDocTabLabel2";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel2, true);
      this.edtDocTabLabel2.Size = new Size(99, 20);
      this.edtDocTabLabel2.TabIndex = 2;
      this.edtDocTabLabel3.Allow = "";
      this.edtDocTabLabel3.Disallow = "";
      this.edtDocTabLabel3.eMask = eMasks.maskCustom;
      this.edtDocTabLabel3.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel3, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel3, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel3, "1048");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel3, HelpNavigator.TopicId);
      this.edtDocTabLabel3.Location = new Point(12, 87);
      this.edtDocTabLabel3.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel3.Name = "edtDocTabLabel3";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel3, true);
      this.edtDocTabLabel3.Size = new Size(99, 20);
      this.edtDocTabLabel3.TabIndex = 3;
      this.edtDocTabLabel4.Allow = "";
      this.edtDocTabLabel4.Disallow = "";
      this.edtDocTabLabel4.eMask = eMasks.maskCustom;
      this.edtDocTabLabel4.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel4, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel4, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel4, "1049");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel4, HelpNavigator.TopicId);
      this.edtDocTabLabel4.Location = new Point(12, 113);
      this.edtDocTabLabel4.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel4.Name = "edtDocTabLabel4";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel4, true);
      this.edtDocTabLabel4.Size = new Size(99, 20);
      this.edtDocTabLabel4.TabIndex = 4;
      this.edtDocTabLabel5.Allow = "";
      this.edtDocTabLabel5.Disallow = "";
      this.edtDocTabLabel5.eMask = eMasks.maskCustom;
      this.edtDocTabLabel5.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel5, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel5, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel5, "1050");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel5, HelpNavigator.TopicId);
      this.edtDocTabLabel5.Location = new Point(322, 35);
      this.edtDocTabLabel5.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel5.Name = "edtDocTabLabel5";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel5, true);
      this.edtDocTabLabel5.Size = new Size(99, 20);
      this.edtDocTabLabel5.TabIndex = 9;
      this.edtDocTabLabel6.Allow = "";
      this.edtDocTabLabel6.Disallow = "";
      this.edtDocTabLabel6.eMask = eMasks.maskCustom;
      this.edtDocTabLabel6.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel6, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel6, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel6, "1051");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel6, HelpNavigator.TopicId);
      this.edtDocTabLabel6.Location = new Point(322, 61);
      this.edtDocTabLabel6.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel6.Name = "edtDocTabLabel6";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel6, true);
      this.edtDocTabLabel6.Size = new Size(99, 20);
      this.edtDocTabLabel6.TabIndex = 10;
      this.edtDocTabLabel7.Allow = "";
      this.edtDocTabLabel7.Disallow = "";
      this.edtDocTabLabel7.eMask = eMasks.maskCustom;
      this.edtDocTabLabel7.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel7, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel7, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel7, "1052");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel7, HelpNavigator.TopicId);
      this.edtDocTabLabel7.Location = new Point(322, 87);
      this.edtDocTabLabel7.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel7.Name = "edtDocTabLabel7";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel7, true);
      this.edtDocTabLabel7.Size = new Size(99, 20);
      this.edtDocTabLabel7.TabIndex = 11;
      this.edtDocTabLabel8.Allow = "";
      this.edtDocTabLabel8.Disallow = "";
      this.edtDocTabLabel8.eMask = eMasks.maskCustom;
      this.edtDocTabLabel8.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel8, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel8, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel8, "1053");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel8, HelpNavigator.TopicId);
      this.edtDocTabLabel8.Location = new Point(322, 113);
      this.edtDocTabLabel8.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel8.Name = "edtDocTabLabel8";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel8, true);
      this.edtDocTabLabel8.Size = new Size(99, 20);
      this.edtDocTabLabel8.TabIndex = 12;
      this.edtDocTabLabel9.Allow = "";
      this.edtDocTabLabel9.Disallow = "";
      this.edtDocTabLabel9.eMask = eMasks.maskCustom;
      this.edtDocTabLabel9.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel9, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel9, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel9, "1054");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel9, HelpNavigator.TopicId);
      this.edtDocTabLabel9.Location = new Point(632, 35);
      this.edtDocTabLabel9.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel9.Name = "edtDocTabLabel9";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel9, true);
      this.edtDocTabLabel9.Size = new Size(99, 20);
      this.edtDocTabLabel9.TabIndex = 17;
      this.chkPrintTotalsDocTab.AutoSize = true;
      this.helpProvider1.SetHelpKeyword((Control) this.chkPrintTotalsDocTab, "1055");
      this.helpProvider1.SetHelpNavigator((Control) this.chkPrintTotalsDocTab, HelpNavigator.TopicId);
      this.chkPrintTotalsDocTab.Location = new Point(12, 12);
      this.chkPrintTotalsDocTab.Name = "chkPrintTotalsDocTab";
      this.helpProvider1.SetShowHelp((Control) this.chkPrintTotalsDocTab, true);
      this.chkPrintTotalsDocTab.Size = new Size(114, 17);
      this.chkPrintTotalsDocTab.TabIndex = 0;
      this.chkPrintTotalsDocTab.Text = "&Print totals doc-tab";
      this.chkPrintTotalsDocTab.UseVisualStyleBackColor = true;
      this.cboDocTabData1.DisplayMemberQ = "";
      this.cboDocTabData1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData1.DropDownWidth = 310;
      this.cboDocTabData1.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData1, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData1, SystemColors.WindowText);
      this.cboDocTabData1.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData1, "1056");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData1, HelpNavigator.TopicId);
      this.cboDocTabData1.Location = new Point(114, 34);
      this.cboDocTabData1.MaxDropDownItems = 15;
      this.cboDocTabData1.Name = "cboDocTabData1";
      this.cboDocTabData1.Prompt = "Select an item...";
      this.cboDocTabData1.SelectedIndexQ = -1;
      this.cboDocTabData1.SelectedItemQ = (object) null;
      this.cboDocTabData1.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData1, true);
      this.cboDocTabData1.Size = new Size(193, 21);
      this.cboDocTabData1.TabIndex = 5;
      this.cboDocTabData1.ValueMemberQ = "";
      this.cboDocTabData2.DisplayMemberQ = "";
      this.cboDocTabData2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData2.DropDownWidth = 310;
      this.cboDocTabData2.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData2, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData2, SystemColors.WindowText);
      this.cboDocTabData2.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData2, "1057");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData2, HelpNavigator.TopicId);
      this.cboDocTabData2.Location = new Point(114, 60);
      this.cboDocTabData2.MaxDropDownItems = 15;
      this.cboDocTabData2.Name = "cboDocTabData2";
      this.cboDocTabData2.Prompt = "Select an item...";
      this.cboDocTabData2.SelectedIndexQ = -1;
      this.cboDocTabData2.SelectedItemQ = (object) null;
      this.cboDocTabData2.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData2, true);
      this.cboDocTabData2.Size = new Size(193, 21);
      this.cboDocTabData2.TabIndex = 6;
      this.cboDocTabData2.ValueMemberQ = "";
      this.cboDocTabData3.DisplayMemberQ = "";
      this.cboDocTabData3.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData3.DropDownWidth = 310;
      this.cboDocTabData3.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData3, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData3, SystemColors.WindowText);
      this.cboDocTabData3.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData3, "1058");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData3, HelpNavigator.TopicId);
      this.cboDocTabData3.Location = new Point(114, 86);
      this.cboDocTabData3.MaxDropDownItems = 15;
      this.cboDocTabData3.Name = "cboDocTabData3";
      this.cboDocTabData3.Prompt = "Select an item...";
      this.cboDocTabData3.SelectedIndexQ = -1;
      this.cboDocTabData3.SelectedItemQ = (object) null;
      this.cboDocTabData3.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData3, true);
      this.cboDocTabData3.Size = new Size(193, 21);
      this.cboDocTabData3.TabIndex = 7;
      this.cboDocTabData3.ValueMemberQ = "";
      this.cboDocTabData4.DisplayMemberQ = "";
      this.cboDocTabData4.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData4.DropDownWidth = 310;
      this.cboDocTabData4.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData4, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData4, SystemColors.WindowText);
      this.cboDocTabData4.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData4, "1059");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData4, HelpNavigator.TopicId);
      this.cboDocTabData4.Location = new Point(114, 112);
      this.cboDocTabData4.MaxDropDownItems = 15;
      this.cboDocTabData4.Name = "cboDocTabData4";
      this.cboDocTabData4.Prompt = "Select an item...";
      this.cboDocTabData4.SelectedIndexQ = -1;
      this.cboDocTabData4.SelectedItemQ = (object) null;
      this.cboDocTabData4.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData4, true);
      this.cboDocTabData4.Size = new Size(193, 21);
      this.cboDocTabData4.TabIndex = 8;
      this.cboDocTabData4.ValueMemberQ = "";
      this.cboDocTabData5.DisplayMemberQ = "";
      this.cboDocTabData5.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData5.DropDownWidth = 310;
      this.cboDocTabData5.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData5, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData5, SystemColors.WindowText);
      this.cboDocTabData5.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData5, "1060");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData5, HelpNavigator.TopicId);
      this.cboDocTabData5.Location = new Point(424, 34);
      this.cboDocTabData5.MaxDropDownItems = 15;
      this.cboDocTabData5.Name = "cboDocTabData5";
      this.cboDocTabData5.Prompt = "Select an item...";
      this.cboDocTabData5.SelectedIndexQ = -1;
      this.cboDocTabData5.SelectedItemQ = (object) null;
      this.cboDocTabData5.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData5, true);
      this.cboDocTabData5.Size = new Size(193, 21);
      this.cboDocTabData5.TabIndex = 13;
      this.cboDocTabData5.ValueMemberQ = "";
      this.cboDocTabData6.DisplayMemberQ = "";
      this.cboDocTabData6.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData6.DropDownWidth = 310;
      this.cboDocTabData6.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData6, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData6, SystemColors.WindowText);
      this.cboDocTabData6.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData6, "1061");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData6, HelpNavigator.TopicId);
      this.cboDocTabData6.Location = new Point(424, 60);
      this.cboDocTabData6.MaxDropDownItems = 15;
      this.cboDocTabData6.Name = "cboDocTabData6";
      this.cboDocTabData6.Prompt = "Select an item...";
      this.cboDocTabData6.SelectedIndexQ = -1;
      this.cboDocTabData6.SelectedItemQ = (object) null;
      this.cboDocTabData6.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData6, true);
      this.cboDocTabData6.Size = new Size(193, 21);
      this.cboDocTabData6.TabIndex = 14;
      this.cboDocTabData6.ValueMemberQ = "";
      this.cboDocTabData7.DisplayMemberQ = "";
      this.cboDocTabData7.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData7.DropDownWidth = 310;
      this.cboDocTabData7.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData7, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData7, SystemColors.WindowText);
      this.cboDocTabData7.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData7, "1062");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData7, HelpNavigator.TopicId);
      this.cboDocTabData7.Location = new Point(424, 86);
      this.cboDocTabData7.MaxDropDownItems = 15;
      this.cboDocTabData7.Name = "cboDocTabData7";
      this.cboDocTabData7.Prompt = "Select an item...";
      this.cboDocTabData7.SelectedIndexQ = -1;
      this.cboDocTabData7.SelectedItemQ = (object) null;
      this.cboDocTabData7.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData7, true);
      this.cboDocTabData7.Size = new Size(193, 21);
      this.cboDocTabData7.TabIndex = 15;
      this.cboDocTabData7.ValueMemberQ = "";
      this.cboDocTabData8.DisplayMemberQ = "";
      this.cboDocTabData8.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData8.DropDownWidth = 310;
      this.cboDocTabData8.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData8, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData8, SystemColors.WindowText);
      this.cboDocTabData8.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData8, "1063");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData8, HelpNavigator.TopicId);
      this.cboDocTabData8.Location = new Point(424, 112);
      this.cboDocTabData8.MaxDropDownItems = 15;
      this.cboDocTabData8.Name = "cboDocTabData8";
      this.cboDocTabData8.Prompt = "Select an item...";
      this.cboDocTabData8.SelectedIndexQ = -1;
      this.cboDocTabData8.SelectedItemQ = (object) null;
      this.cboDocTabData8.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData8, true);
      this.cboDocTabData8.Size = new Size(193, 21);
      this.cboDocTabData8.TabIndex = 16;
      this.cboDocTabData8.ValueMemberQ = "";
      this.cboDocTabData9.DisplayMemberQ = "";
      this.cboDocTabData9.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData9.DropDownWidth = 310;
      this.cboDocTabData9.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData9, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData9, SystemColors.WindowText);
      this.cboDocTabData9.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData9, "1064");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData9, HelpNavigator.TopicId);
      this.cboDocTabData9.Location = new Point(734, 34);
      this.cboDocTabData9.MaxDropDownItems = 15;
      this.cboDocTabData9.Name = "cboDocTabData9";
      this.cboDocTabData9.Prompt = "Select an item...";
      this.cboDocTabData9.SelectedIndexQ = -1;
      this.cboDocTabData9.SelectedItemQ = (object) null;
      this.cboDocTabData9.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData9, true);
      this.cboDocTabData9.Size = new Size(193, 21);
      this.cboDocTabData9.TabIndex = 21;
      this.cboDocTabData9.ValueMemberQ = "";
      this.cboDocTabData10.DisplayMemberQ = "";
      this.cboDocTabData10.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData10.DropDownWidth = 310;
      this.cboDocTabData10.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData10, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData10, SystemColors.WindowText);
      this.cboDocTabData10.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData10, "1065");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData10, HelpNavigator.TopicId);
      this.cboDocTabData10.Location = new Point(734, 60);
      this.cboDocTabData10.MaxDropDownItems = 15;
      this.cboDocTabData10.Name = "cboDocTabData10";
      this.cboDocTabData10.Prompt = "Select an item...";
      this.cboDocTabData10.SelectedIndexQ = -1;
      this.cboDocTabData10.SelectedItemQ = (object) null;
      this.cboDocTabData10.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData10, true);
      this.cboDocTabData10.Size = new Size(193, 21);
      this.cboDocTabData10.TabIndex = 22;
      this.cboDocTabData10.ValueMemberQ = "";
      this.edtDocTabLabel10.Allow = "";
      this.edtDocTabLabel10.Disallow = "";
      this.edtDocTabLabel10.eMask = eMasks.maskCustom;
      this.edtDocTabLabel10.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel10, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel10, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel10, "1066");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel10, HelpNavigator.TopicId);
      this.edtDocTabLabel10.Location = new Point(632, 61);
      this.edtDocTabLabel10.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel10.Name = "edtDocTabLabel10";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel10, true);
      this.edtDocTabLabel10.Size = new Size(99, 20);
      this.edtDocTabLabel10.TabIndex = 18;
      this.cboDocTabData11.DisplayMemberQ = "";
      this.cboDocTabData11.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData11.DropDownWidth = 310;
      this.cboDocTabData11.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData11, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData11, SystemColors.WindowText);
      this.cboDocTabData11.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData11, "1067");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData11, HelpNavigator.TopicId);
      this.cboDocTabData11.Location = new Point(734, 86);
      this.cboDocTabData11.MaxDropDownItems = 15;
      this.cboDocTabData11.Name = "cboDocTabData11";
      this.cboDocTabData11.Prompt = "Select an item...";
      this.cboDocTabData11.SelectedIndexQ = -1;
      this.cboDocTabData11.SelectedItemQ = (object) null;
      this.cboDocTabData11.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData11, true);
      this.cboDocTabData11.Size = new Size(193, 21);
      this.cboDocTabData11.TabIndex = 23;
      this.cboDocTabData11.ValueMemberQ = "";
      this.edtDocTabLabel11.Allow = "";
      this.edtDocTabLabel11.Disallow = "";
      this.edtDocTabLabel11.eMask = eMasks.maskCustom;
      this.edtDocTabLabel11.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel11, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel11, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel11, "1068");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel11, HelpNavigator.TopicId);
      this.edtDocTabLabel11.Location = new Point(632, 87);
      this.edtDocTabLabel11.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel11.Name = "edtDocTabLabel11";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel11, true);
      this.edtDocTabLabel11.Size = new Size(99, 20);
      this.edtDocTabLabel11.TabIndex = 19;
      this.cboDocTabData12.DisplayMemberQ = "";
      this.cboDocTabData12.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData12.DropDownWidth = 310;
      this.cboDocTabData12.DroppedDownQ = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData12, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData12, SystemColors.WindowText);
      this.cboDocTabData12.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData12, "1069");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData12, HelpNavigator.TopicId);
      this.cboDocTabData12.Location = new Point(734, 112);
      this.cboDocTabData12.MaxDropDownItems = 15;
      this.cboDocTabData12.Name = "cboDocTabData12";
      this.cboDocTabData12.Prompt = "Select an item...";
      this.cboDocTabData12.SelectedIndexQ = -1;
      this.cboDocTabData12.SelectedItemQ = (object) null;
      this.cboDocTabData12.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData12, true);
      this.cboDocTabData12.Size = new Size(193, 21);
      this.cboDocTabData12.TabIndex = 24;
      this.cboDocTabData12.ValueMemberQ = "";
      this.edtDocTabLabel12.Allow = "";
      this.edtDocTabLabel12.Disallow = "";
      this.edtDocTabLabel12.eMask = eMasks.maskCustom;
      this.edtDocTabLabel12.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel12, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel12, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel12, "1070");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel12, HelpNavigator.TopicId);
      this.edtDocTabLabel12.Location = new Point(632, 113);
      this.edtDocTabLabel12.Mask = "IIIIIIIII~!\"";
      this.edtDocTabLabel12.Name = "edtDocTabLabel12";
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel12, true);
      this.edtDocTabLabel12.Size = new Size(99, 20);
      this.edtDocTabLabel12.TabIndex = 20;
      this.cboDocTabData13.DisplayMemberQ = "";
      this.cboDocTabData13.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData13.DropDownWidth = 310;
      this.cboDocTabData13.DroppedDownQ = false;
      this.cboDocTabData13.Enabled = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData13, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData13, SystemColors.WindowText);
      this.cboDocTabData13.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData13, "1071");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData13, HelpNavigator.TopicId);
      this.cboDocTabData13.Location = new Point(424, 138);
      this.cboDocTabData13.MaxDropDownItems = 15;
      this.cboDocTabData13.Name = "cboDocTabData13";
      this.cboDocTabData13.Prompt = "Select an item...";
      this.cboDocTabData13.SelectedIndexQ = -1;
      this.cboDocTabData13.SelectedItemQ = (object) null;
      this.cboDocTabData13.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData13, true);
      this.cboDocTabData13.Size = new Size(193, 21);
      this.cboDocTabData13.TabIndex = 27;
      this.cboDocTabData13.ValueMemberQ = "";
      this.edtDocTabLabel13.Allow = "";
      this.edtDocTabLabel13.Disallow = "";
      this.edtDocTabLabel13.eMask = eMasks.maskCustom;
      this.edtDocTabLabel13.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel13, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel13, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel13, "1072");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel13, HelpNavigator.TopicId);
      this.edtDocTabLabel13.Location = new Point(322, 139);
      this.edtDocTabLabel13.Mask = "";
      this.edtDocTabLabel13.Name = "edtDocTabLabel13";
      this.edtDocTabLabel13.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel13, true);
      this.edtDocTabLabel13.Size = new Size(99, 20);
      this.edtDocTabLabel13.TabIndex = 25;
      this.cboDocTabData14.DisplayMemberQ = "";
      this.cboDocTabData14.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDocTabData14.DropDownWidth = 310;
      this.cboDocTabData14.DroppedDownQ = false;
      this.cboDocTabData14.Enabled = false;
      this.focusExtender1.SetFocusBackColor((Control) this.cboDocTabData14, SystemColors.Window);
      this.focusExtender1.SetFocusForeColor((Control) this.cboDocTabData14, SystemColors.WindowText);
      this.cboDocTabData14.FormattingEnabled = true;
      this.helpProvider1.SetHelpKeyword((Control) this.cboDocTabData14, "1073");
      this.helpProvider1.SetHelpNavigator((Control) this.cboDocTabData14, HelpNavigator.TopicId);
      this.cboDocTabData14.Location = new Point(424, 164);
      this.cboDocTabData14.MaxDropDownItems = 15;
      this.cboDocTabData14.Name = "cboDocTabData14";
      this.cboDocTabData14.Prompt = "Select an item...";
      this.cboDocTabData14.SelectedIndexQ = -1;
      this.cboDocTabData14.SelectedItemQ = (object) null;
      this.cboDocTabData14.SelectedValueQ = (object) null;
      this.helpProvider1.SetShowHelp((Control) this.cboDocTabData14, true);
      this.cboDocTabData14.Size = new Size(193, 21);
      this.cboDocTabData14.TabIndex = 28;
      this.cboDocTabData14.ValueMemberQ = "";
      this.edtDocTabLabel14.Allow = "";
      this.edtDocTabLabel14.Disallow = "";
      this.edtDocTabLabel14.eMask = eMasks.maskCustom;
      this.edtDocTabLabel14.FillFrom = LeftRightAlignment.Left;
      this.focusExtender1.SetFocusBackColor((Control) this.edtDocTabLabel14, SystemColors.Control);
      this.focusExtender1.SetFocusForeColor((Control) this.edtDocTabLabel14, SystemColors.WindowText);
      this.helpProvider1.SetHelpKeyword((Control) this.edtDocTabLabel14, "1074");
      this.helpProvider1.SetHelpNavigator((Control) this.edtDocTabLabel14, HelpNavigator.TopicId);
      this.edtDocTabLabel14.Location = new Point(322, 165);
      this.edtDocTabLabel14.Mask = "";
      this.edtDocTabLabel14.Name = "edtDocTabLabel14";
      this.edtDocTabLabel14.ReadOnly = true;
      this.helpProvider1.SetShowHelp((Control) this.edtDocTabLabel14, true);
      this.edtDocTabLabel14.Size = new Size(99, 20);
      this.edtDocTabLabel14.TabIndex = 26;
      this.focusExtender1.FocusBackColor = Color.Blue;
      this.focusExtender1.FocusForeColor = Color.White;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.cboDocTabData14);
      this.Controls.Add((Control) this.edtDocTabLabel14);
      this.Controls.Add((Control) this.cboDocTabData13);
      this.Controls.Add((Control) this.edtDocTabLabel13);
      this.Controls.Add((Control) this.cboDocTabData12);
      this.Controls.Add((Control) this.edtDocTabLabel12);
      this.Controls.Add((Control) this.cboDocTabData11);
      this.Controls.Add((Control) this.edtDocTabLabel11);
      this.Controls.Add((Control) this.cboDocTabData10);
      this.Controls.Add((Control) this.edtDocTabLabel10);
      this.Controls.Add((Control) this.cboDocTabData9);
      this.Controls.Add((Control) this.cboDocTabData8);
      this.Controls.Add((Control) this.cboDocTabData4);
      this.Controls.Add((Control) this.cboDocTabData7);
      this.Controls.Add((Control) this.edtDocTabLabel5);
      this.Controls.Add((Control) this.cboDocTabData6);
      this.Controls.Add((Control) this.edtDocTabLabel4);
      this.Controls.Add((Control) this.cboDocTabData5);
      this.Controls.Add((Control) this.edtDocTabLabel6);
      this.Controls.Add((Control) this.edtDocTabLabel3);
      this.Controls.Add((Control) this.cboDocTabData3);
      this.Controls.Add((Control) this.edtDocTabLabel7);
      this.Controls.Add((Control) this.cboDocTabData2);
      this.Controls.Add((Control) this.edtDocTabLabel2);
      this.Controls.Add((Control) this.cboDocTabData1);
      this.Controls.Add((Control) this.edtDocTabLabel8);
      this.Controls.Add((Control) this.chkPrintTotalsDocTab);
      this.Controls.Add((Control) this.edtDocTabLabel1);
      this.Controls.Add((Control) this.edtDocTabLabel9);
      this.Name = nameof (ucDocTab);
      this.Size = new Size(939, 194);
      this.Load += new EventHandler(this.ucDocTab_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
