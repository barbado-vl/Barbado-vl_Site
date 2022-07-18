using Barbado_vl_Site.Domain.Entities;
using Barbado_vl_Site.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Barbado_vl_Site.Domain.Repositories.EntityFramework
{
    public class EFTextFieldsRepository : ITextFieldsRepository
    {
        private readonly AppDbContext context;

        public EFTextFieldsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<TextField> GetTextFields()
        {
            return context.TextFields;
        }
        public TextField GetTextFieldsById(Guid id)
        {
            return context.TextFields.FirstOrDefault(x => x.Id == id);
        }

        public TextField GetTextByCodeWord(string codeWord)
        {
            return context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
        }

        public void SaveTextField(TextField entity)
        {
            // если Id, тип Guid, пустой, т.е. равен структуре default из system.Guid, то ставим фалг добавить новую запись
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;

            context.SaveChanges();
        }

        public void DeleteTextField(Guid id)
        {
            // удалять нечего, поэтому создаем новый TextField и его же удаляем
            context.TextFields.Remove(new TextField() { Id = id });
            context.SaveChanges();
        }

    }
}
