using Newtonsoft.Json;

namespace RingCentral
{
    public class ErrorResponse : Serializable
    {
        // Collection of all gathered errors
        public Error[] errors;
    }
}