using System.Text.Json.Serialization;

namespace FluentV2Ray.Interop.Model.V5
{
    public class LogObject
    {
        public LogSpecObject? Access { get; set; }
        public LogSpecObject? Error { get; set; }
    }

    public class LogSpecObject
    {
        public LogType Type { get; set; }
        public string? Path { get; set; }
        public LogLevel Level { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LogType
    {
        None,
        Console,
        File
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LogLevel
    {
        None,
        Error,
        Warning,
        Info,
        Debug,
    }
}