using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Account.Extension.ActiveCalls
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
            return $"{parent.Path()}/active-calls";
        }

        /// <summary>
        /// Operation: Get User Active Calls
        /// Http Get /restapi/v1.0/account/{accountId}/extension/{extensionId}/active-calls
        /// </summary>
        public async Task<RingCentral.UserActiveCallsResponse> Get(
            ListExtensionActiveCallsParameters queryParams = null, CancellationToken? cancellationToken = null)
        {
            return await rc.Get<RingCentral.UserActiveCallsResponse>(this.Path(), queryParams, cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Account.Extension
{
    public partial class Index
    {
        public Restapi.Account.Extension.ActiveCalls.Index ActiveCalls()
        {
            return new Restapi.Account.Extension.ActiveCalls.Index(this);
        }
    }
}