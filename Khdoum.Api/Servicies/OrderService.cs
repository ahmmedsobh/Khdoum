﻿using Khdoum.Api.Data;
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
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext context;

        public OrderService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddOrder(OrderViewModel Order)
        {
            await context.Orders.AddAsync(Order.Order);
            var result = await context.SaveChangesAsync();

            if (result > 0)
            {
                foreach(var item in Order.OrderDetails)
                {
                    item.OrderId = Order.Order.ID;

                    var OrderDetails = new OrderDetails()
                    {
                        ProductId = item.ProductId,
                        OrderId = item.OrderId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Value = item.Value,
                    };


                    await context.OrderDetails.AddAsync(OrderDetails);
                }

                if((await context.SaveChangesAsync()) > 0)
                {
                    return await Task.FromResult(true);
                }
            }

            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteOrder(long OrderId)
        {
            var result = await context.Orders
                .FirstOrDefaultAsync(o => o.ID == OrderId);
            if (result != null)
            {
                context.Orders.Remove(result);
                var orderDetails = context.OrderDetails.Where(od => od.OrderId == OrderId);
                context.OrderDetails.RemoveRange(orderDetails);
                if(await context.SaveChangesAsync() > 0)
                {
                    return await Task.FromResult(true);
                }
            }

            return await Task.FromResult(false);
        }

        public async Task<OrderViewModel> GetOrder(long OrderId)
        {
            OrderViewModel order = new OrderViewModel();
            order.Order = await context.Orders.FirstOrDefaultAsync(o => o.ID == OrderId);
            order.OrderDetails =await (from od in context.OrderDetails
                                       from o in context.Orders
                                       from p in context.Products
                                       from m in context.Users
                                       from mp in context.MarketProducts
                                       where o.ID == od.OrderId
                                       && p.ID == od.ProductId
                                       && p.ID == mp.ProductId
                                       && m.Id == mp.UserId
                                       && od.OrderId == OrderId
                                  select new OrderDetailsViewModel
                                  {
                                      ProductId = od.ProductId,
                                      OrderId = od.OrderId,
                                      Quantity = od.Quantity,
                                      Price = od.Price,
                                      Value = od.Value,
                                      MarketName = m.Name,
                                      Name = p.Name,
                                      ImgUrl = p.ImgUrl == "false" ? $"{Constants.BaseAddress}Uploads/default.png" : $"{Constants.BaseAddress}Uploads/Products/{p.ImgUrl}"
                                  }).ToListAsync();
            return order;
        }

        public Task<IEnumerable<OrderViewModel>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersWithoutDetails()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersWithoutDetailsForUser(string UserId)
        {
            var orders = await context.Orders.Where(o => o.UserId == UserId).ToListAsync();
            return orders;
        }

        public async Task<bool> UpdateOrderHeader(Order Order)
        {
            var OrderToUpdate = await context.Orders.FirstOrDefaultAsync(o => o.ID == Order.ID);
            if(OrderToUpdate != null)
            {
                OrderToUpdate.Status = Order.Status;
                context.Orders.Update(OrderToUpdate);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);

        }
    }
}