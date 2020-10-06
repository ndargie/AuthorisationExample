using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using Zetes.Dtos;
using Zetes.RestService.Entities.Views;
using Zetes.RestService.Services;

namespace Zetes.RestService.Managers
{
    public class WorkItemManager : IWorkItemManager
    {
        private readonly IRepository _repository;

        public WorkItemManager(IRepository repository)
        {
            _repository = repository;
        }
        public DataSourceResult GetList(DataSourceRequest request, string username)
        {
            var query = _repository.GetAll<ViewWorkItem>().Where(x => x.User == username).Select(x => new WorkItemDto()
            {
                Id = x.Id,
                Customer = x.Customer,
                OrderNumber = x.OrderNumber,
                Quote = x.Quote,
                Status = x.Status
            });
            return query.ToDataSourceResult(request);
        }
    }
}
