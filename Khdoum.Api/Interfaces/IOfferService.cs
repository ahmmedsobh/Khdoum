﻿using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Khdoum.Api.Models.ViewModels.AppViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IOfferService
    {
        Task<IEnumerable<ProductOfferViewModel>> GetOffers();
        Task<IEnumerable<OffersViewModel>> GetOffersForMobileApp();
        Task<ProductOfferViewModel> GetOffer(int OfferId);
        Task<bool> AddOffer(ProductOffer Offer);
        Task<bool> UpdateOffer(ProductOffer Offer);
        Task<bool> DeleteOffer(int OfferId);
    }
}
