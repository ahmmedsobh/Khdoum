using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IMarketService
    {
        Task<IEnumerable<MarketViewModel>> GetMarkets();
        Task<MarketViewModel> GetMarket(string MarketId);
        Task<MarketViewModel> AddMarket(MarketViewModel Market);
        Task<MarketViewModel> UpdateMarket(MarketViewModel Market);
        Task<MarketViewModel> DeleteMarket(string MarketId);
        Task<IEnumerable<MarketProductsViewModel>> GetMarketProducts(string MarketId);
        Task<bool> AddMarketProducts(List<MarketProductsViewModel> Products);
    }
}
