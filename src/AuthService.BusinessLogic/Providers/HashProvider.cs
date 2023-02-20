using AuthService.BusinessLogic.Contracts.Providers;

namespace AuthService.BusinessLogic.Providers
{
    public class HashProvider : IHashProvider
    {
        public string Hash(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }

        public bool Verify(string value, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(value, hash);
        }
    }
}
