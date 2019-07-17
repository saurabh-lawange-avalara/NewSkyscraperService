using System;
using System.Dynamic;
using System.Net;
using Avalara.Skyscraper.Common;
using Avalara.Skyscraper.Models;
using Avalara.Skyscraper.Services;
using Avalara.Skyscraper.Web.Common;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Avalara.Skyscraper.Web.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeComplianceUser]
    public class WebFileController : BaseController
    {
        private ISkyscraperService _service;
        public WebFileController(ISkyscraperService service, ILog logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(long jobId)
        {
            try
            {
                _logger.Info("In Get WebFile Job response by jobId");
                if (jobId <= 0)
                {
                    _logger.Error("Invalid Job Id");
                    return new JsonResult("Invalid Job Id") { StatusCode = (int)HttpStatusCode.BadRequest };
                }

                var response = _service.GetWebFile(jobId);

                if (response == null)
                {
                    return GetApiResponse(HttpStatusCode.NotFound, string.Format("WebFile job {0} not found", jobId));
                }

                //If job is yet to complete then send 202 with current status details.
                if (!CommonHelper.IsJobComplete(response.Status))
                {
                    dynamic returnObj = new ExpandoObject();
                    returnObj.JobId = response.JobId;
                    returnObj.Status = response.Status.ToString();
                    returnObj.Message = string.Format("WebFile job {0} still processing", response.JobId);
                    returnObj.Error = response.Error;
                    return GetApiResponse(HttpStatusCode.Accepted, returnObj);
                }
                if (response.Status.Equals(JobStatus.FAILED.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return GetApiResponse(HttpStatusCode.BadRequest, response);
                }
                return GetApiResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return GetApiResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }     
    }
}