using Avalara.Skyscraper.Models;
using Avalara.Skyscraper.Services;
using Avalara.Skyscraper.Web.Common;
using Avalara.Skyscraper.Web.Common.Extensions;
using log4net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Skyscraper.Web.Common.Filters
{
    public class LoggingFilter : IActionFilter
    {
        //public Log logger { get; set; }
        private DateTime starttime;
        IMemoryCache _memoryCache;
        ILog _logger;
        ISkyscraperService _skyscraperService;

        public LoggingFilter(IMemoryCache memoryCache, ISkyscraperService skyscraperService, ILog logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _skyscraperService = skyscraperService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Log the Action method entry
            string controllerName = context.ActionDescriptor.RouteValues["controller"];
            string controllerAction = context.ActionDescriptor.RouteValues["action"];

            context.HttpContext.Items[controllerAction] = DateTime.Now;

            string APICallerContext = string.Empty;
            if (context.HttpContext.Request.Headers.Any(x => x.Key == "apikey"))
            {
                var apivalue = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "apikey").Value;
                if (!string.IsNullOrEmpty(apivalue) && apivalue.Count() > 0)
                {
                    ServiceModel_ClientAPIKeys obj = new ServiceModel_ClientAPIKeys();
                    obj = (ServiceModel_ClientAPIKeys)_memoryCache.Get(apivalue.FirstOrDefault());
                    if (obj != null)
                    {
                        APICallerContext = obj.ToString();
                    }
                }
            }
            string loginfo = string.Format("{{ APICallerContext:'{3}', Controller:'{0}', Action:'{1}', " +
                "ApiName:{0}_{1}, Event:'Start', DateTime:'{2}' }}", 
                controllerName, controllerAction, DateTime.Now.ToString(), APICallerContext);            
            _logger.Info(loginfo);

            //check api status
            var apiStatus = _skyscraperService.GetApiStatus(apiName: controllerName.Replace("Controller", ""), method: controllerAction);

            if (apiStatus != null && apiStatus.Count > 0 && apiStatus.FirstOrDefault().Status == false)
            {
                context.GetCustomizedResponse(HttpStatusCode.Forbidden, "API temporarily suspended for maintenence. Please try after sometime.");
            }
        }

        public void OnActionExecuted(ActionExecutedContext Context)
        {
            string controllerName = Context.ActionDescriptor.RouteValues["controller"];
            string controllerAction = Context.ActionDescriptor.RouteValues["action"];

            DateTime datetiemObj = (DateTime)Context.HttpContext.Items[controllerAction];
            TimeSpan elapsedTime = DateTime.Now - datetiemObj;

            string loginfo = string.Format("{{ Controller:'{0}', Action:'{0}-{1}', ApiName:{0}_{1}, " +
                "Event:'End', DateTime:'{2}', Elapsed:'{3}'}}", 
                controllerName, controllerAction, DateTime.Now.ToString(), elapsedTime.Milliseconds);
            _logger.Info(loginfo);
        }
    }
}
