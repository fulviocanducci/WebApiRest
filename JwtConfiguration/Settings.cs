using System.Text;

namespace WebApiRest.JwtConfiguration
{
    public static class Settings
    {
        public static string Secret { get; } = "fedaf7d8863b48e197b9287d492b708e";
        public static byte[] SecretKey { get; } = Encoding.ASCII.GetBytes(Secret);
    }
}