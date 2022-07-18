using Barbado_vl_Site.Domain.Entities;
using Barbado_vl_Site.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Barbado_vl_Site.Domain.Repositories.EntityFramework
{
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        private readonly AppDbContext context;

        public EFServiceItemsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ServiceItem> GetSericeItems()
        {
            return context.ServiceItems;
        }

        public ServiceItem GetSericeItemsById(Guid id)
        {
            return context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveSericeItems(ServiceItem entity)
        {
            // если Id, тип Guid, пустой, т.е. равен структуре default из system.Guid, то ставим фалг добавить новую запись
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;

            context.SaveChanges();
        }

        public void DeleteSericeItems(Guid id)
        {
            // удалять нечего, поэтому создаем новый ServiceItem и его же удаляем
            context.ServiceItems.Remove(new ServiceItem() { Id = id });
            context.SaveChanges();
        }

    }
}
