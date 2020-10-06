using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Dtos;

namespace TestRestService.Manager
{
    public interface IWorkItemManager
    {
        DataSourceResult GetForList(DataSourceRequest request);
        WorkItemDto UpdateWorkItem(WorkItemDto workItemDto);
    }
}
