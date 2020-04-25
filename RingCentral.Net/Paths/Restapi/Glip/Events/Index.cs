using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Glip.Events
{
    public partial class Index
    {
        public RestClient rc;
        public string eventId;
        public Restapi.Glip.Index parent;

        public Index(Restapi.Glip.Index parent, string eventId = null)
        {
            this.parent = parent;
            this.rc = parent.rc;
            this.eventId = eventId;
        }

        public string Path(bool withParameter = true)
        {
            if (withParameter && eventId != null)
            {
                return $"{parent.Path()}/events/{eventId}";
            }

            return $"{parent.Path()}/events";
        }

        /// <summary>
        /// Operation: Get User Events List
        /// Rate Limit Group: Medium
        /// Http Get /restapi/v1.0/glip/events
        /// </summary>
        public async Task<RingCentral.GlipEventsInfo> List(ReadGlipEventsParameters queryParams = null,
            CancellationToken? cancellationToken = null)
        {
            return await rc.Get<RingCentral.GlipEventsInfo>(this.Path(false), queryParams, cancellationToken);
        }

        /// <summary>
        /// Operation: Create Event
        /// Rate Limit Group: Medium
        /// Http Post /restapi/v1.0/glip/events
        /// </summary>
        public async Task<RingCentral.GlipEventInfo> Post(RingCentral.GlipEventCreate glipEventCreate,
            CancellationToken? cancellationToken = null)
        {
            return await rc.Post<RingCentral.GlipEventInfo>(this.Path(false), glipEventCreate, null, cancellationToken);
        }

        /// <summary>
        /// Operation: Get Event
        /// Rate Limit Group: Medium
        /// Http Get /restapi/v1.0/glip/events/{eventId}
        /// </summary>
        public async Task<RingCentral.GlipEventInfo> Get(CancellationToken? cancellationToken = null)
        {
            if (this.eventId == null)
            {
                throw new System.ArgumentNullException("eventId");
            }

            return await rc.Get<RingCentral.GlipEventInfo>(this.Path(), null, cancellationToken);
        }

        /// <summary>
        /// Operation: Update Event
        /// Rate Limit Group: Medium
        /// Http Put /restapi/v1.0/glip/events/{eventId}
        /// </summary>
        public async Task<RingCentral.GlipEventInfo> Put(RingCentral.GlipEventCreate glipEventCreate,
            CancellationToken? cancellationToken = null)
        {
            if (this.eventId == null)
            {
                throw new System.ArgumentNullException("eventId");
            }

            return await rc.Put<RingCentral.GlipEventInfo>(this.Path(), glipEventCreate, null, cancellationToken);
        }

        /// <summary>
        /// Operation: Delete Event
        /// Rate Limit Group: Medium
        /// Http Delete /restapi/v1.0/glip/events/{eventId}
        /// </summary>
        public async Task<string> Delete(CancellationToken? cancellationToken = null)
        {
            if (this.eventId == null)
            {
                throw new System.ArgumentNullException("eventId");
            }

            return await rc.Delete<string>(this.Path(), null, cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Glip
{
    public partial class Index
    {
        public Restapi.Glip.Events.Index Events(string eventId = null)
        {
            return new Restapi.Glip.Events.Index(this, eventId);
        }
    }
}