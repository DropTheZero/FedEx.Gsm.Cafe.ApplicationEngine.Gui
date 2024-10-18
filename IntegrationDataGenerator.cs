// Decompiled with JetBrains decompiler
// Type: FedEx.Gsm.Cafe.ApplicationEngine.Gui.IntegrationDataGenerator
// Assembly: FedEx.Gsm.Cafe.ApplicationEngine.Gui, Version=38.55.1083.0, Culture=neutral, PublicKeyToken=null
// MVID: E00013B0-CD62-4398-B66C-8F9513C81EC9
// Assembly location: C:\Program Files (x86)\FedEx\ShipManager\BIN\FedEx.Gsm.Cafe.ApplicationEngine.Gui.exe

using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields;
using FedEx.Gsm.Cafe.ApplicationEngine.Gui.UtilityFunctions;
using FedEx.Gsm.Cafe.ApplicationEngine.ShipEngineAdapter;
using FedEx.Gsm.Common.Logging;
using FedEx.Gsm.ShipEngine.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

#nullable disable
namespace FedEx.Gsm.Cafe.ApplicationEngine.Gui
{
  internal class IntegrationDataGenerator
  {
    private Account _account;
    private ShippingEngineAdapter _engine;

    public IntegrationDataGenerator(Account account, ShippingEngineAdapter engine)
    {
      if (account == null)
        throw new ArgumentNullException(nameof (account));
      if (engine == null)
        throw new ArgumentNullException(nameof (engine));
      this._account = account;
      this._engine = engine;
    }

    public void GenerateFiles(string culture)
    {
      try
      {
        ServicePackageResponse servicePackaging = this._engine.GetServicePackaging(new ServicePackageRequest()
        {
          Culture = culture
        });
        new IntegrationDataGenerator.OfferingFileGenerator(servicePackaging).GenerateFile(this._account, culture);
        new IntegrationDataGenerator.FieldsFileGenerator(servicePackaging).GenerateFile(this._account, culture);
        new IntegrationDataGenerator.CountryFileGenerator().GenerateFile(this._account, culture);
      }
      catch (Exception ex)
      {
        FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "IntegrationDataGenerator.GenerateFiles", "Exception occurred generating files: " + ex?.ToString());
      }
    }

    private abstract class FileGenerator
    {
      protected FedEx.Gsm.Common.ConfigManager.ConfigManager _config = new FedEx.Gsm.Common.ConfigManager.ConfigManager(FedEx.Gsm.Common.ConfigManager.ConfigManager.Sections.GUISETTINGS);

      public abstract bool IsApplicable(Account account);

