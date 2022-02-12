using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Khdoum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService Orders;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICurrentUserService currentUser;

        public OrdersController(IOrderService Orders, UserManager<ApplicationUser> userManager,ICurrentUserService currentUser)
        {
            this.Orders = Orders;
            this.userManager = userManager;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrdersWithoutDetails()
        {
            try
            {
                return Ok(await Orders.GetOrdersWithoutDetails());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [Authorize]
        [HttpGet("GetOrdersByStatusWithoutDetailsForUser/{Status}")]
        public async Task<ActionResult> GetOrdersByStatusWithoutDetailsForUser(int Status = 0 )
        {
            try
            {
                var UserName = HttpContext.User.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault()?.Value;

                var User = await userManager.FindByNameAsync(UserName);
                var UserId = "";

                if (User != null)
                {
                    UserId = User.Id;
                }

                if (UserId == null || UserId == "")
                {
                    return BadRequest();
                }

                var orders = await Orders.GetOrdersByStatusWithoutDetailsForUser(UserId,Status);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [Authorize]
        [HttpGet("GetOrdersByStatusWithoutDetailsForDelegate/{Status}")]
        public async Task<ActionResult> GetOrdersByStatusWithoutDetailsForDelegate(int Status = 0)
        {
            try
            {
                var UserName = HttpContext.User.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault()?.Value;

                var User = await userManager.FindByNameAsync(UserName);
                var UserId = "";

                if (User != null)
                {
                    UserId = User.Id;
                }

                if (UserId == null || UserId == "")
                {
                    return BadRequest();
                }

                var orders = await Orders.GetOrdersByStatusWithoutDetailsForDelegate(UserId, Status);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [Authorize]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<OrderViewModel>> GetOrder(long id)
        {
            try
            {
                var result = await Orders.GetOrder(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderViewModel Order)
        {
            try
            {
                
                var UserId = await currentUser.GetUserId(HttpContext);

                if (string.IsNullOrEmpty(UserId))
                    return BadRequest();

                //Order.Order.UserId = "e7ef2099-baa3-4137-bbef-cd1666face01";
                Order.Order.UserId = UserId;
                var result = await Orders.AddOrder(Order);

                if(result)
                {
                    return Ok();
                }

                return BadRequest();
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new product record");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateOrder(Order order)
        {
            try
            {
                string UserId = await currentUser.GetUserId(HttpContext);

                if(string.IsNullOrEmpty(UserId))
                {
                    return BadRequest();
                }

                order.DeliveryId = UserId;

                var orderToUpdate = await Orders.GetOrder(order.ID);

                if (orderToUpdate == null)
                    return NotFound($"Order with Id = {order.ID} not found");

                var r = await Orders.UpdateOrderHeader(order);
                if(r)
                {
                    return Ok();
                }

                return BadRequest();

               
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteOrder(long id)
        {
            try
            {
                var orderToDelete = await Orders.GetOrder(id);

                if (orderToDelete == null)
                {
                    return NotFound($"Order with Id = {id} not found");
                }

                var r = await Orders.DeleteOrder(id);
                if(r)
                {
                    return Ok();
                }

                return BadRequest();
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        //[HttpGet]
        //[Route("TestDelegateOrders")]
        //public async Task<ActionResult> TestDelegateOrders()
        //{
        //    var o = await Orders.GetOrdersByStatusWithoutDetailsForDelegate("f10b0586-169e-472c-8a57-2c2c04455192",1);
        //    return Ok(o);
        //}
    }
}
