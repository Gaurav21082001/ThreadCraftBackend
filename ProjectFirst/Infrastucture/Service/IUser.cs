using ProjectFirst.DTO;
using ProjectFirst.Models;
using System.Threading.Tasks;

namespace ProjectFirst.Infrastucture.Service
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> AddUserAsync(User user);

        //Task<User> Login(string email, string password);
        LoginResponseDTO Login(LoginRequestDTO loginDto);
        Task<User> UpdateUserDetail(int id,User user);

        Task<User> GetUserDetails(int userId);

        void setUserAddress(string address,int userId);
    }
}
