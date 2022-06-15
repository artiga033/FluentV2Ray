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
using Shadowsocks.Interop.V2Ray.Transport;
using System.Diagnostics;

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
            Init();
            this.Load();
        }
        public CoreConfigController(ILogger<CoreConfigController> logger, string confpath)
        {
            this.ConfigPath = confpath;
            this._logger = logger;
            Init();
            this.Load();
        }
        public JsonSerializerOptions ConfigJsonSerializerOptions { get; } = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        private void Init()
        {
            //this.ConfigJsonSerializerOptions.Converters.Add(new EmptyNullConverter());
        }
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
                            outbound.Settings = JsonSerializer.Deserialize(settingEle, t, ConfigJsonSerializerOptions);
                        }
                    }
                    this.InitConfigForApp();
                }
                catch (Exception e)
                {
                    _logger.LogError("Failed to load config file" + e.Message + e.StackTrace);
                }
            }
            else
            {
                config = Config.Default;
            }
        }
        /// <summary>
        /// Save configuration file.
        /// </summary>
        public void Save()
        {
            this.ClearConfigForCore();
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
            try
            {
                this.Load();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to load config file after it was saved" + e.Message + e.StackTrace);
            }
        }
        /// <summary>
        /// <para>Formats the <see cref="Config"/> object for this App.</para>
        /// <para>This will do operations like initializing the nullable properties, setting collection length to 1...etc.</para>
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void FormatConfigForApp()
        {
            throw new NotImplementedException();

            Format(config);
            void Format(object obj)
            {
                Debug.WriteLine(obj.ToString());
                var properties = obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                foreach (var property in properties)
                {
                    var value = property.GetValue(obj);
                    bool isCollection = (!property.PropertyType.Equals(typeof(string)))
                        && property.PropertyType.GetInterface(nameof(System.Collections.ICollection)) != null
                        && property.PropertyType.IsGenericType;
                    if (isCollection)
                    {
                        //property.SetValue(obj, new[] { Activator.CreateInstance(property.PropertyType.GetGenericTypeDefinition()) });
                    }
                    else if (value is null)
                    {
                        property.SetValue(obj, property.PropertyType.Equals(typeof(string)) ? string.Empty : Activator.CreateInstance(property.PropertyType));
                    }
                    if (!isCollection && !IsSimple(property.PropertyType))
                        Format(property.GetValue(obj) ?? throw new ArgumentNullException());
                }
            }
            // https://stackoverflow.com/questions/863881/how-do-i-tell-if-a-type-is-a-simple-type-i-e-holds-a-single-value
            bool IsSimple(Type type)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // nullable type, check if the nested type is simple.
                    return IsSimple(type.GetGenericArguments()[0]);
                }
                return type.IsPrimitive
                  || type.IsEnum
                  || type.Equals(typeof(string))
                  || type.Equals(typeof(decimal));
            }
        }
        /// <summary>
        /// <para>Clear the <see cref="Config"/> object so that the saved file will be as small and readable as possible.</para>
        /// <para>This will do operations like setting unused properties to null... etc</para>
        /// </summary>
        private void ClearConfigForCore()
        {
            var outbounds = this.Config.Outbounds;
            foreach (var o in outbounds)
            {
                if (o.StreamSettings != null)
                {
                    var tStreamSetting = new StreamSettingsObject()
                    {
                        Network = o.StreamSettings.Network,
                        Security = o.StreamSettings.Security,
                        Sockopt = o.StreamSettings.Sockopt,
                        TlsSettings = o.StreamSettings.TlsSettings,
                    };

                    switch (o.StreamSettings.Network)
                    {
                        case "tcp": tStreamSetting.TcpSettings = o.StreamSettings.TcpSettings; break;
                        case "kcp": tStreamSetting.KcpSettings = o.StreamSettings.KcpSettings; break;
                        case "ws": tStreamSetting.WsSettings = o.StreamSettings.WsSettings; break;
                        case "http": tStreamSetting.HttpSettings = o.StreamSettings.HttpSettings; break;
                        case "domainsocket": tStreamSetting.DsSettings = o.StreamSettings.DsSettings; break;
                        case "quic": tStreamSetting.QuicSettings = o.StreamSettings.QuicSettings; break;
                        default: break;
                    }
                    o.StreamSettings = tStreamSetting;
                }
            }
        }
        /// <summary>
        /// <para>Initialize the <see cref="Config"/> object for this App.</para>
        /// <para>This will do operations like initializing the nullable properties, setting collection length to 1...etc.</para>
        /// <para>An opposite operation to <see cref="ClearConfigForCore"/></para>
        /// </summary>
        private void InitConfigForApp()
        {
            var outbounds = this.Config.Outbounds;
            foreach (var o in outbounds)
            {
                if (o.StreamSettings == null)
                    o.StreamSettings = StreamSettingsObject.DefaultWsTlsAllInit();
                else
                {
                    var t = StreamSettingsObject.DefaultWsTlsAllInit();
                    t.Network = o.StreamSettings.Network;
                    t.Security = o.StreamSettings.Security;
                    t.Sockopt = o.StreamSettings.Sockopt;
                    t.TlsSettings = o.StreamSettings.TlsSettings;
                    switch (o.StreamSettings.Network)
                    {
                        case "tcp": t.TcpSettings = o.StreamSettings.TcpSettings; break;
                        case "kcp": t.KcpSettings = o.StreamSettings.KcpSettings; break;
                        case "ws": t.WsSettings = o.StreamSettings.WsSettings; break;
                        case "http": t.HttpSettings = o.StreamSettings.HttpSettings; break;
                        case "domainsocket": t.DsSettings = o.StreamSettings.DsSettings; break;
                        case "quic": t.QuicSettings = o.StreamSettings.QuicSettings; break;
                        default: break;
                    }
                    o.StreamSettings = t;
                }
            }
        }
    }

    public class EmptyNullConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStringValue(value);
            }
            else
                writer.WriteNull("");
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
