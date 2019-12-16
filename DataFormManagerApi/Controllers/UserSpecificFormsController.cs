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
        [HttpGet, Route("{userId}")]
        public IEnumerable<ReleaseObjectModel> GetUserFormDatasApi(int userId)
        {
            List<ReleaseObjectModel> dataList = UserSpecificFormsHelper.GetUserFormDataList(userId);
            return dataList;
        }
    }
}
