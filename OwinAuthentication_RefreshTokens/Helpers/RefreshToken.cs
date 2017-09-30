using System;

namespace OwinAuthentication_RefreshTokens.Helpers
{
    internal class RefreshToken
    {
        public string ClientId { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string Id { get; set; }
        public DateTime IssuedUtc { get; set; }
        public string Subject { get; set; }
        public string ProtectedTicket { get; set; }
    }
}