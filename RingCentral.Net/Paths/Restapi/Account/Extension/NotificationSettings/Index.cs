using System.Threading.Tasks;
using System.Threading;

namespace RingCentral.Paths.Restapi.Account.Extension.NotificationSettings
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
            return $"{parent.Path()}/notification-settings";
        }

        /// <summary>
        /// Operation: Get Notification Settings
        /// Http Get /restapi/v1.0/account/{accountId}/extension/{extensionId}/notification-settings
        /// </summary>
        public async Task<RingCentral.NotificationSettings> Get(CancellationToken? cancellationToken = null)
        {
            return await rc.Get<RingCentral.NotificationSettings>(this.Path(), null, cancellationToken);
        }

        /// <summary>
        /// Operation: Update Notification Settings
        /// Http Put /restapi/v1.0/account/{accountId}/extension/{extensionId}/notification-settings
        /// </summary>
        public async Task<RingCentral.NotificationSettings> Put(
            RingCentral.NotificationSettingsUpdateRequest notificationSettingsUpdateRequest,
            CancellationToken? cancellationToken = null)
        {
            return await rc.Put<RingCentral.NotificationSettings>(this.Path(), notificationSettingsUpdateRequest, null,
                cancellationToken);
        }
    }
}

namespace RingCentral.Paths.Restapi.Account.Extension
{
    public partial class Index
    {
        public Restapi.Account.Extension.NotificationSettings.Index NotificationSettings()
        {
            return new Restapi.Account.Extension.NotificationSettings.Index(this);
        }
    }
}