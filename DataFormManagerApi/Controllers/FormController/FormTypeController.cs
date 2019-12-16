using DataFormManager.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DataFormManagerApi.Controllers.FormController
{
    [RoutePrefix("api/formtype")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FormTypeController : ApiController
    {
        [HttpGet, Route("{formName}")]
        public FormTypeModel GetFormTypeApi(String formName)
        {
            return(FormTypeHelper.GetFormFields(formName));
        }
    }
}
