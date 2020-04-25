using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Account.ActiveCalls
{
    public partial class Index
    {
        public RestClient rc;
        public Restapi.Account.Index parent;

        public Index(Restapi.Account.Index parent)
        {
            this.parent = parent;
            this.rc = parent.rc;
        }

        public string Path()
        {
            return $"{parent.Path()}/active-calls";
        }

        /// <summary>
        /// Operation: Get Company Active Calls
        /// Rate Limit Group: Heavy
        /// Http Get /restapi/v1.0/account/{accountId}/active-calls
        /// </summary>
        public async Task<RingCentral.CompanyActiveCallsResponse> Get(
            ListCompanyActiveCallsParameters queryParams = null, CancellationToken? cancellationToken = null)
        {
            return await rc.Get<RingCentral.CompanyActiveCallsResponse>(this.Path(), queryParams, cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Account
{
    public partial class Index
    {
        public Restapi.Account.ActiveCalls.Index ActiveCalls()
        {
            return new Restapi.Account.ActiveCalls.Index(this);
        }
    }
}