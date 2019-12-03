using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Helpers;
using System.Net.Http;
using System.Net;

namespace dataFormManagerApi.Controllers
{
    [RoutePrefix("api/login")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
            [HttpGet, System.Web.Http.Route("google")]
            public IEnumerable<string> GoogleLogin()
            {
                var uri = Request.RequestUri;
                string response = null;
                string code = null;
                code = System.Web.HttpUtility.ParseQueryString(uri.Query)["code"];
                response = LoginHelper.GetAccesToken(code);
                return new string[] { code, response };
            }

        [HttpGet, System.Web.Http.Route("getAuthCode")]
        public System.Net.Http.HttpResponseMessage GetAuthCode()
        {
            string url = LoginHelper.GetAuthCode();

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(url);
            return response;
        }

    }
    }