using DataFormManager.Models;
using Helpers;
using System;
using System.Collections.Generic;
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
        [HttpPost, Route("add")]
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

        [HttpPut, Route("update")]
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


        [HttpDelete, Route("{formId}")]
        public HttpResponseMessage DeleteFormDataApi(int formId)
        {
            string formType = FormHelper.GetFormTypeNameUsingId(formId);
            string accessToken = Request.Headers.Authorization.Parameter;
            if (PermissionHelper.IsUserHasPermission(accessToken,formType , "FullAccess"))
            {
                FormHelper.DeleteFormData(formId);
                var message = Request.CreateResponse(HttpStatusCode.OK);
                return message;
            }
            else
            {
                var message = Request.CreateResponse(HttpStatusCode.Forbidden, "User does not have the permission to perform this action");
                return message;
            }

        }

        [HttpGet, Route("{formName}")]
        public HttpResponseMessage GetFormsToAssignApi(string formName)
        {
            string accessToken = Request.Headers.Authorization.Parameter;
            if (PermissionHelper.IsUserHasPermission(accessToken, formName, "FullAccess"))
            {
                List<FormDataModel> dataList = FormHelper.GetFormsToAssign();
                var message = Request.CreateResponse(HttpStatusCode.OK, dataList);
                return message;
            }
            else
            {
                var message = Request.CreateResponse(HttpStatusCode.Forbidden, "User does not have the permission to perform this action");
                return message;
            }
        }
        
        [HttpGet,Route("users/{formType}")]
        public HttpResponseMessage GetUsersToAssignApi(string formType)
        {

            string accessToken = Request.Headers.Authorization.Parameter;
            if (PermissionHelper.IsUserHasPermission(accessToken, formType, "FullAccess"))
            {
                List<UserIdNamemodel> userList = FormHelper.GetUsersToAssign();
                var message = Request.CreateResponse(HttpStatusCode.OK, userList);
                return message;
            }
            else
            {
                var message = Request.CreateResponse(HttpStatusCode.Forbidden, "User does not have the permission to perform this action");
                return message;
            }
        }
    }

}


