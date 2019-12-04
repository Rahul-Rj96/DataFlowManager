using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Helpers;
namespace DataFormManagerApi
{
   

        public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
           
                string subKey = "";
                var cookie = HttpContext.Current.Request.Cookies;
                if (cookie != null)
                {
                    subKey = cookie["subKey"].Value;               
                }

                if (LoginHelper.IsUserRegistered(subKey))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(subKey), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }

