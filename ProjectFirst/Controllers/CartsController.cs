using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFirst.Infrastucture.Implementation;
using ProjectFirst.Infrastucture.Service;
using ProjectFirst.Models;

namespace ProjectFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Customer")]
    public class CartsController : ControllerBase
    {
        private readonly ICart cartRepo;

        public CartsController(ICart cartRepo)
        {
            this.cartRepo = cartRepo;
        }

        // GET: api/Carts
        [HttpGet]
        [Route("GetCartItems")]
       
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            try
            {
                var result = cartRepo.GetAllCartItems(GetIdFromToken());
                if(result == null)
                {
                    return NoContent();
                }
                return Ok(result);
            }
           catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("AddItemToCart/{productId}")]
        
        public async Task<ActionResult<Cart>> PostCart(int productId)
        {
            try
            {
                var result = await cartRepo.AddToCart(productId, GetIdFromToken());
                if (result == null)
                {
                    return NoContent();
                }

                return Ok(result);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        // DELETE: api/Carts/5
        [HttpDelete]
        [Route("RemoveItem/{productId}")]
        
        public async Task<IActionResult> RemoveItem( int productId)
        {
            try
            {
                var cart = await cartRepo.RemoveItem(productId, GetIdFromToken());
                if (cart == null)
                {
                    return NotFound();
                }
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

        [HttpPut]
        [Route("updateItem/{productId}")]
     
        public IActionResult UpdateItem([FromRoute] int productId,[FromBody]int quantity)
        {
            try
            {
                cartRepo.UpdateItem(GetIdFromToken(), productId, quantity);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        [Route("EmptyCart")]
      
        public IActionResult EmptyCart()
        {
            try
            {
                cartRepo.EmptyCart(GetIdFromToken());
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        
       
    }
}
