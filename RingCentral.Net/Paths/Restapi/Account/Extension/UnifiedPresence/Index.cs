using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Account.Extension.UnifiedPresence
{
    public partial class Index
    {
        public RestClient rc;
        public Restapi.Account.Extension.Index parent;

        public Index(Restapi.Account.Extension.Index parent)
        {
            this.parent = parent;
            this.rc = parent.rc;
        }

        public string Path()
        {
            return $"{parent.Path()}/unified-presence";
        }

        /// <summary>
        /// Operation: Get Unified Presence
        /// Rate Limit Group: Medium
        /// Http Get /restapi/v1.0/account/{accountId}/extension/{extensionId}/unified-presence
        /// </summary>
        public async Task<RingCentral.UnifiedPresence> Get(CancellationToken? cancellationToken = null)
        {
            return await rc.Get<RingCentral.UnifiedPresence>(this.Path(), null, cancellationToken);
        }

        /// <summary>
        /// Operation: Update Unified Presence
        /// Rate Limit Group: Medium
        /// Http Patch /restapi/v1.0/account/{accountId}/extension/{extensionId}/unified-presence
        /// </summary>
        public async Task<RingCentral.UnifiedPresence> Patch(RingCentral.UpdateUnifiedPresence updateUnifiedPresence,
            CancellationToken? cancellationToken = null)
        {
            return await rc.Patch<RingCentral.UnifiedPresence>(this.Path(), updateUnifiedPresence, null,
                cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Account.Extension
{
    public partial class Index
    {
        public Restapi.Account.Extension.UnifiedPresence.Index UnifiedPresence()
        {
            return new Restapi.Account.Extension.UnifiedPresence.Index(this);
        }
    }
}