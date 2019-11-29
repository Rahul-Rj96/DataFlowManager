using DataFormManager.Models;
using Helpers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DataFormManagerApi.Controllers
{
    [RoutePrefix("api/form")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReleaseFormController : ApiController
    {
        [HttpPost, Route("releaseform")]
        public IHttpActionResult AddReleaseDataApi(ReleaseObjectModel releaseData)
        {
            int formId = ReleaseFormHelper.AddReleaseData(releaseData);
            return Ok(formId);
        }
    }
}


