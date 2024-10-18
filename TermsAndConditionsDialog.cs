// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.TermsAndConditionsDialog
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  public class TermsAndConditionsDialog : Form
  {
    private const int ERROR_INSUFFICIENT_BUFFER = 122;
    private int nextCharacter;
    private IContainer components;
    private TextBox txtTermsAndConditions;
    private Panel panel1;
    private CheckBox chkAccept;
    private Button btnCancel;
    private Button btnPrint;
    private Button btnOk;
    private PrintDocument printDocument1;

    public TermsAndConditionsDialog(string title, string terms, bool requireAccept)
    {
      this.InitializeComponent();
      this.Text = title;
      this.txtTermsAndConditions.Text = terms;
      this.chkAccept.Visible = requireAccept;
      this.btnOk.Enabled = !requireAccept;
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
      this.nextCharacter = 0;
      if (GuiData.CurrentAccount != null)
      {
        string printerName = Utility.GetPrinterName(FormSetting.FormSettingTypes.FORM_SETTINGS_REPORTS);
        if (string.Equals(printerName, GuiData.Languafier.Translate("None")) || string.IsNullOrEmpty(printerName))
        {
          int num = (int) MessageBox.Show(GuiData.Languafier.TranslateMessage(3345));
        }
        else
          this.printDocument1.PrinterSettings.PrinterName = printerName;
      }
      try
      {
        this.printDocument1.Print();
      }
      catch (Win32Exception ex)
      {
        if (ex.NativeErrorCode == 122)
          return;
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "TermsAndConditionsDialog.btnPrint_Click", "Error printing terms and conditions: " + ex?.ToString());
      }
    }

    private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
      string s = this.txtTermsAndConditions.Text.Substring(this.nextCharacter);
      Graphics graphics = e.Graphics;
      string text = s;
      Font font = this.txtTermsAndConditions.Font;
      Rectangle marginBounds = e.MarginBounds;
      double width = (double) marginBounds.Width;
      marginBounds = e.MarginBounds;
      double height = (double) marginBounds.Height;
      SizeF layoutArea = new SizeF((float) width, (float) height);
      StringFormat genericTypographic = StringFormat.GenericTypographic;
      int num1;
      ref int local1 = ref num1;
      int num2;
      ref int local2 = ref num2;
      graphics.MeasureString(text, font, layoutArea, genericTypographic, out local1, out local2);
      e.Graphics.DrawString(s, this.txtTermsAndConditions.Font, Brushes.Black, (RectangleF) e.MarginBounds, StringFormat.GenericTypographic);
      this.nextCharacter += num1;
      if (this.nextCharacter < this.txtTermsAndConditions.Text.Length)
        e.HasMorePages = true;
      else
        e.HasMorePages = false;
    }

    private void printDocument1_EndPrint(object sender, PrintEventArgs e) => this.nextCharacter = 0;

    private void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
      this.btnOk.Enabled = this.chkAccept.Checked;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TermsAndConditionsDialog));
      this.txtTermsAndConditions = new TextBox();
      this.panel1 = new Panel();
      this.btnPrint = new Button();
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.chkAccept = new CheckBox();
      this.printDocument1 = new PrintDocument();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.txtTermsAndConditions, "txtTermsAndConditions");
      this.txtTermsAndConditions.BackColor = SystemColors.Window;
      this.txtTermsAndConditions.BorderStyle = BorderStyle.FixedSingle;
      this.txtTermsAndConditions.Name = "txtTermsAndConditions";
      this.txtTermsAndConditions.ReadOnly = true;
      this.txtTermsAndConditions.TabStop = false;
      this.panel1.Controls.Add((Control) this.btnPrint);
      this.panel1.Controls.Add((Control) this.btnOk);
      this.panel1.Controls.Add((Control) this.btnCancel);
      this.panel1.Controls.Add((Control) this.chkAccept);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      componentResourceManager.ApplyResources((object) this.btnPrint, "btnPrint");
      this.btnPrint.Name = "btnPrint";
      this.btnPrint.UseVisualStyleBackColor = true;
      this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
      componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.btnOk.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.chkAccept, "chkAccept");
      this.chkAccept.Name = "chkAccept";
      this.chkAccept.UseVisualStyleBackColor = true;
      this.chkAccept.CheckedChanged += new EventHandler(this.chkAccept_CheckedChanged);
      this.printDocument1.DocumentName = "Terms and Conditions";
      this.printDocument1.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
      this.printDocument1.EndPrint += new PrintEventHandler(this.printDocument1_EndPrint);
      this.AcceptButton = (IButtonControl) this.btnOk;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.txtTermsAndConditions);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (TermsAndConditionsDialog);
      this.ShowIcon = false;
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
