namespace FluentV2Ray.Interop.Model.Protocols.VMess
{
    public class DetourObject : IV2RayConfig
    {
        public string To { get; set; }

        public DetourObject(string tagToDetour)
        {
            To = tagToDetour;
        }
    }
}
