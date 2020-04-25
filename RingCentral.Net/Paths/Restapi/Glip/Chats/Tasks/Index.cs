using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Glip.Chats.Tasks
{
    public partial class Index
    {
        public RestClient rc;
        public Restapi.Glip.Chats.Index parent;

        public Index(Restapi.Glip.Chats.Index parent)
        {
            this.parent = parent;
            this.rc = parent.rc;
        }

        public string Path()
        {
            return $"{parent.Path()}/tasks";
        }

        /// <summary>
        /// Operation: Get Chat Tasks
        /// Http Get /restapi/v1.0/glip/chats/{chatId}/tasks
        /// </summary>
        public async Task<RingCentral.GlipTaskList> Get(ListChatTasksParameters queryParams = null,
            CancellationToken? cancellationToken = null)
        {
            return await rc.Get<RingCentral.GlipTaskList>(this.Path(), queryParams, cancellationToken);
        }

        /// <summary>
        /// Operation: Create Task
        /// Http Post /restapi/v1.0/glip/chats/{chatId}/tasks
        /// </summary>
        public async Task<RingCentral.GlipTaskInfo> Post(RingCentral.GlipCreateTask glipCreateTask,
            CancellationToken? cancellationToken = null)
        {
            return await rc.Post<RingCentral.GlipTaskInfo>(this.Path(), glipCreateTask, null, cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Glip.Chats
{
    public partial class Index
    {
        public Restapi.Glip.Chats.Tasks.Index Tasks()
        {
            return new Restapi.Glip.Chats.Tasks.Index(this);
        }
    }
}