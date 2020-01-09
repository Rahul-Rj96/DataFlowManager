using DataFormManager.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DataFormManagerApi.Controllers
{
    [BasicAuthentication]
    [RoutePrefix("api/userspecificforms")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    public class GetUserFormListController : ApiController
    {
        [HttpGet, Route("{formName}")]
        public HttpResponseMessage GetUserFormDatasApi(string formName)
        {
            string accessToken = Request.Headers.Authorization.Parameter;
            UserObjectModel userObj = TokenHelper.getUserByAccessToken(accessToken);
            if (userObj != null)
            {
                List<FormDataModel> dataList = UserSpecificFormsHelper.GetUserFormDataList(userObj.UserId,formName);
                var message = Request.CreateResponse(HttpStatusCode.OK, dataList);
                return message;
            }
            else
            {
                throw new Exception("Invalid User");
            }
        }

        [HttpGet, Route("{formName}/{start}/{count}")]
        public HttpResponseMessage GetLimitedUserFormDatasApi(string formName,string start, string count)
        {
            string accessToken = Request.Headers.Authorization.Parameter;
            UserObjectModel userObj = TokenHelper.getUserByAccessToken(accessToken);
            if (userObj != null)
            {
                List<FormDataModel> dataList = UserSpecificFormsHelper.GetLimitedUserFormDataList(userObj.UserId, formName,start,count);
                var message = Request.CreateResponse(HttpStatusCode.OK, dataList);
                return message;
            }
            else
            {
                throw new Exception("Invalid User");
            }
        }
    }
}
