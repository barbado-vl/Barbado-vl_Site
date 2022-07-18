using Barbado_vl_Site.Domain.Entities;
using System;
using System.Linq;

namespace Barbado_vl_Site.Domain.Repositories.Abstract
{
    public interface IServiceItemsRepository
    {
        IQueryable<ServiceItem> GetSericeItems();
        ServiceItem GetSericeItemsById(Guid id);
        void SaveSericeItems(ServiceItem entity);
        void DeleteSericeItems(Guid id);
    }
}
