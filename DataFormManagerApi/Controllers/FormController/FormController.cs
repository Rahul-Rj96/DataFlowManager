using DataFormManager.Models;
using Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;


namespace DataFormManagerApi.Controllers
{
    [RoutePrefix("api/form")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [BasicAuthentication]
    public class FormDataController : ApiController
    {
        [HttpPost,Route("data")]
        public HttpResponseMessage AddFormDataApi(FormDataModel formData)
        {
            string accessToken = Request.Headers.Authorization.Parameter;
            if (PermissionHelper.IsUserHasPermission(accessToken, formData.FormType, "FullAccess"))
            {
                UserObjectModel userObj = TokenHelper.getUserByAccessToken(accessToken);
                FormHelper.AddFormData(formData, userObj.UserId);
                var message = Request.CreateResponse(HttpStatusCode.OK);
                return message;
            }
            else
            {
                var message = Request.CreateResponse(HttpStatusCode.Forbidden, "User does not have the permission to perform this action");
                return message;
            }
                
        }

        [HttpPut, Route("data")]
        public HttpResponseMessage UpdateFormDataApi(FormDataModel formData)
        {
            string accessToken = Request.Headers.Authorization.Parameter;
            if (PermissionHelper.IsUserHasPermission(accessToken, formData.FormType, "Write"))
            {
                FormHelper.UpdateFormData(formData);
                var message = Request.CreateResponse(HttpStatusCode.OK);
                return message;
            }
            else
            {
                var message = Request.CreateResponse(HttpStatusCode.Forbidden,"User does not have the permission to perform this action");
                return message;
            }
        }
            
   }
}


