using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Account.Telephony.Sessions.Parties.Reject
{
    public partial class Index
    {
        public RestClient rc;
        public Restapi.Account.Telephony.Sessions.Parties.Index parent;

        public Index(Restapi.Account.Telephony.Sessions.Parties.Index parent)
        {
            this.parent = parent;
            this.rc = parent.rc;
        }

        public string Path()
        {
            return $"{parent.Path()}/reject";
        }

        /// <summary>
        /// Operation: Reject Call Party
        /// HTTP Method: POST
        /// Endpoint: /restapi/v1.0/account/{accountId}/telephony/sessions/{telephonySessionId}/parties/{partyId}/reject
        /// Rate Limit Group: Light
        /// App Permission: CallControl
        /// User Permission: undefined
        /// </summary>
        public async Task<string> Post(CancellationToken? cancellationToken = null)
        {
            return await rc.Post<string>(this.Path(), null, cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Account.Telephony.Sessions.Parties
{
    public partial class Index
    {
        public Restapi.Account.Telephony.Sessions.Parties.Reject.Index Reject()
        {
            return new Restapi.Account.Telephony.Sessions.Parties.Reject.Index(this);
        }
    }
}