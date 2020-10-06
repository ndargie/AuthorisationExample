using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestService.Controllers
{
    public class BaseController : ControllerBase
    {
        protected DataSourceRequest GetDataSourceRequest()
        {
            var queryString = HttpContext.Request.QueryString.ToString();
            var rawString = QueryHelpers.ParseQuery(queryString).FirstOrDefault();
            var dataString = Uri.UnescapeDataString(rawString.Key);
            var dataSourceRequest = JsonConvert.DeserializeObject<DataSourceRequest>(dataString,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                });
            return dataSourceRequest;
        }

        protected string GetUsername()
        {
            return HttpContext.User?.Identity.Name;
        }
    }
}
