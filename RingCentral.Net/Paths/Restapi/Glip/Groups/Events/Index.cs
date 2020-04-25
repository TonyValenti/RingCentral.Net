using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Glip.Groups.Events
{
    public partial class Index
    {
        public RestClient rc;
        public Restapi.Glip.Groups.Index parent;

        public Index(Restapi.Glip.Groups.Index parent)
        {
            this.parent = parent;
            this.rc = parent.rc;
        }

        public string Path()
        {
            return $"{parent.Path()}/events";
        }

        /// <summary>
        /// Operation: Create Event by Group ID
        /// Rate Limit Group: Medium
        /// Http Post /restapi/v1.0/glip/groups/{groupId}/events
        /// </summary>
        public async Task<RingCentral.GlipEventInfo> Post(RingCentral.GlipEventCreate glipEventCreate,
            CancellationToken? cancellationToken = null)
        {
            return await rc.Post<RingCentral.GlipEventInfo>(this.Path(), glipEventCreate, null, cancellationToken);
        }

        /// <summary>
        /// Operation: Get Group Events
        /// Rate Limit Group: Medium
        /// Http Get /restapi/v1.0/glip/groups/{groupId}/events
        /// </summary>
        public async Task<RingCentral.GlipEventInfo> Get(CancellationToken? cancellationToken = null)
        {
            return await rc.Get<RingCentral.GlipEventInfo>(this.Path(), null, cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Glip.Groups
{
    public partial class Index
    {
        public Restapi.Glip.Groups.Events.Index Events()
        {
            return new Restapi.Glip.Groups.Events.Index(this);
        }
    }
}