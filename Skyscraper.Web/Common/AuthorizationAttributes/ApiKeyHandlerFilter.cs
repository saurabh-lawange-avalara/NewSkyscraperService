using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Collections.Generic;
using Avalara.Skyscraper.Models;
using Avalara.Skyscraper.Services;
using Microsoft.Extensions.DependencyInjection;
using Avalara.Skyscraper.Web.Common.Extensions;

namespace Avalara.Skyscraper.Web.Common
{
    public class ApiKeyHandler : IActionFilter
    {
        AuthHelper auth = null;
        IMemoryCache _memoryCache;
        public ApiKeyHandler(IMemoryCache memoryCache)
        {            
            _memoryCache = memoryCache;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string apiKey = string.Empty;
                auth = new AuthHelper(Startup.Configuration, context.HttpContext.RequestServices.GetService<ISkyscraperService>());
                if (context.HttpContext.Request.Headers.ContainsKey("apikey"))
                {
                    var apiKeyHeader = context.HttpContext.Request.Headers.Where(x => x.Key == "apikey").FirstOrDefault();
                    if (!string.IsNullOrEmpty(apiKeyHeader.Value) && apiKeyHeader.Value.Count() > 0)
                    {
                        apiKey = apiKeyHeader.Value.FirstOrDefault().ToString();
                    }
                }
                //else if (context.Request.RequestUri.AbsolutePath.IndexOf("swagger", StringComparison.InvariantCultureIgnoreCase) >= 0)
                //{

                //if (!string.IsNullOrEmpty(Startup.Configuration["SwaggerAPIKey"]))
                //{
                //    apiKey = Startup.Configuration["SwaggerAPIKey"];
                //}
                //}

                if (!string.IsNullOrEmpty(apiKey))
                {
                    ClientAPIKeysModel obj = new ClientAPIKeysModel();

                    obj = (ClientAPIKeysModel)_memoryCache.Get(apiKey);
                    if (obj == null)
                    {
                        // service.GetAllSkyscraperUsers();
                        obj = auth.GetClientAPIKeyObject(apiKey);
                        if (obj != null)
                        {
                            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                            options.SetAbsoluteExpiration(obj.ExpirationDate.Value);
                            _memoryCache.Set(apiKey, obj, options);
                        }
                    }
                    if (obj != null)
                    {
                        if (obj.ExpirationDate < DateTime.Today)
                        {
                            context.GetCustomizedResponse(HttpStatusCode.Forbidden, "Your API key has expired. Please contact someone from SkyScraper team to provide you with an updated key");
                        }
                        else
                        {
                            context.HttpContext.Items.Add(new KeyValuePair<object, object>("DepartmentId", obj.DepartmentId));
                            context.HttpContext.Items.Add(new KeyValuePair<object, object>("ClientApiKeyId", obj.Id));
                        }

                    }
                    else
                    {
                        context.GetCustomizedResponse(HttpStatusCode.Forbidden, "Your API key is incorrect");
                    }
                }
                // Uncomment this in the March 2018 cycle.==> code uncommented
                else
                {
                    context.GetCustomizedResponse(HttpStatusCode.Forbidden, "API Key required. Please contact Skyscraper team.");
                }

            }
            catch (Exception ex)
            {
                context.GetCustomizedResponse(HttpStatusCode.InternalServerError, ex.Message);
                throw ex;
            }
        }
    }
}
