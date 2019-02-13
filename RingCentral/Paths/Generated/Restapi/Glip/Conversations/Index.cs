using System.Threading.Tasks;

namespace RingCentral.Paths.Restapi.Glip.Conversations
{
    public partial class Index
    {
        public RestClient rc;
        public string chatId;
        public Restapi.Glip.Index parent;

        public Index(Restapi.Glip.Index parent, string chatId = null)
        {
            this.parent = parent;
            this.rc = parent.rc;
            this.chatId = chatId;
        }

        public string Path(bool withParameter = true)
        {
            if (withParameter && chatId != null)
            {
                return $"{parent.Path()}/conversations/{chatId}";
            }

            return $"{parent.Path()}/conversations";
        }

        public class ListQueryParams
        {
            // Number of conversations to be fetched by one request. The maximum value is 250, by default - 30
            public string recordCount;

            // Pagination token.
            public string pageToken;
        }

        public async Task<RingCentral.GlipConversationsList> List(ListQueryParams queryParams = null)
        {
            return await rc.Get<RingCentral.GlipConversationsList>(this.Path(false), queryParams);
        }

        public async Task<RingCentral.GlipConversationInfo> Post(
            RingCentral.GlipPostMembersListBody glipPostMembersListBody)
        {
            return await rc.Post<RingCentral.GlipConversationInfo>(this.Path(false), glipPostMembersListBody);
        }

        public async Task<RingCentral.GlipConversationInfo> Get()
        {
            if (this.chatId == null)
            {
                throw new System.ArgumentNullException("chatId");
            }

            return await rc.Get<RingCentral.GlipConversationInfo>(this.Path());
        }
    }
}

namespace RingCentral.Paths.Restapi.Glip
{
    public partial class Index
    {
        public Restapi.Glip.Conversations.Index Conversations(string chatId = null)
        {
            return new Restapi.Glip.Conversations.Index(this, chatId);
        }
    }
}