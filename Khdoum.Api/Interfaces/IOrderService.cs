using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetOrders();
        Task<IEnumerable<Order>> GetOrdersWithoutDetails();
        Task<IEnumerable<Order>> GetOrdersByStatusWithoutDetailsForUser(string UserId,int Status);
        Task<OrderViewModel> GetOrder(long OrderId);
        Task<bool> AddOrder(OrderViewModel Order);
        Task<bool> UpdateOrderHeader(Order Order);
        Task<bool> DeleteOrder(long OrderId);
    }
}
