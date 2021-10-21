using Khdoum.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Category>> GetLastLevelCategories();
        Task<Category> GetCategory(long CategoryId);
        Task<Category> AddCategory(Category Category);
        Task<Category> UpdateCategory(Category Category);
        Task<Category> DeleteCategory(long CategoryId);
    }
}
