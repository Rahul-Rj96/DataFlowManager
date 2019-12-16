using DataFormManager.Models;
using Helpers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DataFormManagerApi.Controllers
{
    [RoutePrefix("api/form")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FormDataController : ApiController
    {
        [HttpPost,Route("adddata")]
        public IHttpActionResult AddFormDataApi(FormDataModel formData)
        {
            int formId = FormHelper.AddFormData(formData);
            return Ok(formId);
        }
    }
}


