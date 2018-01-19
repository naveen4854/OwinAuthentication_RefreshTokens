using OwinAuthentication_RefreshTokens.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OwinAuthentication_RefreshTokens.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ExternalLogin()
        {
            return new ChallengeResult("Google", "http://localhost/auth/signin-google", this.Request);
        }
    }
}
