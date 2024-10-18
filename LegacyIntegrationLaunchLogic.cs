// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.LegacyIntegrationLaunchLogic
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Integration;
using FedEx.Gsm.Common.ConfigManager;
using FedEx.Gsm.Common.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class LegacyIntegrationLaunchLogic
  {
    private string GetAppDataPath()
    {
      string empty = string.Empty;
      string[] strArray = DataFileLocations.Instance.RootFilesDir.Split(Path.DirectorySeparatorChar);
      return string.Join(Path.DirectorySeparatorChar.ToString(), strArray, 0, strArray.Length - 2);
    }

    private XmlDocument GetXmlDocument(string appDataPath, string xmlFileName)
    {
      XmlDocument xmlDocument;
      try
      {
        xmlFileName = Path.Combine(Path.Combine(appDataPath, "Integration\\Configurations"), xmlFileName);
        if (File.Exists(xmlFileName))
        {
          using (FileStream inStream = new FileStream(xmlFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
          {
            xmlDocument = new XmlDocument();
            xmlDocument.Load((Stream) inStream);
          }
        }
        else
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_Integration, "IntegrationLauncher.GetXmlDocument", "Could not find file at " + xmlFileName);
          xmlDocument = (XmlDocument) null;
        }
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "IntegrationLauncher.GetXmlDocument", "Error loading document " + ex.ToString());
        xmlDocument = (XmlDocument) null;
      }
      return xmlDocument;
    }

    private string GetConfigValue(XmlDocument xmlDoc, string searchKey)
    {
      foreach (XmlNode xmlNode in xmlDoc.GetElementsByTagName("add"))
      {
        XmlAttribute attribute1 = xmlNode.Attributes["key"];
        if (attribute1 != null && string.Equals(attribute1.Value, searchKey, StringComparison.OrdinalIgnoreCase))
        {
          XmlAttribute attribute2 = xmlNode.Attributes["value"];
          if (attribute2 != null)
            return attribute2.Value;
        }
      }
      return string.Empty;
    }

    private string ReadConfigValue(string key)
    {
      try
      {
        XmlDocument xmlDocument = this.GetXmlDocument(this.GetAppDataPath(), this.ConfigFileName);
        if (xmlDocument != null)
          return this.GetConfigValue(xmlDocument, key);
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_Integration, "IntegrationLauncher.ReadConfigValue", "Could not find config file " + this.ConfigFileName);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "IntegrationLauncher.ReadConfigValue", "Error looking up path " + ex.ToString());
        return string.Empty;
      }
      return string.Empty;
    }

    private string GetPath()
    {
      try
      {
        string path = this.ReadConfigValue(this.LaunchDirectoryKey);
        if (!string.IsNullOrEmpty(path))
          return Path.Combine(this.PreparePath(path), this.ExeName);
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_Integration, "IntegrationLauncher.GetPath", "Could not find value for " + this.LaunchDirectoryKey);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "IntegrationLauncher.GetPath", "Error looking up path " + ex.ToString());
        return string.Empty;
      }
      return string.Empty;
    }

    public string ConfigFileName { get; private set; }

    public string LaunchDirectoryKey { get; private set; }

    public string ExeName { get; private set; }

    public string Arguments { get; private set; }

    public int DirectoryClimbCount { get; private set; }

    public IIntegrationEventNotifier Listener { get; private set; }

    public LegacyIntegrationLaunchLogic(
      string config,
      string key,
      string exe,
      IIntegrationEventNotifier listener)
      : this(config, key, exe, string.Empty, 0, listener)
    {
    }

    public LegacyIntegrationLaunchLogic(
      string config,
      string key,
      string exe,
      string args,
      int dircount,
      IIntegrationEventNotifier listener)
    {
      this.ConfigFileName = config;
      this.LaunchDirectoryKey = key;
      this.ExeName = exe;
      this.Arguments = args;
      this.DirectoryClimbCount = dircount;
      this.Listener = listener;
    }

    protected virtual string PreparePath(string path)
    {
      for (int index = 0; index < this.DirectoryClimbCount; ++index)
        path = Path.GetDirectoryName(path);
      return path;
    }

    public bool IsInstalled()
    {
      try
      {
        return File.Exists(this.GetPath());
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool Launch()
    {
      bool flag;
      try
      {
        string path = this.GetPath();
        if (File.Exists(path))
        {
          this.StartProcess(path);
          if (this.Listener != null)
            this.Listener.Register();
          flag = true;
        }
        else
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "LaunchIntegration", "File not found for path: " + path);
          flag = false;
        }
      }
      catch (Exception ex)
      {
        flag = false;
      }
      return flag;
    }

    private void StartProcess(string path)
    {
      Process process = Process.Start(path, this.Arguments);
      try
      {
        process.WaitForInputIdle(1000);
      }
      catch (InvalidOperationException ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_Integration, "LaunchIntegration", "Error waiting for process start " + ex.ToString());
      }
    }

    public string GetVersion() => this.ReadConfigValue("Version");
  }
}
