using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestService.Models
{
    [ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))]
    public class DataSourceRequestWithBinding : DataSourceRequest
    {
    }
}
