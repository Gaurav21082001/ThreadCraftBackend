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
    public class CategoriesController : ControllerBase
    {
        private readonly IProduct productRepo;

        public CategoriesController(IProduct productRepo)
        {
            this.productRepo = productRepo;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            try
            {
                var result = await productRepo.GetAllCategories();
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

        // GET: api/Categories/5
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var category = await productRepo.GetCategoriesByCategoryId(id);

                if (category != null)
                {
                    return Ok(category);
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            return NoContent();
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCategory(int id,[FromBody] Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            

            try
            {
                await productRepo.UpdateCategory(id,category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Category>> PostCategory([FromBody]Category category)
        {
            try
            {
                await productRepo.AddCategory(category);

                return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await productRepo.DeleteCategory(id);
                if (category != null)
                {
                    return Ok("Deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
           
            

            
        }

        private bool CategoryExists(int id)
        {
            return productRepo.CategoryExists(id);
        }
}
}
