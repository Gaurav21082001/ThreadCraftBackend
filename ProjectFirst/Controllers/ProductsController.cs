using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFirst.Infrastucture.Service;
using ProjectFirst.Models;

namespace ProjectFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct productRepo;

        public ProductsController(IProduct productRepo)
        {
            this.productRepo = productRepo;
        }

        // GET: api/Products
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                var result = await productRepo.GetAllProducts();
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            return NoContent();
            
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = await productRepo.GetProductById(id);

                if (product != null)
                {
                    return Ok(product);
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

            return NoContent();
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
           
            try
            {
                await productRepo.UpdateProduct(id, product);
                 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            try
            {
                var result = await productRepo.AddProduct(product);

                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await productRepo.DeleteProduct(id);
                if (product == null)
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
                
            return Ok();
        }

        private bool ProductExists(int id)
        {
            return productRepo.ProductExists(id);
        }

        [HttpGet]
        [Route("search/{searchItem}")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProductByNameOrDesc(string searchItem)
        {
            try
            {
                var result = await productRepo.SearchProduct(searchItem);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            return NoContent();
        }

        [HttpGet("GetAllProductsByCategoryId")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                var result = await productRepo.GetProductsByCategoryId(categoryId);
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
