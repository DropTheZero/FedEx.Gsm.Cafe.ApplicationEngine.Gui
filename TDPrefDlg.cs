// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.TDPrefDlg
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Preferences;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Shared;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UserControls;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class TDPrefDlg : HelpFormBase
  {
    private TDShipDefl _preferenceObject;
    private Utility.FormOperation _eOperation;
    private IContainer components;
    private Button btnOk;
    private Button btnCancel;
    private Profile_Header TDProfileHeader;
    private TabControlEx tabControlTDPrefs;
    private TabPage tabPageTDFieldPrefs;
    private TabPage tabPageTDExpressOtherPrefs;
    private TabPage tabPageTDGroundOtherPrefs;
    private TDFieldPreferences TDFieldPreferences;
    private TDOtherPreferences TDOtherExpressPreferences;
    private TDOtherPreferences TDOtherGroundPreferences;
    private TabPage tabPageTDShipAlertPrefs;
    private TDShipAlertPreferences TDShipAlertPreferences;
    private TabPage tabPageCustomsDocuments;
    protected ColorGroupBox gbxLetterhead;
    private Button btnUploadImages;
    protected Label label2;

    public TDPrefDlg(TDShipDefl tdPrefs, Utility.FormOperation eOperation)
    {
      this.InitializeComponent();
      if (this.DesignMode)
        return;
      this._preferenceObject = tdPrefs;
      this._eOperation = eOperation;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.TDProfileHeader.ProfileCode.Trim()))
      {
        int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(13547), Error.ErrorType.Failure);
        this.DialogResult = DialogResult.None;
      }
      else
      {
        this._preferenceObject.ProfileCode = this.TDProfileHeader.ProfileCode;
        this._preferenceObject.Description = this.TDProfileHeader.ProfileDescription;
        if (!this.TDFieldPreferences.OkToClose())
          this.DialogResult = DialogResult.None;
        else if (!this.TDShipAlertPreferences.OkToClose())
        {
          this.DialogResult = DialogResult.None;
        }
        else
        {
          this.TDOtherExpressPreferences.ScreenToObject();
          if (GuiData.CurrentAccount.IsGroundEnabled)
            this.TDOtherGroundPreferences.ScreenToObject();
          if (this._preferenceObject.ExpressHandlingCharge.VariableChgInd && this._preferenceObject.ExpressHandlingCharge.ComputationInd == HandlingCharge.HANDLINGCOMPTYPE.FLAT_AMOUNT || this._preferenceObject.GroundHandlingCharge.VariableChgInd && this._preferenceObject.GroundHandlingCharge.ComputationInd == HandlingCharge.HANDLINGCOMPTYPE.FLAT_AMOUNT)
          {
            int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(13838), Error.ErrorType.Failure);
            this.DialogResult = DialogResult.None;
          }
          else if (this._preferenceObject.FieldPrefs[12].Behavior == ShipDefl.Behavior.Skip && this._preferenceObject.RequireReference)
          {
            int num = (int) Utility.DisplayError(GuiData.Languafier.TranslateError(9524), Error.ErrorType.Failure);
            this.TDOtherExpressPreferences.chkRequireReferences.Focus();
            this.DialogResult = DialogResult.None;
          }
          else
            this.DialogResult = DialogResult.OK;
        }
      }
    }

    private void TDPrefDlg_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.TDProfileHeader.ProfileCode = this._preferenceObject.ProfileCode;
      this.TDProfileHeader.ProfileDescription = this._preferenceObject.Description;
      this.TDProfileHeader.EnableProfileCode = this._eOperation == Utility.FormOperation.Add || this._eOperation == Utility.FormOperation.AddByDup;
      this.TDProfileHeader.EnableProfileDesc = this._preferenceObject.ProfileCode != "DEFAULT";
      this.TDFieldPreferences.PreferenceObject = (ShipDefl) this._preferenceObject;
      this.TDOtherExpressPreferences.InitOtherPrefs(this._preferenceObject, Shipment.CarrierType.Express);
      this.TDShipAlertPreferences.PreferenceObject = (ShipDefl) this._preferenceObject;
      if (GuiData.CurrentAccount.IsGroundEnabled)
        this.TDOtherGroundPreferences.InitOtherPrefs(this._preferenceObject, Shipment.CarrierType.Ground);
      else
        this.tabControlTDPrefs.HideTabPage(this.tabPageTDGroundOtherPrefs);
      this.ObjectToScreen();
    }

    private void ObjectToScreen()
    {
      this.TDFieldPreferences.ObjectToScreen();
      this.TDOtherExpressPreferences.ObjectToScreen();
      if (!GuiData.CurrentAccount.IsGroundEnabled)
        return;
      this.TDOtherGroundPreferences.ObjectToScreen();
    }

    private void btnUploadImages_Click(object sender, EventArgs e)
    {
      UploadImagesDlg uploadImagesDlg = new UploadImagesDlg();
      int printType = (int) this._preferenceObject.PrintType;
      uploadImagesDlg.ApplyLetterheadToCountry = this._preferenceObject.PrintType != ImagePrintType.NoneSelected ? (object) (int) this._preferenceObject.PrintType : (object) 1;
      if (this._preferenceObject.CompanyLogo != null)
        uploadImagesDlg.LetterheadImage = this._preferenceObject.CompanyLogo;
      if (this._preferenceObject.SignatureImage != null)
        uploadImagesDlg.SignatureImage = this._preferenceObject.SignatureImage;
      uploadImagesDlg.AlwaysApplySignature = this._preferenceObject.AlwaysApplySignature;
      if (uploadImagesDlg.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        this._preferenceObject.AlwaysApplySignature = uploadImagesDlg.AlwaysApplySignature;
        this._preferenceObject.PrintType = (ImagePrintType) Enum.Parse(typeof (ImagePrintType), uploadImagesDlg.ApplyLetterheadToCountry.ToString());
        this._preferenceObject.CompanyLogo = uploadImagesDlg.LetterheadImage ?? new BinaryDocument(string.Empty, string.Empty, new byte[0]);
        this._preferenceObject.SignatureImage = uploadImagesDlg.SignatureImage ?? new BinaryDocument(string.Empty, string.Empty, new byte[0]);
      }
      catch (Exception ex)
      {
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TDPrefDlg));
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.tabControlTDPrefs = new TabControlEx();
      this.tabPageTDFieldPrefs = new TabPage();
      this.TDFieldPreferences = new TDFieldPreferences();
      this.tabPageTDExpressOtherPrefs = new TabPage();
      this.TDOtherExpressPreferences = new TDOtherPreferences();
      this.tabPageTDShipAlertPrefs = new TabPage();
      this.TDShipAlertPreferences = new TDShipAlertPreferences();
      this.tabPageTDGroundOtherPrefs = new TabPage();
      this.TDOtherGroundPreferences = new TDOtherPreferences();
      this.tabPageCustomsDocuments = new TabPage();
      this.gbxLetterhead = new ColorGroupBox();
      this.btnUploadImages = new Button();
      this.label2 = new Label();
      this.TDProfileHeader = new Profile_Header();
      this.tabControlTDPrefs.SuspendLayout();
      this.tabPageTDFieldPrefs.SuspendLayout();
      this.tabPageTDExpressOtherPrefs.SuspendLayout();
      this.tabPageTDShipAlertPrefs.SuspendLayout();
      this.tabPageTDGroundOtherPrefs.SuspendLayout();
      this.tabPageCustomsDocuments.SuspendLayout();
      this.gbxLetterhead.SuspendLayout();
      this.SuspendLayout();
      this.btnOk.DialogResult = DialogResult.OK;
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.Name = "btnOk";
      this.helpProvider1.SetShowHelp((Control) this.btnOk, (bool) componentResourceManager.GetObject("btnOk.ShowHelp"));
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.btnCancel.DialogResult = DialogResult.Cancel;
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.Name = "btnCancel";
      this.helpProvider1.SetShowHelp((Control) this.btnCancel, (bool) componentResourceManager.GetObject("btnCancel.ShowHelp"));
      this.btnCancel.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.tabControlTDPrefs, "tabControlTDPrefs");
      this.tabControlTDPrefs.Controls.Add((Control) this.tabPageTDFieldPrefs);
      this.tabControlTDPrefs.Controls.Add((Control) this.tabPageTDExpressOtherPrefs);
      this.tabControlTDPrefs.Controls.Add((Control) this.tabPageTDShipAlertPrefs);
      this.tabControlTDPrefs.Controls.Add((Control) this.tabPageTDGroundOtherPrefs);
      this.tabControlTDPrefs.Controls.Add((Control) this.tabPageCustomsDocuments);
      this.tabControlTDPrefs.DrawMode = TabDrawMode.OwnerDrawFixed;
      this.helpProvider1.SetHelpKeyword((Control) this.tabControlTDPrefs, componentResourceManager.GetString("tabControlTDPrefs.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.tabControlTDPrefs, (HelpNavigator) componentResourceManager.GetObject("tabControlTDPrefs.HelpNavigator"));
      this.tabControlTDPrefs.MnemonicEnabled = true;
      this.tabControlTDPrefs.Multiline = true;
      this.tabControlTDPrefs.Name = "tabControlTDPrefs";
      this.tabControlTDPrefs.SelectedIndex = 0;
      this.helpProvider1.SetShowHelp((Control) this.tabControlTDPrefs, (bool) componentResourceManager.GetObject("tabControlTDPrefs.ShowHelp"));
      this.tabControlTDPrefs.UseIndexAsMnemonic = true;
      this.tabPageTDFieldPrefs.Controls.Add((Control) this.TDFieldPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageTDFieldPrefs, "tabPageTDFieldPrefs");
      this.tabPageTDFieldPrefs.Name = "tabPageTDFieldPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageTDFieldPrefs, (bool) componentResourceManager.GetObject("tabPageTDFieldPrefs.ShowHelp"));
      this.tabPageTDFieldPrefs.UseVisualStyleBackColor = true;
      this.TDFieldPreferences.BackColor = Color.White;
      this.TDFieldPreferences.CurrentPrefTypeIndex = -1;
      componentResourceManager.ApplyResources((object) this.TDFieldPreferences, "TDFieldPreferences");
      this.TDFieldPreferences.IsLoading = false;
      this.TDFieldPreferences.Name = "TDFieldPreferences";
      this.TDFieldPreferences.PreferenceObject = (ShipDefl) null;
      this.TDFieldPreferences.PrevPrefTypeIndex = -1;
      this.helpProvider1.SetShowHelp((Control) this.TDFieldPreferences, (bool) componentResourceManager.GetObject("TDFieldPreferences.ShowHelp"));
      this.tabPageTDExpressOtherPrefs.Controls.Add((Control) this.TDOtherExpressPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageTDExpressOtherPrefs, "tabPageTDExpressOtherPrefs");
      this.tabPageTDExpressOtherPrefs.Name = "tabPageTDExpressOtherPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageTDExpressOtherPrefs, (bool) componentResourceManager.GetObject("tabPageTDExpressOtherPrefs.ShowHelp"));
      this.tabPageTDExpressOtherPrefs.UseVisualStyleBackColor = true;
      this.TDOtherExpressPreferences.BackColor = Color.White;
      this.TDOtherExpressPreferences.Carrier = Shipment.CarrierType.NoCarrier;
      componentResourceManager.ApplyResources((object) this.TDOtherExpressPreferences, "TDOtherExpressPreferences");
      this.TDOtherExpressPreferences.Name = "TDOtherExpressPreferences";
      this.TDOtherExpressPreferences.PreferenceObject = (ShipDefl) null;
      this.helpProvider1.SetShowHelp((Control) this.TDOtherExpressPreferences, (bool) componentResourceManager.GetObject("TDOtherExpressPreferences.ShowHelp"));
      this.tabPageTDShipAlertPrefs.BackColor = Color.White;
      this.tabPageTDShipAlertPrefs.Controls.Add((Control) this.TDShipAlertPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageTDShipAlertPrefs, "tabPageTDShipAlertPrefs");
      this.tabPageTDShipAlertPrefs.Name = "tabPageTDShipAlertPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageTDShipAlertPrefs, (bool) componentResourceManager.GetObject("tabPageTDShipAlertPrefs.ShowHelp"));
      this.TDShipAlertPreferences.CurrentPrefTypeIndex = -1;
      componentResourceManager.ApplyResources((object) this.TDShipAlertPreferences, "TDShipAlertPreferences");
      this.TDShipAlertPreferences.IsLoading = false;
      this.TDShipAlertPreferences.Name = "TDShipAlertPreferences";
      this.TDShipAlertPreferences.PreferenceObject = (ShipDefl) null;
      this.TDShipAlertPreferences.PrevPrefTypeIndex = -1;
      this.helpProvider1.SetShowHelp((Control) this.TDShipAlertPreferences, (bool) componentResourceManager.GetObject("TDShipAlertPreferences.ShowHelp"));
      this.tabPageTDGroundOtherPrefs.Controls.Add((Control) this.TDOtherGroundPreferences);
      componentResourceManager.ApplyResources((object) this.tabPageTDGroundOtherPrefs, "tabPageTDGroundOtherPrefs");
      this.tabPageTDGroundOtherPrefs.Name = "tabPageTDGroundOtherPrefs";
      this.helpProvider1.SetShowHelp((Control) this.tabPageTDGroundOtherPrefs, (bool) componentResourceManager.GetObject("tabPageTDGroundOtherPrefs.ShowHelp"));
      this.tabPageTDGroundOtherPrefs.UseVisualStyleBackColor = true;
      this.TDOtherGroundPreferences.BackColor = Color.White;
      this.TDOtherGroundPreferences.Carrier = Shipment.CarrierType.NoCarrier;
      componentResourceManager.ApplyResources((object) this.TDOtherGroundPreferences, "TDOtherGroundPreferences");
      this.TDOtherGroundPreferences.Name = "TDOtherGroundPreferences";
      this.TDOtherGroundPreferences.PreferenceObject = (ShipDefl) null;
      this.helpProvider1.SetShowHelp((Control) this.TDOtherGroundPreferences, (bool) componentResourceManager.GetObject("TDOtherGroundPreferences.ShowHelp"));
      this.tabPageCustomsDocuments.BackColor = Color.White;
      this.tabPageCustomsDocuments.Controls.Add((Control) this.gbxLetterhead);
      componentResourceManager.ApplyResources((object) this.tabPageCustomsDocuments, "tabPageCustomsDocuments");
      this.tabPageCustomsDocuments.Name = "tabPageCustomsDocuments";
      this.helpProvider1.SetShowHelp((Control) this.tabPageCustomsDocuments, (bool) componentResourceManager.GetObject("tabPageCustomsDocuments.ShowHelp"));
      this.gbxLetterhead.BorderThickness = 1f;
      this.gbxLetterhead.Controls.Add((Control) this.btnUploadImages);
      this.gbxLetterhead.Controls.Add((Control) this.label2);
      this.gbxLetterhead.GroupTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      componentResourceManager.ApplyResources((object) this.gbxLetterhead, "gbxLetterhead");
      this.gbxLetterhead.Name = "gbxLetterhead";
      this.gbxLetterhead.RoundCorners = 5;
      this.helpProvider1.SetShowHelp((Control) this.gbxLetterhead, (bool) componentResourceManager.GetObject("gbxLetterhead.ShowHelp"));
      this.gbxLetterhead.TabStop = false;
      this.helpProvider1.SetHelpKeyword((Control) this.btnUploadImages, componentResourceManager.GetString("btnUploadImages.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this.btnUploadImages, (HelpNavigator) componentResourceManager.GetObject("btnUploadImages.HelpNavigator"));
      componentResourceManager.ApplyResources((object) this.btnUploadImages, "btnUploadImages");
      this.btnUploadImages.Name = "btnUploadImages";
      this.helpProvider1.SetShowHelp((Control) this.btnUploadImages, (bool) componentResourceManager.GetObject("btnUploadImages.ShowHelp"));
      this.btnUploadImages.UseVisualStyleBackColor = true;
      this.btnUploadImages.Click += new EventHandler(this.btnUploadImages_Click);
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      this.helpProvider1.SetShowHelp((Control) this.label2, (bool) componentResourceManager.GetObject("label2.ShowHelp"));
      componentResourceManager.ApplyResources((object) this.TDProfileHeader, "TDProfileHeader");
      this.TDProfileHeader.Name = "TDProfileHeader";
      this.TDProfileHeader.ProfileCode = "Profile Name";
      this.TDProfileHeader.ProfileDescription = "Profile Description";
      this.helpProvider1.SetShowHelp((Control) this.TDProfileHeader, (bool) componentResourceManager.GetObject("TDProfileHeader.ShowHelp"));
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.Controls.Add((Control) this.tabControlTDPrefs);
      this.Controls.Add((Control) this.TDProfileHeader);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOk);
      this.HelpButton = false;
      this.helpProvider1.SetHelpKeyword((Control) this, componentResourceManager.GetString("$this.HelpKeyword"));
      this.helpProvider1.SetHelpNavigator((Control) this, (HelpNavigator) componentResourceManager.GetObject("$this.HelpNavigator"));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (TDPrefDlg);
      this.helpProvider1.SetShowHelp((Control) this, (bool) componentResourceManager.GetObject("$this.ShowHelp"));
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Load += new EventHandler(this.TDPrefDlg_Load);
      this.tabControlTDPrefs.ResumeLayout(false);
      this.tabPageTDFieldPrefs.ResumeLayout(false);
      this.tabPageTDExpressOtherPrefs.ResumeLayout(false);
      this.tabPageTDShipAlertPrefs.ResumeLayout(false);
      this.tabPageTDGroundOtherPrefs.ResumeLayout(false);
      this.tabPageCustomsDocuments.ResumeLayout(false);
      this.gbxLetterhead.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
