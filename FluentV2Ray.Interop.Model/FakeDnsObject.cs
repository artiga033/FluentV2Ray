namespace FluentV2Ray.Interop.Model
{
    public class FakeDnsObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the IP pool CIDR.
        /// </summary>
        public string IpPool { get; set; }

        /// <summary>
        /// Gets or sets the IP pool size.
        /// </summary>
        public long PoolSize { get; set; }
        public FakeDnsObject(string ipPool, long poolSize)
        {
            IpPool = ipPool;
            PoolSize = poolSize;
        }
    }
}
