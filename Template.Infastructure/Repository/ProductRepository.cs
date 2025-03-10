using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Data;
using Template.Application.Interfaces;
using Template.Domain.Entities;

namespace Template.Infastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Product> CreateAsync(Product obj)
        {
            await _db.Products.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            Product? product = await _db.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (product is not null) 
            {
                _db.Remove(product);
                await _db.SaveChangesAsync();
                return product;
            }
            return null;
        }

        public async Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var product = _db.Products.Include(a => a.Category).AsQueryable();
            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrEmpty(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    product = product.Where(i => i.Name.Contains(filterQuery));
                }
            }
            //Sort 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    product = isAscending ? product.OrderBy(i => i.Name) : product.OrderByDescending(i => i.Name);
                }
                else if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    product = isAscending ? product.OrderBy(i => i.Price) : product.OrderByDescending(i => i.Price);
                }
            }
            //Paginate
            var skipResults = (pageNumber - 1) * pageSize;

            return await product.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _db.Products.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Product?> UpdateAsync(Guid id, Product obj)
        {

            Product? product = await _db.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (product is null) 
            {
                return null;
            }
            product.Name = obj.Name;
            product.Price = obj.Price;
            product.CategoryId = obj.CategoryId;
            await _db.SaveChangesAsync();
            return product;

        }
    }
}
