using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;

namespace Avalara.Skyscraper.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ILog _logger;
        JsonSerializerSettings _serializer;
        public BaseController()
        {
            if (_serializer == null)
            {
                _serializer = new JsonSerializerSettings();
                _serializer.Converters.Add(new StringEnumConverter());
                _serializer.NullValueHandling = NullValueHandling.Include;
                _serializer.DefaultValueHandling = DefaultValueHandling.Include;
            }
        }

        protected JsonResult GetApiResponse(HttpStatusCode statusCode, object response)
        {
            return new JsonResult(response) { StatusCode = (int)statusCode };
        }
    }
}
