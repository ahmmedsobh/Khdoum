using Khdoum.Api.Data;
using Khdoum.Api.Helpers;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Khdoum.Api.Models.ViewModels.AppViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Servicies
{
    public class OffersService : IOfferService
    {
        private readonly ApplicationDbContext context;

        public OffersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddOffer(ProductOffer Offer)
        {
            context.ProductOffers.Add(Offer);
            var Result = await SaveChangesAsync();
            return await Task.FromResult(Result);
        }

        public async Task<bool> DeleteOffer(int OfferId)
        {
            var Offer = await context.ProductOffers.FirstOrDefaultAsync(o=>o.Id == OfferId);
            context.ProductOffers.Remove(Offer);
            var Result = await SaveChangesAsync();
            return await Task.FromResult(Result);
        }

        public async Task<ProductOfferViewModel> GetOffer(int OfferId)
        {
            var offer = (from o in context.ProductOffers
                        join mp in context.MarketProducts on o.MarketProductsID equals mp.ID
                        join p in context.Products on mp.ProductId equals p.ID
                        where o.Id == OfferId
                        select new ProductOfferViewModel
                        {
                            Id = o.Id,
                            Name = p.Name,
                            MaximumUseCount = o.MaximumUseCount,
                            UsedCount = o.UsedCount,
                            Discount = o.Discount,
                            DiscountType = o.DiscountType,
                            IsActive = o.IsActive,
                            ExpiryDate = o.ExpiryDate,
                            Img = p.ImgUrl == "false" ? $"{Constants.BaseAddress}Uploads/default.png" : $"{Constants.BaseAddress}Uploads/Products/{p.ImgUrl}",
                            MarketProductsID = o.MarketProductsID
                        }).FirstOrDefaultAsync();

            return await offer;
        }

        public async Task<IEnumerable<ProductOfferViewModel>> GetOffers()
        {
            var offers = await (from o in context.ProductOffers
                         join mp in context.MarketProducts on o.MarketProductsID equals mp.ID
                         join p in context.Products on mp.ProductId equals p.ID
                         select new ProductOfferViewModel
                         {
                             Id = o.Id,
                             Name = p.Name,
                             MaximumUseCount = o.MaximumUseCount,
                             UsedCount = o.UsedCount,
                             Discount = o.Discount,
                             DiscountType = o.DiscountType,
                             IsActive = o.IsActive,
                             ExpiryDate = o.ExpiryDate,
                             Img = p.ImgUrl == "false" ? $"{Constants.BaseAddress}Uploads/default.png" : $"{Constants.BaseAddress}Uploads/Products/{p.ImgUrl}",
                             MarketProductsID = o.MarketProductsID
                         }).ToListAsync();

            return  offers;
        }

        public async Task<IEnumerable<OffersViewModel>> GetOffersForMobileApp()
        {
            var Offers = (from mp in context.MarketProducts
                            join p in context.Products on mp.ProductId equals p.ID
                            join m in context.Users on mp.UserId equals m.Id
                            join c in context.Categories on p.CategoryId equals c.ID
                            join u in context.Units on p.UnitId equals u.ID 
                            join s in context.States on m.StateId equals s.ID
                            join o in context.ProductOffers on mp.ID equals o.MarketProductsID
                            select new OffersViewModel
                            {
                                ID = mp.ID,
                                Name = p.Name,
                                Price = CalculateDiscount(o.Discount,mp.Price,o.DiscountType),
                                ImgUrl = p.ImgUrl == "false" ? $"{Constants.BaseAddress}Uploads/default.png" : $"{Constants.BaseAddress}Uploads/Products/{p.ImgUrl}",
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
                                DeliveryService = s.DeliveryService,
                                Discount = o.Discount,
                                DiscountType = o.DiscountType
                            }).ToListAsync();

            return await Offers;
        }

        public async Task<bool> UpdateOffer(ProductOffer offer)
        {
            context.ProductOffers.Update(offer);
            var Result = await SaveChangesAsync();
            return await Task.FromResult(Result);
        }

        async Task<bool> SaveChangesAsync()
        {
            var Result = await context.SaveChangesAsync() > 0 ? true : false;
            return Result;
        }

        static decimal CalculateDiscount(int Discount, decimal Price,int DiscountType)
        {
            decimal PriceAfterDiscount = Price;
            if (DiscountType == 1)
            {
                decimal DiscountPercent = decimal.Parse(Discount.ToString()) / 100;
                decimal DiscountValue = Price * DiscountPercent;
                PriceAfterDiscount = Price - DiscountValue;
            }
            else if (DiscountType == 2)
            {
                PriceAfterDiscount = Price - Discount;
            }

            return Math.Round(PriceAfterDiscount, 2);
        }

    }
}
