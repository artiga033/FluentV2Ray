using FluentV2Ray.Interop.Model.Inbound;
using FluentV2Ray.Interop.Model.JsonHelpers;
using FluentV2Ray.Interop.Model.Protocols;
using FluentV2Ray.Interop.Model.Transport;
using System.Text.Json.Serialization;

namespace FluentV2Ray.Interop.Model
{
    [JsonConverter(typeof(InboundObjectJsonConverter))]
    public class InboundObject : IV2RayConfig
    {
        public string Tag { get; set; } = "";
        public string? Listen { get; set; }
        /// <summary>
        /// Gets or sets the port to listen.
        /// Can be a number or a string.
        /// if it's a string, it can begin with `env:<VarName>` to use a environment variable,
        ///     or it can be a number in string form, or a range of ports e.g. 5-10
        /// </summary>
        [JsonConverter(typeof(PortObjectJsonConverter))]
        public object? Port { get; set; }
        public Protocol Protocol { get; set; }
        public InboundConfigurationObjectBase? Settings { get; set; }
        public StreamSettingsObject? StreamSettings { get; set; }
        public SniffingObject? Sniffing { get; set; }
        public AllocateObject? Allocate { get; set; }

        public InboundObject() { }

        public static InboundObject DefaultLocalSocks => new()
        {
            Tag = "socks-in",
            Listen = "127.0.0.1",
            Port = 1080,
            Protocol = Protocol.Socks,
            Settings = new Protocols.Socks.InboundConfigurationObject(),
            Sniffing = new(),
        };

        public static InboundObject DefaultLocalHttp => new()
        {
            Tag = "http-in",
            Listen = "127.0.0.1",
            Port = 8080,
            Protocol = Protocol.Http,
            Sniffing = new(),
        };
    }
}
