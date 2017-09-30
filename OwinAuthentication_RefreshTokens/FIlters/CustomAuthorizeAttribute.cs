using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OwinAuthentication_RefreshTokens.FIlters
{

    public class CustomAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public string Roles { get; set; }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            if (!string.IsNullOrEmpty(Roles))
            {
                if (!(principal.HasClaim(x => x.Type == ClaimTypes.Role)))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    return Task.FromResult<object>(null);
                }

                var claim = principal.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Role).Value;
                var rolesArr = claim.Split(',');

                foreach (var role in Roles.Split(','))
                {
                    if (!rolesArr.Contains(role))
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                        return Task.FromResult<object>(null);
                    }
                }
            }

            //User is Authorized, complete execution
            return Task.FromResult<object>(null);
        }
    }
}