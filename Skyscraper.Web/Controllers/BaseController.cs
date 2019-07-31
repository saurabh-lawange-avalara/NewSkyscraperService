using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
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

        protected int DepartmentId
        {
            get
            {
                try
                {
                    object _deptid;
                    if (!Request.HttpContext.Items.TryGetValue("DepartmentId", out _deptid))
                    {
                        return 0;
                    }
                    else
                    {
                        return (int)_deptid;
                    }
                }
                catch
                {
                    _logger.Error("DepartmentId not found in request.");
                    throw new Exception("Please make sure you sent a valid api key");
                }
            }

        }

        protected int ClientApiKeyId
        {
            get
            {
                try
                {
                    object _apiKeyId;
                    if (!Request.HttpContext.Items.TryGetValue("ClientApiKeyId", out _apiKeyId))
                    {
                        return 0;
                    }
                    else
                    {
                        return (int)_apiKeyId;
                    }
                }
                catch
                {
                    _logger.Error("ClientApiKeyId not found in request.");
                    throw new Exception("Please make sure you sent a valid api key");
                }
            }

        }


    }
}
