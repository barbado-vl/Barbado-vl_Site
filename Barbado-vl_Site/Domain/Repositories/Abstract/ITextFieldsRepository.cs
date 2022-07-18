using Barbado_vl_Site.Domain.Entities;
using System;
using System.Linq;

namespace Barbado_vl_Site.Domain.Repositories.Abstract
{
    public interface ITextFieldsRepository
    {
        IQueryable<TextField> GetTextFields();
        TextField GetTextFieldsById(Guid id);
        TextField GetTextByCodeWord(string codeWord);
        void SaveTextField(TextField entity);
        void DeleteTextField(Guid id);
    }
}
