using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using p = Shadowsocks.Interop.V2Ray.Protocols;

namespace FluentV2Ray.Interop.Model.Protocols
{
    [JsonConverter(typeof(JsonProtocolConverter))]
    public enum Protocol
    {
        Blackhole,
        Dns,
        DokodemoDoor,
        Freedom,
        Http,
        Socks,
        Vmess,
        Shadowsocks,
        Trojan,
        Vless,
        Loopback
    }
    /// <summary>
    /// Use this converter, the protocol dokodemo-door has a dash('-') char in it.
    /// <para>
    /// However C# does not allow us to use this in enum name.
    /// Thus makes <see cref="JsonStringEnumConverter"/> hard to work.
    /// So we have this one, giving a special treat for dokodemo-door.
    /// </para>
    /// </summary>
    public class JsonProtocolConverter : JsonConverter<Protocol>
    {
        private const string dokodemoDoor = "dokodemo-door";
        public override Protocol Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            if (s == dokodemoDoor)
                return Protocol.DokodemoDoor;
            return (Protocol)Enum.Parse(typeToConvert, s, ignoreCase: true);
        }

        public override void Write(Utf8JsonWriter writer, Protocol value, JsonSerializerOptions options)
        {
            if (value == Protocol.DokodemoDoor)
            {
                writer.WriteStringValue(dokodemoDoor);
                return;
            }
            else
            {
                string s = value.ToString();
                // to camelCase
                writer.WriteStringValue(char.ToLower(s[0]) + s[1..]);
            }
        }
    }
    public static class ProtocolExtensions
    {
        public static Type GetOutboundConfigType(this Protocol p)
        {
            return p switch
            {
                Protocol.Vmess=> typeof(p.VMess.OutboundConfigurationObject),
                Protocol.Shadowsocks => typeof(p.Shadowsocks.OutboundConfigurationObject),
                _=>throw new NotImplementedException()
            };
        }
    }
}
