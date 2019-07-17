using System;
using System.Net;
using Avalara.Skyscraper.Services;
using Avalara.Skyscraper.Web.Common;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Avalara.Skyscraper.Web.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeComplianceUser]
    public class ApiStatusController : BaseController
    {
        private ISkyscraperService _service;
        public ApiStatusController(ISkyscraperService service, ILog logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string apiName = null, string method = null)
        {
            try
            {
                _logger.Info("In Get ApiStatus");
                var apiStatus = _service.GetApiStatus(apiName, method);
                _logger.Info("Got ApiStatus");
                //return 404 if no record present for these parameters.
                if (apiStatus == null || apiStatus.Count == 0)
                {
                    return GetApiResponse(HttpStatusCode.NotFound, "No record found.");
                }
                return GetApiResponse(HttpStatusCode.OK, apiStatus);
            }
            catch (Exception ex)
            {
                return GetApiResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}