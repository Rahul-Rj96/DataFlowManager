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
    [BasicAuthentication]
    [RoutePrefix("api/formtype")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    public class FormTypeController : ApiController
    {
        [HttpGet, Route("{formName}")]
        public FormTypeModel GetFormTypeApi(String formName)
        {
            try
            {
                return (FormTypeHelper.GetFormFields(formName));
            }
            catch
            {
                throw new Exception("Error Occurred");
            }
            
        }
    }
}
