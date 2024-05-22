using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFirst.DTO;
using ProjectFirst.Infrastucture.Service;
using ProjectFirst.Models;

namespace ProjectFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser userInterface;

        public UserController(IUser userInterface)
        {
            this.userInterface = userInterface;

        }

        

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            try
            {
                var result = await userInterface.GetAll();
                if (result != null)
                {
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
           
            return NotFound();
        }

        [HttpGet("details")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<User>> GetUserDetailsById()
        {
            int id = int.Parse(HttpContext.User.FindFirst("userId").Value);
            try
            {
                var result = await userInterface.GetUserDetails(id);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)
        {
            int id = int.Parse(HttpContext.User.FindFirst("userId").Value);

            try
            {
                var result = await userInterface.UpdateUserDetail(id, user);
                if (result != null)
                {
                    return Ok(result);
                }
            }
           catch(Exception ex)
            {
                return  BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpPost("login")]
        public ActionResult<LoginResponseDTO> Login([FromBody]LoginRequestDTO loginDto)
        {

            try
            {
                var result = userInterface.Login(loginDto);

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

        [HttpPost("address")]
        [Authorize(Roles="Customer")]
        public IActionResult SetAddress(string address)
        {
            int id = int.Parse(HttpContext.User.FindFirst("userId").Value);
            try
            {
                userInterface.setUserAddress(address, id);

                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody]User user)
        {
            try
            {
                var result = await userInterface.AddUserAsync(user);
                if (result != null)
                {
                    return Ok(result);
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest();
            
        }



    }
}
