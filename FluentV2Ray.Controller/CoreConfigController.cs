using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using FluentV2Ray.Interop.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using NLog;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using FluentV2Ray.Interop.Model.Protocols;

namespace FluentV2Ray.Controller
{
    public class CoreConfigController
    {
        private string configPath = "coreConfig.json";
        public string ConfigPath { get => configPath; set { configPath = value; } }
        private readonly ILogger<CoreConfigController> _logger;
        private Config config;
        public Config Config { get => config; }

        public CoreConfigController(ILogger<CoreConfigController> logger)
        {
            this._logger = logger;
            this.Load();
        }
        public CoreConfigController(ILogger<CoreConfigController> logger, string confpath)
        {
            this.ConfigPath = confpath;
            this._logger = logger;
            this.Load();
        }
        public JsonSerializerOptions ConfigJsonSerializerOptions { get; } = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        /// <summary>
        /// Load configuration file.
        /// </summary>
        [MemberNotNull(nameof(config), nameof(Config))]
        public void Load()
        {
            if (System.IO.File.Exists(ConfigPath))
            {
                var json = System.IO.File.ReadAllText(ConfigPath);
                try
                {
                    config = JsonSerializer.Deserialize<Config>(json, ConfigJsonSerializerOptions) ?? new Config();
                    foreach (var outbound in config.Outbounds)
                    {
                        if (outbound.Settings != null)
                        {
                            var t = outbound.Protocol.GetOutboundConfigType();
                            var settingEle = (JsonElement)outbound.Settings;
                            outbound.Settings = JsonSerializer.Deserialize(settingEle, t,ConfigJsonSerializerOptions);
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("Failed to load config file" + e.Message + e.StackTrace);
                }
            }
            else
            {
                config = new Config();
            }
        }
        /// <summary>
        /// Save configuration file.
        /// </summary>
        public void Save()
        {
            var json = JsonSerializer.Serialize(config, ConfigJsonSerializerOptions);
            try
            {
                File.WriteAllText(ConfigPath, json);
                _logger.LogInformation($"New config file saved.({ConfigPath})");

            }
            catch (Exception e)
            {
                _logger.LogError("Failed to save config file" + e.Message + e.StackTrace);
            }
        }
    }
    public static partial class ControllerDIExtensions
    {
        /// <summary>
        /// Add CoreConfigController to DI container.
        /// </summary>
        /// <param name="confPath">the core's config file path</param>
        public static IServiceCollection AddCoreConfigController(this IServiceCollection services, string confPath = "coreConfig.json")
        {
            var factory = (IServiceProvider s) =>
                new CoreConfigController(s.GetRequiredService<ILogger<CoreConfigController>>(), confPath);
            services.AddSingleton<CoreConfigController>(factory);
            return services;
        }
    }

}
