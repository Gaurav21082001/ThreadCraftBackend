using ProjectFirst.Infrastucture.Service;
using ProjectFirst.Models;
using System.Security.Claims;

namespace ProjectFirst.Infrastucture.Implementation
{
    public class OrderRepository : IOrder
    {
        private readonly ProjectDbContext context;
        private readonly ICart cart;
        
        public OrderRepository(ProjectDbContext context,ICart cart)
        {
            this.context = context;
            this.cart = cart;
            
        }

        public Order BuyNow(int userId)
        {
          
           
            var order = new Order
            {
                UserId = userId,
                TotalAmount = GetTotalAmount(userId),
                ShippingCharge = 50,
                NetAmount = GetTotalAmount(userId) + 50
            };
            
            var result=context.Orders.Add(order);
             context.SaveChanges();
            return result.Entity;
        }

        public void EmptyCart(int userId)
        {
            var items = context.Carts.Where(t => t.UserId == userId).ToList();
            foreach (var item in items)
            {
                context.Carts.Remove(item);
            }
        }
       
        public int GetNetAmount(int userId)
        {
            var total = context.Orders.FirstOrDefault(t => t.UserId == userId).NetAmount;
            return total;
             
        }

        public int GetTotalAmount(int userId)
        {
            
            return cart.GetToTalPrice(userId);
        }

        public IEnumerable<Order> getAllOrders()
        {
            return context.Orders.ToList();
        }

        public  void updateStatus(Guid orderId, string status)
        {
            var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if(order != null)
            {
                order.Status = status;
                context.SaveChanges();
            }
            
        }

        public Order getOrderStatusByOrderId(Guid orderId)
        {
            return context.Orders.FirstOrDefault(t => t.OrderId == orderId);
        }
    }
}
