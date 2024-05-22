using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFirst.Infrastucture.Service;
using ProjectFirst.Models;
using System.Security.Claims;

namespace ProjectFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   /* [Authorize(Roles = "Customer")]*/
    public class Order_ItemsController : ControllerBase
    {
        private readonly IOrder_Item order_ItemRepo;
        private readonly ICart cartRepo;

        public Order_ItemsController(IOrder_Item order_ItemRepo, ICart cartRepo)
        {
            this.order_ItemRepo = order_ItemRepo;
            this.cartRepo = cartRepo;
        }

        [HttpPost("{orderId}")]
        [Authorize(Roles = "Customer")]

        public  IActionResult AddOrderedItems([FromRoute]Guid orderId)
        {
            var id = int.Parse(HttpContext.User.FindFirst(c => c.Type == "UserId").Value);
            try
            {
                order_ItemRepo.AddOrderdItems(id, orderId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AllOrders")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetOrderedItems()
        {
            try
            {
                var result = order_ItemRepo.GetAllOrders();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }



        [HttpGet]
        [Route("getOrderItems")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetOrderItems()
        {
            int id = int.Parse(HttpContext.User.FindFirst(t => t.Type == "UserId").Value);
            try
            {
                var result = cartRepo.GetAllCartItems(id);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{orderId}")]
        
        public IActionResult GetOrderedItems(Guid orderId)
        {
            try
            {
                var result = order_ItemRepo.GetOrderedItems(orderId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("MyOrders")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetMyOrders()
        {
            
            int id = int.Parse(HttpContext.User.FindFirst(t => t.Type == "UserId").Value);
            try
            {
                var result = order_ItemRepo.GetMyOrders(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
