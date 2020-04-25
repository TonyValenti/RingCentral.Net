using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Status
{
    public partial class Index
    {
        public RestClient rc;
        public Restapi.Index parent;

        public Index(Restapi.Index parent)
        {
            this.parent = parent;
            this.rc = parent.rc;
        }

        public string Path()
        {
            return $"{parent.Path()}/status";
        }

        /// <summary>
        /// Operation: Get Service Status
        /// Rate Limit Group: NoThrottling
        /// Http Get /restapi/v1.0/status
        /// </summary>
        public async Task<string> Get(CancellationToken? cancellationToken = null)
        {
            return await rc.Get<string>(this.Path(), null, cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi
{
    public partial class Index
    {
        public Restapi.Status.Index Status()
        {
            return new Restapi.Status.Index(this);
        }
    }
}