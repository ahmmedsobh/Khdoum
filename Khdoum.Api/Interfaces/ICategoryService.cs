using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels.AppViewModels;
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
        Task<IEnumerable<Category>> GetChildCategories(long ParentId);
        Task<IEnumerable<ItemViewModel>> GetFrom1To2LevelCategories();
        Task<Category> GetCategory(long CategoryId);
        Task<Category> AddCategory(Category Category);
        Task<Category> UpdateCategory(Category Category);
        Task<Category> DeleteCategory(long CategoryId);
    }
}
