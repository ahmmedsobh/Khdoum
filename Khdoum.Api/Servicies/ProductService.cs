using Khdoum.Api.Data;
using Khdoum.Api.Helpers;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Servicies
{
    public class ProductService:IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> AddProduct(Product Product)
        {
            var result = await context.Products.AddAsync(Product);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> DeleteProduct(long ProductId)
        {
            var result = await context.Products
                .FirstOrDefaultAsync(p => p.ID == ProductId);
            if (result != null)
            {
                context.Products.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Product> GetProduct(long ProductId)
        {
            return await context.Products.FirstOrDefaultAsync(p=>p.ID == ProductId);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProducts()
        {
            var products = (from p in context.Products
                            from c in context.Categories
                            from u in context.Units
                            where p.CategoryId == c.ID && p.UnitId == u.ID
                            select new ProductViewModel
                            {
                                ID = p.ID,
                                Name = p.Name,
                                Price = p.Price,
                                ImgUrl = p.ImgUrl,
                                IsActive = p.IsActive,
                                CategoryId = p.CategoryId,
                                UnitId = p.UnitId,
                                CategoryName = c.Name,
                                UnitName = u.Name,
                            }).ToListAsync();

            return await products;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsByCategoryId(long CategoryId)
        {
            var products = (from p in context.Products
                            from m in context.Users
                            from mp in context.MarketProducts
                            from c in context.Categories
                            from u in context.Units
                            from s in context.States
                            where p.CategoryId == c.ID && p.UnitId == u.ID && p.CategoryId == CategoryId
                            && mp.ProductId == p.ID && mp.UserId == m.Id 
                            && m.StateId == s.ID 
                            select new ProductViewModel
                            {
                                ID = mp.ID,
                                Name = p.Name,
                                Price = mp.Price,
                                ImgUrl = p.ImgUrl == "false"?$"{Constants.BaseAddress}Uploads/default.png": $"{Constants.BaseAddress}Uploads/Products/{p.ImgUrl}",
                                IsActive = p.IsActive,
                                CategoryId = p.CategoryId,
                                UnitId = p.UnitId,
                                CategoryName = c.Name,
                                UnitName = u.Name,
                                QuantityDuration = p.QuantityDuration,
                                MarketId = mp.UserId,
                                MarketName = m.Name,
                                ProductId = p.ID,
                                StateName = s.Name,
                                StateId = s.ID,
                                DeliveryService = s.DeliveryService
                            }).ToListAsync();

            return await products;
        }

        public async Task<ProductViewModel> GetViewProduct(long ProductId)
        {
            var product = (from p in context.Products
                           from c in context.Categories
                           from u in context.Units
                           where p.CategoryId == c.ID && p.UnitId == u.ID && p.ID == ProductId
                           select new ProductViewModel
                           {
                               ID = p.ID,
                               Name = p.Name,
                               Price = p.Price,
                               ImgUrl = p.ImgUrl,
                               IsActive = p.IsActive,
                               CategoryId = p.CategoryId,
                               UnitId = p.UnitId,
                               CategoryName = c.Name,
                               UnitName = u.Name,
                               QuantityDuration = p.QuantityDuration
                           }).FirstOrDefaultAsync();

            return await product;
        }

        public async Task<Product> UpdateProduct(Product Product)
        {
            var result = await context.Products
               .FirstOrDefaultAsync(p => p.ID == Product.ID);

            if (result != null)
            {
                context.Update(Product);
                await context.SaveChangesAsync();
                return Product;
            }

            return null;
        }

        
    }
}
