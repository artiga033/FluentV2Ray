using FluentV2Ray.Interop.Model;
using FluentV2Ray.Interop.Model.Protocols;
using FluentV2Ray.Interop.Model.Transport;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

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
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new ConfigPocoJsonConverterFactory() {
                IgnoreRules = {
                    x => x is null,
                    x=> x is IEnumerable i && !i.GetEnumerator().MoveNext() // x is IEnumrable, but it's empty.

                }}}
        };
        private void Init()
        {

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
                    //foreach (var outbound in config.Outbounds)
                    //{
                    //    if (outbound.Settings != null)
                    //    {
                    //        var t = outbound.Protocol.GetOutboundConfigType();
                    //        var settingEle = (JsonElement)outbound.Settings;
                    //        outbound.Settings = JsonSerializer.Deserialize(settingEle, t, ConfigJsonSerializerOptions);
                    //    }
                    //}
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
            //this.ClearConfigForCore();
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
                    o.StreamSettings = StreamSettingsObject.DefaultAllInit();
                else
                {
                    var t = StreamSettingsObject.DefaultAllInit();
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

    /// <summary>
    /// Factory mode is low efficient. Check Comments of <see cref="ConfigPocoJsonConverter{T}" for detail/>
    /// </summary>
    public class ConfigPocoJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.GetCustomAttribute<JsonConverterAttribute>() != null)
                return false;
            return typeof(IV2RayConfig).IsAssignableFrom(typeToConvert);
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(typeof(ConfigPocoJsonConverter<>).MakeGenericType(typeToConvert), IgnoreRules)!;
        }
        public IList<Func<object?, bool>> IgnoreRules { get; } = new List<Func<object?, bool>>();
    }
    /// <summary>
    /// Avoid Direct Use of this Converter. Polymorphic converter can't correctly handle arrays yet.
    /// <para>
    /// runtime has made a change in .NET 7, see <see href="https://github.com/dotnet/runtime/pull/73259"/>
    /// </para>
    /// <para>
    /// TODO: This Converter can be directly added to Serialize Options when Migrating to .NET 7
    /// <para>
    /// That is, directly inherits <see cref="JsonConverter{IV2RayConfig}"/> so that we can use one Converter to handle all objects.
    /// Current factory mode creates a <see cref="JsonConverter"/> for every type. It's very low perfomance.
    /// </para>
    /// <para>
    /// In the Future may be we can also use Source Generator for higher performance.
    /// </para>
    /// </para>
    /// </summary>

    // in future .NET 7, this implementation works.
    // public class ConfigPocoConverter : JsonConverter<IV2RayConfig> 
    public class ConfigPocoJsonConverter<T> : JsonConverter<T> where T : IV2RayConfig
    {
        public ConfigPocoJsonConverter(IList<Func<object?, bool>> ignoreRules)
        {
            IgnoreRules = ignoreRules;
        }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var obj = (T)(Activator.CreateInstance(typeToConvert) ?? throw new ArgumentException());

            var propertyInfos = typeToConvert.GetProperties(publicInstance | BindingFlags.SetProperty);
            List<int> settedPropIndex = new();
            do
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();

                    var i = Enumerable.Range(0, propertyInfos.Length).FirstOrDefault(x => propertyName == (options.PropertyNamingPolicy?.ConvertName(propertyInfos[x].Name) ?? propertyInfos[x].Name), -1);
                    if (i == -1)
                        continue;
                    var propertyInfo = propertyInfos[i];
                    settedPropIndex.Add(i);

                    object? propertyVal = null;
                    reader.Read();

                    //var customConverter = propertyInfo.GetCustomAttribute<JsonConverterAttribute>();
                    //if (customConverter != null)
                    //{
                    //    //propertyVal = customConverter.CreateConverter(propertyInfo.PropertyType); // Todo: use json converte specified on property attribute
                    //} /*else*/

                    propertyVal = JsonSerializer.Deserialize(ref reader, propertyInfo.PropertyType, options) ?? throw new JsonException();

                    if (propertyVal == null)
                        propertyVal = InitType(propertyInfo.PropertyType);
                    propertyInfo.SetValue(obj, propertyVal);
                }
            } while (reader.Read());

            var notSettedIndex = Enumerable.Range(0, propertyInfos.Length).Except(settedPropIndex);
            foreach (int i in notSettedIndex)
            {
                if (propertyInfos[i].GetValue(obj) == null)
                    propertyInfos[i].SetValue(obj, InitType(propertyInfos[i].PropertyType));
            }

            return obj;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var propertyInfos = value.GetType().GetProperties(publicInstance);
            writer.WriteStartObject();
            foreach (var property in propertyInfos)
            {
                var propertyVal = property.GetValue(value);

                // skip the property if satisfies custom rules or it's equal to default value;
                bool skip = false;
                foreach (var shouldIgnore in IgnoreRules)
                {
                    if (shouldIgnore(propertyVal))
                    {
                        skip = true;
                        break;
                    }
                }

                var defaultAttr = property.GetCustomAttribute<DefaultValueAttribute>();
                if (skip || (defaultAttr != null && Equals(defaultAttr.Value, propertyVal)))
                {
                    continue;
                }

                string jsonVal = JsonSerializer.SerializeToElement(propertyVal, options).GetRawText();

                writer.WritePropertyName(options.PropertyNamingPolicy?.ConvertName(property.Name) ?? property.Name);
                writer.WriteRawValue(jsonVal);
            }
            writer.WriteEndObject();
        }

        private object InitType(Type type)
        {
            object? obj;

            var defaultVal = type.GetCustomAttribute<DefaultValueAttribute>()?.Value;
            if (defaultVal != null)
            {
                obj = defaultVal;
            }
            else if (type == typeof(string)) obj = "";
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) obj = Activator.CreateInstance(type);
            else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(type)) // note: string is also Enumable, but we have `else if` 
            {
                if (type.IsInterface && type.IsGenericType)
                    obj = Activator.CreateInstance(typeof(List<>).MakeGenericType(type.GetGenericArguments()));
                else
                    obj = Activator.CreateInstance(type);
            }
            else { obj = Activator.CreateInstance(type); EnsureMemberNotNull(); }

            void EnsureMemberNotNull()
            {
                var properties = type.GetProperties(publicInstance | BindingFlags.SetProperty | BindingFlags.GetProperty);
                foreach (var p in properties)
                {
                    var pVal = p.GetValue(obj);
                    if (pVal == null)
                    {
                        p.SetValue(obj, InitType(p.PropertyType));
                    }
                }
            }
            return obj;
        }
        private IList<Func<object?, bool>> IgnoreRules { get; }
        private const BindingFlags publicInstance = BindingFlags.Public | BindingFlags.Instance;
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
