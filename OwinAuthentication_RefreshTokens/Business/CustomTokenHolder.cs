using OwinAuthentication_RefreshTokens.Helpers;
using System.Collections.Concurrent;

namespace OwinAuthentication_RefreshTokens.Business
{
    internal static class CustomTokenHolder
    {
        private static ConcurrentDictionary<string, RefreshToken> tokens = new ConcurrentDictionary<string, RefreshToken>();

        public static ConcurrentDictionary<string, RefreshToken> GetAllTokens()
        {
            return tokens;
        }
        public static bool AddToken(string key, RefreshToken token)
        {
            return tokens.TryAdd(key, token);
        }

        public static RefreshToken GetToken(string key)
        {
            var token = new RefreshToken();
            if (tokens.TryGetValue(key, out token))
                return token;

            return null;
        }

        public static bool RemoveToken(string hashedTokenId)
        {
            var token = new RefreshToken();
            return tokens.TryRemove(hashedTokenId, out token);
        }
    }
}