using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.Dtos;
using TestRestService.Data;
using TestRestService.Manager;
using TestRestService.Models;

namespace TestRestService.Controllers
{
    public class SecretDateController : BaseController
    {
        private IWorkItemManager _workItemManager;

        public SecretDateController(IWorkItemManager workItemManager)
        {
            _workItemManager = workItemManager;
        }

        [Route("/secret")]
        [Authorize]
        public DateTime Index()
        {
            return DateTime.Now.AddDays(5);
        }        

        [Route("/secretdata")]
        [Authorize]
        public DataSourceResult GetTestDataSource([DataSourceRequest]DataSourceRequest request)
        {
            var dataSourceRequest = GetDataSourceRequest();
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

            return testDataModels.ToDataSourceResult(dataSourceRequest);
        }

        [Route("/getworkitems")]
        [Authorize]
        public DataSourceResult GetWorkItems([DataSourceRequest]DataSourceRequest request)
        {
            var dataSourceRequest = GetDataSourceRequest();
            return _workItemManager.GetForList(dataSourceRequest);
        }

        [Route("/updateworkitem")]
        [Authorize]
        public WorkItemDto UpdateWorkItem(WorkItemDto workItemDto)
        {
            return _workItemManager.UpdateWorkItem(workItemDto);
        }
    }
}
