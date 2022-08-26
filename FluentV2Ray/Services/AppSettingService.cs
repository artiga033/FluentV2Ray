using FluentV2Ray.Models;
using FluentV2Ray.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;

namespace FluentV2Ray.Services
{
    public class AppSettingService : IAppSettingService
    {
        private string settingPath = "appsetting.json";
        public string SettingPath => settingPath;
        private readonly ILogger<AppSettingService> _logger;
        private AppSetting appSetting;
        public AppSetting AppSetting => appSetting;

        public AppSettingService(ILogger<AppSettingService> logger)
        {
            _logger = logger;

            if (File.Exists(settingPath))
            {
                try
                {
                    string json = File.ReadAllText(settingPath);
                    this.appSetting = JsonSerializer.Deserialize<AppSetting>(json) ?? new AppSetting();
                }
                catch (SystemException)
                {
                    _logger.LogError("Error reading setting's file. The program can't access file " + settingPath);
                    throw;
                }
            }
            else this.appSetting = new AppSetting();
        }

        public bool CheckSettingValid()
        {
            throw new NotImplementedException();
        }

        public void SaveAppSetting()
        {
            string json = JsonSerializer.Serialize(this.appSetting);
            try
            {
                File.WriteAllText(settingPath, json);
            }
            catch (SystemException)
            {
                _logger.LogError("Error writing setting's file. The program can't access file " + settingPath);
                throw;
            }
        }
    }
}
