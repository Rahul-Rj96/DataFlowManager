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
    public class GetUserFormListController : ApiController
    {
        [HttpGet, Route("form")]
        public HttpResponseMessage GetUserFormDatasApi()
        {
            string accessToken = Request.Headers.Authorization.Parameter;
            UserObjectModel userObj = TokenHelper.getUserByAccessToken(accessToken);
            if (userObj != null)
            {
                List<FormDataModel> dataList = UserSpecificFormsHelper.GetUserFormDataList(userObj.UserId);
                var message = Request.CreateResponse(HttpStatusCode.OK, dataList);
                return message;
            }
            else
            {
                var message = Request.CreateResponse(HttpStatusCode.NotFound, "Invalid User");
                return message;
            }
        }
    }
}
