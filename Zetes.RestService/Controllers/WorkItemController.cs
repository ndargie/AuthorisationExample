using System;
using System.Linq;
using System.Net.Http;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Zetes.RestService.Managers;

namespace Zetes.RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : BaseController
    {
        private readonly IWorkItemManager _workItemManager;

        public WorkItemController(IWorkItemManager workItemManager)
        {
            _workItemManager = workItemManager;
        }


        [Authorize]
        public DataSourceResult GetForList()
        {
            var rawQueryString = HttpContext.Request.QueryString.ToString();
            // PM> Install-Package Microsoft.AspNetCore.WebUtilities
            var rawQueryStringKeyValue = QueryHelpers.ParseQuery(rawQueryString).FirstOrDefault();
            var dataString = Uri.UnescapeDataString(rawQueryStringKeyValue.Key);
          
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(dataString, new JsonSerializerSettings
               {
                   TypeNameHandling = TypeNameHandling.Objects
               });

            var username =  GetUsername();

            return _workItemManager.GetList(request, username);
        }
    }
}