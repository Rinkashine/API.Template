using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities;


namespace Template.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category> CreateAsync(Category obj);
        Task<Category?> UpdateAsync(Category obj, Guid id);
        Task<Category?> DeleteAsync(Guid id);
    }
}
