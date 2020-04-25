using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Account.Extension.MessageStore
{
    public partial class Index
    {
        public RestClient rc;
        public string messageId;
        public Restapi.Account.Extension.Index parent;

        public Index(Restapi.Account.Extension.Index parent, string messageId = null)
        {
            this.parent = parent;
            this.rc = parent.rc;
            this.messageId = messageId;
        }

        public string Path(bool withParameter = true)
        {
            if (withParameter && messageId != null)
            {
                return $"{parent.Path()}/message-store/{messageId}";
            }

            return $"{parent.Path()}/message-store";
        }

        /// <summary>
        /// Operation: Get Message List
        /// Rate Limit Group: Light
        /// Http Get /restapi/v1.0/account/{accountId}/extension/{extensionId}/message-store
        /// </summary>
        public async Task<RingCentral.GetMessageList> List(ListMessagesParameters queryParams = null,
            CancellationToken? cancellationToken = null)
        {
            return await rc.Get<RingCentral.GetMessageList>(this.Path(false), queryParams, cancellationToken);
        }

        /// <summary>
        /// Operation: Get Message
        /// Rate Limit Group: Light
        /// Http Get /restapi/v1.0/account/{accountId}/extension/{extensionId}/message-store/{messageId}
        /// </summary>
        public async Task<RingCentral.GetMessageInfoResponse> Get(CancellationToken? cancellationToken = null)
        {
            if (this.messageId == null)
            {
                throw new System.ArgumentNullException("messageId");
            }

            return await rc.Get<RingCentral.GetMessageInfoResponse>(this.Path(), null, cancellationToken);
        }

        /// <summary>
        /// Operation: Update Message List
        /// Rate Limit Group: Medium
        /// Http Put /restapi/v1.0/account/{accountId}/extension/{extensionId}/message-store/{messageId}
        /// </summary>
        public async Task<RingCentral.GetMessageInfoResponse> Put(RingCentral.UpdateMessageRequest updateMessageRequest,
            UpdateMessageParameters queryParams = null, CancellationToken? cancellationToken = null)
        {
            if (this.messageId == null)
            {
                throw new System.ArgumentNullException("messageId");
            }

            return await rc.Put<RingCentral.GetMessageInfoResponse>(this.Path(), updateMessageRequest, queryParams,
                cancellationToken);
        }

        /// <summary>
        /// Operation: Delete Message
        /// Rate Limit Group: Medium
        /// Http Delete /restapi/v1.0/account/{accountId}/extension/{extensionId}/message-store/{messageId}
        /// </summary>
        public async Task<string> Delete(DeleteMessageParameters queryParams = null,
            CancellationToken? cancellationToken = null)
        {
            if (this.messageId == null)
            {
                throw new System.ArgumentNullException("messageId");
            }

            return await rc.Delete<string>(this.Path(), queryParams, cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Account.Extension
{
    public partial class Index
    {
        public Restapi.Account.Extension.MessageStore.Index MessageStore(string messageId = null)
        {
            return new Restapi.Account.Extension.MessageStore.Index(this, messageId);
        }
    }
}