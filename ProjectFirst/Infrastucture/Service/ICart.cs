using ProjectFirst.Infrastucture.Implementation;
using ProjectFirst.Models;

namespace ProjectFirst.Infrastucture.Service
{
    public interface ICart
    {
       
        Task<Cart> AddToCart(int productId, int userId);
        List<Cart> GetAllCartItems(int userId);
        
        int GetToTalPrice(int userId);
        Task<Cart> RemoveItem(int productId, int userId);

        void UpdateItem(int productId, int userId,int quantity);

        void EmptyCart(int userId);

        


    }
}
