using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zetes.RestService.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        protected string GetUsername()
        {
            return HttpContext.User.Identity.Name;
        }

    }
}
