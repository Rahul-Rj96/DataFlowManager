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
    [RoutePrefix("api/userforms")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserFormsController : ApiController
    {

        [HttpPost]
        public IHttpActionResult AddUserFormsDataApi(UserFormObjectModel userFormsData )
        {
            UserFormsHelper.AddUserFormsData(userFormsData);
            return Ok();
        }
   
    }
    
}
