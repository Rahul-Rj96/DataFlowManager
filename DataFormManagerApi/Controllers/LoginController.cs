using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Helpers;
using System.Net.Http;
using System.Net;
using DataFormManager.Models;
using System.Net.Http.Headers;
using System.Web.SessionState;
using DataFormManagerApi;

namespace dataFormManagerApi.Controllers
{
    [RoutePrefix("api/login")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        [HttpGet, System.Web.Http.Route("google")]
        public HttpResponseMessage GoogleLogin()
        {
            var uri = Request.RequestUri;
            HttpContext context = HttpContext.Current;
            var code = System.Web.HttpUtility.ParseQueryString(uri.Query)["code"];
            UserObjectModel userObj = LoginHelper.GetAccesToken(code);

            var resp = new HttpResponseMessage(HttpStatusCode.Moved);
            resp.Headers.Location = new Uri(@"http://dataformmanager.dev37.grcdev.com/success.html");

            var cookie = new CookieHeaderValue("subKey", (string)(context.Session["Sub"]));
            //cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return resp;
        }
        
        [HttpGet, Route("getAuthCode")]
        public System.Net.Http.HttpResponseMessage GetAuthCode()
        {
            string url = LoginHelper.GetAuthCode();
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(url);
            return response;
        }

        [BasicAuthenticationAttribute]
        [HttpGet, Route("success")]
        public System.Net.Http.HttpResponseMessage successPage()
        {
            string url = @"http://dataformmanager.dev37.grcdev.com/success.html";
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(url);
            return response;
        }
    }
}