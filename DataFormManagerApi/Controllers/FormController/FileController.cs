using DataFormManager.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
namespace DataFormManagerApi.Controllers.FormController
{
    [BasicAuthentication]
    [RoutePrefix("api/file")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    public class FileController : ApiController
    {
        [HttpPost, Route("create")]
        public HttpResponseMessage InsertFileApi()
        {
            var file = System.Web.HttpContext.Current.Request.Files;
            string formType = file.Keys[0];
            string accessToken = Request.Headers.Authorization.Parameter;
            if (PermissionHelper.IsUserHasPermission(accessToken, formType, "Write"))
            {
                int fileId = FormHelper.InsertFile(file);
                var message = Request.CreateResponse(HttpStatusCode.Created, fileId.ToString());
                return message;
            }
            else
            {
                throw new Exception("Permission Denied");
            }
    
        }

        [HttpPost, Route("edit/{id}")]
        public HttpResponseMessage EditFileApi(string id)
        {
            
            var file = System.Web.HttpContext.Current.Request.Files;
            string formType = file.Keys[0];
            string accessToken = Request.Headers.Authorization.Parameter;
            if (PermissionHelper.IsUserHasPermission(accessToken, formType, "Write"))
            {
                FormHelper.EditFile(file,Int32.Parse(id));
                var message = Request.CreateResponse(HttpStatusCode.OK,"changed");
                return message;
            }
            else
            {
                throw new Exception("Permission Denied");
            }

        }

        [HttpGet, Route("read/{id}")]
        public HttpResponseMessage ReadFileApi(string id)
        {

            string accessToken = Request.Headers.Authorization.Parameter;
            if (PermissionHelper.IsUserHasPermission(accessToken, "Bill", "Write"))
            {
                fileObj file = FormHelper.GetFile(id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                response.Content = new ByteArrayContent(file.fileRecord);

                //Set the Response Content Length.
                response.Content.Headers.ContentLength = file.fileRecord.LongLength;

                //Set the Content Disposition Header Value and FileName.
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = file.fileName;

                //Set the File Content Type.
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(file.fileType);
                return response;
            }
            else
            {
                throw new Exception("Permission Denied");
            }

        }

    }
    }
//var f = file[0];
//var filePath = @"Z:\" + f.FileName;
//f.SaveAs(filePath);
//string a = file.ToString();

//var c = file[0].ContentType;
//var d = file[0].FileName;
//Stream f = file[0].InputStream;
//var e = file[0].ContentLength;
