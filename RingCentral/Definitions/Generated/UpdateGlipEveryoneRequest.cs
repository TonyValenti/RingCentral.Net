namespace RingCentral
{
    public class UpdateGlipEveryoneRequest
    {
        // Everyone chat name. Maximum number of characters suppported is 250
        public long? name;

        // Everyone chat description. Maximum number of characters suppported is 1000
        public string description;
    }
}