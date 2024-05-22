using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProjectFirst.DTO;
using ProjectFirst.Infrastucture.Service;
using ProjectFirst.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectFirst.Infrastucture.Implementation
{
    public class UserRepository : IUser
    {
        private readonly ProjectDbContext context;
        private readonly IConfiguration configuration;

        public UserRepository(ProjectDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await context.Users.Where(x=>x.Role!="Admin").ToListAsync();
        }
        public async Task<User> AddUserAsync(User user)
        {
            if(UserExist(user))
            {
                return null;
            }
            else
            {
                var result = await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return result.Entity;
            }
             
        }

        public LoginResponseDTO Login(LoginRequestDTO loginDto)
        {
            var userExist=  context.Users.FirstOrDefault(t=>t.Email == loginDto.Email && EF.Functions.Collate(t.Password, "SQL_Latin1_General_CP1_CS_AS") == loginDto.Password);
            if (userExist != null)
            {
               var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
               var credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email,userExist.Email),
                    new Claim("UserId",userExist.UserId.ToString()),
                    new Claim(ClaimTypes.Role,userExist.Role)
                };
                var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(30),signingCredentials: credentials);
               
                var jwtToken= new JwtSecurityTokenHandler().WriteToken(token);
               
                var response = new LoginResponseDTO
                {
                    Token = jwtToken,
                };
                return response;
            }
            return null;
        }

        public async Task<User> UpdateUserDetail(int id,User user)
        {

            var result = await context.Users.FirstOrDefaultAsync(t => t.UserId == id);
            if(result != null)
            {
                result.Name= user.Name;
                result.Email= user.Email;
                result.Password= user.Password;
                result.PhoneNumber= user.PhoneNumber;
                result.Address= user.Address;

                await context.SaveChangesAsync();
                return result;
            }   
            return null;
        }

        public async Task<User> GetUserDetails(int userId)
        {
            var result = await context.Users.FirstOrDefaultAsync(t => t.UserId == userId);
            if(result != null)
            {
                return result;
            }
            return null;
        }
        public void setUserAddress(string addressObj,int userId)
        {
            var result =  context.Users.FirstOrDefault(t => t.UserId == userId);
            if(result != null)
            {
                result.Address= addressObj;
            }

            context.SaveChanges();

        }

        private bool UserExist(User user)
        {
            return context.Users.Any(t=>t.Email==user.Email);
        }
    }
}
