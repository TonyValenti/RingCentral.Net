using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Account.Directory.Entries.Search
{
    public partial class Index
    {
        public RestClient rc;
        public Restapi.Account.Directory.Entries.Index parent;

        public Index(Restapi.Account.Directory.Entries.Index parent)
        {
            this.parent = parent;
            this.rc = parent.rc;
        }

        public string Path()
        {
            return $"{parent.Path()}/search";
        }

        /// <summary>
        /// Operation: Search Company Directory Entries
        /// Rate Limit Group: Heavy
        /// Http Post /restapi/v1.0/account/{accountId}/directory/entries/search
        /// </summary>
        public async Task<RingCentral.DirectoryResource> Post(
            RingCentral.SearchDirectoryEntriesRequest searchDirectoryEntriesRequest,
            CancellationToken? cancellationToken = null)
        {
            return await rc.Post<RingCentral.DirectoryResource>(this.Path(), searchDirectoryEntriesRequest, null,
                cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Account.Directory.Entries
{
    public partial class Index
    {
        public Restapi.Account.Directory.Entries.Search.Index Search()
        {
            return new Restapi.Account.Directory.Entries.Search.Index(this);
        }
    }
}