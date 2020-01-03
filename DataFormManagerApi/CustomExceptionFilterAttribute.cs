using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;


namespace DataFormManagerApi
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception.Message == "Permission Denied")
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            if (context.Exception.Message == "Error Occurred")
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (context.Exception.Message == "Invalid User")
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            if (context.Exception.Message == "DB Error")
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            }
        }

    }
}