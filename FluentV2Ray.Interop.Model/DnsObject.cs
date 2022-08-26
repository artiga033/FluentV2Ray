using FluentV2Ray.Interop.Model.Dns;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentV2Ray.Interop.Model
{
    public class DnsObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the dictionary storing hosts.
        /// The key is the hostname.
        /// The value can either be a hostname or an IP address.
        /// Support multiple ip addresses, since v4.37.3+, see https://www.v2fly.org/config/dns.html#dnsobject
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Hosts { get; set; } = new Dictionary<string, IEnumerable<string>>();

        /// <summary>
        /// Gets or sets the list of DNS servers.
        /// A DNS server can either be a <see cref="ServerObject"/> or a <see cref="string"/>.
        /// </summary>

        public IList<object> Servers { get; set; } = new List<object>();
        // TODO: Make a custom JsonConverter to handle string input
        //[JsonConverter(typeof(ServerCollectionJsonConverterFactory))]
        //public IList<ServerObject> Servers { get; set; } = new List<ServerObject>();

        /// <summary>
        /// Gets or sets the client IP used when sending requests to DNS server.
        /// </summary>
        public string? ClientIp { get; set; }

        /// <summary>
        /// Gets or sets whether to disable internal DNS cache.
        /// </summary>
        [DefaultValue(false)]
        public bool DisableCache { get; set; }
        /// <summary>
        /// Gets or sets whether to disable DNS fallback
        /// (4.37.2+) Disable DNS Fallback
        /// </summary>
        [DefaultValue(false)]
        public bool DisableFallback { get; set; }
        /// <summary>
        /// (4.37.0+) DNS Query's network type.
        /// UseIPv4, UseIPv6 and UseIP means to query A record only, AAAA only, or both.
        /// </summary>
        [DefaultValue("UseIP")]
        public string QueryStrategy { get; set; } = "UseIP";
        /// <summary>
        /// Gets or sets the inbound tag for DNS traffic.
        /// </summary>
        public string? Tag { get; set; }

        public class ServerCollectionJsonConverterFactory : JsonConverterFactory
        {
            public override bool CanConvert(Type typeToConvert)
            {
                return true;
            }

            public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            {
                return new ServerCollectionJsonConverter();
            }
        }
        public class ServerCollectionJsonConverter : JsonConverter<IList<object>>
        {
            public override IList<object>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var list = new List<ServerObject>();
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    if (reader.TokenType == JsonTokenType.StartObject)
                    {
                        list.Add(JsonSerializer.Deserialize<ServerObject>(ref reader, options));
                    }
                    else if (reader.TokenType == JsonTokenType.String)
                    {
                        list.Add(new ServerObject() { Address = reader.GetString() });
                    }
                    else
                    {
                        throw new ArgumentException("DnsServer Should be the object or a string");
                    }
                }
                return (IList<object>)list; // TODO: IList is not covarient. Much work to do.
            }

            public override void Write(Utf8JsonWriter writer, IList<object> value, JsonSerializerOptions options)
            {
                writer.WriteStartArray();
                foreach (var item in value)
                {
                    if (item is string str)
                    {
                        writer.WriteStringValue(str);
                    }
                    else if (item is ServerObject svr)
                    {
                        writer.WriteRawValue(JsonSerializer.Serialize(svr, options));
                    }
                }
                writer.WriteEndArray();
            }
        }
    }

}
