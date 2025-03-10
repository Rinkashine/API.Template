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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Category> CreateAsync(Category obj)
        {
            await _db.Categories.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
            
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {

            Category? categories = await _db.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if(categories is null)
            {
                return null;
            }
            _db.Categories.Remove(categories);
            await _db.SaveChangesAsync();
            return categories;
        }

        public async Task<Category?> UpdateAsync(Category obj, Guid id)
        {
            Category? categories = await _db.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (categories is null)
            {
                return null;
            }
            categories.Name = obj.Name;
            await _db.SaveChangesAsync();
            return categories;
        }
    }
}
