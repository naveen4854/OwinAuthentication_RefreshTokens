using System.Collections.Generic;

namespace OwinAuthentication_RefreshTokens.Helpers
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public bool IsSupervisor { get; set; }
    }
}