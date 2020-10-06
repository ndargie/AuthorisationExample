using System;

namespace Zetes.RestService.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime? DateModified { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }

    public interface IViewEntity
    {
        int Id { get; set; }
    }
}
