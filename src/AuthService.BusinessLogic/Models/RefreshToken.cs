namespace AuthService.BusinessLogic.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }

        public DateTime IssuedAtUtc { get; set; }

        public DateTime ExpiresAtUtc { get; set; }
    }
}
