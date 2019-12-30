using DataFormManager.Models;
using Helpers;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
            UserObjectModel userObj = TokenHelper.getUserByAccessToken(accessToken);
            FormHelper.AddFormData(formData,userObj.UserId);
            var message = Request.CreateResponse(HttpStatusCode.OK);
            return message;
        }

        [HttpPut, Route("data")]
        public HttpResponseMessage UpdateFormDataApi(FormDataModel formData)
        {
            FormHelper.UpdateFormData(formData);
            var message = Request.CreateResponse(HttpStatusCode.OK);
            return message;
            
        }
    }
}


