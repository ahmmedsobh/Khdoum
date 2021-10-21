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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService Categories;
        private readonly UploadImages uploadImages;

        public CategoriesController(ICategoryService Categories, UploadImages uploadImages)
        {
            this.Categories = Categories;
            this.uploadImages = uploadImages;
            uploadImages.Folder = "Categories";
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                return Ok(await Categories.GetCategories());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("GetLastLevelCategories")]
        public async Task<ActionResult> GetLastLevelCategories()
        {
            try
            {
                return Ok(await Categories.GetLastLevelCategories());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var result = await Categories.GetCategory(id);

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
        public async Task<ActionResult<CategoriesViewModel>> CreateCategory([FromForm] CategoriesViewModel category)
        {
            try
            {
                if (category == null)
                    return BadRequest();

                var CategoryToAdd = new Category()
                {
                    Name = category.Name,
                    ParentId = category.ParentId,
                    LevelStatus = category.LevelStatus,
                    IsActive = category.IsActive
                };


                CategoryToAdd.ImgUrl = uploadImages.AddImage(category.Image);

                var createdCategory = await Categories.AddCategory(CategoryToAdd);

                return CreatedAtAction(nameof(GetCategories),
                    new { id = createdCategory.ID }, createdCategory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new category record");
            }
        }

        //[HttpPut("{id:int}")]
        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory([FromForm] CategoriesViewModel category)
        {
            try
            {
              

                var categoryToUpdate = await Categories.GetCategory(category.ID);

                if (categoryToUpdate == null)
                    return NotFound($"Category with Id = {category.ID} not found");

                categoryToUpdate.Name = category.Name;
                categoryToUpdate.ParentId = category.ParentId;
                categoryToUpdate.LevelStatus = category.LevelStatus;
                categoryToUpdate.IsActive = category.IsActive;
                categoryToUpdate.ImgUrl = uploadImages.UpdateImage(categoryToUpdate.ImgUrl, category.Image);

                return await Categories.UpdateCategory(categoryToUpdate);
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            try
            {
                var categoryToDelete = await Categories.GetCategory(id);

                if (categoryToDelete == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }
                uploadImages.DeleteImage(categoryToDelete.ImgUrl);
                return await Categories.DeleteCategory(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
