using IdentityModel.Client;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Test.Dtos;
using TestFrontEnd.Consumer;
using TestFrontEnd.ViewModels;

namespace TestFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRemoteConsumer _remoteConsumer;

        public HomeController(IRemoteConsumer remoteConsumer)
        {
            _remoteConsumer = remoteConsumer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var content = _remoteConsumer.GetData<DateTime>("https://localhost:44338/secret", accessToken);
            return View(new DateModel() { CurrentDate = content });
        }

        [Authorize]
        public IActionResult SecretData()
        {
            return View();
        }

        public JsonResult GetSecretData([DataSourceRequest]DataSourceRequest request)
        {
            var accessToken = HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();
            var parameters = JsonConvert.SerializeObject(request, Formatting.None, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
            
            var data = _remoteConsumer.
                GetData<DataSourceResultDto<TestDataModel>>($"https://localhost:44338/secretdata?{parameters}", accessToken);
            return Json(data);
        }

        [Authorize]
        public IActionResult WorkItemData()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetWorkItems([DataSourceRequest]DataSourceRequest request)
        {
            var accessToken = HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();
            var parameters = JsonConvert.SerializeObject(request, Formatting.None, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
            var data = _remoteConsumer
                .GetData<DataSourceResultDto<WorkItemDto>>($"https://localhost:44338/getworkitems?{parameters}", accessToken);
            return Json(data);
        }

        public ActionResult UpdateWorkItem(WorkItemDto workItemDto)
        {
            var accessToken = HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();
            var data = _remoteConsumer.PostData<WorkItemDto>("https://localhost:44338/updateworkitem", accessToken, workItemDto);
            return Json(data);
        }

        public IActionResult Logout()
        {
            return SignOut("Cookie", "oidc");
        }

        public IActionResult GetSecretDataTest([DataSourceRequest]DataSourceRequest request)
        {
            List<TestDataModel> testDataModels = new List<TestDataModel>()
            {
                new TestDataModel()
                {
                    Id = 1, Description = "Item number 1", Name = "Item1"
                },
                new TestDataModel()
                {
                    Id = 2, Description = "Item number 2", Name = "Item2"
                },
                new TestDataModel()
                {
                    Id = 3, Description = "Item number 3", Name = "Item3"
                },
                new TestDataModel()
                {
                    Id = 4, Description = "Item number 4", Name = "Item4"
                },
            };
            return Json(testDataModels.ToDataSourceResult(request));
        }
    }
}
