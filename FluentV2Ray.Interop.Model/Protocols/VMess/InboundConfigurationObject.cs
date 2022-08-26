namespace FluentV2Ray.Interop.Model.Protocols.VMess
{
    public class InboundConfigurationObject : InboundConfigurationObjectBase
    {
        public List<ClientObject> Clients { get; set; }
        public DefaultObject? Default { get; set; }
        public DetourObject? Detour { get; set; }

        public InboundConfigurationObject()
        {
            Clients = new();
        }
    }
}
