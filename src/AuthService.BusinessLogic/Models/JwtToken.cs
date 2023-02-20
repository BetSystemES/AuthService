namespace AuthService.BusinessLogic.Models
{
    public class JwtToken
    {
        public string Token { get; set; }

        public DateTime IssuedAtUtc { get; set; }

        public DateTime ExpiresAtUtc { get; set; }
    }
}
