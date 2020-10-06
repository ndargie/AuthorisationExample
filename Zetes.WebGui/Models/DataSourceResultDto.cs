using Kendo.Mvc.Infrastructure;
using System.Collections.Generic;

namespace Zetes.WebGui.Models
{
    public class DataSourceResultDto<T>
    {
        public IEnumerable<AggregateResult> AggregateResults { get; set; }
        public IEnumerable<T> Data { get; set; }
        public object Error { get; set; }
        public int Total { get; set; }
    }
}
