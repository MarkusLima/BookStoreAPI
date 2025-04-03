namespace BookStoreAPI
{
    public class Configuration
    {
        public static string PrivateKey { get; set; } = Environment.GetEnvironmentVariable("TokenKey") ?? "TokenKey";
    }
}
