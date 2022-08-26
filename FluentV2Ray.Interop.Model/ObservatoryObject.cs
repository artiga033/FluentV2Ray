namespace FluentV2Ray.Interop.Model
{
    /// <summary>
    /// Connection Observatory confirms the outbounds' status through periodic connection.
    /// </summary>
    public class ObservatoryObject : IV2RayConfig
    {
        public ICollection<string>? SubjectSelector { get; set; }
        public string? ProbeURL;
        public string? ProbeInterval;
    }
}
