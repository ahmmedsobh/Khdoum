using Khdoum.UI.Helpers;
using Khdoum.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Khdoum.UI.Controllers
{
    public class CategoriesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = new Category();
            model.Categories = (await CategoriesList()).ToList();

            return View(model);
        }

        public async Task<IActionResult> GetList()
        {
            IEnumerable<Category> Categories = await CategoriesList();
            return Json(new
            {
                Categories = Categories
            });
        }

        public async Task<IEnumerable<Category>> CategoriesList()
        {
            IEnumerable<Category> Categories = new List<Category>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/categories");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(json);
            }

            return Categories;
        }

        public async Task<IActionResult> GetSingle(long Id = 0 )
        {
            Category Category = new Category();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/Categories/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Category = JsonConvert.DeserializeObject<Category>(json);
            }

            return Json(new
            {
                Category = Category
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(ModelState.IsValid)
            {
                var Client = new HttpClient();
                
                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(category.Name),"Name");
                content.Add(new StringContent(category.ParentId.ToString()), "ParentId");
                content.Add(new StringContent(category.IsActive.ToString()), "IsActive");
                if(category.Image != null)
                {
                    var FileContent = new StreamContent(category.Image.OpenReadStream());
                    FileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(category.Image.ContentType);
                    FileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "Image",
                        FileName = category.Image.FileName
                    };
                    content.Add(FileContent);
                }
                

                var response =await Client.PostAsync(Constant.BaseAddress + "api/Categories", content);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        Status = true
                    });
                }
            }

            return Json(new
            {
                Status = false
            });
        }
        public async Task<IActionResult> Update(Category category)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(category.ID.ToString()), "ID");
                content.Add(new StringContent(category.Name), "Name");
                content.Add(new StringContent(category.ParentId.ToString()), "ParentId");
                content.Add(new StringContent(category.IsActive.ToString()), "IsActive");
                if(category.Image != null)
                {
                    var FileContent = new StreamContent(category.Image.OpenReadStream());
                    FileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(category.Image.ContentType);
                    FileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "Image",
                        FileName = category.Image.FileName
                    };
                    content.Add(FileContent);
                }
               

                var response = await Client.PutAsync(Constant.BaseAddress + "api/Categories", content);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        Status = true
                    });
                }
            }

            return Json(new
            {
                Status = false
            });
        }
        public async Task<IActionResult> Delete(long Id)
        {
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.DeleteAsync($"{Constant.BaseAddress}api/Categories/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                return Json(new
                {
                    Status = true
                });
            }

            return Json(new
            {
                Status = false
            });
        }
    }
}
