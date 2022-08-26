using FluentV2Ray.Interop.Model.Outbound;
using FluentV2Ray.Interop.Model.Protocols;
using FluentV2Ray.Interop.Model.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FluentV2Ray.Interop.Model.JsonHelpers
{
    public class OutboundObjectJsonConverter : JsonConverter<OutboundObject>
    {
        public override OutboundObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            OutboundObject obj = new();
            Protocol? currentPtl = null;
            object? settings = null;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;
                else if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    if (propertyName == options.PolicizeNaming(nameof(obj.Mux)))
                        obj.Mux = JsonSerializer.Deserialize<MuxObject>(ref reader, options);
                    else if (propertyName == options.PolicizeNaming(nameof(obj.Protocol)))
                    {
                        currentPtl = JsonSerializer.Deserialize<Protocol>(ref reader, options);
                        obj.Protocol = currentPtl.Value;
                        if (settings is JsonElement sele)
                        {
                            Type settingType = obj.Protocol.GetOutboundConfigType();
                            settings = JsonSerializer.Deserialize(sele, settingType, options);
                        }
                    }

                    else if (propertyName == options.PolicizeNaming(nameof(obj.ProxySettings)))
                        obj.ProxySettings = JsonSerializer.Deserialize<ProxySettingsObject>(ref reader, options);
                    else if (propertyName == options.PolicizeNaming(nameof(obj.SendThrough)))
                        obj.SendThrough = reader.GetString();
                    else if (propertyName == options.PolicizeNaming(nameof(obj.Settings)))
                    {
                        if (currentPtl.HasValue)
                            settings = JsonSerializer.Deserialize(ref reader, currentPtl.Value.GetOutboundConfigType(), options);
                        else
                            settings = JsonSerializer.Deserialize<object>(ref reader, options);
                    }
                    else if (propertyName == options.PolicizeNaming(nameof(obj.StreamSettings)))
                        obj.StreamSettings = JsonSerializer.Deserialize<StreamSettingsObject>(ref reader, options);
                    else if (propertyName == options.PolicizeNaming(nameof(obj.Tag)))
                        obj.Tag = reader.GetString() ?? "";
                }
            }
            if (currentPtl.HasValue)
                obj.Protocol = currentPtl.Value;
            if (settings != null)
                obj.Settings = (OutboundConfigurationObjectBase)settings;
            else obj.Settings = Activator.CreateInstance(obj.Protocol.GetOutboundConfigType()) as OutboundConfigurationObjectBase;
            return obj;
        }

        public override void Write(Utf8JsonWriter writer, OutboundObject value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(options.PolicizeNaming(nameof(value.Tag)), value.Tag);
            writer.WriteString(options.PolicizeNaming(nameof(value.Protocol)), JsonSerializer.SerializeToElement(value.Protocol).GetString());

            if (value.Mux != null)
            {
                writer.WritePropertyName(options.PolicizeNaming(nameof(value.Mux)));
                writer.WriteRawValue(JsonSerializer.Serialize(value.Mux, options));
            }
            if (value.ProxySettings != null)
            {
                writer.WritePropertyName(options.PolicizeNaming(nameof(value.ProxySettings)));
                writer.WriteRawValue(JsonSerializer.Serialize(value.ProxySettings, options));
            }
            if (value.SendThrough != null)
            {
                writer.WriteStringValue(options.PolicizeNaming(nameof(value.SendThrough)));
            }
            if (value.Settings != null)
            {
                writer.WritePropertyName(options.PolicizeNaming(nameof(value.Settings)));
                writer.WriteRawValue(JsonSerializer.Serialize(value.Settings, options));
            }
            if (value.StreamSettings != null)
            {
                writer.WritePropertyName(options.PolicizeNaming(nameof(value.StreamSettings)));
                writer.WriteRawValue(JsonSerializer.Serialize(value.StreamSettings, options));
            }

            writer.WriteEndObject();
        }
    }
}
