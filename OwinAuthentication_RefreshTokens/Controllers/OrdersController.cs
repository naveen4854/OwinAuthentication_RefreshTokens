using OwinAuthentication_RefreshTokens.FIlters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OwinAuthentication_RefreshTokens.Controllers
{
    public class OrdersController : ApiController
    {
        [HttpGet]
        [Route("test")]
        [CustomAuthorize]
        public string test()
        {
            return "success";
        }

        [HttpGet]
        [Route("test2")]
        [CustomAuthorize(Roles = "Admin1")]
        public string test2()
        {
            return "success2";
        }

        [HttpGet]
        [Route("test3")]
        [CustomAuthorize(Roles = "Admin")]
        public string test3()
        {
            return "success3";
        }
    }
}
