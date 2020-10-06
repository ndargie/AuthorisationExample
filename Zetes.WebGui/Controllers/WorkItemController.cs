using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Zetes.Dtos;
using Zetes.WebGui.Consumers;
using Zetes.WebGui.Controllers.Shared;
using Zetes.WebGui.Models;

namespace Zetes.WebGui.Controllers
{
    public class WorkItemController : BaseController
    {
        private readonly IRemoteConsumer _remoteConsumer;

        public WorkItemController(IRemoteConsumer remoteConsumer)
        {
            _remoteConsumer = remoteConsumer;
        }


     
        [Authorize]
        public async Task<IActionResult> List()
        {
            string username = GetUsername();
            string authToken = await GetAccessToken();
            return View("_List");
        }

        public async Task<IActionResult> GetForList([DataSourceRequest]DataSourceRequest request)
        {
            var parameters = JsonConvert.SerializeObject(request, Formatting.None, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
            
            var records = await _remoteConsumer.GetDate<DataSourceResultDto<WorkItemDto>>($"WorkItem/GetForList?{parameters}", await GetAccessToken());
            return Json(records);
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
    }
}
