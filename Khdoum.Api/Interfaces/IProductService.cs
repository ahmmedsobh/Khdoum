using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProducts();
        Task<IEnumerable<ProductViewModel>> GetProductsByCategoryId(long CategoryId);
        Task<ProductViewModel> GetViewProduct(long ProductId);
        Task<Product> GetProduct(long ProductId);
        Task<Product> AddProduct(Product Product);
        Task<Product> UpdateProduct(Product Product);
        Task<Product> DeleteProduct(long ProductId);
    }
}
