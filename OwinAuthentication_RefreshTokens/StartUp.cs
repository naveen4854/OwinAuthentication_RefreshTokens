using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using OwinAuthentication_RefreshTokens.Providers;
using System;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(OwinAuthentication_RefreshTokens.Startup))]
namespace OwinAuthentication_RefreshTokens
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new AuthorizationServerProvider(),
                RefreshTokenProvider = new RefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Configure google authentication
            var options = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "925148130964-elr3u3qrnftut09ced43tqrbdlfaeirk.apps.googleusercontent.com",
                ClientSecret = "qiC-roy6ZF5PFz_MLGZLJyW3"
            };
            app.UseGoogleAuthentication(options);

            //// Configure facebook authentication
            //var fbOptions = new FacebookAuthenticationOptions()
            //{

            //};
            //app.UseFacebookAuthentication(fbOptions);

            app.SetDefaultSignInAsAuthenticationType("External");
        }
    }
}