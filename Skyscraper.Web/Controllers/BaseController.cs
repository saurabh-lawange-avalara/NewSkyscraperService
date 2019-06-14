using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalara.Skyscraper.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ILog _logger;
    }
}
