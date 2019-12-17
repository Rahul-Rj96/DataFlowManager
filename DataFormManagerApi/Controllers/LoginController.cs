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
            var code = System.Web.HttpUtility.ParseQueryString(uri.Query)["code"];
            TokenObjectModel tokenObj = LoginHelper.GetGoogleAccesToken(code);
            HttpResponseMessage resp =  LoginHelper.CreateCookie(tokenObj.AuthorizationCode);
            return (resp); 
        }
        [HttpGet, Route("getAuthCode")]
        public HttpResponseMessage GetAuthCode()
        {
            string url = LoginHelper.GetGoogleAuthCode();
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(url);
            return response;
        }

        [HttpGet, Route("verifyCookie")]
        public HttpResponseMessage verifyCookie()
        {
            
            HttpContext context = HttpContext.Current;
            string SessionId = "";
            string UserId = "";
            string Username= "";
            string SubKey = "";

            CookieHeaderValue cookie = Request.Headers.GetCookies("session").FirstOrDefault();
            if (cookie != null)
            {
                CookieState cookieState = cookie["session"];
                SessionId = cookieState["SessionId"];
                UserId = cookieState["UserId"];
                Username = cookieState["Username"];
                SubKey = cookieState["SubKey"];
            }

            if (SubKey == (string)(context.Session["SubKey"]) & SessionId == (string)(context.Session["SessionId"]))
            {           
                return Request.CreateResponse(HttpStatusCode.OK, "success");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "failed");
            }
        }

        [HttpPost, Route("AccessToken")]
        public HttpResponseMessage getAccessTokenByAuthorizationCode([FromBody]CodeObject codeObj)
        {
            TokenObjectModel tokenObj = TokenHelper.getTokenByAuthorizationCode(codeObj.code);
            if (tokenObj != null)
            {
                var message = Request.CreateResponse(HttpStatusCode.Created, tokenObj);
                return message; ;
            }
            else
            {
                var message = Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Token");
                return message;
            }
            

        }
        [HttpPost, Route("RefreshToken")]
        public HttpResponseMessage getAccessTokenByRefreshToken([FromBody]RefreshTokenObject refreshTokenObj)
        {
            UserObjectModel userObj = TokenHelper.getUserByRefreshToken(refreshTokenObj.RefreshToken);
            TokenObjectModel tokenObj = TokenHelper.createToken();
            tokenObj = TokenHelper.getTokenByAuthorizationCode(tokenObj.AuthorizationCode);
            if (userObj!=null || tokenObj!=null)
            {
                UserTokensObjectModel userTokenObj = new UserTokensObjectModel(userObj.UserId, tokenObj.TokenId);
                bool success = UserTokensHelper.MapUserToken(userTokenObj);
                var message = Request.CreateResponse(HttpStatusCode.Created, tokenObj);
                return message;
            }
            else
            {
                var message = Request.CreateResponse(HttpStatusCode.Unauthorized,"Invalid Token");
                return message;
            }
            
           
        }
    }
}