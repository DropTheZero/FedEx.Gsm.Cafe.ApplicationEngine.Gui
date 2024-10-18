// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.IntegrationLauncher
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Integration;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class IntegrationLauncher
  {
    public LegacyIntegrationLaunchLogic FXI { get; private set; }

    public LegacyIntegrationLaunchLogic FXIA { get; private set; }

    public LegacyIntegrationLaunchLogic FXIAWizard { get; private set; }

    public IntegrationLauncher()
    {
      this.FXIA = new LegacyIntegrationLaunchLogic("FXIAnetConfig.xml", "InstallDirectory", "IASE.exe", (IIntegrationEventNotifier) new CompositeEventNotifier());
      this.FXIAWizard = new LegacyIntegrationLaunchLogic("FXIAnetConfig.xml", "InstallDirectory", "IA.UserInterface.exe", "/culture " + GuiData.ConfigManager.Language, 0, (IIntegrationEventNotifier) null);
      this.FXI = new LegacyIntegrationLaunchLogic("FXIConfig.xml", "SystemDirectory", "FSI.exe", "-r", 1, (IIntegrationEventNotifier) new LegacyEventNotifier());
    }
  }
}
