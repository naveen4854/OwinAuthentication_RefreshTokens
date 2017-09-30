using Microsoft.Owin.Security.Infrastructure;
using OwinAuthentication_RefreshTokens.Business;
using OwinAuthentication_RefreshTokens.Helpers;
using System;
using System.Threading.Tasks;

namespace OwinAuthentication_RefreshTokens.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = Guid.NewGuid().ToString("n");

            using (AccountBusiness _repo = new AccountBusiness())
            {
                var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

                var token = new RefreshToken()
                {
                    Id = Utilities.GetHash(refreshTokenId),
                    ClientId = "",
                    Subject = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                token.ProtectedTicket = context.SerializeTicket();

                var result = await _repo.AddRefreshToken(token);

                if (result)
                {
                    context.SetToken(refreshTokenId);
                }

            }
        }
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null)
                allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = Utilities.GetHash(context.Token);

            using (AccountBusiness _repo = new AccountBusiness())
            {
                var refreshToken = await _repo.FindRefreshToken(hashedTokenId);

                if (refreshToken != null)
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                    var result = await _repo.RemoveRefreshToken(hashedTokenId);
                }
            }
        }

        void IAuthenticationTokenProvider.Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        void IAuthenticationTokenProvider.Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }


    }
}