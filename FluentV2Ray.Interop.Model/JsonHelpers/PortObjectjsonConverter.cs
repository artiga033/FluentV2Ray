using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FluentV2Ray.Interop.Model.JsonHelpers
{
    public class PortObjectJsonConverter : JsonConverter<object>
    {
        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Number => reader.GetInt32().ToString(),
                JsonTokenType.String => reader.GetString() ?? "",
                _ => throw new JsonException("port can only be of type number or string")
            };
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            if (value is int i)
            {
                writer.WriteNumberValue(i);
            }
            else if (value is string s)
            {
                writer.WriteStringValue(s);
            }
            else
            {
                throw new JsonException();
            }
        }
    }
}
