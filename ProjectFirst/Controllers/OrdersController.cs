using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFirst.Infrastucture.Service;

namespace ProjectFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrdersController : ControllerBase
    {
        private readonly IOrder order;

        public OrdersController(IOrder order)
        {
            this.order = order;
        }

        [HttpPost]
        [Route("AddOrder")]
        [Authorize(Roles = "Customer")]
        public IActionResult BuyNow() {

            try
            {
                var result=order.BuyNow(GetIdFromToken());
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }


        [HttpGet]
        [Route("AllOrders")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var result = order.getAllOrders();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("UpdateStatus")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateOrderStatus(Guid orderId,string status)
        {
            try
            {
                order.updateStatus(orderId, status);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        private int GetIdFromToken()
        {
            var id = HttpContext.User.FindFirst("userId").Value;
            return int.Parse(id);
        }

        [HttpGet]
        [Route("getStatus")]
        [Authorize(Roles ="Customer")]

        public IActionResult getOrderStatusByOrderId(Guid orderId)
        {
            try
            {
                var result = order.getOrderStatusByOrderId(orderId);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
           
        }

       
    }
}
