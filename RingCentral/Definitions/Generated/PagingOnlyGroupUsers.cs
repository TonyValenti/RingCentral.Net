namespace RingCentral
{
    public class PagingOnlyGroupUsers
    {
        // List of users allowed to page this group
        public PagingGroupExtensionInfo[] records;

        // Information on navigation
        public NavigationInfo navigation;

        // Information on paging
        public PagingInfo paging;
    }
}