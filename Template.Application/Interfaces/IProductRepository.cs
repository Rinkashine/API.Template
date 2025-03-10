using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities;

namespace Template.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Product?> GetByIdAsync(Guid id);

        Task<Product> CreateAsync(Product obj);
        Task<Product?> UpdateAsync(Guid id, Product obj);
        Task<Product?> DeleteAsync(Guid id);
    }
}
