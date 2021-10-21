using Khdoum.Api.Data;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Servicies
{
    public class CategoryService: ICategoryService
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Category> AddCategory(Category Category)
        {
            var ParentCategory = await context.Categories.FirstOrDefaultAsync(c => c.ID == Category.ParentId);
            if(ParentCategory != null)
            {
                ParentCategory.LevelStatus = true;
            }
            var result = await context.Categories.AddAsync(Category);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Category> DeleteCategory(long CategoryId)
        {
            var result = await context.Categories
                .FirstOrDefaultAsync(c => c.ID == CategoryId);
            if (result != null)
            {
                var ChildCategories = await context.Categories.Where(c => c.ParentId == CategoryId).ToListAsync();
                if(ChildCategories == null)
                {
                    context.Categories.Remove(result);
                    await context.SaveChangesAsync();
                }
               
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(long CategoryId)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.ID == CategoryId);
        }

        public async Task<IEnumerable<Category>> GetLastLevelCategories()
        {
            return await context.Categories.Where(c=>c.LevelStatus == false).ToListAsync();
        }


        public async Task<Category> UpdateCategory(Category Category)
        {
            var result = await context.Categories
                .FirstOrDefaultAsync(c => c.ID == Category.ID);

            if (result != null)
            {
                var ParentCategory = await context.Categories.FirstOrDefaultAsync(c => c.ID == Category.ParentId);
                if(ParentCategory != null)
                {
                    ParentCategory.LevelStatus = true;
                }
                context.Update(Category);
                await context.SaveChangesAsync();
                return Category;
            }

            return null;
        }

        //functions

        
    }
}
