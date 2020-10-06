using EntityFrameWorkHelper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using Test.Dtos;
using TestRestService.Data;
using TestRestService.Entities;

namespace TestRestService.Manager
{
    public class WorkItemManager : IWorkItemManager
    {
        private readonly WorkItemContext _workItemContext;

        public WorkItemManager(WorkItemContext workItemContext)
        {
            _workItemContext = workItemContext;
        }

        public DataSourceResult GetForList(DataSourceRequest request)
        {
            var query = _workItemContext.WorkItems
                .Select(x => new WorkItemDto() { 
                    Id = x.Id, 
                    Description = x.Description, 
                    OrderNumber = x.OrderNumber, 
                    Quote = x.Quote, 
                    InvoicePaid = x.InvoicePaid, 
                    InvoiceRaised = x.InvoiceRaised, 
                    Finished = x.Finished, 
                    Started = x.Started, 
                    QuoteRaised = x.QuoteRaised 
                });
            return query.ToDataSourceResult(request);
        }

        public WorkItemDto UpdateWorkItem(WorkItemDto workItemDto)
        {
            var workItem = _workItemContext.WorkItems.SingleOrDefault(x => x.Id == workItemDto.Id);
            if(workItem != null)
            {
                workItem.Quote = workItemDto.Quote;
                workItem.Description = workItemDto.Description;
            }
            return workItemDto;
        }
    }
}
