using Microsoft.EntityFrameworkCore;
using ProjectFirst.Infrastucture.Service;
using ProjectFirst.Models;

namespace ProjectFirst.Infrastucture.Implementation
{
    public class Order_ItemsRepository : IOrder_Item
    {
        private readonly ProjectDbContext context;
        
        public Order_ItemsRepository(ProjectDbContext context)
        {
            this.context = context;
        }
        public void AddOrderdItems(int userId,Guid orderId)
        {
            var cartItems=context.Carts.Where(t => t.UserId == userId).ToList();
            foreach (var cartItem in cartItems)
            {
                
                var quantity = context.Products.FirstOrDefault(t => t.ProductId == cartItem.ProductId).Stock;
                if (quantity >=cartItem.Quantity)
                {
                    var orderItem = new Order_Items
                    {

                        ProductId = cartItem.ProductId,
                        OrderId = orderId,
                        ProductName = context.Products.FirstOrDefault(t => t.ProductId == cartItem.ProductId).ProductName,
                        UserId = userId,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.UnitPrice,
                        TotalAmount = cartItem.Quantity * cartItem.UnitPrice,

                    };
                    context.Products.FirstOrDefault(t => t.ProductId == cartItem.ProductId).Stock = quantity - cartItem.Quantity;
                    context.Order_Items.Add(orderItem);
                }
                continue;

            }
            context.SaveChanges();
        }
        public async Task<IEnumerable<Order_Items>> GetOrderedItems(Guid orderId)
        {
           var result= context.Order_Items.Where(t=>t.OrderId==orderId).ToList();

            return result;
        }

        public IEnumerable<Order_Items> GetMyOrders(int userId)
        {
            var result= context.Order_Items.Where(t=>t.UserId==userId).ToList();
            return result;
        }

        public IEnumerable<Order_Items> GetAllOrders()
        {
            var result =  context.Order_Items.ToList();
            return result;
        }
    }
}
