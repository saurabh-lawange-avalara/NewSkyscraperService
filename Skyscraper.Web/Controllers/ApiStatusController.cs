using System;
using System.Net;
using Avalara.Skyscraper.Services;
using Avalara.Skyscraper.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Avalara.Skyscraper.Web.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeComplianceUser]
    public class ApiStatusController : Controller
    {
        private ISkyscraperService _service;
        public ApiStatusController(ISkyscraperService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(string apiName = null, string method = null)
        {
            try
            {
                var apiStatus = _service.GetApiStatus(apiName, method);

                //return 404 if no record present for these parameters.
                if (apiStatus == null || apiStatus.Count == 0)
                {
                    return new JsonResult("No record found.") { StatusCode = (int)HttpStatusCode.NotFound };
                }
                return new JsonResult(apiStatus);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }
    }
}