namespace BookStoreAPI.Tools
{
    public class ExceptionsCode : Exception
    {
        public int StatusCode { get; }

        public ExceptionsCode(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
