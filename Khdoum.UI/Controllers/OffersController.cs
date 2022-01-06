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
    public class OffersController : Controller
    {
        public IActionResult Index()
        {
            var model = new ProductOffer();
            return View(model);
        }

       

        public async Task<IActionResult> GetList()
        {
            IEnumerable<ProductOffer> Offers = await OffersList();
            return Json(new
            {
                Offers = Offers
            });
        }

        public async Task<IEnumerable<ProductOffer>> OffersList()
        {
            IEnumerable<ProductOffer> offers = new List<ProductOffer>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/offers");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                offers = JsonConvert.DeserializeObject<IEnumerable<ProductOffer>>(json);
            }

            return offers;
        }

        public async Task<IActionResult> GetSingle(int Id = 0)
        {
            ProductOffer Offer = new ProductOffer();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/offers/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Offer = JsonConvert.DeserializeObject<ProductOffer>(json);
            }

            return Json(new
            {
                Offer = Offer
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductOffer offer)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(offer.MaximumUseCount.ToString()), "MaximumUseCount");
                content.Add(new StringContent(offer.UsedCount.ToString()), "UsedCount");
                content.Add(new StringContent(offer.Discount.ToString()), "Discount");
                content.Add(new StringContent(offer.DiscountType.ToString()), "DiscountType");
                content.Add(new StringContent(offer.ExpiryDate.ToString()), "ExpiryDate");
                content.Add(new StringContent(offer.MarketProductsID.ToString()), "MarketProductsID");
                content.Add(new StringContent(offer.IsActive.ToString()), "IsActive");
                
                var response = await Client.PostAsync(Constant.BaseAddress + "api/Offers", content);
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

        public async Task<IActionResult> Update(ProductOffer offer)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(offer.Id.ToString()), "Id");
                content.Add(new StringContent(offer.MaximumUseCount.ToString()), "MaximumUseCount");
                content.Add(new StringContent(offer.UsedCount.ToString()), "UsedCount");
                content.Add(new StringContent(offer.Discount.ToString()), "Discount");
                content.Add(new StringContent(offer.DiscountType.ToString()), "DiscountType");
                content.Add(new StringContent(offer.ExpiryDate.ToString()), "ExpiryDate");
                content.Add(new StringContent(offer.MarketProductsID.ToString()), "MarketProductsID");
                content.Add(new StringContent(offer.IsActive.ToString()), "IsActive");
               


                var response = await Client.PutAsync(Constant.BaseAddress + "api/Offers", content);
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
        public async Task<IActionResult> Delete(int Id)
        {
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.DeleteAsync($"{Constant.BaseAddress}api/Offers/{Id}");
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

        public async Task<IActionResult> ProductsList(string term)
        {
            IEnumerable<Product> products = new List<Product>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/products/GetAllMarketsProducts/{term}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
            }

            return Json(new
            {
                products = products.Select(p=> new { id=p.ID,text=$"{p.Name}({p.Price})({p.MarketName})"})
            });
        }
    }
}
