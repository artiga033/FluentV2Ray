using FluentV2Ray.Interop.Model.Inbound;
using FluentV2Ray.Interop.Model.Protocols;
using FluentV2Ray.Interop.Model.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FluentV2Ray.Interop.Model.JsonHelpers
{
    public class InboundObjectJsonConverter : JsonConverter<InboundObject>
    {

        public override InboundObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            InboundObject obj = new InboundObject();
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

                    if (propertyName == options.PolicizeNaming(nameof(obj.Port)))
                        obj.Port = new PortObjectJsonConverter().Read(ref reader, typeof(object), options);
                    else if (propertyName == options.PolicizeNaming(nameof(obj.Protocol)))
                    {
                        currentPtl = JsonSerializer.Deserialize<Protocol>(ref reader, options);
                        obj.Protocol = currentPtl.Value;
                        // has been setted first but at that time not knowing protocol, so deserializied to JsonElement. 
                        if (settings is JsonElement sele)
                        {
                            Type settingType = obj.Protocol.GetInboundConfigType();
                            settings = JsonSerializer.Deserialize(sele, settingType, options);
                        }
                    }
                    else if (propertyName == options.PolicizeNaming(nameof(obj.Settings)))
                    {
                        if (currentPtl.HasValue)
                        {
                            settings = JsonSerializer.Deserialize(ref reader, currentPtl.Value.GetInboundConfigType(), options);
                        }
                        // this serializes to JsonElement
                        else
                            settings = JsonSerializer.Deserialize<object>(ref reader, options);
                    }
                    else if (propertyName == options.PolicizeNaming(nameof(obj.Tag)))
                        obj.Tag = reader.GetString() ?? "";
                    else if (propertyName == options.PolicizeNaming(nameof(obj.Listen)))
                        obj.Listen = reader.GetString();
                    else if (propertyName == options.PolicizeNaming(nameof(obj.Sniffing)))
                        obj.Sniffing = JsonSerializer.Deserialize<SniffingObject>(ref reader, options);
                    else if (propertyName == options.PolicizeNaming(nameof(obj.StreamSettings)))
                        obj.StreamSettings = JsonSerializer.Deserialize<StreamSettingsObject>(ref reader, options);
                    else if (propertyName == options.PolicizeNaming(nameof(obj.Allocate)))
                        obj.Allocate = JsonSerializer.Deserialize<AllocateObject>(ref reader, options);

                }
            }
            if (currentPtl.HasValue)
                obj.Protocol = currentPtl.Value;
            if (settings != null)
                obj.Settings = (InboundConfigurationObjectBase)settings;
            return obj;
        }

        public override void Write(Utf8JsonWriter writer, InboundObject value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(options.PolicizeNaming(nameof(value.Tag)), value.Tag);
            writer.WriteString(options.PolicizeNaming(nameof(value.Protocol)), JsonSerializer.SerializeToElement(value.Protocol).GetString());

            if (value.Allocate != null)
            {
                writer.WritePropertyName(options.PolicizeNaming(nameof(value.Allocate)));
                writer.WriteRawValue(JsonSerializer.Serialize(value.Allocate, options));
            }
            if (value.Listen != null)
            {
                writer.WritePropertyName(options.PolicizeNaming(nameof(value.Listen)));
                writer.WriteStringValue(value.Listen);
            }
            if (value.Port != null)
            {
                writer.WritePropertyName(options.PolicizeNaming(nameof(value.Port)));
                writer.WriteStringValue(value.Port.ToString());
            }
            if (value.Settings != null)
            {
                writer.WritePropertyName(options.PolicizeNaming(nameof(value.Settings)));
                writer.WriteRawValue(JsonSerializer.Serialize(value.Settings, options));
            }
            if (value.Sniffing != null)
            {
                writer.WritePropertyName(options.PolicizeNaming(nameof(value.Sniffing)));
                writer.WriteRawValue(JsonSerializer.Serialize(value.Sniffing, options));

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
