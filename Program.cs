// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.Program
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Properties;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Common.Logging;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal static class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      Program.DoStartup(args);
      SplashScreen.Close();
    }

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    private static void DoStartup(string[] args)
    {
      bool createdNew = false;
      Mutex mutex = (Mutex) null;
      try
      {
        MutexSecurity mutexSecurity = new MutexSecurity();
        MutexAccessRule rule = new MutexAccessRule((IdentityReference) new SecurityIdentifier(WellKnownSidType.WorldSid, (SecurityIdentifier) null), MutexRights.FullControl, AccessControlType.Allow);
        mutexSecurity.AddAccessRule(rule);
        mutex = new Mutex(true, "Global\\CafeGui", out createdNew, mutexSecurity);
        if (!createdNew)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_GUI, nameof (DoStartup), "Detected another version of Cafe AE already running");
          Process current = Process.GetCurrentProcess();
          Process process = Array.Find<Process>(Process.GetProcessesByName(current.ProcessName), (Predicate<Process>) (p => p.SessionId == current.SessionId && p.Id != current.Id));
          if (process != null)
          {
            Program.SetForegroundWindow(process.MainWindowHandle);
          }
          else
          {
            int num = (int) MessageBox.Show("FedEx Ship Manager is already running in another session.", "Cannot start FSM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
        }
        else if (!Environment.UserInteractive)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, "Program.DoStartup", "Cafe was started in a non-interactive session, exiting");
        }
        else
        {
          string str = (string) null;
          FedEx.Gsm.Common.ConfigManager.ConfigManager configManager = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUI);
          configManager.GetProfileString("Settings", "Language", out str);
          if (str != null)
          {
            if (str.Length == 3)
            {
              str = Utility.OldLangCodeToCulture(str);
              configManager.SetProfileValue("Settings", "Language", (object) str);
            }
          }
          else
          {
            str = "en-US";
            configManager.SetProfileValue("Settings", "Language", (object) str);
          }
          CultureInfo ci = new CultureInfo(str);
          Thread.CurrentThread.CurrentUICulture = ci;
          Thread.CurrentThread.CurrentCulture = ci;
          CultureInfo.DefaultThreadCurrentCulture = ci;
          CultureInfo.DefaultThreadCurrentUICulture = ci;
          Program.CustomizeCulture(ci);
          Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
          Application.ThreadException += new ThreadExceptionEventHandler(Program.Application_ThreadException);
          AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          Application.Run((Form) new ShellForm());
        }
      }
      catch (Exception ex)
      {
        Program.HandleException(ex);
      }
      finally
      {
        if (createdNew && mutex != null)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_GUI, nameof (DoStartup), "Releasing singleapplicationinstance mutex");
          mutex.ReleaseMutex();
        }
      }
    }

    private static void CustomizeCulture(CultureInfo ci)
    {
      if (ci == null || ci.DateTimeFormat == null || ci.DateTimeFormat.IsReadOnly)
        return;
      if (ci.Name == "fr-CA")
        ci.DateTimeFormat.DateSeparator = CultureInfo.InvariantCulture.DateTimeFormat.DateSeparator;
      ci.DateTimeFormat.AbbreviatedMonthNames = Resources.AbbreviatedMonthNameList.Split(',');
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
      Program.HandleException(e.Exception);
      Application.Exit();
    }

    private static void CurrentDomain_UnhandledException(
      object sender,
      UnhandledExceptionEventArgs e)
    {
      if (!(e.ExceptionObject is Exception))
        return;
      if (e.ExceptionObject is Exception exceptionObject)
        Program.HandleException(exceptionObject);
      else
        FxLogger.LogMessage(e.IsTerminating ? FedEx.Gsm.Common.Logging.LogLevel.Critical : FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_GUI, "AppDomain", "Unhandled Exception Occured. Additional information unavailable.");
    }

    private static void HandleException(Exception e)
    {
      FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Critical, FxLogger.AppCode_GUI, nameof (HandleException), e.ToString());
      int num = (int) MessageBox.Show("FedEx Ship Manager has encountered an error and must exit. See program log for details.");
    }
  }
}
