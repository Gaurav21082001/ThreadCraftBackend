using Microsoft.EntityFrameworkCore;
using ProjectFirst.Infrastucture.Service;
using ProjectFirst.Models;

namespace ProjectFirst.Infrastucture.Implementation
{
    public class CartRepository : ICart
    {
        private readonly ProjectDbContext context;
        public CartRepository(ProjectDbContext context) { 
            this.context = context;
        }
        public List<Cart> GetAllCartItems(int userId)
        {
            return  context.Carts.Where(t=>t.UserId==userId).ToList();
        }
        public async Task<Cart> AddToCart(int productId,int userId)
        {

            var productExist = await context.Carts.FirstOrDefaultAsync(t => t.ProductId == productId && t.UserId == userId);
            if (productExist == null)
            {
                productExist = new Cart
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = 1,
                    Product = context.Products.SingleOrDefault(t => t.ProductId == productId),
                    UnitPrice = context.Products.SingleOrDefault(t => t.ProductId == productId).Price

                };
                await context.Carts.AddAsync(productExist);
                
            }
            else
            {
                productExist.Quantity++;
                
            }
             context.SaveChanges();
            return productExist;
        }

        public async Task<Cart> RemoveItem(int productId,int userId)
        {
            var result= context.Carts.FirstOrDefault(t=>t.ProductId==productId && t.UserId==userId);
            if (result != null)
            {
                context.Carts.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }


        public int GetToTalPrice(int userId)
        {
            int sum = 0;
            var items= GetAllCartItems(userId);
            foreach (var item in items)
            {
                sum += item.UnitPrice*item.Quantity;
            }
            return sum;
        }

        public void UpdateItem(int userId, int productId,int quantity)
        {
            
                var myItem = context.Carts.FirstOrDefault(t => t.UserId == userId && t.ProductId == productId);
                if (myItem != null)
                {
                    myItem.Quantity = quantity;
                    
                }
            context.SaveChanges();
        }

        public void EmptyCart(int userId)
        {
            var cartItems = context.Carts.Where(
          c => c.UserId == userId);
            foreach (var cartItem in cartItems)
            {
                context.Carts.Remove(cartItem);
            }
            // Save changes.             
            context.SaveChanges();
        }

       
    }

  
}
