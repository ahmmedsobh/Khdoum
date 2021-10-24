using Khdoum.UI.Helpers;
using Khdoum.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Khdoum.UI.Controllers
{
    public class ProductsController : Controller
    {
       

        public async Task<IActionResult> Index()
        {
            var model = new Product();
            model.Categories = (await CategoriesList()).ToList();
            model.Units = (await UnitsList()).ToList();
            return View(model);
        }

        public async Task<IActionResult> GetList()
        {
            IEnumerable<Product> Products = await ProductsList();
            return Json(new
            {
                Products = Products
            });
        }

        public async Task<IEnumerable<Product>> ProductsList()
        {
            IEnumerable<Product> Products = new List<Product>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/products");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
            }

            return Products;
        }

        public async Task<IEnumerable<Category>> CategoriesList()
        {
            IEnumerable<Category> Categories = new List<Category>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/categories/GetLastLevelCategories");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(json);
            }

            return Categories;
        }

        public async Task<IEnumerable<Unit>> UnitsList()
        {
            IEnumerable<Unit> Units = new List<Unit>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/units");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Units = JsonConvert.DeserializeObject<IEnumerable<Unit>>(json);
            }

            return Units;
        }
        public async Task<IActionResult> GetSingle(long Id = 0)
        {
            Product Product = new Product();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/Products/GetViewProduct/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Product = JsonConvert.DeserializeObject<Product>(json);
            }

            return Json(new
            {
                Product = Product
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product Product)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(Product.Name), "Name");
                content.Add(new StringContent(Product.Price.ToString()), "Price");
                content.Add(new StringContent(Product.CategoryId.ToString()), "CategoryId");
                content.Add(new StringContent(Product.UnitId.ToString()), "UnitId");
                content.Add(new StringContent(Product.IsActive.ToString()), "IsActive");
                content.Add(new StringContent(Product.QuantityDuration.ToString()), "QuantityDuration");

                if (Product.Image != null)
                {
                    var FileContent = new StreamContent(Product.Image.OpenReadStream());
                    FileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Product.Image.ContentType);
                    FileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "Image",
                        FileName = Product.Image.FileName
                    };
                    content.Add(FileContent);
                }


                var response = await Client.PostAsync(Constant.BaseAddress + "api/Products", content);
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
        public async Task<IActionResult> Update(Product Product)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(Product.ID.ToString()), "ID");
                content.Add(new StringContent(Product.Name), "Name");
                content.Add(new StringContent(Product.Price.ToString()), "Price");
                content.Add(new StringContent(Product.CategoryId.ToString()), "CategoryId");
                content.Add(new StringContent(Product.UnitId.ToString()), "UnitId");
                content.Add(new StringContent(Product.IsActive.ToString()), "IsActive");
                content.Add(new StringContent(Product.QuantityDuration.ToString()), "QuantityDuration");

                if (Product.Image != null)
                {
                    var FileContent = new StreamContent(Product.Image.OpenReadStream());
                    FileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Product.Image.ContentType);
                    FileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "Image",
                        FileName = Product.Image.FileName
                    };
                    content.Add(FileContent);
                }


                var response = await Client.PutAsync(Constant.BaseAddress + "api/Products", content);
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
            HttpResponseMessage response = await Client.DeleteAsync($"{Constant.BaseAddress}api/products/{Id}");
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
