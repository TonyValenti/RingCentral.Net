namespace RingCentral
{
    public class UserCallLogResponse
    {
        // List of call log records
        public CallLogRecord[] records; // Required

        // Information on navigation
        public NavigationInfo navigation; // Required

        // Information on paging
        public PagingInfo paging; // Required
    }
}