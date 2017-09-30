using OwinAuthentication_RefreshTokens.Helpers;
using System;
using System.Threading.Tasks;

namespace OwinAuthentication_RefreshTokens.Business
{
    internal class AccountBusiness : IDisposable
    {
        public async Task<User> FindUser(string userName, string password)
        {
            var user = new User { UserName = userName };
            return await Task.FromResult(user);
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            return await Task.FromResult(CustomTokenHolder.AddToken(token.Id, token));
        }

        public async Task<RefreshToken> FindRefreshToken(string hashedTokenId)
        {
            return await Task.FromResult(CustomTokenHolder.GetToken(hashedTokenId));
        }

        public async Task<bool> RemoveRefreshToken(string hashedTokenId)
        {
            return await Task.FromResult(CustomTokenHolder.RemoveToken(hashedTokenId));
        }

        public void Dispose()
        {
        }
    }
}