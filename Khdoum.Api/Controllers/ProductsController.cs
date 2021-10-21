using Khdoum.Api.Helpers;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService Products;
        private readonly UploadImages uploadImages;

        public ProductsController(IProductService Products , UploadImages uploadImages)
        {
            this.Products = Products;
            this.uploadImages = uploadImages;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                return Ok(await Products.GetProducts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var result = await Products.GetProduct(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{GetViewProduct}/{id:int}")]
        public async Task<ActionResult<ProductViewModel>> GetViewProduct(int id)
        {
            try
            {
                var result = await Products.GetViewProduct(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromForm]ProductViewModel product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var ProductToAdd = new Product()
                {
                    Name = product.Name,
                    Price = product.Price,
                    IsActive = product.IsActive,
                    CategoryId = product.CategoryId,
                    UnitId = product.UnitId
                };

                ProductToAdd.ImgUrl = uploadImages.AddImage(product.Image);

                var createdProduct = await Products.AddProduct(ProductToAdd);

                return CreatedAtAction(nameof(GetProducts),
                    new { id = createdProduct.ID }, createdProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new product record");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct([FromForm] ProductViewModel product)
        {
            try
            {
                

                var productToUpdate = await Products.GetProduct(product.ID);

                if (productToUpdate == null)
                    return NotFound($"Product with Id = {product.ID} not found");

                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.IsActive = product.IsActive;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.UnitId = product.UnitId;
                productToUpdate.ImgUrl = uploadImages.UpdateImage(productToUpdate.ImgUrl, product.Image);

                return await Products.UpdateProduct(productToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(long id)
        {
            try
            {
                var productToDelete = await Products.GetProduct(id);

                if (productToDelete == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }
                uploadImages.DeleteImage(productToDelete.ImgUrl);
                return await Products.DeleteProduct(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
