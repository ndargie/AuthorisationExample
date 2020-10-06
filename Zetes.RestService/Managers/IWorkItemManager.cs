using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Zetes.RestService.Managers
{
    public interface IWorkItemManager
    {
        DataSourceResult GetList(DataSourceRequest request, string username);
    }
}
