using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Dictionary.Greeting
{
    public partial class Index
    {
        public RestClient rc;
        public string greetingId;
        public Restapi.Dictionary.Index parent;

        public Index(Restapi.Dictionary.Index parent, string greetingId = null)
        {
            this.parent = parent;
            this.rc = parent.rc;
            this.greetingId = greetingId;
        }

        public string Path(bool withParameter = true)
        {
            if (withParameter && greetingId != null)
            {
                return $"{parent.Path()}/greeting/{greetingId}";
            }

            return $"{parent.Path()}/greeting";
        }

        /// <summary>
        /// Operation: Get Standard Greeting List
        /// Http Get /restapi/v1.0/dictionary/greeting
        /// </summary>
        public async Task<RingCentral.DictionaryGreetingList> List(ListStandardGreetingsParameters queryParams = null,
            CancellationToken? cancellationToken = null)
        {
            return await rc.Get<RingCentral.DictionaryGreetingList>(this.Path(false), queryParams, cancellationToken);
        }

        /// <summary>
        /// Operation: Get Standard Greeting
        /// Http Get /restapi/v1.0/dictionary/greeting/{greetingId}
        /// </summary>
        public async Task<RingCentral.DictionaryGreetingInfo> Get(CancellationToken? cancellationToken = null)
        {
            if (this.greetingId == null)
            {
                throw new System.ArgumentNullException("greetingId");
            }

            return await rc.Get<RingCentral.DictionaryGreetingInfo>(this.Path(), null, cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Dictionary
{
    public partial class Index
    {
        public Restapi.Dictionary.Greeting.Index Greeting(string greetingId = null)
        {
            return new Restapi.Dictionary.Greeting.Index(this, greetingId);
        }
    }
}