      public void GenerateFile(Account account, string culture)
      {
        if (this.IsApplicable(account))
        {
          if (this.ShouldGenerateFile(culture))
          {
            try
            {
              this.GenerateFileInternal(account, culture);
              this.UpdateIndicators(culture);
            }
            catch (Exception ex)
            {
              FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "IntegrationDataGenerator.GenerateFiles", "Failed to generate file " + Path.GetFileName(this.GetFilePath()) + " due to exception " + ex?.ToString());
            }
          }
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_Integration, "IntegrationDataGenerator.GenerateFiles", "Conditions not met to generate file " + Path.GetFileName(this.GetFilePath()));
        }
        else
          this.TryDeleteFile(this.GetFilePath());
      }

      protected abstract void UpdateIndicators(string culture);

      protected abstract void GenerateFileInternal(Account account, string culture);

      protected abstract bool ShouldGenerateFile(string culture);

      protected abstract string GetFilePath();

      protected void TryDeleteFile(string path)
      {
        try
        {
          if (!File.Exists(path))
            return;
          File.Delete(path);
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Info, FxLogger.AppCode_Integration, "IntegrationDataGenerator.TryDeleteFile", "Deleted file '" + Path.GetFileName(path) + "' because init control off");
        }
        catch (Exception ex)
        {
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_Integration, "IntegrationDataGenerator.TryDeleteFile", "Exception occurred deleting  file '" + Path.GetFileName(path) + "': " + ex?.ToString());
        }
      }
    }

    private class OfferingFileGenerator : IntegrationDataGenerator.FileGenerator
    {
      private List<IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator> _generators;

      public OfferingFileGenerator(ServicePackageResponse response)
      {
        this._generators = new List<IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator>()
        {
          (IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator) new IntegrationDataGenerator.OfferingFileGenerator.ServiceOfferingGenerator(response),
          (IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator) new IntegrationDataGenerator.OfferingFileGenerator.PackageOfferingGenerator(response),
          (IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator) new IntegrationDataGenerator.OfferingFileGenerator.CurrencyOfferingGenerator(),
          (IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator) new IntegrationDataGenerator.OfferingFileGenerator.CEDFilingOptionGenerator()
        };
      }

      public override bool IsApplicable(Account account)
      {
        return this._generators.Any<IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator>((Func<IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator, bool>) (g => g.CanGenerateOfferings(account)));
      }

      protected override void UpdateIndicators(string culture)
      {
        this._config.SetProfileValue("INTEGRATION", "OFFERINGSCHANGED", (object) false);
        this._config.SetProfileValue("INTEGRATION", "OFFERINGSCULTURE", (object) culture);
      }

      protected override bool ShouldGenerateFile(string culture)
      {
        bool bVal;
        string str;
        return !this._config.GetProfileBool("INTEGRATION", "OFFERINGSCHANGED", out bVal) | bVal || !this._config.GetProfileString("INTEGRATION", "OFFERINGSCULTURE", out str) || str != culture || !File.Exists(this.GetFilePath());
      }

      protected override string GetFilePath()
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "FedEx\\Integration\\Configurations\\Offerings.xml");
      }

      protected override void GenerateFileInternal(Account account, string culture)
      {
        IEnumerable<OfferingSet> offeringSets = this._generators.Where<IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator>((Func<IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator, bool>) (g => g.CanGenerateOfferings(account))).Select<IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator, OfferingSet>((Func<IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator, OfferingSet>) (g => g.GenerateOfferings(account, culture)));
        Offerings o = new Offerings()
        {
          OfferingSets = new List<OfferingSet>()
        };
        foreach (OfferingSet offeringSet in offeringSets)
        {
          if (offeringSet.Offerings != null && offeringSet.Offerings.Count > 0)
            o.OfferingSets.Add(offeringSet);
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_Integration, "OfferingFileGenerator.GenerateFileInternal", "Omitted offeringset " + offeringSet.Name + " because it was empty");
        }
        string filePath = this.GetFilePath();
        using (StreamWriter text = File.CreateText(filePath))
          new XmlSerializer(typeof (Offerings)).Serialize((TextWriter) text, (object) o);
      }

      private interface IOfferingGenerator
      {
        bool CanGenerateOfferings(Account account);

        OfferingSet GenerateOfferings(Account account, string culture);
      }

      private class ServiceOfferingGenerator : 
        IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator
      {
        private ServicePackageResponse response;

        public ServiceOfferingGenerator(ServicePackageResponse response)
        {
          this.response = response;
        }

        public bool CanGenerateOfferings(Account account)
        {
          return account.is_GPR2_OFFERING_MASTER_Initiative_Enabled;
        }

        public OfferingSet GenerateOfferings(Account account, string culture)
        {
          List<FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering> offeringList = new List<FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering>();
          if (this.response.ServiceList != null && this.response.ServiceList.Count > 0)
          {
            foreach (Svc service in this.response.ServiceList)
            {
              if (!string.IsNullOrEmpty(service.FXIACode) && !string.IsNullOrEmpty(service.PassportCode))
                offeringList.Add(new FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering()
                {
                  Name = service.SvcMediumDesc,
                  FXIACode = service.FXIACode,
                  PassportCode = service.PassportCode
                });
              else
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_Integration, "ServiceOfferingGenerator.GenerateOfferings", "Omitted service due to missing data: " + service?.ToString());
            }
          }
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "ServiceOfferingGenerator.GenerateOfferings", "No services returned");
          return new OfferingSet()
          {
            Name = "servicetypes",
            Offerings = offeringList
          };
        }
      }

      private class PackageOfferingGenerator : 
        IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator
      {
        private ServicePackageResponse response;

        public PackageOfferingGenerator(ServicePackageResponse response)
        {
          this.response = response;
        }

        public bool CanGenerateOfferings(Account account)
        {
          return account.is_GPR2_OFFERING_MASTER_Initiative_Enabled;
        }

        public OfferingSet GenerateOfferings(Account account, string culture)
        {
          List<FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering> offeringList = new List<FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering>();
          if (this.response.PkgingList != null && this.response.PkgingList.Count > 0)
          {
            foreach (Pkging pkging in this.response.PkgingList)
            {
              if (!string.IsNullOrEmpty(pkging.FXIACode) && !string.IsNullOrEmpty(pkging.PassportCode))
                offeringList.Add(new FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering()
                {
                  Name = pkging.MedName,
                  FXIACode = pkging.FXIACode,
                  PassportCode = pkging.PassportCode
                });
              else
                FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_Integration, "PackageOfferingGenerator.GenerateOfferings", "Omitted package due to missing data: " + pkging?.ToString());
            }
          }
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "PackageOfferingGenerator.GenerateOfferings", "No packages returned");
          return new OfferingSet()
          {
            Name = "packagetypes",
            Offerings = offeringList
          };
        }
      }

      private class CurrencyOfferingGenerator : 
        IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator
      {
        public bool CanGenerateOfferings(Account account)
        {
          return account.is_GPR4_CURRENCY_Initiative_Enabled;
        }

        public OfferingSet GenerateOfferings(Account account, string culture)
        {
          List<FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering> offeringList = new List<FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering>();
          CurrencyListResponse currencyList = GuiData.AppController.ShipEngine.GetCurrencyList();
          if (currencyList != null && !currencyList.HasError && currencyList.CurrencyDataList != null)
          {
            foreach (CurrencyData currencyData in currencyList.CurrencyDataList)
              offeringList.Add(new FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering()
              {
                Name = CurrencyNameHelper.GetCurrencyName(culture, currencyData),
                PassportCode = currencyData.IATACode,
                FXIACode = currencyData.IATACode
              });
          }
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "CurrencyOfferingGenerator.GenerateOfferings", "No currency data returned");
          return new OfferingSet()
          {
            Name = "currencytypes",
            Offerings = offeringList
          };
        }
      }

      private class CEDFilingOptionGenerator : 
        IntegrationDataGenerator.OfferingFileGenerator.IOfferingGenerator
      {
        public bool CanGenerateOfferings(Account account) => true;

        public OfferingSet GenerateOfferings(Account account, string culture)
        {
          List<FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering> offeringList = new List<FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering>();
          FilingOptionsListResponse filingOptions1 = GuiData.AppController.ShipEngine.GetFilingOptions(culture);
          if (filingOptions1 != null && filingOptions1.FilingOptions != null)
          {
            foreach (ExportDeclaration.FilingOptions filingOptions2 in (IEnumerable<ExportDeclaration.FilingOptions>) filingOptions1.FilingOptions.OrderBy<ExportDeclaration.FilingOptions, string>((Func<ExportDeclaration.FilingOptions, string>) (d => d.Code)))
              offeringList.Add(new FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Offering()
              {
                Name = filingOptions2.Description,
                PassportCode = filingOptions2.Code,
                FXIACode = filingOptions2.Code
              });
          }
          else
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "CEDFilingOptionGenerator.GenerateOfferings", "No filing options returned");
          return new OfferingSet()
          {
            Name = "b13atypes",
            Offerings = offeringList
          };
        }
      }
    }

    private class FieldsFileGenerator : IntegrationDataGenerator.FileGenerator
    {
      private ServicePackageResponse response;

      public FieldsFileGenerator(ServicePackageResponse response) => this.response = response;

      public override bool IsApplicable(Account account)
      {
        return account.is_GPR3_SVCOPTION_OFFERING_Initiative_Enabled;
      }

      protected override bool ShouldGenerateFile(string culture)
      {
        bool bVal;
        string str;
        return !this._config.GetProfileBool("INTEGRATION", "FIELDSCHANGED", out bVal) | bVal || !this._config.GetProfileString("INTEGRATION", "FIELDSCULTURE", out str) || str != culture || !File.Exists(this.GetFilePath());
      }

      protected override void UpdateIndicators(string culture)
      {
        this._config.SetProfileValue("INTEGRATION", "FIELDSCHANGED", (object) false);
        this._config.SetProfileValue("INTEGRATION", "FIELDSCULTURE", (object) culture);
      }

      protected override string GetFilePath()
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "FedEx\\Integration\\Configurations\\ExternalCafeFields.xml");
      }

      protected override void GenerateFileInternal(Account account, string culture)
      {
        string categoryName = GuiData.Languafier.Translate("FxiaFieldsCategoryName");
        string groupName = GuiData.Languafier.Translate("FxiaFieldsGroupName");
        GuiData.Languafier.Translate("SpecialServiceIndicatorFormat");
        List<SplSvc> source = new List<SplSvc>();
        if (this.response.SpecialServiceList != null)
          source.AddRange((IEnumerable<SplSvc>) this.response.SpecialServiceList);
        if (this.response.NewSpecialServiceList != null)
          source.AddRange((IEnumerable<SplSvc>) this.response.NewSpecialServiceList);
        if (source.Count > 0)
        {
          List<Field> list = source.Where<SplSvc>((Func<SplSvc, bool>) (ss => ss.SpecialServiceCode == SplSvc.SpecialServiceType.NUM_SPEC_SVCS && !string.IsNullOrEmpty(ss.HandlingCode))).Select<SplSvc, Field>((Func<SplSvc, Field>) (ss => new Field()
          {
            ShipmentName = Utility.GetSpecialServiceIndicatorName(ss),
            ReturnName = Utility.GetSpecialServiceIndicatorName(ss),
            Key = "5400-" + ss.HandlingCode,
            IntegrationType = IntegrationType.Both,
            ScreenType = ScreenType.BothScreens,
            ID = "5400",
            Type = "3",
            Size = "1",
            PackageLevel = ".SpecialServices.AdditionalOptions",
            ShipmentLevel = ".SpecialServices.AdditionalOptions",
            MpsCollection = true,
            Shipment = new FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.Fields.Shipment()
            {
              CategoryName = categoryName,
              GroupName = groupName,
              ConversionGroup = "booleans"
            },
            Return = new Return()
            {
              CategoryName = categoryName,
              GroupName = groupName,
              ConversionGroup = "booleans"
            }
          })).ToList<Field>();
          if (list.Count > 0)
          {
            AvailableFields o = new AvailableFields();
            Category category = new Category()
            {
              ShipmentName = categoryName,
              ReturnName = categoryName,
              IntegrationType = IntegrationType.Both,
              ScreenType = ScreenType.BothScreens,
              ExpOrder = 5,
              ImpOrder = 4
            };
            Group group = new Group()
            {
              ShipmentName = groupName,
              ReturnName = groupName,
              IntegrationType = IntegrationType.Both,
              ScreenType = ScreenType.BothScreens
            };
            group.Fields = list;
            category.Groups.Add(group);
            o.Catagories.Add(category);
            string filePath = this.GetFilePath();
            using (StreamWriter text = File.CreateText(filePath))
              new XmlSerializer(typeof (AvailableFields)).Serialize((TextWriter) text, (object) o);
          }
          else
          {
            FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Warning, FxLogger.AppCode_Integration, "IntegrationDataGenerator.GenerateFieldsFile", "No dynamic service options returned, deleting file");
            this.TryDeleteFile(this.GetFilePath());
          }
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "IntegrationDataGenerator.GenerateFieldsFile", "No service options returned");
      }
    }

    private class CountryFileGenerator : IntegrationDataGenerator.FileGenerator
    {
      public override bool IsApplicable(Account account)
      {
        return account.is_GPR4_5_NEW_MARKET_EXPANSION_Initiative_Enabled;
      }

      protected override string GetFilePath()
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "FedEx\\Integration\\Configurations\\CountryProfiles.xml");
      }

      protected override bool ShouldGenerateFile(string culture)
      {
        bool bVal;
        return !this._config.GetProfileBool("INTEGRATION", "COUNTRIESCHANGED", out bVal) | bVal || !File.Exists(this.GetFilePath());
      }

      protected override void UpdateIndicators(string culture)
      {
        this._config.SetProfileValue("INTEGRATION", "COUNTRIESCHANGED", (object) false);
      }

      protected override void GenerateFileInternal(Account account, string culture)
      {
        CountryListResponse countryProfileList = GuiData.AppController.ShipEngine.GetCountryProfileList();
        if (countryProfileList != null && !countryProfileList.HasError && countryProfileList.CountryProfileList != null)
        {
          CountryProfiles o = new CountryProfiles()
          {
            Countries = countryProfileList.CountryProfileList.Select<FedEx.Gsm.ShipEngine.Entities.CountryProfile, FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.CountryProfile>((Func<FedEx.Gsm.ShipEngine.Entities.CountryProfile, FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.CountryProfile>) (c => new FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.CountryProfile()
            {
              Code = c.code,
              IsDomestic = c.isDomesticSupported,
              IsInternational = c.isInternationalSupported
            })).ToList<FedEx.Gsm.Cafe.ApplicationEngine.Gui.Integration.Data.CountryProfile>()
          };
          string filePath = this.GetFilePath();
          using (StreamWriter text = File.CreateText(filePath))
            new XmlSerializer(typeof (CountryProfiles)).Serialize((TextWriter) text, (object) o);
        }
        else
          FxLogger.LogMessage(FedEx.Gsm.Common.Logging.LogLevel.Error, FxLogger.AppCode_Integration, "CountryFileGenerator.GenerateFileInternal", "No country data returned");
      }
    }
  }
}
