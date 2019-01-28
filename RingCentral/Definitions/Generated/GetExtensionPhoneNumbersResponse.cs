namespace RingCentral
{
    public class GetExtensionPhoneNumbersResponse
    {
        // List of phone numbers
        public UserPhoneNumberInfo[] records; // Required

        // Information on navigation
        public NavigationInfo navigation; // Required

        // Information on paging
        public PagingInfo paging; // Required
    }
}